using System;

namespace homework8
{
    internal class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return
                $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, MiddleName: {MiddleName}, BirthDate: {BirthDate}";
        }
    }
}