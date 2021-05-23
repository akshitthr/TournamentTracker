using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;
using TrackerLibrary.DataAccess.TextProcessor;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        public void CreatePerson(PersonModel model)
        {
            List<PersonModel> records = TextConnectorProcessor.ReadFromPersonFile();

            model.Id = records.Any() ? records.LastOrDefault().Id + 1 : 1;
            model.WriteToCSVFile(GlobalConfig.PersonFileName);
        }

        public void CreatePrize(PrizeModel model)
        {
            List<PrizeModel> records = TextConnectorProcessor.ReadFromPrizeFile();

            model.Id = records.Any() ? records.LastOrDefault().Id + 1 : 1;
            model.WriteToCSVFile(GlobalConfig.PrizeFileName);
        }

        public void CreateTeam(TeamModel model)
        {
            // Read Team File Data and assign Id
            List<TeamModel> teamRecords = TextConnectorProcessor.ReadFromTeamFile();
            model.Id = (teamRecords.Any()) ? teamRecords.LastOrDefault().Id + 1 : 1;

            // Read Team Members File Data and get max Id
            List<int[]> teamMemberRecords = TextConnectorProcessor.ReadFromTeamMemberFile();
            int teamMemberId = teamMemberRecords.Any() ? teamMemberRecords.LastOrDefault()[0] + 1 : 1;

            // Write New Team to File
            string[] team = { model.Id.ToString(), model.TeamName };
            team.WriteToCSVFile(GlobalConfig.TeamFileName, "field");

            // Iterate through Team Members in Team model
            foreach (PersonModel person in model.TeamMembers)
            {
                // Assign Ids to teamMember array
                int[] teamMember = new int[3];
                teamMember[0] = teamMemberId;
                teamMember[1] = model.Id;
                teamMember[2] = person.Id;

                // Write Ids to File
                teamMember.WriteToCSVFile(GlobalConfig.TeamMembersFileName, "field");

                //Increment Id by 1
                teamMemberId++;
            }
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TextConnectorProcessor.ReadFromTournamentFile();
            model.Id = tournaments.Any() ? tournaments.LastOrDefault().Id + 1 : 1;
            // Change below
            int matchupModelsId = TextConnectorProcessor.ReadFromMatchupsFile().Any() ? TextConnectorProcessor.ReadFromMatchupsFile().LastOrDefault()[0] + 1 : 1;
            int matchupEntriesId = TextConnectorProcessor.ReadFromMatchupEntriesFile().Any() ? TextConnectorProcessor.ReadFromMatchupEntriesFile().LastOrDefault()[0] + 1 : 1;

            List<int[]> tournamentEntries = TextConnectorProcessor.ReadFromTournamentEntriesFile();
            int tournamentEntriesId = tournamentEntries.Any() ? tournamentEntries.LastOrDefault()[0] + 1 : 1;

            List<int[]> tournamentPrizes = TextConnectorProcessor.ReadFromTournamentPrizesFile();
            int tournamentPrizesId = tournamentPrizes.Any() ? tournamentPrizes.LastOrDefault()[0] + 1 : 1;

            string[] tournament = { model.Id.ToString(), model.TournamentName, model.EntryFee.ToString() };
            tournament.WriteToCSVFile(GlobalConfig.TournamentsFileName, "field");

            foreach (TeamModel team in model.EnteredTeams)
            {
                int[] records = new int[3];
                records[0] = tournamentEntriesId;
                records[1] = model.Id;
                records[2] = team.Id;

                records.WriteToCSVFile(GlobalConfig.TournamentEntriesFileName, "field");

                tournamentEntriesId++;
            }

            foreach (PrizeModel prize in model.Prizes)
            {
                int[] records = new int[3];
                records[0] = tournamentPrizesId;
                records[1] = model.Id;
                records[2] = prize.Id;

                records.WriteToCSVFile(GlobalConfig.TournamentPrizesFileName, "field");

                tournamentPrizesId++;
            }

            foreach (List<MatchupModel> round in model.Rounds)
            {
                string[] matchupRecords = new string[4];
                foreach (MatchupModel matchup in round)
                {
                    matchup.Id = matchupModelsId;

                    matchupRecords[0] = matchup.Id.ToString();
                    matchupRecords[1] = model.Id.ToString();
                    matchupRecords[2] = "";
                    matchupRecords[3] = matchup.MatchupRound.ToString();

                    matchupRecords.WriteToCSVFile(GlobalConfig.MatchupsFileName, "field");

                    foreach (MatchupEntryModel matchupEntry in matchup.Entries)
                    {
                        matchupEntry.Id = matchupEntriesId;

                        string[] matchupEntryRecords = new string[5];
                        matchupEntryRecords[0] = matchupEntry.Id.ToString();
                        matchupEntryRecords[1] = matchup.Id.ToString();

                        if (matchupEntry.ParentMatchup is null)
                        {
                            matchupEntryRecords[2] = "";
                        }
                        else
                        {
                            matchupEntryRecords[2] = matchupEntry.ParentMatchup.Id.ToString();
                        }

                        if (matchupEntry.TeamCompeting is null)
                        {
                            matchupEntryRecords[3] = "";
                        }
                        else
                        {
                            matchupEntryRecords[3] = matchupEntry.TeamCompeting.Id.ToString();
                        }

                        matchupEntryRecords[4] = "";

                        matchupEntryRecords.WriteToCSVFile(GlobalConfig.MatchupEntriesFileName, "field");

                        matchupEntriesId++;
                    }

                    matchupModelsId++;
                }
            }
            TournamentLogic.AlertTeamToNewRound(model);
            TournamentLogic.UpdateRounds(model);
        }

        public List<PersonModel> GetAllPerson()
        {
            return TextConnectorProcessor.ReadFromPersonFile();
        }

        public List<TeamModel> GetAllTeams()
        {
            return TextConnectorProcessor.ReadFromTeamFile();
        }

        public List<TournamentModel> GetAllTournaments()
        {
            return TextConnectorProcessor.ReadFromTournamentFile();
        }

        public void UpdateMatchup(MatchupModel model)
        {
            if (model.Winner != null)
            {
                List<int[]> matchups = TextConnectorProcessor.ReadFromMatchupsFile();
                matchups.Find(x => x[0] == model.Id)[2] = model.Winner.Id;

                for (int i = 0; i < matchups.Count; i++)
                {
                    int[] matchup = matchups[i];
                    matchup.WriteToCSVFile(GlobalConfig.MatchupsFileName, "field", i != 0);
                }
            }

            List<int[]> matchupEntries = TextConnectorProcessor.ReadFromMatchupEntriesFile();
            foreach (MatchupEntryModel matchupEntryModel in model.Entries)
            {
                if (matchupEntryModel.TeamCompeting != null)
                {
                    matchupEntries.Find(x => x[0] == matchupEntryModel.Id)[3] = matchupEntryModel.TeamCompeting.Id;
                    matchupEntries.Find(x => x[0] == matchupEntryModel.Id)[4] = matchupEntryModel.Score;
                }
            }

            for (int i = 0; i < matchupEntries.Count; i++)
            {
                int[] matchupEntry = matchupEntries[i];
                matchupEntry.WriteToCSVFile(GlobalConfig.MatchupEntriesFileName, "field", i != 0);
            }
        }
    }
}
