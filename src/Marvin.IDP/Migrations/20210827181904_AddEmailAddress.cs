using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marvin.IDP.Migrations
{
    public partial class AddEmailAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("03ccb4c1-26fc-473a-9a55-7128bf6ecada"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("0ada0b1c-f796-4fa8-a39c-17f658070c30"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("18298cf0-21c1-4f9a-ba4b-7a80b93b1661"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("4a75b34a-7e38-4a4d-819a-2dabee4a0c3b"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("5035abb3-5090-423d-8bf0-3ddd9f36e660"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("71ba9b5e-ad26-4bff-b433-5eed4b997627"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("8072bd93-6bd8-49ea-9046-349b9da26c77"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("db9dcf7c-e6d9-49b1-b530-adbdbb957de3"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("f5fd89fd-6a68-4862-aa1b-ae784cc37df3"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("f769b99c-df28-4830-9d0c-80298b86a99f"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "Users",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityCodeExpirationDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCodeExpirationDate",
                table: "Users");

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("18298cf0-21c1-4f9a-ba4b-7a80b93b1661"), "881373f3-d27b-4b7f-9d16-8f2951783755", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Frank" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("db9dcf7c-e6d9-49b1-b530-adbdbb957de3"), "6327c5c0-9a7b-4f1a-bfe1-fa953b98ea7d", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Underwood" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("f5fd89fd-6a68-4862-aa1b-ae784cc37df3"), "3409b86d-6933-4b32-bc48-c85158e0b3bb", "email", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "frank@someprovider.com" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("f769b99c-df28-4830-9d0c-80298b86a99f"), "5e242557-602b-4410-8c13-8d90e91bb5ac", "address", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Main Road 1" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("5035abb3-5090-423d-8bf0-3ddd9f36e660"), "9f5c311d-7d71-483c-8189-e5a3ea3146cf", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("0ada0b1c-f796-4fa8-a39c-17f658070c30"), "3ade1a3a-0cae-4717-b21b-c04b53c48fb4", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Claire" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("8072bd93-6bd8-49ea-9046-349b9da26c77"), "b47251b6-1f3e-46a2-9c7b-79853780d9b8", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Underwood" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("4a75b34a-7e38-4a4d-819a-2dabee4a0c3b"), "f8f4ef51-fca1-4c1a-8e49-0b75e5d14640", "email", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "claire@someprovider.com" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("71ba9b5e-ad26-4bff-b433-5eed4b997627"), "e6dc58eb-c43a-4288-8986-5c048628e787", "address", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Big Street 2" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("03ccb4c1-26fc-473a-9a55-7128bf6ecada"), "c61dc032-2b7f-4512-81f5-753a07dc10d0", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "f77d57fc-61d8-4d56-aea2-fba55567ebbb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "aebf9e5d-e4a0-4cd9-a0a9-a1801d21600a");
        }
    }
}
