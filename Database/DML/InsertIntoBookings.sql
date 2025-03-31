SELECT TOP (1000) [BookingID]
      ,[Payment]
      ,[MarshalID]
      ,[PassengerID]
      ,[DestinationID]
      ,[BookingFee]
      ,[BookingDay]
      ,[Timeslot]
      ,[TaxiId]
  FROM [TaxiAppdb].[dbo].[Bookings]

INSERT INTO [TaxiAppdb].[dbo].[Bookings]
(BookingID, Payment, MarshalID, PassengerID, DestinationID, BookingFee, BookingDay, Timeslot, TaxiId)
VALUES
(41, 1, 1, 1, 11, '', '2024-05-13', '10:00:00', 1),
(42, 0, 2, 2, 12, '', '2024-05-13', '11:00:00', 2),
(43, 1, 3, 3, 13, '', '2024-05-15', '12:00:00', 3),
(44, 1, 4, 4, 14, '', '2024-05-13', '10:00:00', 4),
(45, 0, 5, 5, 15, '', '2024-05-14', '11:00:00', 5),
(46, 1, 6, 6, 16, '', '2024-05-15', '12:00:00', 6);
