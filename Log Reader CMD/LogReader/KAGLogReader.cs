using System;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace LogReader
{
	class KAGLogReader
	{
		static void Main()
		{
            //HACK needs changing, but doesn't work without this line
			new FileIOPermission(PermissionState.None) { AllLocalFiles = FileIOPermissionAccess.Read };

			bool done;
			do
			{
				string directory = ChooseLogsToView();
				ReadFiles(directory);

				done = IsFinished();
			} while(!done);

			ExitProgram();
		}
 		static string ChooseLogsToView()
		{
			Console.WriteLine("What logs would you like to view today?");
			Console.WriteLine("\t1) Classic");
			Console.WriteLine("\t2) Beta");
			Console.WriteLine("\t3) Release");
			
			bool isValid;
			int choice;
			do
			{
				Console.Write("\nYour choice: ");
				isValid = int.TryParse(Console.ReadLine(), out choice);
				if(!isValid || (choice < 1 || choice > 3))
				{
					Console.WriteLine("Please Enter a 1, 2 or 3.");
				}
			} while(!isValid || (choice < 1 || choice > 3));

			switch(choice)
			{
				case 1: return @"C:\Users\Username\KAG\Logs";
				case 2: return @"C:\Users\Username\KAG-Beta\Logs";
				case 3: return @"C:\Program Files (x86)\Steam\steamapps\common\King Arthur's Gold\Logs";
                default: return @"Error";
			}
		}

		static void ReadFiles(string dir)
		{
			Directory.SetCurrentDirectory(dir);

			string[] output = Directory.GetFiles(Directory.GetCurrentDirectory(), "chat*.txt", SearchOption.AllDirectories);
			foreach(string[] splitText in output.Select(file => File.ReadAllText(file).Split('\n')))
			{
				foreach(string line in splitText.Where(line => Regex.IsMatch(line, @"\n{0,1}\[[0-9]{2}:[0-9]{2}:[0-9]{2}\] <[^\n]*")))
				{
					Console.WriteLine(line.TrimStart('\n'));
				}
				Console.WriteLine("\n\n\t\tPress Enter to view the next file\n");
				Console.ReadLine();
			}
		}

		static bool IsFinished()
		{
			Console.WriteLine("Would you like to view another directory?");
			Console.WriteLine("\t1) Yes");
			Console.WriteLine("\t2) No");

			bool isValid;
			int choice;
			do
			{
				Console.Write("Your choice: ");
				isValid = int.TryParse(Console.ReadLine(), out choice);
			} while(!isValid || (choice != 1 && choice != 2));

			return choice != 1;
		}

		static void ExitProgram()
		{
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
