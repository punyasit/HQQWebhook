using Microsoft.VisualStudio.TestTools.UnitTesting;
using HQQLibrary.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using Newtonsoft.Json;
using Flurl.Http;
using System.Data;
using HQQLibrary.Model.Models.Marketing;
using HQQLibrary.Utilities;
using HQQLibrary.Model.Models.MaticonDB;

namespace HQQLibrary.Manager.Tests
{
    [TestClass()]
    public class WebScrapperManagerTests
    {
        ShopeeDataExtraction wsMgr;

        public WebScrapperManagerTests()
        {
            InitVariable();
        }

        private void InitVariable()
        {
            wsMgr = new ShopeeDataExtraction();
        }

        [TestMethod()]
        public void WS01_GetDocumentNodesTest()
        {
            List<HtmlNode> lstNode = wsMgr.GetDocumentNodes(@"(//div[contains(@class,'shop-search-result-view__item col-xs-2-4')]");
            Assert.IsTrue(lstNode.Count > 0);
        }
        //# Get element from selenium https://stackoverflow.com/questions/23587862/how-to-get-all-elements-into-list-or-string-with-selenium-using-c

        [TestMethod]
        public void WS02_ExecuteGatherShopInfoTest()
        {
            wsMgr.ExecuteGatherShopInfo();
            Assert.IsTrue(true);
        }

       

       


    }
}
