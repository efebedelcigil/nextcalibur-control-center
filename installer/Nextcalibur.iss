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
DefaultDirName={autopf}\Nextcalibur
DisableDirPage=no
DirExistsWarning=no
DisableProgramGroupPage=yes
; Elevated, and into Program Files: the application runs as administrator and
; is started without a prompt by a scheduled task, so the files it runs from
; must be somewhere only administrators can write - otherwise anything
; running as the account could replace the executable and be run elevated at
; the next start. Program Files is that place, and the elevated application
; updates itself there without trouble.
PrivilegesRequired=admin
; The application is x64 only, and the wizard must run in 64-bit mode: a
; 32-bit Setup sees SysWOW64 as {sys}, where there is no nvml.dll, and
; Program Files (x86) as {autopf}. Found on the real machine, 12 September
; 2026, as "NVIDIA driver not found" on a laptop with the driver installed.
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
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
tr.WelcomeLabel2=Bu sihirbaz [name/ver] uygulamasını bilgisayarınıza kuracak.%n%nNextcalibur, Casper Excalibur Control Center'ın yerini alır: sıcaklıklar, fanlar, klavye aydınlatması, güç modları ve grafik modu, bilgisayarın kendi donanım yazılımı arayüzünden. Kendi sürücüsü yoktur; Casper'ın yazılımı gibi yönetici olarak çalışır ve bir kez sorduktan sonra bir daha sormaz.
tr.SelectDirDesc=[name] nereye kurulsun?
tr.SelectDirLabel3=Kurulum [name] uygulamasını aşağıdaki klasöre kuracak. Program Files önerilir: uygulama yönetici olarak başlatıldığı için dosyaları yalnızca yöneticilerin yazabildiği bir yerde durmalıdır.
tr.FinishedLabelNoIcons=Kurulum [name] uygulamasını bilgisayarınıza kurdu.
tr.FinishedLabel=Kurulum [name] uygulamasını bilgisayarınıza kurdu. Windows ile birlikte başlar ve bildirim alanında yaşar; pencere bir çift tıklama uzağınızda.
en.WelcomeLabel2=This will install [name/ver] on your computer.%n%nNextcalibur replaces the Casper Excalibur Control Center: temperatures, fans, keyboard lighting, power modes and graphics mode, from the laptop's own firmware interface. It installs no driver of its own; like Casper's software it runs as administrator, asking once and not again.
en.SelectDirDesc=Where should [name] be installed?
en.SelectDirLabel3=Setup will install [name] into the following folder. Program Files is recommended: the application is started as administrator, so its files should sit where only administrators can write.
en.FinishedLabelNoIcons=Setup has finished installing [name] on your computer.
en.FinishedLabel=Setup has finished installing [name] on your computer. It starts with Windows and lives in the notification area; the window is a double-click away.

