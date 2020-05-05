using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HQQWebhook.Model
{
    public class FbFeedResponse
    {
        public string PostID { get; set; }
        public string CommentID { get; internal set; }
        public string Message { get; set; }
        public string RecipientId { get; set; }
        public string FromName { get; set; }
        public string PostURL { get; internal set; }
    }

}
