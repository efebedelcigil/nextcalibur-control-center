; Nextcalibur.iss - the installer's face.
;
; Inno Setup draws the wizard: where to install, what is happening, a finish
; page. Velopack does the installing and keeps doing the updating afterwards.
; The two are joined at one line: this wizard runs Velopack's Setup silently
; into the folder the person chose. Velopack's own Add/Remove entry is the one
; that remains (its uninstaller runs the application's clean-up hook), so this
; wizard registers none of its own.
;
; Build: ISCC.exe installer\Nextcalibur.iss   (after build.ps1, which makes
; releases\Nextcalibur-win-Setup.exe). Output: installer\output\.

#define AppName "Nextcalibur Control Center"
#ifndef AppVersion
  ; build.ps1 passes /DAppVersion=x.y.z; by hand, the engine's file version is used.
  #define AppVersion GetVersionNumbersString("..\releases\Nextcalibur-win-Setup.exe")
#endif
#define Publisher "Efe Bedelcigil"
#define Url "https://github.com/efebedelcigil/nextcalibur-control-center"

[Setup]
AppId={{7C1E2B3A-3B6E-4C1B-9A55-0F2D6A6C9E01}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#Publisher}
AppPublisherURL={#Url}
AppSupportURL={#Url}
DefaultDirName={localappdata}\Nextcalibur
DisableDirPage=no
DirExistsWarning=no
DisableProgramGroupPage=yes
; Per-user, no elevation: the application updates itself without administrator,
; which it can only do in a folder the account owns.
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=
; Velopack registers the uninstaller; a second entry would be one too many.
Uninstallable=no
CreateUninstallRegKey=no
UpdateUninstallLogAppName=no
OutputDir=output
OutputBaseFilename=Nextcalibur-Setup-{#AppVersion}
SetupIconFile=..\src\Nextcalibur.App\Assets\app.ico
WizardStyle=modern
WizardSizePercent=110
Compression=lzma2/ultra64
SolidCompression=yes
ShowLanguageDialog=no

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"

[Messages]
WelcomeLabel2=This will install [name/ver] on your computer.%n%nNextcalibur replaces the Casper Excalibur Control Center: temperatures, fans, keyboard lighting, power modes and graphics mode, from the laptop's own firmware interface. It installs no driver and runs without administrator rights.
SelectDirDesc=Where should [name] be installed?
SelectDirLabel3=Setup will install [name] into the following folder. Updates install themselves here later without asking for administrator rights, so choose a folder your account can write to.
FinishedLabelNoIcons=Setup has finished installing [name] on your computer.
FinishedLabel=Setup has finished installing [name] on your computer. It starts with Windows and lives in the notification area; the window is a double-click away.

[Files]
; The engine, carried inside and run once.
Source: "..\releases\Nextcalibur-win-Setup.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

[Run]
; Velopack installs into the chosen folder, quietly, and does not start the
; application itself (the finish page offers that).
Filename: "{tmp}\Nextcalibur-win-Setup.exe"; Parameters: "--silent --installto ""{app}"""; StatusMsg: "Installing Nextcalibur..."; Flags: runhidden waituntilterminated
Filename: "{app}\Nextcalibur.exe"; Description: "Start Nextcalibur now"; Flags: postinstall nowait skipifsilent

[Code]
// A folder the account cannot write to would install today and fail to
// update tomorrow. Say so before the wizard goes on.
function NextButtonClick(CurPageID: Integer): Boolean;
var
  Dir, Probe: String;
begin
  Result := True;
  if CurPageID = wpSelectDir then
  begin
    Dir := WizardDirValue;
    if not DirExists(Dir) then
    begin
      if not ForceDirectories(Dir) then
      begin
        MsgBox('Nextcalibur cannot create that folder. Choose one your account can write to - updates install there later without administrator rights.', mbError, MB_OK);
        Result := False;
        exit;
      end;
      RemoveDir(Dir);
    end
    else
    begin
      Probe := AddBackslash(Dir) + 'nextcalibur-write-test.tmp';
      if not SaveStringToFile(Probe, 'ok', False) then
      begin
        MsgBox('Nextcalibur cannot write to that folder. Choose one your account can write to - updates install there later without administrator rights.', mbError, MB_OK);
        Result := False;
        exit;
      end;
      DeleteFile(Probe);
    end;
  end;
end;
