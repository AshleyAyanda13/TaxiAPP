SELECT TOP (1000) [DestinationID]
      ,[Province]
      ,[City]
      ,[TaxiID]
  FROM [TaxiAppdb].[dbo].[Destination]

 INSERT INTO [TaxiAppdb].[dbo].[Destination] 
    ([DestinationID],[Province], [City], [TaxiID]) 
VALUES 
    (11,'Gauteng', 'Johannesburg', 1),
    (12,'Western Cape', 'Cape Town', 2),
    (13,'KwaZulu-Natal', 'Durban', 3),
    (14,'Eastern Cape', 'Port Elizabeth', 4),
    (15,'Free State', 'Bloemfontein', 5),
    (16,'Limpopo', 'Polokwane', 6);