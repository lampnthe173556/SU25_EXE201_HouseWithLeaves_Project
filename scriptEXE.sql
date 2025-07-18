USE [master]
GO
/****** Object:  Database [MiniPlantStore]    Script Date: 6/22/2025 11:28:42 PM ******/
CREATE DATABASE [MiniPlantStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MiniPlantStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SIXPM\MSSQL\DATA\MiniPlantStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MiniPlantStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SIXPM\MSSQL\DATA\MiniPlantStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MiniPlantStore] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiniPlantStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiniPlantStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MiniPlantStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MiniPlantStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MiniPlantStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MiniPlantStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [MiniPlantStore] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MiniPlantStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MiniPlantStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MiniPlantStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MiniPlantStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MiniPlantStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MiniPlantStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MiniPlantStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MiniPlantStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MiniPlantStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MiniPlantStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MiniPlantStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MiniPlantStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MiniPlantStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MiniPlantStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MiniPlantStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MiniPlantStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MiniPlantStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MiniPlantStore] SET  MULTI_USER 
GO
ALTER DATABASE [MiniPlantStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MiniPlantStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MiniPlantStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MiniPlantStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MiniPlantStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MiniPlantStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MiniPlantStore] SET QUERY_STORE = ON
GO
ALTER DATABASE [MiniPlantStore] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MiniPlantStore]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[CartItemID] [int] IDENTITY(1,1) NOT NULL,
	[CartID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[Size] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CartItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[contact_id] [int] IDENTITY(1,1) NOT NULL,
	[email_contact] [nvarchar](max) NOT NULL,
	[description_contact] [nvarchar](max) NULL,
	[status] [nvarchar](max) NOT NULL,
	[send_at] [datetime] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[contact_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[CouponID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[MaxUsage] [int] NULL,
	[DiscountType] [nvarchar](20) NOT NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[DiscountAmount] [decimal](10, 2) NULL,
	[MinOrderAmount] [decimal](10, 2) NULL,
	[MaxOrderAmount] [decimal](10, 2) NULL,
	[ExpiryDate] [date] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CouponID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FavoriteProducts]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavoriteProducts](
	[FavoriteID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FavoriteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[SubtotalAmount] [decimal](10, 2) NULL,
	[DiscountAppliedAmount] [decimal](10, 2) NULL,
	[ShippingCostApplied] [decimal](10, 2) NULL,
	[TotalAmount] [decimal](10, 2) NULL,
	[CouponID] [int] NULL,
	[ShippingAddress] [nvarchar](255) NULL,
	[Ward] [nvarchar](100) NULL,
	[District] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[PaymentMethodID] [int] NULL,
	[ShippingMethodID] [int] NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[PaymentMethodID] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [nvarchar](100) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ImageURL] [nvarchar](255) NOT NULL,
	[MainPicture] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Size] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[QuantityInStock] [int] NULL,
	[CategoryID] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShippingMethods]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingMethods](
	[ShippingMethodID] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [nvarchar](100) NULL,
	[ShippingCost] [decimal](10, 2) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ShippingMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/22/2025 11:28:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [nvarchar](10) NULL,
	[RoleID] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([CartID], [UserID], [CreatedAt], [UpdatedAt]) VALUES (1, 1, CAST(N'2025-06-17T16:42:31.740' AS DateTime), CAST(N'2025-06-22T09:49:56.187' AS DateTime))
INSERT [dbo].[Cart] ([CartID], [UserID], [CreatedAt], [UpdatedAt]) VALUES (9, 2, CAST(N'2025-06-17T17:48:59.277' AS DateTime), CAST(N'2025-06-22T22:11:30.927' AS DateTime))
INSERT [dbo].[Cart] ([CartID], [UserID], [CreatedAt], [UpdatedAt]) VALUES (46, 9, CAST(N'2025-06-18T21:02:41.263' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[CartItems] ON 

INSERT [dbo].[CartItems] ([CartItemID], [CartID], [ProductID], [Quantity], [UnitPrice], [Size]) VALUES (15, 9, 7, 4, CAST(99000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[CartItems] ([CartItemID], [CartID], [ProductID], [Quantity], [UnitPrice], [Size]) VALUES (16, 9, 2, 1, CAST(120000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[CartItems] ([CartItemID], [CartID], [ProductID], [Quantity], [UnitPrice], [Size]) VALUES (17, 9, 3, 1, CAST(140000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[CartItems] ([CartItemID], [CartID], [ProductID], [Quantity], [UnitPrice], [Size]) VALUES (18, 1, 1, 1, CAST(100000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[CartItems] ([CartItemID], [CartID], [ProductID], [Quantity], [UnitPrice], [Size]) VALUES (19, 1, 8, 1, CAST(100000.00 AS Decimal(10, 2)), NULL)
INSERT [dbo].[CartItems] ([CartItemID], [CartID], [ProductID], [Quantity], [UnitPrice], [Size]) VALUES (20, 9, 1, 1, CAST(100000.00 AS Decimal(10, 2)), NULL)
SET IDENTITY_INSERT [dbo].[CartItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Status]) VALUES (1, N'Mệnh Kim', NULL, 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Status]) VALUES (2, N'Mệnh Mộc', NULL, 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Status]) VALUES (3, N'Mệnh Thủy', NULL, 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Status]) VALUES (4, N'Mệnh Hỏa', NULL, 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Status]) VALUES (5, N'Mệnh Thổ', NULL, 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Status]) VALUES (6, N'Chậu', NULL, 1)
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Status]) VALUES (7, N'Quà tặng', NULL, 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (1, N'pntlam12g03@gmail.com', N'demo', N'Rejected', CAST(N'2025-06-09T00:14:43.110' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (2, N'pntlam12g03@gmail.com', N'demomomomo', N'CHƯA XEM', CAST(N'2025-06-09T00:18:17.467' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (3, N'pntlam12g03@gmail.com', N'aaaabb', N'CHƯA XEM', CAST(N'2025-06-13T12:36:41.460' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (4, N'lampnthe173556@gmail.com', N'abcde', N'CHƯA XEM', CAST(N'2025-06-13T12:39:33.790' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (5, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:14.997' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (6, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:16.313' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (7, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:16.753' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (8, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:16.910' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (9, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:17.047' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (10, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:17.210' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (11, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:18.620' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (12, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:18.777' AS DateTime))
INSERT [dbo].[Contact] ([contact_id], [email_contact], [description_contact], [status], [send_at]) VALUES (13, N'lampnthe173556@gmail.com', N'abc', N'CHƯA XEM', CAST(N'2025-06-13T12:44:18.953' AS DateTime))
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (1, 3, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (2, 3, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (3, 4, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (4, 4, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (5, 5, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (6, 5, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (7, 6, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (8, 6, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (9, 7, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (10, 7, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (11, 8, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (12, 8, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (13, 9, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (14, 9, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (15, 10, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (16, 10, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (17, 11, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (18, 11, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (19, 12, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (20, 12, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (21, 13, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (22, 13, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (23, 14, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (24, 14, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (25, 15, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (26, 15, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (27, 16, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (28, 16, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (29, 17, 7, 4, CAST(99000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (30, 17, 2, 1, CAST(120000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (37, 21, 1, 1, CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (38, 21, 8, 1, CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[OrderDetails] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (39, 22, 3, 1, CAST(140000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (3, 2, CAST(N'2025-06-21T16:34:15.380' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Xã Tiền Phong', N'Huyện Mê Linh', N'Thành phố Hà Nội', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (4, 2, CAST(N'2025-06-21T16:37:12.230' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(516000.00 AS Decimal(10, 2)), NULL, N'abc', N'Xã Má Lé', N'Huyện Đồng Văn', N'Tỉnh Hà Giang', 1, 2, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (5, 2, CAST(N'2025-06-21T16:38:19.737' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Phường Đồng Xuân', N'Quận Hoàn Kiếm', N'Thành phố Hà Nội', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (6, 2, CAST(N'2025-06-21T16:39:10.800' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'z', N'Phường Sông Cầu', N'Thành Phố Bắc Kạn', N'Tỉnh Bắc Kạn', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (7, 2, CAST(N'2025-06-21T16:44:30.047' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Phường Trần Phú', N'Thành phố Hà Giang', N'Tỉnh Hà Giang', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (8, 2, CAST(N'2025-06-21T16:48:34.290' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Phường Sông Hiến', N'Thành phố Cao Bằng', N'Tỉnh Cao Bằng', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (9, 2, CAST(N'2025-06-21T16:49:55.787' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Phường Quang Trung', N'Thành phố Uông Bí', N'Tỉnh Quảng Ninh', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (10, 2, CAST(N'2025-06-21T16:53:47.107' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Phường Đồng Xuân', N'Quận Hoàn Kiếm', N'Thành phố Hà Nội', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (11, 2, CAST(N'2025-06-21T16:54:39.917' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Xã Lũng Cú', N'Huyện Đồng Văn', N'Tỉnh Hà Giang', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (12, 2, CAST(N'2025-06-21T16:56:51.577' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'a', N'Xã Du Già', N'Huyện Yên Minh', N'Tỉnh Hà Giang', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (13, 2, CAST(N'2025-06-21T16:58:31.443' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(516000.00 AS Decimal(10, 2)), NULL, N'abc', N'Xã Văn Phú', N'Huyện Nho Quan', N'Tỉnh Ninh Bình', 1, 2, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (14, 2, CAST(N'2025-06-21T17:00:41.957' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Phường Ninh Mỹ', N'Thành phố Hoa Lư', N'Tỉnh Ninh Bình', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (15, 2, CAST(N'2025-06-21T17:04:41.670' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'a', N'Phường Sông Bằng', N'Thành phố Cao Bằng', N'Tỉnh Cao Bằng', 1, 1, N'Accepted')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (16, 2, CAST(N'2025-06-21T17:04:43.260' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'a', N'Phường Sông Bằng', N'Thành phố Cao Bằng', N'Tỉnh Cao Bằng', 1, 1, N'Rejected')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (17, 2, CAST(N'2025-06-21T17:04:54.583' AS DateTime), CAST(516000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(551000.00 AS Decimal(10, 2)), NULL, N'abc', N'Xã Lũng Phìn', N'Huyện Đồng Văn', N'Tỉnh Hà Giang', 1, 1, N'Accepted')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (21, 1, CAST(N'2025-06-22T09:50:16.657' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(235000.00 AS Decimal(10, 2)), NULL, N'uhuhuhu', N'Phường Tràng Tiền', N'Quận Hoàn Kiếm', N'Thành phố Hà Nội', 1, 1, N'PENDING')
INSERT [dbo].[Orders] ([OrderID], [UserID], [OrderDate], [SubtotalAmount], [DiscountAppliedAmount], [ShippingCostApplied], [TotalAmount], [CouponID], [ShippingAddress], [Ward], [District], [City], [PaymentMethodID], [ShippingMethodID], [Status]) VALUES (22, 2, CAST(N'2025-06-22T22:34:14.453' AS DateTime), CAST(140000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(35000.00 AS Decimal(10, 2)), CAST(175000.00 AS Decimal(10, 2)), NULL, N'0376242766', N'Phường Tràng Tiền', N'Quận Hoàn Kiếm', N'Thành phố Hà Nội', 2, 1, N'ACCEPTED')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentMethods] ON 

INSERT [dbo].[PaymentMethods] ([PaymentMethodID], [MethodName], [Status]) VALUES (1, N'COD', 1)
INSERT [dbo].[PaymentMethods] ([PaymentMethodID], [MethodName], [Status]) VALUES (2, N'QRCode', 1)
SET IDENTITY_INSERT [dbo].[PaymentMethods] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductImages] ON 

INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (1, 1, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayCamNhung.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (2, 2, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayLuoiHoMini.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (3, 3, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayKimTien.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (4, 4, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CaySenDa.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (5, 5, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayXuongRongMini.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (6, 6, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayTungBongLai.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (7, 7, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayNgocNgan.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (8, 8, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayDuoiCong.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (9, 9, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayCauTieuTram.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (10, 10, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayTaiPhat.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (11, 11, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayTreMini.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (12, 12, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayKimNgan.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (13, 13, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayTruongSinh.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (14, 14, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayHoaDongTien.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (15, 15, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayHoaXacPhao.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (16, 16, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayTrucNhatVang.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (17, 17, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayDuongXi.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (18, 18, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayThuongXuan.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (19, 19, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayTrauBa.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (20, 20, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayLanY.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (21, 21, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CayPhuQuy.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (22, 22, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/CaySenDa.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (23, 23, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/ChauTuIn.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (24, 24, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/ChauSuDongLuc.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (25, 25, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/ChauGo.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (26, 26, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/ChauSuThuong.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (27, 27, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/ChauSuTron.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (28, 28, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/HopCarton.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (29, 29, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/XopChongXoc.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (30, 30, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/RomKho.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (31, 31, N'chưa có', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (32, 32, N'https://pub-d913787827c5463bace67e1c9ad26c51.r2.dev/c5efd76d-3c5b-43a2-9653-ea13f2dc74bb-Screenshot 2025-06-22 232612.png', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (33, 33, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/NemThomHuXanh.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (34, 34, N'https://pub-49dfec2518574e5582b9c93461dd9c79.r2.dev/NenThomHoaTulip.jpg', 1)
INSERT [dbo].[ProductImages] ([ImageID], [ProductID], [ImageURL], [MainPicture]) VALUES (35, 35, N'https://pub-d913787827c5463bace67e1c9ad26c51.r2.dev/32fb35ae-228a-44e4-8d95-bbcf2c133853-Screenshot 2025-06-22 232359.png', 1)
SET IDENTITY_INSERT [dbo].[ProductImages] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (1, N'Cây Cẩm Nhung (đỏ hồng)', NULL, N'Lá nhỏ, có gân màu trắng, hồng hoặc đỏ nổi bật.', CAST(100000.00 AS Decimal(10, 2)), 10, 4, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (2, N'Cây Lưỡi Hổ Mini', NULL, N'Giúp lọc không khí hiệu quả.', CAST(120000.00 AS Decimal(10, 2)), 10, 1, CAST(N'2025-06-06T15:55:07.530' AS DateTime), CAST(N'2025-06-06T15:55:07.530' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (3, N'Cây Kim Tiền', NULL, N'Mang lại tài lộc, thịnh vượng.', CAST(140000.00 AS Decimal(10, 2)), 10, 5, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (4, N'Cây Sen Đá', NULL, N'Dễ chăm sóc, phù hợp với người bận rộn.', CAST(120000.00 AS Decimal(10, 2)), 10, 4, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (5, N'Cây Xương Rồng Mini', NULL, N'Biểu tượng của sự kiên cường và bảo vệ.', CAST(150000.00 AS Decimal(10, 2)), 10, 5, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (6, N'Cây Tùng Bồng Lai', NULL, N'Mang lại sự bình an, may mắn.', CAST(150000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (7, N'Cây Ngọc Ngân', NULL, N'Tượng trưng cho sự may mắn và thịnh vượng.', CAST(99000.00 AS Decimal(10, 2)), 10, 1, CAST(N'2025-06-06T15:55:07.530' AS DateTime), CAST(N'2025-06-06T15:55:07.530' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (8, N'Cây Đuôi Công (tím/đỏ)', NULL, N'Mang lại sự may mắn và thịnh vượng.', CAST(100000.00 AS Decimal(10, 2)), 10, 4, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (9, N'Cây Cau Tiểu Trâm', NULL, N'Biểu tượng của sự thăng tiến và phát triển.', CAST(100000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (10, N'Cây Tai Phật', NULL, N'Mang lại sự bảo vệ và che chở.', CAST(100000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (11, N'Cây Tre Mini', NULL, N'Tượng trưng cho sự kiên cường và may mắn.', CAST(130000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (12, N'Cây Kim Ngân', NULL, N'Mang ý nghĩa hút tài lộc, mang đến sự thịnh vượng.', CAST(150000.00 AS Decimal(10, 2)), 10, 1, CAST(N'2025-06-06T15:55:07.530' AS DateTime), CAST(N'2025-06-06T15:55:07.530' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (13, N'Cây Trường Sinh', NULL, N'Tượng trưng cho sức sống bền bỉ.', CAST(119000.00 AS Decimal(10, 2)), 10, 3, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (14, N'Cây Hoa Đồng Tiền', NULL, N'Biểu tượng của sự may mắn và tài lộc.', CAST(100000.00 AS Decimal(10, 2)), 10, 5, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (15, N'Cây Hoa Xác Pháo', NULL, N'Mang lại sự may mắn và tài lộc.', CAST(100000.00 AS Decimal(10, 2)), 10, 1, CAST(N'2025-06-06T15:55:07.530' AS DateTime), CAST(N'2025-06-06T15:55:07.530' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (16, N'Cây Trúc Nhật Vàng', NULL, N'Tượng trưng cho sự may mắn và tài lộc.', CAST(100000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (17, N'Cây Dương Xỉ', NULL, N'Giúp thanh lọc không khí.', CAST(100000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (18, N'Cây Thường Xuân', NULL, N'Mang lại sự may mắn và tài lộc.', CAST(119000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (19, N'Cây Trầu Bà', NULL, N'Mang ý nghĩa hút tài lộc, mang đến sự thịnh vượng.', CAST(135000.00 AS Decimal(10, 2)), 10, 2, CAST(N'2025-06-06T15:55:07.547' AS DateTime), CAST(N'2025-06-06T15:55:07.547' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (20, N'Cây Lan Ý', NULL, N'Có khả năng làm sạch không khí, hút vượng khí và tài lộc.', CAST(150000.00 AS Decimal(10, 2)), 10, 1, CAST(N'2025-06-06T15:55:07.530' AS DateTime), CAST(N'2025-06-06T15:55:07.530' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (21, N'Cây Phú Quý', NULL, N'Tượng trưng cho sự giàu có và thịnh vượng.', CAST(109000.00 AS Decimal(10, 2)), 10, 4, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (22, N'Cây Sen Đá Nâu', NULL, N'Dễ chăm sóc, phù hợp với người bận rộn.', CAST(100000.00 AS Decimal(10, 2)), 10, 5, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (23, N'Khắc chữ theo yêu cầu', NULL, NULL, CAST(53000.00 AS Decimal(10, 2)), 10, 6, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (24, N'Chậu sứ (tạo động lực)', NULL, NULL, CAST(18000.00 AS Decimal(10, 2)), 10, 6, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (25, N'Chậu gỗ', NULL, NULL, CAST(29000.00 AS Decimal(10, 2)), 10, 6, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (26, N'hôm nay có gì vui nè', NULL, NULL, CAST(95000.00 AS Decimal(10, 2)), 10, 6, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (27, N'Chậu trơn sứ', NULL, NULL, CAST(19000.00 AS Decimal(10, 2)), 10, 6, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (28, N'Hộp carton ( 30x20x15)', NULL, NULL, CAST(3000.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (29, N'Xốp chống xốc', NULL, NULL, CAST(50000.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (30, N'Rơm Khô', NULL, NULL, CAST(30000.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (31, N'Logo', NULL, NULL, CAST(120000.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-22T20:54:34.837' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (32, N'Thiệp cảm ơn', N'Nhiều loại', NULL, CAST(1600.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-22T23:26:42.117' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (33, N'Nến thơm hũ xanh lá', NULL, NULL, CAST(95000.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (34, N'Nến thơm hoa tulip', NULL, NULL, CAST(46000.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-06T15:55:07.550' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Size], [Description], [Price], [QuantityInStock], [CategoryID], [CreatedAt], [UpdatedAt], [IsActive], [IsDeleted]) VALUES (35, N'Túi mica', N'Đủ cỡ', NULL, CAST(14500.00 AS Decimal(10, 2)), 10, 7, CAST(N'2025-06-06T15:55:07.550' AS DateTime), CAST(N'2025-06-22T23:25:14.167' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Customer')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[ShippingMethods] ON 

INSERT [dbo].[ShippingMethods] ([ShippingMethodID], [MethodName], [ShippingCost], [Status]) VALUES (1, N'Tiêu Chuẩn', CAST(35000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[ShippingMethods] ([ShippingMethodID], [MethodName], [ShippingCost], [Status]) VALUES (2, N'Nhận Tại Cửa Hàng', CAST(0.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[ShippingMethods] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Email], [PasswordHash], [FullName], [Phone], [DateOfBirth], [Gender], [RoleID], [CreatedAt], [UpdatedAt], [Status]) VALUES (1, N'demo@example.com', N'15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225', N'Phạm Ngọc Tùng Lâm', N'0913540430', CAST(N'2025-06-08' AS Date), N'MALE', 1, CAST(N'2025-06-09T23:37:00.000' AS DateTime), CAST(N'2025-06-22T03:08:06.857' AS DateTime), 0)
INSERT [dbo].[Users] ([UserID], [Email], [PasswordHash], [FullName], [Phone], [DateOfBirth], [Gender], [RoleID], [CreatedAt], [UpdatedAt], [Status]) VALUES (2, N'pntlam12g03@gmail.com', N'15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225', N'Phạm Ngọc Tùng Lâm', N'0913540430', CAST(N'2025-05-24' AS Date), N'MALE', 1, CAST(N'2025-06-10T09:31:58.643' AS DateTime), CAST(N'2025-06-13T12:45:08.287' AS DateTime), 1)
INSERT [dbo].[Users] ([UserID], [Email], [PasswordHash], [FullName], [Phone], [DateOfBirth], [Gender], [RoleID], [CreatedAt], [UpdatedAt], [Status]) VALUES (3, N'lamlam276762@gmail.com', N'9b0f3cba1b9ff6102c728d835e7eca4e3a83af46cd943e0e50b5c190b6a88778', N'Phạm Ngọc Tùng Lâm', N'0913540430', CAST(N'2025-06-08' AS Date), NULL, 1, CAST(N'2025-06-10T09:33:59.587' AS DateTime), NULL, 1)
INSERT [dbo].[Users] ([UserID], [Email], [PasswordHash], [FullName], [Phone], [DateOfBirth], [Gender], [RoleID], [CreatedAt], [UpdatedAt], [Status]) VALUES (4, N'test@gmail.com', N'c5ab23e8a8e02bf0a42b335ceab0c0e957a0f594b1ca57f8adc985d9931b3c48', N'Phạm Ngọc Tùng Lâm', N'0913540430', CAST(N'2025-06-02' AS Date), N'MALE', 1, CAST(N'2025-06-10T09:43:02.973' AS DateTime), CAST(N'2025-06-10T09:42:55.767' AS DateTime), 1)
INSERT [dbo].[Users] ([UserID], [Email], [PasswordHash], [FullName], [Phone], [DateOfBirth], [Gender], [RoleID], [CreatedAt], [UpdatedAt], [Status]) VALUES (5, N'test2@gmail.com', N'123456789', N'abc', N'1234567890', CAST(N'2025-06-01' AS Date), N'MALE', 1, CAST(N'2025-06-10T23:00:44.487' AS DateTime), CAST(N'2025-06-22T03:53:35.030' AS DateTime), 0)
INSERT [dbo].[Users] ([UserID], [Email], [PasswordHash], [FullName], [Phone], [DateOfBirth], [Gender], [RoleID], [CreatedAt], [UpdatedAt], [Status]) VALUES (9, N'admin@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'abc', N'1234567890', NULL, NULL, 2, CAST(N'2025-06-18T21:01:38.923' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [UQ__Cart__1788CCAD317EE2FD]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Cart] ADD UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Cart__1788CCADA2AB6891]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Cart] ADD UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Cart_Product_Size]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[CartItems] ADD  CONSTRAINT [UQ_Cart_Product_Size] UNIQUE NONCLUSTERED 
(
	[CartID] ASC,
	[ProductID] ASC,
	[Size] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Coupons__A25C5AA714C50731]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Coupons] ADD UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Coupons__A25C5AA75B10757B]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Coupons] ADD UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_User_Product]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[FavoriteProducts] ADD  CONSTRAINT [UQ_User_Product] UNIQUE NONCLUSTERED 
(
	[UserID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__8A2B6160508ECF63]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__8A2B6160A5FB9F30]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053441220787]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534699E1370]    Script Date: 6/22/2025 11:28:43 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CartItems] ADD  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Coupons] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[FavoriteProducts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [SubtotalAmount]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [DiscountAppliedAmount]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [ShippingCostApplied]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[PaymentMethods] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [QuantityInStock]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ShippingMethods] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [RoleID]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Cart] FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Cart]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Product]
GO
ALTER TABLE [dbo].[FavoriteProducts]  WITH CHECK ADD  CONSTRAINT [FK_Favorite_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FavoriteProducts] CHECK CONSTRAINT [FK_Favorite_Product]
GO
ALTER TABLE [dbo].[FavoriteProducts]  WITH CHECK ADD  CONSTRAINT [FK_Favorite_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FavoriteProducts] CHECK CONSTRAINT [FK_Favorite_User]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CouponID])
REFERENCES [dbo].[Coupons] ([CouponID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethods] ([PaymentMethodID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([ShippingMethodID])
REFERENCES [dbo].[ShippingMethods] ([ShippingMethodID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD CHECK  (([DiscountType]=N'FixedAmount' OR [DiscountType]=N'Percentage'))
GO
ALTER TABLE [dbo].[Coupons]  WITH CHECK ADD CHECK  (([DiscountType]=N'FixedAmount' OR [DiscountType]=N'Percentage'))
GO
USE [master]
GO
ALTER DATABASE [MiniPlantStore] SET  READ_WRITE 
GO
