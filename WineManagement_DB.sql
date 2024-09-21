-- Tạo cơ sở dữ liệu WineManagementSystem
CREATE DATABASE WineManagementSystem;
GO

-- Sử dụng cơ sở dữ liệu vừa tạo
USE WineManagementSystem;
GO

-- Tạo bảng Role (Vai trò)
CREATE TABLE Role (
    Role_Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

-- Tạo bảng Account (Tài khoản)
CREATE TABLE Account (
    Account_Id INT PRIMARY KEY IDENTITY(1,1),
    Role_Id INT,
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Status NVARCHAR(50),
    FOREIGN KEY (Role_Id) REFERENCES Role(Role_Id)
);

-- Tạo bảng Category (Danh mục sản phẩm)
CREATE TABLE Category (
    Category_Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);

-- Tạo bảng Supplier (Nhà cung cấp)
CREATE TABLE Supplier (
    Supplier_Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

-- Tạo bảng Wine (Rượu)
CREATE TABLE Wine (
    Wine_Id INT PRIMARY KEY IDENTITY(1,1),
    Category_Id INT,
    Name NVARCHAR(100) NOT NULL,
    Origin NVARCHAR(100),
    Volume DECIMAL(10,2),
    AlcContent DECIMAL(5,2),
    Description NVARCHAR(255),
    Status NVARCHAR(50),
    FOREIGN KEY (Category_Id) REFERENCES Category(Category_Id)
);

-- Tạo bảng WineImportRequest (Yêu cầu nhập hàng rượu)
CREATE TABLE WineImportRequest (
    Request_Id INT PRIMARY KEY IDENTITY(1,1),
    Supplier_Id INT,
    Manager_Id INT,
    Wine_Id INT,
    Quantity INT,
    RequestData DATE,
    Description NVARCHAR(255),
    Status NVARCHAR(50),
    FOREIGN KEY (Supplier_Id) REFERENCES Supplier(Supplier_Id),
    FOREIGN KEY (Wine_Id) REFERENCES Wine(Wine_Id)
);

-- Tạo bảng WineBatch (Lô hàng rượu)
CREATE TABLE WineBatch (
    Batch_Id INT PRIMARY KEY IDENTITY(1,1),
    Wine_Id INT,
    Request_Id INT,
    BatchNumber NVARCHAR(50),
    ImportDate DATE,
    Quantity INT,
    ProductionYear INT,
    Status NVARCHAR(50),
    FOREIGN KEY (Wine_Id) REFERENCES Wine(Wine_Id),
    FOREIGN KEY (Request_Id) REFERENCES WineImportRequest(Request_Id)
);

-- Tạo bảng WineStorageLocation (Vị trí kho rượu)
CREATE TABLE WineStorageLocation (
    Location_Id INT PRIMARY KEY IDENTITY(1,1),
    FloorNumber INT,
    Zone NVARCHAR(50),
    ShelfCode NVARCHAR(50),
    Capacity INT,
    Description NVARCHAR(255),
    Status NVARCHAR(50)
);

-- Tạo bảng WineTransaction (Giao dịch rượu)
CREATE TABLE WineTransaction (
    Transaction_Id INT PRIMARY KEY IDENTITY(1,1),
    Batch_Id INT,
    Wine_Id INT,
    Inspector_Id INT,
    Location_Id INT,
    Quantity INT,
    TransType NVARCHAR(50), -- Loại giao dịch (vd: Nhập kho, Xuất kho, Hư hỏng)
    TransDate DATE, -- Ngày giao dịch
    Description NVARCHAR(255), -- Mô tả giao dịch
    ImageURL NVARCHAR(255), -- Đường dẫn ảnh minh họa
    FOREIGN KEY (Batch_Id) REFERENCES WineBatch(Batch_Id),
    FOREIGN KEY (Wine_Id) REFERENCES Wine(Wine_Id),
    FOREIGN KEY (Location_Id) REFERENCES WineStorageLocation(Location_Id)
);
-- Tạo bảng WineImportCheck (Kiểm tra nhập hàng rượu)
CREATE TABLE WineImportCheck (
    Check_Id INT PRIMARY KEY IDENTITY(1,1),
    Request_Id INT,
    Batch_Id INT,
    Inspector_Id INT,
    Wine_Id INT,
    Quantity INT,
    Status NVARCHAR(50),
    CheckDate DATE,
    Description NVARCHAR(255),
    ImageURL NVARCHAR(255),
    FOREIGN KEY (Request_Id) REFERENCES WineImportRequest(Request_Id),
    FOREIGN KEY (Batch_Id) REFERENCES WineBatch(Batch_Id),
    FOREIGN KEY (Wine_Id) REFERENCES Wine(Wine_Id)
);

-- Kiểm tra các bảng
SELECT * FROM Role;
SELECT * FROM Account;
SELECT * FROM Category;
SELECT * FROM Supplier;
SELECT * FROM Wine;
SELECT * FROM WineBatch;
SELECT * FROM WineImportRequest;
SELECT * FROM WineTransaction;
SELECT * FROM WineStorageLocation;
SELECT * FROM WineImportCheck;
