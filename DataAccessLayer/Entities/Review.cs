using System;
using System.Collections.Generic;

namespace Catalog.DataAccessLayer.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public int ProductId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
