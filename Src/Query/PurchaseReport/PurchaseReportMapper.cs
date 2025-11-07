using Query.PurchaseReport.DTOs;

namespace Query.PurchaseReport
{
    public static class PurchaseReportMapper
    {
        public static PurchaseReportDto Map(this Domain.PurchaseReportAgg.PurchaseReport purchaseReport)
        {
            return new PurchaseReportDto
            {
                CreationDate = purchaseReport.CreationDate,
                Id = purchaseReport.Id,
                ProductId = purchaseReport.ProductId,
                Profit = purchaseReport.Profit,
                ProfitPerDang = purchaseReport.ProfitPerDang,
                PurchaseDang = purchaseReport.PurchaseDang,
                PurchaseDangPerDang = purchaseReport.PurchaseDangPerDang,
                PurchasePricePerDang = purchaseReport.PurchasePricePerDang,
                TotalDang = purchaseReport.TotalDang,
                PurchasePrice = purchaseReport.PurchasePrice,
                TotalPrice = purchaseReport.TotalPrice,
                TotalProfit = purchaseReport.TotalProfit,
                UserId = purchaseReport.UserId,
            };
        }
    }
}