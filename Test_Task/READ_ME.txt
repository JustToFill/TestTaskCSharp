# Test Task

In this GitHub repository is a C# console application that synchronizes two folders: a source folder and a replica folder. 
The program ensures that the replica folder maintains an identical copy of the source folder. 
Synchronization is performed periodically based on a user-defined interval. 
All file creation, copying, and removal operations are logged to both the console and a log file.

## Features

- One-way synchronization from source to replica.
- Periodic synchronization based on a specified interval.
- Detailed logging of file operations.
- Command-line interface for specifying folder paths, synchronization interval, and log file path.

## Usage

- Open a Command Prompt.
- Navigate to net6.0 through Test_Task > Test_task > bin > Debug > net6.0.
- Run the executable with the required arguments 

### Command-Line Arguments

- --source <source_path>: Path to the source folder.
- --replica <destination_path>: Path to the destination folder.
- --interval <interval_in_seconds>: Synchronization interval in seconds.
- --log <log_file_path>: Path to the log file.

### Example

Test_Task.exe --source "C:\path\to\sourceFolder" --destination "C:\path\to\destinationFolder" --log "C:\path\to\logfile.txt" --interval 60

