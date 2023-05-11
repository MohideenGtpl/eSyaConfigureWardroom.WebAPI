using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConfigureWardRoom.DL.Entities
{
    public partial class eSyaEnterpriseContext : DbContext
    {
        public static string _connString = "";
        public eSyaEnterpriseContext()
        {
        }

        public eSyaEnterpriseContext(DbContextOptions<eSyaEnterpriseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GtEcbsln> GtEcbsln { get; set; }
        public virtual DbSet<GtEcstrm> GtEcstrm { get; set; }
        public virtual DbSet<GtEswrbl> GtEswrbl { get; set; }
        public virtual DbSet<GtEswrlc> GtEswrlc { get; set; }
        public virtual DbSet<GtEswrms> GtEswrms { get; set; }
        public virtual DbSet<GtEswrpa> GtEswrpa { get; set; }
        public virtual DbSet<GtEswrrm> GtEswrrm { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<GtEcbsln>(entity =>
            {
                entity.HasKey(e => new { e.BusinessId, e.SegmentId })
                    .HasName("PK_GT_ECBSLN_1");

                entity.ToTable("GT_ECBSLN");

                entity.HasIndex(e => e.BusinessKey)
                    .HasName("IX_GT_ECBSLN")
                    .IsUnique();

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.SegmentId).HasColumnName("SegmentID");

                entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.EActiveUsers)
                    .IsRequired()
                    .HasColumnName("eActiveUsers");

                entity.Property(e => e.EBusinessKey)
                    .IsRequired()
                    .HasColumnName("eBusinessKey");

                entity.Property(e => e.ENoOfBeds).HasColumnName("eNoOfBeds");

                entity.Property(e => e.ESyaLicenseType)
                    .IsRequired()
                    .HasColumnName("eSyaLicenseType")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EUserLicenses)
                    .IsRequired()
                    .HasColumnName("eUserLicenses");

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("FormID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.LocationCode)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.LocationDescription)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.TocurrConversion).HasColumnName("TOCurrConversion");

                entity.Property(e => e.TolocalCurrency)
                    .IsRequired()
                    .HasColumnName("TOLocalCurrency")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TorealCurrency).HasColumnName("TORealCurrency");
            });

            modelBuilder.Entity<GtEcstrm>(entity =>
            {
                entity.HasKey(e => e.StoreCode)
                    .HasName("PK_GT_ECSTRM_1");

                entity.ToTable("GT_ECSTRM");

                entity.Property(e => e.StoreCode).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("FormID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.StoreDesc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StoreType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GtEswrbl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.WardId, e.RoomId, e.LocationId });

                entity.ToTable("GT_ESWRBL");

                entity.Property(e => e.WardId).HasColumnName("WardID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("FormID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEswrlc>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.LocationId });

                entity.ToTable("GT_ESWRLC");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("FormID")
                    .HasMaxLength(10);

                entity.Property(e => e.LocationDesc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEswrms>(entity =>
            {
                entity.HasKey(e => e.WardId);

                entity.ToTable("GT_ESWRMS");

                entity.Property(e => e.WardId)
                    .HasColumnName("WardID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("FormID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.WardDesc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WardShortDesc)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<GtEswrpa>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.WardId, e.RoomId, e.ParameterId });

                entity.ToTable("GT_ESWRPA");

                entity.Property(e => e.WardId).HasColumnName("WardID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("FormID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc).HasMaxLength(15);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEswrrm>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("GT_ESWRRM");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("FormID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.RoomDesc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoomShortDesc)
                    .IsRequired()
                    .HasMaxLength(10);
            });
        }
    }
}
