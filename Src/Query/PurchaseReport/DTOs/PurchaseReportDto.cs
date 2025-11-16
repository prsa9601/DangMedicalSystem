using Common.Query;
using Common.Query.Filter;
using Domain.ProductAgg;
using Query.Product.DTOs;
using Query.User.DTOs;

namespace Query.PurchaseReport.DTOs
{
    public class PurchaseReportDto : BaseDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        //اطلاعات کل خرید آن زمان
        public string TotalPrice { get; set; }
        public string TotalDang { get; set; }
        public string TotalProfit { get; set; }
        //اطلاعات خریداری شده
        public string PurchasePrice { get; set; }
        public int PurchaseDang { get; set; }
        public string Profit { get; set; }
        //اطلاعات اون موقع بر اساس یک دانگ
        public string ProfitPerDang { get; set; }
        public string PurchasePricePerDang { get; set; }
        public int PurchaseDangPerDang { get; set; }
    }
    public class PurchaseReportFilterParam : BaseFilterParam
    {
        public PurchaseReportFilter? PurchaseReportFilter { get; set; }
        public Guid? ProductId { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public enum PurchaseReportFilter
    {
        None,
        HightPrice,
        LowPrice
    }

    public class PurchaseReportFilterResult : BaseFilter<PurchaseReportDto, PurchaseReportFilterParam>
    {

    }
    public class PurchaseReportUserInvestmentFilterResult : BaseFilter<UserPurchaseReportDto, PurchaseReportFilterParam>
    {

    }

    public class ProductPurchaseReportDto
    {
        public string  Title { get; set; }
        public string ImageName { get; set; }
        public Guid PurchaseId { get; set; }
    }
    public class UserPurchaseReportDto
    {
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string Lastame { get; set; }
        public int InvestmentCount { get; set; }
        public List<ProductPurchaseReportDto> ProductPurchase { get; set; }
        public List<PurchaseReportDto> PurchaseReport { get; set; }

        //public UserBankAccountDto BankAccount { get; set; }

    }
}
