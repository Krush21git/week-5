using IndustryConnect_Week5_WebApi.Dtos;
using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.Mappers
{
    public class CustomerMapper
    {
        public static CustomerDto EntityToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                // Map other properties
            };
        }

        public static Customer DtoToEntity(CustomerDto customerDto)
        {
            return new Customer
            {
                Id = customerDto.Id,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                DateOfBirth = customerDto.DateOfBirth,
                // Map other properties
            };
        }
    }
}
