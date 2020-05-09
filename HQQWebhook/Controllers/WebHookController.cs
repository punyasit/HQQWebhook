using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Internal;

using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using HQQWebhook.Model;
using HQQWebhook.Manager;
using Microsoft.Extensions.Logging;

using System.Dynamic;
using HQQWebhook.Model.FacebookMessaging;
using HQQLibrary.Manager;
using HQQLibrary.Model.Models.MaticonDB;

namespace HQQWebhook.Controllers
{
    /*
    * Copyright 2016-present, Punyasit, HelloQQShop.com
    * All rights reserved.
    * All Source code modified from Facebook.com from Express.js
    */

    [Produces("application/json")]
    [Route("webhook")]
    public class WebHookController : Controller
    {
        private readonly IConfiguration WebConfig;
        private readonly ILogger<VersionController> logger;

        private FacebookAPIManager fbAPIMgr;
        private FbWebhookConfig fbConfig = new FbWebhookConfig();
        private DialogflowManager dialogFlowMgr;
        private DialogflowInfo endFlowItem;
        private enum ReplyType { PrivateReply, MessageReply };
        private PayloadResponse endflowPayload;

        public WebHookController(IConfiguration configuration, ILogger<VersionController> log)
        {
            WebConfig = configuration;
            fbConfig = configuration.GetSection("FbWebhookConfig").Get<FbWebhookConfig>();
            var strEndflowPayload = configuration.GetSection("DialogFlow:EndflowPayload").Value;

            this.logger = log;
            fbAPIMgr = new FacebookAPIManager(fbConfig, logger);
            dialogFlowMgr = new DialogflowManager();
            endflowPayload = dialogFlowMgr.GetDirectPayload(strEndflowPayload).FirstOrDefault();
        }

        /*
        * Use your own validation token. Check that the token used in the Webhook
        * setup is the same token used here.
        *
        */
        // GET: api/WebHook/
        [HttpGet]
        public int Get()
        {
            if (Request.Query["hub.mode"] == "subscribe" &&
                Request.Query["hub.verify_token"] == fbConfig.ValidationToken)
            {
                Response.StatusCode = 200;
                return int.Parse(Request.Query["hub.challenge"]);
            }
            else
            {
                Response.StatusCode = 403;
                return 1;
            }
        }

        // POST: api/WebHook
        /*
         * All callbacks for Messenger are POST-ed. They will be sent to the same
         * webhook. Be sure to subscribe your app to your page to receive callbacks
         * for your page.
         * https://developers.facebook.com/docs/messenger-platform/product-overview/setup#subscribe_app
         *
         */
        [HttpPost]
        public void Post([FromBody] dynamic dataContent)
        {
            // dynamic dataContent = content; JObject

            if (dataContent["object"].Value == "page")
            {
                foreach (var pageEntry in dataContent.entry)
                {
                    var pageId = pageEntry.id.Value;
                    var timeOfEvent = pageEntry.id.Value;

                    if (pageEntry.messaging != null && fbConfig.EnabledAutoMessage)
                    {
                        foreach (var messagingEvent in pageEntry.messaging)
                        {
                            if (messagingEvent.message != null)
                            {
                                receivedMessage(messagingEvent);
                            }

                            //# OTHER MEESAGE TYPE #//
                            //########################
                            #region [ UNUSED: OTHER MESSAGE TYPE ]

                            //if (messagingEvent.optin != null)
                            //{
                            //    receivedAuthentication(messagingEvent);
                            //}
                            //else if (messagingEvent.message != null)
                            //{
                            //    receivedMessage(messagingEvent);
                            //}
                            //else if (messagingEvent.deliver != null)
                            //{
                            //    receivedDeliveryConfirmation(messagingEvent);
                            //}
                            //else if (messagingEvent.postback != null)
                            //{
                            //    receivedPostback(messagingEvent);
                            //}
                            //else if (messagingEvent.read != null)
                            //{
                            //    receivedMessageRead(messagingEvent);
                            //}
                            //else if (messagingEvent.account_linking != null)
                            //{
                            //    receivedAccountLink(messagingEvent);
                            //}
                            //else
                            //{
                            //    Console.Write(string.Format("Webhook received unknown messagingEvent: {0} ", messagingEvent));
                            //}

                            #endregion
                        }
                    }
                    else
                    {
                        if (pageEntry != null && fbConfig.EnabledFeedResponse)
                        {
                            dynamic dmPageEntry = (dynamic)pageEntry;
                            if (dmPageEntry.changes != null)
                            {
                                if (dmPageEntry.changes[0].field != null
                                    && dmPageEntry.changes[0].field == "feed")
                                {
                                    recievedFeedAction(dmPageEntry.changes[0].value);
                                }
                            }
                        }
                    }
                }

            }

            HttpContext.Response.StatusCode = 200;
        }

