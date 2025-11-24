namespace Common.Domain.BankInformationValidation.Strategy
{
    public class CardNumberValidate : IBankInfoVerification
    {
        //Luhn Algorithm
        public bool Verify(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 16)
                return false;

            int sum = 0;
            bool alternate = false;

            for (int i = value.Length - 1; i >= 0; i--)
            {
                int digit = value[i] - '0';

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit = (digit % 10) + 1;
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }
    }
}
