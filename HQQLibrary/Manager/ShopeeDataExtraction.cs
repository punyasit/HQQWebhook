using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Flurl.Http;
using HQQLibrary.Manager.Base;
using HQQLibrary.Model.Models.Marketing;
using HQQLibrary.Model.Models.MaticonDB;
using HQQLibrary.Model.Utilities;
using HQQLibrary.Utilities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Serilog;
using Serilog.Sinks.File;

namespace HQQLibrary.Manager
{
    public class ShopeeDataExtraction : HQQBase
    {
        //# Capture data from Selenium Web Driver
        //# https://stackoverflow.com/questions/51752785/capturing-data-from-browser-page-inspector-in-c-sharp-net-core-console-applicat

        private HtmlDocument pageDocument = new HtmlDocument();
        private static string PAGE_URL = "https://shopee.co.th/shop/{0}/search?page={1}&sortBy=pop";
        private static string DATA_URL = "https://shopee.co.th/api/v2/search_items/?by=pop&limit=30&match_id={0}&newest={1}&order=desc&page_type=shop&version=2";
        private static int ITEM_LIMIT = 30;

        private Regex rxExtract = new Regex(@"\(([^\)]*)\)");
        private Match rxMatch;
        private DateTime startTime;

        public ShopeeDataExtraction()
            : base()
        {
            InitVariable();
        }
        public void InitVariable()
        {

        }
        private void InitLogger()
        {
            IConfiguration appConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var filename = appConfig.GetSection("ConsoleLogger:filename").Value;
            filename = string.Format("{0}-{1}.log", filename, DateTime.Now.ToString("ddMMyyHHmmss"));

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(filename)
                .CreateLogger();
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

        public void ExecuteGatherShopInfo()
        {
            this.InitLogger();

            string strProductData = string.Empty;
            string strPageData = string.Empty;
            List<HqqCompetitorShop> lstCompeteShop = new List<HqqCompetitorShop>();
            List<ProductPageItem> lstprdPageItem = new List<ProductPageItem>();
            ProductPageItem prdPageItem = new ProductPageItem();

            string execDataURL = string.Empty;
            string execPageURL = string.Empty;
            string strConvertPrice = string.Empty;
            int deConvertPrice = 0;

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            //# Load Shop URL
            lstCompeteShop = this.Context.HqqCompetitorShop
               .Where(item => item.Status == 1)
               .ToList();

            startTime = DateTime.Now;
            Log.Information("Start Process Start Time: {0:dd:MM:yyyy HH:mm:ss}", startTime);
            Log.Information("Get Shop Information, {0}", lstCompeteShop.Count);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                foreach (var cpShopItem in lstCompeteShop)
                {
                    ///#DEBUG 
                    cpShopItem.RunPageNo = 1;
                    for (int iPage = 0; iPage < cpShopItem.RunPageNo; iPage++)
                    {
                        try
                        {
                            Log.Information("Start Query from Shop, {0} Page {1}", cpShopItem.ShopName, (iPage + 1));

                            execPageURL = string.Format(PAGE_URL, cpShopItem.ShopId, iPage);
                            execDataURL = string.Format(DATA_URL, cpShopItem.ShopId, (ITEM_LIMIT * iPage));

                            driver.Navigate().GoToUrl(execDataURL);
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
                            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            var result = wait.Until(ExpectedConditions.ElementExists(By.TagName("pre")));
                            strProductData = driver.FindElementByTagName("pre").GetAttribute("innerHTML");

                            //File.WriteAllText(HQQUtilities.AssemblyDirectory + "/dummy-overalljson.txt", strJsonObj);
                            //# strProductData = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-overalljson.txt");

                            driver.Navigate().GoToUrl(execPageURL);
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
                            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            result = wait.Until(ExpectedConditions.ElementExists(By.ClassName("shop-search-result-view__item")));
                            strPageData = driver.FindElementByTagName("head").GetAttribute("innerHTML");
                            var lstShopSelectResult = driver.FindElementsByClassName("shop-search-result-view__item");

                            //File.WriteAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt", lstShopSelectResult[0].GetAttribute("innerHTML"));
                            //strProductData = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt");

                            ////# Load page that need to Get information.

                            Log.Information("Get Product Sale Amount Info");
                            foreach (var item in lstShopSelectResult)
                            {
                                prdPageItem = GetProductPrimaryInfo(lstprdPageItem,
                                    ref strConvertPrice,
                                    ref deConvertPrice,
                                    item);
                            }

                            //File.WriteAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt", JsonConvert.SerializeObject(lstprdPageItem));
                            //string strObjItem = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt");
                            //lstprdPageItem = JsonConvert.DeserializeObject<List<ProductPageItem>>(strObjItem);

                            Log.Information("Found Interest Product, {0} records", lstprdPageItem.Count);
                            //# Iterage to the each page for get information.
                            foreach (var item in lstprdPageItem)
                            {
                                Log.Information("Navigate and Query Product ID, {0} ", item.ProductId);
                                try
                                {
                                    NavigateQueryProductInfo(ref strConvertPrice,
                                        ref deConvertPrice, driver,
                                        out wait, out result, item);
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("Errir on Product ID: {0}, Error: {1} ", item.ProductId, ex.Message);
                                }

                            }
                            //strPageData = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-htmlfile.txt");
                        }
                        catch (Exception ex)
                        {
                            string strEx = ex.ToString();

                        }

                        Log.Information("Start extract product information, {0} records", lstprdPageItem.Count);
                        this.ExtractProductData(cpShopItem, strProductData, strPageData, lstprdPageItem);

                        Log.Information("Completed extract product information, {0} records", lstprdPageItem.Count);
                        Log.Information("Finished Process Time: {0:dd:MM:yyyy HH:mm:ss}, Elasped Time: {1} Minutes", DateTime.Now, (DateTime.Now - startTime).TotalMinutes);
                    }
                }

                driver.Close();
                driver.Quit();
            }

        }

