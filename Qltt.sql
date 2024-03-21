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
  maKH nvarchar(50) NOT NULL,
  hoTenKH nvarchar(50) NULL,
  emailKH nvarchar(50) NULL,
  sdtKH char(10) NULL,
  diaChiKH nvarchar(50) NULL,
  matKhau nvarchar(10) NULL,
  PRIMARY KEY (maKH)
);

CREATE TABLE PhongBan
(
  maPH INT IDENTITY(1,1)NOT NULL,
  tenPH nvarchar(50) NULL,
  soNV INT NULL,
  PRIMARY KEY (maPH)
);

CREATE TABLE ChucVu
(
  maCV INT IDENTITY(1,1) NOT NULL,
  tenCV nvarchar(50) NULL,
  PRIMARY KEY (maCV)
);

CREATE TABLE SuKien
(
  maSK INT IDENTITY(1,1) NOT NULL,
  tenSK nvarchar(50) NULL,
  ngayKT DATETIME NULL,
  moTaSK nvarchar(50) NULL,
  ngayBD DATETIME NULL,
  soLuongNguoi INT NULL,
  maKH nvarchar(50) NULL,
  PRIMARY KEY (maSK),
  FOREIGN KEY (maKH) REFERENCES KhachHang(maKH)
);

CREATE TABLE TinhTrangSan
(
  maTinhTrang INT IDENTITY(1,1) NOT NULL,
  mota nvarchar(50) NULL,
  PRIMARY KEY (maTinhTrang)
);

CREATE TABLE tinhTrangHD
(
  maTinhTrang INT IDENTITY(1,1) NOT NULL,
  mo_ta nvarchar(50) NULL,
  PRIMARY KEY (maTinhTrang)
);

CREATE TABLE tinhTrangPT
(
  maTinhTrang INT IDENTITY(1,1) NOT NULL,
  tinhTrang nvarchar(50) NULL,
  PRIMARY KEY (maTinhTrang)
);

CREATE TABLE LoaiSan
(
  Loai_San INT IDENTITY(1,1) NOT NULL,
  moTa nvarchar(50) NULL,
  giaThue INT NULL,
  anh nvarchar(200) NULL,
  PRIMARY KEY (Loai_San)
);



CREATE TABLE TinNhan
(
  id int IDENTITY(1,1) NOT NULL,
    ma_kh nvarchar(50)  NULL,
    ho_ten nvarchar(100)  NULL,
    mail nvarchar(100)  NULL,
    noi_dung nvarchar(500)  NULL,
    ngay_gui datetime  NULL,
    danh_gia int  NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (ma_kh) REFERENCES KhachHang(maKH)
);

CREATE TABLE NhanVien
(
  maNV INT IDENTITY(1,1) NOT NULL,
  hotenNV nvarchar(50)  NULL,
  emailNV nvarchar(50) NULL,
  sdtNV char(10) NULL,
  diaChiNV nvarchar(50) NULL,
  dobNV DATETIME NULL,
  taiKhoan char(20) NULL,
  matKhau nvarchar(10) NULL,
  maPH INT NULL,
  maCV INT NULL,
  maSK INT NULL,
  PRIMARY KEY (maNV),
  FOREIGN KEY (maPH) REFERENCES PhongBan(maPH),
  FOREIGN KEY (maCV) REFERENCES ChucVu(maCV),
  FOREIGN KEY (maSK) REFERENCES SuKien(maSK)
);

CREATE TABLE San
(
  maSan INT IDENTITY(1,1) NOT NULL,
  maSoSan char(10)  NULL,
  Loai_San INT NULL,
  maTinhTrang INT NULL,
  PRIMARY KEY (maSan),
  FOREIGN KEY (maTinhTrang) REFERENCES TinhTrangSan(maTinhTrang),
  FOREIGN KEY (Loai_San) REFERENCES LoaiSan(Loai_San)
);

CREATE TABLE PhieuThueSan
(
  maPT INT IDENTITY(1,1)NOT NULL,
  soGioThue INT NULL,
  NgayThue DATETIME NULL,
  NgayDat datetime null,
  maSan INT NULL,
  maNV INT NULL,
  maKH nvarchar(50) NULL,
  maTinhTrang INT NULL,
  thong_tin_khach_hang_thue nvarchar(400) NULL,
  PRIMARY KEY (maPT),
  FOREIGN KEY (maSan) REFERENCES San(maSan),
  FOREIGN KEY (maNV) REFERENCES NhanVien(maNV),
  FOREIGN KEY (maKH) REFERENCES KhachHang(maKH),
  FOREIGN KEY (maTinhTrang) REFERENCES tinhTrangPT(maTinhTrang)
);




