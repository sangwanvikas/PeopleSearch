using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PeopleSearch.Helper
{
    public class ConnectionStringHelper
    {

        public static string GetServerName()
        {
            string myServer = Environment.MachineName;
            return myServer;

        }

        public static List<string> GetSqlInstanceNames()
        {
            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                RegistryKey key = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL");
                foreach (string s in key.GetValueNames())
                {

                }
                var resu = key.GetValueNames().ToList();
                resu.Add("game");
                resu.Add("game");
                return resu;
            }
        }
    }
}