        private void recievedFeedAction(dynamic feedResponse)
        {
            if (feedResponse.verb == "add")
            {
                string strUserId;
                Random rand = new Random();
                FbFeedResponse feedResp = new FbFeedResponse();

                feedResp.PostID = feedResponse.post_id;
                feedResp.CommentID = feedResponse.comment_id;
                feedResp.Message = feedResponse.message;
                feedResp.FromName = feedResponse.from.name;
                feedResp.PostURL = feedResponse.permalink_url;

                strUserId = feedResponse.from.id;

                //# Validate existing sender Id from Database.
                //# If not exist, call reciepient id from API

                PrivateReply privateReply = dialogFlowMgr.GetPrivateReply(feedResp.Message);

                try
                {
                    if (!string.IsNullOrEmpty(feedResp.CommentID)
                        && (privateReply != null
                        && privateReply.ReplyMessages.Count > 0))
                    {
                        string replyMessage = string.Empty;
                        var selectedItem = 0;
                        if (privateReply.ReplyMessages.Count() > 1)
                        {
                            selectedItem = rand.Next(0, privateReply.ReplyMessages.Count);
                            replyMessage = privateReply.ReplyMessages[selectedItem];
                        }
                        else
                        {
                            replyMessage = privateReply.ReplyMessages[0];
                        }

                        //# PRIVATE REPLY
                        //SendPrivateReply(feedResp, replyMsg);
                        SendMessageQuickReply(
                            ReplyType.PrivateReply,
                            feedResp.CommentID,
                            replyMessage,
                            privateReply.PayloadResponses);

                        //# NOTIFY COMMENT
                        ReplyComment(feedResp, RandomAnswer(privateReply.ResponseComments));
                    }
                }
                catch (Exception ex)
                {
                    logger.LogInformation(ConstInfo.LOG_TRACE_PREFIX +
                        string.Format(" recievedFeedAction, Ex: {0}, Source: {1}"
                        , ex.Message
                        , JsonConvert.SerializeObject(feedResp))
                        );
                }
            }
        }

        public void verifyRequestSignature()
        {
            string signature = Request.Headers["x-hub-signature"];

            if (signature != null)
            {
                Console.Write("Couldn't validate the signature.");
            }
            else
            {
                var elements = signature.Split('=');
                var method = elements[0];
                var signatureHash = elements[1];

                //var expectedHash = crypto.createHmac('sha1', APP_SECRET)
                //                    .update(buf)
                //                    .digest('hex');

                //if (signatureHash != expectedHash)
                //{
                //    throw new Error("Couldn't validate the request signature.");
                //}
            }
        }

        private void receivedAccountLink(object messagingEvent)
        {
            throw new NotImplementedException();
        }

        private void receivedMessageRead(object messagingEvent)
        {
            throw new NotImplementedException();
        }

        private void receivedPostback(object messagingEvent)
        {
            throw new NotImplementedException();
        }

        private void receivedDeliveryConfirmation(object messagingEvent)
        {
            throw new NotImplementedException();
        }

        private void receivedMessage(dynamic messagingEvent)
        {
            var senderID = messagingEvent.sender.id;
            var message = messagingEvent.message;
            string isEcho = message.is_echo;
            string messageText = message.text;
            string respHeader = string.Empty;

            #region[UNUSED : MESSAGE TYPE THAT MAY BE USE IN THE FUTURE]
            //var recipientID = messagingEvent.recipient.id;
            //var timeOfMessage = messagingEvent.timestamp;
            //string messageId = message.mid;
            //string appId = message.app_id;
            //string metadata = message.metadata;

            //# USE FOR IMAGE CHECKING AND PROCESSING
            //# You may get a text or attachment but not both
            //# Try to get attachment.

            //string messageAttachments = string.Empty;
            //try
            //{
            //    messageAttachments = message.attachments;
            //}
            //catch (Exception)
            //{
            //}
            #endregion

            var quickReply = message.quick_reply;
            if (isEcho != null)
            {
                // Just logging message echoes to console
                //console.log("Received echo for message %s and app %d with metadata %s",
                //messageId, appId, metadata);
                return;
            }
            else if (quickReply != null)
            {
                var quickReplyPayload = quickReply.payload.ToString();

                //# PROCCSS QUICK REPLY / LOAD DATA AND REPLY BACK 
                DialogflowInfo dialogAnswer = dialogFlowMgr.GetDialogFromPayload(quickReplyPayload);

                if (dialogAnswer.ResponseProducts != null
                    && dialogAnswer.ResponseProducts.Count() > 0)
                {
                    respHeader = RandomAnswer(dialogAnswer.ResponseHeader);
                    SendTextMessage(senderID, respHeader);
                    SendMessageTemplate(senderID, dialogAnswer.ResponseProducts);
                }

                if (dialogAnswer.PayloadResponses != null
                     && dialogAnswer.PayloadResponses.Count > 0)
                {
                    SendMessageQuickReply(ReplyType.MessageReply,
                        senderID, respHeader,
                        dialogAnswer.PayloadResponses);
                }

                return;
            }

            if (!string.IsNullOrEmpty(messageText))
            {
                // If we receive a text message, check to see if it matches any special
                // keywords and send back the corresponding example. Otherwise, just echo
                // the text we received.
                Regex rgx = new Regex(@"/[^\w\s] / gi");
                string keyword = rgx.Replace(messageText, "").Trim().ToLower();
                var dialogAnswer = dialogFlowMgr.GetDialogFromKeyword(keyword);

                if (dialogAnswer != null)
                {
                    SendMessageQuickReply(ReplyType.MessageReply,
                          senderID, RandomAnswer(dialogAnswer.ResponseHeader), dialogAnswer.PayloadResponses);
                }
            }
        }
        private string RandomAnswer(List<string> lstAnswer)
        {
            return lstAnswer[new Random().Next(0, ((lstAnswer.Count() - 1) < 0 ? 0 : lstAnswer.Count()))];
        }

