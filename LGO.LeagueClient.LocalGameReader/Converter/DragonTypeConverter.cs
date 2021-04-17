using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class DragonTypeConverter : ReadonlyStringEnumConverter<LeagueDragonType>
    {
        public DragonTypeConverter() : base(LeagueDragonType.Undefined,
                                            ("FIRE", LeagueDragonType.Infernal),
                                            ("WATER", LeagueDragonType.Ocean),
                                            ("EARTH", LeagueDragonType.Mountain),
                                            ("WIND", LeagueDragonType.Cloud),
                                            ("ELDER", LeagueDragonType.Elder))
        {
        }
    }
}