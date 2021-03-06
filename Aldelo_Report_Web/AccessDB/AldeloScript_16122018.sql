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
/****** Object:  Table [dbo].[Menu]    Script Date: 16/12/2018 13:53:07 ******/
DROP TABLE [dbo].[Menu]
GO
/****** Object:  Table [dbo].[CompanyMenuList]    Script Date: 16/12/2018 13:53:07 ******/
DROP TABLE [dbo].[CompanyMenuList]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 16/12/2018 13:53:07 ******/
DROP TABLE [dbo].[Company]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 16/12/2018 13:53:07 ******/
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 16/12/2018 13:53:07 ******/
DROP TABLE [dbo].[Account]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 16/12/2018 13:53:07 ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 16/12/2018 13:53:07 ******/
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
/****** Object:  Table [dbo].[Company]    Script Date: 16/12/2018 13:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AddressId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[CompanyMenuList]    Script Date: 16/12/2018 13:53:07 ******/
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
/****** Object:  Table [dbo].[Menu]    Script Date: 16/12/2018 13:53:07 ******/
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
