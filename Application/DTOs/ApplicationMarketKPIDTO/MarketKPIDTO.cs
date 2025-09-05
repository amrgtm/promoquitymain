using Domain.Entities;

namespace Application.DTOs.ApplicationMarketKPIDTO
{
    public class MarketKPIDTO: CommonFields
    {
        public long CompanyId { get; set; }
        public string Beta { get; set; }
        public string LowMarketCap { get; set; }
        public string HighMarketCap { get; set; }
        public string MedMarketCap { get; set; }
        public string RetailSales { get; set; }
        public string ActiveUsers { get; set; }
        public string Volume { get; set; }
        public string Duedegi { get; set; }
        public string Description { get; set; }
    }
}
