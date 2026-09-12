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
en.TypeStandard=Standard - the application and everything it can use
tr.TypeStandard=Standart - uygulama ve kullanabildiği her şey
en.TypeCustom=Custom - choose which dependencies to install
tr.TypeCustom=Özel - hangi bağımlılıkların kurulacağını seçin
en.CompApp=Nextcalibur Control Center
tr.CompApp=Nextcalibur Control Center
en.CompDeps=Dependencies (optional; the application works without them)
tr.CompDeps=Bağımlılıklar (isteğe bağlı; uygulama onlarsız da çalışır)
en.CompPawnIO=PawnIO driver - reads the processor's power (signed, open source, pawnio.eu)
tr.CompPawnIO=PawnIO sürücüsü - işlemcinin güç tüketimini okur (imzalı, açık kaynak, pawnio.eu)
en.AlreadyInstalledRepair=Nextcalibur %1 is already installed in%n%2%n%nOnly one copy can be installed. Repair it? This reinstalls the application in place, keeps your settings, and puts back anything missing - start-up, dependencies. To move it, remove it first from Settings > Apps.
tr.AlreadyInstalledRepair=Nextcalibur %1 zaten şurada kurulu:%n%2%n%nYalnızca bir kopya kurulabilir. Onarılsın mı? Uygulama yerinde yeniden kurulur, ayarlarınız korunur, eksik olan her şey - başlangıç, bağımlılıklar - geri konur. Taşımak için önce Ayarlar > Uygulamalar'dan kaldırın.
en.RepairTitle=Repair
tr.RepairTitle=Onar
en.DownloadingPawnIO=Downloading the PawnIO driver...
tr.DownloadingPawnIO=PawnIO sürücüsü indiriliyor...
en.PawnIOFailed=The PawnIO driver could not be installed now (%1). Nextcalibur works without it; it will offer it again later.
tr.PawnIOFailed=PawnIO sürücüsü şu an kurulamadı (%1). Nextcalibur onsuz da çalışır; daha sonra yeniden önerecek.
en.PawnIOBadSignature=the download is not signed by namazso.eu
tr.PawnIOBadSignature=indirilen dosya namazso.eu imzalı değil
en.NoNvidiaDriver=The NVIDIA graphics driver was not found (nvml.dll). Nextcalibur reads the graphics card through it. Install the driver from nvidia.com first, then run this setup again.
tr.NoNvidiaDriver=NVIDIA grafik sürücüsü bulunamadı (nvml.dll). Nextcalibur ekran kartını onun üzerinden okur. Önce nvidia.com'dan sürücüyü kurun, sonra bu kurulumu yeniden çalıştırın.

[Types]
Name: "standard"; Description: "{cm:TypeStandard}"
Name: "custom"; Description: "{cm:TypeCustom}"; Flags: iscustom

; The application is fixed. Under "Dependencies" sit the things it can use
; but does not need: leave one out and the feature it serves reads "--".
; The application offers the same ones later when missing or behind.
[Components]
Name: "app"; Description: "{cm:CompApp}"; Types: standard custom; Flags: fixed
Name: "deps"; Description: "{cm:CompDeps}"; Types: standard custom
Name: "deps\pawnio"; Description: "{cm:CompPawnIO}"; Types: standard; Check: not PawnIOInstalled

[Tasks]
Name: "startup"; Description: "{cm:StartWithWindows}"; Flags: checkedonce; Check: not Repairing

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
var
  DownloadPage: TDownloadWizardPage;

function PawnIOInstalled(): Boolean;
var
  V: String;
