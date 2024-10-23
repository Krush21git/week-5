using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
