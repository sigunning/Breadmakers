using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;


// TODO: Return success/failure codes from each method

namespace Breadmakers
{
    public class DataAccess
    {
        private string _connectionString;


        public DataAccess(string configKey)
        // constructor
        {
            // get connection string from config
            _connectionString = ConfigurationManager.ConnectionStrings[configKey].ToString();
        }

        // ---------------------------------------------------------------------
        // Connect to database and return connection object
        // ---------------------------------------------------------------------
        private IDbConnection GetConnection()
        // return a new connection object for either SQLite or std SQL
        {
            IDbConnection db;
            try
            {
                // determine if SQLite or SQL
                //if (_connectionString.Contains(".db"))
                //    {db= new SQLiteConnection(_connectionString); }
                //else
                    { db= new SqlConnection(_connectionString);   }

                return (db);
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        
        // ---------------------------------------------------------------------
        // Log message to db syslog
        // ---------------------------------------------------------------------
        public void SysLog(string logText)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    var affectedRows = db.Execute("spSysLog",
                    new { ProcessName = "DLTransform",  LogText = logText },
                    commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
            }

        }
       


    }
}
