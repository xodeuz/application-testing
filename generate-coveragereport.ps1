<#
    How to use:
        Open powershell, navigate to the location of the script, might need to change ExecutionPolicy to Unrestricted
        in powershell window enter:

        generate-report.ps1 -ReportPaht C:\CodeCoverage\MyReport\Current -SolutionPath C:\code\myproject -History C:\CodeCoverage\MyReport\History

        Example:
        generate-report.ps1 -ReportPaht C:\CodeCoverage\user-api\Current -SolutionPath C:\code\user-api -History C:\CodeCoverage\user-api\History

    Suggestion:
        Create .bat file with pre-defined parameters set for easier use locally

#>
[cmdletbinding()]
param(
    [Parameter(Mandatory = $False, ValueFromPipeline = $True, ValueFromPipelinebyPropertyName = $True)]
    [PSDefaultValue(Help = 'Path to where the end test reports will be located')]
    [string[]]$ReportPath = '.',

    [Parameter(Mandatory = $False, ValueFromPipeline = $True, ValueFromPipelinebyPropertyName = $True)]
    [PSDefaultValue(Help = 'Path to the directory where .sln file is located for targeted solution')]
    [string]$SolutionPath = '.',

    [Parameter(Mandatory = $False, ValueFromPipeline = $True, ValueFromPipelinebyPropertyName = $True)]
    [PSDefaultValue(Help = 'Path to the directory where history should be stored')]
    [string]$HistoryPath = '.\report_history'
)

function Write-SectionHeading($message) {
    Write-Host "=========================================================" -ForegroundColor Blue
    Write-Host $message -ForegroundColor Blue
    Write-Host "=========================================================" -ForegroundColor Blue
}

# Note: This would not be needed for a CI/CD pipeline coverage collection as the build agent usually clears its temporary working directory on start of new builds
function Start-PreRunCleanup() {
    Write-SectionHeading -message "Cleaning up previous runs"
    if (Test-Path $ReportPath/testresults) {
        Remove-Item -Recurse -Force $ReportPath/testresults
        Write-Host "Removed old testresults folder..."
    }
    else {
        Write-Host "Target directory is ready, skipping clean up"
    }
    
}

function Start-ReportGeneration() {
    
    Write-SectionHeading -message "Installing dotnet-reportgenerator-globaltool"
    dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.1.4 --verbosity quiet

    Write-SectionHeading -message "Running tests and collecting coverage"
    dotnet test $SolutionPath --logger:trx --results-directory $ReportPath/testresults/xplat --collect:"XPlat Code Coverage" --verbosity quiet /p:CollectCoverage=true 

    Write-SectionHeading -message "Generate HTML report"
    $targetDir = "-targetdir:{0}\testresults\report\html" -f $ReportPath
    $reportLocation = "-reports:{0}\testresults\xplat\*\*.xml" -f $ReportPath
    $historyLocation = "-historydir:{0}" -f $HistoryPath

    # Execute the dotnet tool and generate the report
    reportgenerator $reportLocation $targetDir $historyLocation "-reporttypes:Html;HtmlSummary;HTMLChart;Cobertura" "-assemblyfilters:-*.UnitTest" "riskHotspotsAnalysisThresholds:metricThresholdForCyclomaticComplexity=20"
}


# Clear the screen and call functions in order
Clear-Host
Start-PreRunCleanup
Start-ReportGeneration
Write-Host "All done!" -ForegroundColor Green
