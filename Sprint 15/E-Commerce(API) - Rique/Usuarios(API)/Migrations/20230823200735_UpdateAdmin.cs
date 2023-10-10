﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Usuarios_API_.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4d53081b-2b85-4291-9801-f61a82c6b2ce"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93904639-bdfc-466e-884b-3b5656986b28"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a1429fb2-ce65-4624-8920-c0077b856793"), null, "lojista", "LOJISTA" },
                    { new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948"), null, "admin", "ADMIN" },
                    { new Guid("e1c427ed-427b-42d6-9acf-8911a5e2dc39"), null, "cliente", "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "CPF", "Cidade", "Complemento", "ConcurrencyStamp", "DataEHoraCriacao", "DataEHoraModificacao", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logradouro", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TipoDeUsuario", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948"), 0, "Jardim Odete", "08598372", "38479412917", "Itaquaquecetuba", "Casa", "4d07a728-75df-482d-ae9a-c16ad9265301", new DateTime(2023, 8, 23, 17, 7, 34, 731, DateTimeKind.Local).AddTicks(231), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", false, false, null, "Rua Cambuci", "ADMIN@ADMIN.COM", "ADMIN", 49u, "AQAAAAIAAYagAAAAEEyE93tEjnE7AOyT8Ly4B1F/tgvqEBqXqRYt6TMKi2wEeoVf0IoY/p7JGJ9GeUKL+w==", null, false, "801a753e-5ca1-4866-a3d0-c3b778d0fb0c", true, "Admin", false, "SP", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948"), new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1429fb2-ce65-4624-8920-c0077b856793"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1c427ed-427b-42d6-9acf-8911a5e2dc39"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948"), new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("db82dc95-bb75-47ba-84a6-8547ddb7c948"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4d53081b-2b85-4291-9801-f61a82c6b2ce"), null, "cliente", "CLIENTE" },
                    { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), null, "admin", "ADMIN" },
                    { new Guid("93904639-bdfc-466e-884b-3b5656986b28"), null, "lojista", "LOJISTA" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "CPF", "Cidade", "Complemento", "ConcurrencyStamp", "DataEHoraCriacao", "DataEHoraModificacao", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logradouro", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TipoDeUsuario", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), 0, "Jardim Odete", "08598372", "11111111111", "Itaquaquecetuba", "Casa", "73e17c74-3174-4d32-9753-51d6c3637e6f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", false, false, null, "Rua Cambuci", "ADMIN@ADMIN.COM", "ADMIN", 49u, "AQAAAAIAAYagAAAAEE2ytNn/prx/XWQeVxNUAtBH452MCIME/CRaoS4BBu+OzRmE2Q2tVXeR5DQ/qOKxiQ==", null, false, "12f01b6a-8c96-4632-a6c8-56648931f67a", true, "Admin", false, "SP", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03") });
        }
    }
}
