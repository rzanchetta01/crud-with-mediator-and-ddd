using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace domain.Entities;

public partial class PlayerStat : BaseEntity
{
    public Guid PlayerId { get; set; }
    public Guid TeamId { get; set; }
    public Guid OpponentTeamId { get; set; }
    public short Points { get; set; }
    public short Rebounds { get; set; }
    public short Assists { get; set; }
    public short Turnover { get; set; }
    public DateTime MatchDate { get; set; }
    public virtual Team OpponentTeam { get; set; } = null!;
    public virtual Player Player { get; set; } = null!;
    public virtual Team Team { get; set; } = null!;
}
