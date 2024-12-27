-- Tạo database QUANLYTAINGUYENRUNG3
CREATE DATABASE QUANLYTAINGUYENRUNG3;
GO

-- Sử dụng database
USE QUANLYTAINGUYENRUNG3;
GO

-- Tạo bảng huyện
CREATE TABLE DISTRICT (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL
);
GO

-- Tạo bảng xã
CREATE TABLE COMMUNE (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
    ID_DISTRICT INT NOT NULL,
    FOREIGN KEY (ID_DISTRICT) REFERENCES DISTRICT(ID)
);
GO

-- Tạo bảng phân quyền nhóm người dùng
CREATE TABLE ROLES_GROUP (
    ID INT IDENTITY(1,1) PRIMARY KEY,
	ROLES BIT, -- O USER 1 ADMIN
);
GO

-- Tạo bảng vai trò (user, admin)
CREATE TABLE ROLES (
    ID INT IDENTITY(1,1) PRIMARY KEY,
	ROLES BIT, -- O USER 1 ADMIN
);
GO

-- Tạo bảng nhóm người dùng (kỹ sư, chuyên viên, kiểm lâm,...)
CREATE TABLE GROUPS (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
	STATUS BIT NOT NULL, -- TRẠNG THÁI 0 OFF TRẠNG THÁI 1 ON  
	ID_ROLES_GROUP INT, 
    FOREIGN KEY (ID_ROLES_GROUP) REFERENCES ROLES_GROUP(ID),
);
GO

-- Tạo bảng người dùng
CREATE TABLE [USER] (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
	STATUS BIT, --TRẠNG THÁI 0 OFF TRẠNG THÁI 1 ON 
    EMAIL VARCHAR(100),
    PHONE VARCHAR(100) NOT NULL,
	DISPLAY_NAME NVARCHAR(100) DEFAULT 'NO NAME',
    USER_NAME NVARCHAR(100) NOT NULL,
    PASSWORD NVARCHAR(100) NOT NULL,
    ID_COMMUNE INT NOT NULL,
    ID_ROLES INT NOT NULL,
    FOREIGN KEY (ID_ROLES) REFERENCES ROLES(ID),
    FOREIGN KEY (ID_COMMUNE) REFERENCES COMMUNE(ID)
);
GO

-- Tạo bảng trung gian mối quan hệ của user và groups 
CREATE TABLE USER_GROUP (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ID_USER INT NOT NULL,
    ID_GROUP INT NOT NULL,
    FOREIGN KEY (ID_USER) REFERENCES [USER](ID),
    FOREIGN KEY (ID_GROUP) REFERENCES GROUPS(ID)
);
GO

-- Tạo bảng lịch sử tác động
CREATE TABLE IMPACT_HISTORY (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TYPE NVARCHAR(100) NOT NULL, --Loại tác động
    TIME TIME,
    DAY DATE,
	ID_USER INT,
	FOREIGN KEY (ID_USER) REFERENCES [USER](ID)

);
GO

-- Tạo bảng lịch sử truy cập
CREATE TABLE ACCESS_HISTORY (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TYPE NVARCHAR(100) NOT NULL, -- Loại truy cập
    TIME TIME,
    DAY DATE,	
	ID_USER INT,
	FOREIGN KEY (ID_USER) REFERENCES [USER](ID)
);
GO

-- Tạo bảng cơ sở sản xuất giống cây trồng
CREATE TABLE PLANT_BREEDING_FACILITY (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
	STATUS BIT NOT NULL, -- 0 OFF 1 ON 
    ADDRESS NVARCHAR(255) NOT NULL, -- ĐỊA CHỈ
    CONTACT_FACE NVARCHAR(100) NOT NULL, -- THÔNG TIN LIÊN HỆ
	CONTACT_MAIL NVARCHAR(100) NOT NULL, 
	CONTACT_PHONE NVARCHAR(100) NOT NULL,
	ACREAGE FLOAT NOT NULL, -- DIỆN TÍCH 
    SEEDLINGS_YIELD INT NOT NULL, -- SẢN LƯỢNG CÂY GIỐNG 
	LABOR INT NOT NULL, -- SỐ NHÂN CÔNG 
	IMAGE_LANT_BREEDING_FACILITY VARBINARY(MAX), -- bản đồ cơ sở sản xuất giống cây
    ID_COMMUNE INT NOT NULL,
    FOREIGN KEY (ID_COMMUNE) REFERENCES COMMUNE(ID)
);
GO

