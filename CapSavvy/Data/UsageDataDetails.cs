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
    public class UsageDataDetails
    {
        public double Up { get; set; }
        public double Down { get; set; }
        public double Total { get; set; }
        public double UpAverage { get { return GetAverage(Up); } }
        public double DownAverage { get { return GetAverage(Down); } }
        public double TotalAverage { get { return GetAverage(Total); } }
        public double UpPredicted { get { return UpAverage * DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); } }
        public double DownPredicted { get { return DownAverage * DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); } }
        public double TotalPredicted { get { return TotalAverage * DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); } }
        public bool RealTime { get; set; }

        public UsageDataDetails()
        {
            Up = Down = Total = 0;
        }

        private double GetAverage(double amount)
        {
            if (amount <= 0) {
                return 0;
            } else if ((DateTime.Now.Day == 1) && (!RealTime)) {
                return 0;
            } else {
                return amount / (DateTime.Now.Day - 1 + (RealTime ? ((double)DateTime.Now.Hour / 24.0) + ((double)DateTime.Now.Minute / 1440.0) : 0));
            }
        }
    } 
}
