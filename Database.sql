CREATE DATABASE BV
USE BV


CREATE TABLE CTHDVatDung (
    MaHoaDon NVARCHAR(20),
    MaVatDung NVARCHAR(20),
    SoLuong INT,
    ThanhTien DECIMAL(10,2),
    PRIMARY KEY (MaHoaDon, MaVatDung)
);

CREATE TABLE VatDung (
    MaVatDung NVARCHAR(20) PRIMARY KEY,
    TenVatDung NVARCHAR(100),
    MoTa NTEXT,
    SoLuong INT,
    Gia DECIMAL(10,2),
    MaQuanLy NVARCHAR(20)
);

CREATE TABLE HoaDon (
    MaHoaDon NVARCHAR(20) PRIMARY KEY,
    TenHoaDon NVARCHAR(50),
    MaBenhNhan NVARCHAR(20),
    MaNhanVien NVARCHAR(20),
    NgayLapHoaDon DATE,
    GiaTien DECIMAL(10,2),
    TrangThai NVARCHAR(50)
);

CREATE TABLE Thuoc (
    MaThuoc NVARCHAR(20) PRIMARY KEY,
    TenThuoc NVARCHAR(100),
    CongDung NTEXT,
    SoLuong INT,
    GiaTien DECIMAL(10,2),
    HanSuDung DATE
);

CREATE TABLE CongViec (
    MaCongViec NVARCHAR(20) PRIMARY KEY,
    TenCongViec NVARCHAR(100),
    MoTaCongViec NTEXT,
    GhiChu NTEXT
);

CREATE TABLE LichTruc (
    MaLichTruc NVARCHAR(20) PRIMARY KEY,
    MaBacSi NVARCHAR(20),
    NgayTruc DATE,
    PhanCong NVARCHAR(20),
    TrangThai NVARCHAR(50)
);

CREATE TABLE Khoa (
    MaKhoa NVARCHAR(20) PRIMARY KEY,
    TenKhoa NVARCHAR(100),
    TruongKhoa NVARCHAR(20)
);

CREATE TABLE ChuyenNganh (
    MaChuyenNganh NVARCHAR(20) PRIMARY KEY,
    TenChuyenNganh NVARCHAR(100),
    Khoa NVARCHAR(20)
);

CREATE TABLE Role (
    RoleID NVARCHAR(20) PRIMARY KEY,
    TenRole NVARCHAR(50)
);

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
);

CREATE TABLE LichHenKham (
    MaLichHen NVARCHAR(20) PRIMARY KEY,
    MaBenhNhan NVARCHAR(20),
    NgayHenKham DATE,
    MaBacSi NVARCHAR(20)
);

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
);

CREATE TABLE BenhAn (
    MaBenhAn NVARCHAR(20) PRIMARY KEY,
    MaBenhNhan NVARCHAR(20),
    NgayTaoLap DATE,
    Benh NVARCHAR(20),
    TinhTrang NTEXT,
    DieuTri NTEXT
);

CREATE TABLE Benh (
    MaBenh NVARCHAR(20) PRIMARY KEY,
    TenBenh NVARCHAR(100),
    MoTa NTEXT,
    TrieuChung NTEXT
);

CREATE TABLE CTDonThuoc (
    MaDonThuoc NVARCHAR(20),
    MaThuoc NVARCHAR(20),
    SoLuong INT,
    GiaTien DECIMAL(10,2),
    HuongDanSuDung NTEXT,
    PRIMARY KEY (MaDonThuoc, MaThuoc)
);

CREATE TABLE DonThuoc (
    MaDonThuoc NVARCHAR(20) PRIMARY KEY,
    MaBenhNhan NVARCHAR(20),
    MaBacSi NVARCHAR(20),
    NgayLapDon DATE,
    MaHoaDon NVARCHAR(20)
);

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
);
GO

ALTER TABLE BenhNhan ADD FOREIGN KEY (MaKhoa) REFERENCES Khoa (MaKhoa)
GO

ALTER TABLE DonThuoc ADD FOREIGN KEY (MaHoaDon) REFERENCES HoaDon (MaHoaDon)
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

-- Vô hiệu hóa tất cả ràng buộc khóa ngoại
ALTER TABLE NhanVien NOCHECK CONSTRAINT ALL;
ALTER TABLE Khoa NOCHECK CONSTRAINT ALL;
ALTER TABLE ChuyenNganh NOCHECK CONSTRAINT ALL;

