namespace Application.DTOs.ApplicationValuationKPIDTO
{
    public class UpdateValuationKPIDTO
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string PBRatio { get; set; }
        public string DividentYield { get; set; }
        public string Ebitda { get; set; }
        public string Description { get; set; }
    }
}
