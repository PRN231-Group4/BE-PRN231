USE [master]
GO
/****** Object:  Database [WineManagementSystem]    Script Date: 16/10/2024 14:44:46 ******/
CREATE DATABASE [WineManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WineManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WineManagementSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WineManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WineManagementSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WineManagementSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WineManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WineManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WineManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WineManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WineManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WineManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [WineManagementSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [WineManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WineManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WineManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WineManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WineManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WineManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WineManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WineManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WineManagementSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WineManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WineManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WineManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WineManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WineManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WineManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WineManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WineManagementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WineManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [WineManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WineManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WineManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WineManagementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WineManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WineManagementSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WineManagementSystem] SET QUERY_STORE = OFF
GO
USE [WineManagementSystem]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Account_Id] [int] IDENTITY(1,1) NOT NULL,
	[Role_Id] [int] NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Account_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Category_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Role_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Role_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[Supplier_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Supplier_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wine]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wine](
	[Wine_Id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Id] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Origin] [nvarchar](100) NULL,
	[Volume] [decimal](10, 2) NULL,
	[AlcContent] [decimal](5, 2) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Wine_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineBatch]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WineBatch](
	[Batch_Id] [int] IDENTITY(1,1) NOT NULL,
	[Wine_Id] [int] NULL,
	[Request_Id] [int] NULL,
	[BatchNumber] [nvarchar](50) NULL,
	[ImportDate] [datetime] NULL,
	[Quantity] [int] NULL,
	[ProductionYear] [int] NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK__WineBatc__28E47C736E8C3D1C] PRIMARY KEY CLUSTERED 
