using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LogReaderGUI
{
	public partial class KagLogReader : Form
	{
		private const int VERSION_NUMBER = 1;
		private const int SECOND_FILE = 1;

		private const string PATH_BUTTON_TEXT = "filepath for selected version";
		private const string CLASSIC = "Classic";
		private const string PRE_STEAM = "Pre-Steam";
		private const string STEAM = "Steam";
		private static readonly string FILE_PATH = Application.UserAppDataPath + "\\logReader.txt";

		private static string currentSelectedVersion;
		private static string currentSelectedMode;
		private static string currentWorkingMode;
		private static string currentWorkingVersion;

		private static int currentFile;
		private static int currentClassicFile = 1;
		private static int currentPreSteamFile = 1;
		private static int currentSteamFile = 1;

		private List<string> logFiles;
		private string playerToSearchFor;
		private string kagVersion;
		private string workingDirectory;
		private bool dirSet, modeSet;
		private int totalNumFiles;

		private Regex chatMessage = new Regex(@".*?<[^\n]*", RegexOptions.IgnoreCase);

		public KagLogReader() { InitializeComponent(); }

		private void KagLogReader_Load(object sender, EventArgs e)
		{
			Application.ApplicationExit += OnApplicationExit;
			string errorText = string.Empty;
			new FileIOPermission(PermissionState.None) { AllLocalFiles = FileIOPermissionAccess.Read, AllFiles = FileIOPermissionAccess.Read};
			try
			{
				string[] text = File.ReadAllText(FILE_PATH).Split('\n');
				currentClassicFile = int.Parse(text[0]);
				currentPreSteamFile = int.Parse(text[1]);
				currentSteamFile = int.Parse(text[2]);
				classicDirDialog.SelectedPath = text[3];
				preSteamDirDialog.SelectedPath = text[4];
				steamDirDialog.SelectedPath = text[5];
			}
			catch(FileNotFoundException) {}
			catch { errorText = "The application couldn't load the saved variables."; }

			displayOutput.Text = HelpText() + errorText;
		}

		private static string HelpText()
			=> $"Welcome to KAG Log Reader version {VERSION_NUMBER}" +
			   "\n\n" +
			   "To begin, select a version using the radio buttons on the left " +
			   "and set the filepath for that version's chat logs using the button beside them "
			   +
			   "then select a search mode using the set of radio buttons on the right." +
			   "\n\n" +
			   "Finally, click the Begin button to display the first file, " +
			   "and click the Previous/Next File buttons to move through the files." +
			   "\n\n" +
			   "If the Begin button is not enabled, make sure you have selected both a version "
			   +
			   "and a mode, and that you have set the directory for that version." +
			   "\n\n" +
			   "If the neither of the Previous/Next File buttons are enabled, only 1 log was " +
			   "found in the given directory." +
			   "\n\n";

		private void ChangeFilepathButton_Click(object sender, EventArgs e)
		{
			if(classicDirButton.Checked)
				GetDirectory(classicDirDialog);
			else if(preSteamDirButton.Checked)
				GetDirectory(preSteamDirDialog);
			else
				GetDirectory(steamDirDialog);

			if((dirSet && modeSet) && workingDirectory != string.Empty)
				beginButton.Enabled = true;
		}

		private void GetDirectory(FolderBrowserDialog dialog)
		{
			if(dialog.ShowDialog() == DialogResult.OK)
				SetDirectory("Change", dialog.SelectedPath);
		}

		private void ClassicDirButton_CheckedChanged(object sender, EventArgs e)
			=> SetVersionAndDirectory(classicDirDialog.SelectedPath, CLASSIC);

		private void PreSteamDirButton_CheckedChanged(object sender, EventArgs e)
			=> SetVersionAndDirectory(preSteamDirDialog.SelectedPath, PRE_STEAM);

		private void SteamDirButton_CheckedChanged(object sender, EventArgs e)
			=> SetVersionAndDirectory(steamDirDialog.SelectedPath, STEAM);
		
		private void SetVersionAndDirectory(string path, string version)
		{
			SetDirectory(path == string.Empty ? "Set" : "Change", path);

			currentSelectedVersion = version;
			OnRadioButtonChanged("dir");
		}

		private void SetDirectory(string text, string directory)
		{
			changeFilepathButton.Text = $"{text} {PATH_BUTTON_TEXT}";
			workingDirectory = directory;
		}

		private void ReadFileButton_CheckedChanged(object sender, EventArgs e)
			=> SetMode(false, false, "Read");

		private void PlayerSearchButton_CheckedChanged(object sender, EventArgs e)
			=> SetMode(true, false, "Search");

		private void TimesPlayedWithButton_CheckedChanged(object sender, EventArgs e) 
			=> SetMode(false, true, "Plays");

		private void SetMode(bool playerSearchOn, bool timesPlayedWithOn, string currentMode)
		{
			playerSearchText.Enabled = playerSearchOn;
			timesPlayedWithText.Enabled = timesPlayedWithOn;
			currentSelectedMode = currentMode;
			OnRadioButtonChanged("mode");
		}

		private void OnRadioButtonChanged(string buttonJob)
		{
			SetPreviousNextButtonStates();

			if(dirSet && modeSet)
			{
				beginButton.Enabled = 
                    workingDirectory != string.Empty 
                    && (currentSelectedMode != currentWorkingMode 
                        || currentSelectedVersion != currentWorkingVersion);
				return;
			}

			switch(buttonJob)
			{
				case "dir":
					dirSet = true;
					break;
				case "mode":
					modeSet = true;
					break;
			}

			if((dirSet && modeSet) && workingDirectory != string.Empty)
				beginButton.Enabled = true;
		}

		private void SetPreviousNextButtonStates()
		{
			if(currentSelectedMode == currentWorkingMode && currentSelectedVersion == currentWorkingVersion)
			{
				if(currentFile < totalNumFiles)
					nextFileButton.Enabled = true;
				if(currentFile > 1)
					previousFileButton.Enabled = true;
			}
			else
			{
				previousFileButton.Enabled = false;
				nextFileButton.Enabled = false;
			}
		}

		private void BeginButton_Click(object sender, EventArgs e)
		{
			beginButton.Enabled = false;

			if(classicDirButton.Checked)
				GetFileFor("Classic");
			else if(preSteamDirButton.Checked)
				GetFileFor("Pre-Steam");
			else
				GetFileFor("Steam");

			if (timesPlayedWithButton.Checked)
				return;

			nextFileButton.Enabled = totalNumFiles > 1;
			previousFileButton.Enabled = currentFile > 1;
		}

		private void GetFileFor(string version)
		{
			currentFile = currentClassicFile;
			currentWorkingVersion = version;
			headerLabel.Text = $"{version} Files:";
			GetOutput(version);
		}

		private void GetOutput(string version)
		{
			Directory.SetCurrentDirectory(workingDirectory);
			try
			{
				logFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "chat*.txt", SearchOption.AllDirectories).ToList();
				totalNumFiles = logFiles.Count;
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show(@"KAG Log Reader does not have permission to access the files in this directory, " +
				                @"if your log files are in this directory please place them in a seperate location.");
				return;
			}

			if (totalNumFiles == 0)
			{
				MessageBox.Show(@"The current directory selected has no KAG Logs in it, " +
				                @"ensure you've selected the correct folder and try set the path again.");
				return;
			}

			if(readFileButton.Checked)
				DisplayOutput("Read", ReadFiles());
			else if(playerSearchButton.Checked)
				DisplayOutput("Search", PlayerSearch(playerSearchText.Text));
			else
				DisplayOutput("Plays", TimesPlayedWith(timesPlayedWithText.Text, version));
		}

		private void DisplayOutput(string mode, string text)
		{
			displayOutput.Text = text;
			currentWorkingMode = mode;
		}

		private void PreviousFileButton_Click(object sender, EventArgs e)
			=> ChangeFileNumber(currentFile - 1, SECOND_FILE, nextFileButton, previousFileButton);

		private void NextFileButton_Click(object sender, EventArgs e)
			=> ChangeFileNumber(currentFile + 1, totalNumFiles, previousFileButton, nextFileButton);

		private void ChangeFileNumber(int num, int boundary, Button button1, Button button2)
		{
			currentFile = num;
			SetCurrentFile();

			if(previousFileButton.Enabled == false)
				button1.Enabled = true;

			if(currentFile == boundary)
				button2.Enabled = false;

            if(readFileButton.Checked)
                displayOutput.Text = ReadCurrentFile(logFiles[currentFile = 1]);
            else if(playerSearchButton.Checked)
                displayOutput.Text = SearchForPlayer(logFiles[currentFile - 1], playerToSearchFor);
            else
                displayOutput.Text = PlayedWithCount(playerToSearchFor, kagVersion);
		}

		private void SetCurrentFile()
		{
			if(classicDirButton.Checked)
				currentClassicFile = currentFile;
			else if(preSteamDirButton.Checked)
				currentPreSteamFile = currentFile;
			else
				currentSteamFile = currentFile;
		}
		
		private string ReadFiles() 
			=> ReadCurrentFile(logFiles[currentFile - 1]);

		private string ReadCurrentFile(string file)
			=> GetFileText(file, line => chatMessage.IsMatch(line));

		private string PlayerSearch(string player) 
			=> SearchForPlayer(logFiles[currentFile - 1], (playerToSearchFor = player));

		private string SearchForPlayer(string file, string player)
			=> GetFileText(file, line => line.Contains(player) && line.Contains("<") && line.Contains(">"));

		private string GetFileText(string file, Func<string, bool> meetsCondition)
		{
			string outputText = string.Empty;
			while(true)
			{
				outputText = File.ReadAllText(file).Split('\n')
					.Where(meetsCondition)
					.Aggregate(outputText, (currentLine, nextLine) => currentLine + nextLine);

				if(outputText.Length == 0)
				{
					logFiles.RemoveAt(currentFile - 1);
					file = logFiles[currentFile - 1];
					continue;
				}
				return outputText;
			}
		}

		private string TimesPlayedWith(string player, string version)
			=> PlayedWithCount((playerToSearchFor = player), (kagVersion = version));

		private string PlayedWithCount(string player, string version)
			=> $"You have played with {player} approximately " +
			   $"{logFiles.Select(f => File.ReadAllText(f).Split('\n')).Count(t => t.Any(l => l.Contains(player)))} " +
			   $"times in {version} KAG.";

		private void OnApplicationExit(object sender, EventArgs e)
			=> File.WriteAllText(FILE_PATH,
			                     $"{currentClassicFile}\n" +
			                     $"{currentPreSteamFile}\n" +
			                     $"{currentSteamFile}\n" +
			                     $"{classicDirDialog.SelectedPath}\n" +
			                     $"{preSteamDirDialog.SelectedPath}\n" +
			                     $"{steamDirDialog.SelectedPath}");
	}
}