-- Tạo bảng danh mục loại giống cây trồng
CREATE TABLE PLANT_VARIETY_TYPE (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
	TYPE NVARCHAR(100) NOT NULL, --LOẠI GIỐNG CÂY
	PRICE INT NOT NULL, -- GIÁ
    HEIGHT INT NOT NULL, -- CHIỀU CAO CÂY GIỐNG 
    ID_PLANT_BREEDING_FACILITY INT NOT NULL,
    FOREIGN KEY (ID_PLANT_BREEDING_FACILITY) REFERENCES PLANT_BREEDING_FACILITY(ID)
);
GO

-- Tạo bảng cơ sở sản xuất chế biến gỗ
CREATE TABLE WOOD_PROCESSING_FACILITY (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
	STATUS BIT NOT NULL, -- 0 OFF 1 ON 
    ADDRESS NVARCHAR(255) NOT NULL,
    CONTACT_FACE NVARCHAR(100) NOT NULL, -- THÔNG TIN LIÊN HỆ
	CONTACT_MAIL NVARCHAR(100) NOT NULL, 
	CONTACT_PHONE NVARCHAR(100) NOT NULL,
	LABOR INT NOT NULL, -- SỐ NHÂN CÔNG 
	ACREAGE FLOAT NOT NULL, -- DIỆN TÍCH HECTA
	Yield FLOAT, -- SẢN LƯỢNG TẤN / NĂM
	WOOD_SPECIES_PROVIDED NVARCHAR(500), -- LOẠI GỖ ĐẦU VÀO 
	PRODUCT NVARCHAR(300) NOT NULL, -- SẢN PHẨM 
	IMAGE_WOOD_PROCESSING_FACILITY VARBINARY(MAX), -- bản đồ cơ sở CHẾ BIẾN GỖ 
    ID_COMMUNE INT NOT NULL,
    FOREIGN KEY (ID_COMMUNE) REFERENCES COMMUNE(ID)
);
GO

-- Tạo bảng danh mục loại hình sản xuất
CREATE TABLE PRODUCTION_TYPE (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
    ID_WOOD_PROCESSING_FACILITY INT NOT NULL,
    FOREIGN KEY (ID_WOOD_PROCESSING_FACILITY) REFERENCES WOOD_PROCESSING_FACILITY(ID)
);
GO

-- Tạo bảng danh mục hình thức hoạt động
CREATE TABLE ACTIVITY_FORM (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
    ID_WOOD_PROCESSING_FACILITY INT NOT NULL,
    FOREIGN KEY (ID_WOOD_PROCESSING_FACILITY) REFERENCES WOOD_PROCESSING_FACILITY(ID)
);
GO

-- Tạo bảng tài nguyên, sinh vật rừng 
--CREATE TABLE ANIMAL_RESOURCE  (
--    ID INT IDENTITY(1,1) PRIMARY KEY,
--    NAME NVARCHAR(100) NOT NULL,
--    ID_COMMUNE INT NOT NULL,
--    FOREIGN KEY (ID_COMMUNE) REFERENCES COMMUNE(ID)
--);
--GO

-- Tạo bảng cơ sở lưu trữ động vật
CREATE TABLE ANIMAL_STORAGE (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
    ADDRESS NVARCHAR(255),
    CONTACT_FACE NVARCHAR(100) NOT NULL, -- THÔNG TIN LIÊN HỆ
	CONTACT_MAIL NVARCHAR(100) NOT NULL, 
	CONTACT_PHONE NVARCHAR(100) NOT NULL,
	LABOR INT NOT NULL, -- SỐ NHÂN CÔNG 
	ACREAGE FLOAT NOT NULL, -- DIỆN TÍCH 
	IMAGE_ANIMAL_STORAGE VARBINARY(MAX), -- bản đồ cơ sở lưu trữ động vật
    ID_COMMUNE INT NOT NULL,
    FOREIGN KEY (ID_COMMUNE) REFERENCES COMMUNE(ID)
);
GO

