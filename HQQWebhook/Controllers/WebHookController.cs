using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using HQQWebhook.Manager;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Internal;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

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

        private string APP_SECRET = string.Empty;
        private string VALIDATION_TOKEN = string.Empty;
        private string PAGE_ACCESS_TOKEN = string.Empty;
        private string SERVER_URL = string.Empty;
        private string FB_MESSAGE_API = string.Empty;

        public WebHookController(IConfiguration configuration)
        {
            WebConfig = configuration;
            InitVariable();
        }

        private void InitVariable()
        {
            APP_SECRET = WebConfig["appSecret"];
            VALIDATION_TOKEN = WebConfig["validationToken"];
            PAGE_ACCESS_TOKEN = WebConfig["pageAccessToken"];
            SERVER_URL = WebConfig["serverURL"];
            FB_MESSAGE_API = WebConfig["facebookMessageApi"];
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
                Request.Query["hub.verify_token"] == WebConfig["validationToken"])
            {
                Response.StatusCode = 200;
                return int.Parse(Request.Query["hub.challenge"]);
            }
            else
            {
                Response.StatusCode = 403;
                return 0;
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

                    foreach (var messagingEvent in pageEntry.messaging)
                    {
                        if (messagingEvent.optin != null)
                        {
                            receivedAuthentication(messagingEvent);
                        }
                        else if (messagingEvent.message != null)
                        {
                            receivedMessage(messagingEvent);
                        }
                        else if (messagingEvent.deliver != null)
                        {
                            receivedDeliveryConfirmation(messagingEvent);
                        }
                        else if (messagingEvent.postback != null)
                        {
                            receivedPostback(messagingEvent);
                        }
                        else if (messagingEvent.read != null)
                        {
                            receivedMessageRead(messagingEvent);
                        }
                        else if (messagingEvent.account_linking != null)
                        {
                            receivedAccountLink(messagingEvent);
                        }
                        else
                        {
                            Console.Write(string.Format("Webhook received unknown messagingEvent: {0} ", messagingEvent));
                        }
                    }
                }
            }

            HttpContext.Response.StatusCode = 200;
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
            var recipientID = messagingEvent.recipient.id;
            var timeOfMessage = messagingEvent.timestamp;
            var message = messagingEvent.message;

            //console.log("Received message for user %d and page %d at %d with message:",
            //senderID, recipientID, timeOfMessage);
            //console.log(JSON.stringify(message));

            string isEcho = message.is_echo;
            string messageId = message.mid;
            string appId = message.app_id;
            string metadata = message.metadata;

            // You may get a text or attachment but not both
            string messageText = message.text;
            string messageAttachments = message.attachments;
            var quickReply = message.quick_reply;

            if (!string.IsNullOrEmpty(isEcho))
            {
                // Just logging message echoes to console
                //console.log("Received echo for message %s and app %d with metadata %s",
                //messageId, appId, metadata);
                return;
            }
            else if (!string.IsNullOrEmpty(quickReply))
            {
                var quickReplyPayload = quickReply.payload;
                //console.log("Quick reply for message %s with payload %s",
                //messageId, quickReplyPayload);

                sendTextMessage(senderID, "Quick reply tapped");
                return;
            }

            if (!string.IsNullOrEmpty(messageText))
            {
                // If we receive a text message, check to see if it matches any special
                // keywords and send back the corresponding example. Otherwise, just echo
                // the text we received.

                Regex rgx = new Regex(@"/[^\w\s] / gi");
                switch (rgx.Replace(messageText, "").Trim().ToLower())
                {
                    case "hello":
                    case "hi":
                        sendHiMessage(senderID);
                        break;

                    case "image":
                        sendImageMessage(senderID);
                        break;

                    //case "gif":
                    //    requiresServerURL(sendGifMessage, [senderID]);
                    //    break;

                    //case "audio":
                    //    requiresServerURL(sendAudioMessage, [senderID]);
                    //    break;

                    //case "video":
                    //    requiresServerURL(sendVideoMessage, [senderID]);
                    //    break;

                    //case "file":
                    //    requiresServerURL(sendFileMessage, [senderID]);
                    //    break;

                    case "ขอรายละเอียดเพิ่มเติม":
                    case "button":
                        sendTypingOn(senderID);
                        sendButtonMessage(senderID);
                        break;

                    //case "generic":
                    //    requiresServerURL(sendGenericMessage, [senderID]);
                    //    break;

                    //case "receipt":
                    //    requiresServerURL(sendReceiptMessage, [senderID]);
                    //    break;

                    //case "quick reply":
                    //case "ตอบหน่อย":
                    //    sendQuickReply(senderID);
                    //    break;

                    //case "read receipt":
                    //    sendReadReceipt(senderID);
                    //    break;

                    case "typing on":
                        sendTypingOn(senderID);
                        break;

                    case "typing off":
                        sendTypingOff(senderID);
                        break;

                    //case "account linking":
                    //    requiresServerURL(sendAccountLinking, [senderID]);
                    //    break;

                    default:
                        sendTextMessage(senderID, messageText);
                        break;

                }
            }
            else if (!string.IsNullOrEmpty(messageAttachments))
            {
                sendTextMessage(senderID, "Message with attachment received");
            }
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
                        url = SERVER_URL + "/assets/rift.png"
                    }
                }
                }
            };
            callSendAPI(messageData);
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
            callSendAPI(messageData);
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

            callSendAPI(messageData);

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

            callSendAPI(messageData);
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

            callSendAPI(messageData);
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
            sendTextMessage(senderID, "Authentication successful");
        }


        private void sendTextMessage(dynamic recipientId, string messageText)
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

            callSendAPI(messageData);
        }

        private void callSendAPI(dynamic messageData)
        {

            HttpResponseMessage response = (new HttpClient().PostAsJsonAsync(
                FB_MESSAGE_API + "?access_token=" + PAGE_ACCESS_TOKEN
                , (object)messageData)).Result;

            if (response != null)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // #LOG : Error log
                }
            }
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

