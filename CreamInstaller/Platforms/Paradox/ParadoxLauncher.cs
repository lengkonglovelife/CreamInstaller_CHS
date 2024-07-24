using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreamInstaller.Forms;
using CreamInstaller.Resources;
using CreamInstaller.Utility;
using Microsoft.Win32;
using static CreamInstaller.Resources.Resources;

namespace CreamInstaller.Platforms.Paradox;

internal static class ParadoxLauncher
{
    public enum RepairResult
    {
        Failure = -1,
        Unnecessary = 0,
        Success
    }

    private static string installPath;

    internal static string InstallPath
    {
        get
        {
            installPath ??= Registry.GetValue(@"HKEY_CURRENT_USER\Software\Paradox Interactive\Paradox Launcher v2",
                "安装启动器", null) as string;
            return installPath.ResolvePath();
        }
    }

    private static void PopulateDlc(Selection paradoxLauncher = null)
    {
        paradoxLauncher ??= Selection.FromId(Platform.Paradox, "PL");
        if (paradoxLauncher is null)
            return;
        paradoxLauncher.ExtraSelections.Clear();
        foreach (Selection selection in Selection.AllEnabled.Where(s =>
                     !s.Equals(paradoxLauncher) && s.Publisher == "Paradox Interactive"))
            _ = paradoxLauncher.ExtraSelections.Add(selection);
        if (paradoxLauncher.ExtraSelections.Count > 0)
            return;
        foreach (Selection selection in Selection.All.Keys.Where(s =>
                     !s.Equals(paradoxLauncher) && s.Publisher == "Paradox Interactive"))
            _ = paradoxLauncher.ExtraSelections.Add(selection);
    }

    internal static bool DlcDialog(Form form)
    {
        Selection paradoxLauncher = Selection.FromId(Platform.Paradox, "PL");
        if (paradoxLauncher is null || !paradoxLauncher.Enabled)
            return false;
        PopulateDlc(paradoxLauncher);
        if (paradoxLauncher.ExtraSelections.Count > 0)
            return false;
        using DialogForm dialogForm = new(form);
        return dialogForm.Show(SystemIcons.Warning,
            "警告: 没有DLC可以添加到 Paradox Launcher 进行解锁"
            + "\n\n只为Paradox Launcher安装DLC解锁工具可能会导致现在的设置被删除",
            "忽略", "取消",
            "Paradox Launcher") != DialogResult.OK;
    }

