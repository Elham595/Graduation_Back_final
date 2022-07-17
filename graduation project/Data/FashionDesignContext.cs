using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using graduation_project.Models;

namespace graduation_project.Data
{
    public partial class FashionDesignContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AdminOwner> AdminOwners { get; set; }
        public virtual DbSet<AprovedOffer> AprovedOffers { get; set; }
        public virtual DbSet<Blogger> Bloggers { get; set; }
        public virtual DbSet<BottomMeasurement> BottomMeasurements { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientDesignerOrder> ClientDesignerOrders { get; set; }
        public virtual DbSet<ClientStoreOrder> ClientStoreOrders { get; set; }
        public virtual DbSet<ClientStoreOrderItem> ClientStoreOrderItems { get; set; }
        public virtual DbSet<Clothe> Clothes { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<CompletedOrder> CompletedOrders { get; set; }
        public virtual DbSet<Design> Designs { get; set; }
        public virtual DbSet<DesignOrder> DesignOrders { get; set; }
        public virtual DbSet<DesignOrderFabric> DesignOrderFabrics { get; set; }
        public virtual DbSet<Designer> Designers { get; set; }
        public virtual DbSet<DesignerOfDesign> DesignerOfDesigns { get; set; }
        public virtual DbSet<Fabric> Fabrics { get; set; }
        public virtual DbSet<FabricOfStore> FabricOfStores { get; set; }
        public virtual DbSet<GetRole> GetRoles { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }
        public virtual DbSet<MessageChat> MessageChats { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Tailor> Tailors { get; set; }
        public virtual DbSet<TailorOffer> TailorOffers { get; set; }
        public virtual DbSet<TailorOfferFabricMetter> TailorOfferFabricMetters { get; set; }
        public virtual DbSet<TopMeasurment> TopMeasurments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        public FashionDesignContext(DbContextOptions<FashionDesignContext> options) : base(options)
        {
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Fashion-DesignV1;Integrated Security=True;Trust Server Certificate=True;Command Timeout=300");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_User_Role");
            });

            modelBuilder.Entity<AdminOwner>(entity =>
            {
                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.AdminOwners)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK_Admin_Owner_Account");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.AdminOwner)
                    .HasForeignKey<AdminOwner>(d => d.UserName)
                    .HasConstraintName("FK_Admin_Owner_User");
            });

