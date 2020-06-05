using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Help
{
    public class TimeZoneHelper
    {
        private const String TIME_ZONE_WIN_ID_MID_ATL_STAD = "Mid-Atlantic Standard Time";
        private const string TIME_ZONE_WIN_ID_KAMCHATKA = "Kamchatka Standard Time";
        private const String TIME_ZONE_ID_MID_ATL_STAD = "Atlantic/South_Georgia";
        private const String TIME_ZONE_ID_KAMCHATKA = "Asia/Kamchatka";


        private static IReadOnlyDictionary<string, string> TimeZoneWinIdToTimeZoneMap { get; } = new Dictionary<string, string>{
            {"Dateline Standard Time", "Etc/GMT+12"},
            {"UTC-11", "Etc/GMT+11"},
            {"Hawaiian Standard Time", "Pacific/Honolulu"},
            {"Aleutian Standard Time", "America/Adak"},
            {"Marquesas Standard Time", "Pacific/Marquesas"},
            {"UTC-09", "Etc/GMT+9"},
            {"Alaskan Standard Time", "America/Anchorage"},
            {"Pacific Standard Time (Mexico)", "America/Tijuana"},
            {"UTC-08", "Etc/GMT+8"},
            {"Pacific Standard Time", "America/Los_Angeles"},
            {"US Mountain Standard Time", "America/Phoenix"},
            {"Mountain Standard Time (Mexico)", "America/Chihuahua"},
            {"Mountain Standard Time", "America/Denver"},
            {"Central America Standard Time", "America/Guatemala"},
            {"Central Standard Time", "America/Chicago"},
            {"Easter Island Standard Time", "Pacific/Easter"},
            {"Central Standard Time (Mexico)", "America/Mexico_City"},
            {"Canada Central Standard Time", "America/Regina"},
            {"Eastern Standard Time", "America/New_York"},
            {"Eastern Standard Time (Mexico)", "America/Cancun"},
            {"US Eastern Standard Time", "America/Indiana/Indianapolis"},
            {"Cuba Standard Time", "America/Havana"},
            {"SA Pacific Standard Time", "America/Bogota"},
            {"Haiti Standard Time", "America/Port-au-Prince"},
            {"SA Western Standard Time", "America/La_Paz"},
            {"Paraguay Standard Time", "America/Asuncion"},
            {"Venezuela Standard Time", "America/Caracas"},
            {"Pacific SA Standard Time", "America/Santiago"},
            {"Atlantic Standard Time", "America/Halifax"},
            {"Central Brazilian Standard Time", "America/Cuiaba"},
            {"Turks And Caicos Standard Time", "America/Grand_Turk"},
            {"Newfoundland Standard Time", "America/St_Johns"},
            {"SA Eastern Standard Time", "America/Cayenne"},
            {"Saint Pierre Standard Time", "America/Miquelon"},
            {"E. South America Standard Time", "America/Sao_Paulo"},
            {"Argentina Standard Time", "America/Argentina/Buenos_Aires"},
            {"Greenland Standard Time", "America/Nuuk"},
            {"Bahia Standard Time", "America/Bahia"},
            {"Montevideo Standard Time", "America/Montevideo"},
            {"Tocantins Standard Time", "America/Araguaina"},
            {"UTC-02", "Etc/GMT+2"},
            {"Azores Standard Time", "Atlantic/Azores"},
            {"Cape Verde Standard Time", "Atlantic/Cape_Verde"},
            {"UTC", "Etc/GMT"},
            {"Morocco Standard Time", "Africa/Casablanca"},
            {"Greenwich Standard Time", "Atlantic/Reykjavik"},
            {"GMT Standard Time", "Europe/London"},
            {"W. Central Africa Standard Time", "Africa/Lagos"},
            {"Romance Standard Time", "Europe/Paris"},
            {"Namibia Standard Time", "Africa/Windhoek"},
            {"Central European Standard Time", "Europe/Warsaw"},
            {"Central Europe Standard Time", "Europe/Budapest"},
            {"W. Europe Standard Time", "Europe/Berlin"},
            {"West Bank Standard Time", "Asia/Hebron"},
            {"Kaliningrad Standard Time", "Europe/Kaliningrad"},
            {"South Africa Standard Time", "Africa/Johannesburg"},
            {"E. Europe Standard Time", "Europe/Chisinau"},
            {"Syria Standard Time", "Asia/Damascus"},
            {"Jordan Standard Time", "Asia/Amman"},
            {"Egypt Standard Time", "Africa/Cairo"},
            {"Libya Standard Time", "Africa/Tripoli"},
            {"Israel Standard Time", "Asia/Jerusalem"},
            {"Middle East Standard Time", "Asia/Beirut"},
            {"FLE Standard Time", "Europe/Kiev"},
            {"GTB Standard Time", "Europe/Bucharest"},
            {"Turkey Standard Time", "Europe/Istanbul"},
            {"E. Africa Standard Time", "Africa/Nairobi"},
            {"Arabic Standard Time", "Asia/Baghdad"},
            {"Belarus Standard Time", "Europe/Minsk"},
            {"Arab Standard Time", "Asia/Riyadh"},
            {"Russian Standard Time", "Europe/Moscow"},
            {"Iran Standard Time", "Asia/Tehran"},
            {"Russia Time Zone 3", "Europe/Samara"},
            {"Caucasus Standard Time", "Asia/Yerevan"},
            {"Azerbaijan Standard Time", "Asia/Baku"},
            {"Georgian Standard Time", "Asia/Tbilisi"},
            {"Mauritius Standard Time", "Indian/Mauritius"},
            {"Arabian Standard Time", "Asia/Dubai"},
            {"Astrakhan Standard Time", "Europe/Astrakhan"},
            {"Afghanistan Standard Time", "Asia/Kabul"},
            {"Pakistan Standard Time", "Asia/Karachi"},
            {"Ekaterinburg Standard Time", "Asia/Yekaterinburg"},
            {"West Asia Standard Time", "Asia/Tashkent"},
            {"Sri Lanka Standard Time", "Asia/Colombo"},
            {"India Standard Time", "Asia/Kolkata"},
            {"Nepal Standard Time", "Asia/Kathmandu"},
            {"Bangladesh Standard Time", "Asia/Dhaka"},
            {"Omsk Standard Time", "Asia/Omsk"},
            {"Central Asia Standard Time", "Asia/Almaty"},
            {"Myanmar Standard Time", "Asia/Yangon"},
            {"North Asia Standard Time", "Asia/Krasnoyarsk"},
            {"Altai Standard Time", "Asia/Barnaul"},
            {"Tomsk Standard Time", "Asia/Tomsk"},
            {"N. Central Asia Standard Time", "Asia/Novosibirsk"},
            {"SE Asia Standard Time", "Asia/Bangkok"},
            {"W. Mongolia Standard Time", "Asia/Hovd"},
            {"Ulaanbaatar Standard Time", "Asia/Ulaanbaatar"},
            {"North Asia East Standard Time", "Asia/Irkutsk"},
            {"China Standard Time", "Asia/Shanghai"},
            {"Taipei Standard Time", "Asia/Taipei"},
            {"Singapore Standard Time", "Asia/Singapore"},
            {"W. Australia Standard Time", "Australia/Perth"},
            {"North Korea Standard Time", "Asia/Pyongyang"},
            {"Aus Central W. Standard Time", "Australia/Eucla"},
            {"Tokyo Standard Time", "Asia/Tokyo"},
            {"Transbaikal Standard Time", "Asia/Chita"},
            {"Yakutsk Standard Time", "Asia/Yakutsk"},
            {"Korea Standard Time", "Asia/Seoul"},
            {"AUS Central Standard Time", "Australia/Darwin"},
            {"Cen. Australia Standard Time", "Australia/Adelaide"},
            {"West Pacific Standard Time", "Pacific/Port_Moresby"},
            {"AUS Eastern Standard Time", "Australia/Sydney"},
            {"E. Australia Standard Time", "Australia/Brisbane"},
            {"Vladivostok Standard Time", "Asia/Vladivostok"},
            {"Tasmania Standard Time", "Australia/Hobart"},
            {"Lord Howe Standard Time", "Australia/Lord_Howe"},
            {"Russia Time Zone 10", "Asia/Srednekolymsk"},
            {"Bougainville Standard Time", "Pacific/Bougainville"},
            {"Central Pacific Standard Time", "Pacific/Guadalcanal"},
            {"Sakhalin Standard Time", "Asia/Sakhalin"},
            {"Norfolk Standard Time", "Pacific/Norfolk"},
            {"Magadan Standard Time", "Asia/Magadan"},
            {"UTC+12", "Etc/GMT-12"},
            {"New Zealand Standard Time", "Pacific/Auckland"},
            {"Fiji Standard Time", "Pacific/Fiji"},
            {"Russia Time Zone 11", "Asia/Kamchatka"},
            {"Chatham Islands Standard Time", "Pacific/Chatham"},
            {"Tonga Standard Time", "Pacific/Tongatapu"},
            {"Samoa Standard Time", "Pacific/Apia"},
            {"Line Islands Standard Time", "Pacific/Kiritimati"},
            {TIME_ZONE_WIN_ID_MID_ATL_STAD, TIME_ZONE_ID_MID_ATL_STAD},
            {TIME_ZONE_WIN_ID_KAMCHATKA, TIME_ZONE_ID_KAMCHATKA}
        };

        public static string GetTimeZoneId(TimeZoneInfo timeZoneInfo)
        {
            var tempWindowsTimezoneId = timeZoneInfo.Id;
            if (TimeZoneWinIdToTimeZoneMap.ContainsKey(tempWindowsTimezoneId))
            {
                string result = TimeZoneWinIdToTimeZoneMap[tempWindowsTimezoneId];
                return result;
            }
            else
            {
                return TimeZoneWinIdToTimeZoneMap[TimeZoneInfo.Utc.Id];
            }
        }
    }
}
