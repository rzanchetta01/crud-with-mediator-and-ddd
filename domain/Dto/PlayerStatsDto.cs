namespace domain.Dto;

public record PlayerStatsDto
(
    Guid Id,
    Guid PlayerId,
    Guid TeamId,
    Guid OpponentTeamId,
    short Points,
    short Rebounds,
    short Assists,
    short Turnover,
    DateTime MatchDate
) { }