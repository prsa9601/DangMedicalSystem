
namespace Common.Domain.DomainValidation
{
    public class DecimalValidation
    {
        public static void DecimalGuard(string value)
        {
            if (!decimal.TryParse(value, out _))
            {
                throw new ArgumentException("مقدار شامل حروف نمی شود.");
            }
        }
        public static void IntGuard(string value)
        {
            if (!int.TryParse(value, out _))
            {
                throw new ArgumentException("مقدار شامل حروف نمی شود.");
            }
        }

    }
}
