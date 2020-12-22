CREATE DATABASE CSDL
Go
USE CSDL
GO
CREATE TABLE PhanQuyen(
MaPQ INT IDENTITY PRIMARY KEY,
LoaiQuyen INT NOT NULL,
TenPQ NVARCHAR(50) NOT NULL,
)
GO

CREATE TABLE NhanVien(
MaNV INT IDENTITY PRIMARY KEY,
HoTen NVARCHAR(50) NOT NULL,
NgaySinh DATE NOT NULL CHECK (NgaySinh < GetDate()),
GioiTinh NVARCHAR(10) NOT NULL CHECK(GioiTinh IN ( 'Nam', 'Nu', 'Khac')),
DiaChi NVARCHAR(200) NOT NULL,
SDT NVARCHAR(20) NOT NULL,
MaPQ INT FOREIGN KEY REFERENCES dbo.PhanQuyen(MaPQ),
TenTK NVARCHAR(20) UNIQUE NOT NULL,
MK NVARCHAR(20) DEFAULT N'1' NOT NULL,
Chan NVARCHAR(2) NOT NULL,
)
GO
CREATE TABLE NSX(
MaNSX INT IDENTITY PRIMARY KEY,
TenNSX NVARCHAR(50) NOT NULL,
DiaChi NVARCHAR(200) NOT NULL,
)
GO

CREATE TABLE HangHoa(
MaSP INT IDENTITY PRIMARY KEY,
MaNSX INT FOREIGN KEY REFERENCES NSX(MaNSX) ON DELETE CASCADE ,
TenSP NVARCHAR(50) NOT NULL,
CPU NVARCHAR(100) NOT NULL,
RAM NVARCHAR(50) NOT NULL,
ManHinh NVARCHAR(50) NOT NULL,
PIN NVARCHAR(20) NOT NULL,
GiaBan INT NOT NULL CHECK(GiaBan > 0),
GiaGoc INT NOT NULL CHECK(GiaGoc > 0),
SoLuong INT NOT NULL CHECK(SoLuong >= 0),
)
GO

CREATE TABLE HangHoaImage(
MaSP INT FOREIGN KEY REFERENCES HangHoa(MaSP),
Hinh  VARBINARY(MAX) NOT NULL,
)
GO

CREATE TABLE DonNhap(
MaGiaoDich INT IDENTITY PRIMARY KEY,
ThoiGian DATE DEFAULT GETDATE(),
TongTien INT NOT NULL CHECK(TongTien > 0),
ViTri NVARCHAR(1000)
)
GO

CREATE TABLE Nhap(
MaGiaoDich INT NOT NULL REFERENCES dbo.DonNhap(MaGiaoDich) ON DELETE CASCADE,
MaSP INT NOT NULL REFERENCES dbo.HangHoa(MaSP) ON DELETE CASCADE,
SoLuong INT NOT NULL CHECK(SoLuong > 0),
PRIMARY KEY(MaGiaoDich, MaSP)
)

CREATE TABLE PT_ThanhToan(
MaPTTT INT IDENTITY PRIMARY KEY,
TenPTTT NVARCHAR(50) NOT NULL,
)
GO

CREATE TABLE KhachHang(
MaKH INT IDENTITY PRIMARY KEY,
HoTen CHAR(50) NOT NULL,
GioiTinh CHAR(10) NULL CHECK(GioiTinh IN ( 'Nam', 'Nu', 'Khac')),
DiaChi CHAR(200) NOT NULL,
SDT CHAR(15) NOT NULL,
)
GO
CREATE TABLE KhuyenMai(
MaKhuyenMai INT IDENTITY PRIMARY KEY,
GiaTriKhuyenMai INT NOT NULL CHECK(GiaTriKhuyenMai > 0),
NgayBatDau DATE NOT NULL DEFAULT GETDATE(),
NgayKetThuc DATE NOT NULL CHECK (NgayKetThuc > GETDATE()),
)
GO

CREATE TABLE Bill(
MaBill INT IDENTITY PRIMARY KEY,
TongTien INT DEFAULT 0 CHECK(TongTien > 0),
ThoiGian DATE DEFAULT GETDATE(),
MaKH INT NOT NULL,
MaPTTT INT NOT NULL,
MaKhuyenMai INT NULL,
DiaChi NVARCHAR(200) NOT NULL,
MaNV INT FOREIGN KEY REFERENCES dbo.NhanVien(MaNV) ON DELETE CASCADE,
FOREIGN KEY(MaKH) REFERENCES dbo.KhachHang(MaKH) ON DELETE CASCADE,
FOREIGN KEY(MaPTTT) REFERENCES dbo.PT_ThanhToan(MaPTTT) ON DELETE CASCADE,
FOREIGN KEY(MaKhuyenMai) REFERENCES dbo.KhuyenMai(MaKhuyenMai) ON DELETE CASCADE,
)
GO

