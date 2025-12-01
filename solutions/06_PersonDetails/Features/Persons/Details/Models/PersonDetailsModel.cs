namespace Nihdi.DevoLearning.Presentation.Features.Persons.Details.Models
{
    public class PersonDetailsModel
    {
        public int Id
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public string FirstName
        {
            get; set;
        }

        public int GenderId
        {
            get; set;
        }

        public int CivilStateId
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

        public DateTime CreatedOn
        {
            get; set;
        }

        public string CreatedBy
        {
            get; set;
        }

        public DateTime ModifiedOn
        {
            get; set;
        }

        public string ModifiedBy
        {
            get; set;
        }
    }
}
