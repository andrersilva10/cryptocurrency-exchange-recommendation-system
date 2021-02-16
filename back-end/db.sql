create database cryptocurrency_exchanges;
go

CREATE TABLE TB_EXCHANGES(
	ID INT PRIMARY KEY,
	[NAME] VARCHAR(55),
	[NAME_ID] VARCHAR(55),
	[URL] NVARCHAR(255),
	[VOLUME_USD] DECIMAL(25,5),
	ACTIVE_PAIRS INT,
	COUNTRY VARCHAR(55)
);
GO

CREATE TABLE TB_EXCHANGE_PAIRS(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ID_EXCHANGE INT,
	BASE VARCHAR(10),
	QUOTE VARCHAR(10),
	[VOLUME] DECIMAL(25,5),
	[PRICE] DECIMAL(25,5),
	[PRICE_USD] DECIMAL(25,5),
	[TIME] INT,
	FOREIGN KEY ID_EXCHANGE REFERENCES TB_EXCHANGES(ID)
);
GO