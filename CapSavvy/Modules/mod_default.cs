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

using CapSavvy.Data;

namespace CapSavvy.Modules
{
    public class mod_default : mod_base
    {
        public mod_default(UsageData usageData) : base(usageData) { }

        public override string moduleName
        {
            get { return "Invalid Username"; }
        }

        public override string validUsernameRegex
        {
            get { return string.Empty; }
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
            return string.Empty;
        }

        protected override bool ParseHTML(string html)
        {
            usage.RealTime = false; // TODO Using this as the default to match previous behaviour.  Should be updated to proper value later

            usage.All.Down = 0;
            usage.All.Up = 0;
            usage.All.Total = 0;

            usage.OffPeak.Down = 0;
            usage.OffPeak.Up = 0;
            usage.OffPeak.Total = 0;

            usage.Peak.Down = 0;
            usage.Peak.Up = 0;
            usage.Peak.Total = 0;

            return true;
        }
    }
}
