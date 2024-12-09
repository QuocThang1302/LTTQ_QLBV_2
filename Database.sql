CREATE DATABASE BV
USE BV

CREATE TABLE CTHDVatDung (
  MaHoaDon NVARCHAR(20),
  MaVatDung NVARCHAR(20),
  SoLuong INT,
  ThanhTien DECIMAL(10,2),
  PRIMARY KEY (MaHoaDon, MaVatDung)
)

GO

CREATE TABLE VatDung (
  MaVatDung NVARCHAR(20) PRIMARY KEY,
  TenVatDung NVARCHAR(100),
  MoTa NTEXT,
  SoLuong INT,
  Gia DECIMAL(10,2),
  MaQuanLy NVARCHAR(20)
)
SELECT * from VatDung
GO

CREATE TABLE HoaDon (
  MaHoaDon NVARCHAR(20) PRIMARY KEY,
  TenHoaDon NVARCHAR(50),
  MaBenhNhan NVARCHAR(20),
  MaNhanVien NVARCHAR(20),
  NgayLapHoaDon DATE,
  GiaTien DECIMAL(10,2),
  TrangThai NVARCHAR(50)
)
GO

CREATE TABLE Thuoc (
  MaThuoc NVARCHAR(20) PRIMARY KEY,
  TenThuoc NVARCHAR(100),
  CongDung NTEXT,
  SoLuong INT,
  GiaTien DECIMAL(10,2),
  HanSuDung DATE
)
GO

CREATE TABLE CongViec (
  MaCongViec NVARCHAR(20) PRIMARY KEY,
  TenCongViec NVARCHAR(100),
  MoTaCongViec NTEXT,
  GhiChu NTEXT
)
GO

CREATE TABLE LichTruc (
  MaLichTruc NVARCHAR(20) PRIMARY KEY,
  MaBacSi NVARCHAR(20),
  NgayTruc DATE,
  PhanCong NVARCHAR(20),
  TrangThai NVARCHAR(50)
)
GO

CREATE TABLE Khoa (
  MaKhoa NVARCHAR(20) PRIMARY KEY,
  TenKhoa NVARCHAR(100),
  TruongKhoa NVARCHAR(20)
)
GO

CREATE TABLE ChuyenNganh (
  MaChuyenNganh NVARCHAR(20) PRIMARY KEY,
  TenChuyenNganh NVARCHAR(100),
  Khoa NVARCHAR(20)
)
GO

CREATE TABLE Role (
  RoleID NVARCHAR(20) PRIMARY KEY,
  TenRole NVARCHAR(50)
)
GO

CREATE TABLE PhieuKhamBenh (
  MaPhieuKham NVARCHAR(20) PRIMARY KEY,
  MaBenhNhan NVARCHAR(20),
  NgayKham DATE,
  LyDoKhamBenh NTEXT,
  KhamLamSang NTEXT,
  ChanDoan NTEXT,
  KetQuaKham NTEXT,
  DieuTri NTEXT,
  MaBacSi NVARCHAR(20)
)
GO

CREATE TABLE LichHenKham (
  MaLichHen NVARCHAR(20) PRIMARY KEY,
  MaBenhNhan NVARCHAR(20),
  NgayHenKham DATE,
  MaBacSi NVARCHAR(20)
)
GO

CREATE TABLE BenhNhan (
  MaBenhNhan NVARCHAR(20) PRIMARY KEY,
  Ho NVARCHAR(50),
  Ten NVARCHAR(50),
  NgaySinh DATE,
  GioiTinh NVARCHAR(10),
  CCCD NVARCHAR(20),
  NgheNghiep NVARCHAR(100),
  DiaChi NTEXT,
  SDT NVARCHAR(20),
  Email NVARCHAR(100),
  MaKhoa NVARCHAR(20)
)
GO

