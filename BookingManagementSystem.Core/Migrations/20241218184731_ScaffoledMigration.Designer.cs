﻿// <auto-generated />
using System;
using BookingManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingManagementSystem.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241218184731_ScaffoledMigration")]
    partial class ScaffoledMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(5);

                    b.HasKey("Id");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.BadReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminNotes")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("EntityId")
                        .HasColumnType("integer");

                    b.Property<int>("EntityType")
                        .HasColumnType("integer");

                    b.Property<int?>("HandledByAdminId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("HandledDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReportReason")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BadReports");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PropertyId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.FAQ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<int>("FAQCategory")
                        .HasColumnType("integer")
                        .HasColumnName("FAQCategory");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FAQs", (string)null);
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateSent")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("SenderName")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("BookingId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CityOrDistrict")
                        .HasColumnType("text");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.PrimitiveCollection<int[]>("DestinationTypes")
                        .HasColumnType("integer[]");

                    b.Property<int>("HostId")
                        .HasColumnType("integer");

                    b.PrimitiveCollection<string[]>("ImagePaths")
                        .HasColumnType("text[]");

                    b.Property<bool>("IsAvailable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsFavourite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsPetFriendly")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsPriority")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsRequested")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("LastEditedStep")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("'-1'::integer");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int>("MaxGuests")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .HasColumnType("text");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("numeric");

                    b.Property<string>("StateOrProvince")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("StreetAddress")
                        .HasColumnType("text");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.HasIndex(new[] { "CountryId" }, "IX_Properties_CountryId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.PropertyAmenity", b =>
                {
                    b.Property<int>("PropertyId")
                        .HasColumnType("integer");

                    b.Property<int>("AmenityId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.HasKey("PropertyId", "AmenityId")
                        .HasName("PropertyAmenities_pkey");

                    b.HasIndex("AmenityId");

                    b.ToTable("PropertyAmenities");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.PropertyPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsMandatory")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PropertyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PropertyId" }, "IX_PropertyPolicies_PropertyId");

                    b.ToTable("PropertyPolicies");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.QnA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int?>("HostId")
                        .HasColumnType("integer");

                    b.Property<int>("PropertyId")
                        .HasColumnType("integer");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("HostId");

                    b.HasIndex("PropertyId");

                    b.ToTable("QnAs");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PropertyId")
                        .HasColumnType("integer");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("GovernmentId")
                        .HasColumnType("text");

                    b.Property<bool>("IsEliteHost")
                        .HasColumnType("boolean");

                    b.PrimitiveCollection<string[]>("Languages")
                        .HasColumnType("text[]");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("ShortBio")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("MaxDiscountValue")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("MinimumOrderValue")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ValidUntil")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Services.CountryInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "geonameId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AreaInSqKm")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "areaInSqKm");

                    b.Property<string>("Capital")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "capital");

                    b.Property<string>("Continent")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "continent");

                    b.Property<string>("ContinentName")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "continentName");

                    b.Property<string>("CountryCode")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "countryCode");

                    b.Property<string>("CountryName")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "countryName");

                    b.Property<string>("CurrencyCode")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "currencyCode");

                    b.Property<double>("East")
                        .HasColumnType("double precision")
                        .HasAnnotation("Relational:JsonPropertyName", "east");

                    b.Property<string>("FipsCode")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "fipsCode");

                    b.Property<string>("IsoAlpha3")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "isoAlpha3");

                    b.Property<string>("IsoNumeric")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "isoNumeric");

                    b.Property<string>("Languages")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "languages");

                    b.Property<double>("North")
                        .HasColumnType("double precision")
                        .HasAnnotation("Relational:JsonPropertyName", "north");

                    b.Property<string>("Population")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "population");

                    b.Property<string>("PostalCodeFormat")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "postalCodeFormat");

                    b.Property<double>("South")
                        .HasColumnType("double precision")
                        .HasAnnotation("Relational:JsonPropertyName", "south");

                    b.Property<double>("West")
                        .HasColumnType("double precision")
                        .HasAnnotation("Relational:JsonPropertyName", "west");

                    b.HasKey("Id");

                    b.ToTable("CountryInfo", (string)null);
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.BadReport", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.User", "User")
                        .WithMany("BadReports")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Booking", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.Property", "Property")
                        .WithMany("Bookings")
                        .HasForeignKey("PropertyId")
                        .IsRequired();

                    b.HasOne("BookingManagementSystem.Core.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Notification", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Payment", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.Booking", "Booking")
                        .WithMany("Payments")
                        .HasForeignKey("BookingId")
                        .IsRequired();

                    b.HasOne("BookingManagementSystem.Core.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Property", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Services.CountryInfo", "Country")
                        .WithMany("Properties")
                        .HasForeignKey("CountryId");

                    b.HasOne("BookingManagementSystem.Core.Models.User", "Host")
                        .WithMany("Properties")
                        .HasForeignKey("HostId")
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Host");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.PropertyAmenity", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.Amenity", "Amenity")
                        .WithMany("PropertyAmenities")
                        .HasForeignKey("AmenityId")
                        .IsRequired();

                    b.HasOne("BookingManagementSystem.Core.Models.Property", "Property")
                        .WithMany("PropertyAmenities")
                        .HasForeignKey("PropertyId")
                        .IsRequired();

                    b.Navigation("Amenity");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.PropertyPolicy", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.Property", "Property")
                        .WithMany("PropertyPolicies")
                        .HasForeignKey("PropertyId");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.QnA", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.User", "Customer")
                        .WithMany("QnACustomers")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.HasOne("BookingManagementSystem.Core.Models.User", "Host")
                        .WithMany("QnAHosts")
                        .HasForeignKey("HostId");

                    b.HasOne("BookingManagementSystem.Core.Models.Property", "Property")
                        .WithMany("QnAs")
                        .HasForeignKey("PropertyId")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Host");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Review", b =>
                {
                    b.HasOne("BookingManagementSystem.Core.Models.Property", "Property")
                        .WithMany("Reviews")
                        .HasForeignKey("PropertyId")
                        .IsRequired();

                    b.HasOne("BookingManagementSystem.Core.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Amenity", b =>
                {
                    b.Navigation("PropertyAmenities");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Booking", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.Property", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("PropertyAmenities");

                    b.Navigation("PropertyPolicies");

                    b.Navigation("QnAs");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Models.User", b =>
                {
                    b.Navigation("BadReports");

                    b.Navigation("Bookings");

                    b.Navigation("Notifications");

                    b.Navigation("Payments");

                    b.Navigation("Properties");

                    b.Navigation("QnACustomers");

                    b.Navigation("QnAHosts");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BookingManagementSystem.Core.Services.CountryInfo", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
