# Clean out injected environment variables that will cause service monitor to fail
$variables = Get-ChildItem Env:*WIN_WEBSERVER* -Name

foreach($item in $variables)
    { 
        Write-Output "Removing environment variable $item"
        Remove-Item Env:$item 
    }

C:\ServiceMonitor.exe w3svc