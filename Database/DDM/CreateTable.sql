Create table Passenger(
PassengerID int Identity(1,1),
Name text not null,
Surname text not null,
CellphoneNr varchar (13),
PaymentMethod varchar(10),
primary key(PassengerID)
);

Create table TaxiMarshal(
MarshalID int Identity(1,1),
Name text not null,
Surname text not null,
primary key(MarshalID)
);

Create table Driver(
DriverID int Identity(1,1),
Name varchar (100),
Surname varchar (100),
primary key(DriverID)
);


Create table Taxi(
TaxiID int Identity(1,1),
BrandName text not null,
Model varchar (100),
Registration varchar (100),
primary key(TaxiID),
DriverID int not null,
Foreign Key(DriverID) references Driver
);

Create table Destination(
DestinationID int not null,
Province varchar (100),
 City varchar (255),
primary key(DestinationID),
TaxiID int not null,
Foreign Key(TaxiID) references Taxi
);


Create table Bookings(
BookingID int not null,
Payment bit,
Timeslot datetime not null,
primary key(BookingID),
MarshalID int not null,
PassengerID int not null,
DestinationID int not null,
Foreign Key(PassengerID) references Passenger,
Foreign Key(MarshalID) references TaxiMarshal,
Foreign Key(DestinationID) references Destination
);

Create table BookingReciept(
BookingID int not null,
DestinationID int not null,
Foreign Key(BookingID) references Bookings,
Foreign Key(DestinationID) references Destination
);

Create table DriverDel(
DriverID int Identity(1,1),
DeleteDate datetime,
primary key(DriverID),
Foreign Key(DriverID) references Driver
);


alter table dbo.Bookings
add BookingFee int;