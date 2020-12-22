CREATE DATABASE CSDL
Go
USE CSDL
GO
/*
Tao Bảng
*/
/*
fn_MaHoaMD5
*/
----------------------------------------------
GO
CREATE FUNCTION [dbo].fn_MaHoaMD5 ( @strInput NVARCHAR(4000) ) 
RETURNS NVARCHAR(4000)
AS 
BEGIN 
RETURN (SELECT CONVERT(VARCHAR(32), HashBytes('MD5',@strInput ), 2) as md5)
END
---------------------------------------------
/*
Doi Chu Hoa Sang Chu Thuong
*/
----------------------------------------------
GO
CREATE FUNCTION [dbo].fn_HoaSangThuong ( @strInput NVARCHAR(4000) ) 
RETURNS NVARCHAR(4000)
AS 
BEGIN 
	IF @strInput IS NULL
		 RETURN @strInput 
	IF @strInput = '' 
		RETURN @strInput 
	
	DECLARE @RT NVARCHAR(4000) 
	DECLARE @SIGN_CHARS NCHAR(136)
	DECLARE @UNSIGN_CHARS NCHAR (136) 
	
	SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý Ă ĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208)
	SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' 
	
	DECLARE @COUNTER int 
	DECLARE @COUNTER1 int 
	
	SET @COUNTER = 1 
	WHILE (@COUNTER <=LEN(@strInput))
		 BEGIN 
			SET @COUNTER1 = 1 
			WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) 
			BEGIN 
				IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) 
					BEGIN 
						IF @COUNTER=1 
							SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
						ELSE 
							SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) 
							BREAK 
					END 
				SET @COUNTER1 = @COUNTER1 +1 
			END 
			SET @COUNTER = @COUNTER +1 
		END 
	SET @strInput = replace(@strInput,' ','-') 
	RETURN 	@strInput 
END
--------------------------------------------------
/*
Table_NhanVien
*/
--------------------------------------------------

GO
CREATE TABLE NhanVien(
MaNV INT     PRIMARY KEY,
HoTen NVARCHAR(50) NOT NULL,
NgaySinh DATE NOT NULL CHECK (NgaySinh < GetDate()),
GioiTinh NVARCHAR(4) NOT NULL CHECK(GioiTinh IN ( 'Nam', 'Nu', 'Khac')),
DiaChi NVARCHAR(200) NOT NULL,

SDT VARCHAR(20) NOT NULL,
CONSTRAINT chk_SDT CHECK (SDT not like '%[^0-9]%'),

TenPQ VARCHAR(50) NOT NULL DEFAULT 'Emp' ,
TenTK VARCHAR(50) NOT NULL, -- Triger
MK NVARCHAR(50) DEFAULT dbo.fn_MaHoaMD5('1') NOT NULL,
Chan INT NOT NULL CHECK(Chan IN (0,1)) DEFAULT 0,
)
GO
--------------------------------------------------
/*
Table_NSX
*/
--------------------------------------------------
CREATE TABLE NSX(
MaNSX INT    PRIMARY KEY,
TenNSX NVARCHAR(50) UNIQUE NOT NULL,
DiaChi NVARCHAR(200) NOT NULL,
)
GO
--------------------------------------------------
/*
Table_HangHoa
*/
-------------------------------------------------
CREATE TABLE HangHoa(
MaSP INT    PRIMARY KEY,
MaNSX INT FOREIGN KEY REFERENCES NSX(MaNSX) ON DELETE CASCADE ,
TenSP NVARCHAR(50) NOT NULL,
CPU NVARCHAR(50) NOT NULL,
RAM NVARCHAR(50) NOT NULL,
ManHinh NVARCHAR(50) NOT NULL,
PIN NVARCHAR(50) NOT NULL,
GiaBan INT NOT NULL CHECK(GiaBan >= 0),
GiaGoc INT NOT NULL CHECK(GiaGoc >= 0),
SoLuong INT NOT NULL CHECK(SoLuong >= 0),
Hinh  VARBINARY(MAX) NULL,
)
GO
--------------------------------------------------
/*
Table_DonNhap
*/
-------------------------------------------------
CREATE TABLE DonNhap(
MaDonNhap INT    PRIMARY KEY,
ThoiGian DATE DEFAULT GETDATE(),
TongTien INT NOT NULL CHECK(TongTien >= 0) DEFAULT 0,
DiaChi NVARCHAR(200) NOT NULL,
)
GO

--------------------------------------------------
/*
Table_DonNhapInfo
*/
-------------------------------------------------
GO
CREATE TABLE DonNhap_Info(
MaDonNhap INT NOT NULL REFERENCES dbo.DonNhap(MaDonNhap) ON DELETE CASCADE,
MaSP INT NOT NULL REFERENCES dbo.HangHoa(MaSP) ON DELETE CASCADE,
SoLuong INT NOT NULL CHECK(SoLuong >= 0),
PRIMARY KEY(MaDonNhap, MaSP)
)
--------------------------------------------------
/*
Table_PT_ThanhToan
*/
-------------------------------------------------
GO
CREATE TABLE PT_ThanhToan(
MaPTTT INT    PRIMARY KEY,
TenPTTT NVARCHAR(50) NOT NULL,
)
GO
--------------------------------------------------
/*
Table_KhachHang
*/
-------------------------------------------------
CREATE TABLE KhachHang(
MaKH INT    PRIMARY KEY,
HoTen NVARCHAR(50) NOT NULL,
GioiTinh NVARCHAR(4) NOT NULL CHECK(GioiTinh IN ( 'Nam', 'Nu', 'Khac')),
DiaChi NVARCHAR(200) NOT NULL,
SDT VARCHAR(20) NOT NULL,
CONSTRAINT CHK_SDT_KhachHang CHECK (SDT not like '%[^0-9]%')
)
GO
CREATE TABLE KhuyenMai(
MaKhuyenMai INT    PRIMARY KEY,
GiaTriKhuyenMai INT NOT NULL CHECK(GiaTriKhuyenMai > 0),
NgayBatDau DATE NOT NULL DEFAULT GETDATE(),
NgayKetThuc DATE NOT NULL CHECK (NgayKetThuc > GETDATE()),
)
GO
-------------------------------------------.
CREATE TABLE Bill(
MaBill INT     PRIMARY KEY,
TongTien INT DEFAULT 0 CHECK(TongTien >= 0),
ThoiGian DATE DEFAULT GETDATE(),
MaKH INT NOT NULL,
MaPTTT INT NOT NULL,
MaKhuyenMai INT NULL,
MaNV INT NOT NULL,

FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE CASCADE,
FOREIGN KEY(MaKH) REFERENCES dbo.KhachHang(MaKH) ON DELETE CASCADE,
FOREIGN KEY(MaPTTT) REFERENCES dbo.PT_ThanhToan(MaPTTT) ON DELETE CASCADE,
FOREIGN KEY(MaKhuyenMai) REFERENCES dbo.KhuyenMai(MaKhuyenMai) ON DELETE CASCADE,
)
GO

CREATE TABLE Bill_info(
MaBill INT REFERENCES dbo.Bill(MaBill),
MaSP INT REFERENCES dbo.HangHoa(MaSP),
SoLuong INT NOT NULL CHECK(SoLuong > 0) DEFAULT 1,
PRIMARY KEY(MaBill, MaSP)
)
GO

-------------------------------------------
/*
1.
Tr_InsertBill
*/
-------------------------------------------
GO
create trigger Trg_InsertBill on dbo.Bill after insert as
BEGIN TRANSACTION;
	UPDATE Bill
	SET ThoiGian = GETDATE() 
	FROM dbo.Bill inner join inserted
	on Inserted.MaBill = Bill.MaBill
COMMIT TRANSACTION;
GO
----------------------------------------------
/*
Tao Truy Van 
-
-
-
-sp_CONFIGURE 'show advanced', 1
GO
RECONFIGURE
GO
sp_CONFIGURE 'Database Mail XPs', 1
GO
RECONFIGURE
GO
*/

-------------------------------------------
/*
1.
Tr_InsertDonNhap
*/
-------------------------------------------
GO
create trigger Trg_InsertDonNhap on DonNhap after insert as
BEGIN TRANSACTION;
	UPDATE DonNhap
	SET ThoiGian = GETDATE() 
	FROM DonNhap inner join inserted
	on DonNhap.MaDonNhap = inserted.MaDonNhap
