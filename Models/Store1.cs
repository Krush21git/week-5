using System;
using System.Collections.Generic;

namespace IndustryConnect_Week5_WebApi.Models;

public partial class Store1
{
    public int StoreId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;
}
