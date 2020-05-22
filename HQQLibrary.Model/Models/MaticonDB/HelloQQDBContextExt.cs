using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HelloQQDBContext : DbContext
    {
        public DbSet<HqqvTopPurchaseProduct> HqqvTopPurchaseProduct { get; set; }
    }

    public partial class HqqvTopPurchaseProduct
    {
        [Key]
        public long Id { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public int ProductId { get; set; }
        public long ProductRefId { get; set; }
       // public string ImageUrl { get; set; }
        //public bool IsNew { get; set; }
        public int Available { get; set; }
        public long SaleHistory { get; set; }
        public int SaleMovement { get; set; }
        public decimal SaleMovementPercentage { get; set; }
        public decimal Price { get; set; }
       // public decimal PriceMovement { get; set; }
       // public decimal PriceMovementPercentage { get; set; }
        public long RatingCount { get; set; }
        public decimal RatingValue { get; set; }
        //public long LikedCount { get; set; }
        //public int LikedMovement { get; set; }
        //public decimal LikedPercentage { get; set; }
        public long Stock { get; set; }
        public int StockMovement { get; set; }
        public decimal StockMovementPercentage { get; set; }


    }
}