        private void SendMessageQuickReply(
            ReplyType replyType,
            dynamic reciepientId,
            string replyMessage,
            List<PayloadResponse> payloadResp)
        {
            MessageData messageData = new MessageData();
            if (replyType == ReplyType.PrivateReply)
            {
                messageData.recipient = new Recipient()
                {
                    comment_id = reciepientId
                };
            }
            else
            {
                messageData.recipient = new Recipient()
                {
                    id = reciepientId
                };
                messageData.messaging_type = "RESPONSE";
            }

            messageData.message = new Message()
            {
                text = replyMessage
            };

            if (payloadResp.Count > 0)
            {
                List<Quick_replies> lstQuickReply = new List<Quick_replies>();
                string respWording;
                foreach (var item in payloadResp.OrderBy(item => item.Id))
                {
                    if (item.ResponseAnswer.Count > 1)
                    {
                        respWording = RandomAnswer(item.ResponseAnswer);
                    }
                    else
                    {
                        respWording = item.ResponseAnswer[0];
                    }

                    if (!string.IsNullOrEmpty(item.ImageURL))
                    {
                        lstQuickReply.Add(new Quick_replies()
                        {
                            content_type = "text",
                            title = respWording,
                            payload = item.Payload,
                            image_url = item.ImageURL
                        });
                    }
                    else
                    {
                        lstQuickReply.Add(new Quick_replies()
                        {
                            content_type = "text",
                            title = respWording,
                            payload = item.Payload,

                        });
                    }
                }

                messageData.message.quick_replies = lstQuickReply;
            }

            fbAPIMgr.CallSendAPI(messageData);
        }

        private void SendPrivateReply(
            FbFeedResponse fResp,
            string messageText)
        {
            var messageData = new
            {
                recipient = new
                {
                    comment_id = fResp.CommentID
                    //,post_id = fResp.PostID
                },

                message = new
                {
                    text = messageText,
                    metadata = "HQQ_AUTO_PRIVATE_MESSAGE"
                    #region [Payload]
                    //,attachment = new
                    //{
                    //    type = "template",
                    //    payload = new
                    //    {
                    //        template_type = "media",
                    //        elements = new[] {
                    //         new  {
                    //                media_type = "image",
                    //                url = fResp.PostURL
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion
                }
            };

            fbAPIMgr.CallSendAPI(messageData);
        }