[CustomMessages]
en.AlreadyInstalled=Nextcalibur %1 is already installed in%n%2%n%nOnly one copy can be installed. It updates itself - open it and use "Check for updates now" in the tray menu. To move it, remove it first from Settings > Apps.
tr.AlreadyInstalled=Nextcalibur %1 zaten şurada kurulu:%n%2%n%nYalnızca bir kopya kurulabilir. Kendini günceller - açın ve bildirim alanı menüsünden "Check for updates now" seçin. Taşımak için önce Ayarlar > Uygulamalar'dan kaldırın.
en.CannotWrite=Nextcalibur cannot write to that folder. Choose another.
tr.CannotWrite=Nextcalibur bu klasöre yazamıyor. Başka bir klasör seçin.
en.MustBeProgramFiles=Nextcalibur has to be installed under Program Files.%n%nIt is started as administrator without a prompt, so its files must sit where only administrators can write - otherwise anything running as an ordinary account could replace them. Choose a folder under%n%1
tr.MustBeProgramFiles=Nextcalibur, Program Files altına kurulmalıdır.%n%nUygulama sorulmadan yönetici olarak başlatılır; bu yüzden dosyaları yalnızca yöneticilerin yazabildiği bir yerde durmalıdır - aksi hâlde sıradan bir hesapla çalışan herhangi bir şey onları değiştirebilir. Şunun altında bir klasör seçin:%n%1
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
en.CompDeps=Dependencies
tr.CompDeps=Bağımlılıklar
en.CompDotNet=.NET 8 desktop runtime - downloaded from Microsoft, about 60 MB
tr.CompDotNet=.NET 8 masaüstü çalışma zamanı - Microsoft'tan indirilir, yaklaşık 60 MB
en.CompDotNetPresent=.NET 8 desktop runtime - already installed
tr.CompDotNetPresent=.NET 8 masaüstü çalışma zamanı - zaten kurulu
en.CompPawnIO=PawnIO driver - reads the processor's power (signed, open source, pawnio.eu)
tr.CompPawnIO=PawnIO sürücüsü - işlemcinin güç tüketimini okur (imzalı, açık kaynak, pawnio.eu)
en.CompPawnIOPresent=PawnIO driver - already installed
tr.CompPawnIOPresent=PawnIO sürücüsü - zaten kurulu
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
en.DownloadingRuntime=Downloading the .NET 8 desktop runtime from Microsoft...
tr.DownloadingRuntime=.NET 8 masaüstü çalışma zamanı Microsoft'tan indiriliyor...
en.RuntimeFailed=The .NET 8 desktop runtime could not be installed (%1).%n%nNextcalibur runs on it and will not start without it. Install it from%nhttps://dotnet.microsoft.com/download/dotnet/8.0 (Desktop Runtime, x64) and start Nextcalibur again.
tr.RuntimeFailed=.NET 8 masaüstü çalışma zamanı kurulamadı (%1).%n%nNextcalibur bunun üzerinde çalışır ve o olmadan başlamaz. Şu adresten kurun:%nhttps://dotnet.microsoft.com/download/dotnet/8.0 (Desktop Runtime, x64) ve Nextcalibur'u yeniden başlatın.
en.RuntimeBadSignature=the download is not signed by Microsoft
tr.RuntimeBadSignature=indirilen dosya Microsoft imzalı değil
en.NoNvidiaDriver=The NVIDIA graphics driver was not found (nvml.dll). Nextcalibur reads the graphics card through it. Install the driver from nvidia.com first, then run this setup again.
tr.NoNvidiaDriver=NVIDIA grafik sürücüsü bulunamadı (nvml.dll). Nextcalibur ekran kartını onun üzerinden okur. Önce nvidia.com'dan sürücüyü kurun, sonra bu kurulumu yeniden çalıştırın.

[Types]
Name: "standard"; Description: "{cm:TypeStandard}"
Name: "custom"; Description: "{cm:TypeCustom}"; Flags: iscustom

; The application is fixed. Under "Dependencies" sit the runtime and whatever
; other dependencies the application can use: missing dependencies are installed
; or offered, while already installed ones are shown as present.
[Components]
Name: "app"; Description: "{cm:CompApp}"; Types: standard custom; Flags: fixed
Name: "deps"; Description: "{cm:CompDeps}"; Types: standard custom
Name: "deps\dotnet"; Description: "{cm:CompDotNet}"; Types: standard custom; Flags: fixed; Check: not DesktopRuntimeInstalled
Name: "deps\dotnet_present"; Description: "{cm:CompDotNetPresent}"; Types: standard custom; Flags: fixed; Check: DesktopRuntimeInstalled
Name: "deps\pawnio"; Description: "{cm:CompPawnIO}"; Types: standard; Check: not PawnIOInstalled
Name: "deps\pawnio_present"; Description: "{cm:CompPawnIOPresent}"; Flags: fixed; Check: PawnIOInstalled

[Tasks]
Name: "startup"; Description: "{cm:StartWithWindows}"; Flags: checkedonce; Check: not Repairing

[Files]
; The engine, carried inside and run once.
Source: "..\releases\Nextcalibur-win-Setup.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

