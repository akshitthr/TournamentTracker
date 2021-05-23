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
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        private List<TeamModel> availableTeams = GlobalConfig.Connection.GetAllTeams();
        private List<TeamModel> selectedTeams = new List<TeamModel>();
        private List<PrizeModel> availablePrizes = new List<PrizeModel>();
        private ITournamentRequester callingForm;
        
        public CreateTournamentForm(ITournamentRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            WireUpLists();
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                TournamentModel model = new TournamentModel();
                model.TournamentName = tournamentNameTextbox.Text;
                model.EntryFee = Decimal.Parse(entryFeeValue.Text);

                foreach (TeamModel team in teamsListbox.Items)
                {
                    model.EnteredTeams.Add(team);
                }

                foreach (PrizeModel prize in prizesListbox.Items)
                {
                    model.Prizes.Add(prize);
                }

                model.CreateAllRounds();

                GlobalConfig.Connection.CreateTournament(model);

                callingForm.TournamentComplete(model);

                this.Close();
            }
            else
            {
                MessageBox.Show("The form conatins invalid data. Please try again.");
            }
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)selectTeamDropdown.SelectedItem;
            if (team != null)
            {
                selectedTeams.Add(team);
                availableTeams.Remove(team);
                WireUpLists();
            }
        }

        private void teamRemoveButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)teamsListbox.SelectedItem;
            if (team != null)
            {
                availableTeams.Add(team);
                selectedTeams.Remove(team);
                WireUpLists();
            }
        }

        private void prizeRemoveButton_Click(object sender, EventArgs e)
        {
            PrizeModel prize = (PrizeModel)prizesListbox.SelectedItem;
            if (prize != null)
            {
                availablePrizes.Remove(prize);
                WireUpLists();
            }
        }

        private void newPrizeLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreatePrizeForm form = new CreatePrizeForm(this);
            form.Show();
        }

        private void newTeamLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm form = new CreateTeamForm(this);
            form.Show();
        }

        private bool validateForm()
        {
            if (tournamentNameTextbox.Text.Length <= 0)
            {
                return false;
            }
            if (!Decimal.TryParse(entryFeeValue.Text, out decimal entryFee))
            {
                return false;
            }
            if (entryFee < 1)
            {
                return false;
            }
            if (teamsListbox.Items.Count < 1)
            {
                return false;
            }
            if (prizesListbox.Items.Count < 1)
            {
                return false;
            }
            return true;
        }
        
        private void WireUpLists()
        {
            selectTeamDropdown.DataSource = null;
            selectTeamDropdown.DataSource = availableTeams;
            selectTeamDropdown.DisplayMember = "TeamName";

            teamsListbox.DataSource = null;
            teamsListbox.DataSource = selectedTeams;
            teamsListbox.DisplayMember = "TeamName";

            prizesListbox.DataSource = null;
            prizesListbox.DataSource = availablePrizes;
            prizesListbox.DisplayMember = "PlaceName";
        }

        public void PrizeComplete(PrizeModel model)
        {
            availablePrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }
    }
}
