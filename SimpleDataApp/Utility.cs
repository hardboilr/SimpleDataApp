using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataApp {
    internal class Utility {

        // get connection from App config file
        internal static string GetConnectionString() {

            string connectionString = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["SimpleDataApp.Properties.Settings.connString"];

            if(settings != null) {
                connectionString = settings.ConnectionString;
            }

            return connectionString;

        }
    }
}