CREATE TABLE BenhAn (
  MaBenhAn NVARCHAR(20) PRIMARY KEY,
  MaBenhNhan NVARCHAR(20),
  NgayTaoLap DATE,
  Benh NVARCHAR(20),
  TinhTrang NTEXT,
  DieuTri NTEXT
)
GO

CREATE TABLE Benh (
  MaBenh NVARCHAR(20) PRIMARY KEY,
  TenBenh NVARCHAR(100),
  MoTa NTEXT,
  TrieuChung NTEXT
)
GO

CREATE TABLE CTDonThuoc (
  MaDonThuoc NVARCHAR(20),
  MaThuoc NVARCHAR(20),
  SoLuong INT,
  GiaTien decimal(10,2),
  HuongDanSuDung NTEXT,
  MaHoaDon NVARCHAR(20),
  PRIMARY KEY (MaDonThuoc, MaThuoc)
)
GO

CREATE TABLE DonThuoc (
  MaDonThuoc NVARCHAR(20) PRIMARY KEY,
  MaBenhNhan NVARCHAR(20),
  MaBacSi NVARCHAR(20),
  NgayLapDon DATE
)
GO

CREATE TABLE NhanVien (
  MaNhanVien NVARCHAR(20) PRIMARY KEY,
  Ho NVARCHAR(50),
  Ten NVARCHAR(50),
  MaChuyenNganh NVARCHAR(20),
  RoleID NVARCHAR(20),
  LoaiNhanVien NVARCHAR(20),
  NgaySinh DATE,
  GioiTinh NVARCHAR(10),
  CCCD NVARCHAR(20),
  DiaChi NTEXT,
  SDT NVARCHAR(20),
  Email NVARCHAR(100),
  MatKhau NVARCHAR(100)
)
GO

ALTER TABLE BenhNhan ADD FOREIGN KEY (MaKhoa) REFERENCES Khoa (MaKhoa)
GO

ALTER TABLE CTDonThuoc ADD FOREIGN KEY (MaHoaDon) REFERENCES HoaDon (MaHoaDon)
GO

ALTER TABLE VatDung ADD FOREIGN KEY (MaQuanLy) REFERENCES NhanVien (MaNhanVien)
GO

ALTER TABLE Khoa ADD FOREIGN KEY (TruongKhoa) REFERENCES NhanVien (MaNhanVien)
GO

ALTER TABLE CTHDVatDung ADD FOREIGN KEY (MaVatDung) REFERENCES VatDung (MaVatDung)
GO

ALTER TABLE CTHDVatDung ADD FOREIGN KEY (MaHoaDon) REFERENCES HoaDon (MaHoaDon)
GO

ALTER TABLE HoaDon ADD FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan (MaBenhNhan)
GO

ALTER TABLE HoaDon ADD FOREIGN KEY (MaNhanVien) REFERENCES NhanVien (MaNhanVien)
GO

ALTER TABLE LichTruc ADD FOREIGN KEY (MaBacSi) REFERENCES NhanVien (MaNhanVien)
GO

ALTER TABLE LichTruc ADD FOREIGN KEY (PhanCong) REFERENCES CongViec (MaCongViec)
GO

ALTER TABLE ChuyenNganh ADD FOREIGN KEY (Khoa) REFERENCES Khoa (MaKhoa)
GO

ALTER TABLE NhanVien ADD FOREIGN KEY (MaChuyenNganh) REFERENCES ChuyenNganh (MaChuyenNganh)
GO

ALTER TABLE NhanVien ADD FOREIGN KEY (RoleID) REFERENCES Role (RoleID)
GO

ALTER TABLE PhieuKhamBenh ADD FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan (MaBenhNhan)
GO

ALTER TABLE PhieuKhamBenh ADD FOREIGN KEY (MaBacSi) REFERENCES NhanVien (MaNhanVien)
GO

ALTER TABLE LichHenKham ADD FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan (MaBenhNhan)
GO

ALTER TABLE LichHenKham ADD FOREIGN KEY (MaBacSi) REFERENCES NhanVien (MaNhanVien)
GO

