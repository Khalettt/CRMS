use CRMS;
CREATE TABLE Users (
    Userid INT PRIMARY KEY,
    Fullname NVARCHAR(100) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Status NVARCHAR(10) NOT NULL,
    Date DATE NOT NULL,
    Photo VARBINARY(MAX)
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    RegistrationDate DATE NOT NULL
);

CREATE TABLE cars (
  car_id INT PRIMARY KEY,
  make VARCHAR(50) NOT NULL,
  model VARCHAR(50) NOT NULL,
  year INT NOT NULL,
  registration_number VARCHAR(50) UNIQUE NOT NULL,
  status VARCHAR(20) NOT NULL CHECK (status IN ('available', 'rented', 'maintenance')),
  rental_price_per_day DECIMAL(10,2) NOT NULL
);

CREATE TABLE Rentals (
    RentalID INT PRIMARY KEY,
    CustomerID INT NOT NULL,
    car_id INT NOT NULL,
    UserID INT, 
    RentDate DATETIME NOT NULL,
    ReturnDate DATETIME,
    TotalAmount DECIMAL(10,2),
    Status VARCHAR(20) DEFAULT 'Ongoing', -- Ongoing, Returned
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (car_id) REFERENCES Cars(car_id),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY,
    RentalID INT NOT NULL,
    PaymentDate DATE,
    AmountPaid DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(50), -- e.g., Cash, Card, Mobile
    FOREIGN KEY (RentalID) REFERENCES Rentals(RentalID)
);

CREATE TABLE CarMaintenance (
    MaintenanceID INT PRIMARY KEY,
    car_id INT NOT NULL,
    MaintenanceDate DATE NOT NULL,
    Description TEXT,
    Cost DECIMAL(10,2),
    FOREIGN KEY (car_id) REFERENCES Cars(car_id)
);

select*from Rentals;
