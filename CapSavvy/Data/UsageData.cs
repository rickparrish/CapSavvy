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

namespace CapSavvy.Data
{
    class UsageData
    {
        public UsageDataDetails Peak { get; set; }
        public UsageDataDetails OffPeak { get; set; }
        public UsageDataDetails All { get; set; }

        public UsageData()
        {
            Peak = new UsageDataDetails();
            OffPeak = new UsageDataDetails();
            All = new UsageDataDetails();
        }
    }
}
