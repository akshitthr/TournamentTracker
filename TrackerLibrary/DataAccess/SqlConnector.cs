using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string dbName = "Tournaments";

        public void CreatePerson(PersonModel model)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@Email", model.Email);
                p.Add("@Phone", model.PhoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }
        }

        public void CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }
        }

        public void CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                foreach (var person in model.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", model.Id);
                    p.Add("@PersonId", person.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                // Save Tournament
                var p = new DynamicParameters();
                p.Add("@TournamentName", model.TournamentName);
                p.Add("@EntryFee", model.EntryFee);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournaments_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                // Save Teams
                foreach (TeamModel team in model.EnteredTeams)
                {
                    p = new DynamicParameters();
                    p.Add("@TournamentId", model.Id);
                    p.Add("@TeamId", team.Id);

                    connection.Execute("dbo.spTournamentEntries_Insert", p, commandType: CommandType.StoredProcedure);
                }

                // Save Prizes
                foreach (PrizeModel prize in model.Prizes)
                {
                    p = new DynamicParameters();
                    p.Add("@TournamentId", model.Id);
                    p.Add("@PrizeId", prize.Id);

                    connection.Execute("dbo.spTournamentPrizes_Insert", p, commandType: CommandType.StoredProcedure);
                }

                // Save Rounds
                foreach (List<MatchupModel> round in model.Rounds)
                {
                    foreach (MatchupModel matchup in round)
                    {
                        p = new DynamicParameters();
                        p.Add("@TournamentId", model.Id);
                        p.Add("@MatchupRound", matchup.MatchupRound);
                        p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);

                        matchup.Id = p.Get<int>("@id");

                        foreach (MatchupEntryModel matchupEntry in matchup.Entries)
                        {
                            p = new DynamicParameters();
                            p.Add("@MatchupId", matchup.Id);

                            if (matchupEntry.ParentMatchup is null)
                            {
                                p.Add("@ParentMatchupId", null);
                            }
                            else
                            {
                                p.Add("@ParentMatchupId", matchupEntry.ParentMatchup.Id);
                            }

                            if (matchupEntry.TeamCompeting is null)
                            {
                                p.Add("@TeamCompetingId", null);
                            }
                            else
                            {
                                p.Add("@TeamCompetingId", matchupEntry.TeamCompeting.Id);
                            }
                            
                            p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                            connection.Execute("dbo.spMatchupEntries_Insert", p, commandType: CommandType.StoredProcedure);

                            matchupEntry.Id = p.Get<int>("@id");
                        }
                    }
                }
            }
            TournamentLogic.AlertTeamToNewRound(model);
            TournamentLogic.UpdateRounds(model);
        }

        public List<PersonModel> GetAllPerson()
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                return connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }
        }

        public List<TeamModel> GetAllTeams()
        {
            List<TeamModel> records = new List<TeamModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                records = connection.Query<TeamModel>("dbo.spTeams_GetAll").ToList();
                
                var p = new DynamicParameters();
                foreach (TeamModel team in records)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", team.Id);

                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetBy", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            return records;
        }

        public List<TournamentModel> GetAllTournaments()
        {
            List<TournamentModel> records = new List<TournamentModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                // Get Teams
                List<TeamModel> teams = GetAllTeams();

                // Populate Tournaments
                records = connection.Query<TournamentModel>("dbo.spTournaments_GetAll").ToList();

                var p = new DynamicParameters();
                foreach (TournamentModel tournament in records)
                {
                    p = new DynamicParameters();
                    p.Add("@TournamentId", tournament.Id);

                    // Populate Tournament Prizes
                    tournament.Prizes = connection.Query<PrizeModel>("dbo.spPrizes_GetByTournamentId", p, commandType: CommandType.StoredProcedure).ToList();
                    
                    // Populate Teams
                    tournament.EnteredTeams = connection.Query<TeamModel>("dbo.spTeams_GetByTournamentId", p, commandType: CommandType.StoredProcedure).ToList();
                    
                    // Populate Matchups
                    List<MatchupModel> matchups = connection.Query<MatchupModel>("dbo.spMatchups_GetByTournamentId", p, commandType: CommandType.StoredProcedure).ToList();

                    // Populate Team Members
                    foreach (TeamModel team in tournament.EnteredTeams)
                    {
                        p = new DynamicParameters();
                        p.Add("@TeamId", team.Id);
                        
                        team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetBy", p, commandType: CommandType.StoredProcedure).ToList();
                    }

                    // Populate Matchup Entries and add Matchups to Tournament Rounds
                    int roundCount = matchups[0].MatchupRound;
                    List<MatchupModel> currentRound = new List<MatchupModel>();
                    foreach (MatchupModel matchup in matchups)
                    {
                        // Populate Winner in Matchup
                        if (matchup.WinnerId > 0)
                        {
                            matchup.Winner = teams.Find(x => x.Id == matchup.WinnerId);
                        }

                        // Populate Matchup Entries
                        p = new DynamicParameters();
                        p.Add("@MatchupId", matchup.Id);

                        matchup.Entries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchupId", p, commandType: CommandType.StoredProcedure).ToList();

                        // Populate Team Competing and Parent Matchup in Matchup Entry
                        foreach (MatchupEntryModel matchupEntry in matchup.Entries)
                        {
                            if (matchupEntry.TeamCompetingId > 0)
                            {
                                matchupEntry.TeamCompeting = teams.Find(x => x.Id == matchupEntry.TeamCompetingId);
                            }
                            if (matchupEntry.ParentMatchupId > 0)
                            {
                                matchupEntry.ParentMatchup = matchups.Find(x => x.Id == matchupEntry.ParentMatchupId);
                            }
                        }

                        if (matchup.MatchupRound != roundCount)
                        {
                            tournament.Rounds.Add(currentRound);
                            currentRound = new List<MatchupModel>();
                            roundCount++;
                        }
                        currentRound.Add(matchup);
                    }
                    tournament.Rounds.Add(currentRound);
                }
            }

            return records;
        }

        public void UpdateMatchup(MatchupModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new DynamicParameters();

                if (model.Winner != null)
                {
                    p.Add("@id", model.Id);
                    p.Add("@WinnerId", model.Winner.Id);

                    connection.Execute("dbo.spMatchups_Update", p, commandType: CommandType.StoredProcedure);
                }

                foreach (MatchupEntryModel matchupEntry in model.Entries)
                {
                    if (matchupEntry.TeamCompeting != null)
                    {
                        p = new DynamicParameters();
                        p.Add("@id", matchupEntry.Id);
                        p.Add("@TeamCompetingId", matchupEntry.TeamCompeting.Id);
                        p.Add("@Score", matchupEntry.Score);

                        connection.Execute("dbo.spMatchupEntries_Update", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }
    }
}
