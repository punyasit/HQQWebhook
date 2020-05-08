using HQQLibrary.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static HQQLibrary.Manager.DialogflowManager;

namespace HQQUnitTest
{
    [TestClass]
    public class DialogFlowTest
    {
        private DialogflowManager dialogFlowMgr;
        public DialogFlowTest()
        {
            InitVariable();
        }

        private void InitVariable()
        {
            dialogFlowMgr = new DialogflowManager();
        }
        [TestMethod]
        public void D01_GetPrivateReply()
        {
            PrivateReply result = dialogFlowMgr.GetPrivateReply("สนใจครับ");
            Assert.IsTrue(result.ResponseComments.Count > 0 && result.ReplyMessages.Count > 0);
        }
        [TestMethod]
        public void D02_GetDialogFromKeyword()
        {
            DialogflowInfo result = dialogFlowMgr.GetDialogFromKeyword("ช่วยแนะนำให้ฉันได้ไหม");
            Assert.IsTrue(result.dialogType == DialogFlowType.Payload
                && result.PayloadResponses.Count > 2);
        }
        [TestMethod]
        public void D02_GetDialogFromPayload()
        {
            DialogflowInfo result = dialogFlowMgr.GetDialogFromPayload("HQQ_PL_GPSWATCH");
            Assert.IsTrue(result.dialogType == DialogFlowType.Products
                && result.ResponseProducts.Count > 0);
        }
    }
}
