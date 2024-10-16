USE [master]
GO
/****** Object:  Database [WineManagementSystem]    Script Date: 16/10/2024 12:57:07 ******/
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
/****** Object:  Table [dbo].[Account]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[Supplier]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[Wine]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[WineBatch]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[WineCheck]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[WineRequest]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[WineStorageLocation]    Script Date: 16/10/2024 12:57:08 ******/
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
/****** Object:  Table [dbo].[WineTransaction]    Script Date: 16/10/2024 12:57:08 ******/
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