-- Tạo bảng danh mục loài động vật
CREATE TABLE ANIMAL_CATEGORY (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
	GENERIC NVARCHAR(100) NOT NULL, -- CHỦNG LOẠI
	DATE_FOUND DATE NOT NULL, -- NGÀY TÌM THẤY 
    QUANTITY INT NOT NULL, -- SỐ LƯỢNG CÁ THỂ
    STATUS NVARCHAR(100), -- TRẠNG THÁI BẢO TỒN ( NGUY CẤP , ỔN ĐỊNH ,...)
    ID_ANIMAL_STORAGE INT NOT NULL,
    FOREIGN KEY (ID_ANIMAL_STORAGE) REFERENCES ANIMAL_STORAGE(ID)
);
GO

-- Tạo bảng danh mục loại biến động
CREATE TABLE CHANGE_TYPE (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
    ID_ANIMAL_CATEGORY INT NOT NULL,
    FOREIGN KEY (ID_ANIMAL_CATEGORY) REFERENCES ANIMAL_CATEGORY(ID)
);
GO

-- Tạo bảng danh mục biến động số lượng cá thể
CREATE TABLE POPULATION_CHANGE (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ANIMAL_ID INT NOT NULL,
    DATE DATE NOT NULL,
    REASON TEXT, -- LÝ DO BIẾN ĐỘNG 
    LOCATION NVARCHAR(255), -- NƠI BIẾN ĐỘNG 
    STATUS BIT, -- TRẠNG THÁI BIẾN ĐÔNG (0 - GIẢM , 1- TĂNG )
    QUANTITY_BEFORE INT, -- SỐ LƯỢNG TRƯỚC 
    QUANTITY_AFTER  INT,
	ID_CHANGE_TYPE INT,
    FOREIGN KEY (ID_CHANGE_TYPE) REFERENCES CHANGE_TYPE(ID)
);
GO

---- Tạo bảng hướng dẫn sử dụng
--CREATE TABLE GUIDE (
--    ID INT IDENTITY(1,1) PRIMARY KEY,
--    DESCRIPTION TEXT,
--    ID_USER INT,
--    FOREIGN KEY (ID_USER) REFERENCES [USER](ID)
--);
--GO

---- Tạo bảng menu
--CREATE TABLE MENU (
--    ID INT IDENTITY(1,1) PRIMARY KEY,
--    DESCRIPTION TEXT
--);
--GO

--INSERT INTO DISTRICT (NAME)
--VALUES 
--(N'Hà Trung'),
--(N'Mường Lát');

---- Chèn dữ liệu cho các xã của huyện Mường Lát
--INSERT INTO COMMUNE (NAME, ID_DISTRICT)
--VALUES 
--(N'Mường Lát', 1),
--(N'Tén Tằn', 1),
--(N'Mường Chanh', 1);

---- Chèn dữ liệu cho các xã của huyện Hà Trung
--INSERT INTO COMMUNE (NAME, ID_DISTRICT)
--VALUES 
--(N'Hà Ngọc', 2),
--(N'Hà Đông', 2),
--(N'Hà Lĩnh', 2);

---- Chèn vai trò vào bảng ROLES
--INSERT INTO ROLES (ROLES)
--VALUES (0), (1); -- 0 cho user, 1 cho admin
--GO

---- Chèn vai trò vào bảng ROLES_GROUP
--INSERT INTO ROLES_GROUP (ROLES)
--VALUES (0), (1); -- 0 cho user, 1 cho admin
--GO

---- Chèn vai trò vào bảng ROLES_GROUP
--INSERT INTO GROUPS (NAME, STATUS, ID_ROLES_GROUP)
--VALUES (N'Group Admin', 1 , 2)
--GO

---- Chèn vai trò vào bảng USER
--INSERT INTO [USER] (NAME, STATUS, EMAIL, PHONE, DISPLAY_NAME, USER_NAME, PASSWORD, ID_COMMUNE, ID_ACCESS_HISTORY, ID_IMPACT_HISTORY, ID_ROLES, ID_GROUP)
--VALUES 
--(N'Lại Minh Tường', 1, 'lai.minh.tuong@example.com', '0123456789', N'Lại Tường sv(sinh viên)', N'lai_minhtuong', N'password1', 4, NULL, NULL, 1, 1),
--(N'Phạm Minh Đức', 1, 'pham.minh.duc@example.com', '0123456788', N'Phạm Đức', N'pham_minhduc', N'password2', 1, NULL, NULL, 1, 1),
--(N'Hoàng Văn Giáp', 1, 'hoang.van.giap@example.com', '0123456787', N' Giáp  đại ca ', N'hoang_vangiap', N'password3', 6, NULL, NULL, 1, 1),
--(N'Bùi Đình Trường', 1, 'bui.dinh.truong@example.com', '0123456786', N'Bùi Trường', N'bui_dinhtruong', N'password4', 1, NULL, NULL, 1, 1);
--GO

