using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public const string PrizeFileName = "PrizeModels.csv";
        public const string PersonFileName = "PersonModels.csv";
        public const string TeamFileName = "TeamModels.csv";
        public const string TeamMembersFileName = "TeamMembers.csv";
        public const string TournamentsFileName = "TournamentModels.csv";
        public const string TournamentEntriesFileName = "TournamentEntries.csv";
        public const string TournamentPrizesFileName = "TournamentPrizes.csv";
        public const string MatchupsFileName = "MatchupModels.csv";
        public const string MatchupEntriesFileName = "MatchupEntries.csv";

        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnection(DatabaseType db)
        {
            if (db == DatabaseType.sql)
            {
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }

            else if (db == DatabaseType.txt)
            {
                TextConnector txt = new TextConnector();
                Connection = txt;
            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
