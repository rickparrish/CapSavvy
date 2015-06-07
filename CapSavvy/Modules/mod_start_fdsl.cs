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
//using System.Text.RegularExpressions;
using System.Collections.Generic;
using CapSavvy.Data;

namespace CapSavvy.Modules
{
    public class mod_start_fdsl : mod_base
    {
        public mod_start_fdsl(UsageData usageData) : base(usageData) { } // Call base constructor

        public override string moduleName
        {
            get { return "Start DSL"; }
        }

        public override string validUsernameRegex
        {
            get { return @"^[A-Z0-9]{7}[A-F0-9]{11}D@(start\.ca)$"; }
        }

        public override bool supportsOffPeak
        {
            get { return true; }
        }

        protected override string GetHTML()
        {
            return GetHTTP("www.start.ca", 80, "/support/capsavvy?code=" + _username);
        }

        protected override bool ParseHTML(string html)
        {
            // Start returns ERROR if you give it an invalid username,
            // but we don't really have any special handling for that
            if (html == "ERROR")
                return false;

            // Usage seems to be in bytes.
            /*Match match = Regex.Match(html,
                @"DL=(\d+),UL=(\d+),TOTAL=(\d+)",
                RegexOptions.Singleline);*/

            //html = "lol\r\n\r\nDL=1234567890,UL=1234567890,TOTAL=2469135780,DLFREE=12345678900,ULFREE=12345678900,TOTALFREE=24691357800";

            Dictionary<string, string> usagePairs = new Dictionary<string, string>();
            bool matchSuccess = false;

            try
            {
                foreach (string node in html.Substring(html.IndexOf("\r\n\r\n") + 4).Split(','))
                {
                    string[] pair = node.Split('=');

                    if (!string.IsNullOrEmpty(pair[1]))
                    {
                        usagePairs.Add(pair[0].ToUpper(), pair[1]);
                    }
                    else
                    {
                        usagePairs.Add(pair[0].ToUpper(), "0");
                    }
                }

                if (usagePairs.Count > 0)
                {
                    matchSuccess = true;
                }
            }
            catch { }


            if (matchSuccess)
            {
                usage.Peak.Down = Convert.ToDouble(usagePairs["DL"]) / 1000000000;
                usage.Peak.Up = Convert.ToDouble(usagePairs["UL"]) / 1000000000;
                usage.Peak.Total = Convert.ToDouble(usagePairs["TOTAL"]) / 1000000000;

                // Check if off-peak stuff is available
                if (usagePairs.ContainsKey("DLFREE") && usagePairs.ContainsKey("ULFREE") && usagePairs.ContainsKey("TOTALFREE"))
                {
                    usage.OffPeak.Down = Convert.ToDouble(usagePairs["DLFREE"]) / 1000000000;
                    usage.OffPeak.Up = Convert.ToDouble(usagePairs["ULFREE"]) / 1000000000;
                    usage.OffPeak.Total = Convert.ToDouble(usagePairs["TOTALFREE"]) / 1000000000;
                }
                else
                {
                    usage.OffPeak.Down = 0;
                    usage.OffPeak.Up = 0;
                    usage.OffPeak.Total = 0;
                }

                usage.All.Down = usage.Peak.Down + usage.OffPeak.Down;
                usage.All.Up = usage.Peak.Up + usage.OffPeak.Up;
                usage.All.Total = usage.Peak.Total + usage.OffPeak.Total; ;
            }

            return matchSuccess;
        }
    }
}
