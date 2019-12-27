$adminRole=[System.Security.Principal.WindowsBuiltInRole]::Administrator

try
{
    Write-Host "Stopping process"
	Get-Process -Name "StockATM"
    Get-Process -Name "StockATM" | stop-process -Force 
	
}
catch [System.Exception]
{
    Write-Host "Process not found"
    "Error: $Error" >> errorlog.txt 
}
finally
{
    Write-Host "cleaning up ..."
    Start-Process -FilePath "C:\TradeSoft\StockATM\StockATM.exe"
}