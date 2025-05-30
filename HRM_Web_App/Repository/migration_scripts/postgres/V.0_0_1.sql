CREATE DATABASE "HRM" WITH OWNER "khai_TR";
\c "HRM" "khai_TR";
CREATE TABLE NhanVien (
                          NhanVienID VARCHAR(50) PRIMARY KEY,
                          TenDangNhap VARCHAR(100) NOT NULL,
                          Email VARCHAR(100) NOT NULL,
                          VaiTro TEXT[] NOT NULL,
                          TenNhanVien VARCHAR(100),
                          NgaySinh DATE,
                          GioiTinh VARCHAR(10),
                          DiaChiThuongTru VARCHAR(200),
                          NoiOHienTai VARCHAR(200),
                          SDT VARCHAR(20),
                          SDTNguoiThan VARCHAR(20),
                          NgayBatDauLamViec DATE,
                          DiemThuong INT DEFAULT 0,
                          MaSoThue VARCHAR(50),
                          CCCD VARCHAR(50),
                          SoNgayNghiCoPhepConLai INT,
                          SoNgayNghiKhongPhep INT,
                          LuongCoBan INT,
                          NgayHetHanCCCD DATE,
                          TienPhat INT,
                          LinkHinh VARCHAR(255),
                          QuanLyID VARCHAR(50),
                          CONSTRAINT fk_quanly FOREIGN KEY (QuanLyID) REFERENCES NhanVien(NhanVienID)
);
CREATE TABLE KinhNghiemLamViec (
                                   Id SERIAL PRIMARY KEY,
                                   ViTri VARCHAR(100),
                                   NgayBatDau DATE,
                                   NgayKetThuc DATE,
                                   ThoiGianLamViec INT,
                                   MoTaCongViec TEXT,
                                   NhanVienID VARCHAR(50) NOT NULL,
                                   CONSTRAINT fk_nv_kn FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
);
CREATE TABLE BangCap (
                         MaBang VARCHAR(50) NOT NULL,
                         TenBang VARCHAR(100) NOT NULL,
                         NgayCap DATE,
                         SDTDonViCap VARCHAR(20),
                         DonViCap VARCHAR(100),
                         HocVi VARCHAR(50),
                         GPA REAL,
                         DTB REAL,
                         NhanVienID VARCHAR(50) NOT NULL,
                         PRIMARY KEY (MaBang, NhanVienID),
                         CONSTRAINT fk_nv_bc FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
);
CREATE TABLE TTNganHang (
                            STKNganHang VARCHAR(50) NOT NULL,
                            TenNganHang VARCHAR(100) NOT NULL,
                            ChiNhanh VARCHAR(100) NOT NULL,
                            Active BOOLEAN DEFAULT FALSE,
                            NhanVienID VARCHAR(50) NOT NULL,
                            PRIMARY KEY (STKNganHang, NhanVienID),
                            CONSTRAINT fk_nv_tk FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
);
CREATE TABLE VaiTroNhanVien (
                                NhanVienID VARCHAR(50) NOT NULL,
                                VaiTro VARCHAR(50) NOT NULL,
                                PRIMARY KEY (NhanVienID, VaiTro),
                                CONSTRAINT fk_nv_vaitro FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
);
