
namespace TrackerUI
{
    partial class TournamentViewerForm
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
            this.headerLabel = new System.Windows.Forms.Label();
            this.tournamentName = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.roundDropdown = new System.Windows.Forms.ComboBox();
            this.unplayedonlyCheckbox = new System.Windows.Forms.CheckBox();
            this.matchupListbox = new System.Windows.Forms.ListBox();
            this.team1NameLabel = new System.Windows.Forms.Label();
            this.team1ScoreLabel = new System.Windows.Forms.Label();
            this.team1ScoreValue = new System.Windows.Forms.TextBox();
            this.team2NameLabel = new System.Windows.Forms.Label();
            this.team2ScoreLabel = new System.Windows.Forms.Label();
            this.team2ScoreValue = new System.Windows.Forms.TextBox();
            this.vsLabel = new System.Windows.Forms.Label();
            this.scoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.headerLabel.Location = new System.Drawing.Point(32, 23);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(214, 50);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Tournament:";
            // 
            // tournamentName
            // 
            this.tournamentName.AutoSize = true;
            this.tournamentName.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tournamentName.Location = new System.Drawing.Point(231, 23);
            this.tournamentName.Name = "tournamentName";
            this.tournamentName.Size = new System.Drawing.Size(157, 50);
            this.tournamentName.TabIndex = 0;
            this.tournamentName.Text = "<name>";
            // 
            // roundLabel
            // 
            this.roundLabel.AutoSize = true;
            this.roundLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.roundLabel.Location = new System.Drawing.Point(55, 102);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(84, 32);
            this.roundLabel.TabIndex = 0;
            this.roundLabel.Text = "Round";
            this.roundLabel.Click += new System.EventHandler(this.roundLabel_Click);
            // 
            // roundDropdown
            // 
            this.roundDropdown.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundDropdown.FormattingEnabled = true;
            this.roundDropdown.Location = new System.Drawing.Point(155, 102);
            this.roundDropdown.Name = "roundDropdown";
            this.roundDropdown.Size = new System.Drawing.Size(152, 33);
            this.roundDropdown.TabIndex = 1;
            this.roundDropdown.SelectedIndexChanged += new System.EventHandler(this.roundDropdown_SelectedIndexChanged);
            // 
            // unplayedonlyCheckbox
            // 
            this.unplayedonlyCheckbox.AutoSize = true;
            this.unplayedonlyCheckbox.BackColor = System.Drawing.Color.White;
            this.unplayedonlyCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unplayedonlyCheckbox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unplayedonlyCheckbox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.unplayedonlyCheckbox.Location = new System.Drawing.Point(155, 141);
            this.unplayedonlyCheckbox.Name = "unplayedonlyCheckbox";
            this.unplayedonlyCheckbox.Size = new System.Drawing.Size(152, 29);
            this.unplayedonlyCheckbox.TabIndex = 2;
            this.unplayedonlyCheckbox.Text = "Unplayed Only";
            this.unplayedonlyCheckbox.UseVisualStyleBackColor = false;
            this.unplayedonlyCheckbox.CheckedChanged += new System.EventHandler(this.unplayedonlyCheckbox_CheckedChanged);
            // 
            // matchupListbox
            // 
            this.matchupListbox.FormattingEnabled = true;
            this.matchupListbox.ItemHeight = 30;
            this.matchupListbox.Location = new System.Drawing.Point(41, 204);
            this.matchupListbox.Name = "matchupListbox";
            this.matchupListbox.Size = new System.Drawing.Size(368, 214);
            this.matchupListbox.TabIndex = 3;
            this.matchupListbox.SelectedIndexChanged += new System.EventHandler(this.matchupListbox_SelectedIndexChanged);
            // 
            // team1NameLabel
            // 
            this.team1NameLabel.AutoSize = true;
            this.team1NameLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team1NameLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.team1NameLabel.Location = new System.Drawing.Point(457, 204);
            this.team1NameLabel.Name = "team1NameLabel";
            this.team1NameLabel.Size = new System.Drawing.Size(101, 30);
            this.team1NameLabel.TabIndex = 0;
            this.team1NameLabel.Text = "<Team1>";
            // 
            // team1ScoreLabel
            // 
            this.team1ScoreLabel.AutoSize = true;
            this.team1ScoreLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team1ScoreLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.team1ScoreLabel.Location = new System.Drawing.Point(457, 237);
            this.team1ScoreLabel.Name = "team1ScoreLabel";
            this.team1ScoreLabel.Size = new System.Drawing.Size(64, 30);
            this.team1ScoreLabel.TabIndex = 0;
            this.team1ScoreLabel.Text = "Score";
            // 
            // team1ScoreValue
            // 
            this.team1ScoreValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team1ScoreValue.Location = new System.Drawing.Point(527, 238);
            this.team1ScoreValue.Name = "team1ScoreValue";
            this.team1ScoreValue.Size = new System.Drawing.Size(100, 29);
            this.team1ScoreValue.TabIndex = 4;
            // 
            // team2NameLabel
            // 
            this.team2NameLabel.AutoSize = true;
            this.team2NameLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team2NameLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.team2NameLabel.Location = new System.Drawing.Point(457, 349);
            this.team2NameLabel.Name = "team2NameLabel";
            this.team2NameLabel.Size = new System.Drawing.Size(101, 30);
            this.team2NameLabel.TabIndex = 0;
            this.team2NameLabel.Text = "<Team2>";
            // 
            // team2ScoreLabel
            // 
            this.team2ScoreLabel.AutoSize = true;
            this.team2ScoreLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team2ScoreLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.team2ScoreLabel.Location = new System.Drawing.Point(457, 388);
            this.team2ScoreLabel.Name = "team2ScoreLabel";
            this.team2ScoreLabel.Size = new System.Drawing.Size(64, 30);
            this.team2ScoreLabel.TabIndex = 0;
            this.team2ScoreLabel.Text = "Score";
            // 
            // team2ScoreValue
            // 
            this.team2ScoreValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.team2ScoreValue.Location = new System.Drawing.Point(527, 388);
            this.team2ScoreValue.Name = "team2ScoreValue";
            this.team2ScoreValue.Size = new System.Drawing.Size(100, 29);
            this.team2ScoreValue.TabIndex = 4;
            // 
            // vsLabel
            // 
            this.vsLabel.AutoSize = true;
            this.vsLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vsLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.vsLabel.Location = new System.Drawing.Point(522, 293);
            this.vsLabel.Name = "vsLabel";
            this.vsLabel.Size = new System.Drawing.Size(37, 30);
            this.vsLabel.TabIndex = 0;
            this.vsLabel.Text = "VS";
            // 
            // scoreButton
            // 
            this.scoreButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.scoreButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.scoreButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.scoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scoreButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.scoreButton.Location = new System.Drawing.Point(565, 293);
            this.scoreButton.Name = "scoreButton";
            this.scoreButton.Size = new System.Drawing.Size(98, 30);
            this.scoreButton.TabIndex = 5;
            this.scoreButton.Text = "Score";
            this.scoreButton.UseVisualStyleBackColor = true;
            this.scoreButton.Click += new System.EventHandler(this.scoreButton_Click);
            // 
            // TournamentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(703, 457);
            this.Controls.Add(this.scoreButton);
            this.Controls.Add(this.team2ScoreValue);
            this.Controls.Add(this.team1ScoreValue);
            this.Controls.Add(this.matchupListbox);
            this.Controls.Add(this.unplayedonlyCheckbox);
            this.Controls.Add(this.roundDropdown);
            this.Controls.Add(this.tournamentName);
            this.Controls.Add(this.team2ScoreLabel);
            this.Controls.Add(this.team1ScoreLabel);
            this.Controls.Add(this.team2NameLabel);
            this.Controls.Add(this.vsLabel);
            this.Controls.Add(this.team1NameLabel);
            this.Controls.Add(this.roundLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "TournamentViewerForm";
            this.Text = "Tournament Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label tournamentName;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.ComboBox roundDropdown;
        private System.Windows.Forms.CheckBox unplayedonlyCheckbox;
        private System.Windows.Forms.ListBox matchupListbox;
        private System.Windows.Forms.Label team1NameLabel;
        private System.Windows.Forms.Label team1ScoreLabel;
        private System.Windows.Forms.TextBox team1ScoreValue;
        private System.Windows.Forms.Label team2NameLabel;
        private System.Windows.Forms.Label team2ScoreLabel;
        private System.Windows.Forms.TextBox team2ScoreValue;
        private System.Windows.Forms.Label vsLabel;
        private System.Windows.Forms.Button scoreButton;
    }
}