ALTER TABLE BenhAn ADD FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan (MaBenhNhan)
GO

ALTER TABLE BenhAn ADD FOREIGN KEY (Benh) REFERENCES Benh (MaBenh)
GO

ALTER TABLE CTDonThuoc ADD FOREIGN KEY (MaDonThuoc) REFERENCES DonThuoc (MaDonThuoc)
GO

ALTER TABLE CTDonThuoc ADD FOREIGN KEY (MaThuoc) REFERENCES Thuoc (MaThuoc)
GO

ALTER TABLE DonThuoc ADD FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan (MaBenhNhan)
GO

ALTER TABLE DonThuoc ADD FOREIGN KEY (MaBacSi) REFERENCES NhanVien (MaNhanVien)
GO

-- Role table
INSERT INTO Role (RoleID, TenRole) VALUES  
('R01', N'Admin'), 
('R02', N'Doctor');

-- CongViec table
INSERT INTO CongViec (MaCongViec, TenCongViec, MoTaCongViec, GhiChu) VALUES  
('CN001', N'Trực khoa nội', N'Thăm khám, điều trị bệnh nhân', N'Trực cả ngày'),
('CN002', N'Trực khoa ngoại', N'Thăm khám, điều trị bệnh nhân', N'Trực ca sáng '),
('CN003', N'Trực khoa nhi', N'Thăm khám, điều trị bệnh nhân', N'Trực ca chiều'),
('CN004', N'Trực khoa tim mạch', N'Thăm khám, điều trị bệnh nhân', N'Trực cả ngày');

-- Vô hiệu hóa tất cả ràng buộc khóa ngoại
ALTER TABLE NhanVien NOCHECK CONSTRAINT ALL;
ALTER TABLE Khoa NOCHECK CONSTRAINT ALL;
ALTER TABLE ChuyenNganh NOCHECK CONSTRAINT ALL;

-- Khoa table
INSERT INTO Khoa (MaKhoa, TenKhoa, TruongKhoa) VALUES
('K01', N'Khoa Nội', 'NV003'),
('K02', N'Khoa Ngoại', 'NV002'),
('K03', N'Khoa Nhi', 'NV003'),
('K04', N'Khoa Tim Mạch', 'NV002');

-- ChuyenNganh table
INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, Khoa) VALUES
('CN01', N'Tim Mạch', 'K04'),
('CN02', N'Nhi Khoa', 'K03'),
('CN03', N'Ngoại Thần Kinh', 'K02'),
('CN04', N'Nội Tiết', 'K01');

-- NhanVien table
INSERT INTO NhanVien (MaNhanVien, Ho, Ten, MaChuyenNganh, RoleID, LoaiNhanVien, NgaySinh, GioiTinh, CCCD, DiaChi, SDT, Email, MatKhau) VALUES
('NV001', N'Nguyen', N'Van A', 'CN01', 'R01', N'Quản lý', '1980-01-01', N'Nam', '123456789', N'Hà Nội', '0901234567', 'a@gmail.com', '123'),
('NV002', N'Tran', N'Thi B', 'CN02', 'R02', N'Bác sĩ', '1985-02-02', N'Nữ', '987654321', N'TP Hồ Chí Minh', '0912345678', 'b@gmail.com', '123'),
('NV003', N'Le', N'Van C', 'CN03', 'R02', N'Bác sĩ', '1990-03-03', N'Nam', '111111111', N'Đà Nẵng', '0923456789', 'c@gmail.com', '123'),
('NV004', N'Pham', N'Thi D', 'CN04', 'R01', N'Quản lý', '1988-04-04', N'Nữ', '222222222', N'Hải Phòng', '0934567890', 'd@gmail.com', '123');

-- Kích hoạt lại các ràng buộc khóa ngoại
ALTER TABLE NhanVien CHECK CONSTRAINT ALL;
ALTER TABLE Khoa CHECK CONSTRAINT ALL;
ALTER TABLE ChuyenNganh CHECK CONSTRAINT ALL;

