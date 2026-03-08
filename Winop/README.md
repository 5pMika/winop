# Winop

WinUI 3 packaged desktop app.

## Tech Stack

- .NET 10.0.103
- Windows App SDK 1.8.5 (1.8.260209005)
- C# 14

## Build

```powershell
dotnet build Winop.csproj -c Debug
```

For a specific platform:

```powershell
dotnet build Winop.csproj -c Debug -p:Platform=x64
```

## Run

**Batch files (workspace root):**
- `run.bat` – Launch the app if already installed
- `build-and-run.bat` – Rebuild, generate MSIX, install, and launch

**Visual Studio (recommended):** Open the solution, press F5 to build, deploy, and run.

**Note:** For first-time install, deploy once from Visual Studio (F5) to create a dev certificate, or run `build-and-run.bat` (which creates a self-signed cert).

## Generate MSIX Package

```powershell
dotnet publish Winop.csproj -c Debug -p:Platform=x64 -p:GenerateAppxPackageOnBuild=true
```

Output: `AppPackages\Winop_1.0.0.0_x64_Debug_Test\Winop_1.0.0.0_x64_Debug.msix`
