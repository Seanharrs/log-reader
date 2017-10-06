namespace LogReaderGUI
{
	partial class KagLogReader
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
			if(disposing && (components != null))
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
			this.dirSelectGroup = new System.Windows.Forms.GroupBox();
			this.changeFilepathButton = new System.Windows.Forms.Button();
			this.steamDirButton = new System.Windows.Forms.RadioButton();
			this.preSteamDirButton = new System.Windows.Forms.RadioButton();
			this.classicDirButton = new System.Windows.Forms.RadioButton();
			this.modeSelectGroup = new System.Windows.Forms.GroupBox();
			this.timesPlayedWithText = new System.Windows.Forms.TextBox();
			this.timesPlayedWithButton = new System.Windows.Forms.RadioButton();
			this.playerSearchText = new System.Windows.Forms.TextBox();
			this.playerSearchButton = new System.Windows.Forms.RadioButton();
			this.readFileButton = new System.Windows.Forms.RadioButton();
			this.beginButton = new System.Windows.Forms.Button();
			this.nextFileButton = new System.Windows.Forms.Button();
			this.previousFileButton = new System.Windows.Forms.Button();
			this.classicDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.preSteamDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.steamDirDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.displayOutput = new System.Windows.Forms.RichTextBox();
			this.dirSelectGroup.SuspendLayout();
			this.modeSelectGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// headerLabel
			// 
			this.headerLabel.AutoSize = true;
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.headerLabel.Location = new System.Drawing.Point(12, 9);
			this.headerLabel.Name = "headerLabel";
			this.headerLabel.Size = new System.Drawing.Size(63, 25);
			this.headerLabel.TabIndex = 0;
			this.headerLabel.Text = "Help:";
			// 
			// dirSelectGroup
			// 
			this.dirSelectGroup.Controls.Add(this.changeFilepathButton);
			this.dirSelectGroup.Controls.Add(this.steamDirButton);
			this.dirSelectGroup.Controls.Add(this.preSteamDirButton);
			this.dirSelectGroup.Controls.Add(this.classicDirButton);
			this.dirSelectGroup.Location = new System.Drawing.Point(12, 208);
			this.dirSelectGroup.Name = "dirSelectGroup";
			this.dirSelectGroup.Size = new System.Drawing.Size(235, 100);
			this.dirSelectGroup.TabIndex = 3;
			this.dirSelectGroup.TabStop = false;
			// 
			// changeFilepathButton
			// 
			this.changeFilepathButton.Location = new System.Drawing.Point(129, 20);
			this.changeFilepathButton.Name = "changeFilepathButton";
			this.changeFilepathButton.Size = new System.Drawing.Size(92, 63);
			this.changeFilepathButton.TabIndex = 10;
			this.changeFilepathButton.Text = "Set filepath for selected version\r\n";
			this.changeFilepathButton.UseVisualStyleBackColor = true;
			this.changeFilepathButton.Click += new System.EventHandler(this.ChangeFilepathButton_Click);
			// 
			// steamDirButton
			// 
			this.steamDirButton.AutoSize = true;
			this.steamDirButton.Location = new System.Drawing.Point(7, 66);
			this.steamDirButton.Name = "steamDirButton";
			this.steamDirButton.Size = new System.Drawing.Size(97, 17);
			this.steamDirButton.TabIndex = 4;
			this.steamDirButton.TabStop = true;
			this.steamDirButton.Text = "Steam Release";
			this.steamDirButton.UseVisualStyleBackColor = true;
			this.steamDirButton.CheckedChanged += new System.EventHandler(this.SteamDirButton_CheckedChanged);
			// 
			// preSteamDirButton
			// 
			this.preSteamDirButton.AutoSize = true;
			this.preSteamDirButton.Location = new System.Drawing.Point(7, 43);
			this.preSteamDirButton.Name = "preSteamDirButton";
			this.preSteamDirButton.Size = new System.Drawing.Size(116, 17);
			this.preSteamDirButton.TabIndex = 2;
			this.preSteamDirButton.TabStop = true;
			this.preSteamDirButton.Text = "Pre-Steam Release";
			this.preSteamDirButton.UseVisualStyleBackColor = true;
			this.preSteamDirButton.CheckedChanged += new System.EventHandler(this.PreSteamDirButton_CheckedChanged);
			// 
			// classicDirButton
			// 
			this.classicDirButton.AutoSize = true;
			this.classicDirButton.Location = new System.Drawing.Point(7, 20);
			this.classicDirButton.Name = "classicDirButton";
			this.classicDirButton.Size = new System.Drawing.Size(58, 17);
			this.classicDirButton.TabIndex = 0;
			this.classicDirButton.TabStop = true;
			this.classicDirButton.Text = "Classic";
			this.classicDirButton.UseVisualStyleBackColor = true;
			this.classicDirButton.CheckedChanged += new System.EventHandler(this.ClassicDirButton_CheckedChanged);
			// 
			// modeSelectGroup
			// 
			this.modeSelectGroup.Controls.Add(this.timesPlayedWithText);
			this.modeSelectGroup.Controls.Add(this.timesPlayedWithButton);
			this.modeSelectGroup.Controls.Add(this.playerSearchText);
			this.modeSelectGroup.Controls.Add(this.playerSearchButton);
			this.modeSelectGroup.Controls.Add(this.readFileButton);
			this.modeSelectGroup.Location = new System.Drawing.Point(366, 208);
			this.modeSelectGroup.Name = "modeSelectGroup";
			this.modeSelectGroup.Size = new System.Drawing.Size(229, 100);
			this.modeSelectGroup.TabIndex = 6;
			this.modeSelectGroup.TabStop = false;
			// 
			// timesPlayedWithText
			// 
			this.timesPlayedWithText.Enabled = false;
			this.timesPlayedWithText.Location = new System.Drawing.Point(123, 63);
			this.timesPlayedWithText.Name = "timesPlayedWithText";
			this.timesPlayedWithText.Size = new System.Drawing.Size(100, 20);
			this.timesPlayedWithText.TabIndex = 5;
			this.timesPlayedWithText.Text = "SlayerSean";
			// 
			// timesPlayedWithButton
			// 
			this.timesPlayedWithButton.AutoSize = true;
			this.timesPlayedWithButton.Location = new System.Drawing.Point(4, 66);
			this.timesPlayedWithButton.Name = "timesPlayedWithButton";
			this.timesPlayedWithButton.Size = new System.Drawing.Size(113, 17);
			this.timesPlayedWithButton.TabIndex = 4;
			this.timesPlayedWithButton.TabStop = true;
			this.timesPlayedWithButton.Text = "Times Played With";
			this.timesPlayedWithButton.UseVisualStyleBackColor = true;
			this.timesPlayedWithButton.CheckedChanged += new System.EventHandler(this.TimesPlayedWithButton_CheckedChanged);
			// 
			// playerSearchText
			// 
			this.playerSearchText.Enabled = false;
			this.playerSearchText.Location = new System.Drawing.Point(123, 40);
			this.playerSearchText.Name = "playerSearchText";
			this.playerSearchText.Size = new System.Drawing.Size(100, 20);
			this.playerSearchText.TabIndex = 3;
			this.playerSearchText.Text = "SlayerSean";
			// 
			// playerSearchButton
			// 
			this.playerSearchButton.AutoSize = true;
			this.playerSearchButton.Location = new System.Drawing.Point(4, 43);
			this.playerSearchButton.Name = "playerSearchButton";
			this.playerSearchButton.Size = new System.Drawing.Size(106, 17);
			this.playerSearchButton.TabIndex = 2;
			this.playerSearchButton.TabStop = true;
			this.playerSearchButton.Text = "Search for Player";
			this.playerSearchButton.UseVisualStyleBackColor = true;
			this.playerSearchButton.CheckedChanged += new System.EventHandler(this.PlayerSearchButton_CheckedChanged);
			// 
			// readFileButton
			// 
			this.readFileButton.AutoSize = true;
			this.readFileButton.Location = new System.Drawing.Point(4, 18);
			this.readFileButton.Name = "readFileButton";
			this.readFileButton.Size = new System.Drawing.Size(88, 17);
			this.readFileButton.TabIndex = 0;
			this.readFileButton.TabStop = true;
			this.readFileButton.Text = "Default Read";
			this.readFileButton.UseVisualStyleBackColor = true;
			this.readFileButton.CheckedChanged += new System.EventHandler(this.ReadFileButton_CheckedChanged);
			// 
			// beginButton
			// 
			this.beginButton.Enabled = false;
			this.beginButton.Location = new System.Drawing.Point(269, 219);
			this.beginButton.Name = "beginButton";
			this.beginButton.Size = new System.Drawing.Size(75, 23);
			this.beginButton.TabIndex = 7;
			this.beginButton.Text = "Begin";
			this.beginButton.UseVisualStyleBackColor = true;
			this.beginButton.Click += new System.EventHandler(this.BeginButton_Click);
			// 
			// nextFileButton
			// 
			this.nextFileButton.Enabled = false;
			this.nextFileButton.Location = new System.Drawing.Point(269, 277);
			this.nextFileButton.Name = "nextFileButton";
			this.nextFileButton.Size = new System.Drawing.Size(75, 23);
			this.nextFileButton.TabIndex = 8;
			this.nextFileButton.Text = "Next File";
			this.nextFileButton.UseVisualStyleBackColor = true;
			this.nextFileButton.Click += new System.EventHandler(this.NextFileButton_Click);
			// 
			// previousFileButton
			// 
			this.previousFileButton.Enabled = false;
			this.previousFileButton.Location = new System.Drawing.Point(269, 248);
			this.previousFileButton.Name = "previousFileButton";
			this.previousFileButton.Size = new System.Drawing.Size(75, 23);
			this.previousFileButton.TabIndex = 9;
			this.previousFileButton.Text = "Previous File";
			this.previousFileButton.UseVisualStyleBackColor = true;
			this.previousFileButton.Click += new System.EventHandler(this.PreviousFileButton_Click);
			// 
			// displayOutput
			// 
			this.displayOutput.Location = new System.Drawing.Point(12, 37);
			this.displayOutput.Name = "displayOutput";
			this.displayOutput.Size = new System.Drawing.Size(583, 176);
			this.displayOutput.TabIndex = 10;
			this.displayOutput.Text = "";
			// 
			// kagLogReader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(607, 320);
			this.Controls.Add(this.displayOutput);
			this.Controls.Add(this.previousFileButton);
			this.Controls.Add(this.nextFileButton);
			this.Controls.Add(this.beginButton);
			this.Controls.Add(this.modeSelectGroup);
			this.Controls.Add(this.dirSelectGroup);
			this.Controls.Add(this.headerLabel);
			this.Name = "KagLogReader";
			this.Text = "KAG Log Reader v1";
			this.Load += new System.EventHandler(this.KagLogReader_Load);
			this.dirSelectGroup.ResumeLayout(false);
			this.dirSelectGroup.PerformLayout();
			this.modeSelectGroup.ResumeLayout(false);
			this.modeSelectGroup.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label headerLabel;
		private System.Windows.Forms.GroupBox dirSelectGroup;
		private System.Windows.Forms.RadioButton classicDirButton;
		private System.Windows.Forms.RadioButton steamDirButton;
		private System.Windows.Forms.RadioButton preSteamDirButton;
		private System.Windows.Forms.GroupBox modeSelectGroup;
		private System.Windows.Forms.TextBox timesPlayedWithText;
		private System.Windows.Forms.RadioButton timesPlayedWithButton;
		private System.Windows.Forms.TextBox playerSearchText;
		private System.Windows.Forms.RadioButton playerSearchButton;
		private System.Windows.Forms.RadioButton readFileButton;
		private System.Windows.Forms.Button beginButton;
		private System.Windows.Forms.Button nextFileButton;
		private System.Windows.Forms.Button previousFileButton;
		private System.Windows.Forms.FolderBrowserDialog classicDirDialog;
		private System.Windows.Forms.FolderBrowserDialog preSteamDirDialog;
		private System.Windows.Forms.FolderBrowserDialog steamDirDialog;
		private System.Windows.Forms.Button changeFilepathButton;
		private System.Windows.Forms.RichTextBox displayOutput;
	}
}

