using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableMembers = GlobalConfig.Connection.GetAllPerson();
        private List<PersonModel> selectedMembers = new List<PersonModel>();
        private ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            WireUpLists();
        }

        private void WireUpLists()
        {
            teamMemberDropdown.DataSource = null;
            teamMemberDropdown.DataSource = availableMembers;
            teamMemberDropdown.DisplayMember = "FullName";

            teamMembersListbox.DataSource = null;
            teamMembersListbox.DataSource = selectedMembers;
            teamMembersListbox.DisplayMember = "FullName";
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            if (validateTeamForm())
            {
                TeamModel model = new TeamModel();
                model.TeamName = teamNameValue.Text;
                model.TeamMembers = selectedMembers;

                TrackerLibrary.GlobalConfig.Connection.CreateTeam(model);

                callingForm.TeamComplete(model);

                this.Close();
            }
            else
            {
                MessageBox.Show("The form has invalid data. Please try again");
            }
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (validatePersonForm())
            {
                PersonModel model = new PersonModel(firstNameValue.Text, lastNameValue.Text, emailValue.Text, phoneValue.Text);

                TrackerLibrary.GlobalConfig.Connection.CreatePerson(model);

                selectedMembers.Add(model);
                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                phoneValue.Text = "";
            }
        }

        private bool validatePersonForm()
        {
            string[] email = emailValue.Text.Split('@');

            if (firstNameValue.Text.Length <= 0)
            {
                MessageBox.Show("Inavlid First Name. Please check your data.");
                return false;
            }
            if (lastNameValue.Text.Length <= 0)
            {
                MessageBox.Show("Inavlid Last Name. Please check your data.");
                return false;
            }
            if (emailValue.Text.Length <= 0 || email.Length != 2 || !email[1].Contains('.'))
            {
                MessageBox.Show("Inavlid Email Address. Please check your data.");
                return false;
            }
            if (phoneValue.Text.Length <= 0)
            {
                MessageBox.Show("Inavlid Phone Number. Please check your data.");
                return false;
            }
            return true;
        }

        private bool validateTeamForm()
        {
            if (teamNameValue.Text.Length <= 0)
            {
                return false;
            }
            if (!selectedMembers.Any())
            {
                return false;
            }
            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            if (teamMemberDropdown.SelectedItem != null)
            {
                PersonModel person = (PersonModel)teamMemberDropdown.SelectedItem;

                availableMembers.Remove(person);
                selectedMembers.Add(person);

                WireUpLists();
            }
        }

        private void memberDeleteButton_Click(object sender, EventArgs e)
        {
            if (teamMembersListbox.SelectedItem != null)
            {
                PersonModel person = (PersonModel)teamMembersListbox.SelectedItem;

                selectedMembers.Remove(person);
                availableMembers.Add(person);

                WireUpLists();
            }
        }
    }
}
