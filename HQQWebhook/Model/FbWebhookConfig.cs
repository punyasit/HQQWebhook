using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HQQWebhook.Model
{
    public class FbWebhookConfig
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string PageId { get; set; }
        public string PageAccessToken { get; set; }
        public string ValidationToken { get; set; }
        public string ServerURL { get; set; }
        public string FacebookApi { get; set; }
        public bool EnabledAutoMessage { get; set; }
        public bool EnabledFeedResponse { get; set; }
    }
}
