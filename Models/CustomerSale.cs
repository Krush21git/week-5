using System;
using System.Collections.Generic;

namespace IndustryConnect_Week5_WebApi.Models;

public partial class CustomerSale
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateSold { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalPurchases { get; set; }
}
