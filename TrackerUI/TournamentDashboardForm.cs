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
    public partial class TournamentDashboardForm : Form, ITournamentRequester
    {
        List<TournamentModel> tournaments = GlobalConfig.Connection.GetAllTournaments();
        
        public TournamentDashboardForm()
        {
            InitializeComponent();
            WireUpLists();
        }

        private void WireUpLists()
        {
            tournamentDropdown.DataSource = null;
            tournamentDropdown.DataSource = tournaments;
            tournamentDropdown.DisplayMember = "TournamentName";
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm form = new CreateTournamentForm(this);
            form.Show();
        }

        public void TournamentComplete(TournamentModel model)
        {
            tournaments.Add(model);
            WireUpLists();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            Form form = new TournamentViewerForm((TournamentModel)tournamentDropdown.SelectedItem);
            form.Show();
        }
    }
}
