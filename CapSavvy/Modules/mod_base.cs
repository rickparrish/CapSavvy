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
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;
using System.IO;
using CapSavvy.Data;
using System.Collections.Generic;

namespace CapSavvy.Modules
{
    public abstract class mod_base
    {
        public mod_base(UsageData usageData)
        {
            usage = usageData;
        }

        #region Variables

        public abstract string moduleName { get; }
        public abstract string validUsernameRegex { get; }
        public abstract bool supportsOffPeak { get; }
        public abstract bool tracksUploads { get; }

        public UsageData usage = new UsageData();
        public int interval = 1800; // Half hour default

        protected string _username = string.Empty;
        protected ErrorState _errorState = ErrorState.Startup;

        #endregion Variables

        #region Accessors

        public string Username
        {
            set
            {
                _username = value;
                if (String.Empty == _username)
                    _errorState = ErrorState.Username;
            }
        }

        public ErrorState Error
        {
            get { return _errorState; }
        }

        #endregion Accessors

        public enum ErrorState
        {
            Startup,
            OK,
            Match,
            HTTP,
            Username,
        }

        public void GetUsage()
        {
            // Override current culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-ca");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-ca");

            string html = GetHTML();

            if (string.IsNullOrEmpty(html))
            {
                _errorState = ErrorState.HTTP;
                return;
            }

            _errorState = ParseHTML(html) ? ErrorState.OK : ErrorState.Match;
        }

        protected abstract string GetHTML();
        protected abstract bool ParseHTML(string html);

        protected string GetHTTPS(string url, Dictionary<string, string> headerList = null)
        {
            try
            {
                WebRequest https = HttpWebRequest.Create(new Uri(url));

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                if (headerList != null)
                {
                    foreach (KeyValuePair<string, string> header in headerList)
                    {
                        https.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)https.GetResponse();
                Stream stream = response.GetResponseStream();

                byte[] receiveBuffer = new byte[1024];
                StringBuilder htmlBuilder = new StringBuilder(16384);

                int byteCount = 0;
                do
                {
                    byteCount = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    htmlBuilder.Append(Encoding.ASCII.GetString(receiveBuffer, 0, byteCount));
                } while (byteCount > 0);

                stream.Close();
                response.Close();

                return htmlBuilder.ToString();
            }
            catch
            {
                return null;
            }
        }

        protected string GetHTTP(string host, int port, string url, string postData = null)
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                socket.Connect(host, port);

                if (!socket.Connected)
                    return null;

                string message;

                if (!string.IsNullOrEmpty(postData))
                {
                    message =
                        "POST " + url + " HTTP/1.0\r\n" +
                        "Host: " + host + "\r\n" +
                        "User-Agent: CapSavvy\r\n" +
                        "Content-Length: " + postData.Length + "\r\n" +
                        "Content-Type: application/x-www-form-urlencoded\r\n" +
                        "\r\n" +
                        postData;
                }
                else
                {
                    message =
                        "GET " + url + " HTTP/1.0\r\n" +
                        "Host: " + host + "\r\n" +
                        "User-Agent: CapSavvy\r\n" +
                        "\r\n";
                }

                socket.Send(System.Text.Encoding.ASCII.GetBytes(message));

                byte[] receiveBuffer = new byte[1024];
                StringBuilder htmlBuilder = new StringBuilder(16384);

                int byteCount = 0;
                do
                {
                    byteCount = socket.Receive(receiveBuffer, receiveBuffer.Length, 0);
                    htmlBuilder.Append(Encoding.ASCII.GetString(receiveBuffer, 0, byteCount));
                } while (byteCount > 0);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                return htmlBuilder.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
