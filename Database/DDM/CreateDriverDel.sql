create table dbo.DriverDel(
Id int Identity(1,1),
Name text,
Surname text,
DeleteDate datetime
Primary key(Id)
)

alter table dbo.DriverDel
Add  DriverId int