INSERT INTO Khoa (MaKhoa, TenKhoa, TruongKhoa) VALUES
('K01', N'Khoa Ngoại', N'NV002'),
('K02', N'Khoa Nội', N'NV003'),
('K03', N'Khoa Nhi', N'NV004'),
('K04', N'Khoa Da Liễu', N'NV005');



INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, Khoa) VALUES
('C01', N'Chuyên ngành Ngoại Tổng Quát', 'K01'),
('C02', N'Chuyên ngành Nội Tổng Quát', 'K02'),
('C03', N'Chuyên ngành Nhi khoa', 'K03'),
('C04', N'Chuyên ngành Da Liễu', 'K04'),
('C05', N'Chuyên ngành Tim Mạch', 'K01'),
('C06', N'Chuyên ngành Hồi Sức Cấp Cứu', 'K02'),
('C07', N'Chuyên ngành Xét Nghiệm', 'K03'),
('C08', N'Chuyên ngành Y Học Cổ Truyền','K04');

INSERT INTO NhanVien (MaNhanVien, Ho, Ten, MaChuyenNganh, RoleID, LoaiNhanVien, NgaySinh, GioiTinh, CCCD, DiaChi, SDT, Email, MatKhau) VALUES
('NV001', N'Nguyễn', N'An', 'C01', 'R01', N'Quản lý', '1980-01-01', N'Nam', '123456789', N'Hà Nội', '0912345678', 'an.nguyen@gmail.com', N'123'),
('NV002', N'Phạm', N'Bình', 'C02', 'R02', N'Bác sĩ', '1985-02-15', N'Nữ', '234567890', N'Thái Nguyên', '0912345679', 'binh.pham@gmail.com', N'123'),
('NV003', N'Lê', N'Cường', 'C03', 'R02', N'Bác sĩ', '1990-03-10', N'Nam', '345678901', N'Bắc Ninh', '0912345680', 'cuong.le@gmail.com', N'123'),
('NV004', N'Trần', N'Diệu', 'C04', 'R02', N'Bác sĩ', '1988-04-25', N'Nữ', '456789012', N'Hải Phòng', '0912345681', 'dieutran@gmail.com', N'123'),
('NV005', N'Nguyễn', N'Ela', 'C05', 'R02', N'Bác sĩ', '1992-05-05', N'Nữ', '567890123', N'Quảng Ninh', '0912345682', 'ela.nguyen@gmail.com', N'123'),
('NV006', N'Đoàn', N'Giang', 'C06', 'R02', N'Bác sĩ', '1987-06-18', N'Nam', '678901234', N'Nam Định', '0912345683', 'giang.doan@gmail.com', N'123'),
('NV007', N'Hồ', N'Hoài', 'C07', 'R02', N'Bác sĩ', '1995-07-30', N'Nữ', '789012345', N'Thừa Thiên Huế', '0912345684', 'hoai.ho@gmail.com', N'123'),
('NV008', N'Huỳnh', N'Thương', 'C08', 'R02', N'Bác sĩ', '1984-08-12', N'Nam', '890123456', N'Vũng Tàu', '0912345685', 'thuong.huynh@gmail.com', N'123');
-- Kích hoạt lại các ràng buộc khóa ngoại
ALTER TABLE NhanVien CHECK CONSTRAINT ALL;
ALTER TABLE Khoa CHECK CONSTRAINT ALL;
ALTER TABLE ChuyenNganh CHECK CONSTRAINT ALL;

INSERT INTO VatDung (MaVatDung, TenVatDung, MoTa, SoLuong, Gia, MaQuanLy) VALUES
('VD001', N'Bông Băng', N'Bông băng y tế dùng để cầm máu', 150, 65000, 'NV001'),
('VD002', N'Kim Tiêm', N'Kim tiêm tiêm thuốc', 180, 85000, 'NV002'),
('VD003', N'Ống nghe', N'Ống nghe tim, phổi cho bác sĩ', 120, 90000, 'NV003'),
('VD004', N'Găng tay', N'Găng tay y tế dùng trong phẫu thuật', 140, 70000, 'NV004'),
('VD005', N'Máy đo huyết áp', N'Máy đo huyết áp tự động', 100, 95000, 'NV005'),
('VD006', N'Máy x-ray', N'Máy chụp x-quang', 180, 85000, 'NV006'),
('VD007', N'Dụng cụ phẫu thuật', N'Dụng cụ dùng trong mổ', 200, 80000, 'NV007'),
('VD008', N'Máy trợ thở', N'Máy giúp thở cho bệnh nhân', 130, 100000, 'NV008');