COMMIT TRANSACTION;
-------------------------------------------
/*
1.
Tr_InsertBill
*/
-------------------------------------------
GO
CREATE TRIGGER [dbo].[tr_KiemTraNgayKetThucKhuyenMai]
ON [dbo].KhuyenMai
FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @NgayBatDau DATE, @NgayKetThuc DATE, @MaKhuyenMai INT;
    SELECT @NgayBatDau=Inserted.NgayBatDau, @NgayKetThuc=Inserted.NgayKetThuc,@MaKhuyenMai=Inserted.MaKhuyenMai
    FROM Inserted;

	IF (DATEDIFF(DAY,@NgayBatDau,@NgayKetThuc)>365)
		BEGIN
			RAISERROR('Thoi Gian Khuyen Mai Qua Gio Han', 16, 1);
			ROLLBACK;
		END
    IF(@NgayKetThuc<@NgayBatDau)BEGIN
        RAISERROR('Ngay KEt Thuc Phai Lon Hon Ngay Bat Dau', 16, 1);
		ROLLBACK;
    END;
	IF ((SELECT COUNT(*)
    FROM dbo.KhuyenMai
    WHERE 
	((@NgayBatDau>=NgayBatDau AND @NgayBatDau<=NgayKetThuc)
		OR(@NgayKetThuc>=NgayBatDau AND @NgayKetThuc<=NgayKetThuc))) > 1)
	BEGIN
	    RAISERROR('Khong Duoc Them Trung', 16, 1);
        ROLLBACK;
	END
END;
GO
---------------------------------------------
/*
IndexNhanVien(HoTen)
*/
----------------------------------------------
Go
CREATE INDEX IndexNhanVien ON dbo.NhanVien(HoTen)
---------------------------------------------
/*
IndexhSanPham(TenSP)
*/
----------------------------------------------
GO
CREATE INDEX IndexSanPham ON dbo.HangHoa(TenSP)
GO
CREATE INDEX IndexNSX ON dbo.NSX(MaNSX)
---------------------------------------------

---------------------------------------------
/*
1.
Pr_IsAccountExists
*/
--------------------------------------------
GO
CREATE FUNCTION fn_IsAccountExists (@tenTK NVARCHAR(50))
RETURNS BIT
BEGIN
	DECLARE @tmp INT, @result BIT
	SELECT @tmp=COUNT(*)
	FROM dbo.NhanVien
	WHERE dbo.NhanVien.TenTK= @tenTK 
	IF @tmp>0
		SET @result=1
	ELSE
		SET @result=0
	RETURN @result
END
---------------------------------------------
/*
1.2
V_LIST_Account
*/
---------------------------------------------
GO
CREATE VIEW V_LIST_Account
AS(	SELECT *
	FROM dbo.NhanVien)
---------------------------------------------
/*
1.3
Pro_KhuyenMaiHienTai
*/
--------------------------------------------- 
GO
CREATE PROC sp_KhuyenMaiHienTai
AS
BEGIN
	SELECT * FROM dbo.KhuyenMai WHERE GETDATE() >=NgayBatDau AND GETDATE() <=NgayKetThuc
END
GO
---------------------------------------------
/*
1.3
sp_GetDonNhapInfoByMaDonNhap
*/
---------------------------------------------
GO
CREATE PROC sp_GetDonNhapInfoByMaDonNhap @MaDonNhap INT
AS(	SELECT dbo.DonNhap_Info.MaDonNhap AS MaDonNhap ,DonNhap_Info.MaSP AS MaSP ,TenSP,DonNhap_Info.SoLuong AS SoLuong
	FROM dbo.DonNhap_Info LEFT JOIN dbo.HangHoa ON HangHoa.MaSP = DonNhap_Info.MaSP WHERE MaDonNhap =@MaDonNhap)
---------------------------------------------
/*
1.3
sp_BillInfo
*/
---------------------------------------------
GO
CREATE PROC sp_GetBillInfoByMaBill @MaBill INT
AS(	SELECT dbo.Bill_info.MaBill AS MaBill ,Bill_info.MaSP AS MaSP ,TenSP,Bill_info.SoLuong AS SoLuong
	FROM dbo.Bill_info LEFT JOIN dbo.HangHoa ON HangHoa.MaSP = dbo.Bill_info.MaSP WHERE MaBill =@MaBill)
---------------------------------------------
/*
1.3
sp_BillInfo
*/
---------------------------------------------
GO
CREATE PROC sp_GetBillInfoByMaBillMax  
AS
BEGIN
	DECLARE @MaBill INT
	SELECT @MaBill = MAX(MaBill) FROM dbo.Bill 
	SELECT dbo.Bill_info.MaBill AS MaBill ,Bill_info.MaSP AS MaSP ,TenSP,Bill_info.SoLuong AS SoLuong
		FROM dbo.Bill_info LEFT JOIN dbo.HangHoa ON HangHoa.MaSP = dbo.Bill_info.MaSP WHERE MaBill =@MaBill
END
---------------------------------------------
/*
1.3
V_LIST_Laptop
*/
---------------------------------------------
GO
CREATE VIEW V_LIST_Laptop
AS(	SELECT *
	FROM dbo.HangHoa)
---------------------------------------------
/*
1.
V_LIST_PhuongThucThanhToan
*/
---------------------------------------------
GO
CREATE VIEW V_LIST_PhuongThucThanhToan
AS( SELECT*
	FROM dbo.PT_ThanhToan
	)
	---------------------------------------------
/*
1.
fn_TimKiemNSXByMaNSX
*/
---------------------------------------------
GO
CREATE FUNCTION fn_TimKiemNSXByMaNSX(@MaNSX INT)
RETURNS TABLE
AS RETURN
 (SELECT * FROM NSX WHERE MaNSX = @MaNSX)
 GO

---------------------------------------------
/*
1.
fn_SearchTongChiKhachHang
*/
---------------------------------------------
GO
CREATE FUNCTION fn_SearchTongChiKhachHang (@MaKH INT)
RETURNS INT
AS
BEGIN
	DECLARE @Tong INT 

	SELECT @Tong = SUM(TongTien)
	FROM dbo.Bill LEFT JOIN dbo.KhachHang 
	ON KhachHang.MaKH = Bill.MaKH
	GROUP BY dbo.Bill.MaKH 
	HAVING  dbo.Bill.MaKH = @MaKH

	RETURN @Tong
END
/*
1.
sp_GetSPByMaSP
*/
---------------------------------------------
GO
CREATE PROC sp_GetSPByMaSP (@MaSP INT)
AS BEGIN
	(SELECT * FROM HangHoa WHERE HangHoa.MaSP = @MaSP)
End
---------------------------------------------
/*
1.
sp_BanChay
*/
-------------------------------------------------------------------------------------------------------------********
GO
CREATE PROC sp_Ban (@Ban INT)
AS
BEGIN
	IF (@Ban=1)
	BEGIN
		SELECT HangHoa.MaSP, MaNSX, TenSP, CPU, RAM, ManHinh, PIN, GiaBan, GiaGoc, HangHoa.SoLuong, Hinh
		FROM (Bill join Bill_info ON Bill.MaBill = Bill_info.MaBill) 
		JOIN HangHoa ON Bill_info.MaSP = HangHoa.MaSP
		WHERE DATEDIFF(day, ThoiGian, GETDATE()) <= 30
		GROUP BY HangHoa.MaSP, MaNSX, TenSP, CPU, RAM, ManHinh, PIN, GiaBan, GiaGoc, HangHoa.SoLuong, Hinh
		ORDER BY Sum(Bill_info.SoLuong) desc
     END
	 ELSE
	 IF (@Ban = -1)
	 BEGIN
		SELECT * FROM dbo.HangHoa
	 END
	 ELSE
	 BEGIN
		SELECT HangHoa.MaSP, MaNSX, TenSP, CPU, RAM, ManHinh, PIN, GiaBan, GiaGoc, HangHoa.SoLuong, Hinh
		FROM (SELECT Bill_info.MaSP, MIN(DATEDIFF(Day,ThoiGian,GETDATE())) AS ThoiGianBanGanNhat
		FROM (Bill join Bill_info ON Bill.MaBill = Bill_info.MaBill) join HangHoa ON Bill_info.MaSP = HangHoa.MaSP
		GROUP BY Bill_info.MaSP, TenSP) AS HaHa, HangHoa
		WHERE ThoiGianBanGanNhat > 90
	 END
