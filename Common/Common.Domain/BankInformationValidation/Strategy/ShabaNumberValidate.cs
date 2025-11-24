using System.Text;

namespace Common.Domain.BankInformationValidation.Strategy
{
    public class ShabaNumberValidate : IBankInfoVerification
    {
        //IBAN


        public bool Verify(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 26)
                return false;

            // جابجایی ۴ کاراکتر اول به انتها
            string rearranged = value.Substring(4) + value.Substring(0, 4);

            // تبدیل حروف به اعداد
            StringBuilder numericIBAN = new StringBuilder();
            foreach (char c in rearranged)
            {
                if (char.IsLetter(c))
                    numericIBAN.Append((c - 'A' + 10).ToString());
                else
                    numericIBAN.Append(c);
            }

            // محاسبه باقیمانده تقسیم بر ۹۷
            string remainder = numericIBAN.ToString();
            while (remainder.Length > 2)
            {
                int segmentLength = Math.Min(7, remainder.Length);
                int segment = int.Parse(remainder.Substring(0, segmentLength));
                remainder = (segment % 97).ToString() + remainder.Substring(segmentLength);
            }

            return int.Parse(remainder) % 97 == 1;
        }

        //public bool Verify(string value)
        //{
        //    if (string.IsNullOrEmpty(value) || value.Length != 26)
        //        return false;

        //    // انتقال ۴ کاراکتر اول به انتها
        //    string rearranged = value.Substring(4) + value.Substring(0, 4);

        //    // تبدیل حروف به اعداد (A=10, B=11, ..., Z=35)
        //    StringBuilder numericIBAN = new StringBuilder();
        //    foreach (char c in rearranged)
        //    {
        //        if (char.IsLetter(c))
        //            numericIBAN.Append((c - 'A' + 10).ToString());
        //        else
        //            numericIBAN.Append(c);
        //    }

        //    // محاسبه باقیمانده تقسیم بر 97
        //    string remainder = numericIBAN.ToString();
        //    while (remainder.Length > 2)
        //    {
        //        int segment = int.Parse(remainder.Substring(0, 7));
        //        remainder = (segment % 97).ToString() + remainder.Substring(7);
        //    }

        //    return int.Parse(remainder) % 97 == 1;
        //}
    }
}
