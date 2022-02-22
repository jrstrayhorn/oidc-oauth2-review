using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marvin.IDP.Migrations
{
    public partial class AddUserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("1aaddc1c-df3a-4460-b4bb-35a782cd61cd"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("217587d8-2887-4259-98bc-5e8649ded97c"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("4809ccc0-86c9-4899-ada0-d133cb29a2bd"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("744b609c-e3c5-493e-8d64-143ca6a36aca"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("97f52609-1b53-4f64-8a44-b2e1be23f956"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("9ef68c4a-a577-4de8-adb8-91720f4fe7ef"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b2af0049-1f65-4bda-97f0-c23269bfa1ec"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("c91fe977-4c91-459a-b46c-a925d09bccd5"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("cdbaa399-1601-4a4f-85c3-c2d52117b3d3"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("da1d1f6a-4963-4275-a449-e0abcac5d690"));

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Provider = table.Column<string>(maxLength: 200, nullable: false),
                    ProviderIdentityKey = table.Column<string>(maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("df67e61f-663f-4009-a573-76d358b850ad"), "58b18db2-7323-483f-b100-b82120399f14", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Frank" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("30ade125-046c-4dd8-a95d-0feea0623694"), "48a3240f-3bcc-4a4d-95b5-df04a914e04c", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Underwood" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("a86d9809-1d1e-49bd-9db8-fcc802428faf"), "f4068b5e-6881-4531-b514-83fcdf5303a2", "email", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "frank@someprovider.com" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("fc7df2ff-f697-45b4-ab37-c55ccfb8e849"), "43cf6a2e-55c0-4ee4-89e9-7470d6c93102", "address", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Main Road 1" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("4b6e52e8-8234-463a-b54e-a70f16d9e659"), "8b985fa5-fb2e-48a4-81a2-8f9f7478be71", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("b97e0bb1-b1f2-4d59-8611-db870da70d01"), "64b7f000-8de7-436a-9a5b-516a4da1141b", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Claire" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("77107dd3-9613-43c8-9aab-44c363970f72"), "00328a1c-ecfe-47ce-a0bf-5f0969e07cfb", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Underwood" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("821696ed-d1db-4bfd-a92c-0a7f46dc6891"), "d30fb44f-2b69-40ad-945c-3eedb2e12f95", "email", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "claire@someprovider.com" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("b172a550-f35a-4e19-9687-473920f0edb1"), "33700450-2de9-4538-b5e0-a3343b62a92e", "address", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Big Street 2" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("d4dc3e62-de4f-48c0-8d1a-02f42a6398f8"), "bee9d5a7-b6b4-4f67-b0bc-a65d3294861a", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "32a6f0fb-6aca-4300-a85b-b136f64c3e6c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "98c60bd8-601c-4054-bca0-05e688022330");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("30ade125-046c-4dd8-a95d-0feea0623694"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("4b6e52e8-8234-463a-b54e-a70f16d9e659"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("77107dd3-9613-43c8-9aab-44c363970f72"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("821696ed-d1db-4bfd-a92c-0a7f46dc6891"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("a86d9809-1d1e-49bd-9db8-fcc802428faf"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b172a550-f35a-4e19-9687-473920f0edb1"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b97e0bb1-b1f2-4d59-8611-db870da70d01"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("d4dc3e62-de4f-48c0-8d1a-02f42a6398f8"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("df67e61f-663f-4009-a573-76d358b850ad"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("fc7df2ff-f697-45b4-ab37-c55ccfb8e849"));

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("97f52609-1b53-4f64-8a44-b2e1be23f956"), "e132fb0f-749c-4933-b85b-c0af8f6e8df5", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Frank" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("da1d1f6a-4963-4275-a449-e0abcac5d690"), "ebb59ae8-56c2-4ba0-bcd5-f9835d6402ac", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Underwood" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("217587d8-2887-4259-98bc-5e8649ded97c"), "47cf850b-9953-48e5-b68e-85c52178bd82", "email", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "frank@someprovider.com" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("9ef68c4a-a577-4de8-adb8-91720f4fe7ef"), "723071b1-994c-44f2-83c3-2a08029274b8", "address", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Main Road 1" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("cdbaa399-1601-4a4f-85c3-c2d52117b3d3"), "34984468-6547-43eb-a1e7-b3b0c737f56a", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("c91fe977-4c91-459a-b46c-a925d09bccd5"), "fdfd1d4f-c6c8-4f76-9f21-7dffda011157", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Claire" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("4809ccc0-86c9-4899-ada0-d133cb29a2bd"), "f27eb819-c055-482e-a24e-9e8e8597b505", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Underwood" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("1aaddc1c-df3a-4460-b4bb-35a782cd61cd"), "ef38fcc5-5adf-4df1-bdcc-e4486880c6e1", "email", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "claire@someprovider.com" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("744b609c-e3c5-493e-8d64-143ca6a36aca"), "3687f227-ed52-433c-b10b-93821e2cb8fa", "address", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Big Street 2" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("b2af0049-1f65-4bda-97f0-c23269bfa1ec"), "71453fe9-3efd-43bb-ade6-3fe913405a57", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "5c1e46c5-33a5-4bee-a972-119459628421");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "f118be8c-960d-4cd6-bd6c-87fe97f003ae");
        }
    }
}
