--Weekly card payments function
create function dbo.udWeeklyCardPayments()
returns table
as
return
(
	SELECT [BookingDay] AS CardPayments,    
		SUM([BookingFee]) OVER (PARTITION BY DATEPART(WEEK, [BookingDay])) AS WeeklySales
	FROM Bookings
	
);

select * from dbo.udWeeklyCardPayments();