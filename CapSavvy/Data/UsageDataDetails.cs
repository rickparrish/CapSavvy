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

namespace CapSavvy.Data
{
    class UsageDataDetails
    {
        public double Up { get; set; }
        public double Down { get; set; }
        public double Total { get; set; }
        public double UpPredicted { get { return Up == 0 || DateTime.Now.Day == 1 ? 0 : Up / (Convert.ToDouble(DateTime.Now.Day - 1) / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)); } }
        public double DownPredicted { get { return Down == 0 || DateTime.Now.Day == 1 ? 0 : Down / (Convert.ToDouble(DateTime.Now.Day - 1) / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)); } }
        public double TotalPredicted { get { return Total == 0 || DateTime.Now.Day == 1 ? 0 : Total / (Convert.ToDouble(DateTime.Now.Day - 1) / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)); } }

        public UsageDataDetails()
        {
            Up = Down = Total = 0;
        }
    } 
}
