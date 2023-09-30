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
    
    //To dto
    public partial PlayerDto Map(Player player);
    public partial PlayerStatsDto Map(PlayerStat playerStat);
    public partial TeamDto Map(Team team);
    public partial TeamStatsDto Map(TeamStat teamStat);

}