begin
  Result := RegQueryStringValue(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\PawnIO', 'DisplayVersion', V);
end;

function OnDownloadProgress(const Url, FileName: String; const Progress, ProgressMax: Int64): Boolean;
begin
  if Progress = ProgressMax then Log(Format('Downloaded %s', [FileName]));
  Result := True;
end;

procedure InitializeWizard;
begin
  DownloadPage := CreateDownloadPage(SetupMessage(msgWizardPreparing), SetupMessage(msgPreparingDesc), @OnDownloadProgress);
end;

// The signature is checked the way the application checks it: Authenticode
// valid, chain built, signed by PawnIO's author. PowerShell does the work;
// a non-zero exit refuses the file.
function PawnIOSignatureIsTrusted(const File: String): Boolean;
var
  Code: Integer;
  Cmd: String;
begin
  Cmd := '-NoProfile -NonInteractive -Command "$s = Get-AuthenticodeSignature ''' + File + '''; ' +
         'if ($s.Status -eq ''Valid'' -and $s.SignerCertificate.Subject -like ''*CN=namazso.eu*'') { exit 0 } else { exit 1 }"';
  Result := Exec('powershell.exe', Cmd, '', SW_HIDE, ewWaitUntilTerminated, Code) and (Code = 0);
end;

// After the application is in place: fetch and install what was ticked.
procedure CurStepChanged(CurStep: TSetupStep);
var
  File: String;
  Code: Integer;
begin
  if (CurStep = ssPostInstall) and WizardIsComponentSelected('deps\pawnio') and (not PawnIOInstalled) then
  begin
    DownloadPage.Clear;
    DownloadPage.Add('https://github.com/namazso/PawnIO.Setup/releases/latest/download/PawnIO_setup.exe', 'PawnIO_setup.exe', '');
    DownloadPage.Show;
    try
      try
        DownloadPage.Download;
        File := ExpandConstant('{tmp}\PawnIO_setup.exe');
        if not PawnIOSignatureIsTrusted(File) then
        begin
          DeleteFile(File);
          MsgBox(FmtMessage(CustomMessage('PawnIOFailed'), [CustomMessage('PawnIOBadSignature')]), mbError, MB_OK);
        end
        else if not (Exec(File, '-install -silent', '', SW_HIDE, ewWaitUntilTerminated, Code) and ((Code = 0) or (Code = 183))) then
          MsgBox(FmtMessage(CustomMessage('PawnIOFailed'), ['exit ' + IntToStr(Code)]), mbError, MB_OK);
      except
        MsgBox(FmtMessage(CustomMessage('PawnIOFailed'), [GetExceptionMessage]), mbError, MB_OK);
      end;
    finally
      DownloadPage.Hide;
    end;
  end;
end;

function StartupChoice(Param: String): String;
begin
  if Repairing then Result := 'keep'
  else if WizardIsTaskSelected('startup') then Result := '1'
  else Result := '0';
end;

procedure CurPageChanged(CurPageID: Integer);
begin
  if Repairing and (CurPageID = wpSelectDir) then
    WizardForm.DirEdit.Text := RepairDir;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  // A repair keeps the folder; the components page still shows so a
  // dependency left out the first time can be added.
  Result := Repairing and (PageID = wpSelectDir);
end;

// One copy per machine. Velopack registers the installed copy under this key;
// a second install elsewhere would take over the entry and orphan the first
// folder, and two versions side by side is exactly what the owner ruled out.
// The installed copy updates itself; there is nothing for a second Setup to do.
// nvml.dll comes with the NVIDIA display driver and cannot be installed on
// its own; without it the graphics card cannot be read. The owner's rule:
// no driver, no install.
function NvidiaDriverPresent(): Boolean;
begin
  Result := FileExists(ExpandConstant('{sys}\nvml.dll'))
         or FileExists(ExpandConstant('{commonpf}\NVIDIA Corporation\NVSMI\nvml.dll'));
end;

var
  RepairDir: String;

function Repairing(): Boolean;
begin
  Result := RepairDir <> '';
end;

function InitializeSetup(): Boolean;
var
  Where, Version: String;
begin
  Result := True;
  RepairDir := '';
  // /skipnvidia=1 is for trying the wizard in a virtual machine, which has
  // no NVIDIA driver and never will; nothing else should pass it.
  if (not NvidiaDriverPresent) and (ExpandConstant('{param:skipnvidia|0}') <> '1') then
  begin
    MsgBox(CustomMessage('NoNvidiaDriver'), mbError, MB_OK);
    Result := False;
    exit;
  end;
  if RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'InstallLocation', Where) then
  begin
    RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'DisplayVersion', Version);
    if MsgBox(FmtMessage(CustomMessage('AlreadyInstalledRepair'), [Version, Where]), mbConfirmation, MB_YESNO) = IDYES then
      RepairDir := Where
    else
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
