USE [WineManagementSystem]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 04/10/2024 13:37:20 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 04/10/2024 13:37:20 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 04/10/2024 13:37:20 ******/
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
/****** Object:  Table [dbo].[Supplier]    Script Date: 04/10/2024 13:37:20 ******/
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
/****** Object:  Table [dbo].[Wine]    Script Date: 04/10/2024 13:37:20 ******/
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
/****** Object:  Table [dbo].[WineBatch]    Script Date: 04/10/2024 13:37:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WineBatch](
	[Batch_Id] [int] IDENTITY(1,1) NOT NULL,
	[Wine_Id] [int] NULL,
	[Request_Id] [int] NULL,
	[BatchNumber] [nvarchar](50) NULL,
	[ImportDate] [date] NULL,
	[Quantity] [int] NULL,
	[ProductionYear] [int] NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Batch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineCheck]    Script Date: 04/10/2024 13:37:20 ******/
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
	[CheckDate] [date] NULL,
	[Description] [nvarchar](255) NULL,
	[ImageURL] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Check_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineRequest]    Script Date: 04/10/2024 13:37:20 ******/
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
	[RequestData] [date] NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Request_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WineStorageLocation]    Script Date: 04/10/2024 13:37:20 ******/
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
/****** Object:  Table [dbo].[WineTransaction]    Script Date: 04/10/2024 13:37:20 ******/
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
	[TransDate] [date] NULL,
	[Description] [nvarchar](255) NULL,
	[ImageURL] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
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
ALTER TABLE [dbo].[WineBatch]  WITH CHECK ADD FOREIGN KEY([Request_Id])
REFERENCES [dbo].[WineRequest] ([Request_Id])
GO
ALTER TABLE [dbo].[WineBatch]  WITH CHECK ADD FOREIGN KEY([Wine_Id])
REFERENCES [dbo].[Wine] ([Wine_Id])
GO
ALTER TABLE [dbo].[WineCheck]  WITH CHECK ADD FOREIGN KEY([Batch_Id])
REFERENCES [dbo].[WineBatch] ([Batch_Id])
GO
ALTER TABLE [dbo].[WineCheck]  WITH CHECK ADD FOREIGN KEY([Request_Id])
REFERENCES [dbo].[WineRequest] ([Request_Id])
GO
ALTER TABLE [dbo].[WineCheck]  WITH CHECK ADD FOREIGN KEY([Wine_Id])
REFERENCES [dbo].[Wine] ([Wine_Id])
GO
ALTER TABLE [dbo].[WineRequest]  WITH CHECK ADD  CONSTRAINT [FK_WineRequest_Supplier] FOREIGN KEY([Supplier_Id])
REFERENCES [dbo].[Supplier] ([Supplier_Id])
GO
ALTER TABLE [dbo].[WineRequest] CHECK CONSTRAINT [FK_WineRequest_Supplier]
GO
ALTER TABLE [dbo].[WineTransaction]  WITH CHECK ADD FOREIGN KEY([Batch_Id])
REFERENCES [dbo].[WineBatch] ([Batch_Id])
GO
ALTER TABLE [dbo].[WineTransaction]  WITH CHECK ADD FOREIGN KEY([Location_Id])
REFERENCES [dbo].[WineStorageLocation] ([Location_Id])
GO
ALTER TABLE [dbo].[WineTransaction]  WITH CHECK ADD FOREIGN KEY([Wine_Id])
REFERENCES [dbo].[Wine] ([Wine_Id])
GO
