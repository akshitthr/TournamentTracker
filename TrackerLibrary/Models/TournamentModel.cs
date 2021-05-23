using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        public event EventHandler <DateTime> OnTournamentComplete;
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public decimal EntryFee { get; set; }
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

        public void TournamentComplete()
        {
            OnTournamentComplete?.Invoke(this, DateTime.Now);
        }

        public TournamentModel(int id, string tournamentName, decimal entryFee)
        {
            Id = id;
            TournamentName = tournamentName;
            EntryFee = entryFee;
        }

        public TournamentModel()
        {

        }
    }
}