        private static ProductPageItem GetProductPrimaryInfo(
            List<ProductPageItem> lstprdPageItem,
            ref string strConvertSold,
            ref int deConvertSold,
            IWebElement item)
        {
            ProductPageItem prdPageItem = new ProductPageItem();
            prdPageItem.URL = item.FindElement(By.TagName("a")).GetAttribute("href");
            prdPageItem.ProductId = (long.Parse(prdPageItem.URL.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last()));

            //#FIND AMOUNT IF AMOUNT < 1000 THEN COlLECT TO STATISTIC
            var tmpSoldElmnt = item.FindElement(By.XPath("div/a/div/div[2]/div[3]/div[3]"));
            if (tmpSoldElmnt != null)
            {
                if (string.IsNullOrEmpty(tmpSoldElmnt.Text))
                {
                    strConvertSold = tmpSoldElmnt.GetAttribute("innerHTML");
                }
                else
                {
                    strConvertSold = tmpSoldElmnt.Text;
                }

                strConvertSold = strConvertSold.Replace("ขายแล้ว ", "").Replace(" ชิ้น", "");
                if (!string.IsNullOrEmpty(strConvertSold))
                {
                    if (strConvertSold.Contains("พัน"))
                    {
                        strConvertSold = strConvertSold.Replace("พัน", "");
                        var converted = Convert.ToDecimal(strConvertSold);
                        deConvertSold = (int)(converted * 1000);
                    }
                    else if (strConvertSold.Contains("หมื่น"))
                    {
                        strConvertSold = strConvertSold.Replace("หมื่น", "");
                        var converted = Convert.ToDecimal(strConvertSold);
                        deConvertSold = (int)(deConvertSold * 10000);
                    }
                    else
                    {
                        deConvertSold = Convert.ToInt32(strConvertSold);
                    }
                }
                else
                {
                    deConvertSold = 0;
                }
            }
            else
            {
                deConvertSold = 0;
                Log.Error("Cannot Convert Sold Amount from product Id: {0}", prdPageItem.ProductId);
            }

            if (deConvertSold < 1000)
            {
                prdPageItem.Sold = deConvertSold;
                lstprdPageItem.Add(prdPageItem);
            }

            return prdPageItem;
        }

