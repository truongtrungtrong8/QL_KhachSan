using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class QL_KhachSanContext : DbContext
    {
        public QL_KhachSanContext()
        {
        }

        public QL_KhachSanContext(DbContextOptions<QL_KhachSanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Businessregistration> Businessregistrations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customerreview> Customerreviews { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Orderroom> Orderrooms { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Pricelist> Pricelists { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Roomimage> Roomimages { get; set; }
        public virtual DbSet<Typeofaccount> Typeofaccounts { get; set; }
        public virtual DbSet<Typeofroom> Typeofrooms { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ASUS;Database=QL_KhachSan;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountUsername)
                    .IsClustered(false);

                entity.ToTable("ACCOUNT");

                entity.HasIndex(e => e.ToaId, "RELATIONSHIP_7_FK");

                entity.Property(e => e.AccountUsername)
                    .HasMaxLength(20)
                    .HasColumnName("ACCOUNT_USERNAME");

                entity.Property(e => e.AccountPassword)
                    .HasMaxLength(20)
                    .HasColumnName("ACCOUNT_PASSWORD");

                entity.Property(e => e.ToaId).HasColumnName("TOA_ID");

                entity.HasOne(d => d.Toa)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ToaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACCOUNT_RELATIONS_TYPEOFAC");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .IsClustered(false);

                entity.ToTable("ADMIN");

                entity.HasIndex(e => e.AccountUsername, "RELATIONSHIP_16_FK");

                entity.Property(e => e.AdminId)
                    .HasMaxLength(20)
                    .HasColumnName("ADMIN_ID");

                entity.Property(e => e.AccountUsername)
                    .HasMaxLength(20)
                    .HasColumnName("ACCOUNT_USERNAME");

                entity.Property(e => e.AdminAddress)
                    .HasMaxLength(100)
                    .HasColumnName("ADMIN_ADDRESS");

                entity.Property(e => e.AdminBankaccount)
                    .HasMaxLength(20)
                    .HasColumnName("ADMIN_BANKACCOUNT");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(50)
                    .HasColumnName("ADMIN_NAME");

                entity.Property(e => e.AdminPhone)
                    .HasMaxLength(12)
                    .HasColumnName("ADMIN_PHONE");

                entity.Property(e => e.AdminSex).HasColumnName("ADMIN_SEX");

                entity.HasOne(d => d.AccountUsernameNavigation)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.AccountUsername)
                    .HasConstraintName("FK_ADMIN_RELATIONS_ACCOUNT");
            });

            modelBuilder.Entity<Businessregistration>(entity =>
            {
                entity.HasKey(e => e.BrId)
                    .IsClustered(false);

                entity.ToTable("BUSINESSREGISTRATION");

                entity.HasIndex(e => e.OwnerId, "RELATIONSHIP_14_FK");

                entity.HasIndex(e => e.HotelId, "RELATIONSHIP_15_FK");

                entity.HasIndex(e => e.PricelistId, "RELATIONSHIP_19_FK");

                entity.Property(e => e.BrId).HasColumnName("BR_ID");

                entity.Property(e => e.BrDate)
                    .HasColumnType("datetime")
                    .HasColumnName("BR_DATE");

                entity.Property(e => e.BrStatus).HasColumnName("BR_STATUS");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.OwnerId).HasColumnName("OWNER_ID");

                entity.Property(e => e.PricelistId).HasColumnName("PRICELIST_ID");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Businessregistrations)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUSINESS_RELATIONS_HOTEL");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Businessregistrations)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUSINESS_RELATIONS_OWNER");

                entity.HasOne(d => d.Pricelist)
                    .WithMany(p => p.Businessregistrations)
                    .HasForeignKey(d => d.PricelistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUSINESS_RELATIONS_PRICELIS");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .IsClustered(false);

                entity.ToTable("CUSTOMER");

                entity.HasIndex(e => e.AccountUsername, "RELATIONSHIP_6_FK");

                entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.AccountUsername)
                    .HasMaxLength(20)
                    .HasColumnName("ACCOUNT_USERNAME");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(100)
                    .HasColumnName("CUSTOMER_ADDRESS");

                entity.Property(e => e.CustomerBankaccount)
                    .HasMaxLength(20)
                    .HasColumnName("CUSTOMER_BANKACCOUNT");

                entity.Property(e => e.CustomerDateofbirth)
                    .HasColumnType("datetime")
                    .HasColumnName("CUSTOMER_DATEOFBIRTH");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(50)
                    .HasColumnName("CUSTOMER_EMAIL");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(70)
                    .HasColumnName("CUSTOMER_NAME");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(11)
                    .HasColumnName("CUSTOMER_PHONE");

                entity.Property(e => e.CustomerSex).HasColumnName("CUSTOMER_SEX");

                entity.HasOne(d => d.AccountUsernameNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AccountUsername)
                    .HasConstraintName("FK_CUSTOMER_RELATIONS_ACCOUNT");
            });

            modelBuilder.Entity<Customerreview>(entity =>
            {
                entity.HasKey(e => new { e.HotelId, e.RoomId, e.CustomerId });

                entity.ToTable("CUSTOMERREVIEW");

                entity.HasIndex(e => e.CustomerId, "CUSTOMERREVIEW2_FK");

                entity.HasIndex(e => new { e.HotelId, e.RoomId }, "CUSTOMERREVIEW_FK");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.CrComment)
                    .HasMaxLength(200)
                    .HasColumnName("CR_COMMENT");

                entity.Property(e => e.CrStar).HasColumnName("CR_STAR");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Customerreviews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMER_CUSTOMERR_CUSTOMER");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Customerreviews)
                    .HasForeignKey(d => new { d.HotelId, d.RoomId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMER_CUSTOMERR_ROOM");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => new { e.ProvinceId, e.DistrictId })
                    .IsClustered(false);

                entity.ToTable("DISTRICT");

                entity.HasIndex(e => e.ProvinceId, "RELATIONSHIP_1_FK");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.DistrictId)
                    .HasMaxLength(10)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(100)
                    .HasColumnName("DISTRICT_NAME");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DISTRICT_RELATIONS_PROVINCE");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.HotelId)
                    .IsClustered(false);

                entity.ToTable("HOTEL");

                entity.HasIndex(e => new { e.ProvinceId, e.DistrictId, e.WardId }, "RELATIONSHIP_17_FK");

                entity.HasIndex(e => e.OwnerId, "RELATIONSHIP_21_FK");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.DistrictId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.HotelAdd)
                    .HasMaxLength(50)
                    .HasColumnName("HOTEL_ADD");

                entity.Property(e => e.HotelAvt)
                    .HasMaxLength(30)
                    .HasColumnName("HOTEL_AVT");

                entity.Property(e => e.HotelName)
                    .HasMaxLength(50)
                    .HasColumnName("HOTEL_NAME");

                entity.Property(e => e.OwnerId).HasColumnName("OWNER_ID");

                entity.Property(e => e.PointId).HasColumnName("POINT_ID");

                entity.Property(e => e.ProvinceId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.WardId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("WARD_ID");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOTEL_RELATIONS_OWNER");

                entity.HasOne(d => d.Point)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.PointId)
                    .HasConstraintName("FK_HOTEL_RELATIONS_POINT");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => new { d.ProvinceId, d.DistrictId, d.WardId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOTEL_RELATIONS_WARD");
            });

            modelBuilder.Entity<Orderroom>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .IsClustered(false);

                entity.ToTable("ORDERROOM");

                entity.HasIndex(e => e.CustomerId, "RELATIONSHIP_11_FK");

                entity.HasIndex(e => new { e.HotelId, e.RoomId }, "RELATIONSHIP_12_FK");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ORDER_DATE");

                entity.Property(e => e.OrderDayend)
                    .HasColumnType("datetime")
                    .HasColumnName("ORDER_DAYEND");

                entity.Property(e => e.OrderDaystart)
                    .HasColumnType("datetime")
                    .HasColumnName("ORDER_DAYSTART");

                entity.Property(e => e.OrderPrice).HasColumnName("ORDER_PRICE");

                entity.Property(e => e.OrderStatus).HasColumnName("ORDER_STATUS");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orderrooms)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERROO_RELATIONS_CUSTOMER");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Orderrooms)
                    .HasForeignKey(d => new { d.HotelId, d.RoomId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERROO_RELATIONS_ROOM");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.OwnerId)
                    .IsClustered(false);

                entity.ToTable("OWNER");

                entity.HasIndex(e => e.AccountUsername, "RELATIONSHIP_18_FK");

                entity.Property(e => e.OwnerId).HasColumnName("OWNER_ID");

                entity.Property(e => e.AccountUsername)
                    .HasMaxLength(20)
                    .HasColumnName("ACCOUNT_USERNAME");

                entity.Property(e => e.OwnerAddress)
                    .HasMaxLength(100)
                    .HasColumnName("OWNER_ADDRESS");

                entity.Property(e => e.OwnerBankaccount)
                    .HasMaxLength(20)
                    .HasColumnName("OWNER_BANKACCOUNT");

                entity.Property(e => e.OwnerDateofbirth)
                    .HasColumnType("datetime")
                    .HasColumnName("OWNER_DATEOFBIRTH");

                entity.Property(e => e.OwnerEmail)
                    .HasMaxLength(50)
                    .HasColumnName("OWNER_EMAIL");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(60)
                    .HasColumnName("OWNER_NAME");

                entity.Property(e => e.OwnerPhone)
                    .HasMaxLength(12)
                    .HasColumnName("OWNER_PHONE");

                entity.Property(e => e.OwnerSex).HasColumnName("OWNER_SEX");

                entity.HasOne(d => d.AccountUsernameNavigation)
                    .WithMany(p => p.Owners)
                    .HasForeignKey(d => d.AccountUsername)
                    .HasConstraintName("FK_OWNER_RELATIONS_ACCOUNT");
            });

            modelBuilder.Entity<Point>(entity =>
            {
                entity.HasKey(e => e.PointId)
                    .IsClustered(false);

                entity.ToTable("POINT");

                entity.Property(e => e.PointId).HasColumnName("POINT_ID");

                entity.Property(e => e.PointX).HasColumnName("POINT_X");

                entity.Property(e => e.PointY).HasColumnName("POINT_Y");
            });

            modelBuilder.Entity<Pricelist>(entity =>
            {
                entity.HasKey(e => e.PricelistId)
                    .IsClustered(false);

                entity.ToTable("PRICELIST");

                entity.Property(e => e.PricelistId).HasColumnName("PRICELIST_ID");

                entity.Property(e => e.PricelistName)
                    .HasMaxLength(50)
                    .HasColumnName("PRICELIST_NAME");

                entity.Property(e => e.PricelistPrice).HasColumnName("PRICELIST_PRICE");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvinceId)
                    .IsClustered(false);

                entity.ToTable("PROVINCE");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(60)
                    .HasColumnName("PROVINCE_NAME");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => new { e.HotelId, e.RoomId })
                    .IsClustered(false);

                entity.ToTable("ROOM");

                entity.HasIndex(e => e.HotelId, "RELATIONSHIP_5_FK");

                entity.HasIndex(e => e.TorId, "RELATIONSHIP_9_FK");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.RoomId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROOM_ID");

                entity.Property(e => e.RoomDescript)
                    .HasMaxLength(1000)
                    .HasColumnName("ROOM_DESCRIPT");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(20)
                    .HasColumnName("ROOM_NAME");

                entity.Property(e => e.RoomStatus).HasColumnName("ROOM_STATUS");

                entity.Property(e => e.TorId).HasColumnName("TOR_ID");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_RELATIONS_HOTEL");

                entity.HasOne(d => d.Tor)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.TorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_RELATIONS_TYPEOFRO");
            });

            modelBuilder.Entity<Roomimage>(entity =>
            {
                entity.HasKey(e => e.RoomimageId)
                    .IsClustered(false);

                entity.ToTable("ROOMIMAGE");

                entity.HasIndex(e => new { e.HotelId, e.RoomId }, "RELATIONSHIP_20_FK");

                entity.Property(e => e.RoomimageId).HasColumnName("ROOMIMAGE_ID");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.RoomimageAvt)
                    .HasMaxLength(30)
                    .HasColumnName("ROOMIMAGE_AVT");

                entity.Property(e => e.RoomimageI1)
                    .HasMaxLength(30)
                    .HasColumnName("ROOMIMAGE_I1");

                entity.Property(e => e.RoomimageI2)
                    .HasMaxLength(30)
                    .HasColumnName("ROOMIMAGE_I2");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Roomimages)
                    .HasForeignKey(d => new { d.HotelId, d.RoomId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOMIMAG_RELATIONS_ROOM");
            });

            modelBuilder.Entity<Typeofaccount>(entity =>
            {
                entity.HasKey(e => e.ToaId)
                    .IsClustered(false);

                entity.ToTable("TYPEOFACCOUNT");

                entity.Property(e => e.ToaId).HasColumnName("TOA_ID");

                entity.Property(e => e.ToaPower)
                    .HasMaxLength(20)
                    .HasColumnName("TOA_POWER");
            });

            modelBuilder.Entity<Typeofroom>(entity =>
            {
                entity.HasKey(e => e.TorId)
                    .IsClustered(false);

                entity.ToTable("TYPEOFROOM");

                entity.HasIndex(e => e.OwnerId, "RELATIONSHIP_13_FK");

                entity.Property(e => e.TorId).HasColumnName("TOR_ID");

                entity.Property(e => e.OwnerId).HasColumnName("OWNER_ID");

                entity.Property(e => e.TorName)
                    .HasMaxLength(20)
                    .HasColumnName("TOR_NAME");

                entity.Property(e => e.TorPrice).HasColumnName("TOR_PRICE");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Typeofrooms)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TYPEOFRO_RELATIONS_OWNER");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => new { e.ProvinceId, e.DistrictId, e.WardId })
                    .IsClustered(false);

                entity.ToTable("WARD");

                entity.HasIndex(e => new { e.ProvinceId, e.DistrictId }, "RELATIONSHIP_2_FK");

                entity.Property(e => e.ProvinceId)
                    .HasMaxLength(10)
                    .HasColumnName("PROVINCE_ID");

                entity.Property(e => e.DistrictId)
                    .HasMaxLength(10)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.WardId)
                    .HasMaxLength(10)
                    .HasColumnName("WARD_ID");

                entity.Property(e => e.WardLevel).HasColumnName("WARD_LEVEL");

                entity.Property(e => e.WardName)
                    .HasMaxLength(100)
                    .HasColumnName("WARD_NAME");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => new { d.ProvinceId, d.DistrictId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WARD_RELATIONS_DISTRICT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
