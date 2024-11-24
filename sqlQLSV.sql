USE [QuanLyDiemTruongDaiHoc]
GO
/****** Object:  Table [dbo].[BoMon]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoMon](
	[MaBoMon] [varchar](10) NOT NULL,
	[TenBoMon] [nvarchar](200) NULL,
	[MaKhoa] [varchar](10) NULL,
	[TruongBoMon] [varchar](10) NULL,
 CONSTRAINT [pk_BoMon] PRIMARY KEY CLUSTERED 
(
	[MaBoMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuongTrinhDaoTao]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuongTrinhDaoTao](
	[MaCTDT] [varchar](25) NOT NULL,
	[TenCTDT] [nvarchar](200) NULL,
	[MaKhoa] [varchar](10) NULL,
 CONSTRAINT [pk_ChuongTrinhDaoTao] PRIMARY KEY CLUSTERED 
(
	[MaCTDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTDT_HocPhan]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTDT_HocPhan](
	[MaCTDT] [varchar](25) NOT NULL,
	[MaHocPhan] [varchar](25) NOT NULL,
	[KyHoc] [int] NULL,
 CONSTRAINT [pk_CTDT_HocPhan] PRIMARY KEY CLUSTERED 
(
	[MaCTDT] ASC,
	[MaHocPhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangVien]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangVien](
	[MaGiangVien] [varchar](10) NOT NULL,
	[HoDem] [nvarchar](35) NULL,
	[Ten] [nvarchar](35) NULL,
	[MaBoMon] [varchar](10) NULL,
	[NgaySinh] [date] NULL,
	[HocHam] [nvarchar](20) NULL,
	[HocVi] [nvarchar](20) NULL,
	[ChucDanh] [nvarchar](20) NULL,
	[DienThoai] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [pk_GiangVien] PRIMARY KEY CLUSTERED 
(
	[MaGiangVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocPhan]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocPhan](
	[MaHocPhan] [varchar](25) NOT NULL,
	[TenHocPhan] [nvarchar](200) NULL,
	[MaBoMon] [varchar](10) NULL,
	[SoTinChi] [int] NULL,
	[TrongSoDiemQuaTrinh] [float] NULL,
	[TrongSoDiemThiKTHP] [float] NULL,
 CONSTRAINT [pk_HocPhan] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khoa]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khoa](
	[MaKhoa] [varchar](10) NOT NULL,
	[TenKhoa] [nvarchar](200) NULL,
 CONSTRAINT [pk_Khoa] PRIMARY KEY CLUSTERED 
(
	[MaKhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[MaLop] [varchar](25) NOT NULL,
	[TenLop] [nvarchar](200) NULL,
	[MaKhoa] [varchar](10) NULL,
	[MaCTDT] [varchar](25) NULL,
	[KhoaHoc] [varchar](10) NULL,
 CONSTRAINT [pk_Lop] PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopHocPhan]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHocPhan](
	[MaHocPhan] [varchar](25) NOT NULL,
	[MaLopHocPhan] [varchar](25) NOT NULL,
	[TenLopHocPhan] [nvarchar](200) NULL,
	[MaGiangVien] [varchar](10) NULL,
	[NamHoc] [varchar](20) NULL,
	[HocKy] [int] NULL,
	[DotHoc] [nchar](10) NULL,
 CONSTRAINT [pk_LopHocPhan] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC,
	[MaLopHocPhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopHocPhan_SinhVien]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHocPhan_SinhVien](
	[MaHocPhan] [varchar](25) NOT NULL,
	[MaLopHocPhan] [varchar](25) NOT NULL,
	[MaSinhVien] [varchar](10) NOT NULL,
	[DiemQuaTrinh] [float] NULL,
	[DiemThiKTHP] [float] NULL,
	[DiemTKHP] [float] NULL,
	[DiemHe4] [float] NULL,
	[DiemHeChu] [varchar](10) NULL,
	[LanHoc] [int] NULL,
 CONSTRAINT [pk_LopHocPhan_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC,
	[MaLopHocPhan] ASC,
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSinhVien] [varchar](10) NOT NULL,
	[HoDem] [nvarchar](35) NULL,
	[Ten] [nvarchar](35) NULL,
	[MaLop] [varchar](25) NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](250) NULL,
	[DienThoai] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [pk_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BoMon]  WITH CHECK ADD  CONSTRAINT [FK_BoMon_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO
ALTER TABLE [dbo].[BoMon] CHECK CONSTRAINT [FK_BoMon_Khoa]
GO
ALTER TABLE [dbo].[ChuongTrinhDaoTao]  WITH CHECK ADD  CONSTRAINT [FK_ChuongTrinhDaoTao_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO
ALTER TABLE [dbo].[ChuongTrinhDaoTao] CHECK CONSTRAINT [FK_ChuongTrinhDaoTao_Khoa]
GO
ALTER TABLE [dbo].[CTDT_HocPhan]  WITH CHECK ADD  CONSTRAINT [FK_CTDT_HocPhan_ChuongTrinhDaoTao] FOREIGN KEY([MaCTDT])
REFERENCES [dbo].[ChuongTrinhDaoTao] ([MaCTDT])
GO
ALTER TABLE [dbo].[CTDT_HocPhan] CHECK CONSTRAINT [FK_CTDT_HocPhan_ChuongTrinhDaoTao]
GO
ALTER TABLE [dbo].[CTDT_HocPhan]  WITH CHECK ADD  CONSTRAINT [FK_CTDT_HocPhan_HocPhan] FOREIGN KEY([MaHocPhan])
REFERENCES [dbo].[HocPhan] ([MaHocPhan])
GO
ALTER TABLE [dbo].[CTDT_HocPhan] CHECK CONSTRAINT [FK_CTDT_HocPhan_HocPhan]
GO
ALTER TABLE [dbo].[GiangVien]  WITH CHECK ADD  CONSTRAINT [FK_GiangVien_BoMon] FOREIGN KEY([MaBoMon])
REFERENCES [dbo].[BoMon] ([MaBoMon])
GO
ALTER TABLE [dbo].[GiangVien] CHECK CONSTRAINT [FK_GiangVien_BoMon]
GO
ALTER TABLE [dbo].[HocPhan]  WITH CHECK ADD  CONSTRAINT [FK_HocPhan_BoMon] FOREIGN KEY([MaBoMon])
REFERENCES [dbo].[BoMon] ([MaBoMon])
GO
ALTER TABLE [dbo].[HocPhan] CHECK CONSTRAINT [FK_HocPhan_BoMon]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [fk_Lop_CTDT] FOREIGN KEY([MaCTDT])
REFERENCES [dbo].[ChuongTrinhDaoTao] ([MaCTDT])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [fk_Lop_CTDT]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [fk_Lop_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [fk_Lop_Khoa]
GO
ALTER TABLE [dbo].[LopHocPhan]  WITH CHECK ADD  CONSTRAINT [FK_LopHocPhan_GiangVien] FOREIGN KEY([MaGiangVien])
REFERENCES [dbo].[GiangVien] ([MaGiangVien])
GO
ALTER TABLE [dbo].[LopHocPhan] CHECK CONSTRAINT [FK_LopHocPhan_GiangVien]
GO
ALTER TABLE [dbo].[LopHocPhan]  WITH CHECK ADD  CONSTRAINT [FK_LopHocPhan_HocPhan] FOREIGN KEY([MaHocPhan])
REFERENCES [dbo].[HocPhan] ([MaHocPhan])
GO
ALTER TABLE [dbo].[LopHocPhan] CHECK CONSTRAINT [FK_LopHocPhan_HocPhan]
GO
ALTER TABLE [dbo].[LopHocPhan_SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_LopHocPhan_SinhVien_LopHocPhan] FOREIGN KEY([MaHocPhan], [MaLopHocPhan])
REFERENCES [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan])
GO
ALTER TABLE [dbo].[LopHocPhan_SinhVien] CHECK CONSTRAINT [FK_LopHocPhan_SinhVien_LopHocPhan]
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_Lop] FOREIGN KEY([MaLop])
REFERENCES [dbo].[Lop] ([MaLop])
GO
ALTER TABLE [dbo].[SinhVien] CHECK CONSTRAINT [FK_SinhVien_Lop]
GO
/****** Object:  StoredProcedure [dbo].[BoMon_Delete]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[BoMon_Delete](
	@maBoMon nvarchar(20)
)
AS
BEGIN
	DELETE FROM BoMon WHERE MaBoMon = @maBoMon
END
GO
/****** Object:  StoredProcedure [dbo].[BoMon_Insert]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[BoMon_Insert](
	@maBoMon nvarchar(20),
	@tenBoMon nvarchar(100),
	@maKhoa nvarchar(20),
	@truongBoMon nvarchar(10)
)
AS
BEGIN
	INSERT INTO BoMon VALUES(@maBoMon, @tenBoMon, @maKhoa, @truongBoMon)
END
GO
/****** Object:  StoredProcedure [dbo].[BoMon_Update]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[BoMon_Update](
	@maBoMon nvarchar(20),
	@tenBoMon nvarchar(100),
	@maKhoa nvarchar(20),
	@truongBoMon nvarchar(10)
)
AS
BEGIN
	UPDATE BoMon SET TenBoMon = @tenBoMon, MaKhoa = @maKhoa, TruongBoMon = @truongBoMon
	WHERE MaBoMon = @maBoMon
END
GO
/****** Object:  StoredProcedure [dbo].[CTDT_Delete]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CTDT_Delete](
	@maCTDT nvarchar(20)
) AS
BEGIN
	DELETE FROM CTDT_HocPhan WHERE MaCTDT = @maCTDT

	DELETE FROM ChuongTrinhDaoTao WHERE MaCTDT = @maCTDT
END
GO
/****** Object:  StoredProcedure [dbo].[CTDT_Insert]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CTDT_Insert](
	@maCTDT nvarchar(20),
	@tenCTDT nvarchar(100),
	@maKhoa nvarchar(10)
) AS
BEGIN
	INSERT INTO ChuongTrinhDaoTao
	VALUES(@maCTDT, @tenCTDT, @maKhoa)
END
GO
/****** Object:  StoredProcedure [dbo].[CTDT_Update]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CTDT_Update](
	@maCTDT nvarchar(20),
	@tenCTDT nvarchar(100),
	@maKhoa nvarchar(10)
) AS
BEGIN
	UPDATE ChuongTrinhDaoTao
	SET TenCTDT = @tenCTDT
	WHERE MaCTDT = @maCTDT
END

GO
/****** Object:  StoredProcedure [dbo].[DIEM_SELECT]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DIEM_SELECT]
AS
BEGIN
	SELECT LopHocPhan.MaHocPhan, LopHocPhan.MaLopHocPhan, SinhVien.MaSinhVien, DiemQuaTrinh, DiemHe4, DiemHeChu, DiemThiKTHP, DiemTKHP, LanHoc, 
			HoDem + ' ' + Ten AS HoTen, Lop.MaLop, MaKhoa, KhoaHoc, TenLopHocPhan, MaGiangVien, NamHoc, HocKy, DotHoc
	FROM 
		LopHocPhan_SinhVien JOIN SinhVien ON SinhVien.MaSinhVien = LopHocPhan_SinhVien.MaSinhVien
		JOIn Lop ON Lop.MaLop = SinhVien.MaLop
		JOIN LopHocPhan ON LopHocPhan.MaHocPhan = LopHocPhan_SinhVien.MaHocPhan
END
GO
/****** Object:  StoredProcedure [dbo].[Diem_Select_With_Condition]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Diem_Select_With_Condition](
	@maKhoa nvarchar(10),
	@khoaHoc nvarchar(10),
	@maLopHp nvarchar(20)
)
AS
BEGIN
	SELECT LopHocPhan.MaHocPhan, LopHocPhan.MaLopHocPhan, SinhVien.MaSinhVien, DiemQuaTrinh, DiemHe4, DiemHeChu, DiemThiKTHP, DiemTKHP, LanHoc, 
			HoDem + ' ' + Ten AS HoTen, b2.MaLop, MaKhoa, KhoaHoc, TenLopHocPhan, MaGiangVien, NamHoc, HocKy, DotHoc, TrongSoDiemQuaTrinh, TrongSoDiemThiKTHP
	FROM 
		(SELECT * FROM LopHocPhan_SinhVien WHERE MaLopHocPhan = @maLopHp) b1 JOIN SinhVien ON SinhVien.MaSinhVien = b1.MaSinhVien
		JOIn (SELECT * FROM Lop WHERE KhoaHoc = @khoaHoc AND MaKhoa = @maKhoa) b2 ON b2.MaLop = SinhVien.MaLop
		JOIN LopHocPhan ON LopHocPhan.MaHocPhan = b1.MaHocPhan
		JOIN HocPhan ON LopHocPhan.MaHocPhan = HocPhan.MaHocPhan
END
GO
/****** Object:  StoredProcedure [dbo].[DIEM_UPDATE]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[DIEM_UPDATE](
	@maHP nvarchar(20),
	@maLopHP nvarchar(20),
	@maSV nvarchar(20),
	@diemTP float,
	@diemThi float,
	@diemKTHP float,
	@diemHe4 float,
	@diemHeChu nvarchar(10)
)
AS
BEGIN
	UPDATE LopHocPhan_SinhVien
	SET DiemQuaTrinh = @diemTP,
		DiemThiKTHP = @diemThi,
		DiemTKHP = @diemKTHP,
		DiemHe4 = @diemHe4,
		DiemHeChu = @diemHeChu
	WHERE MaHocPhan = @maHP AND MaLopHocPhan = @maLopHP AND MaSinhVien = @maSV
END
GO
/****** Object:  StoredProcedure [dbo].[HocPhan_Delete]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[HocPhan_Delete](
	@maHP nvarchar(10)
) AS
BEGIN

	DELETE FROM HocPhan WHERE MaHocPhan = @maHP
END
GO
/****** Object:  StoredProcedure [dbo].[HocPhan_Insert]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HocPhan_Insert](
	@maHP nvarchar(10),
	@tenHP nvarchar(50),
	@maMon nvarchar(10),
	@soTC int,
	@trongSoQT float,
	@trongSoThi float
) AS
BEGIN

	INSERT INTO HocPhan
	VALUES (@maHP, @tenHP, @maMon, @soTC, @trongSoQT, @trongSoThi);
END
GO
/****** Object:  StoredProcedure [dbo].[HocPhan_SelectByMaCTDT]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HocPhan_SelectByMaCTDT](
	@maCTDT nvarchar(30)
)
AS
BEGIN
	SELECT * FROM CTDT_HocPhan JOIN HocPhan ON CTDT_HocPhan.MaHocPhan = HocPhan.MaHocPhan
	WHERE MaCTDT = @maCTDT
END
GO
/****** Object:  StoredProcedure [dbo].[HocPhan_Update]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[HocPhan_Update](
	@maHP nvarchar(10),
	@tenHP nvarchar(50),
	@maMon nvarchar(10),
	@soTC int,
	@trongSoQT float,
	@trongSoThi float
) AS
BEGIN

	UPDATE HocPhan
	SET TenHocPhan = @tenHP, MaBoMon = @maMon, SoTinChi = @soTC, TrongSoDiemQuaTrinh = @trongSoQT, TrongSoDiemThiKTHP = @trongSoThi WHERE MaHocPhan = @maHP
END
GO
/****** Object:  StoredProcedure [dbo].[Khoa_Delete]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Khoa_Delete](
	@maKhoa nvarchar(10)
)
AS
BEGIN
	DELETE FROM Khoa WHERE MaKhoa = @maKhoa
END
GO
/****** Object:  StoredProcedure [dbo].[Khoa_Insert]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Khoa_Insert](
	@maKhoa nvarchar(10),
	@tenKhoa nvarchar(50)
)
AS
BEGIN
	INSERT INTO Khoa VALUES(@maKhoa, @tenKhoa)
END
GO
/****** Object:  StoredProcedure [dbo].[Khoa_Update]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Khoa_Update](
	@maKhoa nvarchar(10),
	@tenKhoa nvarchar(50)
)
AS
BEGIN
	UPDATE Khoa SET TenKhoa = @tenKhoa WHERE MaKhoa = @maKhoa
END
GO
/****** Object:  StoredProcedure [dbo].[LopHP_Delete]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LopHP_Delete](
	@maLopHP nvarchar(30)
) AS
BEGIN
	DELETE FROM LopHocPhan_SinhVien WHERE MaLopHocPhan = @maLopHP

	Delete FROM LopHocPhan WHERE MaLopHocPhan = @maLopHP
END
GO
/****** Object:  StoredProcedure [dbo].[LopHP_Insert]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[LopHP_Insert](
	@maHP nvarchar(20),
	@maLopHP nvarchar(30),
	@tenLopHP nvarchar(100),
	@maGV nvarchar(10),
	@namHoc nvarchar(20),
	@hocKy nvarchar(10)
) AS
BEGIN
	INSERT INTO LopHocPhan VALUES(@maHP, @maLopHP, @tenLopHP, @maGV, @namHoc, @hocKy);
END
GO
/****** Object:  StoredProcedure [dbo].[LopHP_SelectByKhoaAndCTDT]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LopHP_SelectByKhoaAndCTDT](
	@maKhoa nvarchar(20),
	@maCTDT nvarchar(20)
)
AS
BEGIN
	SELECT 
		*
	FROM
		(SELECT * FROM Khoa WHERE MaKhoa = @maKhoa ) b1 JOIN (SELECT * FROM ChuongTrinhDaoTao WHERE MaCTDT = @maCTDT) b2 ON b1.MaKhoa = b2.MaKhoa
		JOIn CTDT_HocPhan ON b2.MaCTDT = CTDT_HocPhan.MaCTDT
		JOIN HocPhan ON CTDT_HocPhan.MaHocPhan = HocPhan.MaHocPhan
END
GO
/****** Object:  StoredProcedure [dbo].[LopHP_SelectByMaHP]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LopHP_SelectByMaHP](
	@maHP nvarchar(20)
)
AS
BEGIN
	SELECT * FROM LopHocPhan WHERE MaHocPhan = @maHP
END
GO
/****** Object:  StoredProcedure [dbo].[LopHP_SV_Add]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LopHP_SV_Add](
	@maHP nvarchar(20),
	@maLopHP nvarchar(20),
	@maSV nvarchar(30)
)
AS BEGIN
	DECLARE @soLan INT;

	SELECT @soLan = ISNULL(MAX(LanHoc), 0) + 1
	FROM LopHocPhan_SinhVien 
	WHERE MaSinhVien = @maSV AND MaHocPhan = @maHP;

	INSERT INTO LopHocPhan_SinhVien
	VALUES (@maHP, @maLopHP, @maSV, 0, 0, 0, 0, 0, @soLan)
END
GO
/****** Object:  StoredProcedure [dbo].[LopHP_SV_Remove]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LopHP_SV_Remove](
	@maHP nvarchar(20),
	@maLopHP nvarchar(20),
	@maSV nvarchar(30)
) AS
BEGIN
	DELETE FROM LopHocPhan_SinhVien
	WHERE MaHocPhan = @maHP AND MaLopHocPhan = @maLopHP AND MaSinhVien = @maSV
END
GO
/****** Object:  StoredProcedure [dbo].[LopHP_Update]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LopHP_Update](
	@maHP nvarchar(20),
	@maLopHP nvarchar(30),
	@tenLopHP nvarchar(100),
	@maGV nvarchar(10),
	@namHoc nvarchar(20),
	@hocKy nvarchar(10)
) AS
BEGIN
	UPDATE LopHocPhan SET TenLopHocPhan = @tenLopHP, MaGiangVien = @maGV, NamHoc = @namHoc, HocKy = @hocKy WHERE MaHocPhan = @maHP AND MaLopHocPhan = @maLopHP
END
GO
/****** Object:  StoredProcedure [dbo].[Lops_By]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Lops_By](
	@maKhoa nvarchar(50),
	@maCTDT nvarchar(50),
	@KhoaHoc nvarchar(5)
)
AS
BEGIN
	SELECT * FROM Lop WHERE MaCTDT = @maCTDT AND MaKhoa = @maKhoa AND KhoaHoc = @KhoaHoc
END
GO
/****** Object:  StoredProcedure [dbo].[Lops_Delete]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Lops_Delete](
	@maLop varchar(25)
)
AS
BEGIN
	DELETE FROM Lop WHERE MaLop = @maLop
END
GO
/****** Object:  StoredProcedure [dbo].[Lops_Insert]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Lops_Insert](
	@maLop varchar(25),
	@tenLop nvarchar(200),
	@maKhoa varchar(10),
	@maCTDT varchar(25),
	@khoaHoc varchar(10)
)
AS
BEGIN
	INSERT INTO Lop VALUES (@maLop, @tenLop, @maKhoa, @maCTDT, @khoaHoc)
END
GO
/****** Object:  StoredProcedure [dbo].[Lops_Update]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Lops_Update](
	@maLop varchar(25),
	@tenLop nvarchar(200),
	@maKhoa varchar(10),
	@maCTDT varchar(25),
	@khoaHoc varchar(10)
)
AS
BEGIN
	Update Lop SET TenLop = @tenLop, MaKhoa = @maKhoa, MaCTDT = @maCTDT, KhoaHoc = @khoaHoc
		WHERE MaLop = @maLop
END
GO
/****** Object:  StoredProcedure [dbo].[SinhVien_ByLopHP]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SinhVien_ByLopHP](
	@maLopHP nvarchar(20)
)
AS
BEGIN
	SELECT * FROM SinhVien 
		JOIN (SELECT MaSinhVien, LanHoc FROM LopHocPhan_SinhVien WHERE MaLopHocPhan = @maLopHP) b
		ON SinhVien.MaSinhVien = b.MaSinhVien
END
GO
/****** Object:  StoredProcedure [dbo].[SinhViens_Delete]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SinhViens_Delete](
	@maSV varchar(10)
)
AS BEGIN
	DELETE FROM SinhVien WHERE MaSinhVien = @maSV
END
GO
/****** Object:  StoredProcedure [dbo].[SinhViens_Insert]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SinhViens_Insert](
	@maSV varchar(10),
	@hoDem nvarchar(35),
	@ten nvarchar(35),
	@maLop varchar(25),
	@ngaySinh date,
	@diaChi nvarchar(250),
	@dienThoai varchar(15),
	@email varchar(50)
)
AS
BEGIN
	INSERT INTO SinhVien
	VALUES(@maSV, @hoDem, @ten, @maLop, @ngaySinh, @diaChi, @dienThoai, @email)
END
GO
/****** Object:  StoredProcedure [dbo].[SinhViens_Update]    Script Date: 11/25/2024 4:59:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SinhViens_Update](
	@maSV varchar(10),
	@hoDem nvarchar(35),
	@ten nvarchar(35),
	@ngaySinh date,
	@diaChi nvarchar(250),
	@dienThoai varchar(15),
	@email varchar(50)
)
AS
BEGIN
	UPDATE SinhVien
	SET HoDem = @hoDem, Ten = @ten, NgaySinh = @ngaySinh, DiaChi = @diaChi, DienThoai = @dienThoai, Email = @email
	WHERE MaSinhVien = @maSV
END
GO
