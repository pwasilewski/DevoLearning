namespace Nihdi.DevoLearning.Contracts.Persons.Create
{
    using System;

    public class PersonCreateDto
    {
        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public int? GenderId
        {
            get; set;
        }

        public int? CivilStateId
        {
            get; set;
        }

        public DateTime? BirthDate
        {
            get; set;
        }

        public DateTime? DeceasedDate
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string Mobile
        {
            get; set;
        }
    }
}
