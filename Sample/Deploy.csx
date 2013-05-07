using System;
using System.Diagnostics;
using System.IO;

public static bool Run(string fileName, string parameters)
{
	Console.WriteLine("Starting {0}", fileName);

	var process = new Process();
	process.StartInfo.UseShellExecute = false;

	process.StartInfo.FileName = fileName;
	process.StartInfo.Arguments = parameters; 

	process.Start();

    process.WaitForExit();

    Console.WriteLine("Finished {0}", fileName);
    return process.ExitCode == 0;
}

public enum BuildMode
{
	Debug,
	Release	
}

public static void Deploy(string solution = @"Somesolution\Somesolution.sln", bool build = true, bool copy = true, BuildMode configuration = BuildMode.Debug)
{
	var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),@"Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe");

	if (build)
	{
		if (!Run(path,string.Format("{0} /p:Configuration={1} /t:Build", solution, configuration )))
		{
			Console.WriteLine("Failed.");	
		}
	}

	string sourceFolder = string.Format("Somesolution/SomeConsole/bin/{0}", configuration);
	string destinationFolder = "Output";

	if (copy)
	{
		var arguments = string.Format(@"{0} {1} /e /xf *vshost.*", sourceFolder, destinationFolder);

		Run("robocopy", arguments);	
	}

	Run("explorer", destinationFolder);
}