INSERT INTO BenhNhan (MaBenhNhan, Ho, Ten, NgaySinh, GioiTinh, CCCD, NgheNghiep, DiaChi, SDT, Email, MaKhoa) VALUES
('BN001', N'Nguyễn', N'An', '1990-01-01', N'Nam', '123456789', N'Kỹ sư', N'Hà Nội', '0912345678', 'an.nguyen@gmail.com', 'K01'),
('BN002', N'Phạm', N'Bình', '1985-02-15', N'Nữ', '234567890', N'Giáo viên', N'Thái Nguyên', '0912345679', 'binh.pham@gmail.com', 'K02'),
('BN003', N'Lê', N'Cường', '1990-03-10', N'Nam', '345678901', N'Nhân viên văn phòng', N'Bắc Ninh', '0912345680', 'cuong.le@gmail.com', 'K03'),
('BN004', N'Trần', N'Diệu', '1988-04-25', N'Nữ', '456789012', N'Nhà báo', N'Hải Phòng', '0912345681', 'dieutran@gmail.com', 'K04'),
('BN005', N'Nguyễn', N'Ela', '1992-05-05', N'Nữ', '567890123', N'Tiếp viên hàng không', N'Quảng Ninh', '0912345682', 'ela.nguyen@gmail.com', 'K01'),
('BN006', N'Đoàn', N'Giang', '1987-06-18', N'Nam', '678901234', N'Bác sĩ', N'Nam Định', '0912345683', 'giang.doan@gmail.com', 'K01'),
('BN007', N'Hồ', N'Hoài', '1995-07-30', N'Nữ', '789012345', N'Sinh viên', N'Thừa Thiên Huế', '0912345684', 'hoai.ho@gmail.com', 'K01'),
('BN008', N'Huỳnh', N'Thương', '1984-08-12', N'Nam', '890123456', N'Nông dân', N'Vũng Tàu', '0912345685', 'thuong.huynh@gmail.com', 'K01'),
('BN009', N'Nguyễn', N'Tuấn', '1993-09-20', N'Nam', '123456790', N'Kỹ sư', N'TP Hồ Chí Minh', '0912345686', 'tuan.nguyen@gmail.com', 'K01'),
('BN010', N'Phan', N'Lan', '1990-10-10', N'Nữ', '234567891', N'Giáo viên', N'Vĩnh Phúc', '0912345687', 'lan.phan@gmail.com', 'K02'),
('BN011', N'Lê', N'Trâm', '1994-11-11', N'Nữ', '345678902', N'Nhân viên ngân hàng', N'Tp Hồ Chí Minh', '0912345688', 'tram.le@gmail.com', 'K03'),
('BN012', N'Trần', N'Quang', '1986-12-05', N'Nam', '456789013', N'Nhà báo', N'Hà Nội', '0912345689', 'quang.tran@gmail.com', 'K03'),
('BN013', N'Nguyễn', N'Phúc', '1989-02-22', N'Nam', '567890124', N'Tiếp viên hàng không', N'Đà Nẵng', '0912345690', 'phuc.nguyen@gmail.com', 'K03'),
('BN014', N'Đoàn', N'Lan', '1985-03-15', N'Nữ', '678901235', N'Bác sĩ', N'Quảng Nam', '0912345691', 'lan.doan@gmail.com', 'K03'),
('BN015', N'Hồ', N'Giang', '1996-04-25', N'Nữ', '789012346', N'Sinh viên', N'Thái Nguyên', '0912345692', 'giang.ho@gmail.com', 'K03'),
('BN016', N'Huỳnh', N'Phong', '1983-06-30', N'Nam', '890123457', N'Nông dân', N'Bình Thuận', '0912345693', 'phong.huynh@gmail.com', 'K04'),
('BN017', N'Nguyễn', N'Tri', '1991-07-10', N'Nam', '123456791', N'Kỹ sư', N'TP Hồ Chí Minh', '0912345694', 'tri.nguyen@gmail.com', 'K01'),
('BN018', N'Phan', N'Anh', '1992-08-15', N'Nữ', '234567892', N'Giáo viên', N'TP Đà Nẵng', '0912345695', 'anh.phan@gmail.com', 'K02'),
('BN019', N'Lê', N'Phúc', '1988-05-20', N'Nam', '345678903', N'Nhân viên văn phòng', N'Quảng Ngãi', '0912345696', 'phuc.le@gmail.com', 'K03'),
('BN020', N'Trần', N'Thu', '1994-09-18', N'Nữ', '456789014', N'Nhà báo', N'Hồ Chí Minh', '0912345697', 'thu.tran@gmail.com', 'K04'),
('BN021', N'Nguyễn', N'Triệu', '1993-06-25', N'Nam', '567890125', N'Tiếp viên hàng không', N'Tp Hà Nội', '0912345698', 'trieu.nguyen@gmail.com', 'K02'),
('BN022', N'Đoàn', N'Khanh', '1987-11-11', N'Nam', '678901236', N'Bác sĩ', N'Thái Bình', '0912345699', 'khanh.doan@gmail.com', 'K01'),
('BN023', N'Hồ', N'Anh', '1990-03-05', N'Nữ', '789012347', N'Sinh viên', N'Quảng Bình', '0912345700', 'anh.ho@gmail.com', 'K01'),
('BN024', N'Huỳnh', N'Quyên', '1985-02-14', N'Nữ', '890123458', N'Nông dân', N'Bến Tre', '0912345701', 'quyen.huynh@gmail.com', 'K01');

