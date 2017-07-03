use ServiceStation
go

-- персоны
create table Persons(
	Id			int				not null	identity(1,1),	-- ид
	Surname		nvarchar(50)	not null,					-- фамилия
	Name		nvarchar(50)	not null,					-- имя
	Patronymic	nvarchar(50),								-- отчество
	BirthDate	date			not null,					-- дата рождения
	[Address]	nvarchar(200)	not null,					-- адрес прописки
	Passport	nvarchar(8)		not null,					-- Серия и номер паспорта
	License		nvarchar(12),								-- водительские права
	constraint Persons_PK primary key (Id)					-- первичный ключ по ид
);


-- модели автомобилей
create table Models(
	Id			int				not null	identity(1,1),	-- ид
	Name		nvarchar(50)	not null,					-- модель
	constraint Models_PK primary key (Id)					-- первичный ключ по ид
);


-- разряды работников
create table Categories(
	Id			int				not null	identity(1,1),	-- ид
	Name		nvarchar(50)	not null,					-- разряд
	constraint Categories_PK primary key (Id)				-- первичный ключ по ид
);


-- специальности работников
create table Specialities(
	Id			int				not null	identity(1,1),	-- ид
	Name		nvarchar(50)	not null,					-- специальность
	constraint Specialities_PK primary key (Id)				-- первичный ключ по ид
);


-- неисправности
create table Malfunctions(
	Id			int				not null	identity(1,1),	-- ид
	Name		nvarchar(100)	not null,					-- наименование неисправности
	constraint Malfunctions_PK primary key (Id)				-- первичный ключ по ид
);


-- перечень услуг
create table ServicesInfos(
	Id			int				not null	identity(1,1),	-- ид
	Name		nvarchar(100)	not null,					-- наименование услуги
	Price		int				not null,					-- стоимость услуги
	constraint ServicesInfos_PK primary key (Id)			-- первичный ключ по ид
);


-- работники
create table Employees(
	Id				int	not null	identity(1,1),	-- ид
	PersonId		int	not null,					-- личные данные
	SpecialityId	int	not null,					-- специальность
	CategoryId		int	not null,					-- категория
	Experience		int	not null,					-- стаж
	IsWorking		bit	not null	default(1),		-- работает ли (1 - работает, 0 - уволен)
	constraint Employees_PK				primary key (Id),											-- первичный ключ по ид
	constraint EmployeesPerson_FK		foreign key (PersonId)		references Persons(Id),			-- внешний ключ к личным данным
	constraint EmployeesSpeciality_FK	foreign key (SpecialityId)	references Specialities(Id),	-- внешний ключ к специальностям
	constraint EmployeesCategory_FK		foreign key (CategoryId)	references Categories(Id)		-- внешний ключ к категориям
);


-- машины
create table Cars(
	Id				int				not null	identity(1,1),	-- ид
	ModelId			int				not null,					-- модель
	ProductionYear	date			not null,					-- год выпуска
	Number			nvarchar(8)		not null,					-- гос номер
	Color			nvarchar(25)	not null,					-- цвет
	OwnerId			int				not null,					-- владелец (таблица Persons)
	constraint Cars_PK		primary key (Id),								-- первичный ключ по ид
	constraint CarsOwner_FK	foreign key (OwnerId) references Persons(Id),	-- внешний ключ к таблице личных данных владельца
	constraint CarsModel_FK foreign key (ModelId) references Models(Id)		-- внешний ключ к таблице моделей автомобиля
);


-- неисправности автомобилей
create table CarMalfunctions(
	Id				int		not null	identity(1,1),	-- ид
	CarId			int		not null,					-- автомобиль
	MalfunctionId	int		not null,					-- неисправность
	constraint CarMalfunctions_PK				primary key (Id),										-- первичный ключ по ид
	constraint CarMalfunctionsCar_FK			foreign key (CarId)			references Cars(Id),		-- внешний ключ к таблице автомобилей
	constraint CarMalfunctionsMalfunction_FK	foreign key (MalfunctionId) references Malfunctions(Id)	-- внешний ключ к таблице неисправностей
);


-- заказы
create table Orders(
	Id				int		not null	identity(1,1),	-- ид
	ClientId		int		not null,					-- клиент (таблица Persons)
	CarId			int		not null,					-- автомобиль
	BeginDate		date	not null,					-- дата принятия заказа
	FinishDate		date	not null,					-- дата выдачи заказа
	IsFinished		bit		not null	default(0),		-- заказ выполнен (на случай расхождения с графиком)
	constraint Orders_PK		primary key (Id),								-- первичный ключ по ид
	constraint OrdersClient_FK	foreign key (ClientId)	references Persons(Id),	-- внешний ключ к таблице личных данных клиента
	constraint OrdersCar_FK		foreign key (CarId)		references Cars(Id)		-- внешний ключ к таблице автомобилей
);


-- услуги в заказе
create table OrderServices(
	Id			int		not null	identity(1,1),	-- ид
	OrderId		int		not null,					-- заказ
	ServiceId	int		not null,					-- информация об услуге (таблица ServicesInfos)
	constraint OrderServices_PK			primary key (Id),										-- первичный ключ по ид
	constraint OrderServicesOrder_FK	foreign key (OrderId)	references Orders(Id),			-- внешний ключ к таблице заказов
	constraint OrderServicesService_FK	foreign key (ServiceId) references ServicesInfos(Id)	-- внешний ключ к таблице услуг
);


-- исполнители (работники выполняющие работу для определенного заказа по определенной услуге)
create table Executors(
	Id				int		not null	identity(1,1),	-- ид
	ServiceId		int		not null,					-- услуга в заказе (таблица OrderServices)
	EmployeeId		int		not null,					-- работник, выполняющий услугу
	constraint Executors_PK			primary key (Id),											-- первичный ключ по ид
	constraint ExecutorsService_FK	foreign key (ServiceId)		references OrderServices(Id),	-- внешний ключ к таблице услуг в заказе
	constraint ExecutorsEmployee_FK foreign key (EmployeeId)	references Employees(Id)		-- внешний ключ к таблице работников
);