using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace domain.Entities;

public partial class Team : BaseEntity
{
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public virtual ICollection<PlayerStat> PlayerStatOpponentTeams { get; set; } = new List<PlayerStat>();
    public virtual ICollection<PlayerStat> PlayerStatTeams { get; set; } = new List<PlayerStat>();
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
