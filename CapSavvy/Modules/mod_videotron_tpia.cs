/*

CapSavvy, a program to track bandwidth usage for customers of TekSavvy and their resellers
Copyright (C) 2014 Adam Zey, Darryl Tam

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

*/

using System;
using System.Text.RegularExpressions;
using CapSavvy.Data;

namespace CapSavvy.Modules
{
    public class mod_videotron_tpia : mod_base
    {
        public mod_videotron_tpia(UsageData usageData)
            : base(usageData) // Call base constructor
        {
            interval = 7200; // Two hours
        }

        public override string moduleName
        {
            get { return "Videotron TPIA"; }
        }

        public override string validUsernameRegex
        {
            get { return @"^vl[a-z]{6}$"; }
        }

        public override bool supportsOffPeak
        {
            get { return false; }
        }

        public override bool tracksUploads
        {
            get { return true; } // TODO Defaulting to true to match behaviour before this property was added.  Should confirm what the real value should be
        }

        protected override string GetHTML()
        {
            return GetHTTPS("https://extranet.videotron.com/services/secur/extranet/tpia/Usage.do?lang=ENGLISH&compteInternet=" + _username);
        }

        protected override bool ParseHTML(string html)
        {
            Match match = Regex.Match(html,
                @"<td [^>]+>" + DateTime.Now.ToString("yyyy-MM") + @"-01 to<br />" + DateTime.Now.ToString("yyyy-MM-dd") + @"</td>\s+" +
                @"<td [^>]+>[\d\.,]+</td>\s+" +
                @"<td [^>]+>([\d\.,]+)</td>\s+" +
                @"<td [^>]+>[\d\.,]+</td>\s+" +
                @"<td [^>]+>([\d\.,]+)</td>\s+" +
                @"<td [^>]+>[\d\.,]+</td>\s+" +
                @"<td [^>]+>([\d\.,]+)</td>\s+",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (match.Success)
            {
                usage.RealTime = false; // TODO Using this as the default to match previous behaviour.  Should be updated to proper value later

                usage.All.Down = Convert.ToDouble(match.Groups[1].Value);
                usage.All.Up = Convert.ToDouble(match.Groups[2].Value);
                usage.All.Total = Convert.ToDouble(match.Groups[3].Value);

                // Videotron doesn't support peak/offpeak concept
                usage.OffPeak.Down = 0;
                usage.OffPeak.Up = 0;
                usage.OffPeak.Total = 0;
                usage.Peak.Down = usage.All.Down;
                usage.Peak.Up = usage.All.Up;
                usage.Peak.Total = usage.All.Total;
            }

            return match.Success;
        }
    }
}
