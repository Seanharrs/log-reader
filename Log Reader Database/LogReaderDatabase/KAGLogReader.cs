using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace LogReaderDatabase
{
	public class KAGLogReader
	{
        //Change Uid and Password to be correct values on use
        //TODO Ask for Uid and Password on startup rather than storing on code.
		private const string DB_CONNECTOR = "Server=localhost;Database=kag_log_reader;Uid=root;Password=DbPw;";
		private static MySqlConnection connection = new MySqlConnection(DB_CONNECTOR);
		private static MySqlCommand command;
		private static MySqlDataReader reader;

		private static void Main()
		{
            //HACK Was getting IO errors without this, but this feels unneccessary
            //especially since it's not being assigned to any variable.
			new FileIOPermission(PermissionState.None) { AllLocalFiles = FileIOPermissionAccess.Read };

			bool done = false;
			while(!done)
			{
				string directory = ChooseLogsToView();
				ReadFiles(directory);
				done = IsFinished();
			}
			ExitProgram();
		}

		private static void AddCommandParameters()
		{
			command.Parameters.AddWithValue("@fileName", string.Empty);
			command.Parameters.AddWithValue("@timeMade", string.Empty);
			command.Parameters.AddWithValue("@lineNumber", int.MinValue);
			command.Parameters.AddWithValue("@timeSaid", string.Empty);
			command.Parameters.AddWithValue("@whatSaid", string.Empty);
			command.Parameters.AddWithValue("@whoSaid", string.Empty);
		}

		private static string ChooseLogsToView()
		{
			Console.WriteLine("What logs would you like to view today?");
			Console.WriteLine("\t1) Classic");
			Console.WriteLine("\t2) Beta");
			Console.WriteLine("\t3) Release");

			bool isValid = false;
			int choice = 0;
			while(!isValid || (choice < 1 || choice > 3))
			{
				Console.Write("\nYour choice: ");
				isValid = int.TryParse(Console.ReadLine(), out choice);
				if(!isValid || (choice < 1 || choice > 3))
					Console.WriteLine("Please Enter a 1, 2 or 3.");
			}

			switch(choice)
			{
				case 1:
					return @"C:\Users\Username\KAG\Logs";
				case 2:
					return @"C:\Users\Username\KAG-Beta\Logs";
				case 3:
					return @"C:\Program Files (x86)\Steam\steamapps\common\King Arthur's Gold\Logs";
				default:
					return @"Error";
			}
		}

		private static void ReadFiles(string dir)
		{
			Directory.SetCurrentDirectory(dir);

			string[] output = Directory.GetFiles(Directory.GetCurrentDirectory(), "chat*.txt", SearchOption.AllDirectories);
			foreach(string fileLocation in output)
			{
				string[] fileLocationComponents = fileLocation.Split('\\');
				string fileName = fileLocationComponents[fileLocationComponents.Length - 1];
				if(!CheckDatabase(fileName))
				{
					string fileNums = fileName.Remove(fileName.Length - 4, 4).Remove(0, 5);
					string fileCreationTime =
						$"20{fileNums.Substring(0, 2)}-{fileNums.Substring(3, 2)}-{fileNums.Substring(6, 2)} " +
						$"{fileNums.Substring(9, 2)}:{fileNums.Substring(12, 2)}:{fileNums.Substring(15, 2)}";
					AddFileToDatabase(fileName, fileCreationTime);
					AddDataToDatabase(fileName, File.ReadAllText(fileLocation).Split('\n').ToList());
				}
				GetDataFromDatabase(fileName);
				Console.WriteLine("\n\n\t\tPress any key to read the next file");
				Console.ReadLine();
			}
		}

		private static void GetDataFromDatabase(string fileName)
		{
			if(connection.State == ConnectionState.Closed)
                connection.Open();

			try
			{
				command = connection.CreateCommand();
				AddCommandParameters();
				command.CommandText = "SELECT line_number, time_said, who_said, what_said FROM chat " +
				                      "WHERE file_name LIKE @fileName " +
				                      "ORDER BY line_number ASC";
				command.Parameters["@fileName"].Value = "%" + fileName + "%";
				reader = command.ExecuteReader();

				while(reader.Read())
                    Console.WriteLine($"[{reader[1]}] <{reader[2]}> {reader[3]}");
			}
			finally
			{
				reader.Close();
				connection.Close();
			}
		}

		private static bool CheckDatabase(string fileName)
		{
			if(connection.State == ConnectionState.Closed)
                connection.Open();

			try
			{
                command = connection.CreateCommand();
                command.CommandText = "SELECT file_name FROM files";
				reader = command.ExecuteReader();

                while(reader.Read())
					if((string)reader[0] == fileName)
                        return true;

				return false;
			}
			finally
			{
				reader.Close();
				connection.Close();
			}
		}

		private static void AddFileToDatabase(string fileName, string fileCreatedTime)
		{
			if(connection.State == ConnectionState.Closed)
                connection.Open();

			try
			{
                command = connection.CreateCommand();
                AddCommandParameters();

                command.CommandText = "INSERT INTO files(file_name, creation_time) VALUES(@fileName, @timeMade)";
				command.Parameters["@fileName"].Value = fileName;
				command.Parameters["@timeMade"].Value = fileCreatedTime;
				command.ExecuteNonQuery();
			}
			finally { connection.Close(); }
		}

		private static void AddDataToDatabase(string fileName, IEnumerable<string> lines)
		{
			if(connection.State == ConnectionState.Closed)
                connection.Open();
			
			List<string> chatLines =
                lines.Where(line => Regex.IsMatch(line, @"\n?\[[0-9:]{8}\] <[^\n]*"))
			         .Select(line => line.TrimStart('\n'))
                     .ToList();
			try
			{
				for(int index = 0; index < chatLines.Count; ++index)
				{
					string line = chatLines[index];
					Match match = Regex.Match(line, @"\[([0-9:]{8})\] <([^>]*)> ([^\r\n]*)");
					string timeSaid = match.Groups[1].Value;
					string whoSaid = match.Groups[2].Value;
					string whatSaid = match.Groups[3].Value;

					command = connection.CreateCommand();
					AddCommandParameters();
					command.CommandText = "INSERT INTO chat(file_name, line_number, who_said, time_said, what_said) " +
					                      "VALUES(@fileName, @lineNumber, @whoSaid, @timeSaid, @whatSaid)";
					command.Parameters["@fileName"].Value = fileName;
					command.Parameters["@lineNumber"].Value = index;
					command.Parameters["@whoSaid"].Value = whoSaid;
					command.Parameters["@timeSaid"].Value = timeSaid;
					command.Parameters["@whatSaid"].Value = whatSaid;
					command.ExecuteNonQuery();
				}
			}
			finally { connection.Close(); }
		}

		private static bool IsFinished()
		{
			Console.WriteLine("Would you like to view another directory?");
			Console.WriteLine("\t1) Yes");
			Console.WriteLine("\t2) No");

			bool isValid = false;
			int choice = 0;
			while(!isValid || (choice != 1 && choice != 2))
			{
				Console.Write("Your choice: ");
				isValid = int.TryParse(Console.ReadLine(), out choice);
			}

			return choice == 2;
		}

		private static void ExitProgram()
		{
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
