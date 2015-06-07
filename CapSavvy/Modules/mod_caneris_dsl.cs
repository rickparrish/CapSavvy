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
    public class mod_caneris_dsl : mod_base
    {
        public mod_caneris_dsl(UsageData usageData) : base(usageData) { } // Call base constructor

        public override string moduleName
        {
            get { return "Caneris DSL"; }
        }

        public override string validUsernameRegex
        {
            // Valid logins are [a-z0-9]{3,20}@caneris (no .com on the end), but usage is retrieved by 5 digit account number.
            get { return @"^[1-9]\d{4}$"; }
        }

        public override bool supportsOffPeak
        {
            get { return false; }
        }

        protected override string GetHTML()
        {
            return GetHTTP("www.caneris.com", 80, "/Usage?", "acctnum=" + _username + "&y=" + DateTime.Now.Year.ToString() + "&m=" + DateTime.Now.Month.ToString());
        }

        protected override bool ParseHTML(string html)
        {
            // Please note that Caneris supports multiple logins per account, we only support getting the usage for the first.
            Match match = Regex.Match(html,
                @"<tr>\s*<td>[a-z0-9]{3,20}@caneris</td>\s*<td>[0-9 ]*</td>\s*<td>([\d\.,]+)\s*GB</td>\s*<td>([\d\.,]+)\s*GB</td>\s*<td>([\d\.,]+)\s*GB</td></tr>",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (match.Success)
            {
                usage.All.Down = Convert.ToDouble(match.Groups[1].Value);
                usage.All.Up = Convert.ToDouble(match.Groups[2].Value);
                usage.All.Total = Convert.ToDouble(match.Groups[3].Value);

                // Caneris doesn't support peak/offpeak concept
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