INSERT INTO HoaDon (MaHoaDon, TenHoaDon, MaBenhNhan, MaNhanVien, NgayLapHoaDon, GiaTien, TrangThai) VALUES
('HD001', N'Hóa Đơn 01', 'BN001', 'NV001', '2024-12-01', 500.00, N'Chưa thanh toán'),
('HD002', N'Hóa Đơn 02', 'BN002', 'NV002', '2024-12-02', 200.00, N'Đã thanh toán'),
('HD003', N'Hóa Đơn 03', 'BN003', 'NV003', '2024-12-03', 800.00, N'Chưa thanh toán'),
('HD004', N'Hóa Đơn 04', 'BN004', 'NV004', '2024-12-04', 400.00, N'Đã thanh toán'),
('HD005', N'Hóa Đơn 05', 'BN005', 'NV005', '2024-12-05', 1000.00, N'Chưa thanh toán'),
('HD006', N'Hóa Đơn 06', 'BN006', 'NV006', '2024-12-06', 300.00, N'Đã thanh toán'),
('HD007', N'Hóa Đơn 07', 'BN007', 'NV007', '2024-12-07', 700.00, N'Chưa thanh toán'),
('HD008', N'Hóa Đơn 08', 'BN008', 'NV008', '2024-12-08', 600.00, N'Đã thanh toán'),
('HD009', N'Hóa Đơn 09', 'BN009', 'NV001', '2024-07-01', 600.00, N'Đã thanh toán'),
('HD010', N'Hóa Đơn 10', 'BN010', 'NV002', '2024-08-02', 500.00, N'Chưa thanh toán'),
('HD011', N'Hóa Đơn 11', 'BN011', 'NV003', '2024-09-03', 400.00, N'Đã thanh toán'),
('HD012', N'Hóa Đơn 12', 'BN012', 'NV004', '2024-10-04', 900.00, N'Chưa thanh toán'),
('HD013', N'Hóa Đơn 13', 'BN013', 'NV005', '2024-11-05', 1000.00, N'Đã thanh toán'),
('HD014', N'Hóa Đơn 14', 'BN014', 'NV006', '2024-11-06', 700.00, N'Chưa thanh toán'),
('HD015', N'Hóa Đơn 15', 'BN015', 'NV007', '2024-07-10', 700.00, N'Chưa thanh toán'),
('HD016', N'Hóa Đơn 16', 'BN016', 'NV008', '2024-08-12', 850.00, N'Đã thanh toán'),
('HD017', N'Hóa Đơn 17', 'BN017', 'NV001', '2024-09-15', 550.00, N'Chưa thanh toán'),
('HD018', N'Hóa Đơn 18', 'BN018', 'NV002', '2024-10-17', 1200.00, N'Chưa thanh toán'),
('HD019', N'Hóa Đơn 19', 'BN019', 'NV003', '2024-11-18', 900.00, N'Đã thanh toán'),
('HD020', N'Hóa Đơn 20', 'BN020', 'NV004', '2024-11-19', 950.00, N'Chưa thanh toán'),
('HD021', N'Hóa Đơn 21', 'BN020', 'NV005', '2024-07-15', 450.00, N'Chưa thanh toán'),
('HD022', N'Hóa Đơn 22', 'BN019', 'NV006', '2024-08-18', 700.00, N'Đã thanh toán'),
('HD023', N'Hóa Đơn 23', 'BN024', 'NV007', '2024-09-20', 650.00, N'Chưa thanh toán'),
('HD024', N'Hóa Đơn 24', 'BN023', 'NV008', '2024-10-21', 1300.00, N'Chưa thanh toán'),
('HD025', N'Hóa Đơn 25', 'BN022', 'NV001', '2024-11-22', 950.00, N'Đã thanh toán'),
('HD026', N'Hóa Đơn 26', 'BN021', 'NV002', '2024-11-23', 800.00, N'Chưa thanh toán');

