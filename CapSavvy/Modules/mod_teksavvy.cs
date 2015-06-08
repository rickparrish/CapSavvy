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
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using CapSavvy.Data;

namespace CapSavvy.Modules
{
    public class mod_teksavvy : mod_base
    {
        int oidOffset = 0;

        public mod_teksavvy(UsageData usageData) : base(usageData) { } // Call base constructor

        public override string moduleName
        {
            get { return "TekSavvy"; }
        }

        public override string validUsernameRegex
        {
            get { return @"^([0-9A-F]{32})(|@teksavvy.com)(|\+[0-9]{1,4})$"; }
        }

        public override bool supportsOffPeak
        {
            get { return true; }
        }

        public override bool tracksUploads
        {
            get { return true; } // TODO Defaulting to true to match behaviour before this property was added.  Should confirm what the real value should be
        }

        protected override string GetHTML()
        {
            // We know the match will succeed, since we had to validate the username to get here.
            var match = Regex.Match(_username, validUsernameRegex);

            oidOffset = string.IsNullOrEmpty(match.Groups[3].Value) ? 0 : Convert.ToInt32(match.Groups[3].Value);

            Dictionary<string, string> headerList = new Dictionary<string, string>(1);

            headerList.Add("TekSavvy-APIKey", match.Groups[1].Value);

            return GetHTTPS("https://api.teksavvy.com/web/Usage/UsageSummaryRecords?$filter=IsCurrent%20eq%20true", headerList);

//            return @"{
// ""odata.metadata"":""https://<serviceUrl>/Usage/$metadata#UsageSummaryRecords"",""value"":[
// {
//  ""StartDate"":""2014-01-01T00:00:00"",""EndDate"":""2014-01-09T00:00:00"",""OID"":""120000"",""IsCurrent"":true,""OnPeakDownload"":12.56,""OnPeakUpload"":7.98,""OffPeakDownload"":0.1,""OffPeakUpload"":1.04
// },{
//  ""StartDate"":""2014-01-01T00:00:00"",""EndDate"":""2014-01-09T00:00:00"",""OID"":""320000"",""IsCurrent"":true,""OnPeakDownload"":20.56,""OnPeakUpload"":9.98,""OffPeakDownload"":0.1,""OffPeakUpload"":2.07
// },{
//  ""StartDate"":""2014-01-01T00:00:00"",""EndDate"":""2014-01-09T00:00:00"",""OID"":""568000"",""IsCurrent"":true,""OnPeakDownload"":32.56,""OnPeakUpload"":9.98,""OffPeakDownload"":54.1,""OffPeakUpload"":1.07
// },{
//  ""StartDate"":""2014-01-01T00:00:00"",""EndDate"":""2014-01-09T00:00:00"",""OID"":""428000"",""IsCurrent"":true,""OnPeakDownload"":32.56,""OnPeakUpload"":9.98,""OffPeakDownload"":54.1,""OffPeakUpload"":1.07
// }]}";
        }

        protected override bool ParseHTML(string html)
        {
            try
            {
                // oidOffset is kind of a kludge to support the few people with multiple OIDs
                var value = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(html)["value"] as ArrayList;
                var usageRecord = value[oidOffset] as Dictionary<string, object>;

                usage.RealTime = false; // TODO Using this as the default to match previous behaviour.  Should be updated to proper value later

                usage.Peak.Down = Convert.ToDouble(usageRecord["OnPeakDownload"]);
                usage.Peak.Up = Convert.ToDouble(usageRecord["OnPeakUpload"]);
                usage.Peak.Total = usage.Peak.Down + usage.Peak.Up;

                usage.OffPeak.Down = Convert.ToDouble(usageRecord["OffPeakDownload"]);
                usage.OffPeak.Up = Convert.ToDouble(usageRecord["OffPeakUpload"]);
                usage.OffPeak.Total = usage.OffPeak.Down + usage.OffPeak.Up;

                usage.All.Down = usage.Peak.Down + usage.OffPeak.Down;
                usage.All.Up = usage.Peak.Up + usage.OffPeak.Up;
                usage.All.Total = usage.All.Down + usage.All.Up;

                return true;
            }
            catch (Exception e)
            {
                string foo = e.ToString();
                return false;
            }
        }
    }
}