-- LichTruc table
INSERT INTO LichTruc (MaLichTruc, MaBacSi, NgayTruc, PhanCong, TrangThai) VALUES 
('LT001', 'NV002', '2024-08-18', 'CN001', N'Đã hoàn thành'),
('LT002', 'NV003', '2024-08-19', 'CN002', N'Đã hoàn thành'),
('LT003', 'NV002', '2024-08-19', 'CN003', N'Đang thực hiện'),
('LT004', 'NV003', '2024-08-20', 'CN004', N'Chưa thực hiện');

-- BenhNhan table
INSERT INTO BenhNhan (MaBenhNhan, Ho, Ten, NgaySinh, GioiTinh, CCCD, NgheNghiep, DiaChi, SDT, Email, MaKhoa) VALUES
('BN001', N'Le', N'Thi C', '1990-03-03', N'Nữ', '123123123', N'Nhân viên văn phòng', N'Đà Nẵng', '0923456789', 'c@gmail.com', 'K01'),
('BN002', N'Pham', N'Van D', '1995-04-04', N'Nam', '321321321', N'Sinh viên', N'Hải Phòng', '0934567890', 'd@gmail.com', 'K02'),
('BN003', N'Nguyen', N'Thi E', '1988-05-05', N'Nữ', '456456456', N'Giáo viên', N'Cần Thơ', '0945678901', 'e@gmail.com', 'K03'),
('BN004', N'Tran', N'Van F', '1992-06-06', N'Nam', '654654654', N'Kỹ sư', N'Quảng Ninh', '0956789012', 'f@gmail.com', 'K04');

-- VatDung table
INSERT INTO VatDung (MaVatDung, TenVatDung, MoTa, SoLuong, Gia, MaQuanLy) VALUES
('VD001', N'Băng gạc', N'Băng gạc y tế', 100, 5000.00, 'NV001'),
('VD002', N'Nhiệt kế', N'Nhiệt kế điện tử', 50, 150000.00, 'NV002'),
('VD003', N'Ống nghe', N'Ống nghe y tế', 20, 300000.00, 'NV003'),
('VD004', N'Máy đo huyết áp', N'Máy đo huyết áp tự động', 10, 1000000.00, 'NV004');

-- HoaDon table
INSERT INTO HoaDon (MaHoaDon, TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon, GiaTien, TrangThai) VALUES
('HD001', N'Thanh toán khám bệnh', 'BN001', 'NV001', '2024-01-01', 500000.00, N'Đã thanh toán'),
('HD002', N'Thanh toán xét nghiệm', 'BN002', 'NV002', '2024-01-02', 300000.00, N'Chưa thanh toán'),
('HD003', N'Thanh toán thuốc', 'BN003', 'NV003', '2024-01-03', 400000.00, N'Đã thanh toán'),
('HD004', N'Thanh toán dịch vụ', 'BN004', 'NV004', '2024-01-04', 600000.00, N'Chưa thanh toán');

-- Thuoc table
INSERT INTO Thuoc (MaThuoc, TenThuoc, CongDung, SoLuong, GiaTien, HanSuDung) VALUES
('T001', N'Paracetamol', N'Giảm đau, hạ sốt', 200, 2000.00, '2025-01-01'),
('T002', N'Vitamin C', N'Tăng sức đề kháng', 300, 1000.00, '2024-12-31'),
('T003', N'Amoxicillin', N'Kháng sinh', 150, 5000.00, '2025-06-30'),
('T004', N'Ibuprofen', N'Giảm đau, kháng viêm', 100, 3000.00, '2024-11-30');

-- DonThuoc table
INSERT INTO DonThuoc (MaDonThuoc, MaBenhNhan, MaBacSi, NgayLapDon) VALUES
('DT001', 'BN001', 'NV002', '2024-01-01'),
('DT002', 'BN002', 'NV003', '2024-01-02'),
('DT003', 'BN003', 'NV004', '2024-01-03'),
('DT004', 'BN004', 'NV001', '2024-01-04');

