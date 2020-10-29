# Clean out injected environment variables that will cause service monitor to fail
$variables = Get-ChildItem Env:*WIN_WEBSERVER* -Name

foreach($item in $variables)
    { 
        Write-Output "Removing environment variable $item"
        Remove-Item Env:$item 
    }

$cm_webconfig_path="c:\var\run\web-config\Web.config"

if (Test-Path $cm_webconfig_path) {
    "Moving Web.config from configmap at {0} into {1}" -f $cm_webconfig_path, (Get-Location)
    cp $cm_webconfig_path .
}
else
{
    Write-Output "No configmap based Web.config found."
}

C:\ServiceMonitor.exe w3svc