INSERT INTO Thuoc (MaThuoc, TenThuoc, CongDung, SoLuong, GiaTien, HanSuDung) VALUES
('T001', N'Paracetamol', N'Giảm đau, hạ sốt', 150, 60000, '2025-12-01'),
('T002', N'Aspirin', N'Giảm đau, chống viêm', 180, 85000, '2026-06-15'),
('T003', N'Amoxicillin', N'Kháng sinh điều trị nhiễm khuẩn', 12, 95000, '2025-09-01'),
('T004', N'Metformin', N'Điều trị tiểu đường', 200, 80000, '2027-03-20'),
('T005', N'Ibuprofen', N'Giảm đau, kháng viêm', 100, 100000, '2025-05-10'),
('T006', N'Loratadine', N'Thuốc chống dị ứng', 140, 55000, '2026-11-12'),
('T007', N'Omeprazole', N'Thuốc trị dạ dày', 190, 75000, '2026-02-01'),
('T008', N'Ciprofloxacin', N'Kháng sinh', 160, 90000, '2025-08-25');

INSERT INTO CongViec (MaCongViec, TenCongViec, MoTaCongViec, GhiChu) VALUES
('CV001', N'Phẫu thuật', N'Phẫu thuật điều trị bệnh nhân', N'Phải có sự chuẩn bị kỹ lưỡng'),
('CV002', N'Khám bệnh', N'Khám bệnh và xác định bệnh', N'Chú ý lịch hẹn'),
('CV003', N'Thuốc', N'Phát thuốc cho bệnh nhân', N'Đảm bảo đủ số lượng thuốc'),
('CV004', N'Triển khai xét nghiệm', N'Xét nghiệm chẩn đoán bệnh', N'Kiểm tra chính xác'),
('CV005', N'Chăm sóc bệnh nhân', N'Chăm sóc bệnh nhân sau phẫu thuật', N'Cần theo dõi thường xuyên'),
('CV006', N'Thăm khám tại nhà', N'Thăm khám bệnh nhân tại nhà', N'Phải mang đầy đủ dụng cụ y tế'),
('CV007', N'Thực hiện phẫu thuật', N'Phẫu thuật điều trị bệnh nhân', N'Chỉ thực hiện sau khi hội chẩn'),
('CV008', N'Xét nghiệm chẩn đoán', N'Chẩn đoán bệnh thông qua xét nghiệm', N'Báo cáo kết quả ngay sau khi có');

INSERT INTO LichTruc (MaLichTruc, MaBacSi, NgayTruc, PhanCong, TrangThai) VALUES
('LT001', 'NV001', '2024-12-01', N'CV001', N'Hoàn thành'),
('LT002', 'NV002', '2024-12-02', N'CV002', N'Hoàn thành'),
('LT003', 'NV003', '2024-12-03', N'CV003', N'Chưa hoàn thành'),
('LT004', 'NV004', '2024-12-04', N'CV004', N'Hoàn thành'),
('LT005', 'NV005', '2024-12-05', N'CV005', N'Chưa hoàn thành'),
('LT006', 'NV006', '2024-12-06', N'CV006', N'Hoàn thành'),
('LT007', 'NV007', '2024-12-07', N'CV007', N'Chưa hoàn thành'),
('LT008', 'NV008', '2024-12-08', N'CV008', N'Hoàn thành');

INSERT INTO Benh (MaBenh, TenBenh, MoTa, TrieuChung) VALUES
('B001', N'Cảm cúm', N'Bệnh do virus gây ra, thường gây sốt và đau họng', N'Sốt, ho, đau họng'),
('B002', N'Viêm phổi', N'Viêm phổi do vi khuẩn hoặc virus', N'Khó thở, sốt'),
('B003', N'Tiêu chảy', N'Rối loạn tiêu hóa gây tiêu chảy', N'Tiêu chảy, đau bụng'),
('B004', N'Tăng huyết áp', N'Áp lực máu cao liên tục', N'Đau đầu, chóng mặt'),
('B005', N'Đau dạ dày', N'Viêm loét dạ dày', N'Đau bụng, khó tiêu'),
('B006', N'Viêm gan B', N'Viêm gan do virus B', N'Vàng da, mệt mỏi'),
('B007', N'Mãn tính', N'Bệnh không thể chữa khỏi', N'Khó thở, đau ngực'),
('B008', N'Meo mỡ máu', N'Rối loạn cholesterol', N'Đau ngực, chóng mặt');


