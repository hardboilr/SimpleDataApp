CREATE PROCEDURE [Sales].[uspNewCustomer]
@CustomerName NVARCHAR (40),
@CustomerID INT OUTPUT
AS
BEGIN
INSERT INTO [Sales].[Customer] (CustomerName) VALUES (@CustomerName);
SET @CustomerID = SCOPE_IDENTITY();
RETURN @@ERROR
END