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
    public class mod_start_fwireless : mod_start_fdsl
    {
        public mod_start_fwireless(UsageData usageData) : base(usageData) { } // Call base constructor

        public override string moduleName
        {
            get { return "Start Wireless"; }
        }

        public override string validUsernameRegex
        {
            get { return @"^[A-Z0-9]{7}[A-F0-9]{11}W@(start\.ca)$"; }
        }
    }
}
