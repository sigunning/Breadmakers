using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;
using System.Data.SQLite;
using Breadmakers.Models;
using System.Diagnostics;
using System.Web;


// TODO: Return success/failure codes from each method

namespace Breadmakers
{
    public class DataAccess
    {
        private string _connectionString;
        //private bool _isSqlite = false;
        private string _startDate = "StartDate";
        private string _roundDate = "RoundDate";
        private string vPlayerScore = string.Concat(
        " PS.*",
        ", S.SocietyId",
        ", S.SocietyName",
        ", C.CompetitionId",
        ", C.CompetitionName",
        ", C.CompetitionDescription",
        ", c.CodeName",
        ", C.Venue",
        //", C.StartDate", 
        ", C.Accommodation",
        ", C.Winner",
        ", R.CourseId",
        ", G.CourseName",
        ", R.RoundId",
        //", R.RoundDate",
        ", PS.PlayerId",
        ", P.PlayerName",
        " From Competition C",
        " Inner join Society S on S.SocietyId = C.SocietyId",
        " Inner Join Round R on R.CompetitionId = C.CompetitionId",
        " Inner join Course G on G.CourseId = R.CourseId",
        " Left Outer join PlayerScore PS on PS.RoundId = R.RoundId",
        " Left Outer join Player P on P.PlayerId = PS.PlayerId");


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
                if (_connectionString.Contains(".db"))
                {
                    //if uri, then convert to local path for Azure
                    if (_connectionString.Contains("/"))
                        _connectionString = string.Concat("Data Source=", HttpContext.Current.Server.MapPath(_connectionString));
                    
                    db = new SQLiteConnection(_connectionString);
                    // different date functions for SQLite for the date fields
                    _startDate = "Date(StartDate,'unixepoch') As StartDate ";
                    _roundDate = "Date(RoundDate,'unixepoch') As RoundDate ";
                }
                else
                    { db= new SqlConnection(_connectionString);   }

                return (db);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
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

        public  Course GetCourse(string courseId)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    //var output =  db.Query<Order>(@"Select * From DrinkOrder;").ToList();
                    string sql = string.Concat("Select *   ",
                        "From Course Where CourseId=@courseId ; ");

                    var p = new DynamicParameters();
                    p.Add("CourseId", courseId);

                    var output = db.Query<Course>(sql, p,
                        commandType: CommandType.Text);

                    return output.FirstOrDefault<Course>();

                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
                return null;
            }

        }


        // ---------------------------------------------------------------------
        // Get BreadBoard comps
        // ---------------------------------------------------------------------
        public List<BreadBoard> GetListBreadBoard(string societyId)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    string sql =string.Concat( "Select Distinct CompetitionId,CodeName, ",
                        "CompetitionName,CompetitionDescription,Venue,"+ _startDate+ ", IFNULL(Winner,'NA') As Winner, Accommodation  ",
                        "From Competition where CodeName  Like 'BRD%' ",
                        " and SocietyId=@societyid Order By StartDate Desc ; ");
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@SocietyId", societyId, DbType.String, ParameterDirection.Input);
                    var output = db.Query<BreadBoard>(sql, parameter,
                                commandType: CommandType.Text).ToList();


                    return (output);
                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
                return null;
            }

        }

        public BreadBoard GetBreadboard(string competitionId)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    //var output =  db.Query<Order>(@"Select * From DrinkOrder;").ToList();
                    string sql = string.Concat("Select Distinct CompetitionId,CodeName, ",
                       "CompetitionName,CompetitionDescription,Venue," + _startDate + ", Winner, Accommodation  ",
                       "From Competition where CompetitionId = @CompetitionId " );

                    var p = new DynamicParameters();
                    p.Add("CompetitionId", competitionId);

                    var output = db.Query<BreadBoard>(sql, p,
                        commandType: CommandType.Text);

                    return output.FirstOrDefault<BreadBoard>();

                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
                return null;
            }

        }

        

        // ---------------------------------------------------------------------
        // Get BATWAII comps
        // ---------------------------------------------------------------------
        public List<Batwaiii> GetListBatwaiii(string societyId)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    string sql = string.Concat("Select Distinct CompetitionId,CodeName, ",
                        "CompetitionName,CompetitionDescription,Venue," + _startDate + ", Winner, Accommodation  ",
                        "From Competition where CodeName Like 'BAT%' ",
                        " and SocietyId=@societyid Order By Startdate Desc ; ");
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@SocietyId", societyId, DbType.String, ParameterDirection.Input);
                    var output = db.Query<Batwaiii>(sql, parameter,
                                commandType: CommandType.Text).ToList();


                    return (output);
                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
                return null;
            }

        }

        public Batwaiii GetBatwaiii(string competitionId)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    //var output =  db.Query<Order>(@"Select * From DrinkOrder;").ToList();
                    string sql = string.Concat("Select Distinct CompetitionId,CodeName, ",
                       "CompetitionName,CompetitionDescription,Venue," + _startDate + ", Winner, Accommodation  ",
                       "From Competition where CompetitionId = @CompetitionId ; ");

                    var p = new DynamicParameters();
                    p.Add("CompetitionId", competitionId);

                    var output = db.Query<Batwaiii>(sql, p,
                        commandType: CommandType.Text);

                    return output.FirstOrDefault<Batwaiii>();

                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
                return null;
            }

        }

        // ---------------------------------------------------------------------
        // Get player scores for competition
        // ---------------------------------------------------------------------
        public List<PlayerRound> GetPlayerScores(string competitionId)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    string sql = string.Concat("Select ", _startDate, ",", _roundDate, ",", vPlayerScore, " where C.CompetitionId=@competitionId; ");
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@competitionId", competitionId, DbType.String, ParameterDirection.Input);
                    var output =  db.Query<PlayerRound>(sql, parameter,
                                commandType: CommandType.Text).ToList();
                    

                    return (output);
                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
                return null;
            }

        }

        // ---------------------------------------------------------------------
        // Get rounds for batwaiii
        // ---------------------------------------------------------------------
        public List<Round> GetRounds(string competitionId)
        {
            try
            {
                // enclose within a using statement so that connection is automatically closed on exit
                using (IDbConnection db = GetConnection())
                {
                    string sql =string.Concat("Select ",_startDate,",",_roundDate,",", vPlayerScore , " where C.competitionId=@competitionId; ");
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@competitionId", competitionId, DbType.String, ParameterDirection.Input);
                    var output = db.Query<Round>(sql, parameter,
                                commandType: CommandType.Text).ToList();


                    return (output);
                }
            }
            catch (Exception ex)
            {
                //Logging.WriteLog(string.Concat(MethodBase.GetCurrentMethod(), ": ", ex.Message));
                return null;
            }

        }


    }
}
