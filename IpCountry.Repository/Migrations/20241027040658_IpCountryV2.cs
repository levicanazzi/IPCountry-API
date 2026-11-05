using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IpCountry.Repository.Migrations
{
    /// <inheritdoc />
    public partial class IpCountryV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    SET IDENTITY_INSERT [dbo].[Countries] ON

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (1,
                                    N'Greece', N'GR', N'GRC', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (2,
                                    N'Germany', N'DE', N'DEU', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (3,
                                    N'Cyprus', N'CY', N'CYP', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (4,
                                    N'United States', N'US', N'USA', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (6,
                                    N'Spain', N'ES', N'ESP', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (7,
                                    N'France', N'FR', N'FRA', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (8,
                                    N'Italy', N'IT', N'IA ', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (9,
                                    N'Japan', N'JP', N'JPN', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    INSERT [dbo].[Countries] ([Id], [Name], [TwoLetterCode], [ThreeLetterCode], [CreatedAt]) VALUES (10,
                                    N'China', N'CN', N'CHN', CAST(N'2022-10-12T06:46:10.5000000' AS DateTime2))

                                    SET IDENTITY_INSERT [dbo].[Countries] OFF

                                    SET IDENTITY_INSERT [dbo].[IPAddresses] ON

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (6, 1,
                                    N'44.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (7, 2,
                                    N'45.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (8, 3,
                                    N'46.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (9, 4,
                                    N'47.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (10, 6,
                                    N'49.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (11, 7,
                                    N'41.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (12, 8,
                                    N'42.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (13, 9,
                                    N'43.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (14, 10,
                                    N'50.255.255.254', CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:06.8566667' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (15, 1,
                                    N'44.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (16, 2,
                                    N'45.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (17, 3,
                                    N'46.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (18, 4,
                                    N'47.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (19, 6,
                                    N'49.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (20, 7,
                                    N'41.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (21, 8,
                                    N'42.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (22, 9,
                                    N'43.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (23, 10,
                                    N'50.25.55.254', CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:33.3800000' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (24, 1,
                                    N'44.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (25, 2,
                                    N'45.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (26, 3,
                                    N'46.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (27, 4,
                                    N'47.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (28, 6,
                                    N'49.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (29, 7,
                                    N'41.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (30, 8,
                                    N'42.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (31, 9,
                                    N'43.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (32, 10,
                                    N'50.25.55.4', CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2),
                                    CAST(N'2022-10-12T07:04:51.3233333' AS DateTime2))

                                    INSERT [dbo].[IPAddresses] ([Id], [CountryId], [IP], [CreatedAt], [UpdatedAt]) VALUES (33, 1,
                                    N'10.20.30.40', CAST(N'2022-10-12T08:41:37.3100000' AS DateTime2),
                                    CAST(N'2022-10-12T08:41:37.3100000' AS DateTime2))

                                    SET IDENTITY_INSERT [dbo].[IPAddresses] OFF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE IPAddresses;

                                    DELETE FROM Countries;");
        }
    }
}
