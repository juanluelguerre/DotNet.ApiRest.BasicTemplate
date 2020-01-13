[CmdletBinding()]
param(    
    [PSCredential] $Credential,
    [Parameter(Mandatory=$False, HelpMessage='Tenant ID (This is a GUID which represents the "Directory ID" of the AzureAD tenant into which you want to create the apps')]    
    [string] $tenantId="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxxx",
    [Parameter(Mandatory=$False, HelpMessage='API Name without suffix and preffix. i.e: Client.Project.API-NAME.Api')]
    [string] $appName="Items"
)

Import-Module AzureAD
$ErrorActionPreference = 'Stop'

Function Cleanup
{
<#
.Description
This function removes the Azure AD applications for the sample. These applications were created by the Configure.ps1 script
#>

    # $tenantId is the Active Directory Tenant. This is a GUID which represents the "Directory ID" of the AzureAD tenant 
    # into which you want to create the apps. Look it up in the Azure portal in the "Properties" of the Azure AD. 

    # Login to Azure PowerShell (interactive if credentials are not already provided:
    # you'll need to sign-in with creds enabling your to create apps in the tenant)
    if (!$Credential -and $TenantId)
    {
        $creds = Connect-AzureAD -TenantId $tenantId
    }
    else
    {
        if (!$TenantId)
        {
            $creds = Connect-AzureAD -Credential $Credential
        }
        else
        {
            $creds = Connect-AzureAD -TenantId $tenantId -Credential $Credential
        }
    }

    if (!$tenantId)
    {
        $tenantId = $creds.Tenant.Id
    }
    $tenant = Get-AzureADTenantDetail
    $tenantName =  ($tenant.VerifiedDomains | Where-Object { $_._Default -eq $True }).Name
    
    $fullappName = "ElGuerre.$ApplicationName"
    # $fullWebName = "ElGuerre.Web"
    
    # Removes the applications
    Write-Host "Cleaning-up applications from tenant '$tenantName'"

    Write-Host "Removing 'service/api' ($fullappName) if needed"
    $app=Get-AzureADApplication -Filter "identifierUris/any(uri:uri eq 'https://$tenantName/$fullappName')"  
    if ($app)
    {
        Write-Host "Removing $app.ObjectId ..."
        Remove-AzureADApplication -ObjectId $app.ObjectId
        Write-Host "Removed."
    }

    # Write-Host "Removing 'client/web' ($fullWebName) if needed"
    # $app=Get-AzureADApplication -Filter "DisplayName eq '$fullWebName'"  
    # if ($app)
    # {
    #     Write-Host "Removing $app.ObjectId ..."
    #     Remove-AzureADApplication -ObjectId $app.ObjectId
    #     Write-Host "Removed."
    # }

}

Cleanup -Credential $Credential -tenantId $TenantId -appName $ApplicationName
