using System;
using HQQLibrary.Model.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HelloQQDBContext : DbContext
    {
        public HelloQQDBContext()
        {
        }

        public HelloQQDBContext(DbContextOptions<HelloQQDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HqqCategory> HqqCategory { get; set; }
        public virtual DbSet<HqqCompetitorProduct> HqqCompetitorProduct { get; set; }
        public virtual DbSet<HqqCompetitorShop> HqqCompetitorShop { get; set; }
        public virtual DbSet<HqqCpProductStatistic> HqqCpProductStatistic { get; set; }
        public virtual DbSet<HqqDialogflow> HqqDialogflow { get; set; }
        public virtual DbSet<HqqDialogflowAddon> HqqDialogflowAddon { get; set; }
        public virtual DbSet<HqqImages> HqqImages { get; set; }
        public virtual DbSet<HqqLogLogin> HqqLogLogin { get; set; }
        public virtual DbSet<HqqMember> HqqMember { get; set; }
        public virtual DbSet<HqqMemberProduct> HqqMemberProduct { get; set; }
        public virtual DbSet<HqqMetaLocation> HqqMetaLocation { get; set; }
        public virtual DbSet<HqqPrice> HqqPrice { get; set; }
        public virtual DbSet<HqqProduct> HqqProduct { get; set; }
        public virtual DbSet<HqqProductFaq> HqqProductFaq { get; set; }
        public virtual DbSet<HqqProductImages> HqqProductImages { get; set; }
        public virtual DbSet<HqqProductManual> HqqProductManual { get; set; }
        public virtual DbSet<HqqProductReview> HqqProductReview { get; set; }
        public virtual DbSet<HqqSaleChannel> HqqSaleChannel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(new AppConfiguration().ConnectionString, x => x.ServerVersion("5.6.40-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HqqCategory>(entity =>
            {
                entity.ToTable("hqq_category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InformationUrl)
                    .HasColumnName("information_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<HqqCompetitorProduct>(entity =>
            {
                entity.ToTable("hqq_competitor_product");

                entity.HasIndex(e => e.ShopId)
                    .HasName("shop_lnk_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IsNew)
                    .HasColumnName("is_new")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductRefId)
                    .HasColumnName("product_ref_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ShopId)
                    .HasColumnName("shop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.HqqCompetitorProduct)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shop_lnk_product");
            });

            modelBuilder.Entity<HqqCompetitorShop>(entity =>
            {
                entity.ToTable("hqq_competitor_shop");

                entity.HasIndex(e => e.ChannelId)
                    .HasName("channel_lnk_competitor_shop_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ChannelId)
                    .HasColumnName("channel_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataUrl)
                    .HasColumnName("data_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Follower)
                    .HasColumnName("follower")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.PageUrl)
                    .HasColumnName("page_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RatingCount)
                    .HasColumnName("rating_count")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RatingValue)
                    .HasColumnName("rating_value")
                    .HasColumnType("decimal(3,2)");

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasColumnName("shop_name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.HqqCompetitorShop)
                    .HasForeignKey(d => d.ChannelId)
                    .HasConstraintName("channel_lnk_competitor_shop");
            });

            modelBuilder.Entity<HqqCpProductStatistic>(entity =>
            {
                entity.ToTable("hqq_cp_product_statistic");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_statistic_lnk_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Available)
                    .HasColumnName("available")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.LikeCount)
                    .HasColumnName("like_count")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PriceMovement)
                    .HasColumnName("price_movement")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PriceMovementPercentage)
                    .HasColumnName("price_movement_percentage")
                    .HasColumnType("decimal(3,2)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RatingCount)
                    .HasColumnName("rating_count")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RatingValue)
                    .HasColumnName("rating_value")
                    .HasColumnType("decimal(3,2)");

                entity.Property(e => e.SaleHistory)
                    .HasColumnName("sale_history")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SaleMovement)
                    .HasColumnName("sale_movement")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SaleMovementPercentage)
                    .HasColumnName("sale_movement_percentage")
                    .HasColumnType("decimal(3,2)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Stock)
                    .HasColumnName("stock")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.StockMovement)
                    .HasColumnName("stock_movement")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqCpProductStatistic)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_statistic_lnk_product");
            });

            modelBuilder.Entity<HqqDialogflow>(entity =>
            {
                entity.ToTable("hqq_dialogflow");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_link_dialogflow_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FlowType)
                    .HasColumnName("flow_type")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MatchKeywords)
                    .HasColumnName("match_keywords")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Payload)
                    .HasColumnName("payload")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PayloadImgUrl)
                    .HasColumnName("payload_img_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResponseAnswer)
                    .HasColumnName("response_answer")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ResponseHeader)
                    .HasColumnName("response_header")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ResponseItems)
                    .HasColumnName("response_items")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqDialogflow)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("product_link_dialogflow");
            });

            modelBuilder.Entity<HqqDialogflowAddon>(entity =>
            {
                entity.ToTable("hqq_dialogflow_addon");

                entity.HasIndex(e => e.DialogflowId)
                    .HasName("dialogflow_lnk_addon_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_lnk_addon_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DialogflowId)
                    .HasColumnName("dialogflow_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("varchar(15)")
                    .HasComment("web_url, postback, video")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Dialogflow)
                    .WithMany(p => p.HqqDialogflowAddon)
                    .HasForeignKey(d => d.DialogflowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dialogflow_lnk_addon");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqDialogflowAddon)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_lnk_addon");
            });

            modelBuilder.Entity<HqqImages>(entity =>
            {
                entity.ToTable("hqq_images");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EntityId)
                    .HasColumnName("entity_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<HqqLogLogin>(entity =>
            {
                entity.ToTable("hqq_log_login");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(8)");

                entity.Property(e => e.LoginTime)
                    .HasColumnName("login_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MemberId)
                    .HasColumnName("member_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<HqqMember>(entity =>
            {
                entity.ToTable("hqq_member");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.FacebookId)
                    .HasColumnName("facebook_id")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FacebookName)
                    .IsRequired()
                    .HasColumnName("facebook_name")
                    .HasColumnType("varchar(125)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnName("fullname")
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HometownCode)
                    .HasColumnName("hometown_code")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LocationCode)
                    .HasColumnName("location_code")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.PictureUrl)
                    .HasColumnName("picture_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasColumnName("postcode")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<HqqMemberProduct>(entity =>
            {
                entity.ToTable("hqq_member_product");

                entity.HasIndex(e => e.MemberId)
                    .HasName("member_product_link_member_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("member_product_lnk_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GaranteeExpired)
                    .HasColumnName("garantee_expired")
                    .HasColumnType("datetime");

                entity.Property(e => e.MemberId)
                    .HasColumnName("member_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("purchase_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.HqqMemberProduct)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("member_product_lnk_member");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqMemberProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("member_product_lnk_product");
            });

            modelBuilder.Entity<HqqMetaLocation>(entity =>
            {
                entity.ToTable("hqq_meta_location");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsoCode)
                    .IsRequired()
                    .HasColumnName("ISO_code")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Keyword)
                    .HasColumnName("keyword")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("'TH'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<HqqPrice>(entity =>
            {
                entity.ToTable("hqq_price");

                entity.HasIndex(e => e.ChannelId)
                    .HasName("product_lnk_channel_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("hqq_product_price_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AffiliateUrl)
                    .HasColumnName("affiliate_url")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ChannelId)
                    .HasColumnName("channel_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.PriceDate)
                    .HasColumnName("price_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.HqqPrice)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_lnk_channel");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqPrice)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_lnk_price");
            });

            modelBuilder.Entity<HqqProduct>(entity =>
            {
                entity.ToTable("hqq_product");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("category_link_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InformationUrl)
                    .HasColumnName("information_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PreviewImageUrl)
                    .HasColumnName("preview_image_url")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.HqqProduct)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_link_product");
            });

            modelBuilder.Entity<HqqProductFaq>(entity =>
            {
                entity.ToTable("hqq_product_faq");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_faq_lnk_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Answer)
                    .HasColumnName("answer")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Question)
                    .HasColumnName("question")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqProductFaq)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_faq_lnk_product");
            });

            modelBuilder.Entity<HqqProductImages>(entity =>
            {
                entity.ToTable("hqq_product_images");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_images_lnk_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_images_lnk_product");
            });

            modelBuilder.Entity<HqqProductManual>(entity =>
            {
                entity.ToTable("hqq_product_manual");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_manual_lnk_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqProductManual)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_manual_lnk_product");
            });

            modelBuilder.Entity<HqqProductReview>(entity =>
            {
                entity.ToTable("hqq_product_review");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_review_lnk_product_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .IsRequired()
                    .HasColumnName("created_on")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasColumnName("review")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ShortDescription)
                    .IsRequired()
                    .HasColumnName("short_description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.HqqProductReview)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_review_lnk_product");
            });

            modelBuilder.Entity<HqqSaleChannel>(entity =>
            {
                entity.ToTable("hqq_sale_channel");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasComment("Facebook, Shopee, Lazada, Line, Direct")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
