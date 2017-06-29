namespace Maintenance_RolicGoronok
{
    partial class Orders
    {
        public string ShortBeginDate {
            get { return BeginDate.ToShortDateString(); }
        }

        public string ShortFinishDate {
            get { return FinishDate.ToShortDateString(); }
        }
    }// class Orders

    partial class Executors
    {
        public override string ToString()
        {
            return $"{Employees}";
        }// ToString
    }// class Executors

    partial class Employees
    {
        public override string ToString()
        {
            return $"{Persons}";
        }// ToString
    }// class Employees

    partial class ServicesInfos
    {
        public override string ToString()
        {
            return Name;
        }// ToString
    }// class ServicesInfos

    partial class Malfunctions
    {
        public override string ToString()
        {
            return Name;
        }// ToString
    }// class Malfunctions

    partial class Persons
    {
        public override string ToString()
        {
            return $"{Surname} {Name[0]}.{Patronymic[0]}.";
        }// ToString
    }// class Persons

    partial class Models
    {
        public override string ToString()
        {
            return Name;
        }// ToString
    }// class Models

    partial class Specialities
    {
        public override string ToString()
        {
            return Name;
        }// ToString
    }// class Specialities

    partial class Categories
    {
        public override string ToString()
        {
            return Name;
        }// ToString
    }// class Categories
}