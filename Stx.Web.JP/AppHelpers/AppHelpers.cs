﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stx.Web.JP
{
    public static class AppHelpers
    {
        public static List<SelectListItem> TimeZones = new List<SelectListItem>
            {
               new SelectListItem { Value="-12",Text ="(GMT-12:00) International Date Line West" },
                            new SelectListItem { Value="-11",Text ="(GMT-11:00) Midway Island, Samoa" },
                            new SelectListItem { Value="-10",Text ="(GMT-10:00) Hawaii" },
                            new SelectListItem { Value="-9",Text ="(GMT-09:00) Alaska" },
                            new SelectListItem { Value="-8",Text ="(GMT-08:00) Pacific Time (US & Canada)" },
                            new SelectListItem { Value="-8",Text ="(GMT-08:00) Tijuana, Baja California" },
                            new SelectListItem { Value="-7",Text ="(GMT-07:00) Arizona" },
                            new SelectListItem { Value="-7",Text ="(GMT-07:00) Chihuahua, La Paz, Mazatlan" },
                            new SelectListItem { Value="-7",Text ="(GMT-07:00) Mountain Time (US & Canada)" },
                            new SelectListItem { Value="-6",Text ="(GMT-06:00) Central America" },
                            new SelectListItem { Value="-6",Text ="(GMT-06:00) Central Time (US & Canada)" },
                            new SelectListItem { Value="-6",Text ="(GMT-06:00) Guadalajara, Mexico City, Monterrey" },
                            new SelectListItem { Value="-6",Text ="(GMT-06:00) Saskatchewan" },
                            new SelectListItem { Value="-5",Text ="(GMT-05:00) Bogota, Lima, Quito, Rio Branco" },
                            new SelectListItem { Value="-5",Text ="(GMT-05:00) Eastern Time (US & Canada)" },
                            new SelectListItem { Value="-5",Text ="(GMT-05:00) Indiana (East)" },
                            new SelectListItem { Value="-4",Text ="(GMT-04:00) Atlantic Time (Canada)" },
                            new SelectListItem { Value="-4",Text ="(GMT-04:00) Caracas, La Paz" },
                            new SelectListItem { Value="-4",Text ="(GMT-04:00) Manaus" },
                            new SelectListItem { Value="-4",Text ="(GMT-04:00) Santiago" },
                            new SelectListItem { Value="-3.5",Text ="(GMT-03:30) Newfoundland" },
                            new SelectListItem { Value="-3",Text ="(GMT-03:00) Brasilia" },
                            new SelectListItem { Value="-3",Text ="(GMT-03:00) Buenos Aires, Georgetown" },
                            new SelectListItem { Value="-3",Text ="(GMT-03:00) Greenland" },
                            new SelectListItem { Value="-3",Text ="(GMT-03:00) Montevideo" },
                            new SelectListItem { Value="-2",Text ="(GMT-02:00) Mid-Atlantic" },
                            new SelectListItem { Value="-1",Text ="(GMT-01:00) Cape Verde Is." },
                            new SelectListItem { Value="-1",Text ="(GMT-01:00) Azores" },
                            new SelectListItem { Value="0",Text ="(GMT+00:00) Casablanca, Monrovia, Reykjavik" },
                            new SelectListItem { Value="0",Text ="(GMT+00:00) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London" },
                            new SelectListItem { Value="1",Text ="(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna" },
                            new SelectListItem { Value="1",Text ="(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague" },
                            new SelectListItem { Value="1",Text ="(GMT+01:00) Brussels, Copenhagen, Madrid, Paris" },
                            new SelectListItem { Value="1",Text ="(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb" },
                            new SelectListItem { Value="1",Text ="(GMT+01:00) West Central Africa" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Amman" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Athens, Bucharest, Istanbul" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Beirut" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Cairo" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Harare, Pretoria" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Jerusalem" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Minsk" },
                            new SelectListItem { Value="2",Text ="(GMT+02:00) Windhoek" },
                            new SelectListItem { Value="3",Text ="(GMT+03:00) Kuwait, Riyadh, Baghdad" },
                            new SelectListItem { Value="3",Text ="(GMT+03:00) Moscow, St. Petersburg, Volgograd" },
                            new SelectListItem { Value="3",Text ="(GMT+03:00) Nairobi" },
                            new SelectListItem { Value="3",Text ="(GMT+03:00) Tbilisi" },
                            new SelectListItem { Value="3.5",Text ="(GMT+03:30) Tehran" },
                            new SelectListItem { Value="4",Text ="(GMT+04:00) Abu Dhabi, Muscat" },
                            new SelectListItem { Value="4",Text ="(GMT+04:00) Baku" },
                            new SelectListItem { Value="4",Text ="(GMT+04:00) Yerevan" },
                            new SelectListItem { Value="4.5",Text ="(GMT+04:30) Kabul" },
                            new SelectListItem { Value="5",Text ="(GMT+05:00) Yekaterinburg" },
                            new SelectListItem { Value="5",Text ="(GMT+05:00) Islamabad, Karachi, Tashkent" },
                            new SelectListItem { Value="5.5",Text ="(GMT+05:30) Sri Jayawardenapura" },
                            new SelectListItem { Value="5.5",Text ="(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi" },
                            new SelectListItem { Value="5.75",Text ="(GMT+05:45) Kathmandu" },
                            new SelectListItem { Value="6",Text ="(GMT+06:00) Almaty, Novosibirsk" },
                            new SelectListItem { Value="6",Text ="(GMT+06:00) Astana, Dhaka" },
                            new SelectListItem { Value="6.5",Text ="(GMT+06:30) Yangon (Rangoon)" },
                            new SelectListItem { Value="7",Text ="(GMT+07:00) Bangkok, Hanoi, Jakarta" },
                            new SelectListItem { Value="7",Text ="(GMT+07:00) Krasnoyarsk" },
                            new SelectListItem { Value="8",Text ="(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi" },
                            new SelectListItem { Value="8",Text ="(GMT+08:00) Kuala Lumpur, Singapore" },
                            new SelectListItem { Value="8",Text ="(GMT+08:00) Irkutsk, Ulaan Bataar" },
                            new SelectListItem { Value="8",Text ="(GMT+08:00) Perth" },
                            new SelectListItem { Value="8",Text ="(GMT+08:00) Taipei" },
                            new SelectListItem { Value="9",Text ="(GMT+09:00) Osaka, Sapporo, Tokyo" },
                            new SelectListItem { Value="9",Text ="(GMT+09:00) Seoul" },
                            new SelectListItem { Value="9",Text ="(GMT+09:00) Yakutsk" },
                            new SelectListItem { Value="9.5",Text ="(GMT+09:30) Adelaide" },
                            new SelectListItem { Value="9.5",Text ="(GMT+09:30) Darwin" },
                            new SelectListItem { Value="10",Text ="(GMT+10:00) Brisbane" },
                            new SelectListItem { Value="10",Text ="(GMT+10:00) Canberra, Melbourne, Sydney" },
                            new SelectListItem { Value="10",Text ="(GMT+10:00) Hobart" },
                            new SelectListItem { Value="10",Text ="(GMT+10:00) Guam, Port Moresby" },
                            new SelectListItem { Value="10",Text ="(GMT+10:00) Vladivostok" },
                            new SelectListItem { Value="11",Text ="(GMT+11:00) Magadan, Solomon Is., New Caledonia" },
                            new SelectListItem { Value="12",Text ="(GMT+12:00) Auckland, Wellington" },
                            new SelectListItem { Value="12",Text ="(GMT+12:00) Fiji, Kamchatka, Marshall Is." },
                            new SelectListItem { Value="13",Text ="(GMT+13:00) Nuku'alofa" }
            };
        public struct Messages
        {
            public static string SuccessMessage = "Save successful";
            public static string ErrorMessage = "Error occured ";
        }
    }
}
