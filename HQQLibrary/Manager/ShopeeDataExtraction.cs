﻿using System;
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
        private static string PAGE_URL = "https://shopee.co.th/shop/{0}/search?page={1}&sortBy=pop";
        private static string DATA_URL = "https://shopee.co.th/api/v2/search_items/?by=pop&limit=30&match_id={0}&newest={1}&order=desc&page_type=shop&version=2";
        private static int ITEM_LIMIT = 30;

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

        public void ExecuteGatherShopInfo()
        {
            string strProductData = string.Empty;
            string strPageData = string.Empty;
            List<HqqCompetitorShop> lstCompeteShop = new List<HqqCompetitorShop>();
            List<ProductPageItem> lstprdPageItem = new List<ProductPageItem>();
            ProductPageItem prdPageItem = new ProductPageItem();

            Regex rxExtract = new Regex(@"\(([^\)]*)\)");
            Match rxMatch;
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

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                foreach (var cpShopItem in lstCompeteShop)
                {
                    //DEBUG 
                    // cpShopItem.RunPageNo = 1;

                    for (int iPage = 0; iPage < cpShopItem.RunPageNo; iPage++)
                    {
                        try
                        {
                            execPageURL = string.Format(PAGE_URL, cpShopItem.ShopId, iPage);
                            execDataURL = string.Format(DATA_URL, cpShopItem.ShopId, (ITEM_LIMIT * iPage));

                            //driver.Navigate().GoToUrl(execDataURL);
                            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
                            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            //var result = wait.Until(ExpectedConditions.ElementExists(By.TagName("pre")));
                            //strProductData = driver.FindElementByTagName("pre").GetAttribute("innerHTML");
                            //File.WriteAllText(this.AssemblyDirectory + "/dummy-overalljson.txt", strJsonObj);

                            strProductData = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-overalljson.txt");

                            driver.Navigate().GoToUrl(execPageURL);
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
                            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            var result = wait.Until(ExpectedConditions.ElementExists(By.ClassName("shop-search-result-view__item")));
                            strPageData = driver.FindElementByTagName("head").GetAttribute("innerHTML");
                            var lstShopSelectResult = driver.FindElementsByClassName("shop-search-result-view__item");

                            //File.WriteAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt", lstShopSelectResult[0].GetAttribute("innerHTML"));
                            //strProductData = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt");

                            ////# Load page that need to Get information.
                            foreach (var item in lstShopSelectResult)
                            {
                                prdPageItem = new ProductPageItem();
                                prdPageItem.URL = item.FindElement(By.TagName("a")).GetAttribute("href");
                                prdPageItem.ProductId = (long.Parse(prdPageItem.URL.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last()));

                                //#FIND AMOUNT IF AMOUNT < 1000 THEN COlLECT TO STATISTIC
                                var tmpPrice = item.FindElement(By.XPath("//a/div/div[2]/div[3]/div[3]"));
                                if (tmpPrice != null)
                                {
                                    strConvertPrice = tmpPrice.Text.Replace("ขายแล้ว ", "").Replace(" ชิ้น", "");
                                    if (strConvertPrice.Contains("พัน"))
                                    {
                                        strConvertPrice = strConvertPrice.Replace("พัน", "");
                                        var converted = Convert.ToDecimal(strConvertPrice);
                                        deConvertPrice = (int)(converted * 1000);
                                    }
                                    else if (strConvertPrice.Contains("หมื่น"))
                                    {
                                        strConvertPrice = strConvertPrice.Replace("หมื่น", "");
                                        var converted = Convert.ToDecimal(strConvertPrice);
                                        deConvertPrice = (int)(deConvertPrice * 10000);
                                    }
                                    else
                                    {
                                        deConvertPrice = Convert.ToInt32(strConvertPrice);
                                    }
                                }
                                else
                                {
                                    deConvertPrice = 0;
                                }

                                if (deConvertPrice < 1000)
                                {
                                    prdPageItem.Sold = deConvertPrice;
                                    lstprdPageItem.Add(prdPageItem);
                                }

                                prdPageItem.Sold = deConvertPrice;
                                lstprdPageItem.Add(prdPageItem);
                            }

                            File.WriteAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt", JsonConvert.SerializeObject(lstprdPageItem));
                            //string strObjItem = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-product_item.txt");
                            //lstprdPageItem = JsonConvert.DeserializeObject<List<ProductPageItem>>(strObjItem);

                            //# Iterage to the each page for get information.
                            foreach (var item in lstprdPageItem)
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
                                    strConvertPrice = liked.GetAttribute("textContent");
                                    rxMatch = rxExtract.Match(strConvertPrice);
                                    if (rxMatch.Success)
                                    {
                                        strConvertPrice = rxMatch.Value.Replace("(", "").Replace(")", "");
                                    }

                                    if (strConvertPrice.Contains("พัน"))
                                    {
                                        strConvertPrice = strConvertPrice.Replace("พัน", "");
                                        var converted = Convert.ToDecimal(strConvertPrice);
                                        deConvertPrice = (int)(converted * 1000);
                                    }
                                    else if (strConvertPrice.Contains("หมื่น"))
                                    {
                                        strConvertPrice = strConvertPrice.Replace("หมื่น", "");
                                        var converted = Convert.ToDecimal(strConvertPrice);
                                        deConvertPrice = (int)(deConvertPrice * 10000);
                                    }
                                    else
                                    {
                                        deConvertPrice = Convert.ToInt32(strConvertPrice);
                                    }

                                    item.Liked = (int)deConvertPrice;
                                }
                            }

                            //strPageData = File.ReadAllText(HQQUtilities.AssemblyDirectory + "/dummy-htmlfile.txt");

                        }
                        catch (Exception ex)
                        {
                            string strEx = ex.ToString();
                        }
                        finally
                        {

                        }

                        this.ExtractProductData(cpShopItem, strProductData, strPageData, lstprdPageItem);
                    }
                }

                driver.Close();
                driver.Quit();
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
                var cProductInfo = lstExistingCompetPrd.Where(item => item.ProductRefId == productInfo.ProductId).FirstOrDefault();
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
                }
                else
                {
                    cProductInfo.ProductName = productInfo.Name;
                    cProductInfo.ImageUrl = productInfo.Image.ToString();
                    cProductInfo.ModifiedOn = DateTime.Now;
                    cProductInfo.Status = 1;
                }

                //# 3.Insert Product Statistic
                var existPrdStatistic = lstExistingPrdStatistic.Where(item => item.ProductId == cProductInfo.Id).FirstOrDefault();
                var prdPageItem = lstprdPageItem.Where(item => item.ProductId == productInfo.ProductId).FirstOrDefault();
                productStatistic = ExtractStatistic(productInfo, prdPageItem, existPrdStatistic);

                if (cProductInfo.Id == 0)
                {
                    cProductInfo.HqqCpProductStatistic.Add(productStatistic);
                    this.Context.HqqCompetitorProduct
                        .Add(cProductInfo);
                }
                else
                {
                    productStatistic.ProductId = cProductInfo.Id;
                    this.Context.HqqCompetitorShop.Attach(hqqCShop);
                    this.Context.HqqCompetitorProduct.Attach(cProductInfo);
                    this.Context.HqqCpProductStatistic.Add(productStatistic);
                }

                this.Context.SaveChanges();
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

            productStatistic.RatingCount = productInfo.AggregateRating.RatingCount;
            productStatistic.RatingValue = decimal.Parse(productInfo.AggregateRating.RatingValue);
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
                    productStatistic.SaleMovementPercentage = (productStatistic.SaleMovement / existPrdStatistic.SaleHistory) * 100;
                    
                }

                productStatistic.PriceMovement = 0;
                productStatistic.PriceMovement = productStatistic.PriceMovement - existPrdStatistic.PriceMovement;
                if (productStatistic.PriceMovement == 0)
                {
                    productStatistic.PriceMovementPercentage = 0;
                }
                else
                {
                    productStatistic.PriceMovementPercentage = (productStatistic.PriceMovement / existPrdStatistic.PriceMovement) * 100;
                }

                productStatistic.LikedMovement = (int)(productStatistic.LikedCount - existPrdStatistic.LikedCount);
                if (productStatistic.LikedMovement == 0)
                {
                    productStatistic.LikedPercentage = 0;
                }
                else
                {
                    productStatistic.LikedPercentage = (productStatistic.LikedMovement / existPrdStatistic.LikedCount) *100;
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
    }


}