INSERT INTO BenhAn (MaBenhAn, MaBenhNhan, NgayTaoLap, Benh, TinhTrang, DieuTri) VALUES
('BA001', 'BN001', '2024-12-01', N'B001', N'Thuyên giảm', N'Paracetamol, nghỉ ngơi'),
('BA002', 'BN002', '2024-12-02', N'B002', N'Đang điều trị', N'Kháng sinh, oxy'),
('BA003', 'BN003', '2024-12-03', N'B003', N'Thuyên giảm', N'Oresol, nghỉ ngơi'),
('BA004', 'BN004', '2024-12-04', N'B004', N'Đang điều trị', N'Thuốc hạ huyết áp'),
('BA005', 'BN005', '2024-12-05', N'B005', N'Đang điều trị', N'Thuốc dạ dày'),
('BA006', 'BN006', '2024-12-06', N'B006', N'Đang điều trị', N'Kháng virus'),
('BA007', 'BN007', '2024-12-07', N'B007', N'Ổn định', N'Tập thể dục thường xuyên'),
('BA008', 'BN008', '2024-12-08', N'B008', N'Đang điều trị', N'Chế độ ăn kiêng'); --Lỗi

INSERT INTO CTHDVatDung (MaHoaDon, MaVatDung, SoLuong, ThanhTien) VALUES
('HD001', 'VD001', 15, 975000),
('HD002', 'VD002', 12, 1020000),
('HD003', 'VD003', 10, 900000),
('HD004', 'VD004', 14, 980000),
('HD005', 'VD005', 15, 1425000),
('HD006', 'VD006', 19, 1615000),
('HD007', 'VD007', 18, 1440000),
('HD008', 'VD008', 20, 2000000),
('HD009', 'VD001', 10, 650000),
('HD010', 'VD002', 12, 1020000),
('HD011', 'VD003', 15, 1350000),
('HD012', 'VD004', 14, 980000),
('HD013', 'VD005', 16, 1520000),
('HD014', 'VD006', 17, 1445000),
('HD021', 'VD007', 13, 1040000),
('HD022', 'VD008', 16, 1600000);


INSERT INTO DonThuoc (MaDonThuoc, MaBenhNhan, MaBacSi, NgayLapDon, MaHoaDon) VALUES
('DT001', 'BN001', 'NV001', '2024-11-01', 'HD001'),
('DT002', 'BN002', 'NV002', '2024-08-02', 'HD002'),
('DT003', 'BN003', 'NV003', '2024-10-03', 'HD003'),
('DT004', 'BN004', 'NV004', '2024-09-04', 'HD004'),
('DT005', 'BN005', 'NV005', '2024-12-05', 'HD005'),
('DT006', 'BN006', 'NV006', '2024-12-06', 'HD006'),
('DT007', 'BN007', 'NV007', '2024-12-07', 'HD007'),
('DT008', 'BN008', 'NV008', '2024-12-08', 'HD008'),
('DT009', 'BN009', 'NV001', '2024-07-01', 'HD009'),
('DT010', 'BN010', 'NV002', '2024-10-02', 'HD010'), 
('DT011', 'BN011', 'NV003', '2024-09-03', 'HD011'),
('DT012', 'BN012', 'NV004', '2024-10-04', 'HD012'),
('DT013', 'BN013', 'NV005', '2024-11-05', 'HD013'),
('DT014', 'BN014', 'NV006', '2024-11-06', 'HD014'),
('DT015', 'BN015', 'NV007', '2024-07-10', 'HD015'),
('DT016', 'BN016', 'NV008', '2024-08-12', 'HD016'),
('DT017', 'BN017', 'NV001', '2024-09-15', 'HD017'),
('DT018', 'BN018', 'NV002', '2024-10-17', 'HD018'),
('DT019', 'BN019', 'NV003', '2024-11-18', 'HD019'),
('DT020', 'BN020', 'NV004', '2024-11-19', 'HD020'),
('DT021', 'BN021', 'NV005', '2024-07-15', 'HD021'),
('DT022', 'BN022', 'NV006', '2024-10-18', 'HD022'),
('DT023', 'BN023', 'NV007', '2024-09-20', 'HD023'),
('DT024', 'BN024', 'NV008', '2024-10-21', 'HD024'),
('DT025', 'BN022', 'NV001', '2024-11-22', 'HD025'),
('DT026', 'BN023', 'NV002', '2024-11-23', 'HD026');

