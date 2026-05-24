#!/usr/bin/env pwsh
# Runs the tests project and prints a short summary: Success: X  Failed: Y

$proj = Join-Path $PSScriptRoot "TodoManagementApp.Tests.csproj"
if (-not (Test-Path $proj)) { $proj = Join-Path $PSScriptRoot "TodoManagementApp.Tests.csproj" }

$results = dotnet test $proj --no-build 2>&1
Write-Output $results

$passed = 0
$failed = 0
if ($results -match 'Passed:\s*(\d+)') { $passed = [int]$matches[1] }
if ($results -match 'Failed:\s*(\d+)') { $failed = [int]$matches[1] }
Write-Host "Success: $passed  Failed: $failed"