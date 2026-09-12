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
; The person picks the language first, and the wizard, the notice and the
; installed application's first words follow it.
ShowLanguageDialog=yes

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"; InfoBeforeFile: "notice-en.txt"
Name: "tr"; MessagesFile: "compiler:Languages\Turkish.isl"; InfoBeforeFile: "notice-tr.txt"

[Messages]
tr.WelcomeLabel2=Bu sihirbaz [name/ver] uygulamasını bilgisayarınıza kuracak.%n%nNextcalibur, Casper Excalibur Control Center'ın yerini alır: sıcaklıklar, fanlar, klavye aydınlatması, güç modları ve grafik modu, bilgisayarın kendi donanım yazılımı arayüzünden. Sürücü kurmaz ve yönetici hakları olmadan çalışır.
tr.SelectDirDesc=[name] nereye kurulsun?
tr.SelectDirLabel3=Kurulum [name] uygulamasını aşağıdaki klasöre kuracak. Güncellemeler daha sonra yönetici hakkı istemeden buraya kurulur; bu yüzden hesabınızın yazabildiği bir klasör seçin.
tr.FinishedLabelNoIcons=Kurulum [name] uygulamasını bilgisayarınıza kurdu.
tr.FinishedLabel=Kurulum [name] uygulamasını bilgisayarınıza kurdu. Windows ile birlikte başlar ve bildirim alanında yaşar; pencere bir çift tıklama uzağınızda.
en.WelcomeLabel2=This will install [name/ver] on your computer.%n%nNextcalibur replaces the Casper Excalibur Control Center: temperatures, fans, keyboard lighting, power modes and graphics mode, from the laptop's own firmware interface. It installs no driver and runs without administrator rights.
en.SelectDirDesc=Where should [name] be installed?
en.SelectDirLabel3=Setup will install [name] into the following folder. Updates install themselves here later without asking for administrator rights, so choose a folder your account can write to.
en.FinishedLabelNoIcons=Setup has finished installing [name] on your computer.
en.FinishedLabel=Setup has finished installing [name] on your computer. It starts with Windows and lives in the notification area; the window is a double-click away.

[CustomMessages]
en.AlreadyInstalled=Nextcalibur %1 is already installed in%n%2%n%nOnly one copy can be installed. It updates itself - open it and use "Check for updates now" in the tray menu. To move it, remove it first from Settings > Apps.
tr.AlreadyInstalled=Nextcalibur %1 zaten şurada kurulu:%n%2%n%nYalnızca bir kopya kurulabilir. Kendini günceller - açın ve bildirim alanı menüsünden "Check for updates now" seçin. Taşımak için önce Ayarlar > Uygulamalar'dan kaldırın.
en.CannotWrite=Nextcalibur cannot write to that folder. Choose one your account can write to - updates install there later without administrator rights.
tr.CannotWrite=Nextcalibur bu klasöre yazamıyor. Hesabınızın yazabildiği bir klasör seçin - güncellemeler daha sonra yönetici hakkı istemeden oraya kurulur.
en.Installing=Installing Nextcalibur...
tr.Installing=Nextcalibur kuruluyor...
en.StartWithWindows=Start Nextcalibur with Windows (in the notification area)
tr.StartWithWindows=Nextcalibur Windows ile başlasın (bildirim alanında)

[Tasks]
Name: "startup"; Description: "{cm:StartWithWindows}"; Flags: checkedonce

[Files]
; The engine, carried inside and run once.
Source: "..\releases\Nextcalibur-win-Setup.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

[INI]
; The application reads this on its first run, registers (or not) its logon
; task accordingly, and deletes the file. Written after Velopack has made the
; folder, so it lands beside Nextcalibur.exe.
Filename: "{app}\first-run.ini"; Section: "FirstRun"; Key: "StartWithWindows"; String: "{code:StartupChoice}"

[Run]
; Velopack installs into the chosen folder, quietly, and does not start the
; application itself (the finish page offers that).
Filename: "{tmp}\Nextcalibur-win-Setup.exe"; Parameters: "--silent --installto ""{app}"""; StatusMsg: "{cm:Installing}"; Flags: runhidden waituntilterminated
Filename: "{app}\Nextcalibur.exe"; Description: "{cm:LaunchProgram,Nextcalibur}"; Flags: postinstall nowait skipifsilent

[Code]
function StartupChoice(Param: String): String;
begin
  if WizardIsTaskSelected('startup') then Result := '1' else Result := '0';
end;

// One copy per machine. Velopack registers the installed copy under this key;
// a second install elsewhere would take over the entry and orphan the first
// folder, and two versions side by side is exactly what the owner ruled out.
// The installed copy updates itself; there is nothing for a second Setup to do.
function InitializeSetup(): Boolean;
var
  Where, Version: String;
begin
  Result := True;
  if RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'InstallLocation', Where) then
  begin
    RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'DisplayVersion', Version);
    MsgBox(FmtMessage(CustomMessage('AlreadyInstalled'), [Version, Where]), mbInformation, MB_OK);
    Result := False;
  end;
end;

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
        MsgBox(CustomMessage('CannotWrite'), mbError, MB_OK);
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
        MsgBox(CustomMessage('CannotWrite'), mbError, MB_OK);
        Result := False;
        exit;
      end;
      DeleteFile(Probe);
    end;
  end;
end;
