use KeDonThuoc

-- DANH MỤC
CREATE TABLE DM_ICD
(
	MAICD NVARCHAR(10) NOT NULL,
	TenBenh nvarchar(200) not null,
	TenKhongDau nvarchar(200),
	PRIMARY KEY (MAICD)
)

CREATE TABLE DM_THUOC
(
	MATHUOC NVARCHAR(10) NOT NULL,
	TenThuoc nvarchar(100),
	HoatChat nvarchar(200),
	QuyCach nvarchar(20),
	DonViChan nvarchar(20),
	DonViLe nvarchar(20),
	HeSoQuyDoi int,
	GiaNhap int,
	GiaBanle int,
	GiaBanChan int,
	Cam bit,
	PRIMARY KEY (MATHUOC)
)
CREATE TABLE TOATHUOC(
	MATOATHUOC NVARCHAR(20) NOT NULL,
	MATHUOC NVARCHAR(10) references DM_THUOC(MATHUOC),
	TenThuoc nvarchar(100),
	HoatChat nvarchar(200),
	DonViTinh nvarchar(20),
	NgayDung int,
	Sang int,
	Trua int,
	Chieu int,
	Toi int,
	SoLuong int,
	ThanhTien int,
	NgayKeDon datetime,
	primary key (matoathuoc)
)

-- bảng 
create table BenhNhan (
	MABN INT NOT NULL,
	TEN NVARCHAR(50),
	TUOI INT,
	GIOITINH NVARCHAR(3),
	DIACHI NVARCHAR (200),
	SDT NVARCHAR (11),
	PRIMARY KEY (MABN)
)
create table LichSu (
	MAKHAM INT NOT NULL,
	MABN INT NOT NULL REFERENCES BENHNHAN(MABN),
	MATOATHUOC NVARCHAR(20),
	MAICD NVARCHAR(5),
	MABS NVARCHAR(20),
	PRIMARY KEY (MAKHAM)
)

create table DanDo (
	MADANDO INT NOT NULL,
	MATOATHUOC NVARCHAR(20),
	Loidan nvarchar(200),
	SoNgayTaiKham int,
	TaiKham datetime,
	primary key (madando)
)


