using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HQQWebhook.Model.FacebookMessaging
{
    public class fbQuickReplyItem
    {
        public string ContentType { get; set; }
        public string Title { get; set; }
        public string Payload { get; set; }
        public string ImageURL { get; set; }
    }
}
