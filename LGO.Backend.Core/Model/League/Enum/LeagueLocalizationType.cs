using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Core.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueLocalizationType
    {
        Undefined,
        Czech,
        Greek,
        Polish,
        Romanian,
        Hungarian,
        EnglishUnitedKingdom,
        German,
        SpanishSpain,
        Italian,
        French,
        Japanese,
        Korean,
        SpanishMexico,
        SpanishArgentina,
        PortugueseBrazil,
        EnglishUnitedStates,
        EnglishAustralia,
        Russian,
        Turkish,
        Malay,
        EnglishPhilippines,
        EnglishSingapore,
        Thai,
        Vietnamese,
        Indonesian,
        ChineseMalaysia,
        ChineseChina,
        ChineseTaiwan,
    }

    public static class LeagueLocalizationExtensions
    {
        private static BidirectionalStringMapping<LeagueLocalizationType> CountryCodes { get; } = new((LeagueLocalizationType.Undefined, "Undefined"),
                                                                                                      (LeagueLocalizationType.Czech, "cs_CZ"),
                                                                                                      (LeagueLocalizationType.Greek, "el_GR"),
                                                                                                      (LeagueLocalizationType.Polish, "pl_PL"),
                                                                                                      (LeagueLocalizationType.Romanian, "ro_RO"),
                                                                                                      (LeagueLocalizationType.Hungarian, "hu_HU"),
                                                                                                      (LeagueLocalizationType.EnglishUnitedKingdom, "en_GB"),
                                                                                                      (LeagueLocalizationType.German, "de_DE"),
                                                                                                      (LeagueLocalizationType.SpanishSpain, "es_ES"),
                                                                                                      (LeagueLocalizationType.Italian, "it_IT"),
                                                                                                      (LeagueLocalizationType.French, "fr_FR"),
                                                                                                      (LeagueLocalizationType.Japanese, "ja_JP"),
                                                                                                      (LeagueLocalizationType.Korean, "ko_KR"),
                                                                                                      (LeagueLocalizationType.SpanishMexico, "es_MX"),
                                                                                                      (LeagueLocalizationType.SpanishArgentina, "es_AR"),
                                                                                                      (LeagueLocalizationType.PortugueseBrazil, "pt_BR"),
                                                                                                      (LeagueLocalizationType.EnglishUnitedStates, "en_US"),
                                                                                                      (LeagueLocalizationType.EnglishAustralia, "en_AU"),
                                                                                                      (LeagueLocalizationType.Russian, "ru_RU"),
                                                                                                      (LeagueLocalizationType.Turkish, "tr_TR"),
                                                                                                      (LeagueLocalizationType.Malay, "ms_MY"),
                                                                                                      (LeagueLocalizationType.EnglishPhilippines, "en_PH"),
                                                                                                      (LeagueLocalizationType.EnglishSingapore, "en_SG"),
                                                                                                      (LeagueLocalizationType.Thai, "th_TH"),
                                                                                                      (LeagueLocalizationType.Vietnamese, "vn_VN"),
                                                                                                      (LeagueLocalizationType.Indonesian, "id_ID"),
                                                                                                      (LeagueLocalizationType.ChineseMalaysia, "zh_MY"),
                                                                                                      (LeagueLocalizationType.ChineseChina, "zh_CN"),
                                                                                                      (LeagueLocalizationType.ChineseTaiwan, "zh_TW"));

        public static string ToCountryCode(this LeagueLocalizationType thisLocalizationType)
        {
            return CountryCodes.Get(thisLocalizationType);
        }

        public static LeagueLocalizationType Parse(string countryCode)
        {
            return CountryCodes.Get(countryCode);
        }
    }
}