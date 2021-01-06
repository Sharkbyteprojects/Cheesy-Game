# Store this under (path to Cheese exe)/updater
# to use the Portable Updater
Write-Output "CheesyGame Portable Updater" "<c> Sharkbyteprojects" "Fetch Data"
$webversion = [int](Invoke-WebRequest -Uri "https://github.com/Sharkbyteprojects/Cheesy-Game/raw/master/version.txt").Content
Write-Output "Read Local Data"
$myversion = [int](Get-Content "$((Split-Path -parent $PSCommandPath))/version.txt")
Write-Output "My Version: $([string]$myversion)" "Web Version: $([string]$webversion)"
if($webversion -gt $myversion){
    Write-Output "You need a newer Version, Try Update"
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12, [Net.SecurityProtocolType]::Tls11
    $output = "$($env:temp)\shup\l.zip"
    New-Item -Path (Split-Path -parent $output) -Type Directory
    Invoke-WebRequest -Uri "https://github.com/Sharkbyteprojects/Cheesy-Game/raw/master/builds%20to%20download/buildWindowsPortable.zip" -OutFile $output
    Write-Output "File Downloaded, try Install"
    $of= Read-Host 'Path to Save the New Version:';
    New-Item -Path $of -Type Directory
    Expand-Archive -Path $output -Force -DestinationPath $of
}else {
    Write-Output "You have the Latest Version"
}