using Test_Task;

class Program
{
	static void Main(string[] args)
	{
		if(args.Length != 4)
		{
			Console.WriteLine("Usage: FolderSync --source <source_path> --destination <destination_path>  --log <log_file_path> --interval <interval_in_seconds>" + Environment.NewLine);
			return;
		}

		
		string sourcePath = args[1];
		string destinationPath = args[3];
		string logFilePath = args[5];
		int interval = int.Parse(args[7]);

		FolderSynchronization sync = new FolderSynchronization(sourcePath, destinationPath, logFilePath, interval);
		sync.Start();
	}
}