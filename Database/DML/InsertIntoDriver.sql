SELECT TOP (1000) [DriverID]
      ,[Name]
      ,[Surname]
  FROM [TaxiAppdb].[dbo].[Driver]

INSERT INTO [TaxiAppdb].[dbo].[Driver] 
    ([Name], [Surname]) 
VALUES 
    ('John', 'Doe'),
    ('Jane', 'Doe'),
    ('Alice', 'Johnson'),
    ('Bob', 'Smith'),
    ('Charlie', 'Brown'),
    ('David', 'Williams');