---- Chèn dữ liệu cho cơ sở sản xuất gỗ
--INSERT INTO WOOD_PROCESSING_FACILITY (NAME, STATUS, ADDRESS, CONTACT_FACE, CONTACT_MAIL, CONTACT_PHONE, LABOR, ACREAGE, Yield, WOOD_SPECIES_PROVIDED, PRODUCT, DESCRIPTION, IMAGE_WOOD_PROCESSING_FACILITY, ID_COMMUNE)
--VALUES 
--(N'Cơ sở gỗ Hà Lĩnh', 1, N'Thôn Đông, Hà Lĩnh', N'Công ty A', 'cs_gohalinh@example.com', '0987654321', 20, 1.5, 100.5, N'Keo, Bạch đàn', N'Ván ép', N'Chuyên sản xuất các loại ván ép từ gỗ keo và bạch đàn.', NULL, 6),
--(N'Cơ sở gỗ Hà Đông', 1, N'Thôn Tây, Hà Đông', N'Công ty B', 'cs_gohadong@example.com', '0987654322', 15, 2.0, 150.0, N'Sưa, Lim', N'Đồ nội thất', N'Cơ sở chuyên gia công đồ nội thất từ gỗ sưa và lim.', NULL, 5),
--(N'Cơ sở gỗ Tén Tằn', 1, N'Thôn Nam, Tén Tằn', N'Công ty C', 'cs_gotentan@example.com', '0987654323', 10, 1.0, 80.0, N'Teak', N'Gỗ tròn', N'Sản xuất gỗ tròn cho xuất khẩu.', NULL, 3),
--(N'Cơ sở gỗ Mường Chanh', 1, N'Thôn Bắc, Mường Chanh', N'Công ty D', 'cs_gomuongchanh@example.com', '0987654324', 25, 3.0, 200.0, N'Sấu, Chò chỉ', N'Đồ thủ công mỹ nghệ', N'Sản xuất các sản phẩm thủ công mỹ nghệ từ gỗ sấu và chò chỉ.', NULL, 4);
--GO

---- Chèn dữ liệu loại hình sản xuất cho 4 cơ sở chế biến gỗ
--INSERT INTO PRODUCTION_TYPE (NAME, DESCRIPTION, ID_WOOD_PROCESSING_FACILITY)
--VALUES
--(N'Cưa xẻ gỗ', N'Quy trình cưa xẻ gỗ thô thành các tấm ván', 1), -- Hà Lĩnh
--(N'Sản xuất ván ép', N'Sử dụng gỗ thô để sản xuất ván ép chất lượng cao', 1),
--(N'Cưa xẻ gỗ', N'Cưa xẻ và phân loại gỗ thành sản phẩm nhỏ hơn', 2), -- Hà Đông
--(N'Gia công đồ gỗ', N'Sản xuất đồ nội thất từ nguyên liệu gỗ', 2),
--(N'Sản xuất ván MDF', N'Sản xuất ván MDF từ gỗ vụn', 3), -- Tén Tằn
--(N'Cưa xẻ gỗ', N'Cưa gỗ và xuất khẩu dạng nguyên liệu', 3),
--(N'Cưa xẻ gỗ', N'Quy trình cưa gỗ thành thanh gỗ nhỏ', 4), -- Mường Chanh
--(N'Sản xuất pallet gỗ', N'Tạo pallet gỗ dùng trong vận chuyển', 4);
--GO
---- Chèn dữ liệu hình thức hoạt động cho 4 cơ sở chế biến gỗ
--INSERT INTO ACTIVITY_FORM (NAME, DESCRIPTION, ID_WOOD_PROCESSING_FACILITY)
--VALUES
--(N'Thủ công', N'Sử dụng lao động thủ công, không có máy móc hỗ trợ', 1), -- Hà Lĩnh
--(N'Bán cơ giới', N'Sử dụng máy móc đơn giản kết hợp với lao động thủ công', 1),
--(N'Thủ công', N'Sản xuất theo phương pháp truyền thống', 2), -- Hà Đông
--(N'Cơ giới hóa một phần', N'Sử dụng máy móc hiện đại hơn nhưng chưa tự động hóa hoàn toàn', 2),
--(N'Tự động hóa', N'Sử dụng hệ thống máy móc tự động trong sản xuất', 3), -- Tén Tằn
--(N'Bán cơ giới', N'Kết hợp máy móc và lao động thủ công', 3),
--(N'Cơ giới hóa một phần', N'Sản xuất với sự hỗ trợ của máy móc bán tự động', 4), -- Mường Chanh
--(N'Thủ công', N'Phụ thuộc hoàn toàn vào lao động thủ công', 4);
--GO