[Registry]
; Windows Add/Remove Programs (Installed Apps / Control Panel) registration.
; Under Program Files (machine-wide), Windows expects the uninstaller in HKLM.
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "DisplayName"; ValueData: "{#AppName}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "DisplayVersion"; ValueData: "{#AppVersion}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "Publisher"; ValueData: "{#Publisher}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "DisplayIcon"; ValueData: "{app}\current\Nextcalibur.exe,0"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "InstallLocation"; ValueData: "{app}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "UninstallString"; ValueData: """{app}\Update.exe"" uninstall"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "QuietUninstallString"; ValueData: """{app}\Update.exe"" uninstall -s"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "URLInfoAbout"; ValueData: "{#Url}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: string; ValueName: "HelpLink"; ValueData: "{#Url}/issues"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: dword; ValueName: "NoModify"; ValueData: 1; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: dword; ValueName: "NoRepair"; ValueData: 1; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur"; ValueType: dword; ValueName: "EstimatedSize"; ValueData: 35000; Flags: uninsdeletekey

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
  RepairDir: String;

function Repairing(): Boolean;
begin
  Result := RepairDir <> '';
end;

function PawnIOInstalled(): Boolean;
var
  V: String;
begin
  Result := RegQueryStringValue(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\PawnIO', 'DisplayVersion', V);
end;

// The .NET desktop runtime the application runs on. Framework-dependent
// since 13 September 2026: a runtime carried inside the package is one no
// machine ever patches, and 0.5.3 shipped one that was five days behind
// five security fixes. Velopack's own runtime bootstrap is not used - it
// was watched failing silently on a clean Windows, which is why the
// runtime was carried in the first place - so the wizard fetches it the
// same way it fetches PawnIO: Microsoft's installer, signature checked,
// run quietly, from a folder only administrators can write.
function DesktopRuntimeInstalled(): Boolean;
var
  Folder: String;
  Search: TFindRec;
  Major: Integer;
begin
  Result := False;
  Folder := ExpandConstant('{commonpf}\dotnet\shared\Microsoft.WindowsDesktop.App');
  if not DirExists(Folder) then Exit;
  if FindFirst(Folder + '\*', Search) then
  begin
    try
      repeat
        if (Search.Attributes and FILE_ATTRIBUTE_DIRECTORY) <> 0 then
        begin
          Major := StrToIntDef(Copy(Search.Name, 1, Pos('.', Search.Name + '.') - 1), 0);
          if Major >= 8 then
          begin
            Result := True;
            Exit;
          end;
        end;
      until not FindNext(Search);
    finally
      FindClose(Search);
    end;
  end;
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
// valid (hash, signature, chain, revocation), signed by PawnIO's author -
// the whole subject component, not a substring. PowerShell does the work,
// from its own folder; a non-zero exit refuses the file.
function SignatureIsTrusted(const File, SignerPattern: String): Boolean;
var
  Code: Integer;
  Cmd: String;
begin
  Cmd := '-NoProfile -NonInteractive -Command "$s = Get-AuthenticodeSignature ''' + File + '''; ' +
         'if ($s.Status -eq ''Valid'' -and $s.SignerCertificate.Subject -match ''' + SignerPattern + ''') { exit 0 } else { exit 1 }"';
  Result := Exec(ExpandConstant('{sys}\WindowsPowerShell\v1.0\powershell.exe'), Cmd, '', SW_HIDE, ewWaitUntilTerminated, Code) and (Code = 0);
end;

function PawnIOSignatureIsTrusted(const File: String): Boolean;
begin
  Result := SignatureIsTrusted(File, '(^|, )CN=namazso\.eu(,|$)');
end;

// Microsoft signs with a name that has moved over the years; the constant
// part is the organisation, and the chain is what Get-AuthenticodeSignature
// has already checked by saying Valid.
function MicrosoftSignatureIsTrusted(const File: String): Boolean;
begin
  Result := SignatureIsTrusted(File, '(^|, )O=Microsoft Corporation(,|$)');
end;

// Fetches and installs the runtime. True when the machine has it afterwards.
function InstallDesktopRuntime(): Boolean;
var
  File: String;
  Code: Integer;
begin
  Result := True;
  if DesktopRuntimeInstalled() then Exit;

  Result := False;
  DownloadPage.Clear;
  // Microsoft's evergreen link for the channel: it redirects to the newest
  // patch, so a wizard built months ago still installs a current runtime.
  // The application takes it from there, patch by patch, afterwards.
  DownloadPage.Add('https://aka.ms/dotnet/8.0/windowsdesktop-runtime-win-x64.exe', 'windowsdesktop-runtime-win-x64.exe', '');
  DownloadPage.SetText(CustomMessage('DownloadingRuntime'), '');
  DownloadPage.Show;
  try
    try
      DownloadPage.Download;
      // Checked and run from {app}: only administrators can write there,
      // so nothing can swap the file between the check and the run.
      File := ExpandConstant('{app}\windowsdesktop-runtime-win-x64.exe');
      if not FileCopy(ExpandConstant('{tmp}\windowsdesktop-runtime-win-x64.exe'), File, False) then
        MsgBox(FmtMessage(CustomMessage('RuntimeFailed'), ['copy']), mbError, MB_OK)
      else if not MicrosoftSignatureIsTrusted(File) then
        MsgBox(FmtMessage(CustomMessage('RuntimeFailed'), [CustomMessage('RuntimeBadSignature')]), mbError, MB_OK)
      else if not (Exec(File, '/install /quiet /norestart', '', SW_HIDE, ewWaitUntilTerminated, Code) and ((Code = 0) or (Code = 3010) or (Code = 1638))) then
        MsgBox(FmtMessage(CustomMessage('RuntimeFailed'), ['exit ' + IntToStr(Code)]), mbError, MB_OK)
      else
        Result := True;
      DeleteFile(File);
      DeleteFile(ExpandConstant('{tmp}\windowsdesktop-runtime-win-x64.exe'));
    except
      MsgBox(FmtMessage(CustomMessage('RuntimeFailed'), [GetExceptionMessage]), mbError, MB_OK);
    end;
  finally
    DownloadPage.Hide;
  end;
end;

// After the application is in place: fetch and install what was ticked.
procedure CurStepChanged(CurStep: TSetupStep);
var
  File: String;
  Code: Integer;
begin
  // First, because the application does not start without it. This runs
  // before the [Run] entries, so the runtime is in place before Velopack
  // unpacks the application and before the finish page offers to start it.
  if CurStep = ssPostInstall then
    InstallDesktopRuntime();

  if (CurStep = ssPostInstall) and WizardIsComponentSelected('deps\pawnio') and (not PawnIOInstalled) then
  begin
    DownloadPage.Clear;
    DownloadPage.Add('https://github.com/namazso/PawnIO.Setup/releases/latest/download/PawnIO_setup.exe', 'PawnIO_setup.exe', '');
    DownloadPage.Show;
    try
      try
        DownloadPage.Download;
        // Checked and run from {app}, which only administrators can write,
        // not from {tmp}, which is the account's: nothing running as the
        // account can swap the file between the check and the run there.
        File := ExpandConstant('{app}\PawnIO_setup.exe');
        if not FileCopy(ExpandConstant('{tmp}\PawnIO_setup.exe'), File, False) then
          MsgBox(FmtMessage(CustomMessage('PawnIOFailed'), ['copy']), mbError, MB_OK)
        else if not PawnIOSignatureIsTrusted(File) then
          MsgBox(FmtMessage(CustomMessage('PawnIOFailed'), [CustomMessage('PawnIOBadSignature')]), mbError, MB_OK)
        else if not (Exec(File, '-install -silent', '', SW_HIDE, ewWaitUntilTerminated, Code) and ((Code = 0) or (Code = 183))) then
          MsgBox(FmtMessage(CustomMessage('PawnIOFailed'), ['exit ' + IntToStr(Code)]), mbError, MB_OK);
        DeleteFile(File);
        DeleteFile(ExpandConstant('{tmp}\PawnIO_setup.exe'));
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
  Where := '';
  if not RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'InstallLocation', Where) then
    RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'InstallLocation', Where);

  if Where <> '' then
  begin
    if not RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'DisplayVersion', Version) then
      RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\Nextcalibur', 'DisplayVersion', Version);
    if MsgBox(FmtMessage(CustomMessage('AlreadyInstalledRepair'), [Version, Where]), mbConfirmation, MB_YESNO) = IDYES then
      RepairDir := Where
    else
      Result := False;
  end;
end;

// Under Program Files only. The application is started elevated without a
// prompt by a scheduled task, so the folder it runs from must be one an
// ordinary account cannot write to; Program Files is that folder, and any
// subfolder of it will do.
function UnderProgramFiles(const Dir: String): Boolean;
var
  D, PF, PF32: String;
begin
  D := AddBackslash(Lowercase(Dir));
  PF := AddBackslash(Lowercase(ExpandConstant('{commonpf}')));
  PF32 := AddBackslash(Lowercase(ExpandConstant('{commonpf32}')));
  Result := (Pos(PF, D) = 1) or (Pos(PF32, D) = 1);
end;

// A folder Setup cannot write to is no place to install; say so before the
// wizard goes on.
function NextButtonClick(CurPageID: Integer): Boolean;
var
  Dir, Probe: String;
begin
  Result := True;
  if CurPageID = wpSelectDir then
  begin
    Dir := WizardDirValue;
    if not UnderProgramFiles(Dir) then
    begin
      MsgBox(FmtMessage(CustomMessage('MustBeProgramFiles'), [ExpandConstant('{commonpf}')]), mbError, MB_OK);
      Result := False;
      exit;
    end;
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
