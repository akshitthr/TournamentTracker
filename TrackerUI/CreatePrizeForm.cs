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
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        private IPrizeRequester callingForm;
        
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                PrizeModel model = new PrizeModel(placeNumberValue.Text, placeNameValue.Text, prizeAmountValue.Text, prizePercentageValue.Text);
                
                GlobalConfig.Connection.CreatePrize(model);

                callingForm.PrizeComplete(model);

                this.Close();
            }
            else
            {
                MessageBox.Show("The form has invalid information. Please try again.");
            }
        }

        private bool validateForm()
        {
            if (!int.TryParse(placeNumberValue.Text, out int placeNumber))
            {
                return false;
            }

            if (placeNumber < 1)
            {
                return false;
            }
            
            if (placeNameValue.TextLength <= 0)
            {
                return false;
            }

            if (!decimal.TryParse(prizeAmountValue.Text, out decimal prizeAmount) || !float.TryParse(prizePercentageValue.Text, out float prizePercentage))
            {
                return false;
            }

            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                return false;
            }

            if (prizeAmount > 0 && prizePercentage > 0)
            {
                return false;
            }

            if (prizePercentage > 100)
            {
                return false;
            }

            return true;
        }
    }
}
