using System;
using System.Collections.Generic;

namespace IndustryConnect_Week5_WebApi.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Active { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
