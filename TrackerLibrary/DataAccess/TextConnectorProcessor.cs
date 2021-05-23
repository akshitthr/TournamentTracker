using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextProcessor
{
    public static class TextConnectorProcessor
    {
        private static string FilePath(this string fileName)
        {
            return $"{ GlobalConfig.AppKeyLookup("filePath") }\\{ fileName }";
        }

        private static void CreateNewFile(string filename)
        {
            string filepath = filename.FilePath();

            using (var writer = new StreamWriter(filepath))
            {
                switch (filename)
                {
                    case GlobalConfig.PrizeFileName:
                        writer.Write("Id,PlaceNumber,PlaceName,PrizeAmount,PrizePercentage");
                        break;

                    case GlobalConfig.PersonFileName:
                        writer.Write("Id,FirstName,LastName,Email,PhoneNumber");
                        break;

                    case GlobalConfig.TeamFileName:
                        writer.Write("Id,TeamName");
                        break;

                    case GlobalConfig.TeamMembersFileName:
                        writer.Write("Id,TeamId,PersonId");
                        break;

                    case GlobalConfig.TournamentsFileName:
                        writer.Write("Id,TournamentName,EntryFee");
                        break;

                    case GlobalConfig.TournamentEntriesFileName:
                        writer.Write("Id,TournamentId,TeamId");
                        break;

                    case GlobalConfig.TournamentPrizesFileName:
                        writer.Write("Id,TournamentId,PrizeId");
                        break;

                    case GlobalConfig.MatchupsFileName:
                        writer.Write("Id,TournamentId,WinnerId,MatchupRound");
                        break;

                    case GlobalConfig.MatchupEntriesFileName:
                        writer.Write("Id,MatchupId,ParentMatchupId,TeamCompetingId,Score");
                        break;
                }
            }
        }

        public static void WriteToCSVFile(this object record, string filename, string recordType = "record", bool appendToFile = true)
        {
            string filepath = filename.FilePath();
            
            if (!File.Exists(filepath))
            {
                CreateNewFile(filename);
            }
            using (var writer = new StreamWriter(filepath, append: appendToFile))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    if (!appendToFile)
                    {
                        if (filename == GlobalConfig.MatchupsFileName)
                        {
                            writer.Write("Id,TournamentId,WinnerId,MatchupRound");
                        }
                        else if (filename == GlobalConfig.MatchupEntriesFileName)
                        {
                            writer.Write("Id,MatchupId,ParentMatchupId,TeamCompetingId,Score");
                        }
                    }
                    if (recordType == "record")
                    {
                        csv.WriteRecord(record);
                    }
                    else
                    {
                        csv.WriteField(record);
                    }
                    writer.WriteLine();
                }
            }
        }

        public static List<PrizeModel> ReadFromPrizeFile()
        {
            string filepath = GlobalConfig.PrizeFileName.FilePath();
            
            if (File.Exists(filepath))
            {
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<PrizeModel>().ToList();
                }
            }
            else
            {
                return new List<PrizeModel>();
            }
        }

        public static List<PersonModel> ReadFromPersonFile()
        {
            string filepath = GlobalConfig.PersonFileName.FilePath();
            
            if (File.Exists(filepath))
            {
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<PersonModel>().ToList();
                }
            }
            else
            {
                return new List<PersonModel>();
            }
        }

        public static List<TeamModel> ReadFromTeamFile()
        {
            // Get Filepath
            string filepath = GlobalConfig.TeamFileName.FilePath();

            // Return records if file exists
            if (File.Exists(filepath))
            {
                // Initialize Team list
                List<TeamModel> teams = new List<TeamModel>();

                // Open file
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // Read data
                    csv.Read();
                    csv.ReadHeader();

                    // Read till end of data
                    while (csv.Read())
                    {
                        // Create new TeamModel with Name and Id and add it to records
                        teams.Add(new TeamModel(csv.GetField<int>("Id"), csv.GetField("TeamName")));
                    }
                }

                // Read from Person File
                List<PersonModel> people = ReadFromPersonFile();

                // Read from Team Members File
                List<int[]> teamMembers = ReadFromTeamMemberFile();

                // Iterate through each Team Member Id
                foreach (int[] member in teamMembers)
                {
                    // Find team by Id
                    TeamModel team = teams.Find(x => x.Id == member[1]);

                    // Find person by Id
                    PersonModel person = people.Find(x => x.Id == member[2]);

                    // Add the person to team
                    team.TeamMembers.Add(person);
                }

                // Return Team List
                return teams;
            }
            // return empty list if file does not exist
            else
            {
                return new List<TeamModel>();
            }
        }

        public static List<TournamentModel> ReadFromTournamentFile()
        {
            string filepath = GlobalConfig.TournamentsFileName.FilePath();

            if (File.Exists(filepath))
            {
                List<TournamentModel> tournaments = new List<TournamentModel>();

                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        tournaments.Add(new TournamentModel(csv.GetField<int>("Id"), csv.GetField<string>("TournamentName"), csv.GetField<Decimal>("EntryFee")));
                    }
                }

                List<int[]> tournamentEntries = ReadFromTournamentEntriesFile();
                List<TeamModel> teams = ReadFromTeamFile();
                foreach (int[] record in tournamentEntries)
                {
                    TournamentModel tournament = tournaments.Find(x => x.Id == record[1]);
                    TeamModel team = teams.Find(x => x.Id == record[2]);
                    tournament.EnteredTeams.Add(team);
                }

                List<int[]> tournamentPrizes = ReadFromTournamentPrizesFile();
                List<PrizeModel> prizes = ReadFromPrizeFile();
                foreach (int[] record in tournamentPrizes)
                {
                    TournamentModel tournament = tournaments.Find(x => x.Id == record[1]);
                    PrizeModel prize = prizes.Find(x => x.Id == record[2]);
                    tournament.Prizes.Add(prize);
                }

                foreach (TournamentModel tournament in tournaments)
                {
                    tournament.AddRoundsToTournamentModel();
                }

                return tournaments;
            }
            else
            {
                return new List<TournamentModel>();
            }
        }

        public static List<int[]> ReadFromTeamMemberFile()
        {
            // Get File Path
            string filepath = GlobalConfig.TeamMembersFileName.FilePath();
            
            // Execute if file exists
            if (File.Exists(filepath))
            {
                List<int[]> records = new List<int[]>();

                // Open file
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    // Read from file
                    while (csv.Read())
                    {
                        int[] record = new int[3];

                        // Fill record array with values
                        record[0] = csv.GetField<int>("Id");
                        record[1] = csv.GetField<int>("TeamId");
                        record[2] = csv.GetField<int>("PersonId");

                        // Add record to list
                        records.Add(record); 
                    }
                }

                // Return list of records
                return records;
            }
            // Return empty list if file doesn't exist
            else
            {
                return new List<int[]>();
            }
        }

        public static List<int[]> ReadFromTournamentEntriesFile()
        {
            string filepath = GlobalConfig.TournamentEntriesFileName.FilePath();

            if (File.Exists(filepath))
            {
                List<int[]> records = new List<int[]>();

                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    
                    while (csv.Read())
                    {
                        int[] record = new int[3];
                        record[0] = csv.GetField<int>("Id");
                        record[1] = csv.GetField<int>("TournamentId");
                        record[2] = csv.GetField<int>("TeamId");
                        records.Add(record);
                    }
                }

                return records;
            }
            else
            {
                return new List<int[]>();
            }
        }

        public static List<int[]> ReadFromTournamentPrizesFile()
        {
            string filepath = GlobalConfig.TournamentPrizesFileName.FilePath();

            if (File.Exists(filepath))
            {
                List<int[]> records = new List<int[]>();

                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        int[] record = new int[3];
                        record[0] = csv.GetField<int>("Id");
                        record[1] = csv.GetField<int>("TournamentId");
                        record[2] = csv.GetField<int>("PrizeId");
                        records.Add(record);
                    }
                }

                return records;
            }
            else
            {
                return new List<int[]>();
            }
        }

        public static List<int[]> ReadFromMatchupsFile()
        {
            // "Id,TournamentId,WinnerId,MatchupRound"

            string filepath = GlobalConfig.MatchupsFileName.FilePath();

            if (File.Exists(filepath))
            {
                List<int[]> records = new List<int[]>();
                
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    
                    while (csv.Read())
                    {
                        int[] record = new int[4];
                        record[0] = csv.GetField<int>("Id");
                        record[1] = csv.GetField<int>("TournamentId");
                        record[2] = 0;
                        try
                        {
                            record[2] = csv.GetField<int>("WinnerId");
                        }
                        catch (CsvHelper.TypeConversion.TypeConverterException) { }
                        record[3] = csv.GetField<int>("MatchupRound");

                        records.Add(record);
                    }
                }

                return records;
            }
            else
            {
                return new List<int[]>();
            }
        }

        public static List<int[]> ReadFromMatchupEntriesFile()
        {
            // "Id,MatchupId,ParentMatchupId,TeamCompetingId,Score"

            string filepath = GlobalConfig.MatchupEntriesFileName.FilePath();

            if (File.Exists(filepath))
            {
                List<int[]> records = new List<int[]>();
                
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        int[] record = new int[5];
                        record[0] = csv.GetField<int>("Id");
                        record[1] = csv.GetField<int>("MatchupId");
                        try
                        {
                            record[2] = csv.GetField<int>("ParentMatchupId");
                        }
                        catch (CsvHelper.TypeConversion.TypeConverterException){ }

                        try
                        {
                            record[3] = csv.GetField<int>("TeamCompetingId");
                        }
                        catch (CsvHelper.TypeConversion.TypeConverterException){ }

                        try
                        {
                            record[4] = csv.GetField<int>("Score");
                        }
                        catch (CsvHelper.TypeConversion.TypeConverterException){ }

                        records.Add(record);
                    }
                }

                return records;
            }
            else
            {
                return new List<int[]>();
            }
        }

        private static void AddRoundsToTournamentModel(this TournamentModel model)
        {
            List<int[]> matchupRawData = ReadFromMatchupsFile().FindAll(x => x[1] == model.Id).ToList();
            List<int[]> matchupEntriesRawData = ReadFromMatchupEntriesFile();

            List<MatchupModel> matchupModels = new List<MatchupModel>();
            MatchupModel matchup;
            foreach (int[] dataArray in matchupRawData)
            {
                matchup = new MatchupModel();
                matchup.Id = dataArray[0];
                if (dataArray[2] != 0)
                {
                    matchup.Winner = model.EnteredTeams.Find(x => x.Id == dataArray[2]);
                }
                matchup.MatchupRound = dataArray[3];

                matchupModels.Add(matchup);
            }

            MatchupEntryModel matchupEntry;
            foreach (int[] dataArray in matchupEntriesRawData)
            {
                if (matchupModels.Find(x => x.Id == dataArray[1]) != null)
                {
                    matchup = matchupModels.Find(x => x.Id == dataArray[1]);
                    matchupEntry = new MatchupEntryModel
                    {
                        Id = dataArray[0]
                    };
                    if (matchupModels.Find(x => x.Id == dataArray[2]) != null)
                    {
                        matchupEntry.ParentMatchup = matchupModels.Find(x => x.Id == dataArray[2]);
                    }
                    if (model.EnteredTeams.Find(x => x.Id == dataArray[3]) != null)
                    {
                        matchupEntry.TeamCompeting = model.EnteredTeams.Find(x => x.Id == dataArray[3]);
                    }
                    if (dataArray[4] != 0)
                    {
                        matchupEntry.Score = dataArray[4];
                    }

                    matchup.Entries.Add(matchupEntry);
                }
            }

            matchupModels.OrderBy(x => x.MatchupRound);

            int roundCount = matchupModels[0].MatchupRound;
            List<MatchupModel> currentRound = new List<MatchupModel>();
            foreach (MatchupModel matchupModel in matchupModels)
            {
                if (roundCount != matchupModel.MatchupRound)
                {
                    model.Rounds.Add(currentRound);
                    currentRound = new List<MatchupModel>();
                    roundCount = matchupModel.MatchupRound;
                }
                currentRound.Add(matchupModel);
            }
            model.Rounds.Add(currentRound);
        }
    }
}
