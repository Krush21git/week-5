using System;
using System.Collections.Generic;

namespace IndustryConnect_Week5_WebApi.Models;

public partial class CustomerSaleView1
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime? DateSold { get; set; }
}