CREATE TABLE HoaDonTS
(
  maHDTS INT IDENTITY(1,1) NOT NULL,
  ngayThueSan DATETIME NULL,
  tienSan INT NULL,
  tongTien INT NULL,
  tiendichvu INT NULL,
  maPT INT NULL,
  maNV INT NULL,
  maTinhTrang INT NULL,
  PRIMARY KEY (maHDTS),
  FOREIGN KEY (maPT) REFERENCES PhieuThueSan(maPT),
  FOREIGN KEY (maNV) REFERENCES NhanVien(maNV),
  FOREIGN KEY (maTinhTrang) REFERENCES tinhTrangHD(maTinhTrang)
);
CREATE TABLE DichVu
(
  ma_dv INT IDENTITY(1,1) NOT NULL,
  ten_dv nvarchar(100)  NULL,
  gia float  NULL,
  anh nvarchar(200)  NULL,
  ton_kho INT  NULL,
  PRIMARY KEY (ma_dv)
);
CREATE TABLE DichVuDaDat
(
  id INT IDENTITY(1,1) NOT NULL,
  so_luong INT NULL,
  ma_dv INT NULL,
  maHDTS INT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (ma_dv) REFERENCES DichVu(ma_dv),
  FOREIGN KEY (maHDTS) REFERENCES HoaDonTS(maHDTS)
);
insert into ChucVu values(N'giám đốc')
insert into ChucVu values(N'Trưởng Phòng')
insert into ChucVu values(N'Nhân Viên')
select * from ChucVu

insert into PhongBan values(N'Hành Chính',20)
insert into PhongBan values(N'Nghiệp Vụ',20)
insert into PhongBan values(N'Tài Chính',20)
insert into PhongBan values(N'Dịch Vụ',20)
select * from PhongBan

insert into NhanVien values(N'Nguyễn Hữu Thọ','thogd@gmail.com','0764957606',N'123 cây chuối quận 12','2020-01-19','giamdoc','12345',null,1,null)
insert into NhanVien values(N'Vũ Đức Nam','namtp@gmail.com','0367590203',N'456 cây đu đủ quận 12','2020-06-21','truongphong','12345',1,2,null)
insert into NhanVien values(N'Trương Thanh Bình','binhnv@gmail.com','0836447006',N'789 cây mận bình chánh','2021-09-25','nhanvien','12345',2,3,null)
insert into NhanVien values(N'Nguyễn Nhật Tiến','tiennv@gmail.com','0387737509',N'674 cây mít củ chi','2021-10-19','nhanvien2','12345',3,3,null)
select * from NhanVien

insert into TinhTrangSan values(N'Trống')
insert into TinhTrangSan values(N'Đang sử dụng')
insert into TinhTrangSan values(N'Đang dọn')
insert into TinhTrangSan values(N'Đang bảo trì')
insert into TinhTrangSan values(N'Dừng sử dụng')

insert into tinhTrangPT values (N'Đang đặt')
insert into tinhTrangPT values (N'Đã xong')
insert into tinhTrangPT values (N'Đã hủy')
insert into tinhTrangPT values (N'Đã thanh toán')

insert into tinhTrangHD values (N'Chưa thanh toán')
insert into tinhTrangHD values (N'Đã thanh toán')

insert into LoaiSan values(N'Sân bóng đá', 120000, '/Content/Home/hinh/bgsan.png')
insert into LoaiSan values(N'Hồ bơi', 50000, '/Content/Home/hinh/bghoboi.png')
insert into LoaiSan values(N'Nhà thi đấu', 200000, '/Content/Home/hinh/bgnhathidau.png')


insert into San values('BD01', 1,1)
insert into San values('HB02', 2,1)
insert into San values('NTD01', 3,1)
select * from San
select * from KhachHang

insert into DichVu values(N'Trà đá',20000,'/Content/Home/hinh/DichVu/TraDa1.png',99)
insert into DichVu values(N'Pepsi',10000,'/Content/Home/hinh/DichVu/Pepsi1.png',59)
insert into DichVu values(N'CocaCola',30000,'/Content/Home/hinh/DichVu/CocaCola1.png',69)
insert into DichVu values(N'Nước suối',10000,'/Content/Home/hinh/DichVu/NuocSuoi.png',19)
insert into DichVu values(N'RedBull',15000,'/Content/Home/hinh/DichVu/RedBull.png',39)
select *from DichVu