ALTER TABLE dbo.Bookings add BookingFee int;

ALTER TABLE dbo.Destination DROP COLUMN BookingFee;

ALTER TABLE dbo.Bookings add BookingDay date;
ALTER TABLE dbo.Bookings drop column Timeslot;
ALTER TABLE dbo.Bookings add Timeslot text;

ALTER TABLE dbo.Bookings add TaxiId int;

ALTER TABLE dbo.Bookings add constraint TaxiId FOREIGN KEY (TaxiId) REFERENCES dbo.Taxi (TaxiID);
