SELECT TOP (1000) [TaxiID]
      ,[BrandName]
      ,[Model]
      ,[Registration]
      ,[DriverID]
  FROM [TaxiAppdb].[dbo].[Taxi]

  INSERT INTO [TaxiAppdb].[dbo].[Taxi] 
    ([BrandName], [Model], [Registration], [DriverID]) 
VALUES 
    ('Toyota', 'Corolla', 'AB123CD', 5),
    ('Honda', 'Civic', 'EF456GH', 6),
    ('Ford', 'Focus', 'IJ789KL', 7),
    ('Chevrolet', 'Malibu', 'MN012OP', 8),
    ('Hyundai', 'Elantra', 'QR345ST', 9),
    ('Nissan', 'Sentra', 'UV678WX', 10);

