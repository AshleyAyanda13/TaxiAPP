SELECT TOP (1000) [PassengerID]
      ,[Name]
      ,[Surname]
      ,[CellphoneNr]
      ,[PaymentMethod]
  FROM [TaxiAppdb].[dbo].[Passenger]

--dummy data 
 INSERT INTO [TaxiAppdb].[dbo].[Passenger] 
    ( [Name], [Surname], [CellphoneNr], [PaymentMethod]) 
VALUES 
    ('John', 'Doe', '1234567890', 1),
    ('Jane', 'Doe', '0987654321', 0),
    ('Alice', 'Johnson', '1122334455', 1),
    ('Bob', 'Smith', '2233445566', 0),
    ('Charlie', 'Brown', '3344556677', 1),
    ('David', 'Williams', '4455667788', 1);
