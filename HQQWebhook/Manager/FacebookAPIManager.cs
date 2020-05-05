using HQQWebhook.Controllers;
using HQQWebhook.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using NETHTTP = System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HQQWebhook.Manager
{
    public class FacebookAPIManager
    {
        private const string API_SEND_MESSAGE = "/me/messages";
        //# Referecne : https://developers.facebook.com/docs/facebook-login/access-tokens/#apptokens
        private const string API_GET_PSID_PATTERN = "/{0}/ids_for_pages?page={1}&access_token={2}|{3}";
        // 231362101625904/ids_for_pages?page=899463010162012&access_token=1854024371526354|BW1kxATMkehNhsEJ3Wn15hOwFQQ
        private const string API_POST_REPLY_COMMENT = "/{0}/comments";
        // 231362101625904/ids_for_pages?page=899463010162012&access_token=1854024371526354|BW1kxATMkehNhsEJ3Wn15hOwFQQ

        private FbWebhookConfig fbConfig;
        private ILogger<VersionController> logger;

        public FacebookAPIManager(FbWebhookConfig fbWebhook, 
            ILogger<VersionController> log)
        {
            fbConfig = fbWebhook;
            this.logger = log;
        }

        public string GetSPID(string userId)
        {
            string result = string.Empty;
            NETHTTP.HttpResponseMessage response = (new NETHTTP.HttpClient().GetAsync(
                fbConfig.FacebookApi + string.Format(API_GET_PSID_PATTERN, userId, fbConfig.PageId, fbConfig.AppId, fbConfig.AppSecret)
                ).Result);

            if (response != null)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    logger.LogInformation(ConstInfo.LOG_TRACE_PREFIX +
                       string.Format(" GetSPID, Ex: {0}, Source: {1}"
                       , response.ToString()
                       , userId)
                       );
                }
                else
                {
                    dynamic PageConections = ContentToProcessResult(response.Content);
                    foreach (dynamic item in PageConections.data)
                    {
                        if(item.page.id == fbConfig.PageId)
                        {
                            result = item.id;
                        }
                    }
                }
            }

            return result;
        }

        private dynamic ContentToProcessResult(NETHTTP.HttpContent httpContent)
        {
            dynamic result = null;
            var strResult = httpContent.ReadAsStringAsync();
            if (strResult.Result != null)
            {
                result = JsonConvert.DeserializeObject(strResult.Result);
            }
            return result;            
        }

        public void CallSendAPI(dynamic messageData)
        {
            NETHTTP.HttpResponseMessage response = new NETHTTP.HttpClient().PostAsJsonAsync(
                fbConfig.FacebookApi + API_SEND_MESSAGE + "?access_token=" + fbConfig.PageAccessToken
                , (object)messageData).Result;

            if (response != null)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // #LOG : Error log
                    logger.LogInformation(ConstInfo.LOG_TRACE_PREFIX +
                       string.Format(" CallSendAPI, Ex: {0}, Source: {1}"
                       , response.ToString()
                       , JsonConvert.SerializeObject((object)messageData))
                       );
                }
            }
        }

        public void ReplyComment(string commentId, dynamic messageData)
        {
            NETHTTP.HttpResponseMessage response = new NETHTTP.HttpClient().PostAsJsonAsync(
                fbConfig.FacebookApi +
                string.Format(API_POST_REPLY_COMMENT, commentId)
                + "?access_token=" + fbConfig.PageAccessToken
                , (object)messageData).Result;

            if (response != null)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // #LOG : Error log
                    logger.LogInformation(ConstInfo.LOG_TRACE_PREFIX +
                       string.Format(" ReplyComment, Ex: {0}, Source: {1}"
                       , response.ToString()
                       , JsonConvert.SerializeObject((object)commentId))
                       );
                }
            }
        }
    }
}
