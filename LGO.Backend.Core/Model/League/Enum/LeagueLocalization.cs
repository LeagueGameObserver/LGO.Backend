namespace LGO.Backend.Core.Model.League.Enum
{
    public enum LeagueLocalization
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
        private static BidirectionalStringMapping<LeagueLocalization> CountryCodes { get; } = new((LeagueLocalization.Undefined, "Undefined"),
                                                                                                  (LeagueLocalization.Czech, "cs_CZ"),
                                                                                                  (LeagueLocalization.Greek, "el_GR"),
                                                                                                  (LeagueLocalization.Polish, "pl_PL"),
                                                                                                  (LeagueLocalization.Romanian, "ro_RO"),
                                                                                                  (LeagueLocalization.Hungarian, "hu_HU"),
                                                                                                  (LeagueLocalization.EnglishUnitedKingdom, "en_GB"),
                                                                                                  (LeagueLocalization.German, "de_DE"),
                                                                                                  (LeagueLocalization.SpanishSpain, "es_ES"),
                                                                                                  (LeagueLocalization.Italian, "it_IT"),
                                                                                                  (LeagueLocalization.French, "fr_FR"),
                                                                                                  (LeagueLocalization.Japanese, "ja_JP"),
                                                                                                  (LeagueLocalization.Korean, "ko_KR"),
                                                                                                  (LeagueLocalization.SpanishMexico, "es_MX"),
                                                                                                  (LeagueLocalization.SpanishArgentina, "es_AR"),
                                                                                                  (LeagueLocalization.PortugueseBrazil, "pt_BR"),
                                                                                                  (LeagueLocalization.EnglishUnitedStates, "en_US"),
                                                                                                  (LeagueLocalization.EnglishAustralia, "en_AU"),
                                                                                                  (LeagueLocalization.Russian, "ru_RU"),
                                                                                                  (LeagueLocalization.Turkish, "tr_TR"),
                                                                                                  (LeagueLocalization.Malay, "ms_MY"),
                                                                                                  (LeagueLocalization.EnglishPhilippines, "en_PH"),
                                                                                                  (LeagueLocalization.EnglishSingapore, "en_SG"),
                                                                                                  (LeagueLocalization.Thai, "th_TH"),
                                                                                                  (LeagueLocalization.Vietnamese, "vn_VN"),
                                                                                                  (LeagueLocalization.Indonesian, "id_ID"),
                                                                                                  (LeagueLocalization.ChineseMalaysia, "zh_MY"),
                                                                                                  (LeagueLocalization.ChineseChina, "zh_CN"),
                                                                                                  (LeagueLocalization.ChineseTaiwan, "zh_TW"));

        public static string ToCountryCode(this LeagueLocalization thisLocalization)
        {
            return CountryCodes.Get(thisLocalization);
        }

        public static LeagueLocalization Parse(string countryCode)
        {
            return CountryCodes.Get(countryCode);
        }
    }
}