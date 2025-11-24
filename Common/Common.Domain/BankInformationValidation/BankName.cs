namespace Common.Domain.BankInformationValidation
{
    public static class BankName
    {
        public static string GetBankNameFromCard(this string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 6)
                return "نامشخص";

            string prefix = cardNumber.Substring(0, 6);

            var bankPrefixes = new Dictionary<string, string>
            {
                {"603799", "بانک ملی"},
                {"589210", "بانک سپه"},
                {"627648", "بانک توسعه صادرات"},
                {"627961", "بانک صنعت و معدن"},
                {"603770", "بانک کشاورزی"},
                {"628023", "بانک مسکن"},
                {"627760", "پست بانک"},
                {"502908", "بانک توسعه تعاون"},
                {"627412", "بانک اقتصاد نوین"},
                {"622106", "بانک پارسیان"},
                {"502229", "بانک پاسارگاد"},
                {"627488", "بانک کارآفرین"},
                {"621986", "بانک سامان"},
                {"639346", "بانک سینا"},
                {"639607", "بانک سرمایه"},
                {"636214", "بانک تات"},
                {"502806", "بانک شهر"},
                {"502938", "بانک دی"},
                {"603769", "بانک صادرات"},
                {"610433", "بانک ملت"},
                {"627353", "بانک تجارت"},
                {"589463", "بانک رفاه"},
                {"627381", "بانک انصار"},
                {"639370", "بانک مهر اقتصاد"}
            };

            foreach (var prefixRange in bankPrefixes)
            {
                if (prefix.StartsWith(prefixRange.Key))
                    return prefixRange.Value;
            }

            return "نامشخص";
        }
    }
}
