using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.Dtos
{
    public class SaleDto
    {
            public int Id { get; set; }
            public string StoreName { get; set; }
            public string CustomerName { get; set; }
            public string ProductName { get; set; }
            public DateTime SaleDate { get; set; }

    }
}