CREATE TABLE Bill_info(
MaBill INT REFERENCES dbo.Bill(MaBill),
MaSP INT REFERENCES dbo.HangHoa(MaSP),
SoLuong INT NULL CHECK(SoLuong > 0),
PRIMARY KEY(MaBill, MaSP)
)
GO
----------------------------------------------
GO
CREATE PROCEDURE AddNSX(								@TenNSX NVARCHAR(50) ,
								@DiaChi NVARCHAR(200))
AS
	INSERT INTO dbo.NSX
	VALUES  (@TenNSX,@DiaChi)
----------------------------------------------
GO
CREATE proc ChangeNSX(@MaNSX int, @TenNSX nvarchar(50), @Diachi nvarchar(200))
as	update NSX
	set TenNSX = @TenNSX,
		DiaChi = @Diachi
	where MaNSX=@MaNSX
----------------------------------------------	
GO
CREATE PROCEDURE AddHangHoa(
						@MaNSX INT,
						@TenSP NVARCHAR(50),
						@CPU NVARCHAR(100),
						@RAM NVARCHAR(50),
						@ManHinh NVARCHAR(50),
						@PIN NVARCHAR(20),
						@GiaBan INT,
						@GiaGoc INT,
						@SoLuong INT
					 )
AS
	INSERT INTO dbo.HangHoa
	VALUES  ( @MaNSX, @TenSP, @CPU, @RAM, @ManHinh, @PIN, @GiaBan, @GiaGoc, @SoLuong) 

-----------------------------------------------
GO 
CREATE PROCEDURE ChangeHangHoa(	@MaSP INT, @MaNSX INT, @tenSP NVARCHAR(50), @CPU NVARCHAR(100), @RAM NVARCHAR(50), @ManHinh NVARCHAR(50),
							@PIN NVARCHAR(20), @GiaBan INT, @GiaGoc INT, @SoLuong INT)
AS	UPDATE	dbo.HangHoa
	SET		MaNSX=@MaNSX,
			TenSP=@tenSP,
			CPU=@CPU,
			RAM=@RAM,
			ManHinh=@ManHinh,
			PIN=@PIN,
			GiaBan=@GiaBan,
			GiaGoc=@GiaGoc,
			SoLuong=@SoLuong
	WHERE	MaSP=@MaSP

----------------------------------------------

GO
CREATE PROCEDURE AddNhanVien(
						@HoTen NVARCHAR(50),
						@NgaySinh DATE,
						@GioiTinh NVARCHAR(10),
						@DiaChi NVARCHAR(200),
						@SDT NVARCHAR(20),
						@ChucVu NVARCHAR(50),
						@TenTK NVARCHAR(20),
						@MK NVARCHAR(20),
						@Chan NVARCHAR(2) )
AS
	INSERT INTO dbo.NhanVien
	VALUES  (@HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @ChucVu, @TenTK, @MK, @Chan)
--------------------------------------------
go
	 
---------------------------------------------
GO
CREATE PROCEDURE ChangeBillInfo(@MaBill INT,
								@MaSP INT,
								@SoLuong INT)
AS	UPDATE	dbo.Bill_info
	SET		SoLuong=@SoLuong
	WHERE	MaBill=@MaBill
	AND		MaSP=@MaSP
----------------------------------------------
GO
CREATE PROCEDURE AddBillInfo(	@MaBill INT,
								@MaSP INT,
								@SoLuong INT)
AS
	INSERT INTO dbo.Bill_info
	VALUES  (@MaBill, @MaSP, @SoLuong)  
----------------------------------------------
GO
CREATE PROCEDURE AddBill(	@MaNV INT,@MaBill INT,
					@TongTien INT,
					@NgayDatHang DATE,
					@MaKH INT,
					@MaPTTT INT,
					@MaKM INT,
					@DiaChiGiaoHang NVARCHAR(200))
AS
	INSERT INTO dbo.Bill
	        ( MaBill ,
	          TongTien ,
	          ThoiGian ,
	          MaKH ,
	          MaPTTT ,
	          MaKhuyenMai ,
	          DiaChi ,
	          MaNV
	        )
	VALUES  ( @MaBill , -- MaBill - int
	          @TongTien, -- TongTien - int
	          @NgayDatHang , -- ThoiGian - date
	          @MaKH, -- MaKH - int
	          @MaPTTT, -- MaPTTT - int
	          @MaKM, -- MaKhuyenMai - int
	          @DiaChiGiaoHang , -- DiaChi - nvarchar(200)
	          @MaNV -- MaNV - int
	        )
	
go

---------------------------------------------
Exec ChangeNSX @MaNSX = 2, @TenNSX = N'vn', @DiaChi = N'tq'