END

---------------------------------------------
/*
1.
sp_ListSXSP
*/
---------------------------------------------
GO
CREATE PROC sp_ListSXSP(@Ban INT, @Con INT)
AS
BEGIN
	CREATE TABLE SP_Temp(
	MaSP INT ,
	MaNSX INT  ,
	TenSP NVARCHAR(50) NOT NULL,
	CPU NVARCHAR(50) NOT NULL,
	RAM NVARCHAR(50) NOT NULL,
	ManHinh NVARCHAR(50) NOT NULL,
	PIN NVARCHAR(50) NOT NULL,
	GiaBan INT NOT NULL CHECK(GiaBan >= 0),
	GiaGoc INT NOT NULL CHECK(GiaGoc >= 0),
	SoLuong INT NOT NULL CHECK(SoLuong >= 0),
	Hinh  VARBINARY(MAX) NULL
	)
	INSERT dbo.SP_Temp
	EXEC dbo.sp_Ban @Ban = @Ban

	CREATE TABLE SP_TempCon(
	MaSP INT ,
	MaNSX INT  ,
	TenSP NVARCHAR(50) NOT NULL,
	CPU NVARCHAR(50) NOT NULL,
	RAM NVARCHAR(50) NOT NULL,
	ManHinh NVARCHAR(50) NOT NULL,
	PIN NVARCHAR(50) NOT NULL,
	GiaBan INT NOT NULL CHECK(GiaBan >= 0),
	GiaGoc INT NOT NULL CHECK(GiaGoc >= 0),
	SoLuong INT NOT NULL CHECK(SoLuong >= 0),
	Hinh  VARBINARY(MAX) NULL
	)

	INSERT dbo.SP_TempCon
	EXEC sp_CountProductTonTai @Con = @Con

	SELECT * FROM dbo.SP_Temp 
	UNION 
	SELECT * FROM dbo.SP_TempCon 

END

---------------------------------------------
/*
1.
fn_SoLuongBan
*/
---------------------------------------------
GO
CREATE function fn_SoLuongBan(@MaSp int)
returns int
as
begin
	declare @SoLuong INT = 0
	select @SoLuong = sum(Bill_info.SoLuong)
	FROM (Bill join Bill_info ON Bill.MaBill = Bill_info.MaBill) join HangHoa ON Bill_info.MaSP = HangHoa.MaSP
	WHERE Bill_info.MaSP = @MaSp and DATEDIFF(day, ThoiGian, GETDATE()) <= 30
	GROUP BY Bill_info.MaSP
	return @SoLuong
end
go
---------------------------------------------
/*
1.
V_LIST_KhachHang
*/
---------------------------------------------
GO
CREATE VIEW V_LIST_KhachHang
AS( SELECT dbo.fn_SearchTongChiKhachHang(dbo.KhachHang.MaKH) AS TongTien,* 
	FROM dbo.KhachHang
	)
---------------------------------------------
/*
1.
V_ListNSX
*/
---------------------------------------------
GO
CREATE VIEW V_List_NSX
AS	SELECT *
	FROM dbo.NSX
---------------------------------------------
/*
1.
Fn_GetAccountByHoTen
*/
---------------------------------------------
GO
CREATE FUNCTION fn_GetAccountByHoTen (@HoTen NVARCHAR(50))
RETURNS TABLE
RETURN(	
		SELECT * FROM dbo.NhanVien 
		WHERE dbo.fn_HoaSangThuong(dbo.NhanVien.HoTen)
		LIKE (N'%' + dbo.fn_HoaSangThuong(@HoTen) + '%')
)
GO
---------------------------------------------
GO
CREATE FUNCTION fn_DanhSachNV(@TenNV NVARCHAR(50),@Khoa INT, @Quyen VARCHAR(50) )
RETURNS TABLE
AS 
RETURN
	SELECT * FROM dbo.fn_GetAccountByHoTen(@TenNV)
	UNION 
	SELECT * FROM V_LIST_Account WHERE Chan =@Khoa OR @Khoa = -1
	UNION 
	SELECT * FROM NhanVien WHERE TenPQ= @Quyen OR @Quyen  = 'ALL'
------------------------------------------------
/*
1.
ChangeHangHoa
*/
---------------------------------------------

GO
CREATE PROCEDURE sp_ChangeHangHoa(	@MaSP INT, @MaNSX INT, @tenSP NVARCHAR(50), @CPU NVARCHAR(100), @RAM NVARCHAR(50), @ManHinh NVARCHAR(50),
							@PIN NVARCHAR(20), @GiaBan INT, @GiaGoc INT, @SoLuong INT)
AS
BEGIN TRAN
		UPDATE	dbo.HangHoa
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
		COMMIT


