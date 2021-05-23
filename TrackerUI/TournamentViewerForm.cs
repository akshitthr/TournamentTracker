using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        TournamentModel tournament = new TournamentModel();
        List<int> rounds = new List<int>();
        BindingList<MatchupModel> availableMatchups = new BindingList<MatchupModel>();
        
        public TournamentViewerForm(TournamentModel model)
        {
            tournament = model;
            tournament.OnTournamentComplete += Tournament_OnTournamentComplete; ;
            
            InitializeComponent();

            tournamentName.Text = model.TournamentName;

            for (int i = 1; i <= model.Rounds.Count(); i++)
            {
                rounds.Add(i);
            }

            roundDropdown.DataSource = rounds;

            matchupListbox.DataSource = null;
            matchupListbox.DataSource = availableMatchups;
            matchupListbox.DisplayMember = "DisplayName";
        }

        private void Tournament_OnTournamentComplete(object sender, DateTime e)
        {
            this.Close();
        }

        private void SetTeamName(MatchupModel matchup)
        {
            if (matchup != null)
            {
                for (int i = 0; i < matchup.Entries.Count; i++)
                {
                    MatchupEntryModel matchupEntry = matchup.Entries[i];
                    if (i == 0)
                    {
                        team1NameLabel.Text = matchup.Entries[0].TeamCompeting == null ? "Team Not Set" : matchup.Entries[0].TeamCompeting.TeamName;
                        team1ScoreValue.Text = matchup.Entries[0].TeamCompeting == null ? "0" : matchup.Entries[0].Score.ToString();
                        team2ScoreValue.Text = "0";
                        team2NameLabel.Text = "Team Not Set";
                    }
                    else
                    {
                        team2NameLabel.Text = matchup.Entries[1].TeamCompeting == null ? "Team Not Set" : matchup.Entries[1].TeamCompeting.TeamName;
                        team2ScoreValue.Text = matchup.Entries[1].TeamCompeting == null ? "0" : matchup.Entries[1].Score.ToString();
                    }
                }
            }
        }

        private void LoadMatchups()
        {
            availableMatchups.Clear();
            foreach (MatchupModel matchup in tournament.Rounds[(int)roundDropdown.SelectedItem - 1])
            {
                if (matchup.Winner == null || !unplayedonlyCheckbox.Checked)
                {
                    availableMatchups.Add(matchup); 
                }
            }
            if (availableMatchups.Any())
            {
                SetTeamName(availableMatchups[0]);
                ChangeFormVisibility(true);
            }
            else
            {
                ChangeFormVisibility(availableMatchups.Any());
            }
        }

        private void ChangeFormVisibility(bool isVisible)
        {
            team1NameLabel.Visible = isVisible;
            team2NameLabel.Visible = isVisible;
            team1ScoreLabel.Visible = isVisible;
            team2ScoreLabel.Visible = isVisible;
            team1ScoreValue.Visible = isVisible;
            team2ScoreValue.Visible = isVisible;
            vsLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
        }

        private void roundLabel_Click(object sender, EventArgs e)
        {
            
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                MatchupModel matchup = (MatchupModel)matchupListbox.SelectedItem;

                for (int i = 0; i < matchup.Entries.Count; i++)
                {
                    if (i == 0)
                    {
                        matchup.Entries[0].Score = Int32.Parse(team1ScoreValue.Text);

                    }
                    else
                    {
                        matchup.Entries[1].Score = Int32.Parse(team2ScoreValue.Text);
                    }
                }

                TournamentLogic.UpdateRounds(tournament);
                LoadMatchups();
            }
        }

        private bool validateForm()
        {
            MatchupModel matchup = (MatchupModel)matchupListbox.SelectedItem;
            if (matchup.Entries.Any(x => x.TeamCompeting == null))
            {
                MessageBox.Show(caption: "Inavlid Form Data", text: "Cannot update matchup with undetermined teams.", buttons: MessageBoxButtons.OK);
                return false;
            }
            if (matchup.Winner != null)
            {
                MessageBox.Show(caption: "Inavlid Form Data", text: "This matchup has already been updated and a winner has been determined.", buttons: MessageBoxButtons.OK);
                return false;
            }
            if (!Int32.TryParse(team1ScoreValue.Text, out int team1Score))
            {
                MessageBox.Show(caption: "Inavlid Form Data", text: "Team 1 Score is Invalid. Enter valid score for team 1.", buttons: MessageBoxButtons.OK);
                return false;
            }
            if (!Int32.TryParse(team2ScoreValue.Text, out int team2Score))
            {
                MessageBox.Show(caption: "Inavlid Form Data", text: "Team 2 Score is Invalid. Enter valid score for team 2.", buttons: MessageBoxButtons.OK);
                return false;
            }
            if (team1Score == 0 && team2Score == 0)
            {
                MessageBox.Show(caption: "Inavlid Form Data", text: "Both team scores cannot be 0. Enter score for at least one team.", buttons: MessageBoxButtons.OK);
                return false;
            }
            if (team1Score < 0 || team2Score < 0)
            {
                MessageBox.Show(caption: "Inavlid Form Data", text: "Team Scores cannot be negative. Enter positive scores.", buttons: MessageBoxButtons.OK);
                return false;
            }
            if (team1Score == team2Score)
            {
                MessageBox.Show(caption: "Inavlid Form Data", text: "App does not handle ties.", buttons: MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void roundDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void matchupListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MatchupModel matchup = (MatchupModel)matchupListbox.SelectedItem;
            SetTeamName(matchup);
        }

        private void unplayedonlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }
    }
}
