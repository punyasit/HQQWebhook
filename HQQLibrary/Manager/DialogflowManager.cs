using System;
using System.Text;
using HQQLibrary.Manager.Base;
using HQQLibrary.Model.Models.MaticonDB;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Z.EntityFramework.Plus;

namespace HQQLibrary.Manager
{
    public class DialogflowManager : HQQBase

    {
        public DialogflowManager()
            : base()
        {

        }

        public PrivateReply GetPrivateReply(string keyword)
        {
            PrivateReply privateReply = new PrivateReply();
            List<HqqDialogflow> lstDialogFlow = new List<HqqDialogflow>();

            var quickReplyItem = (from item in base.Context.HqqDialogflow
                                where item.Status == 1
                                && item.MatchKeywords.Contains(keyword)
                                && item.FlowType == "private_reply"
                                select item).FirstOrDefault();

            if (quickReplyItem != null)
            {
                if (!string.IsNullOrEmpty(quickReplyItem.Payload))
                {
                    string[] responseMessages = quickReplyItem.Payload.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in responseMessages)
                    {
                        privateReply.ReplyMessages.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(quickReplyItem.ResponseWording))
                {
                    string[] responseComment = quickReplyItem.ResponseWording.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in responseComment)
                    {
                        privateReply.ResponseComments.Add(item);
                    }
                }
            }

            return privateReply;
        }

        public DialogflowInfo GetDialogFromKeyword(string keyword)
        {
            return GetDialogInfo(SearchType.Keyword, keyword);
        }

        public DialogflowInfo GetDialogFromPayload(string payload)
        {
            return GetDialogInfo(SearchType.Payload, payload);
        }

        private DialogflowInfo GetDialogInfo(SearchType searchType, string searchKey)
        {
            DialogflowInfo dialogFlowInfo = new DialogflowInfo();
            List<HqqDialogflow> lstDialogFlow = new List<HqqDialogflow>();

            string respItems;
            if (searchType == SearchType.Payload)
            {
                respItems = (from item in base.Context.HqqDialogflow
                             where item.Payload == searchKey
                             && item.FlowType == "introduction"
                             select item.ResponseItems).FirstOrDefault();
            }
            else
            {
                respItems = (from item in base.Context.HqqDialogflow
                             where item.MatchKeywords == searchKey
                             && item.FlowType == "introduction"
                             select item.ResponseItems).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(respItems))
            {
                lstDialogFlow = base.Context.HqqDialogflow
                                    .FromSqlRaw(string.Format(@"SELECT * FROM hqq_dialogflow WHERE ID IN ({0})", respItems))
                                    .Where(item => item.Status == 1)
                                    .ToList();

                if (lstDialogFlow != null && lstDialogFlow.Count > 0)
                {
                    if (lstDialogFlow.Where(item => item.ProductId.HasValue).Count() == 0)
                    {
                        if (dialogFlowInfo.dialogType == DialogFlowType.Products)
                        {
                            dialogFlowInfo.dialogType = DialogFlowType.Mix;
                        }
                        else
                        {
                            dialogFlowInfo.dialogType = DialogFlowType.Payload;
                        }

                        dialogFlowInfo.PayloadResponses = new List<PayloadResponse>();
                        foreach (var item in lstDialogFlow)
                        {
                            dialogFlowInfo.PayloadResponses.Add(new PayloadResponse()
                            {
                                Payload = item.Payload,
                                ResponseWording = item.ResponseWording
                            });
                        }
                    }
                }
            }

            List<int?> respProducts = new List<int?>();
            if(lstDialogFlow.Count > 0) 
            {
                respProducts =lstDialogFlow
                    .Where(item => item.ProductId != null)
                    .Select(item => item.ProductId).ToList();
            }

            if (respProducts.Count > 0)
            {
                var productResult = base.Context.HqqProduct
                    .Where(item => item.Status == 1
                     && respProducts.Contains(item.Id));

                if (productResult.Count() > 0)
                {
                    //#Loading Include
                    foreach (var item in productResult)
                    {
                        item.HqqPrice = new List<HqqPrice>();
                        item.HqqPrice.Add((from pItem in item.HqqPrice
                                         orderby pItem.PriceDate descending
                                         select pItem).FirstOrDefault());
                    }

                    dialogFlowInfo.dialogType = DialogFlowType.Products;
                    dialogFlowInfo.ResponseProducts = new List<HqqProduct>();
                    dialogFlowInfo.ResponseProducts = productResult.ToList();
                }

            }

            return dialogFlowInfo;
        }

        public enum SearchType { Payload, Keyword }
        public enum DialogFlowType { None, Payload, Products, Mix }

        public class DialogflowInfo
        {
            public DialogFlowType dialogType { get; set; }
            public List<PayloadResponse> PayloadResponses { get; set; }
            public List<HqqProduct> ResponseProducts { get; set; }

            public DialogflowInfo()
            {
                PayloadResponses = new List<PayloadResponse>();
                ResponseProducts = new List<HqqProduct>();
            }
        }

        public class PayloadResponse
        {
            public string Payload { get; set; }
            public string ResponseWording { get; set; }
        }

        public class PrivateReply
        {
            public List<string> ResponseComments { get; set; }
            public List<string> ReplyMessages { get; set; }

            public PrivateReply()
            {
                ResponseComments = new List<string>();
                ReplyMessages = new List<string>();
            }
        }
    }
}
