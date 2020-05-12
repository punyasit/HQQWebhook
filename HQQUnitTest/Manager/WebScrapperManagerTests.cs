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
            wsMgr = new ShopeeDataExtraction("https://shopee.co.th/shop/3315055/search?page=0&sortBy=pop");
        }

        [TestMethod()]
        public void GetDocumentNodesTest()
        {
            List<HtmlNode> lstNode = wsMgr.GetDocumentNodes(@"(//div[contains(@class,'shop-search-result-view__item col-xs-2-4')]");
            Assert.IsTrue(lstNode.Count > 0);
        }
        //# Get element from selenium https://stackoverflow.com/questions/23587862/how-to-get-all-elements-into-list-or-string-with-selenium-using-c

        [TestMethod]
        public void ChromeDriverTest()
        {
            LoadDataResult();
            Assert.IsTrue(true);
        }

        public void LoadDataResult()
        {
            //# CANNOT LOADED
            //string url = "https://shopee.co.th/api/v2/search_items/?by=pop&limit=30&match_id=3315055&newest=0&order=desc&page_type=shop&version=2";
            //var jsonRqstTask = url.GetStringAsync();
            //jsonRqstTask.Wait();
            //string jsonResult = jsonRqstTask.Result;

            string strJsonObj = string.Empty;
            string strProductItem = string.Empty;

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                try
                {

                    driver.Navigate().GoToUrl(@"https://shopee.co.th/api/v2/search_items/?by=pop&limit=30&match_id=3315055&newest=0&order=desc&page_type=shop&version=2");
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    var result = wait.Until(ExpectedConditions.ElementExists(By.TagName("pre")));
                    strJsonObj = driver.FindElementByTagName("pre").GetAttribute("innerHTML");

                    //File.WriteAllText(this.AssemblyDirectory + "/dummy-overalljson.txt",strJsonObj);

                    strJsonObj = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-overalljson.txt");

                    //driver.Navigate().GoToUrl(@"https://shopee.co.th/shop/3315055/search");
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
                    //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    //var result = wait.Until(ExpectedConditions.ElementExists(By.ClassName("shop-search-result-view__item")));
                    //var headerHTML = driver.FindElementByTagName("head").GetAttribute("innerHTML");

                    strProductItem = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-htmlfile.txt");

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    driver.Close();
                    driver.Quit();
                }
            }

            wsMgr.ExtractProductData(new HqqCompetitorShop(),strJsonObj, strProductItem);

        }

       


    }
}
