namespace Common.Domain.BankInformationValidation.Strategy
{
    public interface IBankInfoVerification
    {
         bool Verify(string value);
    }
}
