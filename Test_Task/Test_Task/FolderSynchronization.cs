using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task
{
	internal class FolderSynchronization
	{
		private string sourcePath;
		private string destinationPath;
		private string logFilePath;
		private int syncInterval;

		public FolderSynchronization(string sourcePath, string destinationPath, string logFilePath, int syncInterval)
		{
			this.sourcePath = sourcePath;
			this.destinationPath = destinationPath;
			this.logFilePath = logFilePath;
			this.syncInterval = syncInterval;
		}

		public void Start()
		{
			while (true)
			{
				SynchronizeFolders(sourcePath, destinationPath);
				Thread.Sleep(syncInterval * 1000);
			}
		}

		private void SynchronizeFolders(string sourcePath, string destinationPath)
		{
			//Verifies if the given destination path refers to an already existing directory
			if (!Directory.Exists(destinationPath))
			{
				Directory.CreateDirectory(destinationPath);
				Log($"Created replicated folder: {destinationPath}");
			}

			//Gets the Information of the source and replica Directory
			var sourceDirectory = new DirectoryInfo(sourcePath);
			var replicaDirectory = new DirectoryInfo(destinationPath);
			if(sourceDirectory.Exists == false)
			{
				Console.WriteLine("Inserted source Folder doesn't exist!");
				return;
			}

			//Deletes any file in the replica folder that is not in the source folder
			foreach(var replicaFile in replicaDirectory.GetFiles("*", SearchOption.AllDirectories))
			{
				string sourceFilePath = Path.Combine(sourcePath, replicaFile.FullName.Substring(destinationPath.Length + 1));
				if(!File.Exists(sourceFilePath))
				{
					File.Delete(replicaFile.FullName);
					Log($"Removed file: {replicaFile.FullName}");
				}
			}

			//Deletes any directory in the replica folder that is not in the source folder
			foreach(var replicaDir in replicaDirectory.GetDirectories("*", SearchOption.AllDirectories))
			{
				string sourceDirPath = Path.Combine(sourcePath, replicaDir.FullName.Substring(destinationPath.Length + 1));
				if (!Directory.Exists(sourceDirPath))
				{
					Directory.Delete(replicaDir.FullName, true);
					Log($"Removed directory: {replicaDir.FullName}");
				}
			}

			//Copies each file from the source directory to the replica
			foreach(var sourceFile in sourceDirectory.GetFiles("*", SearchOption.AllDirectories)) 
			{
				bool alreadyExists = false;
				string targetFilePath = Path.Combine(destinationPath, sourceFile.FullName.Substring(sourcePath.Length + 1));
				Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));

				//If the file already exists in the destination directory the program doesn't copy it again
				foreach(var destinationFile in replicaDirectory.GetFiles("*", SearchOption.AllDirectories))
				{
					if(sourceFile.Name == destinationFile.Name)
					{
						alreadyExists = true;
					}
				}
				if(alreadyExists == false) 
				{
					sourceFile.CopyTo(targetFilePath, true);
					Log($"Copied file: {sourceFile.FullName} to {targetFilePath}");
				}
			}
		}

		private void Log(string text)
		{
			string logText = $"{DateTime.Now} : {text}";
			Console.WriteLine(logText);
			File.AppendAllText(logFilePath, logText + Environment.NewLine);
		}
	}
}
