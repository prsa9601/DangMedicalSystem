using Common.Domain;

namespace Domain.Contact
{
    public class ContactAgg : AggregateRoot
    {
        public ContactAgg(string phoneNumber, string email, string fullName, string title, string description)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            FullName = fullName;
            Title = title;
            Description = description;
            Status = Domain.Contact.ContactStatus.New;
        }
        public void Edit(string phoneNumber, string email, string fullName, string title, string description)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            FullName = fullName;
            Title = title;
            Description = description;
        }
        public void IsAnswered()
        {
            Status = Domain.Contact.ContactStatus.Answered;
        }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Domain.Contact.ContactStatus Status { get; set; }
    }
}
