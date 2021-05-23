using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        public int Id { get; set; }
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        public int WinnerId { get; set; }
        public TeamModel Winner { get; set; }
        public int MatchupRound { get; set; }

        public string DisplayName
        {
            get
            {
                string output = "";
                for (int i = 0; i < Entries.Count(); i++)
                {
                    if (Entries[i].TeamCompeting != null)
                    {
                        if (i == 0)
                        {
                            output = Entries[i].TeamCompeting.TeamName;
                        }
                        else
                        {
                            if (output.Length > 0)
                            {
                                output += $" vs. {Entries[i].TeamCompeting.TeamName}";
                            }
                            else
                            {
                                output = Entries[i].TeamCompeting.TeamName;
                            }
                        }
                    }
                }
                if (output.Length > 0)
                {
                    return output;
                }
                return "Matchup Not Set";
            }
        }
    }
}
