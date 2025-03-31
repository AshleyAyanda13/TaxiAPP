SELECT TOP (1000) [MarshalID]
      ,[Name]
      ,[Surname]
  FROM [TaxiAppdb].[dbo].[TaxiMarshal]

  INSERT INTO [TaxiAppdb].[dbo].[TaxiMarshal] 
    ([Name], [Surname]) 
VALUES 
    ('John', 'Doe'),
    ('Jane', 'Doe'),
    ('Alice', 'Johnson'),
    ('Bob', 'Smith'),
    ('Charlie', 'Brown'),
    ('David', 'Williams');