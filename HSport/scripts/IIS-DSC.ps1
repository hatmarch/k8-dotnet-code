Configuration DeployWebApp
{
  Import-DscResource -ModuleName PsDesiredStateConfiguration

  Node localhost
  {
    #Install the IIS Role
    WindowsFeature IIS
    {
      Name = "Web-Server"
      Ensure = "Present"
    }

    #Install ASP.NET 4.5
    WindowsFeature ASP
    {
      Name = "Web-Asp-Net45"
      Ensure = "Present"
    }

    #Install Management Console
    WindowsFeature WebServerManagementConsole
    {
        Name = "Web-Mgmt-Console"
        Ensure = "Present"
    }
  }
} 
