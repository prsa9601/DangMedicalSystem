using Common.Domain;
using Common.Domain.BankInformationValidation.Strategy;

namespace Domain.UserAgg
{
    public class UserBankAccount : BaseEntity
    {
        public string Shaba { get; private set; }
        public string CardNumber { get; private set; }
        public string FullName { get; private set; }
        //public string LastName { get; private set; }
        public bool IsConfirmed { get; private set; }
        //public int ExpirationDateMonth { get; set; }
        //public int ExpirationDateYear { get; set; }
        public Guid UserId { get; internal set; }

        private UserBankAccount()
        {
            
        }
        public UserBankAccount(string shaba, string cardNumber, string fullName)
        {
            ShabaNumberGuard(shaba);
            CardNumberGuard(cardNumber);
            Shaba = shaba;
            CardNumber = cardNumber;
            FullName = fullName;
        }

        public void EditUserBankAccount(string shaba, string cardNumber, string fullName)
        {
            ShabaNumberGuard(shaba);
            CardNumberGuard(cardNumber);
            Shaba = shaba;
            CardNumber = cardNumber;
            FullName = fullName;
        }

        public void SetAsConfirmed()
        {
            IsConfirmed = true;
        }
        
        public void SetAsNotConfirmed()
        {
            IsConfirmed = false;
        }
        
        public void ShabaNumberGuard(string value)
        {
            var bankInfoVerification = new ShabaNumberValidate();

            if (!bankInfoVerification.Verify(value))
                throw new ArgumentException("شماره شبا وارد شده معتبر نمی باشد");
        }
        
        public void CardNumberGuard(string value)
        {
            var bankInfoVerification = new CardNumberValidate();

            if (!bankInfoVerification.Verify(value))
                throw new ArgumentException("شماره کارت وارد شده معتبر نمی باشد");
        }

    }
}
