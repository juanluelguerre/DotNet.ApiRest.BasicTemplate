# Registro the Aplicaciones en Azure Active Directory (AAD) y actualziación the los ficheros de configuración usando PowerShell

### Presentation of the scripts

Este Script automatiza la creación de aplicaciones en AAD y los ficheos configuración.

Scripts:

- `ConfigureAuth.ps1`:  
  - Crea una aplicación AAD y sus objetos relacionados (permisos, dependencias y secretos),
  - Actualiza los ficheros de configuración de los proyectos C# y/o Angular
  - Crea un fichero resumen `createdApps.html` en el directorio en el cual se ejecuta el script, que contiene para cad aplicación creada en AAD:
    - Identificador de la aplicación
    - AppId de la aplicación
    - url de su registro en [Azure portal](https://portal.azure.com)

- `CleanupAuth.ps1` which cleans-up the Azure AD objects created by `Configure.ps1`. Note that this script does not revert the changes done in the configuration files, though. You will need to undo the change from source control (from Visual Studio, or from the command line using, for instance, git reset).

### Usage pattern for tests and DevOps scenarios

The `ConfigureAuth.ps1` will stop if it tries to create an Azure AD application which already exists in the tenant. For this, if you are using the script to try/test the sample, or in DevOps scenarios, you might want to run `CleanupAuth.ps1` just before `ConfigureAuth.ps1`. This is what is shown in the steps below.

## How to use the app creation scripts ?

### Pre-requisites

To use the app creation scripts:

1. Open PowerShell (On Windows, press  `Windows-R` and type `PowerShell` in the search window)
2. Navigate to the root directory of the project.
3. Until you change it, the default Execution Policy for scripts is usually `Restricted`. In order to run the PowerShell script you need to set the Execution Policy to `Unrestricted`. You can set this just for the current PowerShell process by running the command:
    ```PowerShell
    Set-ExecutionPolicy -Scope Process -ExecutionPolicy Unrestricted
    ```
4. If you have never done it already, in the PowerShell window, install the AzureAD PowerShell modules. For this:

   1. Open PowerShell as admin (On Windows, Search Powershell in the search bar, right click on it and select Run as administrator).
   2. Type:
        ```
        PowerShell
        Install-Module AzureAD
        ```

### Four ways to run the script

We advise four ways of running the script:

- Interactive: you will be prompted for credentials, and the scripts decide in which tenant to create the objects,
- non-interactive: you will provide crendentials, and the scripts decide in which tenant to create the objects,
- Interactive in specific tenant: you will be prompted for credentials, and the scripts decide in which tenant to create the objects,
- non-interactive in specific tenant: you will provide crendentials, and the scripts decide in which tenant to create the objects.

Here are the details on how to do this.

#### Option 1 (interactive)

- Just run ``. .\Configure.ps1``, and you will be prompted to sign-in (email address, password, and if needed MFA).
- The script will be run as the signed-in user and will use the tenant in which the user is defined.

Note that the script will choose the tenant in which to create the applications, based on the user. Also to run the `Cleanup.ps1` script, you will need to re-sign-in.

#### Option 2 (non-interactive)

When you know the indentity and credentials of the user in the name of whom you want to create the applications, you can use the non-interactive approach. It's more adapted to DevOps. Here is an example of script you'd want to run in a PowerShell Window

```PowerShell
$secpasswd = ConvertTo-SecureString "[Password here]" -AsPlainText -Force
$mycreds = New-Object System.Management.Automation.PSCredential ("[login@tenantName here]", $secpasswd)
. .\Cleanup.ps1 -Credential $mycreds
. .\Configure.ps1 -Credential $mycreds
```

Of course, in real life, you might already get the password as a `SecureString`. You might also want to get the password from KeyVault.

#### Option 3 (Interactive, but create apps in a specified tenant)

  if you want to create the apps in a particular tenant, you can use the following option:
- open the [Azure portal](https://portal.azure.com)
- Select the Azure Active directory you are interested in (in the combo-box below your name on the top right of the browser window)
- Find the "Active Directory" object in this tenant
- Go to **Properties** and copy the content of the **Directory Id** property
- Then use the full syntax to run the scripts:

```PowerShell
$tenantId = "yourTenantIdGuid"
. .\Cleanup.ps1 -TenantId $tenantId
. .\Configure.ps1 -TenantId $tenantId
```

#### Option 4 (non-interactive, and create apps in a specified tenant)

This option combines option 2 and option 3: it creates the application in a specific tenant. See option 3 for the way to get the tenant Id. Then run:

```PowerShell
$secpasswd = ConvertTo-SecureString "[Password here]" -AsPlainText -Force
$mycreds = New-Object System.Management.Automation.PSCredential ("[login@tenantName here]", $secpasswd)
$tenantId = "yourTenantIdGuid"
. .\Cleanup.ps1 -Credential $mycreds -TenantId $tenantId
. .\Configure.ps1 -Credential $mycreds -TenantId $tenantId
```
