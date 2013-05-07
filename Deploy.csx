#load "Utilities.csx"


// This is a simple example where we build a solution and copy the exe file to a different location and then launch it
public static void Deploy(string solution = @"sample/somesolution/somesolution.sln", bool build = true, bool copy = true, bool launchSite = true, string sourceFolder = @"sample/somesolution/Someconsole/bin/debug/", string destinationFolder = "Sample/SampleDestination")
{
	var path = GetMsBuild;
	var arguments = string.Format("{0} /p:Configuration=Debug /t:Build", solution);

	Console.WriteLine(arguments);

	if (build)
	{
		if (!Run(path,arguments))
		{
			Console.WriteLine("Failed.");	
		}
	}

	/*if (copy){
		Run("robocopy", @"Website  \\sanistaal-dev02\c$\inetpub\Sanistaal\Website /e /xf *.cs Microsoft.CommerceServer.Internal.ContentListHelper.dll *SQLite.dll sitecore.config WebDev.WebHost.dll Sitecore*.*  
		/xd packages umbraco app_browsers app_code app_data aspnet_client data install umbraco_client obj");	
	}
	*/

	if (copy)
	{
		arguments = string.Format(@"{0} {1} /e /xf *vshost.*", sourceFolder, destinationFolder);
		Run("robocopy", arguments);		
	}

	Process.Start(Path.Combine(destinationFolder,"Someconsole.exe"));

	Run(Path.Combine(destinationFolder,"Someconsole.exe"));
	
}
