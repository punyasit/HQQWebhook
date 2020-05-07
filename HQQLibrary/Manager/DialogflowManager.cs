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

            var lstPrivateReply = (from item in base.Context.HqqDialogflow
                                   where item.Status == 1
                                   && item.MatchKeywords.Contains(keyword)
                                   && item.FlowType == "private_reply"
                                   select item).FirstOrDefault();

            if (lstPrivateReply != null)
            {
                if (!string.IsNullOrEmpty(lstPrivateReply.Payload))
                {
                    string[] responseMessages = lstPrivateReply.Payload.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in responseMessages)
                    {
                        privateReply.ReplyMessages.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(lstPrivateReply.ResponseAnswer))
                {
                    string[] responseComment = lstPrivateReply.ResponseAnswer.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in responseComment)
                    {
                        privateReply.ResponseComments.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(lstPrivateReply.ResponseItems))
                {
                    lstDialogFlow = base.Context.HqqDialogflow
                                   .FromSqlRaw(string.Format(@"SELECT * FROM hqq_dialogflow WHERE ID IN ({0})", lstPrivateReply.ResponseItems))
                                   .Where(item => item.Status == 1)
                                   .ToList();

                    privateReply.PayloadResponses = new List<PayloadResponse>();
                    FillPayloadItems(lstDialogFlow, privateReply.PayloadResponses);
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

            HqqDialogflow hqDialogResult;
            string respItem;

            if (searchType == SearchType.Payload)
            {
                hqDialogResult = (from item in base.Context.HqqDialogflow
                                  where item.Payload == searchKey
                                  && item.FlowType == "introduction"
                                  && item.Status == 1
                                  select item).FirstOrDefault();
            }
            else
            {
                hqDialogResult = (from item in base.Context.HqqDialogflow
                                  where item.MatchKeywords == searchKey
                                  && item.FlowType == "introduction"
                                  && item.Status == 1
                                  select item).FirstOrDefault();
            }

            if (hqDialogResult == null) return null;

            respItem = hqDialogResult.ResponseItems;
            if (!string.IsNullOrEmpty(hqDialogResult.ResponseHeader))
            {
                dialogFlowInfo.ResponseHeader = hqDialogResult.ResponseHeader
                    .Split(new char[] { ';' },StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            if (!string.IsNullOrEmpty(respItem))
            {   
                lstDialogFlow = base.Context.HqqDialogflow
                                    .FromSqlRaw(string.Format(@"SELECT * FROM hqq_dialogflow WHERE ID IN ({0})", respItem))
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

                        var payloadResp = new List<PayloadResponse>();
                        FillPayloadItems(lstDialogFlow, payloadResp);
                        dialogFlowInfo.PayloadResponses = payloadResp;
                    }
                }
            }

            List<int?> respProducts = new List<int?>();
            if (lstDialogFlow.Count > 0)
            {
                respProducts = lstDialogFlow
                    .Where(item => item.ProductId != null)
                    .Select(item => item.ProductId).ToList();
            }

            if (respProducts.Count > 0)
            {
                var productResult = base.Context.HqqProduct
                    .Where(item => item.Status == 1
                     && respProducts.Contains(item.Id)).ToList();

                if (productResult.Count() > 0)
                {
                    //#Loading Include
                    foreach (var item in productResult)
                    {
                        var priceItem = (from pItem in Context.HqqPrice
                                         where pItem.ProductId == item.Id
                                         orderby pItem.PriceDate descending
                                         select pItem).FirstOrDefault();

                        item.HqqPrice = new List<HqqPrice>();
                        item.HqqPrice.Add(priceItem);
                    }

                    dialogFlowInfo.dialogType = DialogFlowType.Products;
                    dialogFlowInfo.ResponseProducts = new List<HqqProduct>();
                    dialogFlowInfo.ResponseProducts = productResult.ToList();
                }

            }

            return dialogFlowInfo;
        }

        private static void FillPayloadItems(List<HqqDialogflow> lstDialogFlow, List<PayloadResponse> payloadResp)
        {
            foreach (var item in lstDialogFlow)
            {
                List<string> lstRespWording = item.ResponseAnswer.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                payloadResp.Add(new PayloadResponse()
                {
                    Payload = item.Payload,
                    ResponseAnswer = lstRespWording
                });
            }
        }
    }

    public enum SearchType { Payload, Keyword }
    public enum DialogFlowType { None, Payload, Products, Mix }
    public class DialogflowInfo
    {
        public DialogFlowType dialogType { get; set; }
        public List<string> ResponseHeader { get; set; }
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
        public List<string> ResponseAnswer { get; set; }
        public string ImageURL { get; set; }
    }

    public class PrivateReply
    {
        public List<string> ResponseComments { get; set; }
        public List<string> ReplyMessages { get; set; }
        public List<PayloadResponse> PayloadResponses { get; set; }

        public PrivateReply()
        {
            ResponseComments = new List<string>();
            ReplyMessages = new List<string>();
        }
    }
}