        private void SendMessageTemplate(
            dynamic recipientId,
            List<HqqProduct> lstProduct)
        {
            MessageData msgData = new MessageData();
            Attachment attachment = new Attachment();
            Payload payload = new Payload();
            List<Elements> lstElProduct = new List<Elements>();
            Elements elProduct = new Elements();

            msgData.recipient = new Recipient()
            {
                id = recipientId
            };

            msgData.message = new Message();
            attachment.type = "template";
            payload.template_type = "generic";

            foreach (var item in lstProduct)
            {
                var hqqDefaultPrice = item.HqqPrice.Where(pi => pi.Channel.Name == "Facebook").FirstOrDefault();
                var hqqOtherPrice = item.HqqPrice.Where(pi => pi.Channel.Name != "Facebook").ToList();

                elProduct = new Elements()
                {
                    title = item.Name,
                    image_url = item.PreviewImageUrl,
                    subtitle = item.Description + string.Format(@"- ราคา:{0:n0}บาท", hqqDefaultPrice.Price),
                    default_action = new Default_action()
                    {
                        type = "web_url",
                        url = item.InformationUrl,
                        webview_height_ratio = "tall"
                    }
                };

                List<Buttons> buttons = new List<Buttons>();
                foreach (var othrPrice in hqqOtherPrice)
                {
                    buttons.Add(new Buttons()
                    {
                        type = "web_url",
                        url = othrPrice.AffiliateUrl,
                        title = othrPrice.Channel.Name
                    });
                }

                if(item.HqqDialogflowAddon != null
                    &&item.HqqDialogflowAddon.ToList().Count > 0){
                    foreach (var hdfa in item.HqqDialogflowAddon.ToList())
                    {
                        buttons.Add(new Buttons()
                        {
                            type = hdfa.Type,
                            url = hdfa.Url,
                            title = hdfa.Name
                        });
                    }
                }

                buttons.Add(new Buttons()
                {
                    type = "postback",
                    title = RandomAnswer(endflowPayload.ResponseAnswer),
                    payload = endflowPayload.Payload
                });

                elProduct.buttons = buttons;
                lstElProduct.Add(elProduct);
            }

            payload.elements = lstElProduct;
            attachment.payload = payload;
            msgData.message.attachment = attachment;

            fbAPIMgr.CallSendAPI(msgData);
        }

        private void sendImageMessage(dynamic recipientId)
        {
            var messageData = new
            {
                recipient = new
                {
                    id = recipientId
                },
                message = new
                {
                    attachment =
                new
                {
                    type = "image",
                    payload =
                    new
                    {
                        url = fbConfig.ServerURL + "/assets/rift.png"
                    }
                }
                }
            };
            fbAPIMgr.CallSendAPI(messageData);
        }

        private void sendTypingOff(dynamic recipientId)
        {
            var messageData = new
            {
                recipient = new
                {
                    id = recipientId
                },
                sender_action = "typing_off"
            };
            fbAPIMgr.CallSendAPI(messageData);
        }

        private void sendTypingOn(dynamic recipientId)
        {
            var messageData = new
            {
                recipient = new
                {
                    id = recipientId
                },
                sender_action = "typing_on"
            };

            fbAPIMgr.CallSendAPI(messageData);

        }

        private void sendButtonMessage(dynamic recipientId)
        {

            var messageData = new
            {
                recipient = new
                {
                    id = recipientId
                },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "button",
                            text = "HelloQQShop",
                            buttons = new[] {
                                new { type = "web_url", title="เว็บไซด์ HelloQQshop", url="http://www.helloqqshop.com", payload = ""},
                                new { type = "phone_number", title="โทรหาเราเลยซิคะ", url="", payload = "6+6926831108" },
                            }
                        }
                    }
                }
            };

            fbAPIMgr.CallSendAPI(messageData);
        }

        private void sendHiMessage(dynamic recipientId)
        {
            var messageData = new
            {
                recipient = new
                {
                    id = recipientId
                },
                message = new
                {
                    text = "สวัสดีค่ะนี่คือ HelloQQ บอท!!!"

                }
            };

            fbAPIMgr.CallSendAPI(messageData);
        }

        private void receivedAuthentication(dynamic messagingEvent)
        {
            var senderID = messagingEvent.sender.id;
            var recipientID = messagingEvent.recipient.id;
            var timeOfAuth = messagingEvent.timestamp;

            // The 'ref' field is set in the 'Send to Messenger' plugin, in the 'data-ref'
            // The developer can set this to an arbitrary value to associate the
            // authentication callback with the 'Send to Messenger' click event. This is
            // a way to do account linking when the user clicks the 'Send to Messenger'
            // plugin.
            var passThroughParam = messagingEvent.optin["ref"];

            Console.Write("Received authentication for user %d and page %d with pass " +
              "through param '%s' at %d", senderID, recipientID, passThroughParam,
              timeOfAuth);

            // When an authentication is received, we'll send a message back to the sender
            // to let them know it was successful.
            SendTextMessage(senderID, "Authentication successful");
        }

        private void SendTextMessage(dynamic recipientId, string messageText)
        {
            var messageData = new
            {
                recipient = new
                {
                    id = recipientId
                },
                message = new
                {
                    text = messageText,
                    metadata = "DEVELOPER_DEFINED_METADATA"
                }
            };

            fbAPIMgr.CallSendAPI(messageData);
        }

        private void ReplyComment(FbFeedResponse fResp, string replyMessage)
        {
            var messageData = new
            {
                message = replyMessage
            };

            fbAPIMgr.ReplyComment(fResp.CommentID, messageData);
        }

        private void receivedMessage(dynamic senderID, string v)
        {
            throw new NotImplementedException();
        }
    }

    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
    }
}

