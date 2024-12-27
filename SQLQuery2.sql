

create table Huyen(
	HuyenID int not null primary key,
	TenHuyen varchar(max),
	SoLuongNguoiDung int
);

create table Xa (
	XaID int not null primary key,
	TenXa varchar(max),
	SoLuongNguoiDung int,
	HuyenID int,
	FOREIGN KEY (HuyenID) REFERENCES Huyen(HuyenID)
);
create table NguoiDung (
	NguoiDungID int not null primary key,
	Ten varchar(max),
	PhanQuyen int not null,
	TrangThai varchar(max),
	Email varchar(max) not null,
	MaKhau varchar(max) not null,
	SDT varchar(max) not null,
	NgayTao Date,
	GioTao Time,
	XaID int not null,
	DiaChiNha varchar(max),
	FOREIGN KEY (XaID) REFERENCES Xa(XaID)
);
create table LichSuTacDong(
	TacDongID int primary key,
	NguoiDungID int,
	NgayGhiNhan Date,
	GioGhiNhan Time,
	LoaiThaoTac varchar(max),
	DoiTuongBiTacDong varchar(max),
	Truoc varchar(max),
	Sau varchar(max),
	GhiChu varchar(max),
	KetQua varchar(max),
	FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID)
);
create table LichSuTruyCap(
	TruyCapID int primary key,
	NguoiDungID int,
	FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(NguoiDungID),
	NgayDangNhap Date,
	GioDangNhap Time,
	NgayDangXuat Date,
	GioDangXuat Time,
	TrangThai varchar(max)
);

