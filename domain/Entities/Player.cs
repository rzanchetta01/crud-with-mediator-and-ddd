using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace domain.Entities;

public partial class Player : BaseEntity
{
    public string Name { get; set; } = null!;
    public short Age { get; set; }
    public Guid TeamId { get; set; }
    public decimal Salary { get; set; }
    public virtual ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();
    public virtual Team Team { get; set; } = null!;
}