---- Chèn dữ liệu cơ sở sản xuất giống cây
--INSERT INTO PLANT_BREEDING_FACILITY (
--    NAME, STATUS, ADDRESS, CONTACT_FACE, CONTACT_MAIL, CONTACT_PHONE, ACREAGE, 
--    SEEDLINGS_YIELD, LABOR, DISCRIPTION, IMAGE_LANT_BREEDING_FACILITY, ID_COMMUNE
--)
--VALUES
---- Cơ sở tại xã Hà Lĩnh
--(N'Cơ sở sản xuất Hà Lĩnh', 1, N'Thôn Trung Sơn, Hà Lĩnh', N'Nguyễn Văn A', 
-- N'contact_halingh@example.com', N'0912345678', 15.5, 5000, 20, 
-- N'Cơ sở giống cây trồng cung cấp các loại cây ăn quả năng suất cao.', NULL, 6),

---- Cơ sở tại xã Hà Ngọc
--(N'Cơ sở sản xuất Hà Ngọc', 1, N'Thôn Đồng Tâm, Hà Ngọc', N'Lê Thị B', 
-- N'contact_hangoc@example.com', N'0987654321', 20.0, 7000, 25, 
-- N'Chuyên sản xuất cây giống phục vụ trồng rừng và cảnh quan.', NULL, 4),

---- Cơ sở tại xã Mường Lát
--(N'Cơ sở sản xuất Mường Lát', 0, N'Bản Nà Ón, Mường Lát', N'Phạm Văn C', 
-- N'contact_muonglat@example.com', N'0909123456', 10.0, 3000, 15,
-- N'Chuyên sản xuất cây giống phục vụ trồng rừng và cảnh quan.', NULL, 1),

-- -- Chèn dữ liệu mẫu vào bảng PLANT_VARIETY_TYPE
--INSERT INTO PLANT_VARIETY_TYPE (NAME, TYPE, DISCRIPTION, PRICE, HEIGHT, ID_PLANT_BREEDING_FACILITY)
--VALUES 
--(N'Cây Keo', N'Cây lấy gỗ', N'Cây Keo phát triển nhanh, dễ trồng, thường dùng làm nguyên liệu gỗ.', 15000, 5, 1),
--(N'Cây Xoài', N'Cây ăn quả', N'Cây Xoài cho quả ngọt, thường trồng tại các vùng nhiệt đới.', 25000, 4, 1),
--(N'Cây Sưa', N'Cây lấy gỗ quý', N'Cây Sưa có giá trị kinh tế cao, thường dùng trong đồ gỗ mỹ nghệ.', 100000, 10, 2),
--(N'Cây Bạch đàn', N'Cây lấy gỗ', N'Cây Bạch đàn phổ biến trong ngành công nghiệp giấy và năng lượng.', 12000, 8, 2),
--(N'Cây Điều', N'Cây ăn quả', N'Cây Điều cho hạt điều chất lượng cao, giá trị xuất khẩu lớn.', 30000, 6, 3),
--(N'Cây Lát hoa', N'Cây lấy gỗ', N'Lát hoa là cây gỗ quý, được sử dụng trong sản xuất nội thất cao cấp.', 80000, 7, 3);
--GO

