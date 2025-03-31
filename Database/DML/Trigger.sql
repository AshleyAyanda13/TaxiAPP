create trigger dbo.LogDelete
on dbo.Driver
after Delete 
as
begin 
  Insert into dbo.DriverDel([Name], Surname, DeleteDate, DriverId) 
  select  d.Name ,  d.Surname, GETDATE(),  d.DriverID
  from deleted as d
  print('deleted driver logged');
end;