(
	[Batch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineCheck]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WineCheck](
	[Check_Id] [int] IDENTITY(1,1) NOT NULL,
	[Request_Id] [int] NULL,
	[Batch_Id] [int] NULL,
	[Inspector_Id] [int] NULL,
	[Wine_Id] [int] NULL,
	[Quantity] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[CheckDate] [datetime] NULL,
	[Description] [nvarchar](255) NULL,
	[ImageURL] [nvarchar](255) NULL,
 CONSTRAINT [PK__WineChec__7063BF732DEFF8B3] PRIMARY KEY CLUSTERED 
(
	[Check_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineRequest]    Script Date: 16/10/2024 14:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WineRequest](
	[Request_Id] [int] IDENTITY(1,1) NOT NULL,
	[Supplier_Id] [int] NULL,
	[Manager_Id] [int] NULL,
	[Wine_Id] [int] NULL,
	[Quantity] [int] NULL,
	[RequestDate] [datetime] NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK__WineRequ__E9C5B3738B07BDD0] PRIMARY KEY CLUSTERED 
(
	[Request_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineStorageLocation]    Script Date: 16/10/2024 14:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WineStorageLocation](
	[Location_Id] [int] IDENTITY(1,1) NOT NULL,
	[FloorNumber] [int] NULL,
	[Zone] [nvarchar](50) NULL,
	[ShelfCode] [nvarchar](50) NULL,
	[Capacity] [int] NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Location_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineTransaction]    Script Date: 16/10/2024 14:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WineTransaction](
	[Transaction_Id] [int] IDENTITY(1,1) NOT NULL,
	[Batch_Id] [int] NULL,
	[Wine_Id] [int] NULL,
	[Inspector_Id] [int] NULL,
	[Location_Id] [int] NULL,
	[Quantity] [int] NULL,
	[TransType] [nvarchar](50) NULL,
	[TransDate] [datetime] NULL,
	[Description] [nvarchar](255) NULL,
	[ImageURL] [nvarchar](255) NULL,
 CONSTRAINT [PK__WineTran__9A8D56051EECF152] PRIMARY KEY CLUSTERED 
(
	[Transaction_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Account_Id], [Role_Id], [Username], [Password], [Status]) VALUES (1, 1, N'admin', N'admin123', N'Active')
INSERT [dbo].[Account] ([Account_Id], [Role_Id], [Username], [Password], [Status]) VALUES (2, 2, N'manager', N'manager123', N'Active')
INSERT [dbo].[Account] ([Account_Id], [Role_Id], [Username], [Password], [Status]) VALUES (3, 3, N'inspector', N'inspector123', N'Active')
INSERT [dbo].[Account] ([Account_Id], [Role_Id], [Username], [Password], [Status]) VALUES (1002, 1, N'admin', N'admin123', N'Active')
INSERT [dbo].[Account] ([Account_Id], [Role_Id], [Username], [Password], [Status]) VALUES (1003, 2, N'manager', N'manager123', N'Active')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Category_Id], [Name], [Description]) VALUES (1, N'Red Wine', N'A type of wine made from dark-colored grape varieties.')
INSERT [dbo].[Category] ([Category_Id], [Name], [Description]) VALUES (2, N'White Wine', N'A type of wine made from green or yellowish grapes.')
INSERT [dbo].[Category] ([Category_Id], [Name], [Description]) VALUES (3, N'Sparkling Wine', N'A wine that has significant levels of carbon dioxide.')
INSERT [dbo].[Category] ([Category_Id], [Name], [Description]) VALUES (1002, N'Red Wine', N'A type of wine made from dark-colored grape varieties.')
INSERT [dbo].[Category] ([Category_Id], [Name], [Description]) VALUES (1003, N'White Wine', N'A type of wine made from green or yellowish grapes.')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Role_Id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([Role_Id], [Name]) VALUES (2, N'Manager')
INSERT [dbo].[Role] ([Role_Id], [Name]) VALUES (3, N'Inspector')
INSERT [dbo].[Role] ([Role_Id], [Name]) VALUES (1002, N'Admin')
INSERT [dbo].[Role] ([Role_Id], [Name]) VALUES (1003, N'Manager')
INSERT [dbo].[Role] ([Role_Id], [Name]) VALUES (1004, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([Supplier_Id], [Name]) VALUES (1, N'Supplier A')
INSERT [dbo].[Supplier] ([Supplier_Id], [Name]) VALUES (2, N'Supplier B')
INSERT [dbo].[Supplier] ([Supplier_Id], [Name]) VALUES (3, N'Supplier C')
INSERT [dbo].[Supplier] ([Supplier_Id], [Name]) VALUES (1002, N'Supplier A')
INSERT [dbo].[Supplier] ([Supplier_Id], [Name]) VALUES (1003, N'Supplier B')
SET IDENTITY_INSERT [dbo].[Supplier] OFF
GO
SET IDENTITY_INSERT [dbo].[Wine] ON 

INSERT [dbo].[Wine] ([Wine_Id], [Category_Id], [Name], [Origin], [Volume], [AlcContent], [Description], [Status]) VALUES (1, 1, N'Cabernet Sauvignon', N'France', CAST(750.00 AS Decimal(10, 2)), CAST(13.50 AS Decimal(5, 2)), N'A full-bodied red wine with dark fruit flavors.', N'Available')
INSERT [dbo].[Wine] ([Wine_Id], [Category_Id], [Name], [Origin], [Volume], [AlcContent], [Description], [Status]) VALUES (2, 2, N'Chardonnay', N'USA', CAST(750.00 AS Decimal(10, 2)), CAST(14.00 AS Decimal(5, 2)), N'A popular white wine known for its rich and creamy flavor.', N'Available')
INSERT [dbo].[Wine] ([Wine_Id], [Category_Id], [Name], [Origin], [Volume], [AlcContent], [Description], [Status]) VALUES (3, 3, N'Prosecco', N'Italy', CAST(750.00 AS Decimal(10, 2)), CAST(11.00 AS Decimal(5, 2)), N'A sparkling wine with fruity and floral notes.', N'Available')
INSERT [dbo].[Wine] ([Wine_Id], [Category_Id], [Name], [Origin], [Volume], [AlcContent], [Description], [Status]) VALUES (1002, 1, N'Chateau Lafite', N'France', CAST(750.00 AS Decimal(10, 2)), CAST(13.50 AS Decimal(5, 2)), N'A renowned Bordeaux wine.', N'Available')
INSERT [dbo].[Wine] ([Wine_Id], [Category_Id], [Name], [Origin], [Volume], [AlcContent], [Description], [Status]) VALUES (1003, 2, N'Sauvignon Blanc', N'New Zealand', CAST(750.00 AS Decimal(10, 2)), CAST(12.50 AS Decimal(5, 2)), N'A crisp white wine.', N'Available')
SET IDENTITY_INSERT [dbo].[Wine] OFF
GO
SET IDENTITY_INSERT [dbo].[WineBatch] ON 

INSERT [dbo].[WineBatch] ([Batch_Id], [Wine_Id], [Request_Id], [BatchNumber], [ImportDate], [Quantity], [ProductionYear], [Status]) VALUES (1, 1, 1, N'BATCH001', CAST(N'2024-10-16T00:00:00.000' AS DateTime), 100, 2022, N'In Stock')
INSERT [dbo].[WineBatch] ([Batch_Id], [Wine_Id], [Request_Id], [BatchNumber], [ImportDate], [Quantity], [ProductionYear], [Status]) VALUES (2, 2, 2, N'BATCH002', CAST(N'2024-10-16T00:00:00.000' AS DateTime), 150, 2023, N'In Stock')
INSERT [dbo].[WineBatch] ([Batch_Id], [Wine_Id], [Request_Id], [BatchNumber], [ImportDate], [Quantity], [ProductionYear], [Status]) VALUES (3, 1, 1, N'WB001', CAST(N'2024-10-16T13:05:38.143' AS DateTime), 100, 2023, N'In Stock')
SET IDENTITY_INSERT [dbo].[WineBatch] OFF
GO
SET IDENTITY_INSERT [dbo].[WineCheck] ON 

INSERT [dbo].[WineCheck] ([Check_Id], [Request_Id], [Batch_Id], [Inspector_Id], [Wine_Id], [Quantity], [Status], [CheckDate], [Description], [ImageURL]) VALUES (1, 1, 1, 3, 1, 100, N'Passed', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Quality check passed.', NULL)
INSERT [dbo].[WineCheck] ([Check_Id], [Request_Id], [Batch_Id], [Inspector_Id], [Wine_Id], [Quantity], [Status], [CheckDate], [Description], [ImageURL]) VALUES (2, 2, 2, 3, 2, 150, N'Pending', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Awaiting inspection.', NULL)
INSERT [dbo].[WineCheck] ([Check_Id], [Request_Id], [Batch_Id], [Inspector_Id], [Wine_Id], [Quantity], [Status], [CheckDate], [Description], [ImageURL]) VALUES (3, 1, 1, 1, 1, 100, NULL, CAST(N'2024-10-16T13:05:38.147' AS DateTime), N'Quality check passed', N'http://example.com/image.jpg')
SET IDENTITY_INSERT [dbo].[WineCheck] OFF
GO
SET IDENTITY_INSERT [dbo].[WineRequest] ON 

INSERT [dbo].[WineRequest] ([Request_Id], [Supplier_Id], [Manager_Id], [Wine_Id], [Quantity], [RequestDate], [Description], [Status]) VALUES (1, 1, 2, 1, 100, CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Request for Cabernet Sauvignon', N'Pending')
INSERT [dbo].[WineRequest] ([Request_Id], [Supplier_Id], [Manager_Id], [Wine_Id], [Quantity], [RequestDate], [Description], [Status]) VALUES (2, 2, 2, 2, 150, CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Request for Chardonnay', N'Pending')
INSERT [dbo].[WineRequest] ([Request_Id], [Supplier_Id], [Manager_Id], [Wine_Id], [Quantity], [RequestDate], [Description], [Status]) VALUES (3, 1, 2, 1, 100, CAST(N'2024-10-16T13:05:38.140' AS DateTime), N'Request for red wine', N'Pending')
SET IDENTITY_INSERT [dbo].[WineRequest] OFF
GO
SET IDENTITY_INSERT [dbo].[WineStorageLocation] ON 

INSERT [dbo].[WineStorageLocation] ([Location_Id], [FloorNumber], [Zone], [ShelfCode], [Capacity], [Description], [Status]) VALUES (1, 1, N'Zone A', N'A1', 500, N'Storage for red wine.', N'Active')
INSERT [dbo].[WineStorageLocation] ([Location_Id], [FloorNumber], [Zone], [ShelfCode], [Capacity], [Description], [Status]) VALUES (2, 1, N'Zone B', N'B1', 300, N'Storage for white wine.', N'Active')
INSERT [dbo].[WineStorageLocation] ([Location_Id], [FloorNumber], [Zone], [ShelfCode], [Capacity], [Description], [Status]) VALUES (1002, 1, N'A', N'S1', 200, N'Storage for red wine', N'Active')
SET IDENTITY_INSERT [dbo].[WineStorageLocation] OFF
GO
SET IDENTITY_INSERT [dbo].[WineTransaction] ON 

INSERT [dbo].[WineTransaction] ([Transaction_Id], [Batch_Id], [Wine_Id], [Inspector_Id], [Location_Id], [Quantity], [TransType], [TransDate], [Description], [ImageURL]) VALUES (1, 1, 1, 3, 1, 50, N'Import', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Imported 50 bottles of Cabernet Sauvignon.', NULL)
INSERT [dbo].[WineTransaction] ([Transaction_Id], [Batch_Id], [Wine_Id], [Inspector_Id], [Location_Id], [Quantity], [TransType], [TransDate], [Description], [ImageURL]) VALUES (2, 2, 2, 3, 1, 75, N'Import', CAST(N'2024-10-16T00:00:00.000' AS DateTime), N'Imported 75 bottles of Chardonnay.', NULL)
INSERT [dbo].[WineTransaction] ([Transaction_Id], [Batch_Id], [Wine_Id], [Inspector_Id], [Location_Id], [Quantity], [TransType], [TransDate], [Description], [ImageURL]) VALUES (3, 1, 1, 1, 1, 100, N'Inbound', CAST(N'2024-10-16T13:05:38.150' AS DateTime), N'Wine received', N'http://example.com/image.jpg')
SET IDENTITY_INSERT [dbo].[WineTransaction] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Role] ([Role_Id])
GO
ALTER TABLE [dbo].[Wine]  WITH CHECK ADD FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Category] ([Category_Id])
GO
ALTER TABLE [dbo].[WineBatch]  WITH CHECK ADD  CONSTRAINT [FK__WineBatch__Reque__37A5467C] FOREIGN KEY([Request_Id])
REFERENCES [dbo].[WineRequest] ([Request_Id])
GO
ALTER TABLE [dbo].[WineBatch] CHECK CONSTRAINT [FK__WineBatch__Reque__37A5467C]
GO
ALTER TABLE [dbo].[WineBatch]  WITH CHECK ADD  CONSTRAINT [FK__WineBatch__Wine___38996AB5] FOREIGN KEY([Wine_Id])
REFERENCES [dbo].[Wine] ([Wine_Id])
GO
ALTER TABLE [dbo].[WineBatch] CHECK CONSTRAINT [FK__WineBatch__Wine___38996AB5]
GO
ALTER TABLE [dbo].[WineCheck]  WITH CHECK ADD  CONSTRAINT [FK__WineCheck__Batch__398D8EEE] FOREIGN KEY([Batch_Id])
REFERENCES [dbo].[WineBatch] ([Batch_Id])
GO
ALTER TABLE [dbo].[WineCheck] CHECK CONSTRAINT [FK__WineCheck__Batch__398D8EEE]
GO
ALTER TABLE [dbo].[WineCheck]  WITH CHECK ADD  CONSTRAINT [FK__WineCheck__Reque__3A81B327] FOREIGN KEY([Request_Id])
REFERENCES [dbo].[WineRequest] ([Request_Id])
GO
ALTER TABLE [dbo].[WineCheck] CHECK CONSTRAINT [FK__WineCheck__Reque__3A81B327]
GO
ALTER TABLE [dbo].[WineCheck]  WITH CHECK ADD  CONSTRAINT [FK__WineCheck__Wine___3B75D760] FOREIGN KEY([Wine_Id])
REFERENCES [dbo].[Wine] ([Wine_Id])
GO
ALTER TABLE [dbo].[WineCheck] CHECK CONSTRAINT [FK__WineCheck__Wine___3B75D760]
GO
ALTER TABLE [dbo].[WineCheck]  WITH CHECK ADD  CONSTRAINT [FK_WineCheck_Account] FOREIGN KEY([Inspector_Id])
REFERENCES [dbo].[Account] ([Account_Id])
GO
ALTER TABLE [dbo].[WineCheck] CHECK CONSTRAINT [FK_WineCheck_Account]
GO
ALTER TABLE [dbo].[WineRequest]  WITH CHECK ADD  CONSTRAINT [FK_WineRequest_Account] FOREIGN KEY([Manager_Id])
REFERENCES [dbo].[Account] ([Account_Id])
GO
ALTER TABLE [dbo].[WineRequest] CHECK CONSTRAINT [FK_WineRequest_Account]
GO
ALTER TABLE [dbo].[WineRequest]  WITH CHECK ADD  CONSTRAINT [FK_WineRequest_Supplier] FOREIGN KEY([Supplier_Id])
REFERENCES [dbo].[Supplier] ([Supplier_Id])
GO
ALTER TABLE [dbo].[WineRequest] CHECK CONSTRAINT [FK_WineRequest_Supplier]
GO
ALTER TABLE [dbo].[WineTransaction]  WITH CHECK ADD  CONSTRAINT [FK__WineTrans__Batch__3F466844] FOREIGN KEY([Batch_Id])
REFERENCES [dbo].[WineBatch] ([Batch_Id])
GO
ALTER TABLE [dbo].[WineTransaction] CHECK CONSTRAINT [FK__WineTrans__Batch__3F466844]
GO
ALTER TABLE [dbo].[WineTransaction]  WITH CHECK ADD  CONSTRAINT [FK__WineTrans__Locat__403A8C7D] FOREIGN KEY([Location_Id])
REFERENCES [dbo].[WineStorageLocation] ([Location_Id])
GO
ALTER TABLE [dbo].[WineTransaction] CHECK CONSTRAINT [FK__WineTrans__Locat__403A8C7D]
GO
ALTER TABLE [dbo].[WineTransaction]  WITH CHECK ADD  CONSTRAINT [FK__WineTrans__Wine___412EB0B6] FOREIGN KEY([Wine_Id])
REFERENCES [dbo].[Wine] ([Wine_Id])
GO
ALTER TABLE [dbo].[WineTransaction] CHECK CONSTRAINT [FK__WineTrans__Wine___412EB0B6]
GO
ALTER TABLE [dbo].[WineTransaction]  WITH CHECK ADD  CONSTRAINT [FK_WineTransaction_Account] FOREIGN KEY([Inspector_Id])
REFERENCES [dbo].[Account] ([Account_Id])
GO
ALTER TABLE [dbo].[WineTransaction] CHECK CONSTRAINT [FK_WineTransaction_Account]
GO
USE [master]
GO
ALTER DATABASE [WineManagementSystem] SET  READ_WRITE 
GO
