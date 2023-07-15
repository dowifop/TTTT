use master
if exists (select*from sysdatabases where name = 'QlyTheThao')
drop database QlyTheThao
go 
create database QlyTheThao
go
use QlyTheThao
go
CREATE TABLE KhachHang
(
  maKH int IDENTITY(1,1) ,
  hoTenKH nvarchar(100) NOT NULL,
  emailKH nvarchar(50) NOT NULL,
  sdtKH CHAR(10) NOT NULL,
  diaChiKH nvarchar(100) NOT NULL,
  taiKhoan VARCHAR(10) NOT NULL,
  matKhau VARCHAR(10) NOT NULL,
  PRIMARY KEY (maKH)
);

CREATE TABLE PhongBan
(
  maPH CHAR(10) NOT NULL,
  tenPH nvarchar(100) NOT NULL,
  mota nvarchar(200) NOT NULL,
  soNV INT NOT NULL,
  PRIMARY KEY (maPH)
);

CREATE TABLE San
(
  maSan CHAR(10) NOT NULL,
  tenSan nvarchar(100) NOT NULL,
  theLoai nvarchar(100) NOT NULL,
  giaThue INT NOT NULL,
  PRIMARY KEY (maSan)
);

CREATE TABLE PhieuThueSan
(
  maPT CHAR(10) NOT NULL,
  ngayLapPhieu DATE NOT NULL,
  maSan CHAR(10) NOT NULL,
  maKH int NOT NULL,
  PRIMARY KEY (maPT),
  FOREIGN KEY (maSan) REFERENCES San(maSan),
  FOREIGN KEY (maKH) REFERENCES KhachHang(maKH)
);

CREATE TABLE ChucVu
(
  maCV INT NOT NULL,
  tenCV INT NOT NULL,
  PRIMARY KEY (maCV)
);

CREATE TABLE NhanVien
(
  maNV CHAR(10) NOT NULL,
  hotenNV nvarchar(100) NOT NULL,
  emailNV nvarchar(50) NOT NULL,
  sdtNV CHAR(10) NOT NULL,
  diaChiNV nvarchar(100) NOT NULL,
  dobNV DATE NOT NULL,
  ngayVaoLam DATE NOT NULL,
  maPH CHAR(10) NOT NULL,
  maCV INT NOT NULL,
  PRIMARY KEY (maNV),
  FOREIGN KEY (maPH) REFERENCES PhongBan(maPH),
  FOREIGN KEY (maCV) REFERENCES ChucVu(maCV)
);

CREATE TABLE HoaDon
(
  maHD CHAR(10) NOT NULL,
  ngayLapHD DATE NOT NULL,
  maPT CHAR(10) NOT NULL,
  maKH int NOT NULL,
  PRIMARY KEY (maHD),
  FOREIGN KEY (maPT) REFERENCES PhieuThueSan(maPT),
  FOREIGN KEY (maKH) REFERENCES KhachHang(maKH)
);

CREATE TABLE LapPhieu
(
  soGioThue INT NOT NULL,
  NgayThue DATE NOT NULL,
  tenNVlapPhieu nvarchar(100) NOT NULL,
  tenSanThue nvarchar(100) NOT NULL,
  maNV CHAR(10) NOT NULL,
  maPT CHAR(10) NOT NULL,
  PRIMARY KEY (maNV, maPT),
  FOREIGN KEY (maNV) REFERENCES NhanVien(maNV),
  FOREIGN KEY (maPT) REFERENCES PhieuThueSan(maPT)
);

CREATE TABLE LapHoaDon
(
  tongTien INT NOT NULL,
  tenNVlapHD nvarchar(100) NOT NULL,
  maNV CHAR(10) NOT NULL,
  maHD CHAR(10) NOT NULL,
  PRIMARY KEY (maNV, maHD),
  FOREIGN KEY (maNV) REFERENCES NhanVien(maNV),
  FOREIGN KEY (maHD) REFERENCES HoaDon(maHD)
);
