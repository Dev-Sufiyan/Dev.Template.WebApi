# Get the current directory as the directory path
$directoryPath = Get-Location

# Hardcoded value to find
$findValue = "Dev.Template"  # Replace "OldValue" with the actual value you want to find

# Get user input for the replacement value
$replacement = Read-Host "Enter the Project Name"

# Find and replace folder names
Get-ChildItem -Path $directoryPath -Recurse -Directory | 
Where-Object { $_.Name -ne $replacement -and $_.Name -like "*$findValue*" } | 
ForEach-Object {
    $newName = $_.Name -replace [regex]::Escape($findValue), $replacement
    if ($newName -ne $_.Name) {
        Rename-Item -Path $_.FullName -NewName $newName -ErrorAction SilentlyContinue
    }
}

# Find and replace file names
Get-ChildItem -Path $directoryPath -Recurse -File | 
Where-Object { $_.Name -ne $replacement -and $_.Name -like "*$findValue*" } | 
ForEach-Object {
    $newName = $_.Name -replace [regex]::Escape($findValue), $replacement
    if ($newName -ne $_.Name) {
        Rename-Item -Path $_.FullName -NewName $newName -ErrorAction SilentlyContinue
    }
}

# Find and replace content in files (.sln and .csproj only)
Get-ChildItem -Path $directoryPath -Recurse -File -Include *.sln, *.csproj | 
ForEach-Object {
    (Get-Content $_.FullName) -replace [regex]::Escape($findValue), $replacement | 
    Set-Content $_.FullName -ErrorAction SilentlyContinue
}

Write-Host "Replacement completed."
