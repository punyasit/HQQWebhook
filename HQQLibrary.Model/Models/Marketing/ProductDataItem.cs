using System;
using System.Collections.Generic;
using System.Text;

namespace HQQLibrary.Model.Models.Marketing
{
    public partial class ProductDataItem
    {
        public long Itemid { get; set; }
        public object WelcomePackageInfo { get; set; }
        public bool Liked { get; set; }
        public object RecommendationInfo { get; set; }
        public object BundleDealInfo { get; set; }
        public long PriceMaxBeforeDiscount { get; set; }
        public string Image { get; set; }
        public bool IsCcInstallmentPaymentEligible { get; set; }
        public long Shopid { get; set; }
        public bool CanUseWholesale { get; set; }
        public object GroupBuyInfo { get; set; }
        public string ReferenceItemId { get; set; }
        public string Currency { get; set; }
        public long RawDiscount { get; set; }
        public bool ShowFreeShipping { get; set; }
        public List<VideoInfoList> VideoInfoList { get; set; }
        public object AdsKeyword { get; set; }
        public object CollectionId { get; set; }
        public List<string> Images { get; set; }
        public object MatchType { get; set; }
        public long PriceBeforeDiscount { get; set; }
        public bool IsCategoryFailed { get; set; }
        public long ShowDiscount { get; set; }
        public long CmtCount { get; set; }
        public long ViewCount { get; set; }
        public object DisplayName { get; set; }
        public long Catid { get; set; }
        public object JsonData { get; set; }
        public object UpcomingFlashSale { get; set; }
        public bool IsOfficialShop { get; set; }
        public string Brand { get; set; }
        public long PriceMin { get; set; }
        public long LikedCount { get; set; }
        public bool CanUseBundleDeal { get; set; }
        public bool ShowOfficialShopLabel { get; set; }
        public object CoinEarnLabel { get; set; }
        public long PriceMinBeforeDiscount { get; set; }
        public long CbOption { get; set; }
        public long Sold { get; set; }
        public object DeductionInfo { get; set; }
        public long Stock { get; set; }
        public long Status { get; set; }
        public long PriceMax { get; set; }
        public object AddOnDealInfo { get; set; }
        public object IsGroupBuyItem { get; set; }
        public object FlashSale { get; set; }
        public long Price { get; set; }
        public string ShopLocation { get; set; }
        public ItemRating ItemRating { get; set; }
        public bool ShowOfficialShopLabelInTitle { get; set; }
        public List<TierVariation> TierVariations { get; set; }
        public object IsAdult { get; set; }
        public string Discount { get; set; }
        public long Flag { get; set; }
        public bool IsNonCcInstallmentPaymentEligible { get; set; }
        public bool HasLowestPriceGuarantee { get; set; }
        public object HasGroupBuyStock { get; set; }
        public object PreviewInfo { get; set; }
        public long WelcomePackageType { get; set; }
        public string Name { get; set; }
        public object Distance { get; set; }
        public object Adsid { get; set; }
        public long Ctime { get; set; }
        public List<object> WholesaleTierList { get; set; }
        public bool ShowShopeeVerifiedLabel { get; set; }
        public object Campaignid { get; set; }
        public object ShowOfficialShopLabelInNormalPosition { get; set; }
        public string ItemStatus { get; set; }
        public bool ShopeeVerified { get; set; }
        public object HiddenPriceDisplay { get; set; }
        public object SizeChart { get; set; }
        public long ItemType { get; set; }
        public object ShippingIconType { get; set; }
        public object CampaignStock { get; set; }
        public List<long> LabelIds { get; set; }
        public long ServiceByShopeeFlag { get; set; }
        public long BadgeIconType { get; set; }
        public long HistoricalSold { get; set; }
        public string TransparentBackgroundImage { get; set; }
    }

    public partial class ItemRating
    {
        public double RatingStar { get; set; }
        public List<long> RatingCount { get; set; }
        public long RcountWithImage { get; set; }
        public long RcountWithContext { get; set; }
    }

    public partial class TierVariation
    {
        public object Images { get; set; }
        public object Properties { get; set; }
        public long Type { get; set; }
        public string Name { get; set; }
        public List<string> Options { get; set; }
    }

    public partial class VideoInfoList
    {
        public long Duration { get; set; }
        public string VideoId { get; set; }
        public string ThumbUrl { get; set; }
    }

}
