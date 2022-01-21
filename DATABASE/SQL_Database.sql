create database Helperland_Database;

Use Helperland_Database
Go

Create Schema Helperland_Data
Go

Create Table Helperland_Data.User_Type
(
    User_Type_ID int primary key identity(1,1) NOT NULL,
	User_Type varchar(30) NOT NULL,
);

Create Table Helperland_Data.Customer_Detail
(
    Customer_ID int identity(1,1) Primary key NOT NULL,
	First_Name varchar(50) NOT NULL,
	Last_Name varchar(50) NOT NULL,
	Email_ID varchar(50) NOT NULL,
	Mobile_No varchar(10) NOT NULL,
	Password varchar(30) NOT NULL,
	Confirm_Password varchar(30) NOT NULL,
	Date_Of_Birth date NULL,
	Prefered_Language varchar(30) NOT NULL,
	User_Type_ID int foreign key references Helperland_Data.User_Type(User_Type_ID) NOT NULL,
);

Create Table Helperland_Data.Service_Provider_Address_Detail
(
    SP_Address_ID int  primary key identity(1,1) NOT NULL,
	Street_Name varchar(50) NOT NULL,
	House_Number int NOT NULL,
	Postal_Code varchar(6) NOT NULL,
	City char(30) NOT NULL,
);

Create Table Helperland_Data.Service_Provider_Detail
(
    Service_Provider_ID int primary key identity(1,1) NOT NULL,
	First_Name varchar(30) NOT NULL,
	Last_Name varchar(30) NOT NULL,
	Email_ID varchar(50) NOT NULL,
	Phone_No varchar(10) NOT NULL,
	Password varchar(30) NOT NULL,
	Confirm_Password varchar(30) NOT NULL,
	News_Letter bit NOT NULL,
	Date_Of_Birth date NULL,
	Nationality varchar(20) NOT NULL,
	Gender varchar(20) NOT NULL,
	Avtar varbinary(max) NULL,
	SP_Address_ID int foreign key references  Helperland_Data.Service_Provider_Address_Detail(SP_Address_ID) NOT NULL,
	User_Type_ID int foreign key references  Helperland_Data.User_Type(User_Type_ID) NOT NULL,
	constraint UC_Email Unique(Email_ID),
);

Create Table Helperland_Data.Contact_Us
(
   Contact_ID int primary key identity(1,1)NOT NULL,
	First_Name varchar(30) NOT NULL,
	Last_Name varchar(30) NOT NULL,
	Mobile_Number varchar(10) NOT NULL,
	Email_ID varchar(50) NOT NULL,
	Subject varchar(20) NOT NULL,
	Message varchar(500) NULL,
	constraint UC_Email_1 Unique(Email_ID),
);

Create Table Helperland_Data.Book_Service
(
    Service_ID int primary key identity(1,1) NOT NULL,
	Comment varchar(500) NULL,
	Service_Date date NOT NULL,
	Service_Time time(7) NOT NULL,
	Total_Service_Time time(7) NOT NULL,
	No_OF_Extra_Services int NULL,
	Have_A_Pet bit NOT NULL,
	Total_Payment float NOT NULL,
    Discount float NOT NULL,
	Grand_Payment float  NOT NULL,
	Service_Provider_ID int foreign key references  Helperland_Data.Service_Provider_Detail(Service_Provider_ID)  NULL,
);

Create Table Helperland_Data.Customer_Book_Service_Detail
(
    Service_Detail_ID int primary key identity(1,1) NOT NULL,
	Service_ID int foreign key references  Helperland_Data.Book_Service(Service_ID) NOT NULL,
	Street_Name varchar(30) NOT NULL,
    Postal_Code varchar(6) NOT NULL,
	City varchar(30) NOT NULL,
	Phone_No varchar(10) NOT NULL,
	Favourite_Service_Provider varchar(50) NULL,
);

Create Table Helperland_Data.Service_Rate_SP
(
    Rate_SP_ID int primary key identity(1,1) NOT NULL,
	Service_ID int foreign key references  Helperland_Data.Book_Service(Service_ID) NOT NULL,
	Customer_ID int foreign key references  Helperland_Data.Customer_Detail(Customer_ID) NOT NULL,
	Service_Provider_ID int foreign key references  Helperland_Data.Service_Provider_Detail(Service_Provider_ID) NOT NULL,
	On_Time_Arrival float NULL,
    Friendly float NULL,
	Quality_Of_Service float NULL,
	Feedback varchar(500) NULL,
	Ratings float NULL,
);

Create Table Helperland_Data.Get_News_Letter
(
     News_ID int primary key identity(1,1) NOT NULL,
	Email_ID varchar(50) NULL,
	
);

Create Table Helperland_Data.Block_Customer
(
  Block_ID int  primary key identity(1,1) NOT NULL,
	Customer_ID int foreign key references  Helperland_Data.Customer_Detail(Customer_ID) NOT NULL,
);