    internal static async Task<RepairResult> Repair(Form form, Selection selection)
    {
        InstallForm installForm = form as InstallForm;
        StringBuilder dialogText = null;
        if (installForm is null)
        {
            Program.Canceled = false;
            dialogText = new();
        }

        using DialogForm dialogForm = new(form);
        bool creamInstalled = false;
        byte[] steamOriginalSdk32 = null;
        byte[] steamOriginalSdk64 = null;
        bool screamInstalled = false;
        byte[] epicOriginalSdk32 = null;
        byte[] epicOriginalSdk64 = null;
        foreach (string directory in selection.DllDirectories.TakeWhile(_ => !Program.Canceled))
        {
            string api32;
            string api32_o;
            string api64;
            string api64_o;
            if (Program.UseSmokeAPI)
            {
                directory.GetSmokeApiComponents(out api32, out api32_o, out api64, out api64_o,
                    out _, out _, out _, out _, out _);
                creamInstalled = creamInstalled || api32_o.FileExists() || api64_o.FileExists()
                                 || api32.FileExists() && api32.IsResourceFile(ResourceIdentifier.Steamworks32)
                                 || api64.FileExists() && api64.IsResourceFile(ResourceIdentifier.Steamworks64);
                await SmokeAPI.Uninstall(directory, deleteOthers: false);
            }
            else
            {
                directory.GetCreamApiComponents(out api32, out api32_o, out api64, out api64_o, out _);
                creamInstalled = creamInstalled || api32_o.FileExists() || api64_o.FileExists()
                                 || api32.FileExists() && api32.IsResourceFile(ResourceIdentifier.Steamworks32)
                                 || api64.FileExists() && api64.IsResourceFile(ResourceIdentifier.Steamworks64);
                await CreamAPI.Uninstall(directory, deleteOthers: false);
            }

            if (steamOriginalSdk32 is null && api32.FileExists() &&
                !api32.IsResourceFile(ResourceIdentifier.Steamworks32))
                steamOriginalSdk32 = api32.ReadFileBytes(true);
            if (steamOriginalSdk64 is null && api64.FileExists() &&
                !api64.IsResourceFile(ResourceIdentifier.Steamworks64))
                steamOriginalSdk64 = api64.ReadFileBytes(true);
            directory.GetScreamApiComponents(out api32, out api32_o, out api64, out api64_o, out _, out _);
            screamInstalled = screamInstalled || api32_o.FileExists() || api64_o.FileExists()
                              || api32.FileExists() && api32.IsResourceFile(ResourceIdentifier.EpicOnlineServices32)
                              || api64.FileExists() && api64.IsResourceFile(ResourceIdentifier.EpicOnlineServices64);
            await ScreamAPI.Uninstall(directory, deleteOthers: false);
            if (epicOriginalSdk32 is null && api32.FileExists() &&
                !api32.IsResourceFile(ResourceIdentifier.EpicOnlineServices32))
                epicOriginalSdk32 = api32.ReadFileBytes(true);
            if (epicOriginalSdk64 is null && api64.FileExists() &&
                !api64.IsResourceFile(ResourceIdentifier.EpicOnlineServices64))
                epicOriginalSdk64 = api64.ReadFileBytes(true);
        }

        if (steamOriginalSdk32 is not null || steamOriginalSdk64 is not null || epicOriginalSdk32 is not null ||
            epicOriginalSdk64 is not null)
        {
            bool neededRepair = false;
            foreach (string directory in selection.DllDirectories.TakeWhile(_ => !Program.Canceled))
            {
                string api32;
                string api64;
                if (Program.UseSmokeAPI)
                    directory.GetSmokeApiComponents(out api32, out _, out api64, out _, out _, out _,
                        out _,
                        out _, out _);
                else
                    directory.GetCreamApiComponents(out api32, out _, out api64, out _, out _);

                if (steamOriginalSdk32 is not null && api32.IsResourceFile(ResourceIdentifier.Steamworks32))
                {
                    steamOriginalSdk32.WriteResource(api32);
                    if (installForm is not null)
                        installForm.UpdateUser("修改后的 Steamworks: " + api32, LogTextBox.Action);
                    else
                        dialogText.AppendLine("修改后的 Steamworks: " + api32);
                    neededRepair = true;
                }

                if (steamOriginalSdk64 is not null && api64.IsResourceFile(ResourceIdentifier.Steamworks64))
                {
                    steamOriginalSdk64.WriteResource(api64);
                    if (installForm is not null)
                        installForm.UpdateUser("修改后的 Steamworks: " + api64, LogTextBox.Action);
                    else
                        dialogText.AppendLine("修改后的 Steamworks: " + api64);
                    neededRepair = true;
                }

                if (creamInstalled)
                    if (Program.UseSmokeAPI)
                        await SmokeAPI.Install(directory, selection, generateConfig: false);
                    else
                        await CreamAPI.Install(directory, selection, generateConfig: false);

                directory.GetScreamApiComponents(out api32, out _, out api64, out _, out _, out _);
                if (epicOriginalSdk32 is not null && api32.IsResourceFile(ResourceIdentifier.EpicOnlineServices32))
                {
                    epicOriginalSdk32.WriteResource(api32);
                    if (installForm is not null)
                        installForm.UpdateUser("修改后的 Epic Online Services: " + api32, LogTextBox.Action);
                    else
                        dialogText.AppendLine("修改后的 Epic Online Services: " + api32);
                    neededRepair = true;
                }

                if (epicOriginalSdk64 is not null && api64.IsResourceFile(ResourceIdentifier.EpicOnlineServices64))
                {
                    epicOriginalSdk64.WriteResource(api64);
                    if (installForm is not null)
                        installForm.UpdateUser("修改后的 Epic Online Services: " + api64, LogTextBox.Action);
                    else
                        dialogText.AppendLine("修改后的 Epic Online Services: " + api64);
                    neededRepair = true;
                }

                if (screamInstalled)
                    await ScreamAPI.Install(directory, selection, generateConfig: false);
            }

            if (!Program.Canceled)
            {
                if (neededRepair)
                {
                    if (installForm is not null)
                        installForm.UpdateUser("Paradox Launcher 成功修补", LogTextBox.Action);
                    else
                    {
                        dialogText.AppendLine("\nParadox Launcher 成功修补");
                        _ = dialogForm.Show(form.Icon, dialogText.ToString(), customFormText: "Paradox Launcher");
                    }

                    return RepairResult.Success;
                }

                if (installForm is not null)
                    installForm.UpdateUser("Paradox Launcher 不需要修补", LogTextBox.Success);
                else
                    _ = dialogForm.Show(SystemIcons.Information, "Paradox Launcher 不需要修补",
                        customFormText: "Paradox Launcher");
                return RepairResult.Unnecessary;
            }
        }

        if (Program.Canceled)
        {
            _ = form is InstallForm
                ? throw new CustomMessageException("修补失败! 操作取消")
                : dialogForm.Show(SystemIcons.Error, "Paradox Launcher 修补失败! 操作取消",
                    customFormText: "Paradox Launcher");
            return RepairResult.Failure;
        }

        _ = form is InstallForm
            ? throw new CustomMessageException(
                "Repair failed! " + "无法找到原SteamWork和Epic Online Services  "
                                  + "重新安装 Paradox Launcher 可能解决此问题")
            : dialogForm.Show(SystemIcons.Error,
                "Paradox Launcher repair failed!" + "\n\n无法找到原SteamWork和Epic Online Services"
                                                  + "\n重新安装 Paradox Launcher 可能解决此问题",
                customFormText: "Paradox Launcher");
        return RepairResult.Failure;
    }
}