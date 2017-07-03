use ServiceStation
go

-- �������
create table Persons(
	Id			int				not null	identity(1,1),	-- ��
	Surname		nvarchar(50)	not null,					-- �������
	Name		nvarchar(50)	not null,					-- ���
	Patronymic	nvarchar(50),								-- ��������
	BirthDate	date			not null,					-- ���� ��������
	[Address]	nvarchar(200)	not null,					-- ����� ��������
	Passport	nvarchar(8)		not null,					-- ����� � ����� ��������
	License		nvarchar(12),								-- ������������ �����
	constraint Persons_PK primary key (Id)					-- ��������� ���� �� ��
);


-- ������ �����������
create table Models(
	Id			int				not null	identity(1,1),	-- ��
	Name		nvarchar(50)	not null,					-- ������
	constraint Models_PK primary key (Id)					-- ��������� ���� �� ��
);


-- ������� ����������
create table Categories(
	Id			int				not null	identity(1,1),	-- ��
	Name		nvarchar(50)	not null,					-- ������
	constraint Categories_PK primary key (Id)				-- ��������� ���� �� ��
);


-- ������������� ����������
create table Specialities(
	Id			int				not null	identity(1,1),	-- ��
	Name		nvarchar(50)	not null,					-- �������������
	constraint Specialities_PK primary key (Id)				-- ��������� ���� �� ��
);


-- �������������
create table Malfunctions(
	Id			int				not null	identity(1,1),	-- ��
	Name		nvarchar(100)	not null,					-- ������������ �������������
	constraint Malfunctions_PK primary key (Id)				-- ��������� ���� �� ��
);


-- �������� �����
create table ServicesInfos(
	Id			int				not null	identity(1,1),	-- ��
	Name		nvarchar(100)	not null,					-- ������������ ������
	Price		int				not null,					-- ��������� ������
	constraint ServicesInfos_PK primary key (Id)			-- ��������� ���� �� ��
);


-- ���������
create table Employees(
	Id				int	not null	identity(1,1),	-- ��
	PersonId		int	not null,					-- ������ ������
	SpecialityId	int	not null,					-- �������������
	CategoryId		int	not null,					-- ���������
	Experience		int	not null,					-- ����
	IsWorking		bit	not null	default(1),		-- �������� �� (1 - ��������, 0 - ������)
	constraint Employees_PK				primary key (Id),											-- ��������� ���� �� ��
	constraint EmployeesPerson_FK		foreign key (PersonId)		references Persons(Id),			-- ������� ���� � ������ ������
	constraint EmployeesSpeciality_FK	foreign key (SpecialityId)	references Specialities(Id),	-- ������� ���� � ��������������
	constraint EmployeesCategory_FK		foreign key (CategoryId)	references Categories(Id)		-- ������� ���� � ����������
);


-- ������
create table Cars(
	Id				int				not null	identity(1,1),	-- ��
	ModelId			int				not null,					-- ������
	ProductionYear	date			not null,					-- ��� �������
	Number			nvarchar(8)		not null,					-- ��� �����
	Color			nvarchar(25)	not null,					-- ����
	OwnerId			int				not null,					-- �������� (������� Persons)
	constraint Cars_PK		primary key (Id),								-- ��������� ���� �� ��
	constraint CarsOwner_FK	foreign key (OwnerId) references Persons(Id),	-- ������� ���� � ������� ������ ������ ���������
	constraint CarsModel_FK foreign key (ModelId) references Models(Id)		-- ������� ���� � ������� ������� ����������
);


-- ������������� �����������
create table CarMalfunctions(
	Id				int		not null	identity(1,1),	-- ��
	CarId			int		not null,					-- ����������
	MalfunctionId	int		not null,					-- �������������
	constraint CarMalfunctions_PK				primary key (Id),										-- ��������� ���� �� ��
	constraint CarMalfunctionsCar_FK			foreign key (CarId)			references Cars(Id),		-- ������� ���� � ������� �����������
	constraint CarMalfunctionsMalfunction_FK	foreign key (MalfunctionId) references Malfunctions(Id)	-- ������� ���� � ������� ��������������
);


-- ������
create table Orders(
	Id				int		not null	identity(1,1),	-- ��
	ClientId		int		not null,					-- ������ (������� Persons)
	CarId			int		not null,					-- ����������
	BeginDate		date	not null,					-- ���� �������� ������
	FinishDate		date	not null,					-- ���� ������ ������
	IsFinished		bit		not null	default(0),		-- ����� �������� (�� ������ ����������� � ��������)
	constraint Orders_PK		primary key (Id),								-- ��������� ���� �� ��
	constraint OrdersClient_FK	foreign key (ClientId)	references Persons(Id),	-- ������� ���� � ������� ������ ������ �������
	constraint OrdersCar_FK		foreign key (CarId)		references Cars(Id)		-- ������� ���� � ������� �����������
);


-- ������ � ������
create table OrderServices(
	Id			int		not null	identity(1,1),	-- ��
	OrderId		int		not null,					-- �����
	ServiceId	int		not null,					-- ���������� �� ������ (������� ServicesInfos)
	constraint OrderServices_PK			primary key (Id),										-- ��������� ���� �� ��
	constraint OrderServicesOrder_FK	foreign key (OrderId)	references Orders(Id),			-- ������� ���� � ������� �������
	constraint OrderServicesService_FK	foreign key (ServiceId) references ServicesInfos(Id)	-- ������� ���� � ������� �����
);


-- ����������� (��������� ����������� ������ ��� ������������� ������ �� ������������ ������)
create table Executors(
	Id				int		not null	identity(1,1),	-- ��
	ServiceId		int		not null,					-- ������ � ������ (������� OrderServices)
	EmployeeId		int		not null,					-- ��������, ����������� ������
	constraint Executors_PK			primary key (Id),											-- ��������� ���� �� ��
	constraint ExecutorsService_FK	foreign key (ServiceId)		references OrderServices(Id),	-- ������� ���� � ������� ����� � ������
	constraint ExecutorsEmployee_FK foreign key (EmployeeId)	references Employees(Id)		-- ������� ���� � ������� ����������
);