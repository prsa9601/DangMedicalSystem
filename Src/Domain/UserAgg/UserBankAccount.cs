using Common.Domain;

namespace Domain.UserAgg
{
    public class UserBankAccount : BaseEntity
    {
        public string Shaba { get; private set; }
        public string CardNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsConfirmed { get; private set; }
        public int ExpirationDateMonth { get; set; }
        public int ExpirationDateYear { get; set; }
        public Guid UserId { get; internal set; }

        public UserBankAccount(string shaba, string cardNumber, string firstName,
            string lastName, int expirationDateMonth, int expirationDateYear)
        {
            Shaba = shaba;
            CardNumber = cardNumber;
            FirstName = firstName;
            LastName = lastName;
            ExpirationDateMonth = expirationDateMonth;
            ExpirationDateYear = expirationDateYear;
        }

        public void EditUserBankAccount(string shaba, string cardNumber, string firstName, string lastName,
            int expirationDateMonth, int expirationDateYear)
        {
            Shaba = shaba;
            CardNumber = cardNumber;
            FirstName = firstName;
            LastName = lastName;
            ExpirationDateMonth = expirationDateMonth;
            ExpirationDateYear = expirationDateYear;
        }

        public void SetAsConfirmed()
        {
            IsConfirmed = true;
        }
        
        public void SetAsNotConfirmed()
        {
            IsConfirmed = false;
        }
    }
}
