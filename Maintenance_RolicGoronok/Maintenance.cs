namespace Maintenance_RolicGoronok
{
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