using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BerberRandevu.Api.Migrations
{
    /// <inheritdoc />
    public partial class IlkVeritabani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Berberler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Uzmanlik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FotoUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berberler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hizmetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SureDakika = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SifreHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalismaSaatleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BerberId = table.Column<int>(type: "int", nullable: false),
                    Gun = table.Column<int>(type: "int", nullable: false),
                    BaslangicSaati = table.Column<TimeOnly>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalismaSaatleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalismaSaatleri_Berberler_BerberId",
                        column: x => x.BerberId,
                        principalTable: "Berberler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    BerberId = table.Column<int>(type: "int", nullable: false),
                    HizmetId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    BaslangicSaati = table.Column<TimeOnly>(type: "time", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Not = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevular_Berberler_BerberId",
                        column: x => x.BerberId,
                        principalTable: "Berberler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Hizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmetler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Yorumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    BerberId = table.Column<int>(type: "int", nullable: false),
                    Puan = table.Column<int>(type: "int", nullable: false),
                    Metin = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorumlar", x => x.Id);
                    table.CheckConstraint("CK_Yorum_Puan", "[Puan] BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_Yorumlar_Berberler_BerberId",
                        column: x => x.BerberId,
                        principalTable: "Berberler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Berberler",
                columns: new[] { "Id", "AdSoyad", "AktifMi", "FotoUrl", "Telefon", "Uzmanlik" },
                values: new object[,]
                {
                    { 1, "Ahmet Usta", true, null, "0555 111 22 33", "Klasik kesim" },
                    { 2, "Mehmet Usta", true, null, "0555 444 55 66", "Sakal ve ustura" },
                    { 3, "Can Usta", true, null, "0555 777 88 99", "Modern kesim" }
                });

            migrationBuilder.InsertData(
                table: "Hizmetler",
                columns: new[] { "Id", "Aciklama", "Ad", "Fiyat", "SureDakika" },
                values: new object[,]
                {
                    { 1, "Yıkama dahil klasik kesim", "Saç Kesimi", 300m, 30 },
                    { 2, "Ustura ile sakal tıraşı", "Sakal Tıraşı", 150m, 15 },
                    { 3, "Saç kesimi ve sakal tıraşı birlikte", "Saç + Sakal", 400m, 45 },
                    { 4, "12 yaş altı", "Çocuk Saç Kesimi", 200m, 20 }
                });

            migrationBuilder.InsertData(
                table: "CalismaSaatleri",
                columns: new[] { "Id", "BaslangicSaati", "BerberId", "BitisSaati", "Gun" },
                values: new object[,]
                {
                    { 1, new TimeOnly(9, 0, 0), 1, new TimeOnly(19, 0, 0), 1 },
                    { 2, new TimeOnly(9, 0, 0), 1, new TimeOnly(19, 0, 0), 2 },
                    { 3, new TimeOnly(9, 0, 0), 1, new TimeOnly(19, 0, 0), 3 },
                    { 4, new TimeOnly(9, 0, 0), 1, new TimeOnly(19, 0, 0), 4 },
                    { 5, new TimeOnly(9, 0, 0), 1, new TimeOnly(19, 0, 0), 5 },
                    { 6, new TimeOnly(9, 0, 0), 1, new TimeOnly(19, 0, 0), 6 },
                    { 7, new TimeOnly(9, 0, 0), 2, new TimeOnly(19, 0, 0), 1 },
                    { 8, new TimeOnly(9, 0, 0), 2, new TimeOnly(19, 0, 0), 2 },
                    { 9, new TimeOnly(9, 0, 0), 2, new TimeOnly(19, 0, 0), 3 },
                    { 10, new TimeOnly(9, 0, 0), 2, new TimeOnly(19, 0, 0), 4 },
                    { 11, new TimeOnly(9, 0, 0), 2, new TimeOnly(19, 0, 0), 5 },
                    { 12, new TimeOnly(9, 0, 0), 2, new TimeOnly(19, 0, 0), 6 },
                    { 13, new TimeOnly(9, 0, 0), 3, new TimeOnly(19, 0, 0), 1 },
                    { 14, new TimeOnly(9, 0, 0), 3, new TimeOnly(19, 0, 0), 2 },
                    { 15, new TimeOnly(9, 0, 0), 3, new TimeOnly(19, 0, 0), 3 },
                    { 16, new TimeOnly(9, 0, 0), 3, new TimeOnly(19, 0, 0), 4 },
                    { 17, new TimeOnly(9, 0, 0), 3, new TimeOnly(19, 0, 0), 5 },
                    { 18, new TimeOnly(9, 0, 0), 3, new TimeOnly(19, 0, 0), 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalismaSaatleri_BerberId",
                table: "CalismaSaatleri",
                column: "BerberId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_Email",
                table: "Kullanicilar",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_BerberId_Tarih_BaslangicSaati",
                table: "Randevular",
                columns: new[] { "BerberId", "Tarih", "BaslangicSaati" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HizmetId",
                table: "Randevular",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_BerberId",
                table: "Yorumlar",
                column: "BerberId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KullaniciId",
                table: "Yorumlar",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalismaSaatleri");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Yorumlar");

            migrationBuilder.DropTable(
                name: "Hizmetler");

            migrationBuilder.DropTable(
                name: "Berberler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