-- Benh table
INSERT INTO Benh (MaBenh, TenBenh, MoTa, TrieuChung) VALUES
('B001', N'Cảm cúm', N'Nhiễm virus', N'Sốt, ho, đau họng'),
('B002', N'Đau dạ dày', N'Viêm loét dạ dày', N'Đau bụng, buồn nôn'),
('B003', N'Tiểu đường', N'Rối loạn đường huyết', N'Khát nước, sụt cân'),
('B004', N'Cao huyết áp', N'Tăng áp lực máu', N'Chóng mặt, đau đầu');
-- BenhAn table
INSERT INTO BenhAn (MaBenhAn, MaBenhNhan, NgayTaoLap, Benh, TinhTrang, DieuTri) VALUES
('BA001', 'BN001', '2024-01-01', 'B001', N'Đã khỏi', N'Nghỉ ngơi, uống thuốc'),
('BA002', 'BN002', '2024-01-02', 'B002', N'Đang điều trị', N'Theo dõi, uống thuốc'),
('BA003', 'BN003', '2024-01-03', 'B003', N'Ổn định', N'Chế độ ăn kiêng'),
('BA004', 'BN004', '2024-01-04', 'B004', N'Đang điều trị', N'Sử dụng thuốc đều đặn');

-- PhieuKhamBenh table
INSERT INTO PhieuKhamBenh (MaPhieuKham, MaBenhNhan, NgayKham, LyDoKhamBenh, KhamLamSang, ChanDoan, KetQuaKham, DieuTri, MaBacSi) VALUES
('PK001', 'BN001', '2024-01-01', N'Sốt cao', N'Kiểm tra nhiệt độ', N'Cảm cúm', N'Ổn định', N'Uống thuốc theo chỉ dẫn', 'NV002'),
('PK002', 'BN002', '2024-01-02', N'Đau bụng', N'Nội soi', N'Đau dạ dày', N'Cần theo dõi', N'Điều chỉnh chế độ ăn', 'NV003'),
('PK003', 'BN003', '2024-01-03', N'Mệt mỏi', N'Xét nghiệm máu', N'Tiểu đường', N'Ổn định', N'Thay đổi lối sống', 'NV004'),
('PK004', 'BN004', '2024-01-04', N'Đau đầu', N'Đo huyết áp', N'Cao huyết áp', N'Đang điều trị', N'Dùng thuốc huyết áp', 'NV001');

-- CTHDVatDung table
INSERT INTO CTHDVatDung (MaHoaDon, MaVatDung, SoLuong, ThanhTien) VALUES
('HD001', 'VD001', 10, 50000.00),
('HD002', 'VD002', 5, 750000.00),
('HD003', 'VD003', 2, 600000.00),
('HD004', 'VD004', 1, 1000000.00);

-- CTDonThuoc table
INSERT INTO CTDonThuoc (MaDonThuoc, MaThuoc, SoLuong, GiaTien, HuongDanSuDung, MaHoaDon) VALUES
('DT001', 'T001', 20, 40000.00, N'Uống 2 viên sau khi ăn', 'HD001'),
('DT002', 'T002', 15, 15000.00, N'Uống 1 viên mỗi sáng', 'HD002'),
('DT003', 'T003', 10, 50000.00, N'Uống 1 viên sau bữa tối', 'HD003'),
('DT004', 'T004', 5, 15000.00, N'Uống 1 viên sau khi ăn', 'HD004');

-- LichHenKham table
INSERT INTO LichHenKham (MaLichHen, MaBenhNhan, NgayHenKham, MaBacSi) VALUES
('LH001', 'BN001', '2024-02-01', 'NV002'),
('LH002', 'BN002', '2024-02-02', 'NV003'),
('LH003', 'BN003', '2024-02-03', 'NV004'),
('LH004', 'BN004', '2024-02-04', 'NV001');