        private void NavigateQueryProductInfo(
            ref string strConvertLiked,
            ref int deConvertLiked,
            ChromeDriver driver,
            out WebDriverWait wait,
            out IWebElement result,
            ProductPageItem item)
        {
            driver.Navigate().GoToUrl(item.URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            result = wait.Until(ExpectedConditions.ElementExists(By.ClassName("product-briefing")));

            var prdDetail = driver.FindElementsByClassName("page-product__detail").FirstOrDefault();

            //# Need  to find quantity from wording by iterrating.
            var lstDataItem = prdDetail.FindElements(By.XPath("div[1]/div[2]/*"));
            if (lstDataItem != null && lstDataItem.Count > 0)
            {
                foreach (var dataitem in lstDataItem)
                {
                    var label = dataitem.FindElement(By.TagName("label"));
                    if (label.GetAttribute("textContent") == "จำนวนสินค้า")
                    {
                        var stock = dataitem.FindElement(By.TagName("div")).GetAttribute("textContent");
                        item.Stock = int.Parse(stock);
                    }
                }
            }

            //var stock = prdDetail.FindElement(By.XPath("//div[1]/div[2]/div[3]/div"));
            //if(stock != null)
            //{
            //    string temp = stock.Text;
            //    item.Stock = int.Parse(stock.Text);
            //}

            var prdBriefing = driver.FindElementsByClassName("product-briefing").FirstOrDefault();
            var liked = prdBriefing.FindElement(By.XPath("div[2]/div[2]/div[2]/div"));
            if (liked != null)
            {
                strConvertLiked = liked.GetAttribute("textContent");
                rxMatch = rxExtract.Match(strConvertLiked);
                if (rxMatch.Success)
                {
                    strConvertLiked = rxMatch.Value.Replace("(", "").Replace(")", "");
                }

                if (strConvertLiked.Contains("พัน"))
                {
                    strConvertLiked = strConvertLiked.Replace("พัน", "");
                    var converted = Convert.ToDecimal(strConvertLiked);
                    deConvertLiked = (int)(converted * 1000);
                }
                else if (strConvertLiked.Contains("หมื่น"))
                {
                    strConvertLiked = strConvertLiked.Replace("หมื่น", "");
                    var converted = Convert.ToDecimal(strConvertLiked);
                    deConvertLiked = (int)(deConvertLiked * 10000);
                }
                else
                {
                    deConvertLiked = Convert.ToInt32(strConvertLiked);
                }

                item.Liked = (int)deConvertLiked;
            }
        }

        private void ExtractProductData(HqqCompetitorShop hqqCShop,
            string jsonProductData,
            string jsonPageData,
            List<ProductPageItem> lstprdPageItem)
        {
            HtmlDocument htmlDoc;
            List<ProductDataItem> lstProductDataItem;
            List<ProductDisplayItem> lstProductDisplayItem = new List<ProductDisplayItem>();
            List<HqqCompetitorProduct> lstExistingCompetPrd = new List<HqqCompetitorProduct>();
            List<HqqCpProductStatistic> lstExistingPrdStatistic = new List<HqqCpProductStatistic>();
            List<HqqCompetitorProduct> lstNewCompetProduct = new List<HqqCompetitorProduct>();
            HqqCpProductStatistic productStatistic = new HqqCpProductStatistic();

            htmlDoc = new HtmlDocument();
            //# Pre-Data Load all product data for checking.

            lstExistingCompetPrd = Context.HqqCompetitorProduct
                .Where(item => item.ShopId == hqqCShop.Id && item.Status == 1).ToList();

            var lstPrdStId = Context.HqqCpProductStatistic
                .Where(item => item.Product.ShopId == hqqCShop.Id)
                .GroupBy(x => x.ProductId, (x, y) => new
                {
                    Id = x,
                    CreatedDate = y.Max(z => z.CreatedOn)
                })
                .Select(gp => gp.Id)
                .ToList();

            lstExistingPrdStatistic = Context.HqqCpProductStatistic
                .Where(item => lstPrdStId.Contains(item.Id))
                .ToList();

            //# Pre-JSON data for Update

            htmlDoc.LoadHtml(jsonPageData);
            //# Load data to the Class
            dynamic objShopInfo = JsonConvert.DeserializeObject(jsonProductData);
            var strProductItem = JsonConvert.SerializeObject(objShopInfo.items);
            lstProductDataItem = JsonConvert.DeserializeObject<List<ProductDataItem>>(strProductItem);

            var lstProductItem = htmlDoc.DocumentNode.SelectNodes(@"//script[@type='application/ld+json']").ToList();
            foreach (var productItem in lstProductItem)
            {
                lstProductDisplayItem.Add(JsonConvert.DeserializeObject<ProductDisplayItem>(productItem.InnerText.Replace("@", "")));
            }
            //# 1.Check Product Shop Information Existing

            var shopInfo = lstProductDisplayItem.Where(item => item.Type == "Organization").FirstOrDefault();

            hqqCShop.RatingCount = shopInfo.AggregateRating.RatingCount;
            hqqCShop.RatingValue = decimal.Parse(shopInfo.AggregateRating.RatingValue);
            hqqCShop.ModifiedOn = DateTime.Now;

            var lstProductInfo = lstProductDisplayItem.Where(item => item.Type == "Product").ToList();
            foreach (var productInfo in lstProductInfo)
            {
                //var dataItem = lstProductDataItem.Where(item => productInfo.ProductId == item.Itemid).FirstOrDefault();
                //# 2.Check Product Exsiting

                Log.Information("Prepare Product name: {0}", productInfo.Name);
                var cProductInfo = lstExistingCompetPrd.Where(item => item.ProductRefId == productInfo.ProductId).FirstOrDefault();
                if (cProductInfo == null)
                {
                    cProductInfo = new HqqCompetitorProduct();
                    cProductInfo.ShopId = hqqCShop.Id;
                    cProductInfo.ProductRefId = productInfo.ProductId;
                    cProductInfo.ProductName = productInfo.Name;
                    cProductInfo.ImageUrl = productInfo.Image.ToString();
                    cProductInfo.IsNew = 1;
                    cProductInfo.CreatedOn = DateTime.Now;
                    cProductInfo.Status = 1;

                    lstNewCompetProduct.Add(cProductInfo);
                }
                else
                {
                    cProductInfo.ProductName = productInfo.Name;
                    cProductInfo.ImageUrl = productInfo.Image.ToString();
                    cProductInfo.IsNew = 0;
                    cProductInfo.ModifiedOn = DateTime.Now;
                    cProductInfo.Status = 1;
                }

                //# 3.Insert Product Statistic
                var existPrdStatistic = lstExistingPrdStatistic.Where(item => item.ProductId == cProductInfo.Id).FirstOrDefault();
                var prdPageItem = lstprdPageItem.Where(item => item.ProductId == productInfo.ProductId).FirstOrDefault();

                if (prdPageItem != null)
                {
                    productStatistic = ExtractStatistic(productInfo, prdPageItem, existPrdStatistic);

                    if (cProductInfo.Id == 0)
                    {
                        cProductInfo.HqqCpProductStatistic.Add(productStatistic);
                        this.Context.HqqCompetitorProduct
                            .Add(cProductInfo);
                    }
                    else
                    {
                        this.Context.HqqCompetitorShop.Attach(hqqCShop);
                        this.Context.HqqCompetitorProduct.Attach(cProductInfo);
                        productStatistic.ProductId = cProductInfo.Id;
                        this.Context.HqqCpProductStatistic.Add(productStatistic);
                    }

                }

                Log.Information("Save Product: {0}", productInfo.Name);

                try
                {
                    ///#DEBUG 
                    this.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Error("Found error on Save Product: {0}, Error: {1}", productInfo.Name, ex.Message);
                }

            }
        }

        private static HqqCpProductStatistic ExtractStatistic(
            ProductDisplayItem productInfo,
            ProductPageItem prdPageItem,
            HqqCpProductStatistic existPrdStatistic)
        {
            HqqCpProductStatistic productStatistic = new HqqCpProductStatistic();
            productStatistic.Available = 0;
            productStatistic.SaleHistory = prdPageItem.Sold;

            if (productInfo.Offers.LowPrice == null) productInfo.Offers.LowPrice = "0";
            if (productInfo.Offers.HighPrice == null) productInfo.Offers.HighPrice = "0";
            if (productInfo.Offers.Price == null) productInfo.Offers.Price = "0";

            productStatistic.Price =
                (productInfo.Offers.Price != "0"
                ? decimal.Parse(productInfo.Offers.Price)
                : (decimal.Parse(productInfo.Offers.LowPrice) < 100
                    ? decimal.Parse(productInfo.Offers.HighPrice)
                    : decimal.Parse(productInfo.Offers.LowPrice)));

            if (productInfo.AggregateRating != null)
            {
                productStatistic.RatingCount = productInfo.AggregateRating.RatingCount;
                productStatistic.RatingValue = decimal.Parse(productInfo.AggregateRating.RatingValue);
            }

            productStatistic.LikedCount = prdPageItem.Liked;
            productStatistic.Stock = prdPageItem.Stock;
            productStatistic.CreatedOn = DateTime.Now;
            productStatistic.Status = 1;

            //# MOVEMENT STATISTIC

            if (existPrdStatistic != null)
            {
                productStatistic.StockMovement = (int)(productStatistic.Stock - existPrdStatistic.Stock);
                if (productStatistic.StockMovement == 0)
                {
                    productStatistic.StockMovementPercentage = 0;
                }
                else
                {
                    productStatistic.StockMovementPercentage = (Math.Abs((decimal)productStatistic.StockMovement) / existPrdStatistic.Stock) * 100;
                    if (productStatistic.StockMovement < 0)
                    {
                        productStatistic.StockMovementPercentage = productStatistic.StockMovementPercentage * (-1);
                    }
                }

                productStatistic.SaleMovement = (int)(productStatistic.SaleHistory - existPrdStatistic.SaleHistory);
                if (productStatistic.SaleMovement == 0)
                {
                    productStatistic.SaleMovementPercentage = 0;
                }
                else
                {
                    productStatistic.SaleMovementPercentage = ((decimal)productStatistic.SaleMovement / (decimal)existPrdStatistic.SaleHistory) * 100;
                    productStatistic.SaleMovementPercentage = Math.Round(productStatistic.SaleMovementPercentage.Value, 2, MidpointRounding.AwayFromZero);

                }

                productStatistic.PriceMovement = 0;
                productStatistic.PriceMovement = productStatistic.Price - existPrdStatistic.Price;
                if (productStatistic.PriceMovement == 0)
                {
                    productStatistic.PriceMovementPercentage = 0;
                }
                else
                {
                    productStatistic.PriceMovementPercentage = ((decimal)productStatistic.PriceMovement / (decimal)existPrdStatistic.Price) * 100;
                    productStatistic.PriceMovementPercentage = Math.Round(productStatistic.PriceMovementPercentage.Value, 2, MidpointRounding.AwayFromZero);
                }

                productStatistic.LikedMovement = (int)(productStatistic.LikedCount - existPrdStatistic.LikedCount);
                if (productStatistic.LikedMovement == 0)
                {
                    productStatistic.LikedPercentage = 0;
                }
                else
                {
                    productStatistic.LikedPercentage = ((decimal)productStatistic.LikedMovement / (decimal)existPrdStatistic.LikedCount) * 100;
                    productStatistic.LikedPercentage = Math.Round(productStatistic.LikedPercentage.Value, 2, MidpointRounding.AwayFromZero);
                }

            }
            else
            {
                productStatistic.StockMovement = 0;
                productStatistic.SaleMovement = 0;
                productStatistic.SaleMovementPercentage = 0;
                productStatistic.PriceMovement = 0;
                productStatistic.PriceMovementPercentage = 0;
                productStatistic.LikedMovement = 0;
                productStatistic.LikedPercentage = 0;
            }

            return productStatistic;
        }

        public enum TPOrder { StockMovement, SaleMovement };
        public List<HqqvCproducts> GetTopProductInfo(TPOrder tpOrder, int limit)
        {
            List<HqqvCproducts> result = new List<HqqvCproducts>();

            DateTime maxDate = Context.HqqvCproducts.Max(item => item.CreatedOn);

            switch (tpOrder)
            {
                case TPOrder.StockMovement:
                    result = Context.HqqvCproducts
                        .Where(item => item.CreatedOn >= maxDate.AddDays(-1))
                        .OrderBy(item => item.StockMovement)
                        .ThenByDescending(item => item.SaleHistory)
                        .Take(limit)
                        .ToList();

                    break;

                case TPOrder.SaleMovement:
                    result = Context.HqqvCproducts
                        .Where(item => item.CreatedOn >= DateTime.Now.AddDays(-1))
                        .OrderByDescending(item => item.SaleMovement)
                        .ThenByDescending(item => item.SaleHistory)
                        .Take(limit)
                        .ToList();

                    break;
            }

            return result;
        }

        public List<CPChartData> GetTopProductChartData(int productAmount, int recordPerProduct)
        {
            List<CPChartData> result = new List<CPChartData>();
            List<HqqCpProductStatistic> cpStatistic = new List<HqqCpProductStatistic>();
            MySqlParameter paramMaxProducts = new MySqlParameter("max_products", productAmount);
            MySqlParameter paramMaxRecords = new MySqlParameter("max_records", recordPerProduct);

            var pStatistics = Context.HqqCpProductStatistic.FromSqlRaw(
                "CALL hqqsp_cproduct_chart(@max_products,@max_records)"
                , paramMaxProducts, paramMaxRecords).ToList();

            var lstProductId = pStatistics.Select(item => item.ProductId).Distinct();
            var lstProductInfo = Context.HqqCompetitorProduct.Where(item => lstProductId.Contains(item.Id)).ToList();

            foreach (var item in lstProductInfo)
            {
                result.Add(
                    new CPChartData()
                    {
                        CPProduct = item,
                        CPProductStatistic = pStatistics.Where(pItem => pItem.ProductId == item.Id).ToList()
                    });
            }

            return result;
        }

    }




}