INSERT INTO CTDonThuoc (MaDonThuoc, MaThuoc, SoLuong, GiaTien, HuongDanSuDung) VALUES
('DT001', 'T001', 15, 15 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T001'), N'Uống 1 viên mỗi 4 giờ'),
('DT002', 'T002', 20, 20 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T002'), N'Uống 1 viên mỗi 6 giờ'),
('DT003', 'T003', 10, 10 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T003'), N'Uống 1 viên mỗi ngày'),
('DT004', 'T004', 18, 18 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T004'), N'Uống 2 viên mỗi ngày'),
('DT005', 'T005', 12, 12 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T005'), N'Uống 1 viên mỗi 8 giờ'),
('DT006', 'T006', 15, 15 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T006'), N'Uống 1 viên mỗi ngày vào buổi sáng'),
('DT007', 'T007', 10, 10 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T007'), N'Uống 1 viên trước bữa ăn'),
('DT008', 'T008', 17, 17 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T008'), N'Uống 1 viên mỗi 12 giờ'),
('DT009', 'T001', 14, 14 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T001'), N'Uống 1 viên mỗi ngày trước khi ăn'),
('DT010', 'T002', 12, 12 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T002'), N'Uống 1 viên mỗi ngày sau bữa ăn'),
('DT011', 'T003', 18, 18 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T003'), N'Uống 1 viên mỗi ngày vào sáng và tối'),
('DT012', 'T004', 15, 15 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T004'), N'Uống 1 viên mỗi ngày vào buổi sáng'),
('DT013', 'T005', 10, 10 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T005'), N'Uống 1 viên mỗi 6 giờ'),
('DT014', 'T006', 16, 16 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T006'), N'Uống 2 viên mỗi 4 giờ'),
('DT015', 'T007', 12, 12 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T007'), N'Uống 1 viên mỗi ngày sau bữa ăn sáng'),
('DT016', 'T008', 15, 15 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T008'), N'Uống 2 viên mỗi ngày vào buổi sáng và tối'),
('DT017', 'T002', 18, 18 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T002'), N'Uống 1 viên mỗi 4 giờ'),
('DT018', 'T001', 20, 20 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T001'), N'Uống 1 viên mỗi ngày trước khi ngủ'),
('DT019', 'T003', 15, 15 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T003'), N'Uống 1 viên mỗi ngày sau bữa trưa'),
('DT020', 'T004', 18, 18 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T004'), N'Uống 1 viên mỗi ngày vào buổi chiều'),
('DT021', 'T005', 14, 14 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T005'), N'Uống 1 viên mỗi ngày trước khi ăn sáng'),
('DT022', 'T006', 16, 16 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T006'), N'Uống 2 viên mỗi ngày vào buổi sáng và tối'),
('DT023', 'T007', 14, 14 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T007'), N'Uống 1 viên mỗi 4 giờ'),
('DT024', 'T001', 15, 15 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T001'), N'Uống 1 viên mỗi ngày trước khi ngủ'),
('DT025', 'T008', 18, 18 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T008'), N'Uống 1 viên mỗi ngày sau bữa trưa'),
('DT026', 'T001', 14, 14 * (SELECT GiaTien FROM Thuoc WHERE MaThuoc = 'T001'), N'Uống 1 viên mỗi ngày vào buổi chiều');

INSERT INTO LichHenKham (MaLichHen, MaBenhNhan, NgayHenKham, MaBacSi) VALUES
('LH001', 'BN001', '2024-12-10', 'NV001'),
('LH002', 'BN002', '2024-12-11', 'NV002'),
('LH003', 'BN003', '2024-12-12', 'NV003'),
('LH004', 'BN004', '2024-12-13', 'NV004'),
('LH005', 'BN005', '2024-12-14', 'NV005'),
('LH006', 'BN006', '2024-12-15', 'NV006'),
('LH007', 'BN007', '2024-12-16', 'NV007'),
('LH008', 'BN008', '2024-12-17', 'NV008');

INSERT INTO PhieuKhamBenh (MaPhieuKham, MaBenhNhan, NgayKham, LyDoKhamBenh, KhamLamSang, ChanDoan, KetQuaKham, DieuTri, MaBacSi) VALUES
('PKB001', 'BN001', '2024-12-01', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm họng', N'Paracetamol, nghỉ ngơi', N'Điều trị triệu chứng', 'NV002'),
('PKB002', 'BN002', '2024-12-07', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm phổi', N'Kháng sinh, oxy', N'Điều trị kháng sinh', 'NV002'),
('PKB003', 'BN003', '2024-12-02', N'Khám sức khỏe', N'Khám tổng quát', N'Tiêu chảy', N'Oresol, nghỉ ngơi', N'Điều trị triệu chứng', 'NV003'),
('PKB004', 'BN004', '2024-12-02', N'Khám sức khỏe', N'Khám tổng quát', N'Tăng huyết áp', N'Thuốc hạ huyết áp', N'Điều trị huyết áp', 'NV004'),
('PKB005', 'BN005', '2024-12-03', N'Khám sức khỏe', N'Khám tổng quát', N'Đau dạ dày', N'Thuốc dạ dày', N'Điều trị dạ dày', 'NV005'),
('PKB006', 'BN006', '2024-12-06', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm gan B', N'Kháng virus', N'Điều trị viêm gan', 'NV006'),
('PKB007', 'BN007', '2024-12-07', N'Khám sức khỏe', N'Khám tổng quát', N'Mãn tính', N'Tập thể dục', N'Trị liệu', 'NV007'),
('PKB008', 'BN008', '2024-12-04', N'Khám sức khỏe', N'Khám tổng quát', N'Meo mỡ máu', N'Chế độ ăn kiêng', N'Điều trị mỡ máu', 'NV008'),
('PKB009', 'BN001', '2024-12-01', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm họng', N'Paracetamol, nghỉ ngơi', N'Điều trị triệu chứng', 'NV002'),
('PKB010', 'BN002', '2024-12-02', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm phổi', N'Kháng sinh, oxy', N'Điều trị kháng sinh', 'NV002'),
('PKB011', 'BN003', '2024-12-03', N'Khám sức khỏe', N'Khám tổng quát', N'Tiêu chảy', N'Oresol, nghỉ ngơi', N'Điều trị triệu chứng', 'NV003'),
('PKB012', 'BN004', '2024-12-03', N'Khám sức khỏe', N'Khám tổng quát', N'Tăng huyết áp', N'Thuốc hạ huyết áp', N'Điều trị huyết áp', 'NV004'),
('PKB013', 'BN005', '2024-12-04', N'Khám sức khỏe', N'Khám tổng quát', N'Đau dạ dày', N'Thuốc dạ dày', N'Điều trị dạ dày', 'NV005'),
('PKB014', 'BN006', '2024-12-04', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm gan B', N'Kháng virus', N'Điều trị viêm gan', 'NV006'),
('PKB015', 'BN007', '2024-12-05', N'Khám sức khỏe', N'Khám tổng quát', N'Mãn tính', N'Tập thể dục', N'Trị liệu', 'NV007'),
('PKB016', 'BN008', '2024-12-05', N'Khám sức khỏe', N'Khám tổng quát', N'Meo mỡ máu', N'Chế độ ăn kiêng', N'Điều trị mỡ máu', 'NV008'),
('PKB017', 'BN001', '2024-12-06', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm họng', N'Paracetamol, nghỉ ngơi', N'Điều trị triệu chứng', 'NV002'),
('PKB018', 'BN002', '2024-12-06', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm phổi', N'Kháng sinh, oxy', N'Điều trị kháng sinh', 'NV002'),
('PKB019', 'BN003', '2024-12-07', N'Khám sức khỏe', N'Khám tổng quát', N'Tiêu chảy', N'Oresol, nghỉ ngơi', N'Điều trị triệu chứng', 'NV003'),
('PKB020', 'BN004', '2024-12-07', N'Khám sức khỏe', N'Khám tổng quát', N'Tăng huyết áp', N'Thuốc hạ huyết áp', N'Điều trị huyết áp', 'NV004'),
('PKB021', 'BN005', '2024-12-01', N'Khám sức khỏe', N'Khám tổng quát', N'Đau dạ dày', N'Thuốc dạ dày', N'Điều trị dạ dày', 'NV005'),
('PKB022', 'BN006', '2024-12-02', N'Khám sức khỏe', N'Khám tổng quát', N'Viêm gan B', N'Kháng virus', N'Điều trị viêm gan', 'NV006'),
('PKB023', 'BN007', '2024-12-03', N'Khám sức khỏe', N'Khám tổng quát', N'Mãn tính', N'Tập thể dục', N'Trị liệu', 'NV007'),
('PKB024', 'BN008', '2024-12-04', N'Khám sức khỏe', N'Khám tổng quát', N'Meo mỡ máu', N'Chế độ ăn kiêng', N'Điều trị mỡ máu', 'NV008');
