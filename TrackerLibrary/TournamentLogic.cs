using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        public static TournamentModel CreateAllRounds(this TournamentModel model)
        {
            List<TeamModel> randomizedTeams = model.EnteredTeams.RandomizeOrder();
            model.EnteredTeams = randomizedTeams;
            int rounds = NumberOfRounds(model.EnteredTeams.Count());
            int byes = NumberOfByes(model.EnteredTeams.Count, rounds);
            CreateFirstRound(model, byes);
            CreateRounds(model, rounds);

            return model;
        }

        public static void UpdateRounds(TournamentModel model)
        {
            int startingRound = model.GetCurrentRound();
            
            List<MatchupModel> matchupsToScore = new List<MatchupModel>();
            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    foreach (MatchupEntryModel matchupEntry in matchup.Entries)
                    {
                        if (matchup.Winner == null && (matchup.Entries.Count() < 2 || matchupEntry.Score > 0))
                        {
                            matchupsToScore.Add(matchup);
                            break;
                        }
                    }
                }
            }

            ScoreMatchups(matchupsToScore);
            AdvanceWinners(matchupsToScore, model);
            matchupsToScore.ForEach(x => GlobalConfig.Connection.UpdateMatchup(x));

            int endingRound = model.GetCurrentRound();

            if (endingRound > startingRound)
            {
                AlertTeamToNewRound(model);
            }
        }

        public static void AlertTeamToNewRound(TournamentModel model)
        {
            List<MatchupModel> round = model.Rounds[model.GetCurrentRound() - 1];
            
            foreach (MatchupModel matchup in round)
            {
                foreach (MatchupEntryModel matchupEntry in matchup.Entries)
                {
                    foreach (PersonModel person in matchupEntry.TeamCompeting.TeamMembers)
                    {
                        AlertPersonToNewRound(person, matchupEntry.TeamCompeting.TeamName, matchup.Entries.Find(x => x.TeamCompeting != matchupEntry.TeamCompeting), model.GetCurrentRound());
                    }
                }
            }
        }

        private static void CompleteTournament(TournamentModel model)
        {
            if (model.Rounds.LastOrDefault().LastOrDefault().Winner == null)
            {
                return;
            }

            TeamModel winner = model.Rounds.LastOrDefault().LastOrDefault().Winner;
            TeamModel runnerUp = model.Rounds.LastOrDefault().LastOrDefault().Entries.Find(x => x.TeamCompeting != winner).TeamCompeting;

            decimal tournamentIncome = model.EnteredTeams.Count() * model.EntryFee;
            
            decimal winnerPrize = CalculatePlayerPrize(tournamentIncome, model.Prizes.Find(x => x.PlaceNumber == 1));
            decimal runnerUpPrize = CalculatePlayerPrize(tournamentIncome, model.Prizes.Find(x => x.PlaceNumber == 2));

            string subject = $"{ winner.TeamName } has won the { model.TournamentName } tournament!";
            StringBuilder body = new StringBuilder();

            body.AppendLine($"<h3>Congratulations to { winner.TeamName } for winning the tournament!</h3>");
            body.AppendLine($"<h4>{ runnerUp.TeamName } is the Runner-up</h4>");

            if (winnerPrize > 0)
            {
                body.AppendLine($"<h5>The winner will receive { winnerPrize }</h5>");
            }
            if (runnerUpPrize > 0)
            {
                body.AppendLine($"<h5>The Runner-Up will receive { runnerUpPrize }</h5>");
            }

            body.AppendLine("<br>");
            body.AppendLine("<p>Thank you all for a great tournament</p>");
            body.AppendLine("<em>~Tournament Tracker</em>");

            List<string> bcc = new List<string>();

            foreach (TeamModel team in model.EnteredTeams)
            {
                foreach (PersonModel person in team.TeamMembers)
                {
                    bcc.Add(person.Email);
                }
            }
            
            EmailLogic.SendEmail(new List<string>(), bcc, subject, body.ToString());

            model.TournamentComplete();
        }

        private static decimal CalculatePlayerPrize(decimal tournamentIncome, PrizeModel prize)
        {
            if (prize == null)
            {
                return 0;
            }

            if (prize.PrizeAmount > 0)
            {
                return prize.PrizeAmount;
            }
            else
            {
                return Decimal.Multiply(tournamentIncome, prize.PrizePercentage / 100);
            }
        }

        private static void AlertPersonToNewRound(PersonModel person, string teamName, MatchupEntryModel competitor, int roundCount)
        {
            string to = person.Email;
            string subject;
            StringBuilder body = new StringBuilder();

            if (competitor != null)
            {
                subject = $"You have a matchup with { competitor.TeamCompeting.TeamName }";

                body.AppendLine($"<h3>Greetings { person.FirstName }!</h3>");
                body.AppendLine($"<h4>Round { roundCount } has begun and your team { teamName } has a new matchup!</h4>");
                body.AppendLine($"<b>Competitor: { competitor.TeamCompeting.TeamName }</b>");
                body.AppendLine("<p>Good luck!</p>");
                body.AppendLine($"<br>");
                body.AppendLine("<em>~Tournament Tracker</em>");
            }
            else
            {
                subject = "You have a bye round!";

                body.AppendLine("<p>Enjoy your round off!</p>");
                body.AppendLine($"<br>");
                body.AppendLine("<em>~Tournament Tracker</em>");
            }

            EmailLogic.SendEmail(to, subject, body.ToString());
        }

        private static int GetCurrentRound(this TournamentModel model)
        {
            int roundCount = 1;
            
            foreach (List<MatchupModel> round in model.Rounds)
            {
                if (round.All(x => x.Winner != null))
                {
                    roundCount++;
                }
                else
                {
                    return roundCount;
                }
            }
            CompleteTournament(model);
            return roundCount - 1;
        }

        private static void ScoreMatchups(List<MatchupModel> matchups)
        {
            foreach (MatchupModel matchup in matchups)
            {
                if (matchup.Entries.Count < 2)
                {
                    matchup.Winner = matchup.Entries[0].TeamCompeting;
                    continue;
                }
                if (matchup.Entries[0].Score == matchup.Entries[1].Score)
                {
                    throw new Exception(message: "App Does Not Handle Ties.");
                }
                if (GlobalConfig.AppKeyLookup("greaterWins") == "0")
                {
                    if (matchup.Entries[0].Score < matchup.Entries[1].Score)
                    {
                        matchup.Winner = matchup.Entries[0].TeamCompeting;
                    }
                    else if (matchup.Entries[0].Score > matchup.Entries[1].Score)
                    {
                        matchup.Winner = matchup.Entries[1].TeamCompeting;
                    }
                }
                else
                {
                    if (matchup.Entries[0].Score > matchup.Entries[1].Score)
                    {
                        matchup.Winner = matchup.Entries[0].TeamCompeting;
                    }
                    else if (matchup.Entries[0].Score < matchup.Entries[1].Score)
                    {
                        matchup.Winner = matchup.Entries[1].TeamCompeting;
                    }
                }
            }
        }

        private static void AdvanceWinners(List<MatchupModel> matchups, TournamentModel model)
        {
            foreach (MatchupModel parentMatchup in matchups)
            {
                foreach (List<MatchupModel> round in model.Rounds)
                {
                    foreach (MatchupModel matchup in round)
                    {
                        foreach (MatchupEntryModel matchupEntry in matchup.Entries)
                        {
                            if (matchupEntry.ParentMatchup == parentMatchup)
                            {
                                matchupEntry.TeamCompeting = parentMatchup.Winner;
                                GlobalConfig.Connection.UpdateMatchup(matchup);
                            }
                        }
                    }
                }
            }
        }

        private static void CreateFirstRound(TournamentModel model, int byes)
        {
            List<MatchupModel> matchups = new List<MatchupModel>();
            MatchupModel currentMatchupModel = new MatchupModel();

            foreach (TeamModel team in model.EnteredTeams)
            {
                currentMatchupModel.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (currentMatchupModel.Entries.Count > 1 || byes > 0)
                {
                    currentMatchupModel.MatchupRound = 1;
                    matchups.Add(currentMatchupModel);

                    if (byes > 0) byes--;
                    currentMatchupModel = new MatchupModel();
                }
            }

            model.Rounds.Add(matchups);
        }

        private static void CreateRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchupModel = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel matchup in previousRound)
                {
                    currentMatchupModel.Entries.Add(new MatchupEntryModel { ParentMatchup = matchup });
                    if (currentMatchupModel.Entries.Count > 1)
                    {
                        currentMatchupModel.MatchupRound = round;
                        currentRound.Add(currentMatchupModel);
                        currentMatchupModel = new MatchupModel();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;

                currentRound = new List<MatchupModel>();
                round++;
            }
        }

        private static List<TeamModel> RandomizeOrder(this List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }

        private static int NumberOfRounds(int teamCount)
        {
            int rounds = 0;
            for(int i = 1; i < teamCount; i *= 2)
            {
                rounds++;
            }
            return rounds;
        }

        private static int NumberOfByes(int teamCount, int rounds)
        {
            int totalTeams = 1;
            for(int i = 0; i < rounds; i++)
            {
                totalTeams *= 2;
            }
            return totalTeams - teamCount;
        }
    }
}
