using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Flurl.Http;
using HQQLibrary.Manager.Base;
using HQQLibrary.Model.Models.Marketing;
using HQQLibrary.Model.Models.MaticonDB;
using HQQLibrary.Utilities;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HQQLibrary.Manager
{
    public class ShopeeDataExtraction : HQQBase
    {
        //# Capture data from Selenium Web Driver
        //# https://stackoverflow.com/questions/51752785/capturing-data-from-browser-page-inspector-in-c-sharp-net-core-console-applicat

        private HtmlDocument pageDocument = new HtmlDocument();
        public ShopeeDataExtraction(string url)
            : base()
        {
            InitVariable(url);
        }

        public void InitVariable(string url)
        {
            var result = LoadWebContentAsync(url);
            LoadHtmlDocument(result);
        }

        private string LoadWebContentAsync(string url)
        {
            string result = string.Empty;
            var task = url.GetStringAsync();

            task.Wait();
            result = task.Result;

            return result;
        }

        private void LoadHtmlDocument(string strHtmlContent)
        {
            pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(strHtmlContent);
        }

        public List<HtmlNode> GetDocumentNodes(string pattern)
        {
            return pageDocument.DocumentNode.SelectNodes(pattern).ToList();
        }

        public void LoadShopInforamation()
        {
            string strJsonObj = string.Empty;
            string strProductItem = string.Empty;
            List<HqqCompetitorShop> lstCompeteShop = new List<HqqCompetitorShop>();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            //# CANNOT LOADED
            //string url = "https://shopee.co.th/api/v2/search_items/?by=pop&limit=30&match_id=3315055&newest=0&order=desc&page_type=shop&version=2";
            //var jsonRqstTask = url.GetStringAsync();
            //jsonRqstTask.Wait();
            //string jsonResult = jsonRqstTask.Result;

            //# Load Shop URL

            lstCompeteShop = this.Context.HqqCompetitorShop
               .Where(item => item.Status == 1)
               .ToList();

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                foreach (var competitorShopItem in lstCompeteShop)
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

                    this.ExtractProductData(competitorShopItem, strJsonObj, strProductItem);
                }
            }
        }

        public DataTable ExtractProductData(HqqCompetitorShop hqqCShop, string jsonShopInfo, string jsonProductItems)
        {
            HtmlDocument htmlDoc;
            List<ProductDataItem> lstProductDataItem;
            List<ProductDisplayItem> lstProductDisplayItem = new List<ProductDisplayItem>();
            List<HqqCompetitorProduct> lstExistingCompetProduct = new List<HqqCompetitorProduct>();
            List<HqqCompetitorProduct> lstNewCompetProduct = new List<HqqCompetitorProduct>();
            HqqCpProductStatistic productStatistic = new HqqCpProductStatistic();

            htmlDoc = new HtmlDocument();
            //# Pre-Data Load all product data for checking.

            lstExistingCompetProduct = Context.HqqCompetitorProduct.Where(item => item.ShopId == hqqCShop.Id && item.Status == 1).ToList();

            //# Pre-JSON data for Update

            htmlDoc.LoadHtml(jsonProductItems);
            //# Load data to the Class
            dynamic objShopInfo = JsonConvert.DeserializeObject(jsonShopInfo);
            var strProductItem = JsonConvert.SerializeObject(objShopInfo.items);
            lstProductDataItem = JsonConvert.DeserializeObject<List<ProductDataItem>>(strProductItem);

            var lstProductItem = htmlDoc.DocumentNode.SelectNodes(@"//script[@type='application/ld+json']").ToList();
            foreach (var productItem in lstProductItem)
            {
                lstProductDisplayItem.Add(JsonConvert.DeserializeObject<ProductDisplayItem>(productItem.InnerText));
            }

            //var itemId = lstProductDataItem.FirstOrDefault().Itemid;
            //var exResult = lstProductDisplayItem.Where(item => item.ProductId == itemId).FirstOrDefault();

            //var temp = objProductItem.Name;
            //temp = objProductItem.ProductId;
            //Uri utemp = objProductItem.Image;
            //temp = objProductItem.Brand;
            //temp = objProductItem.Offers.Price;
            //utemp = objProductItem.Offers.Availability;
            //var ltemp = objProductItem.AggregateRating.RatingCount;
            //temp = objProductItem.AggregateRating.RatingValue;            

            //# 1.Check Product Shop Information Existing

            var shopInfo = lstProductDisplayItem.Where(item => item.Type == "Organization").FirstOrDefault();

            hqqCShop.RatingCount = shopInfo.AggregateRating.RatingCount;
            hqqCShop.RatingValue = decimal.Parse(shopInfo.AggregateRating.RatingValue);
            hqqCShop.ModifiedOn = DateTime.Now;


            var lstProductInfo = lstProductDisplayItem.Where(item => item.Type == "Product").ToList();
            foreach (var productInfo in lstProductInfo)
            {
                var dataItem = lstProductDataItem.Where(item => productInfo.ProductId == item.Itemid).FirstOrDefault();

                //# 2.Check Product Exsiting

                var cProductInfo = lstExistingCompetProduct.Where(item => item.ProductRefId == productInfo.ProductId).FirstOrDefault();
                if (cProductInfo == null)
                {
                    cProductInfo = new HqqCompetitorProduct();
                    cProductInfo.ShopId = hqqCShop.Id;
                    cProductInfo.ProductRefId = productInfo.ProductId;
                    cProductInfo.ProductName = productInfo.Name;
                    cProductInfo.ImageUrl = productInfo.Image.ToString();
                    cProductInfo.CreatedOn = DateTime.Now;
                    cProductInfo.Status = 1;

                    lstNewCompetProduct.Add(cProductInfo);

                    //# 3.Insert Product Statistic
                    //# 4. Calculation Detail                    

                }
                else
                {
                    cProductInfo.ProductName = productInfo.Name;
                    cProductInfo.ImageUrl = productInfo.Image.ToString();
                    cProductInfo.ModifiedOn = DateTime.Now;
                    cProductInfo.Status = 1;
                }

                productStatistic = ExtractStatistic(productInfo, dataItem);
                cProductInfo.HqqCpProductStatistic.Add(productStatistic);
            }

            return new DataTable();
        }

        private static HqqCpProductStatistic ExtractStatistic(ProductDisplayItem productInfo, ProductDataItem dataItem)
        {
            HqqCpProductStatistic productStatistic = new HqqCpProductStatistic();
            productStatistic.Available = 0;
            productStatistic.SaleHistory = dataItem.HistoricalSold;
            productStatistic.Price = decimal.Parse(productInfo.Offers.Price);
            productStatistic.RatingCount = productInfo.AggregateRating.RatingCount;
            productStatistic.RatingValue = decimal.Parse(productInfo.AggregateRating.RatingValue);
            productStatistic.LikeCount = dataItem.LikedCount;
            productStatistic.Stock = dataItem.Stock;
            productStatistic.CreatedOn = DateTime.Now;
            productStatistic.Status = 1;

            //# MOVEMENT STATISTIC
            // productStatistic.SaleMovement


            return productStatistic;
        }
    }


}
