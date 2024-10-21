using System;
using System.Collections.Generic;

namespace RentalMotorbike.BusinessObject;

public partial class RentalDetail
{
    public int RentalDetailId { get; set; }

    public int RentalId { get; set; }

    public int DaysRented { get; set; }

    public decimal DailyPrice { get; set; }

    public decimal TotalCost { get; set; }

    public virtual Rental Rental { get; set; } = null!;
}
