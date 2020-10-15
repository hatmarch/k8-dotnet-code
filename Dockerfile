# This image is too new for our host in OpenShift.  If we try to use this container we
# currently get the error:
#   a Windows version 10.0.19041-based image is incompatible with a 10.0.17763 host
# 
# FROM microsoft/aspnet

# Instead we try to pull an image that is from an OS version less than the host node's
# See also here: https://docs.microsoft.com/en-us/virtualization/windowscontainers/deploy-containers/version-compatibility?tabs=windows-server-2004%2Cwindows-10-2004
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.6.2-windowsservercore-10.0.14393.1884

RUN powershell -NoProfile -Command Remove-Item -Recurse C:\inetpub\wwwroot\*

WORKDIR /inetpub/wwwroot

COPY WebDeploy/ .