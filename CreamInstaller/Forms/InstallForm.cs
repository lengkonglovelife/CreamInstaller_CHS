using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreamInstaller.Components;
using CreamInstaller.Resources;
using CreamInstaller.Utility;
using static CreamInstaller.Platforms.Paradox.ParadoxLauncher;
using static CreamInstaller.Resources.Resources;

namespace CreamInstaller.Forms;

internal sealed partial class InstallForm : CustomForm
{
    private readonly HashSet<Selection> activeSelections = new();
    private readonly bool uninstalling;
    private int completeOperationsCount;
    private int operationsCount;
    internal bool Reselecting;
    private int selectionCount;

    internal InstallForm(bool uninstall = false)
    {
        InitializeComponent();
        Text = Program.ApplicationName;
        logTextBox.BackColor = LogTextBox.Background;
        uninstalling = uninstall;
    }

    private void UpdateProgress(int progress)
    {
        if (!userProgressBar.Disposing && !userProgressBar.IsDisposed)
            Invoke(() =>
            {
                int value = (int)((float)completeOperationsCount / operationsCount * 100) + progress / operationsCount;
                if (value < userProgressBar.Value)
                    return;
                userProgressBar.Value = value;
            });
    }

    internal void UpdateUser(string text, Color color, bool info = true, bool log = true)
    {
        if (info)
            _ = Invoke(() => userInfoLabel.Text = text);
        if (log && !logTextBox.Disposing && !logTextBox.IsDisposed)
            Invoke(() =>
            {
                if (logTextBox.Text.Length > 0)
                    logTextBox.AppendText(Environment.NewLine, color);
                logTextBox.AppendText(text, color);
                logTextBox.Invalidate();
            });
    }

