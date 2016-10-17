using Microsoft.Win32;
using PeopleSearch.Models;
using PeopleSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace PeopleSearch.Helper
{
    public class ConnectionStringHelper
    {

        public static ConnectionStringViewModel GetDefaultConnectionString()
        {
            ConnectionStringViewModel conStringVM = new ConnectionStringViewModel();
            conStringVM.Name = Constants.ConnectionStringName;
            conStringVM.ServerNameValue = GetServerName();
            conStringVM.SqlServerInstances = GetSqlInstanceNames();
            if (conStringVM.SqlServerInstances.Count > 0)
            {
                conStringVM.SqlServerInstanceValue = conStringVM.SqlServerInstances.First();
            }
            conStringVM.DatabaseValue = Constants.PersonString;
            conStringVM.IntegratedSecurityValue = true;
            conStringVM.ProviderName = Constants.ProviderNameAsSqlClient;

            return conStringVM;
        }

        public static string GetServerName()
        {
            string myServer = Environment.MachineName;
            return myServer;

        }

        public static List<string> GetSqlInstanceNames()
        {
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;

            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
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