----------------------------------------------
/*
1.
Pro_AddHangHoa
*/
---------------------------------------------
GO
CREATE PROCEDURE sp_AddHangHoa(
						@MaSP INT,
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
BEGIN TRAN
	IF (EXISTS (SELECT* FROM dbo.HangHoa WHERE MaSP=@MaSP))
		BEGIN
			EXEC dbo.sp_ChangeHangHoa	@MaSP ,
									@MaNSX,
									@TenSP,
									@CPU ,
									@RAM ,
									@ManHinh,
									@PIN , 
									@GiaBan,
									@GiaGoc , 
									@SoLuong 
		END
	ELSE
		BEGIN		
		
			INSERT dbo.HangHoa
			        ( MaSP ,
			          MaNSX ,
			          TenSP ,
			          CPU ,
			          RAM ,
			          ManHinh ,
			          PIN ,
			          GiaBan ,
			          GiaGoc ,
			          SoLuong 
			          )
			VALUES  ( @MaSP,@MaNSX,@TenSP, @CPU, @RAM, @ManHinh, @PIN, @GiaBan, @GiaGoc, @SoLuong) 
		END
	IF(EXISTS(SELECT * FROM dbo.HangHoa WHERE MaSP=@MaSP))
		BEGIN
			COMMIT;
		END
	ELSE
		BEGIN
			ROLLBACK;
		END
---------------------------------------------
/*
1.
Trg_TenTKUniQue
*/
---------------------------------------------
GO
CREATE TRIGGER Tr_TenTKUnique ON dbo.NhanVien 
FOR INSERT
AS
BEGIN
	IF (EXISTS(SELECT * FROM Inserted WHERE dbo.fn_IsAccountExists(Inserted.TenTK) >= 1))
		ROLLBACK;
END

---------------------------------------------
/*
1.
TR_D_GioHang
*/
---------------------------------------------
GO
Create trigger TR_D_GioHang
On dbo.Bill
For Delete
As
Begin
	DELETE Bill_info
	FROM Deleted inner JOIN Bill_info
	ON Bill_info.MaBill = deleted.MaBill
END

/*
1.
Fn_GetKhachHangtByHoTen
*/
---------------------------------------------
GO
CREATE FUNCTION fn_GetKhachHangByHoTen (@HoTen NVARCHAR(50))
RETURNS TABLE AS RETURN 
		SELECT dbo.fn_SearchTongChiKhachHang(KhachHang.MaKH) AS TongTien, * FROM dbo.KhachHang 
		WHERE dbo.fn_HoaSangThuong(dbo.KhachHang.HoTen) LIKE (N'%' + dbo.fn_HoaSangThuong(@HoTen) + '%')

GO

---------------------------------------------
/*
1.
Fn_GetLapByTenSP
*/
---------------------------------------------
GO
CREATE FUNCTION fn_GetLapByTenSP (@TenSP INT)
RETURNS TABLE
RETURN(	SELECT *
		FROM dbo.HangHoa
		WHERE dbo.fn_HoaSangThuong(dbo.HangHoa.TenSP)
		LIKE (N'%' + dbo.fn_HoaSangThuong(@TenSP) + '%')
)
/*
1.
sp_CountProductTonTai
*/
---------------------------------------------
GO
CREATE PROC sp_CountProductTonTai(@tontai INT)
AS
	BEGIN

	IF (@tontai = 1)
		BEGIN
			
			SELECT *
			FROM dbo.HangHoa WHERE SoLuong >0	
			
		END
	IF (@tontai = -1)
		BEGIN	
			SELECT * FROM dbo.HangHoa
		END
	ELSE
		BEGIN
			SELECT *
			FROM dbo.HangHoa WHERE SoLuong =0	
			
		END
	
	END
--------------------------------------------
/*
1.
Pr_IsLoginSuccess
*/
--------------------------------------------

GO
CREATE FUNCTION fn_IsLoginSuccess(@tenTK VARCHAR(50),@MK NVARCHAR(50))
RETURNS BIT
BEGIN
	DECLARE @result BIT
	IF EXISTS (	SELECT * 
				FROM	dbo.NhanVien
				WHERE	dbo.NhanVien.TenTK=@tenTK 
				AND		dbo.NhanVien.MK =dbo.fn_MaHoaMD5(@MK) AND Chan = 0	
				)
		SET @result = 1
	ELSE
		SET @result = 0		 

	RETURN @result
END

--------------------------------------------
/*
1.
SP_ChangeThongTinNhanVien
*/
--------------------------------------------

GO
CREATE PROC sp_ChangeThongTinNhanVien(	@MaNV INT,
							@HoTen NVARCHAR(50),
							@NgaySinh DATE,
							@GioiTinh NVARCHAR(4),
							@DiaChi NVARCHAR(200),
							@SDT NVARCHAR(20),
							@TenTK VARCHAR(50)
							)
AS
BEGIN TRAN
	UPDATE dbo.NhanVien
	SET 
		HoTen=@HoTen,
		NgaySinh=@NgaySinh,
		GioiTinh=@GioiTinh,
		DiaChi=@DiaChi,
		SDT=@SDT,
		TenTK = @TenTK
		
	WHERE MaNV=@MaNV
	COMMIT				
--------------------------------------------
/*
1.
SP_ChangeMatKhauNhanVien
*/
--------------------------------------------
GO
CREATE PROC sp_ChangeMatKhauNhanVien(	
							@TenTK VARCHAR(50),
							@MK NVARCHAR(50),
							@MKMoi NVARCHAR(50),
							@MKMoiXacNhan NVARCHAR(50))
AS
BEGIN TRAN
	UPDATE dbo.NhanVien
	SET 
		MK = dbo.fn_MaHoaMD5(@MKMoi) 
	WHERE TenTK = @TenTK  AND MK = (dbo.fn_MaHoaMD5(@MK)) AND @MKMoi = @MKMoiXacNhan
	COMMIT


	--------------------------------------------
/*
1.
SP_ChangeQuyenNhanVien
*/
--------------------------------------------
GO
CREATE PROC sp_ChangeQuyenNhanVien(	
							@MaNV VARCHAR(50),
							@TenPQ NVARCHAR(50)
							)
AS
BEGIN TRAN
	UPDATE dbo.NhanVien
	SET 
		TenPQ =@TenPQ
	WHERE MaNV = @MaNV  
	COMMIT				
--------------------------------------------				
/*
1.
fn_TongHangHoaCuaNSX
*/
--------------------------------------------
GO
CREATE FUNCTION fn_TongHangHoaCuaNSX(@MaNSX INT)
RETURNS INT
AS 
BEGIN
	DECLARE @Tong INT 
	SELECT @TOng = SUM(dbo.HangHoa.SoLuong) FROM dbo.HangHoa JOIN dbo.NSX ON HangHoa.MaNSX = NSX.MaNSX
	WHERE HangHoa.MaNSX = @MaNSX
	GROUP BY dbo.HangHoa.MaNSX 
	
	RETURN @Tong
END
GO
--------------------------------------------				
/*
1.
fn_GiaTriThuongHieuCuaNSX
*/
--------------------------------------------
GO
CREATE FUNCTION fn_GiaTriThuongHieuCuaNSX(@MaNSX INT)
RETURNS INT
AS 
BEGIN
	DECLARE @Tong INT 
	SELECT @TOng = (AVG(GiaBan)) FROM dbo.NSX JOIN dbo.HangHoa ON HangHoa.MaNSX = NSX.MaNSX
	WHERE HangHoa.MaNSX = @MaNSX
	GROUP BY NSX.MaNSX 
	
	RETURN @Tong
END
GO
--------------------------------------------				
/*
1.
sp_SapXepHangHoaCuaNSX
*/
--------------------------------------------
GO
CREATE PROC  sp_SapXepHangHoaTongHangCuaNSX @Tang INT
AS
BEGIN 
	IF (@Tang = 0)
	BEGIN
		SELECT dbo.NSX.MaNSX,dbo.NSX.TenNSX,dbo.NSX.DiaChi FROM dbo.NSX JOIN dbo.HangHoa ON HangHoa.MaNSX = NSX.MaNSX
		GROUP BY NSX.MaNSX,dbo.NSX.TenNSX,dbo.NSX.DiaChi ORDER BY SUM(SoLuong) DESC
	END
	ELSE
	BEGIN
		SELECT dbo.NSX.MaNSX,dbo.NSX.TenNSX,dbo.NSX.DiaChi FROM dbo.NSX JOIN dbo.HangHoa ON HangHoa.MaNSX = NSX.MaNSX
		GROUP BY NSX.MaNSX,dbo.NSX.TenNSX,dbo.NSX.DiaChi ORDER BY SUM(SoLuong) ASC
	END

END
--------------------------------------------				
/*
1.
fn_TongGiaTriThanhToanBangPTTT
*/
--------------------------------------------
GO
CREATE FUNCTION fn_TongGiaTriThanhToanBangPTTT(@MaPTTT INT)
RETURNS INT
AS 
BEGIN
	DECLARE @Tong INT 
	SELECT @Tong = SUM(TongTien) FROM dbo.PT_ThanhToan JOIN dbo.Bill ON Bill.MaPTTT = PT_ThanhToan.MaPTTT 
	WHERE PT_ThanhToan.MaPTTT = @MaPTTT
	
	RETURN @Tong
END
GO
--------------------------------------------				
/*
1.
sp_GiaTriThuongHieu
*/
--------------------------------------------
GO
CREATE PROC  sp_GiaTriThuongHieu (@Tang INT)
AS
BEGIN 
 IF (@Tang=1)
	BEGIN
		select NSX.MaNSX, TenNSX, DiaChi
		FROM NSX JOIN  HangHoa
		ON HangHoa.MaNSX = NSX.MaNSX
		GROUP by NSX.MaNSX, TenNSX,DiaChi
		ORDER BY AVG(GiaBan) ASC
	END
    ELSE
    BEGIN
		select NSX.MaNSX, TenNSX, DiaChi
		FROM NSX JOIN  HangHoa
		ON HangHoa.MaNSX = NSX.MaNSX
		GROUP by NSX.MaNSX, TenNSX,DiaChi
		ORDER BY AVG(GiaBan) DESC
	END
END
--------------------------------------------				
/*
1.
Cho 2 mốc ngày bắt đầu và ngày kết thức Ta xét xem có bao nhiêu thẻ khuyến mãi được sử dụng trong bill
Face: VanTu 
*/
--------------------------------------------
GO
create function fn_SoLuongTheKhuyenMai(@NgayBatDau date, @NgayKetThuc date)
returns table
as return (SELECT COUNT(MaBill) AS SoMaKMDuocDung
			FROM dbo.Bill
			WHERE MaKH != NULL
			and ((month(@NgayBatDau)-3 > 0 and day(ThoiGian) >= day(@NgayBatDau) and month(ThoiGian) = month(@NgayBatDau)-3 and year(ThoiGian) = year(@NgayBatDau))
				or (month(@NgayBatDau)-3 > 0 and month(ThoiGian) > month(@NgayBatDau)-3 and year(ThoiGian) = year(@NgayBatDau))
				or (month(@NgayBatDau)-3 <= 0 and day(ThoiGian) >= day(@NgayBatDau) and month(ThoiGian) = month(@NgayBatDau)+9 and year(ThoiGian) = year(@NgayBatDau)-1)
				or (month(@NgayBatDau)-3 <= 0 and month(ThoiGian) > month(@NgayBatDau)+9 and year(ThoiGian) = year(@NgayBatDau)-1))
			and
				((month(@NgayKetThuc)-3 > 0 and day(ThoiGian) <= day(@NgayKetThuc) and month(ThoiGian) = month(@NgayKetThuc)-3 and year(ThoiGian) = year(@NgayKetThuc))
				or (month(@NgayKetThuc)-3 > 0 and month(ThoiGian) < month(@NgayKetThuc)-3 and year(ThoiGian) = year(@NgayKetThuc))
				or (month(@NgayKetThuc)-3 <= 0 and day(ThoiGian) <= day(@NgayKetThuc) and month(ThoiGian) = month(@NgayKetThuc)+9 and year(ThoiGian) = year(@NgayKetThuc)-1)
				or (month(@NgayKetThuc)-3 <= 0 and month(ThoiGian) < month(@NgayKetThuc)+9 and year(ThoiGian) = year(@NgayKetThuc)-1)))
GO
--------------------------------------------				
/*
1.
Chọn mã khuyến mãi, xuất ra chênh lệch lượt mua và tiền bán giữa khoảng thời gian diễn ra khuyến mãi 
và cùng kỳ quý trước
fn_LoiNhuanKhuyenMai
Face: VanTu
*/
--------------------------------------------
GO
create function fn_LoiNhuanKhuyenMai(@MaKhuyenMai int, @NgayBatDau date, @NgayKetThuc date)
returns table
as return (select (count(QuyNay.MaBill)-count(QuyTruoc.MaBill)) as ChenhLechDoanhSo, (sum(QuyNay.TongTien)-sum(QuyTruoc.TongTien)) as Loi
		  from  (select MaBill, TongTien 
				 from Bill
				 where MaKhuyenMai = @MaKhuyenMai) as QuyNay,
				(select MaBill, TongTien 
				 from Bill
				 where ((month(@NgayBatDau)-3 > 0 and day(ThoiGian) >= day(@NgayBatDau) and month(ThoiGian) = month(@NgayBatDau)-3 and year(ThoiGian) = year(@NgayBatDau))
					or (month(@NgayBatDau)-3 > 0 and month(ThoiGian) > month(@NgayBatDau)-3 and year(ThoiGian) = year(@NgayBatDau))
					or (month(@NgayBatDau)-3 <= 0 and day(ThoiGian) >= day(@NgayBatDau) and month(ThoiGian) = month(@NgayBatDau)+9 and year(ThoiGian) = year(@NgayBatDau)-1)
					or (month(@NgayBatDau)-3 <= 0 and month(ThoiGian) > month(@NgayBatDau)+9 and year(ThoiGian) = year(@NgayBatDau)-1))
					and
					   ((month(@NgayKetThuc)-3 > 0 and day(ThoiGian) <= day(@NgayKetThuc) and month(ThoiGian) = month(@NgayKetThuc)-3 and year(ThoiGian) = year(@NgayKetThuc))
					or (month(@NgayKetThuc)-3 > 0 and month(ThoiGian) < month(@NgayKetThuc)-3 and year(ThoiGian) = year(@NgayKetThuc))
					or (month(@NgayKetThuc)-3 <= 0 and day(ThoiGian) <= day(@NgayKetThuc) and month(ThoiGian) = month(@NgayKetThuc)+9 and year(ThoiGian) = year(@NgayKetThuc)-1)
					or (month(@NgayKetThuc)-3 <= 0 and month(ThoiGian) < month(@NgayKetThuc)+9 and year(ThoiGian) = year(@NgayKetThuc)-1))) as QuyTruoc)
go
--------------------------------------------				
/*
1.
sp_ChangeChanNhanVien
*/
--------------------------------------------
GO
CREATE PROC sp_ChangeChanNhanVien(	@MaNV VARCHAR(50))
AS
BEGIN TRAN
	IF (EXISTS(SELECT * FROM dbo.NhanVien WHERE MaNV = @MaNV))
	UPDATE dbo.NhanVien
	SET 
		Chan = (CASE WHEN Chan = 0 THEN 1 ELSE 0 END)
	COMMIT					
						
/*
1.
sp_AddNhanVien
*/
--------------------------------------------


GO 
CREATE PROCEDURE sp_AddNhanVien(
						@MaNV INT,
						@HoTen NVARCHAR(50),
						@NgaySinh DATE,
						@GioiTinh VARCHAR(4),
						@DiaChi NVARCHAR(200),
						@SDT NVARCHAR(20),
						@TenTK VARCHAR(50)
						)
AS
BEGIN TRAN
	IF( EXISTS( SELECT* FROM dbo.NhanVien WHERE MaNV=@MaNV))
		BEGIN
			EXEC dbo.sp_ChangeThongTinNhanVien @MaNV = @MaNV, -- int
			    @HoTen = @HoTen, -- nvarchar(50)
			    @NgaySinh = @NgaySinh, -- date
			    @GioiTinh = @GioiTinh, -- nvarchar(4)
			    @DiaChi = @DiaChi, -- nvarchar(200)
			    @SDT = @SDT, -- nvarchar(20)
				@TenTK = @TenTK
		END 
	ELSE
		BEGIN
			INSERT INTO dbo.NhanVien
			        ( MaNV ,
		          HoTen ,
		          NgaySinh ,
		          GioiTinh ,
		          DiaChi ,
		          SDT ,
		          TenTK 
			        )
			VALUES  ( 
					@MaNV,
					@HoTen , -- HoTen - nvarchar(50)
			        @NgaySinh , -- NgaySinh - date
			        @GioiTinh , -- GioiTinh - nvarchar(4)
			        @DiaChi , -- DiaChi - nvarchar(200)
			        @SDT , -- SDT - varchar(20)
			        @TenTK  -- TenTK - varchar(50)
			        )
		END
	IF( EXISTS( SELECT * FROM dbo.NhanVien WHERE MaNV=@MaNV))
		BEGIN
			COMMIT;
		END
	ELSE
		BEGIN
			ROLLBACK;
		END
--------------------------------------------
/*
1.
sp_ChangeNSX
*/
--------------------------------------------
GO
CREATE PROC sp_ChangeNSX(@MaNSX int, @TenNSX nvarchar(50), @Diachi nvarchar(200))
AS
BEGIN TRAN
	IF (EXISTS (SELECT * FROM dbo.NSX  WHERE MaNSX =@MaNSX) )
	UPDATE NSX
	SET TenNSX = @TenNSX,
		DiaChi = @Diachi
	WHERE MaNSX=@MaNSX
	COMMIT;
/*
1.
sp_AddNSX
*/
--------------------------------------------
GO
CREATE PROCEDURE sp_AddNSX(
						@MaNSX	INT,
						@TenNSX NVARCHAR(50) ,
						@DiaChi NVARCHAR(200)	)
AS
BEGIN TRAN 
	IF (EXISTS (SELECT* FROM dbo.NSX WHERE MaNSX=@MaNSX))
		BEGIN 
			EXEC dbo.sp_ChangeNSX	@MaNSX,
								@TenNSX,
								@Diachi
		END
    ELSE
		BEGIN
		INSERT dbo.NSX
		        ( MaNSX, TenNSX, DiaChi )
		VALUES  ( @MaNSX, -- MaNSX - int
		          @TenNSX, -- TenNSX - nvarchar(50)
		         @DiaChi  -- DiaChi - nvarchar(200)
		          )
		END
	IF EXISTS(SELECT * FROM dbo.NSX WHERE MaNSX=@MaNSX)
		BEGIN 
			COMMIT;
		END
	ELSE 
		BEGIN
			ROLLBACK;
		END
 
 /*
1.
sp_ChangeImage
*/
---------------------------------------------

GO
CREATE PROCEDURE sp_ChangeImage(@MaSp INT,
							@Image VARBINARY(MAX)  )
AS
	BEGIN TRAN
	IF (EXISTS (SELECT* FROM dbo.HangHoa WHERE MaSP=@MaSp))
		UPDATE dbo.HangHoa
		SET Hinh = @Image
		WHERE MaSP = @MaSp
	COMMIT
---------------------------------------------
 /*
1.
sp_KhachHangChiNhieuNhatvsItNhat
*/
---------------------------------------------
GO
CREATE PROC sp_KhachHangChiNhieuNhatvsItNhat (@Tang INT)
AS 
BEGIN
	IF (@Tang =0) 
		BEGIN
			SELECT SUM(TongTien) AS TongTien, dbo.KhachHang.MaKH,dbo.KhachHang.MaKH,dbo.KhachHang.HoTen,dbo.KhachHang.GioiTinh,dbo.KhachHang.DiaChi,dbo.KhachHang.SDT
			FROM dbo.Bill JOIN dbo.KhachHang 
			ON KhachHang.MaKH = Bill.MaKH 
			GROUP BY Bill.MaKH,dbo.KhachHang.MaKH,dbo.KhachHang.HoTen,dbo.KhachHang.GioiTinh,dbo.KhachHang.DiaChi,dbo.KhachHang.SDT 
			ORDER BY SUM(TongTien) DESC
		END
	ELSE
    IF (@Tang = 1)
		BEGIN
			SELECT SUM(TongTien) AS TongTien, dbo.KhachHang.MaKH,dbo.KhachHang.MaKH,dbo.KhachHang.HoTen,dbo.KhachHang.GioiTinh,dbo.KhachHang.DiaChi,dbo.KhachHang.SDT
			FROM dbo.Bill JOIN dbo.KhachHang 
			ON KhachHang.MaKH = Bill.MaKH 
			GROUP BY Bill.MaKH,dbo.KhachHang.MaKH,dbo.KhachHang.HoTen,dbo.KhachHang.GioiTinh,dbo.KhachHang.DiaChi,dbo.KhachHang.SDT 
			ORDER BY SUM(TongTien) ASC
		END
END
-------------------------------------------- 
 /*
1.
sp_ChangeBillInfo
*/
---------------------------------------------
GO
CREATE PROCEDURE sp_ChangeBillInfo(@MaBill INT,
								@MaSP INT,
								@SoLuong INT)
AS
BEGIN TRAN
	IF( EXISTS( SELECT* FROM dbo.Bill_info WHERE MaBill=@MaBill))
	UPDATE	dbo.Bill_info
	SET		SoLuong=@SoLuong
	WHERE	MaBill=@MaBill
	AND		MaSP=@MaSP
	COMMIT
--------------------------------------------
/*
1.
sp_AddBillInfo
*/
---------------------------------------------
GO
CREATE PROCEDURE sp_AddBillInfo(@MaBill INT,
								@MaSP INT,
								@SoLuong INT)
AS
BEGIN TRAN
		BEGIN	
			INSERT INTO dbo.Bill_info
			VALUES  (@MaBill, @MaSP, @SoLuong)  
		END
	IF(EXISTS( SELECT * FROM dbo.Bill_info WHERE MaBill=@MaBill AND MaSP = @MaSP))
		BEGIN
			COMMIT
		END
	ELSE
		BEGIN
			ROLLBACK
		END

---------------------------------------------
/*
1.
Tr BillInfo SoLuong To HangHoa SoLuong
*/
---------------------------------------------
GO
CREATE TRIGGER Tr_UpdateBillInfo ON dbo.Bill_info AFTER UPDATE, INSERT,DELETE
AS
BEGIN
	DECLARE @MaSP INT  
	DECLARE @SoLuongSau INT
	SET @SoLuongSau = 0
	SELECT @MaSP = Inserted.MaSP, @SoLuongSau = Inserted.SoLuong FROM Inserted
	DECLARE @SoLuongBanDau INT
	SET @SoLuongBanDau  =0
	SELECT @SoLuongBanDau = Deleted.SoLuong FROM Deleted  

	UPDATE dbo.HangHoa SET SoLuong =  SoLuong + @SoLuongBanDau WHERE  MaSP =@MaSP
	UPDATE dbo.HangHoa SET SoLuong =  SoLuong - @SoLuongSau WHERE  MaSP =@MaSP
END 
 /*
1.
sp_LoadTongBill
*/
---------------------------------------------
GO
CREATE PROC sp_LoadTongBill @MaBill INT
AS
BEGIN TRAN
	DECLARE @Tong INT = 0 
	IF( EXISTS( SELECT*FROM dbo.Bill WHERE MaBill=@MaBill))
	BEGIN
		SELECT @Tong = SUM(dbo.Bill_info.SoLuong*dbo.HangHoa.GiaBan) FROM 
		dbo.Bill_info JOIN dbo.HangHoa ON HangHoa.MaSP = Bill_info.MaSP 
		WHERE MaBill = @MaBill
	END

	DECLARE @GiaKhuyenMai INT =0
	SELECT @GiaKhuyenMai =  GiaTriKhuyenMai FROM dbo.Bill JOIN dbo.KhuyenMai ON KhuyenMai.MaKhuyenMai = Bill.MaKhuyenMai
	 WHERE MaBill = @MaBill

	 IF (@Tong > @GiaKhuyenMai)
	 BEGIN
		SET @Tong = @Tong - @GiaKhuyenMai
		END
		ELSE
        SET @Tong = 0

	UPDATE dbo.Bill SET TongTien = @Tong WHERE MaBill = @MaBill
	COMMIT

---------------------------------------------
/*
1.
sp_AddKiemTraBillInfo
*/
---------------------------------------------***
Go
CREATE PROC sp_AddKiemTraBillInfo
@MaBill INT, @MaSP INT, @SoLuong INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT = 0
	DECLARE @CountSp INT = 1
	
	SELECT @isExitsBillInfo = b.MaBill, @CountSp = b.SoLuong 
	FROM dbo.Bill_info AS b 
	WHERE b.MaBill = @MaBill AND b.MaSP = @MaSP

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @CountSp + @SoLuong
		IF (@newCount > 0)
			UPDATE dbo.Bill_info SET SoLuong = @CountSp + @SoLuong WHERE MaSP = @MaSP AND MaBill = @MaBill
		ELSE
			DELETE dbo.Bill_info WHERE MaBill = @MaBill AND MaSP = @MaSP
	END
	ELSE
	BEGIN
		EXEC sp_AddBillInfo @MaBill, @MaSP, @SoLuong
	END

	EXEC dbo.sp_LoadTongBill @MaBill = @MaBill -- int
	
END
GO
---------------------------------------------
 /*
1.
sp_ChangeBill
*/
---------------------------------------------
GO

CREATE PROCEDURE sp_ChangeBill(@MaBill INT,
						@ThoiGian DATE,
						@MaKH INT,
						@MaPTTT INT,
						@MaKhuyenMai INT,
						@MaNV INT)
AS
BEGIN TRAN
	UPDATE dbo.Bill
	SET 
		ThoiGian=@ThoiGian,
		MaKH=@MaKH,
		MaPTTT=@MaPTTT, 
		MaKhuyenMai=@MaKhuyenMai
		
	WHERE MaBill=@MaBill
	COMMIT
---------------------------------------------
 /*
1.
sp_AddBill
*/
---------------------------------------------
GO

CREATE PROCEDURE sp_AddBill(@MaBill INT,
						@ThoiGian DATE,
						@MaKH INT,
						@MaPTTT INT,
						@MaKhuyenMai INT,
						@MaNV INT)
AS
BEGIN TRAN
	IF( EXISTS( SELECT*FROM dbo.Bill WHERE MaBill=@MaBill))
		BEGIN
			EXEC sp_ChangeBill @MaBill,@ThoiGian,@MaKH,@MaPTTT,@MaKhuyenMai,@MaNV
		END
	ELSE
		BEGIN    
			INSERT dbo.Bill
			        ( MaBill ,
					  TongTien ,
			          MaKH ,
			          MaPTTT ,
			          MaKhuyenMai ,
			          MaNV)
			VALUES  ( @MaBill,
					  0 , -- TongTien - int
			          @MaKH, -- MaKH - int
			          @MaPTTT, -- MaPTTT - int
			          @MaKhuyenMai , -- MaKhuyenMai - int
			          @MaNV  -- MaNV - int
			        )
		END
	IF( EXISTS( SELECT*FROM dbo.Bill WHERE MaBill=@MaBill))
		BEGIN
			COMMIT
		END
	ELSE
		BEGIN
			ROLLBACK
		END
		
GO
 /*
1.
sp_ChangeKhachHang
*/
---------------------------------------------
GO
CREATE PROC sp_ChangeKhachHang(@MaKH INT, @HoTen NVARCHAR(50), @GioiTinh NVARCHAR(4), @DiaChi NVARCHAR(200), @SDT VARCHAR(20))
AS
BEGIN TRAN
	IF( EXISTS( SELECT*FROM dbo.KhachHang WHERE MaKH=@MaKH))
	UPDATE dbo.KhachHang
	SET HoTen=@HoTen,
		GioiTinh=@GioiTinh,
		DiaChi=@DiaChi,
		SDT=@SDT
	WHERE MaKH=@MaKH
	COMMIT

---------------------------------------------
 /*
1.
sp_AddKhachHang
*/
---------------------------------------------***
GO
CREATE PROC sp_AddKhachHang(@MaKH INT, @HoTen NVARCHAR(50), @GioiTinh NVARCHAR(4), @DiaChi NVARCHAR(200), @SDT VARCHAR(20))
AS
BEGIN TRAN
	IF( EXISTS( SELECT*FROM dbo.KhachHang WHERE MaKH=@MaKH))
		BEGIN
			EXEC dbo.sp_ChangeKhachHang @MaKH,@HoTen,@GioiTinh,@DiaChi,@SDT
		END
	ELSE
		BEGIN
		INSERT dbo.KhachHang
		        ( MaKH, HoTen, GioiTinh, DiaChi, SDT )
		VALUES  (@MaKH,@HoTen,@GioiTinh,@DiaChi,@SDT
		          )
			
		END
	IF( EXISTS( SELECT*FROM dbo.KhachHang WHERE MaKH=@MaKH))
		BEGIN
			COMMIT;
		END
	ELSE
		BEGIN
			ROLLBACK;
		END
	

---------------------------------------------
 /*
1.
sp_TimKiemKhuyenMai
*/
---------------------------------------------
GO
CREATE PROC sp_TimKiemKhuyenMai( @NgayBatDau DATE, @NgayKetThuc DATE)
AS
BEGIN
	SELECT * FROM dbo.KhuyenMai WHERE NgayBatDau >=@NgayBatDau AND NgayKetThuc <=@NgayKetThuc
END
---------------------------------------------
 /*
1.
sp_ChangeKhuyenMai
*/
---------------------------------------------
GO
CREATE PROC sp_ChangeKhuyenMai(@MaKhuyenMai INT, @GiaTriKhuyenMai INT, @NgayBatDau DATE, @NgayKetThuc DATE)
AS
BEGIN TRAN
	UPDATE dbo.KhuyenMai
	SET GiaTriKhuyenMai=@GiaTriKhuyenMai,
		NgayBatDau=@NgayBatDau,
		NgayKetThuc=@NgayKetThuc
	WHERE MaKhuyenMai=@MaKhuyenMai
	COMMIT
-----------------------------------------------------0000000000000
GO
CREATE PROC sp_AddKhuyenMai(@MaKhuyenMai INT, @GiaTriKhuyenMai INT, @NgayBatDau DATE, @NgayKetThuc DATE)
AS 
BEGIN TRAN
	IF( EXISTS( SELECT* FROM dbo.KhuyenMai WHERE MaKhuyenMai=@MaKhuyenMai))
		BEGIN
			EXEC sp_ChangeKhuyenMai @MaKhuyenMai, @GiaTriKhuyenMai, @NgayBatDau, @NgayKetThuc
		END
	ELSE
		BEGIN
			INSERT dbo.KhuyenMai
					(MaKhuyenMai,
					 GiaTriKhuyenMai ,
					  NgayBatDau ,
					  NgayKetThuc
					)
			VALUES  (@MaKhuyenMai,@GiaTriKhuyenMai,@NgayBatDau,@NgayKetThuc)
		END
	IF( EXISTS( SELECT*FROM dbo.KhuyenMai WHERE MaKhuyenMai=@MaKhuyenMai))
		BEGIN
			COMMIT
		END
	ELSE
		BEGIN
			ROLLBACK
		END
	
--------------------------------------------------------
GO
CREATE PROC sp_ChangePT_ThanhToan( @MaPTTT INT, @TenPTTT NVARCHAR(50))
AS
BEGIN TRAN 
	UPDATE dbo.PT_ThanhToan
	SET TenPTTT=@TenPTTT
	WHERE MaPTTT=@MaPTTT
	COMMIT
---------------------------------------------------------
GO
CREATE PROC sp_AddPT_ThanhToan( @MaPTTT INT, @TenPTTT NVARCHAR(50))
AS
BEGIN TRAN
	IF( EXISTS( SELECT* FROM dbo.PT_ThanhToan WHERE MaPTTT=@MaPTTT))
		BEGIN	
			EXEC dbo.sp_ChangePT_ThanhToan @MaPTTT, @TenPTTT
		END
	ELSE
		BEGIN
			INSERT dbo.PT_ThanhToan
					(MaPTTT,TenPTTT )
			VALUES  (@MaPTTT,@TenPTTT )
		END
	IF( EXISTS( SELECT*FROM dbo.PT_ThanhToan WHERE MaPTTT=@MaPTTT))
		BEGIN
			COMMIT
		END
	ELSE
		BEGIN
			ROLLBACK
		END
	
----------------------------------------------------------
GO
CREATE PROC sp_ChangeDonNhap(@MaDonNhap INT, @ThoiGian DATE , @DiaChi NVARCHAR(100))
AS 
BEGIN TRAN
	UPDATE dbo.DonNhap
	SET ThoiGian=@ThoiGian,
		DiaChi=@DiaChi
	WHERE MaDonNhap=@MaDonNhap
	COMMIT
GO
------------------------------------------
CREATE PROC sp_ChangeDonNhapInfo(@MaDonNhap INT, @MaSP INT , @SoLuong INT)
AS 
BEGIN TRAN
	UPDATE dbo.DonNhap_Info
	SET MaSP=@MaSP,
		SoLuong=@SoLuong
	WHERE MaDonNhap=@MaDonNhap
	COMMIT
--------------------------------------------------------
GO
CREATE PROC sp_AddDonNhap(@MaDonNhap INT, @ThoiGian DATE, @DiaChi NVARCHAR(100))
AS
BEGIN TRAN
	IF( EXISTS( SELECT* FROM dbo.DonNhap WHERE MaDonNhap=@MaDonNhap))
		BEGIN
			EXEC dbo.sp_ChangeDonNhap @MaDonNhap, @ThoiGian,@DiaChi
		END
	ELSE
		BEGIN
			INSERT dbo.DonNhap
			        ( MaDonNhap,ThoiGian, DiaChi )
			VALUES  (@MaDonNhap, @ThoiGian, @DiaChi)
		END
	IF( EXISTS( SELECT* FROM dbo.DonNhap WHERE MaDonNhap=@MaDonNhap))
		BEGIN
			COMMIT
		END
	ELSE
		BEGIN
			ROLLBACK
		END
	
------------------------------------------------------------
GO
CREATE PROC sp_AddDonNhapInfo (@MaDonNhap INT, @MaSP INT, @SoLuong INT)
AS
BEGIN TRAN
	IF( EXISTS( SELECT * FROM DonNhap_Info WHERE MaDonNhap=@MaDonNhap AND MaSP=@MaSP))
		BEGIN
			EXEC dbo.sp_ChangeDonNhapInfo @MaDonNhap = @MaDonNhap, -- int
			    @MaSP = @MaSP, -- int
			    @SoLuong = @SoLuong -- int
		END
	ELSE
		BEGIN
			INSERT dbo.DonNhap_Info
			        ( MaDonNhap, MaSP, SoLuong )
			VALUES  ( @MaDonNhap,@MaSP,@SoLuong )
		END
	IF( EXISTS( SELECT*FROM dbo.DonNhap_Info WHERE MaDonNhap=@MaDonNhap AND MaSP=@MaSP))
		BEGIN
			COMMIT
		END
	ELSE
		BEGIN
			ROLLBACK
		END
	
-------------------------------------------------------------

GO
CREATE PROC sp_DeleteHangHoa(@MaSP INT)
AS
BEGIN TRAN
	DELETE FROM dbo.HangHoa WHERE MaSP=@MaSP
	IF( EXISTS( SELECT* FROM dbo.HangHoa WHERE MaSP=@MaSP))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END

-----------------------------------------------------------
GO
CREATE PROC sp_DeletePT_ThanhToan(@MaPTTT INT)
AS
BEGIN TRAN
	DELETE FROM dbo.PT_ThanhToan WHERE MaPTTT=@MaPTTT
	IF( EXISTS( SELECT* FROM dbo.PT_ThanhToan WHERE MaPTTT=@MaPTTT))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END
	
------------------------------------------
GO
ALTER  PROC sp_DeleteNSX(@MaNSX INT)
AS
BEGIN TRAN
	DELETE FROM dbo.NSX WHERE MaNSX=@MaNSX
	IF( EXISTS( SELECT* FROM dbo.NSX WHERE MaNSX=@MaNSX))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END

-----------------------------------------------------------
GO
CREATE PROC sp_DeleteNhanVien(@MaNV INT)
AS
BEGIN TRAN
	DELETE FROM dbo.NhanVien WHERE MaNV=@MaNV
	IF( EXISTS( SELECT* FROM dbo.NhanVien WHERE MaNV=@MaNV))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END
	
-----------------------------------------------------------
GO
CREATE PROC sp_DeleteKhuyenMai(@MaKhuyenMai INT)
AS
BEGIN TRAN
	DELETE FROM dbo.KhuyenMai WHERE MaKhuyenMai=@MaKhuyenMai
	IF( EXISTS( SELECT* FROM dbo.KhuyenMai WHERE MaKhuyenMai=@MaKhuyenMai))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END
	
---------------------------------------------------------
GO
CREATE PROC sp_DeleteKhachHang(@MaKH INT)
AS
BEGIN TRAN
	DELETE FROM dbo.KhachHang WHERE MaKH=@MaKH
	IF( EXISTS( SELECT* FROM dbo.KhachHang WHERE MaKH=@MaKH))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END
	
GO
-------------------------------------------
CREATE PROC sp_DeleteDonNhap(@MaDonNhap INT)
AS
BEGIN TRAN
	DELETE FROM dbo.DonNhap WHERE MaDonNhap=@MaDonNhap
	IF( EXISTS( SELECT* FROM dbo.DonNhap WHERE MaDonNhap=@MaDonNhap))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END
GO
-------------------------------------------
CREATE PROC sp_DeleteDonNhapInfo(@MaDonNhap INT,@MaSP INT)
AS
BEGIN TRAN
	DELETE FROM dbo.DonNhap_Info WHERE MaDonNhap=@MaDonNhap AND MaSP = @MaSP
	IF( EXISTS( SELECT* FROM dbo.DonNhap_Info WHERE MaDonNhap=@MaDonNhap AND MaSP = @MaSP))
		BEGIN
			ROLLBACK
		END
	ELSE
		BEGIN
			COMMIT
		END
GO
---------------------------------------------
 /*
1.
sp_GiaTriThanhToan
*/
---------------------------------------------
GO
create function fn_GiaTriThanhToan(@MaPTTT int)
returns int
as
begin
	declare @Tong int
	select @Tong = sum(TongTien) from Bill where @MaPTTT = MaPTTT
	return @Tong
end
GO
---------------------------------------------
 /*
1.
fn_ThongKeMuaCuaKhachBangMa
*/
---------------------------------------------
create function fn_ThongKeMuaCuaKhachBangMa(@MaKH int)
returns table
as return (select Bill_info.MaSP, TenSP, sum(Bill_info.SoLuong) as LuotMua
		   from Bill, Bill_info, HangHoa
		   where MaKH = @MaKH and Bill.MaBill = Bill_info.MaBill
		   group by Bill_info.MaSP, TenSP)
---------------------------------------------
/*
Tr_UpdateDonNhapInfo 
*/
-------------------------------------------
GO
CREATE TRIGGER Tr_UpdateDonNhapInfo ON dbo.DonNhap_Info AFTER UPDATE, INSERT,DELETE
AS
BEGIN
	DECLARE @MaSP INT  
	DECLARE @SoLuongSau INT
	SET @SoLuongSau = 0
	SELECT @MaSP = Inserted.MaSP, @SoLuongSau = Inserted.SoLuong FROM Inserted
	DECLARE @SoLuongBanDau INT
	SET @SoLuongBanDau  =0
	SELECT @SoLuongBanDau = Deleted.SoLuong FROM Deleted  

	UPDATE dbo.HangHoa SET SoLuong =  SoLuong + @SoLuongSau WHERE  MaSP =@MaSP
	UPDATE dbo.HangHoa SET SoLuong =  SoLuong - @SoLuongBanDau WHERE  MaSP =@MaSP
END 
 /*
1.
sp_LoadTongDonNhap
*/
---------------------------------------------
GO
CREATE PROC sp_LoadTongDonNhap @MaDonNhap INT
AS
BEGIN TRAN
	DECLARE @Tong INT = 0 
	IF( EXISTS( SELECT*FROM dbo.DonNhap WHERE MaDonNhap=@MaDonNhap))
	BEGIN
		SELECT @Tong = SUM(dbo.DonNhap_Info.SoLuong*dbo.HangHoa.GiaBan) FROM 
		dbo.DonNhap_Info JOIN dbo.HangHoa ON HangHoa.MaSP = dbo.DonNhap_Info.MaSP 
		WHERE MaDonNhap = @MaDonNhap
	END

	UPDATE dbo.DonNhap SET TongTien = @Tong WHERE MaDonNhap = @MaDonNhap
	COMMIT

---------------------------------------------
/*
1.
sp_AddKiemTraDonNhapInfo
*/
---------------------------------------------***
Go
CREATE PROC sp_AddKiemTraDonNhapInfo
@MaDonNhap INT, @MaSP INT, @SoLuong INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT = 0
	DECLARE @CountSp INT = 1
	
	SELECT @isExitsBillInfo = b.MaDonNhap, @CountSp = b.SoLuong 
	FROM dbo.DonNhap_Info AS b 
	WHERE b.MaDonNhap = @MaDonNhap AND b.MaSP = @MaSP

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @CountSp + @SoLuong
		IF (@newCount > 0)
			UPDATE dbo.DonNhap_Info SET SoLuong = @CountSp + @SoLuong WHERE MaSP = @MaSP AND MaDonNhap = @MaDonNhap
		ELSE
			DELETE dbo.DonNhap_Info WHERE MaDonNhap = @MaDonNhap AND MaSP = @MaSP
	END
	ELSE
	BEGIN
		EXEC dbo.sp_AddDonNhapInfo @MaDonNhap = @MaDonNhap, -- int
		    @MaSP = @MaSP, -- int
		    @SoLuong = @SoLuong -- int

	END

	EXEC dbo.sp_LoadTongDonNhap @MaDonNhap = @MaDonNhap -- int	
END
-------------------------------------------
 /*
1.
Create_User

---------------------------------------------
GO
CREATE LOGIN DieuHanh WITH PASSWORD = '1234' --EXEC sys.sp_addlogin @loginame = QuanLy1, @passwd = 1234
GO
EXECUTE sys.sp_addrole @rolename = DieuHanhRole
GO

EXECUTE sys.sp_adduser @loginame = DieuHanh, 
    @name_in_db = DieuHanhUser , 
    @grpname = DieuHanhRole

GO
GRANT SELECT ON dbo.HangHoa TO DieuHanhUser



--EXEC sp_addrolemember 'DieuHanhRole',  'DieuHanh'
---------------------------------------------

---------------------------------------------
		-----------------------------------------------------
        --EXEC sys.sp_helplogins @LoginNamePattern = NULL -- sysname
        ---------------------------------------------------------
		--EXEC sys.sp_helpuser @name_in_db = NULL -- sysname
		-----------------------------------
		--
		--GO
        --CREATE USER user_ FOR LOGIN Admin_
		--GO
        ----------------
		
		--sp_addrole */
		--Exec sp_AddHangHoa @MaNSX =1, @TenSP =N'LapTop1', @CPU =N'i7', @RAM =N'23', @ManHinh = N'lcd', @PIN=N'4000',@giaGoc= 5,@giaBan=10,@SoLuong =23
	/*
		  catch (SqlException sqlEx)
                {
                    MessageBox.Show("Khong co quyen truy cap Hoac loi du lieu");
                }
				
				*/

--------------------------------------------
/*
1.
HangHoa_Image_VIEW
*/
--------------------------------------------
GO
CREATE VIEW HangHoa_Image_VIEW AS 
SELECT MaSP,Hinh
FROM  dbo.HangHoa
				
--------------------------------------------
/*
1.
View_DonNhapInfo
*/
--------------------------------------------
Go
CREATE PROC sp_DonNhapInfo @MaDonNhap INT AS
BEGIN
	SELECT MaDonNhap,DonNhap_Info.MaSP,DonNhap_Info.SoLuong,TenSP FROM dbo.DonNhap_Info JOIN dbo.HangHoa ON HangHoa.MaSP = DonNhap_Info.MaSP
	WHERE MaDonNhap = @MaDonNhap
END
EXEC dbo.sp_AddDonNhapInfo @MaDonNhap = 0, -- int
    @MaSP = 0, -- int
    @SoLuong = 0 -- int
