USE [master]
GO
/****** Object:  Database [ShoesDB]    Script Date: 12/15/2015 12:13:27 AM ******/
CREATE DATABASE [ShoesDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShoesDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ShoesDB.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ShoesDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ShoesDB_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ShoesDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoesDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShoesDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShoesDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShoesDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShoesDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShoesDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShoesDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShoesDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShoesDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShoesDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShoesDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShoesDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShoesDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShoesDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShoesDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShoesDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ShoesDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShoesDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShoesDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShoesDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShoesDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShoesDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShoesDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShoesDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ShoesDB] SET  MULTI_USER 
GO
ALTER DATABASE [ShoesDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShoesDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShoesDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShoesDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ShoesDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShoesDB', N'ON'
GO
USE [ShoesDB]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[DonHangID] [int] IDENTITY(1,1) NOT NULL,
	[NgayDatHang] [datetime] NOT NULL,
	[TenKhachHang] [nvarchar](510) NULL,
	[DiaChi] [nvarchar](250) NULL,
	[SoDienThoai] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](max) NULL,
	[DaGiaoHang] [bit] NOT NULL,
	[UserId] [nvarchar](128) NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[DonHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHangChiTiet]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHangChiTiet](
	[DonHangID] [int] NOT NULL,
	[SanPhamSizeID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [decimal](18, 0) NOT NULL,
	[ThanhTien] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_DonHangChiTiet] PRIMARY KEY CLUSTERED 
(
	[DonHangID] ASC,
	[SanPhamSizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhuyenMai]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhuyenMai](
	[KhuyenMaiID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](150) NULL,
	[NgayBatDau] [datetime] NOT NULL,
	[NgayKetThuc] [datetime] NOT NULL,
	[Mota] [nvarchar](250) NULL,
	[GiamGia] [float] NOT NULL,
 CONSTRAINT [PK_KhuyenMai] PRIMARY KEY CLUSTERED 
(
	[KhuyenMaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Loai]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Loai](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](100) NULL,
	[ChungLoaiID] [int] NULL,
	[BiDanh] [varchar](100) NULL,
 CONSTRAINT [PK_Loai] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuanTri]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuanTri](
	[Username] [nchar](10) NOT NULL,
	[Password] [nchar](20) NOT NULL,
	[Level] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPham](
	[SanPhamID] [int] IDENTITY(1,1) NOT NULL,
	[MaSanPham] [varchar](50) NOT NULL,
	[TenSanPham] [nvarchar](150) NOT NULL,
	[GiaBan] [decimal](18, 0) NOT NULL,
	[DanhGia] [int] NULL,
	[TrangThai] [tinyint] NULL,
	[Mota] [nvarchar](max) NULL,
	[LoaiID] [int] NOT NULL,
	[NhaSanXuat] [nvarchar](150) NULL,
	[Deleted] [bit] NOT NULL,
	[KhuyenMaiID] [int] NOT NULL,
	[BiDanh] [varchar](150) NULL,
	[SoLanXem] [int] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[SanPhamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SanPhamHinh]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPhamHinh](
	[SanPhamHinhID] [int] IDENTITY(1,1) NOT NULL,
	[TenHinh] [nvarchar](100) NULL,
	[SanPhamID] [int] NOT NULL,
	[ThuTuHienThi] [tinyint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SanPhamHinh] PRIMARY KEY CLUSTERED 
(
	[SanPhamHinhID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SanPhamSize]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPhamSize](
	[SanPhamSizeID] [int] IDENTITY(1,1) NOT NULL,
	[SanPhamID] [int] NOT NULL,
	[SizeID] [int] NOT NULL,
 CONSTRAINT [PK_SanPhamSize] PRIMARY KEY CLUSTERED 
(
	[SanPhamSizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Size]    Script Date: 12/15/2015 12:13:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Size](
	[SizeID] [int] IDENTITY(1,1) NOT NULL,
	[MaSize] [varchar](10) NULL,
	[TenSize] [varchar](10) NULL,
	[GhiChu] [nvarchar](50) NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[DonHang] ON 

INSERT [dbo].[DonHang] ([DonHangID], [NgayDatHang], [TenKhachHang], [DiaChi], [SoDienThoai], [GhiChu], [DaGiaoHang], [UserId]) VALUES (1, CAST(N'2015-12-09 23:41:01.677' AS DateTime), N'phuc', N'123xvnt', N'12312412', N'asdasd', 0, NULL)
SET IDENTITY_INSERT [dbo].[DonHang] OFF
INSERT [dbo].[DonHangChiTiet] ([DonHangID], [SanPhamSizeID], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 50, 1, CAST(190000 AS Decimal(18, 0)), CAST(190000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[KhuyenMai] ON 

INSERT [dbo].[KhuyenMai] ([KhuyenMaiID], [Ten], [NgayBatDau], [NgayKetThuc], [Mota], [GiamGia]) VALUES (1, N'Không', CAST(N'1753-01-01 00:00:00.000' AS DateTime), CAST(N'9999-12-31 11:59:59.000' AS DateTime), N'Không có khuyến mãi', 1)
INSERT [dbo].[KhuyenMai] ([KhuyenMaiID], [Ten], [NgayBatDau], [NgayKetThuc], [Mota], [GiamGia]) VALUES (2, N'Giảm 20%', CAST(N'2015-04-01 00:00:00.000' AS DateTime), CAST(N'2015-05-01 00:00:00.000' AS DateTime), N'Nhân dịp lễ 30/4 và Quốc tế lao động, giảm giá 20% cho tất cả các mặt hàng', 0.8)
INSERT [dbo].[KhuyenMai] ([KhuyenMaiID], [Ten], [NgayBatDau], [NgayKetThuc], [Mota], [GiamGia]) VALUES (6, N'Giảm 10%', CAST(N'2015-08-01 00:00:00.000' AS DateTime), CAST(N'2015-09-03 00:00:00.000' AS DateTime), N'Nhân dịp lễ Quốc khánh, giảm giá 10% cho tất cả các mặt hàng', 0.9)
SET IDENTITY_INSERT [dbo].[KhuyenMai] OFF
SET IDENTITY_INSERT [dbo].[Loai] ON 

INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (3, N'Giày dép nam', NULL, N'Giay-dep-nam')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (4, N'Giày da nam', 3, N'Giay-da-nam')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (5, N'Giày thể thao nam', 3, N'Giay-the-thao-nam')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (6, N'Giày vải nam', 3, N'Giay-vai-nam')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (8, N'Dép da nam', 3, N'Dep-da-nam')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (9, N'Xăng đan nam', 3, N'Xang-dan-nam')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (10, N'Giầy dép nữ', NULL, N'Giay-dep-nu')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (11, N'Giày thể thao nữ', 10, N'Giay-the-thao-nu')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (12, N'Giày da nữ', 10, N'Giay-da-nu')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (13, N'Giày vải nữ', 10, N'Giay-vai-nu')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (14, N'Xăng đan nữ', 10, N'Xang-dan-nu')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (15, N'Giày dép trẻ em', NULL, N'Giay-dep-tre-em')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (16, N'Dép da', 15, N'Dep-da')
INSERT [dbo].[Loai] ([ID], [Ten], [ChungLoaiID], [BiDanh]) VALUES (17, N'Xăng đan', 15, N'Xang-dan')
SET IDENTITY_INSERT [dbo].[Loai] OFF
INSERT [dbo].[QuanTri] ([Username], [Password], [Level]) VALUES (N'phucngo   ', N'070695              ', 1)
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (8, N'SP01', N'Giày da nam olunpo - cao cấp', CAST(1499000 AS Decimal(18, 0)), 0, 1, N'    
                                        
                                        
                                    Nhãn hiệu: Olunpo

Phong cách: Casual

Dành cho: Nam

Chất liệu: Da

Đế giầy: Cao su

Bảo hành:

Baza Việt Nam tự hào là nhà nhập khẩu và phân phối độc quyền, cung cấp tận tay quý khách hàng các sản phẩm giày chính hãng Olunpo với chất lượng tốt nhất và giá cả ưu đãi nhất. Chế độ bảo hành, đổi, trả giày Olunpo áp dụng cho các sản phẩm giày dép thương hiệu Olunpo được bán tại Baza như sau:

A. Thời gian bảo hành:

Thời gian bảo hành 2 tháng tính từ ngày quý khách nhận được hàng.

B. Nội dung bảo hành

a) Trả hàng: Trong vòng 1 tháng, nếu sử dụng trong điều kiện bình thường mà bị hỏng đế, gãy gót, hỏng bề mặt, có thể đổi/ trả giầy.

b) Đổi hàng: Trong vòng 7 ngày nếu quý khách phát hiện ra sự sai khác rõ rệt về sản phẩm thực tế so với sản phẩm mình đặt mua như: không đúng về màu sắc, kích cỡ, sản phẩm, giầy trái và phải không cùng đôi có thể đổi hàng. Cần phải bảo đảm đế giầy không bị bẩn, bề mặt giầy không nhăn hoặc rạn, không ảnh hưởng đến việc tiếp tục tiêu thụ.

c) Bảo dưỡng: Trong thời gian bảo hành quy định ở trên, nếu xuất hiện các vấn đề về chất lượng như bong keo, đứt chỉ, bong bề mặt giầy, có thể liên hệ với chúng tôi để sửa chữa.                                
                                                                
                                                                
                                ', 4, N'Nhập khẩu', 0, 2, N'Giay-da-nam-olunpo---cao-cap', 7)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (10, N'SP02', N'Giày thể thao nữ NU742', CAST(415000 AS Decimal(18, 0)), 0, 1, N'    
                                                                        
                                                                
                                ', 11, N'Nhập khẩu', 0, 2, N'Giay-the-thao-nu-NU742', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (11, N'SP03', N'Dép VENTO VNXK giá rẻ', CAST(190000 AS Decimal(18, 0)), 0, 1, N'...', 9, N'Việt Nam', 0, 1, N'Dep-VENTO-VNXK-gia-re', 4)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (12, N'SP04', N'Giầy lưới cao cấp AlphaShop', CAST(350000 AS Decimal(18, 0)), 0, 2, NULL, 6, N'Việt Nam', 0, 1, N'Giay-luoi-cao-cap-AlphaShop', 3)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (16, N'SP05', N'Giầy mọi nam đẹp giá rẻ', CAST(250000 AS Decimal(18, 0)), 0, 1, NULL, 4, N'Việt Nam', 0, 1, N'Giay-moi-nam-dep-gia-re', 6)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (19, N'SP06', N'Sandal nam SP06', CAST(215000 AS Decimal(18, 0)), 0, 2, NULL, 9, N'Việt Nam', 0, 1, N'Sandal-nam-SP06', 5)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (22, N'SP07', N'Giày Nam Hàn Quốc ', CAST(249000 AS Decimal(18, 0)), 0, 1, NULL, 5, N'Nhập khẩu', 0, 1, N'Giay-Nam-Han-Quoc', 7)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (27, N'SP08', N'Giày da nam Lacoste', CAST(430000 AS Decimal(18, 0)), 0, 1, NULL, 4, N'Nhập khẩu', 0, 2, N'Giay-da-nam-Lacoste', 7)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (28, N'SP09', N'Giày da nam SP09', CAST(340000 AS Decimal(18, 0)), 0, 2, NULL, 4, N'Nhập khẩu', 0, 2, N'Giay-da-nam-SP09', 8)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (32, N'SP10', N'Giày da nam SP10', CAST(425000 AS Decimal(18, 0)), 0, 1, NULL, 4, N'Nhập khẩu', 0, 2, N'Giay-da-nam-SP10', 9)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (33, N'SP11', N'Giày da nam SP11', CAST(379000 AS Decimal(18, 0)), 0, 1, NULL, 4, N'Nhập khẩu', 0, 2, N'Giay-da-nam-SP11', 10)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (34, N'SP12', N'Giày da nam SP12', CAST(658000 AS Decimal(18, 0)), 0, 1, NULL, 4, N'Nhẩu khẩu', 0, 1, N'Giay-da-nam-SP12', 11)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (35, N'SP13', N'Giày da nam SP13', CAST(118000 AS Decimal(18, 0)), 0, 1, NULL, 4, N'Nhẩu khẩu', 0, 1, N'Giay-da-nam-SP13', 12)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSanPham], [TenSanPham], [GiaBan], [DanhGia], [TrangThai], [Mota], [LoaiID], [NhaSanXuat], [Deleted], [KhuyenMaiID], [BiDanh], [SoLanXem]) VALUES (36, N'SP14', N'Giày da nam SP14', CAST(318000 AS Decimal(18, 0)), 0, 1, NULL, 4, N'Nhẩu khẩu', 0, 6, N'Giay-da-nam-SP14', 14)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
SET IDENTITY_INSERT [dbo].[SanPhamHinh] ON 

INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (7, N'GiayDaNam.jpg', 8, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (8, N'GiayDaNam2.jpg', 8, 2, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (9, N'GiayDaNam3.jpg', 8, 3, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (10, N'GiayTTNu.jpg', 10, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (11, N'GiayMoi.jpg', 16, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (12, N'sandalNam.jpg', 19, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (13, N'Giayluoi.jpg', 12, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (14, N'Vento.jpg', 11, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (15, N'Giaythethao.jpg', 22, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (18, N'27.jpg', 27, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (19, N'28.jpg', 28, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (20, N'32.jpg', 32, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (21, N'33.jpg', 33, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (22, N'34.jpg', 34, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (23, N'35.jpg', 35, 1, 0)
INSERT [dbo].[SanPhamHinh] ([SanPhamHinhID], [TenHinh], [SanPhamID], [ThuTuHienThi], [IsDeleted]) VALUES (24, N'36.jpeg', 36, 1, 0)
SET IDENTITY_INSERT [dbo].[SanPhamHinh] OFF
SET IDENTITY_INSERT [dbo].[SanPhamSize] ON 

INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (4, 8, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (5, 8, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (8, 10, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (9, 10, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (10, 10, 3)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (11, 10, 4)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (16, 8, 4)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (19, 36, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (20, 36, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (21, 36, 3)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (23, 33, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (24, 33, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (25, 34, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (26, 34, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (27, 34, 3)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (28, 34, 4)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (29, 35, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (30, 35, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (31, 32, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (32, 32, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (33, 28, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (34, 28, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (35, 28, 3)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (36, 27, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (37, 27, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (38, 27, 3)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (39, 27, 4)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (40, 22, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (41, 22, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (42, 19, 1)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (43, 19, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (45, 19, 4)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (46, 16, 3)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (47, 16, 4)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (48, 12, 3)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (49, 12, 4)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (50, 11, 2)
INSERT [dbo].[SanPhamSize] ([SanPhamSizeID], [SanPhamID], [SizeID]) VALUES (51, 11, 3)
SET IDENTITY_INSERT [dbo].[SanPhamSize] OFF
SET IDENTITY_INSERT [dbo].[Size] ON 

INSERT [dbo].[Size] ([SizeID], [MaSize], [TenSize], [GhiChu]) VALUES (1, N'S', N'39', NULL)
INSERT [dbo].[Size] ([SizeID], [MaSize], [TenSize], [GhiChu]) VALUES (2, N'M', N'40', NULL)
INSERT [dbo].[Size] ([SizeID], [MaSize], [TenSize], [GhiChu]) VALUES (3, N'L', N'41', NULL)
INSERT [dbo].[Size] ([SizeID], [MaSize], [TenSize], [GhiChu]) VALUES (4, N'XL', N'42', NULL)
SET IDENTITY_INSERT [dbo].[Size] OFF
ALTER TABLE [dbo].[DonHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_DonHangChiTiet_DonHang] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([DonHangID])
GO
ALTER TABLE [dbo].[DonHangChiTiet] CHECK CONSTRAINT [FK_DonHangChiTiet_DonHang]
GO
ALTER TABLE [dbo].[DonHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_DonHangChiTiet_SanPhamSize] FOREIGN KEY([SanPhamSizeID])
REFERENCES [dbo].[SanPhamSize] ([SanPhamSizeID])
GO
ALTER TABLE [dbo].[DonHangChiTiet] CHECK CONSTRAINT [FK_DonHangChiTiet_SanPhamSize]
GO
ALTER TABLE [dbo].[Loai]  WITH CHECK ADD  CONSTRAINT [FK_Loai_Loai1] FOREIGN KEY([ChungLoaiID])
REFERENCES [dbo].[Loai] ([ID])
GO
ALTER TABLE [dbo].[Loai] CHECK CONSTRAINT [FK_Loai_Loai1]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_KhuyenMai] FOREIGN KEY([KhuyenMaiID])
REFERENCES [dbo].[KhuyenMai] ([KhuyenMaiID])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_KhuyenMai]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_Loai] FOREIGN KEY([LoaiID])
REFERENCES [dbo].[Loai] ([ID])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_Loai]
GO
ALTER TABLE [dbo].[SanPhamHinh]  WITH CHECK ADD  CONSTRAINT [FK_SanPhamHinh_SanPham] FOREIGN KEY([SanPhamID])
REFERENCES [dbo].[SanPham] ([SanPhamID])
GO
ALTER TABLE [dbo].[SanPhamHinh] CHECK CONSTRAINT [FK_SanPhamHinh_SanPham]
GO
ALTER TABLE [dbo].[SanPhamSize]  WITH CHECK ADD  CONSTRAINT [FK_SanPhamSize_SanPham] FOREIGN KEY([SanPhamID])
REFERENCES [dbo].[SanPham] ([SanPhamID])
GO
ALTER TABLE [dbo].[SanPhamSize] CHECK CONSTRAINT [FK_SanPhamSize_SanPham]
GO
ALTER TABLE [dbo].[SanPhamSize]  WITH CHECK ADD  CONSTRAINT [FK_SanPhamSize_Size] FOREIGN KEY([SizeID])
REFERENCES [dbo].[Size] ([SizeID])
GO
ALTER TABLE [dbo].[SanPhamSize] CHECK CONSTRAINT [FK_SanPhamSize_Size]
GO
USE [master]
GO
ALTER DATABASE [ShoesDB] SET  READ_WRITE 
GO
