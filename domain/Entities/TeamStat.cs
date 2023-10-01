using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace domain.Entities;

public partial class TeamStat : BaseEntity
{
    public Guid TeamId { get; set; }
    public short Title { get; set; }
    public virtual Team? Team { get; set; }
}
