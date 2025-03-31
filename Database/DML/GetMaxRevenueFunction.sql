--Max Revenue Per Week Function
create function dbo.GetMaxRevenue()
returns table
as
return(
	SELECT DATEPART(ISO_WEEK, [BookingDay]) AS WeekNumber,
		MAX(BookingFee) AS MaxRevenue
	FROM Bookings
	GROUP BY DATEPART(ISO_WEEK, [BookingDay])
);

select MaxRevenue from dbo.GetMaxRevenue();