---- Chèn dữ liệu mẫu vào bảng ANIMAL_STORAGE
--INSERT INTO ANIMAL_STORAGE (NAME, ADDRESS, CONTACT_FACE, CONTACT_MAIL, CONTACT_PHONE, LABOR, ACREAGE, DESCRIPTION, ID_COMMUNE)
--VALUES
--(N'Trại Động Vật Hà Lĩnh', N'Số 12, Thôn Lĩnh, Xã Hà Lĩnh', N'facebook.com/traiha', N'traiha@example.com', N'0123-456-789', 10, 5.0, N'Trại chuyên nuôi gia súc và gia cầm.', 6), -- Cơ sở tại xã Hà Lĩnh
--(N'Trại Chăn Nuôi Tèn Tén', N'Số 20, Thôn Tén, Xã Tèn Tén', N'facebook.com/traitentien', N'trenten@example.com', N'0123-456-789', 8, 4.5, N'Trại nuôi thú cưng và gia súc.', 2), -- Cơ sở tại xã Tèn Tén
--GO

---- Chèn dữ liệu mẫu vào bảng ANIMAL_CATEGORY cho từng cơ sở lưu trữ động vật
--INSERT INTO ANIMAL_CATEGORY (NAME, GENERIC, Latitude, Longitude, DATE_FOUND, QUANTITY, STATUS, DESCRIPTION, ID_ANIMAL_RESOURCE)
--VALUES
---- Dữ liệu cho Trại Động Vật Hà Lĩnh
--(N'Hổ', N'Panthera tigris', 20.960, 105.350, '2024-01-15', 5, N'Nguy cấp', N'Loài hổ được tìm thấy trong rừng.', 1),
--(N'Sao', N'Ailuronycteris', 20.960, 105.350, '2024-01-20', 10, N'Ổn định', N'Loài sao được tìm thấy trong các khu vực rừng.', 1),
--(N'Hưu', N'Cervus elaphus', 20.960, 105.350, '2024-01-25', 8, N'Ổn định', N'Loài hươu với bộ lông đẹp.', 1),
---- Dữ liệu cho Tèn Tén
--(N'SV Tường', N'Prionailurus bengalensis', 20.962, 105.352, '2024-01-10', 6, N'Ổn định', N'Loài SV Tường là loài mèo nhỏ.', 2),
--(N'Hổ', N'Panthera tigris', 20.962, 105.352, '2024-01-15', 4, N'Nguy cấp', N'Loài hổ có tính cách hoang dã.', 2),
--(N'Tê tê', N'Manis', 20.962, 105.352, '2024-01-20', 2, N'Nguy cấp', N'Tê tê là loài động vật có vảy.', 2);

---- Chèn dữ liệu vào bảng CHANGE_TYPE cho loài hổ ở Hà Lĩnh
--INSERT INTO CHANGE_TYPE (NAME, DESCRIPTION, ID_ANIMAL_CATEGORY)
--VALUES 
--(N'Hổ', N'Biến động số lượng loài hổ trong tự nhiên.', 1), -- ID_ANIMAL_CATEGORY của loài hổ ở Hà Lĩnh là 1 
--(N'SV Tường', N'Biến động số lượng loài SV Tường trong tự nhiên.', 4); -- ID_ANIMAL_CATEGORY của loài SV Tường ở Tèn Tén là 4

---- Chèn dữ liệu vào bảng POPULATION_CHANGE
--INSERT INTO POPULATION_CHANGE (ANIMAL_ID, DATE, REASON, LOCATION, DESCRIPTION, STATUS, QUANTITY_BEFORE, QUANTITY_AFTER, ID_CHANGE_TYPE)
--VALUES 
--(1, '2024-01-15', N'Phát hiện tăng trưởng tự nhiên.', N'Hà Lĩnh', N'Số lượng hổ đã tăng lên trong khu vực này.', 1, 15, 20, 1), -- Giả sử ID_CHANGE_TYPE của loài hổ là 1
--(4, '2024-01-20', N'Sự bảo vệ từ cơ sở bảo tồn.', N'Tèn Tén', N'Số lượng SV Tường đã tăng nhờ các biện pháp bảo tồn.', 1, 10, 15, 2); -- Giả sử ID_CHANGE_TYPE của loài SV Tường là 2
--GO







