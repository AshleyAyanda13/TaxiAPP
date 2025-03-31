--Min Revenue Per Week Function
create function dbo.GetMinRevenue()
returns table
as
return(
	--SELECT DATEPART(ISO_WEEK, [BookingDay]) AS WeekNumber,
		--MIN(BookingFee) AS MinRevenue
	--FROM Bookings
	--GROUP BY DATEPART(ISO_WEEK, [BookingDay])

	SELECT DATEPART(ISO_WEEK, [BookingDay]) as WeekNumber, 
	MIN(BookingFee) AS MinWeeklyRevenue
	FROM (
		SELECT WeekNumber, Min
		FROM Bookings
		WHERE Payment = 'true'
	) AS Subquery
	GROUP BY WeekNumber;
	
);

select MinRevenue from dbo.GetMinRevenue();

