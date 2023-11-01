SET SERVICENAME=sc create "UdlaUdlaServiceExtractInfoTeams"
SET sc description Udla SDS Teams Sincronization WinService "Servicio Version 1.0 para sincronización de TEAMS"


SET SERVICEDIRECTORYPATH= BinPath="C:\Program Files\UDLA\UdlaSDSTeamsSincronizationWinService\bin"
SET SERVICEFILENAME=Udla.UdlaServiceExtractInfoTeams.WinServiceApp.exe

SET SERVICEFILEPATH=%SERVICEDIRECTORYPATH%\%SERVICEFILENAME%
%SERVICENAME% %SERVICEFILEPATH%

