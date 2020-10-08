Configuration WebDeployOnly
{
  Import-DscResource -ModuleName PsDesiredStateConfiguration

  Node localhost
  {
    #Install Management Service
    WindowsFeature WebServerManagementService
    {
        Name = "Web-Mgmt-Service"
        Ensure = "Present"
    }


    # Install WebDeploy 3.6
    Package InstallWebDeploy
    {
      Path  = "http://download.microsoft.com/download/0/1/D/01DC28EA-638C-4A22-A57B-4CEF97755C6C/WebDeploy_amd64_en-US.msi"
      ProductId = "{6773A61D-755B-4F74-95CC-97920E45E696}"
      Name = "Microsoft Web Deploy 3.6"
      Arguments = "ADDLOCAL=ALL LicenseAccepted=1"
      Ensure = "Present"
    }  

    # Enable Remote Management
    Registry EnableRemoteManagement
    {
        Key = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WebManagement\Server"
        ValueName = "EnableRemoteManagement"
        Ensure = "Present"
        ValueData = 1
        ValueType = "Dword"
    }

    # Ensure Web Server Management Service is runing
    Service WebServerManagementService
    {
        Name        = "WMSVC"
        StartupType = "Automatic"
        State       = "Running"
    }

    # Ensure Web Deployment Agent Service is runing
    Service WebDeploymentAgentService
    {
        Name        = "MsDepSvc"
        StartupType = "Automatic"
        State       = "Running"
    }

  }
} 