    private async Task OperateFor(Selection selection)
    {
        UpdateProgress(0);
        if (selection.Id == "PL")
        {
            UpdateUser("Repairing Paradox Launcher . . . ", LogTextBox.Operation);
            _ = await Repair(this, selection);
        }

        bool useKoaloader = selection.UseProxy && (Program.UseSmokeAPI || selection.Platform is not Platform.Steam);
        bool useCreamApiProxy = selection.UseProxy && !Program.UseSmokeAPI &&
                                (selection.Platform is Platform.Steam || selection.Platform is Platform.Paradox &&
                                    selection.ExtraSelections.Any(s => s.Platform is Platform.Steam));

        UpdateUser(
            $"{(uninstalling ? "卸载" : "安装")}" + $" {(uninstalling ? "从" : "到")} " +
            selection.Name + $"目录 \"{selection.RootDirectory}\" . . . ", LogTextBox.Operation);
        IEnumerable<string> invalidDirectories = (await selection.RootDirectory.GetExecutables())
            ?.Where(d => selection.ExecutableDirectories.All(s => s.directory != Path.GetDirectoryName(d.path)))
            .Select(d => Path.GetDirectoryName(d.path));
        if (selection.ExecutableDirectories.All(s => s.directory != selection.RootDirectory))
            invalidDirectories = invalidDirectories?.Append(selection.RootDirectory);
        invalidDirectories = invalidDirectories?.Distinct();
        if (invalidDirectories is not null)
            foreach (string directory in invalidDirectories)
            {
                if (Program.Canceled)
                    return;

                directory.GetKoaloaderComponents(out string old_config, out string config);
                if (directory.GetKoaloaderProxies().Any(proxy =>
                        proxy.FileExists() && proxy.IsResourceFile(ResourceIdentifier.Koaloader))
                    || directory != selection.RootDirectory &&
                    Koaloader.AutoLoadDLLs.Any(pair => (directory + @"\" + pair.dll).FileExists())
                    || old_config.FileExists() || config.FileExists())
                {
                    UpdateUser(
                        "卸载 Koaloader从" + selection.Name +
                        $"错误目录 \"{directory}\" . . . ", LogTextBox.Operation);
                    await Koaloader.Uninstall(directory, selection.RootDirectory, this);
                }

                directory.GetCreamApiComponents(out _, out _, out _, out _, out config);
                if (directory.GetCreamApiProxies().Any(proxy =>
                        proxy.FileExists() && (proxy.IsResourceFile(ResourceIdentifier.Steamworks32) ||
                                               proxy.IsResourceFile(ResourceIdentifier.Steamworks64))))
                {
                    UpdateUser(
                        "卸载CreamApi代理" + selection.Name +
                        $"错误目录 \"{directory}\" . . . ", LogTextBox.Operation);
                    await CreamAPI.ProxyUninstall(directory, this);
                }
            }

        if (uninstalling || !useKoaloader || !useCreamApiProxy)
            foreach ((string directory, _) in selection.ExecutableDirectories)
            {
                if (Program.Canceled)
                    return;

                if (uninstalling || !useKoaloader)
                {
                    directory.GetKoaloaderComponents(out string old_config, out string config);
                    if (directory.GetKoaloaderProxies().Any(proxy =>
                            proxy.FileExists() && proxy.IsResourceFile(ResourceIdentifier.Koaloader))
                        || Koaloader.AutoLoadDLLs.Any(pair => (directory + @"\" + pair.dll).FileExists()) ||
                        old_config.FileExists() || config.FileExists())
                    {
                        UpdateUser(
                            "卸载Koaloader从 " + selection.Name + $"目录 \"{directory}\" . . . ",
                            LogTextBox.Operation);
                        await Koaloader.Uninstall(directory, selection.RootDirectory, this);
                    }
                }

                if (uninstalling || !useCreamApiProxy)
                {
                    directory.GetCreamApiComponents(out _, out _, out _, out _, out string config);
                    if (directory.GetCreamApiProxies().Any(proxy =>
                            proxy.FileExists() && (proxy.IsResourceFile(ResourceIdentifier.Steamworks32) ||
                                                   proxy.IsResourceFile(ResourceIdentifier.Steamworks64))) ||
                        config.FileExists())
                    {
                        UpdateUser(
                            "卸载CreamApi代理 " + selection.Name +
                            $"目录 \"{directory}\" . . . ", LogTextBox.Operation);
                        await CreamAPI.ProxyUninstall(directory, this);
                    }
                }
            }

        bool uninstallingForProxy = uninstalling || useKoaloader || useCreamApiProxy;
        int count = selection.DllDirectories.Count, cur = 0;
        foreach (string directory in selection.DllDirectories)
        {
            if (Program.Canceled)
                return;

            if (selection.Platform is Platform.Steam or Platform.Paradox)
            {
                if (Program.UseSmokeAPI)
                {
                    directory.GetSmokeApiComponents(out string api32, out string api32_o, out string api64,
                        out string api64_o, out string old_config,
                        out string config, out string old_log, out string log, out string cache);
                    if (uninstallingForProxy
                            ? api32_o.FileExists() || api64_o.FileExists() || old_config.FileExists() ||
                              config.FileExists() || old_log.FileExists() || log.FileExists()
                              || cache.FileExists()
                            : api32.FileExists() || api64.FileExists())
                    {
                        UpdateUser(
                            $"{(uninstallingForProxy ? "卸载" : "安装")} SmokeAPI" +
                            $" {(uninstallingForProxy ? "从" : "到")} " + selection.Name
                            + $"目录 \"{directory}\" . . . ", LogTextBox.Operation);
                        if (uninstallingForProxy)
                            await SmokeAPI.Uninstall(directory, this);
                        else
                            await SmokeAPI.Install(directory, selection, this);
                    }
                }
                else
                {
                    directory.GetCreamApiComponents(out string api32, out string api32_o, out string api64,
                        out string api64_o, out string config);
                    if (uninstallingForProxy
                            ? api32_o.FileExists() || api64_o.FileExists() || config.FileExists()
                            : api32.FileExists() || api64.FileExists())
                    {
                        UpdateUser(
                            $"{(uninstallingForProxy ? "卸载" : "安装")} CreamAPI" +
                            $" {(uninstallingForProxy ? "从" : "到")} " + selection.Name
                            + $"目录 \"{directory}\" . . . ", LogTextBox.Operation);
                        if (uninstallingForProxy)
                            await CreamAPI.Uninstall(directory, this);
                        else
                            await CreamAPI.Install(directory, selection, this);
                    }
                }
            }

            if (selection.Platform is Platform.Epic or Platform.Paradox)
            {
                directory.GetScreamApiComponents(out string api32, out string api32_o, out string api64,
                    out string api64_o, out string config, out string log);
                if (uninstallingForProxy
                        ? api32_o.FileExists() || api64_o.FileExists() || config.FileExists() || log.FileExists()
                        : api32.FileExists() || api64.FileExists())
                {
                    UpdateUser(
                        $"{(uninstallingForProxy ? "卸载" : "安装")} ScreamAPI" +
                        $" {(uninstallingForProxy ? "从" : "到")} " + selection.Name
                        + $"目录 \"{directory}\" . . . ", LogTextBox.Operation);
                    if (uninstallingForProxy)
                        await ScreamAPI.Uninstall(directory, this);
                    else
                        await ScreamAPI.Install(directory, selection, this);
                }
            }

            if (selection.Platform is Platform.Ubisoft)
            {
                directory.GetUplayR1Components(out string api32, out string api32_o, out string api64,
                    out string api64_o, out string config, out string log);
                if (uninstallingForProxy
                        ? api32_o.FileExists() || api64_o.FileExists() || config.FileExists() || log.FileExists()
                        : api32.FileExists() || api64.FileExists())
                {
                    UpdateUser(
                        $"{(uninstallingForProxy ? "卸载" : "安装")} Uplay R1 Unlocker" +
                        $" {(uninstallingForProxy ? "从" : "到")} " + selection.Name
                        + $"目录 \"{directory}\" . . . ", LogTextBox.Operation);
                    if (uninstallingForProxy)
                        await UplayR1.Uninstall(directory, this);
                    else
                        await UplayR1.Install(directory, selection, this);
                }

                directory.GetUplayR2Components(out string old_api32, out string old_api64, out api32, out api32_o,
                    out api64, out api64_o, out config, out log);
                if (uninstallingForProxy
                        ? api32_o.FileExists() || api64_o.FileExists() || config.FileExists() || log.FileExists()
                        : old_api32.FileExists() || old_api64.FileExists() || api32.FileExists() || api64.FileExists())
                {
                    UpdateUser(
                        $"{(uninstallingForProxy ? "卸载" : "安装")} Uplay R2 Unlocker" +
                        $" {(uninstallingForProxy ? "从" : "到")} " + selection.Name
                        + $"目录 \"{directory}\" . . . ", LogTextBox.Operation);
                    if (uninstallingForProxy)
                        await UplayR2.Uninstall(directory, this);
                    else
                        await UplayR2.Install(directory, selection, this);
                }
            }

            UpdateProgress(++cur / count * 100);
        }

        if ((useCreamApiProxy || useKoaloader) && !uninstalling)
            foreach ((string directory, BinaryType binaryType) in selection.ExecutableDirectories)
            {
                if (Program.Canceled)
                    return;

                if (useCreamApiProxy)
                {
                    UpdateUser(
                        "安装CreamApi代理到 " + selection.Name +
                        $"目录 \"{directory}\" . . . ",
                        LogTextBox.Operation);
                    await CreamAPI.ProxyInstall(directory, binaryType, selection, this);
                }
                else if (useKoaloader)
                {
                    UpdateUser("安装 Koaloader 到 " + selection.Name + $"目录 \"{directory}\" . . . ",
                        LogTextBox.Operation);
                    await Koaloader.Install(directory, binaryType, selection, selection.RootDirectory, this);
                }
            }

        UpdateProgress(100);
    }

    private async Task Operate()
    {
        operationsCount = activeSelections.Count;
        completeOperationsCount = 0;
        foreach (Selection selection in activeSelections)
        {
            if (Program.Canceled)
                throw new CustomMessageException("操作取消");
            try
            {
                await OperateFor(selection);
                if (Program.Canceled)
                    throw new CustomMessageException("操作取消");
                UpdateUser($"成功安装到 {selection.Name}.", LogTextBox.Success);
                _ = activeSelections.Remove(selection);
            }
            catch (Exception exception)
            {
                UpdateUser($"安装失败 {selection.Name}: " + exception, LogTextBox.Error);
            }

            ++completeOperationsCount;
        }

        Program.Cleanup();
        int activeCount = activeSelections.Count;
        if (activeCount > 0)
            if (activeCount == 1)
                throw new CustomMessageException($"操作失败原因 {activeSelections.First().Name}.");
            else
                throw new CustomMessageException($"操作失败原因 {activeCount} programs.");
    }

    private async void Start()
    {
        Program.Canceled = false;
        acceptButton.Enabled = false;
        retryButton.Enabled = false;
        cancelButton.Enabled = true;
        reselectButton.Enabled = false;
        userProgressBar.Value = userProgressBar.Minimum;
        try
        {
            await Operate();
            UpdateUser(
                $"成功{(uninstalling ? "卸载" : "安装")} " +
                 "",
                LogTextBox.Success);
        }
        catch (Exception exception)
        {
            UpdateUser(
                $"{(uninstalling ? "卸载" : "安装")} 失败: " +
                exception, LogTextBox.Error);
            retryButton.Enabled = true;
        }

        userProgressBar.Value = userProgressBar.Maximum;
        acceptButton.Enabled = true;
        cancelButton.Enabled = false;
        reselectButton.Enabled = true;
    }

    private void OnLoad(object sender, EventArgs a)
    {
        retry:
        try
        {
            userInfoLabel.Text = "加载中 . . . ";
            logTextBox.Text = string.Empty;
            selectionCount = 0;
            foreach (Selection selection in Selection.AllEnabled)
            {
                selectionCount++;
                _ = activeSelections.Add(selection);
            }

            Start();
        }
        catch (Exception e)
        {
            if (e.HandleException(this))
                goto retry;
            Close();
        }
    }

    private void OnAccept(object sender, EventArgs e)
    {
        Program.Cleanup();
        Close();
    }

    private void OnRetry(object sender, EventArgs e)
    {
        Program.Cleanup();
        Start();
    }

    private void OnCancel(object sender, EventArgs e) => Program.Cleanup();

    private void OnReselect(object sender, EventArgs e)
    {
        Program.Cleanup();
        Reselecting = true;
        Close();
    }
}