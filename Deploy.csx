#load "Utilities.csx"


public static void Deploy(string destinationServerName = "sanistaal-dev02", string solution = "Vertica.Sanistaal.Web.sln", bool build = true, bool copy = true, bool launchSite = true)
{
	var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),@"Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe");

	if (build)
	{
		if (!Run(path,string.Format("{0} /p:Configuration=Debug /t:Build", solution)))
		{
			Console.WriteLine("Failed.");	
		}
	}

	if (copy){
		Run("robocopy", @"Website  \\sanistaal-dev02\c$\inetpub\Sanistaal\Website /e /xf *.cs Microsoft.CommerceServer.Internal.ContentListHelper.dll *SQLite.dll sitecore.config WebDev.WebHost.dll Sitecore*.*  
		/xd packages umbraco app_browsers app_code app_data aspnet_client data install umbraco_client obj");	
	}

	if (launchSite){
		Process.Start("http:sanistaal.local");
	}
}
