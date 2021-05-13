namespace InvestorStore.Sales.Application.Queries.Dtos
{
    public class BasketPaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CvvCode { get; set; }
    }
}