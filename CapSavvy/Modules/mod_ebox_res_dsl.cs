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
    public class mod_ebox_res_dsl : mod_base
    {
        public mod_ebox_res_dsl(UsageData usageData) : base(usageData) { } // Call base constructor

        public override string moduleName
        {
            get { return "Electronicbox Residential DSL"; }
        }

        public override string validUsernameRegex
        {
            get { return @"^[a-z0-9_\-\.]{3,}@(data\.com|ebox\.com|electronicbox\.net|highspeed\.com|internet\.com|ppp\.com|www\.com)$"; }
        }

        public override bool supportsOffPeak
        {
            get { return true; }
        }

        protected override string GetHTML()
        {
            return GetHTTP("www.electronicbox.net", 80, "/en/billing-and-other/internet-usage.html", "retry=0&language=en_US&username=" + _username.Replace("@", "%40"));
        }

        protected override bool ParseHTML(string html)
        {
            Match match = Regex.Match(html,
                @"<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>\s*<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>\s*<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>.*?" +
                @"<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>\s*<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>\s*<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>.*?" +
                @"<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>\s*<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>\s*<tr>\s*<th>[^<]+</th>\s*<td>([\d\.,]+) GB</td>\s*</tr>",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            /*
             <tr>
								<th>Download</th>
								<td>104.84 GB</td>
							</tr>
							<tr>
								<th>Upload</th>
								<td>19.85 GB</td>
							</tr>
							<tr>
								<th>Combine</th>
								<td>124.7 GB</td>
							</tr>
             */

            if (match.Success)
            {
                usage.All.Down = Convert.ToDouble(match.Groups[1].Value);
                usage.All.Up = Convert.ToDouble(match.Groups[2].Value);
                usage.All.Total = Convert.ToDouble(match.Groups[3].Value);

                usage.Peak.Down = Convert.ToDouble(match.Groups[4].Value);
                usage.Peak.Up = Convert.ToDouble(match.Groups[5].Value);
                usage.Peak.Total = Convert.ToDouble(match.Groups[6].Value);

                usage.OffPeak.Down = Convert.ToDouble(match.Groups[7].Value);
                usage.OffPeak.Up = Convert.ToDouble(match.Groups[8].Value);
                usage.OffPeak.Total = Convert.ToDouble(match.Groups[9].Value);
            }

            return match.Success;
        }
    }
}
