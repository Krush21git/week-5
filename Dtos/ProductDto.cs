namespace IndustryConnect_Week5_WebApi.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? Active { get; set; }

        public decimal? Price { get; set; }
    }
}
