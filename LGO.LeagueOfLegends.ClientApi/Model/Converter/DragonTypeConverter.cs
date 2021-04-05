using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class DragonTypeConverter : AbstractStringEnumConverter<LolDragonType>
    {
        public DragonTypeConverter() : base((LolDragonType.Undefined, "UNDEFINED"),
                                            (LolDragonType.Infernal, "FIRE"),
                                            (LolDragonType.Ocean, "WATER"),
                                            (LolDragonType.Mountain, "EARTH"),
                                            (LolDragonType.Cloud, "WIND"),
                                            (LolDragonType.Elder, "ELDER")) { }
    }
}