using domain.Dto;
using domain.Entities;
using Riok.Mapperly.Abstractions;

namespace domain.Mapping;

[Mapper]
public partial class MapperlyMapper
{
    //To entity
    public partial Player Map(PlayerDto playerDto);
    public partial PlayerStat Map(PlayerStatsDto playerStatsDto);
    public partial Team Map(TeamDto teamDto);
    public partial TeamStat Map(TeamStatsDto teamStatsDto);
    
    public partial ICollection<Player> Map(ICollection<PlayerDto> playerDto);
    public partial ICollection<PlayerStat> Map(ICollection<PlayerStatsDto> playerStatsDto);
    public partial ICollection<Team> Map(ICollection<TeamDto> teamDto);
    public partial ICollection<TeamStat> Map(ICollection<TeamStatsDto> teamStatsDto);
    
    //To dto
    public partial PlayerDto Map(Player player);
    public partial PlayerStatsDto Map(PlayerStat playerStat);
    public partial TeamDto Map(Team team);
    public partial TeamStatsDto Map(TeamStat teamStat);
    public partial ICollection<PlayerDto> Map(ICollection<Player> player);
    public partial ICollection<PlayerStatsDto> Map(ICollection<PlayerStat> playerStat);
    public partial ICollection<TeamDto> Map(ICollection<Team> team);
    public partial ICollection<TeamStatsDto> Map(ICollection<TeamStat> teamStat);

}