            modelBuilder.Entity<AprovedOffer>(entity =>
            {
                entity.Property(e => e.OfferId).ValueGeneratedNever();

                entity.Property(e => e.DateOfAproval).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecievedMethod).IsUnicode(false);

                entity.HasOne(d => d.Offer)
                    .WithOne(p => p.AprovedOffer)
                    .HasForeignKey<AprovedOffer>(d => d.OfferId)
                    .HasConstraintName("FK_Aproved_Offer_Tailor_Offer");
            });

            modelBuilder.Entity<Blogger>(entity =>
            {
                entity.Property(e => e.BloggerId).ValueGeneratedNever();

                entity.Property(e => e.AdminUserName).IsUnicode(false);

                entity.Property(e => e.BloggerName).IsUnicode(false);

                entity.HasOne(d => d.AdminUserNameNavigation)
                    .WithMany(p => p.Bloggers)
                    .HasForeignKey(d => d.AdminUserName)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Blogger_Admin_Owner");
            });

            modelBuilder.Entity<BottomMeasurement>(entity =>
            {
                entity.Property(e => e.DesignOrderNumber).ValueGeneratedNever();

                entity.HasOne(d => d.DesignOrderNumberNavigation)
                    .WithOne(p => p.BottomMeasurement)
                    .HasForeignKey<BottomMeasurement>(d => d.DesignOrderNumber)
                    .HasConstraintName("FK_Bottom_Measurement_Design_Order");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.Client)
                    .HasForeignKey<Client>(d => d.UserName)
                    .HasConstraintName("FK_Client_User");
            });

            modelBuilder.Entity<ClientDesignerOrder>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientUserName).IsUnicode(false);

                entity.Property(e => e.DesignPicture).IsUnicode(false);

                entity.Property(e => e.DesignerUserName).IsUnicode(false);

                entity.HasOne(d => d.ClientUserNameNavigation)
                    .WithMany(p => p.ClientDesignerOrders)
                    .HasForeignKey(d => d.ClientUserName)
                    .HasConstraintName("FK_Client_Designer_Order_Client");

                entity.HasOne(d => d.DesignerUserNameNavigation)
                    .WithMany(p => p.ClientDesignerOrders)
                    .HasForeignKey(d => d.DesignerUserName)
                    .HasConstraintName("FK_Client_Designer_Order_Designer");
            });

            modelBuilder.Entity<ClientStoreOrder>(entity =>
            {
                entity.Property(e => e.OrderId).IsUnicode(false);

                entity.Property(e => e.ClientUserName).IsUnicode(false);

                entity.Property(e => e.RecievedMethod).IsUnicode(false);

                entity.HasOne(d => d.ClientUserNameNavigation)
                    .WithMany(p => p.ClientStoreOrders)
                    .HasForeignKey(d => d.ClientUserName)
                    .HasConstraintName("FK_Client_Store_Order_Client");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ClientStoreOrders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Client_Store_Order_Store");
            });

            modelBuilder.Entity<ClientStoreOrderItem>(entity =>
            {
                entity.HasKey(e => e.ItemNumber)
                    .HasName("PK_Client_Store_Order_Items_1");

                entity.Property(e => e.ItemNumber).ValueGeneratedNever();

                entity.Property(e => e.OrderId).IsUnicode(false);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ClientStoreOrderItems)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Store_Order_Items_Color");

                entity.HasOne(d => d.Fabric)
                    .WithMany(p => p.ClientStoreOrderItems)
                    .HasForeignKey(d => d.FabricId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Store_Order_Items_Fabric");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ClientStoreOrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Client_Store_Order_Items_Client_Store_Order");
            });

            modelBuilder.Entity<Clothe>(entity =>
            {
                entity.Property(e => e.ClotheId).ValueGeneratedNever();

                entity.Property(e => e.ClotheName).IsUnicode(false);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.ColorId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CompletedOrder>(entity =>
            {
                entity.Property(e => e.OfferId).ValueGeneratedNever();

                entity.HasOne(d => d.Offer)
                    .WithOne(p => p.CompletedOrder)
                    .HasForeignKey<CompletedOrder>(d => d.OfferId)
                    .HasConstraintName("FK_Completed_Order_Aproved_Offer");
            });

            modelBuilder.Entity<Design>(entity =>
            {
                entity.Property(e => e.DesignDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DesignImage).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.HasOne(d => d.Cloth)
                    .WithMany(p => p.Designs)
                    .HasForeignKey(d => d.ClothId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Design_Clothe");
            });

            modelBuilder.Entity<DesignOrder>(entity =>
            {
                entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.Design)
                    .WithMany(p => p.DesignOrders)
                    .HasForeignKey(d => d.DesignId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Design_Order_Design");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.DesignOrders)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Design_Order_Client");
            });

            modelBuilder.Entity<DesignOrderFabric>(entity =>
            {
                entity.HasKey(e => new { e.FabricId, e.DesignOrderNumber, e.ColorId });

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.DesignOrderFabrics)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_Design_Order_Fabric_Color");

                entity.HasOne(d => d.DesignOrderNumberNavigation)
                    .WithMany(p => p.DesignOrderFabrics)
                    .HasForeignKey(d => d.DesignOrderNumber)
                    .HasConstraintName("FK_Design_Order_Fabric_Design_Order");

                entity.HasOne(d => d.Fabric)
                    .WithMany(p => p.DesignOrderFabrics)
                    .HasForeignKey(d => d.FabricId)
                    .HasConstraintName("FK_Design_Order_Fabric_Fabric");
            });

            modelBuilder.Entity<Designer>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK_Designer_1");

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Designers)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK_Designer_Account");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.Designer)
                    .HasForeignKey<Designer>(d => d.UserName)
                    .HasConstraintName("FK_Designer_User");
            });

            modelBuilder.Entity<DesignerOfDesign>(entity =>
            {
                entity.HasKey(e => new { e.DesignId, e.UserName });

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.Design)
                    .WithMany(p => p.DesignerOfDesigns)
                    .HasForeignKey(d => d.DesignId)
                    .HasConstraintName("FK_Designer_Of_Design_Design");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.DesignerOfDesigns)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_Designer_Of_Design_Designer");
            });

            modelBuilder.Entity<Fabric>(entity =>
            {
                entity.Property(e => e.FabricId).ValueGeneratedNever();

                entity.Property(e => e.FabricName).IsUnicode(false);
            });

            modelBuilder.Entity<FabricOfStore>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.FabricId, e.ColorId });

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.FabricOfStores)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_Fabric_Of_Store_Color");

                entity.HasOne(d => d.Fabric)
                    .WithMany(p => p.FabricOfStores)
                    .HasForeignKey(d => d.FabricId)
                    .HasConstraintName("FK_Fabric_Of_Store_Fabric");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.FabricOfStores)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Fabric_Of_Store_Store");
            });

            modelBuilder.Entity<GetRole>(entity =>
            {
                entity.ToView("GetRoles");

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            modelBuilder.Entity<Instruction>(entity =>
            {
                entity.Property(e => e.InstructionNumber).ValueGeneratedNever();

                entity.HasOne(d => d.Blogger)
                    .WithMany(p => p.Instructions)
                    .HasForeignKey(d => d.BloggerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Instruction_Blogger");
            });

            modelBuilder.Entity<MessageChat>(entity =>
            {
                entity.Property(e => e.MessageHead).IsUnicode(false);

                entity.Property(e => e.MessageText).IsUnicode(false);

                entity.Property(e => e.SendFrom).IsUnicode(false);

                entity.Property(e => e.SendTo).IsUnicode(false);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.Rating)
                    .HasForeignKey<Rating>(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_Account");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.StoreName).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK_Store_Account");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_Store_User");
            });

            modelBuilder.Entity<Tailor>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK_Tailor_1");

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Tailors)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK_Tailor_Account");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.Tailor)
                    .HasForeignKey<Tailor>(d => d.UserName)
                    .HasConstraintName("FK_Tailor_User");
            });

            modelBuilder.Entity<TailorOffer>(entity =>
            {
                entity.Property(e => e.ClientUserName).IsUnicode(false);

                entity.Property(e => e.OfferDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('P')");

                entity.Property(e => e.TailorUserName).IsUnicode(false);

                entity.HasOne(d => d.ClientUserNameNavigation)
                    .WithMany(p => p.TailorOffers)
                    .HasForeignKey(d => d.ClientUserName)
                    .HasConstraintName("FK_Tailor_Offer_Client");

                entity.HasOne(d => d.DesignOrderNumberNavigation)
                    .WithMany(p => p.TailorOffers)
                    .HasForeignKey(d => d.DesignOrderNumber)
                    .HasConstraintName("FK_Tailor_Offer_Design_Order");

                entity.HasOne(d => d.TailorUserNameNavigation)
                    .WithMany(p => p.TailorOffers)
                    .HasForeignKey(d => d.TailorUserName)
                    .HasConstraintName("FK_Tailor_Offer_Tailor");
            });

            modelBuilder.Entity<TailorOfferFabricMetter>(entity =>
            {
                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.TailorOfferFabricMetters)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tailor_Offer_Fabric_Metter_Tailor_Offer");
            });

            modelBuilder.Entity<TopMeasurment>(entity =>
            {
                entity.Property(e => e.DesignOrderNumber).ValueGeneratedNever();

                entity.HasOne(d => d.DesignOrderNumberNavigation)
                    .WithOne(p => p.TopMeasurment)
                    .HasForeignKey<TopMeasurment>(d => d.DesignOrderNumber)
                    .HasConstraintName("FK_Top_Measurment_Design_Order");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.MiddleName).IsUnicode(false);

                entity.Property(e => e.MobileNumber).IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingGeneratedFunctions(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
