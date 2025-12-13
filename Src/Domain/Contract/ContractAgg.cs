using Common.Domain;

namespace Domain.Contract
{
    public class ContractAgg : AggregateRoot
    {
        public ContractAgg(string phoneNumber, string email, string fullName, string title, string description)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            FullName = fullName;
            Title = title;
            Description = description;
            Status = ContractStatus.New;
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
            Status = ContractStatus.Answered;
        }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ContractStatus Status { get; set; }
    }
}
