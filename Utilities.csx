using System;
using System.Diagnostics;
using System.IO;

private static string _environmentPath = null;

// This needs to be changed. It not sure that exitcode 0 indicates success
public static bool Run(string fileName, string parameters = null, bool openInNewWindow = false)
{
	Console.WriteLine("Starting {0}", fileName);

	var process = new Process();
	process.StartInfo.UseShellExecute = openInNewWindow;

	process.StartInfo.FileName = fileName;
	process.StartInfo.Arguments = parameters; 

	process.Start();

    process.WaitForExit();

    Console.WriteLine("{0} Exicited with code {1}", fileName, process.ExitCode);
    

    return process.ExitCode == 0;
}

public static string GetMsBuild
{
	get 
	{
		if (_environmentPath == null)
		{
			string frameworkPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),@"Microsoft.NET");
			var dotnetPath = new DirectoryInfo(frameworkPath);
			string framework = dotnetPath.EnumerateDirectories().Select(x => x.Name).FirstOrDefault(x => x == "Framework64") ?? "Framework";
			dotnetPath = new DirectoryInfo(Path.Combine(dotnetPath.FullName,framework));
			_environmentPath = Path.Combine(dotnetPath.FullName,dotnetPath.GetDirectories().OrderByDescending(x => x.Name).First().Name, "msbuild.exe");

		}
		
		return _environmentPath;
	}
}