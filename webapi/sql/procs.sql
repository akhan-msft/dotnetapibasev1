-- Create the stored procedure
CREATE PROCEDURE GetCustomersByPostalCode
	@PostalCode NVARCHAR(50)
AS
BEGIN
	SELECT * 
	FROM Customers
	WHERE Postal_Code = @PostalCode;
END;
GO
