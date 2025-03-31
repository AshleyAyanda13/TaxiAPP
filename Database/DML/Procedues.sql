--Add Delete Driver Procedure
create procedure dbo.spDeleteDriver @DriverID int
as
begin
	delete from dbo.Driver where DriverID = @DriverID;
end;

exec dbo.spDeleteDriver @DriverID = 4;


--Add Booking Fee Procedure
create procedure dbo.spAddBookingFee(@BookingID int, @Payment bit)
AS
BEGIN


    IF @Payment = 1
		UPDATE  dbo.Bookings
		SET BookingFee = 25
		WHERE BookingID = @BookingID
		
	ELSE
        UPDATE  dbo.Bookings
		SET BookingFee = 0
		WHERE BookingID = @BookingID

END;

-- loop for executing procedure
DECLARE @i INT = 1;
WHILE @i < 7
BEGIN

	DECLARE @y INT = 41;

	WHILE @y < 47
	BEGIN	
		--Fist Check If Payment = 1 for loyalty payments and Update Fee
		If (SELECT Bookings.Payment As PaymentMethod FROM Bookings
		WHERE BookingID = @y) = 'true'
			BEGIN
				-- Execute Procedure
				exec spAddBookingFee @BookingID = @y, @Payment = 1;
				PRINT('Booking fee updated to R25, via loyalty card payment method!');
			END
			ELSE
			BEGIN
				-- Execute Procedure
				exec spAddBookingFee @BookingID = @y, @Payment = 0;
				PRINT('Booking fee updated to R0, via cash payment method!');
			END
			SET @y = @y + 1;
		END

    SET @i = @i + 1;
END;


--exec spAddBookingFee @BookingID = 1, @Payment = 0;


