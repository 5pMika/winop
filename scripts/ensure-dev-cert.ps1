# Creates a self-signed certificate for Winop MSIX development signing.
# Publisher in Package.appxmanifest must match: CN=User Name
param(
    [string]$Subject = "CN=User Name"
)

$CertPath = Join-Path $PSScriptRoot "..\Winop\Winop_TemporaryKey.pfx"

$certDir = Split-Path -Parent $CertPath
if (-not (Test-Path $certDir)) { New-Item -ItemType Directory -Path $certDir -Force | Out-Null }

if (Test-Path $CertPath) {
    Write-Host "Development certificate already exists: $CertPath"
    exit 0
}

Write-Host "Creating development certificate..."
$cert = New-SelfSignedCertificate -Type Custom -KeyUsage DigitalSignature `
    -CertStoreLocation "Cert:\CurrentUser\My" `
    -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}") `
    -Subject $Subject -FriendlyName "Winop Development"

$password = ConvertTo-SecureString -String "dev" -Force -AsPlainText
Export-PfxCertificate -Cert $cert -FilePath $CertPath -Password $password | Out-Null

$cerPath = [System.IO.Path]::ChangeExtension($CertPath, ".cer")
Export-Certificate -Cert $cert -FilePath $cerPath -Type CERT | Out-Null
Write-Host "Certificate created: $CertPath"
