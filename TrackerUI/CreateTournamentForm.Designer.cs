
namespace TrackerUI
{
    partial class CreateTournamentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.createTournamentButton = new System.Windows.Forms.Button();
            this.addTeamButton = new System.Windows.Forms.Button();
            this.teamRemoveButton = new System.Windows.Forms.Button();
            this.prizeRemoveButton = new System.Windows.Forms.Button();
            this.teamsListbox = new System.Windows.Forms.ListBox();
            this.prizesListbox = new System.Windows.Forms.ListBox();
            this.selectTeamLabel = new System.Windows.Forms.Label();
            this.newPrizeLabel = new System.Windows.Forms.LinkLabel();
            this.newTeamLabel = new System.Windows.Forms.LinkLabel();
            this.selectTeamDropdown = new System.Windows.Forms.ComboBox();
            this.tournamentNameTextbox = new System.Windows.Forms.TextBox();
            this.entryFeeValue = new System.Windows.Forms.TextBox();
            this.entryFeeLabel = new System.Windows.Forms.Label();
            this.prizeLabel = new System.Windows.Forms.Label();
            this.teamsLabel = new System.Windows.Forms.Label();
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.headerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // createTournamentButton
            // 
            this.createTournamentButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.createTournamentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.createTournamentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.createTournamentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createTournamentButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createTournamentButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.createTournamentButton.Location = new System.Drawing.Point(346, 471);
            this.createTournamentButton.Name = "createTournamentButton";
            this.createTournamentButton.Size = new System.Drawing.Size(221, 60);
            this.createTournamentButton.TabIndex = 36;
            this.createTournamentButton.Text = "Create Tournament";
            this.createTournamentButton.UseVisualStyleBackColor = true;
            this.createTournamentButton.Click += new System.EventHandler(this.createTournamentButton_Click);
            // 
            // addTeamButton
            // 
            this.addTeamButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.addTeamButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.addTeamButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.addTeamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTeamButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTeamButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.addTeamButton.Location = new System.Drawing.Point(124, 387);
            this.addTeamButton.Name = "addTeamButton";
            this.addTeamButton.Size = new System.Drawing.Size(142, 46);
            this.addTeamButton.TabIndex = 35;
            this.addTeamButton.Text = "Add Team";
            this.addTeamButton.UseVisualStyleBackColor = true;
            this.addTeamButton.Click += new System.EventHandler(this.addTeamButton_Click);
            // 
            // teamRemoveButton
            // 
            this.teamRemoveButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.teamRemoveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.teamRemoveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.teamRemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.teamRemoveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamRemoveButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.teamRemoveButton.Location = new System.Drawing.Point(756, 161);
            this.teamRemoveButton.Name = "teamRemoveButton";
            this.teamRemoveButton.Size = new System.Drawing.Size(118, 59);
            this.teamRemoveButton.TabIndex = 34;
            this.teamRemoveButton.Text = "Remove Selected";
            this.teamRemoveButton.UseVisualStyleBackColor = true;
            this.teamRemoveButton.Click += new System.EventHandler(this.teamRemoveButton_Click);
            // 
            // prizeRemoveButton
            // 
            this.prizeRemoveButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.prizeRemoveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.prizeRemoveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.prizeRemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prizeRemoveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prizeRemoveButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.prizeRemoveButton.Location = new System.Drawing.Point(756, 342);
            this.prizeRemoveButton.Name = "prizeRemoveButton";
            this.prizeRemoveButton.Size = new System.Drawing.Size(118, 59);
            this.prizeRemoveButton.TabIndex = 33;
            this.prizeRemoveButton.Text = "Remove Selected";
            this.prizeRemoveButton.UseVisualStyleBackColor = true;
            this.prizeRemoveButton.Click += new System.EventHandler(this.prizeRemoveButton_Click);
            // 
            // teamsListbox
            // 
            this.teamsListbox.FormattingEnabled = true;
            this.teamsListbox.ItemHeight = 30;
            this.teamsListbox.Location = new System.Drawing.Point(443, 128);
            this.teamsListbox.Name = "teamsListbox";
            this.teamsListbox.Size = new System.Drawing.Size(290, 124);
            this.teamsListbox.TabIndex = 32;
            // 
            // prizesListbox
            // 
            this.prizesListbox.FormattingEnabled = true;
            this.prizesListbox.ItemHeight = 30;
            this.prizesListbox.Location = new System.Drawing.Point(443, 309);
            this.prizesListbox.Name = "prizesListbox";
            this.prizesListbox.Size = new System.Drawing.Size(290, 124);
            this.prizesListbox.TabIndex = 31;
            // 
            // selectTeamLabel
            // 
            this.selectTeamLabel.AutoSize = true;
            this.selectTeamLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectTeamLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.selectTeamLabel.Location = new System.Drawing.Point(37, 308);
            this.selectTeamLabel.Name = "selectTeamLabel";
            this.selectTeamLabel.Size = new System.Drawing.Size(123, 30);
            this.selectTeamLabel.TabIndex = 30;
            this.selectTeamLabel.Text = "Select Team";
            // 
            // newPrizeLabel
            // 
            this.newPrizeLabel.AutoSize = true;
            this.newPrizeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPrizeLabel.Location = new System.Drawing.Point(642, 285);
            this.newPrizeLabel.Name = "newPrizeLabel";
            this.newPrizeLabel.Size = new System.Drawing.Size(91, 21);
            this.newPrizeLabel.TabIndex = 29;
            this.newPrizeLabel.TabStop = true;
            this.newPrizeLabel.Text = "Create New";
            this.newPrizeLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.newPrizeLabel_LinkClicked);
            // 
            // newTeamLabel
            // 
            this.newTeamLabel.AutoSize = true;
            this.newTeamLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newTeamLabel.Location = new System.Drawing.Point(259, 317);
            this.newTeamLabel.Name = "newTeamLabel";
            this.newTeamLabel.Size = new System.Drawing.Size(91, 21);
            this.newTeamLabel.TabIndex = 28;
            this.newTeamLabel.TabStop = true;
            this.newTeamLabel.Text = "Create New";
            this.newTeamLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.newTeamLabel_LinkClicked);
            // 
            // selectTeamDropdown
            // 
            this.selectTeamDropdown.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectTeamDropdown.FormattingEnabled = true;
            this.selectTeamDropdown.Location = new System.Drawing.Point(42, 341);
            this.selectTeamDropdown.Name = "selectTeamDropdown";
            this.selectTeamDropdown.Size = new System.Drawing.Size(308, 33);
            this.selectTeamDropdown.TabIndex = 27;
            // 
            // tournamentNameTextbox
            // 
            this.tournamentNameTextbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentNameTextbox.Location = new System.Drawing.Point(42, 129);
            this.tournamentNameTextbox.Name = "tournamentNameTextbox";
            this.tournamentNameTextbox.Size = new System.Drawing.Size(308, 29);
            this.tournamentNameTextbox.TabIndex = 26;
            // 
            // entryFeeValue
            // 
            this.entryFeeValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryFeeValue.Location = new System.Drawing.Point(141, 218);
            this.entryFeeValue.Name = "entryFeeValue";
            this.entryFeeValue.Size = new System.Drawing.Size(116, 29);
            this.entryFeeValue.TabIndex = 25;
            // 
            // entryFeeLabel
            // 
            this.entryFeeLabel.AutoSize = true;
            this.entryFeeLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryFeeLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.entryFeeLabel.Location = new System.Drawing.Point(37, 218);
            this.entryFeeLabel.Name = "entryFeeLabel";
            this.entryFeeLabel.Size = new System.Drawing.Size(98, 30);
            this.entryFeeLabel.TabIndex = 24;
            this.entryFeeLabel.Text = "Entry Fee";
            // 
            // prizeLabel
            // 
            this.prizeLabel.AutoSize = true;
            this.prizeLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prizeLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.prizeLabel.Location = new System.Drawing.Point(438, 276);
            this.prizeLabel.Name = "prizeLabel";
            this.prizeLabel.Size = new System.Drawing.Size(67, 30);
            this.prizeLabel.TabIndex = 22;
            this.prizeLabel.Text = "Prizes";
            // 
            // teamsLabel
            // 
            this.teamsLabel.AutoSize = true;
            this.teamsLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamsLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.teamsLabel.Location = new System.Drawing.Point(438, 96);
            this.teamsLabel.Name = "teamsLabel";
            this.teamsLabel.Size = new System.Drawing.Size(71, 30);
            this.teamsLabel.TabIndex = 21;
            this.teamsLabel.Text = "Teams";
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentNameLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tournamentNameLabel.Location = new System.Drawing.Point(37, 96);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(186, 30);
            this.tournamentNameLabel.TabIndex = 23;
            this.tournamentNameLabel.Text = "Tournament Name";
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.headerLabel.Location = new System.Drawing.Point(33, 26);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(317, 50);
            this.headerLabel.TabIndex = 20;
            this.headerLabel.Text = "Create Tournament";
            // 
            // CreateTournamentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(905, 569);
            this.Controls.Add(this.createTournamentButton);
            this.Controls.Add(this.addTeamButton);
            this.Controls.Add(this.teamRemoveButton);
            this.Controls.Add(this.prizeRemoveButton);
            this.Controls.Add(this.teamsListbox);
            this.Controls.Add(this.prizesListbox);
            this.Controls.Add(this.selectTeamLabel);
            this.Controls.Add(this.newPrizeLabel);
            this.Controls.Add(this.newTeamLabel);
            this.Controls.Add(this.selectTeamDropdown);
            this.Controls.Add(this.tournamentNameTextbox);
            this.Controls.Add(this.entryFeeValue);
            this.Controls.Add(this.entryFeeLabel);
            this.Controls.Add(this.prizeLabel);
            this.Controls.Add(this.teamsLabel);
            this.Controls.Add(this.tournamentNameLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "CreateTournamentForm";
            this.Text = "Create Tournament";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createTournamentButton;
        private System.Windows.Forms.Button addTeamButton;
        private System.Windows.Forms.Button teamRemoveButton;
        private System.Windows.Forms.Button prizeRemoveButton;
        private System.Windows.Forms.ListBox teamsListbox;
        private System.Windows.Forms.ListBox prizesListbox;
        private System.Windows.Forms.Label selectTeamLabel;
        private System.Windows.Forms.LinkLabel newPrizeLabel;
        private System.Windows.Forms.LinkLabel newTeamLabel;
        private System.Windows.Forms.ComboBox selectTeamDropdown;
        private System.Windows.Forms.TextBox tournamentNameTextbox;
        private System.Windows.Forms.TextBox entryFeeValue;
        private System.Windows.Forms.Label entryFeeLabel;
        private System.Windows.Forms.Label prizeLabel;
        private System.Windows.Forms.Label teamsLabel;
        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.Label headerLabel;
    }
}