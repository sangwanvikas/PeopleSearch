using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.Models
{
    public class ConnectionString
    {
        // "connectionString=Data Source=LENOVO-PC\SQLEXPRESS;Database=person;Integrated Security=True"
        public string ServerNameValue { get; set; }
        public string SqlServerInstanveValue { get; set; }
        public string DatabaseValue { get; set; }
        public bool IntegratedSecurityValue { get; set; }


        public ConnectionString()
        {
            ServerNameValue = default(string);
            SqlServerInstanveValue = default(string);
            DatabaseValue = default(string);
            IntegratedSecurityValue = default(bool);
        }

        public override string ToString()
        {
            string connectionStringValue = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                Constants.ConnectionString, Constants.Equal,
                Constants.DataSource, Constants.Equal, ServerNameValue, Constants.ForwardSlash, SqlServerInstanveValue, Constants.SemiColon,
                Constants.Database, Constants.Equal, DatabaseValue, Constants.SemiColon,
                Constants.IntegratedSecurity, Constants.Equal, IntegratedSecurityValue.ToString());

            return connectionStringValue;
        }
    }
}