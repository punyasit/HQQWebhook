using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HQQWebhook.Model.FacebookMessaging
{

    public class Recipient
    {
        public string id { get; set; }

    }
    public class Quick_replies
    {
        public string content_type { get; set; }
        public string title { get; set; }
        public string payload { get; set; }
        public string image_url { get; set; }

    }
    public class Message
    {
        public string text { get; set; }
        public List<Quick_replies> quick_replies { get; set; }
        public Attachment attachment { get; set; }

    }
    public class MessageData
    {
        public Recipient recipient { get; set; }
        public string messaging_type { get; set; }
        public Message message { get; set; }

    }

    public class Default_action
    {
        public string type { get; set; }
        public string url { get; set; }
        public string webview_height_ratio { get; set; }

    }
    public class Buttons
    {
        public string type { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string payload { get; set; }

    }
    public class Elements
    {
        public string title { get; set; }
        public string image_url { get; set; }
        public string subtitle { get; set; }
        public Default_action default_action { get; set; }
        public List<Buttons> buttons { get; set; }

    }
    public class Payload
    {
        public string template_type { get; set; }
        public List<Elements> elements { get; set; }

    }
    public class Attachment
    {
        public string type { get; set; }
        public Payload payload { get; set; }

    }
   
}
