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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public DirectPayloadResp GetDirectPayload(string payload)
        {
            DirectPayloadResp payloadResp = new DirectPayloadResp();

            var result = (from item in base.Context.HqqDialogflow
                          where item.Payload == payload
                          && item.FlowType == "introduction"
                          && item.Status == 1
                          select item).FirstOrDefault();

            payloadResp = FillPayloadItems(result);
            return payloadResp;
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
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }


            if (!string.IsNullOrEmpty(respItem))
            {
                lstDialogFlow = base.Context.HqqDialogflow                                
                                    .FromSqlRaw(string.Format(@"SELECT * FROM hqq_dialogflow WHERE ID IN ({0})", respItem))
                                    .Where(item => item.Status == 1)
                                    .ToList();

                if (lstDialogFlow != null && lstDialogFlow.Count > 0)
                {
                    if (lstDialogFlow.Where(item => item.ProductId.HasValue).Count() > 0)
                    {
                        if (dialogFlowInfo.dialogType == DialogFlowType.Products)
                        {
                            dialogFlowInfo.dialogType = DialogFlowType.Mix;
                        }
                        else
                        {
                            dialogFlowInfo.dialogType = DialogFlowType.Payload;
                        }
                    }

                    //# LOAD QUICK RESPONSE WITHOUT PRODUCT
                    var payloadResp = new List<PayloadResponse>();
                    FillPayloadItems(lstDialogFlow.Where(item => !item.ProductId.HasValue).ToList(), payloadResp);
                    if (payloadResp.Count > 0)
                    {
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
                    .Include(item => item.HqqDialogflowAddon)
                    .Where(item => item.Status == 1
                     && respProducts.Contains(item.Id))
                    .ToList();

                if (productResult.Count() > 0)
                {
                    //#Loading Include
                    foreach (var item in productResult)
                    {
                        var priceGrp = Context.HqqPrice
                        .Include(ic => ic.Channel)
                        .Where(w => w.ProductId == item.Id)
                        .Select(s => s).AsEnumerable()
                        .GroupBy(gp => gp.ChannelId)
                        .Select(gp2 => (from t2 in gp2 orderby t2.PriceDate descending select t2).FirstOrDefault())
                        .ToList();

                        item.HqqPrice = new List<HqqPrice>();
                        foreach (var pItem in priceGrp)
                        {
                            item.HqqPrice.Add(pItem);
                        }
                    }

                    dialogFlowInfo.dialogType = DialogFlowType.Products;
                    dialogFlowInfo.ResponseProducts = new List<PayloadProduct>();
                    foreach (var prItem in productResult)
                    {
                        var selectedDialogFlow = lstDialogFlow.Where(item => item.ProductId == prItem.Id).FirstOrDefault();

                        dialogFlowInfo.ResponseProducts.Add(new PayloadProduct()
                        {
                            Product = prItem,
                            Order = selectedDialogFlow.Ordering,
                            Payload = selectedDialogFlow.Payload
                        });
                    }
                    
                }
            }

            return dialogFlowInfo;
        }

        private static void FillPayloadItems(List<HqqDialogflow> lstDialogFlow, List<PayloadResponse> payloadResp)
        {
            foreach (var item in lstDialogFlow
                .Where(item => !string.IsNullOrEmpty(item.ResponseAnswer)).ToList())
            {
                List<string> lstRespWording = item.ResponseAnswer
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                payloadResp.Add(new PayloadResponse()
                {   Id = item.Id,
                    Payload = item.Payload,
                    ImageURL = item.PayloadImgUrl,
                    ResponseAnswer = lstRespWording
                });;
            }
        }

        private DirectPayloadResp FillPayloadItems(HqqDialogflow dialogFlow)
        {
            List<string> lstRespWording = new List<string>();
            List<string> lstRespHeader = new List<string>();
            DirectPayloadResp result = new DirectPayloadResp();

            if (!string.IsNullOrEmpty(dialogFlow.ResponseAnswer))
            {
                lstRespWording = dialogFlow.ResponseAnswer
                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                
                if (!string.IsNullOrEmpty(dialogFlow.ResponseHeader))
                {
                     lstRespHeader = dialogFlow.ResponseHeader
                       .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                result = new DirectPayloadResp()
                {
                    Id = dialogFlow.Id,
                    Header = lstRespHeader,
                    Payload = dialogFlow.Payload,                    
                    ImageURL = dialogFlow.PayloadImgUrl,
                    ResponseAnswer = lstRespWording
                };
            }

            return result;
        }
    }

    public enum SearchType { Payload, Keyword }
    public enum DialogFlowType { None, Payload, Products, Mix }
    public class DialogflowInfo
    {
        public DialogFlowType dialogType { get; set; }
        public List<string> ResponseHeader { get; set; }
        public List<PayloadResponse> PayloadResponses { get; set; }
        public List<PayloadProduct> ResponseProducts { get; set; }

        public DialogflowInfo()
        {
            PayloadResponses = new List<PayloadResponse>();
            ResponseProducts = new List<PayloadProduct>();
        }
    }

    public class PayloadProduct
    {
        public HqqProduct Product { get; set; }

        public int? Order { get; set; }

        public string Payload { get; set; }
    }

    public class PayloadResponse
    {
        public int Id { get; set; }
        public string Payload { get; set; }
        public List<string> ResponseAnswer { get; set; }
        public string ImageURL { get; set; }
    }

    public class DirectPayloadResp
    {
        public int Id { get; set; }
        public string Payload { get; set; }
        public List<string> Header { get; set; }
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
