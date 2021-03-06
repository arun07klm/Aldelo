USE [Aldelo]
GO
ALTER TABLE [dbo].[CompanyMenuList] DROP CONSTRAINT [FK_CompanyMenuList_Menu]
GO
ALTER TABLE [dbo].[CompanyMenuList] DROP CONSTRAINT [FK_CompanyMenuList_Company]
GO
ALTER TABLE [dbo].[Company] DROP CONSTRAINT [FK_Company_Address]
GO
ALTER TABLE [dbo].[Company] DROP CONSTRAINT [FK_Company_Account]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 30/12/2018 16:55:42 ******/
DROP TABLE [dbo].[Menu]
GO
/****** Object:  Table [dbo].[CompanyMenuList]    Script Date: 30/12/2018 16:55:42 ******/
DROP TABLE [dbo].[CompanyMenuList]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 30/12/2018 16:55:42 ******/
DROP TABLE [dbo].[Company]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 30/12/2018 16:55:42 ******/
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 30/12/2018 16:55:42 ******/
DROP TABLE [dbo].[Account]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 30/12/2018 16:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[passwordHash] [nvarchar](max) NULL,
	[passwordSalt] [uniqueidentifier] NULL,
	[AuthCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Address]    Script Date: 30/12/2018 16:55:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[Location] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 30/12/2018 16:55:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AddressId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordExpireOn] [date] NULL,
	[DBFolderPath] [nvarchar](200) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CompanyMenuList]    Script Date: 30/12/2018 16:55:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyMenuList](
	[CompanyMenuListId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_CompanyMenuList] PRIMARY KEY CLUSTERED 
(
	[CompanyMenuListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 30/12/2018 16:55:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Path] [nvarchar](50) NOT NULL,
	[Logo] [nvarchar](50) NULL,
	[Style] [nvarchar](50) NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountId], [passwordHash], [passwordSalt], [AuthCode]) VALUES (1, N'UUgeP5yfRP6JCqXvRKNBFHhcYYkDtHbcgnAYaVEo8dk7Bwa7DoSkUWwIBYH2sl8At9hHkDa3h41VZNa2xpR0tZ6y4i3v4HPsxM8wMJKT36yz+uVNUyDwU7qeECJ3JMy/JS5Tp+PUV8wfYZIfj4DuvZTDq/V9Hv1YoIcBTcMHuQU=', N'90b600bc-375e-45af-9a78-38360bb4a52d', NULL)
INSERT [dbo].[Account] ([AccountId], [passwordHash], [passwordSalt], [AuthCode]) VALUES (7, N'H319hWXC7ia6Wtgkq8MrMnkeIrD/TQVDJlk5huQSPA5ZmYX0ofp6QoVSzeRgKIeRa+oxTI+ySUdd7M1YtcOtr4bFI+Z9Snlh15s791+PXeSMUQlGRKTOiMIj+9Kgfqp/FVybBAjrpxfGmmEIkoySqNBmtUFltEf1AgGHEu1qGkA=', N'fa340e3e-ecc0-497e-be07-ac36a5109343', NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([AddressId], [AddressLine1], [Location], [City], [State], [Country]) VALUES (1, N'ABCD', NULL, NULL, NULL, NULL)
INSERT [dbo].[Address] ([AddressId], [AddressLine1], [Location], [City], [State], [Country]) VALUES (7, N'Dubai, UAE', N'kollam', NULL, N'kollam', NULL)
SET IDENTITY_INSERT [dbo].[Address] OFF
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyId], [Name], [AddressId], [AccountId], [Username], [PasswordExpireOn], [DBFolderPath], [CreatedOn], [Status]) VALUES (2, N'First Company', 7, 7, N'FirstCompany', CAST(0xAB410B00 AS Date), N'FirstCompany', CAST(0x0000A9C60116061B AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Company] OFF
SET IDENTITY_INSERT [dbo].[CompanyMenuList] ON 

INSERT [dbo].[CompanyMenuList] ([CompanyMenuListId], [CompanyId], [MenuId], [Status]) VALUES (1, 2, 13, 0)
SET IDENTITY_INSERT [dbo].[CompanyMenuList] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (1, N'Customer Wise', N'home', NULL, N'nav-icon fa fa-circle-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (4, N'Report', N'cust', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (5, N'Sales By Category', N'saleCategories', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (7, N'Sales By MenuItem', N'saleMenuItem', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (8, N'Fastest Moving Item', N'fastestMoving', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (9, N'Driver Delivery Details', N'driverDelivery', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (10, N'Total Delivery Charge', N'totalDeiverCharge', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (11, N'Total Refund Details', N'totalRefunds', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (12, N'Total Payout Details', N'totalpayouts', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
INSERT [dbo].[Menu] ([MenuId], [Name], [Path], [Logo], [Style], [Status]) VALUES (13, N'Complementary Order', N'complementaryorder', NULL, N'nav-icon fa fa-address-card-o text-info', 0)
SET IDENTITY_INSERT [dbo].[Menu] OFF
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Account]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Address]
GO
ALTER TABLE [dbo].[CompanyMenuList]  WITH CHECK ADD  CONSTRAINT [FK_CompanyMenuList_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[CompanyMenuList] CHECK CONSTRAINT [FK_CompanyMenuList_Company]
GO
ALTER TABLE [dbo].[CompanyMenuList]  WITH CHECK ADD  CONSTRAINT [FK_CompanyMenuList_Menu] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[CompanyMenuList] CHECK CONSTRAINT [FK_CompanyMenuList_Menu]
GO
