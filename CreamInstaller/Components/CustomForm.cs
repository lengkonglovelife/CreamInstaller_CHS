using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CreamInstaller.Forms;
using CreamInstaller.Utility;

namespace CreamInstaller.Components;

internal class CustomForm : Form
{
    internal CustomForm()
    {
        Icon = Properties.Resources.Icon;
        KeyPreview = true;
        KeyPress += OnKeyPress;
        ResizeRedraw = true;
        HelpButton = true;
        HelpButtonClicked += OnHelpButtonClicked;
    }

    internal CustomForm(IWin32Window owner) : this()
    {
        if (owner is not Form form)
            return;
        Owner = form;
        InheritLocation(form);
        SizeChanged += (_, _) => InheritLocation(form);
        form.Activated += OnActivation;
        FormClosing += (_, _) => form.Activated -= OnActivation;
        TopLevel = true;
    }

    protected override CreateParams CreateParams // Double buffering for all controls
    {
        get
        {
            CreateParams handleParam = base.CreateParams;
            handleParam.ExStyle |= 0x02; // WS_EX_COMPOSITED       
            return handleParam;
        }
    }

    private void OnHelpButtonClicked(object sender, EventArgs args)
    {
        using DialogForm helpDialog = new(this);
        helpDialog.HelpButton = false;
        const string acidicoala = "https://github.com/acidicoala";
        string repository = $"https://github.com/{Program.RepositoryOwner}/{Program.RepositoryName}";
        _ = helpDialog.Show(SystemIcons.Information,
            "自动遍历所有已安装的Steam、Epic和Ubisoft的游戏与游戏对应DLC,相关DLL位置,\n"
            + "解析SteamCMD、Steam Store和Epic Games Store以获取选择的游戏的DLC，然后提供交互菜单\n"
            + "为了维护CreamInstaller而有效使用解析信息\n\n"
            + $"程序使用最新的[Koaloader]({acidicoala}/Koaloader)、[SmokeAPI]({acidicoala}/SmokeAPI)、[ScreamAPI]({acidicoala}/ScreamAPI)、[Uplay R1 Unlocker]({acidicoala}/ UplayR1Unlocker) 和 [Uplay R2 Unlocker]({acidicoala}/UplayR2Unlocker)，全部由[acidicoala]({acidicoala})\n"
            + $"全部嵌入到软件里;您无需下载已经调用的DLC解锁项目！\n\n"
            + "如何使用:\n" + "    1. 用软件遍历电脑寻找可以解锁的游戏和DLC。\n"
            + "            本软件自动从 Steam、Epic 和 Ubisoft 目录遍历所有已安装的游戏.\n"
            + "    2. 等待自动下载安装SteamCMD(选择的是Steam游戏的时候).\n"
            + "    3. 等待软件解析并缓存所选游戏的信息和 DLC。\n"
            + "             第一次运行可能会加载一段时间,时间取决于选择的游戏DLC数量。\n"
            + "    4. 选择要解锁的游戏的DLC。\n"
            + "            软件没有对每款游戏进行测试，所以你要自行尝试。\n"
            + "    5. 是否勾选Koaloader加载,这个取决于你，如果勾选了,还要选择DLL代理。\n"
            + "           如果默认的“version.dll”不办事，请看这个帖子[此处](https://cs.rin.ru/forum/viewtopic.php?p=2552172#p2552172)找到一个可以用的。\n"
            + "    6.点击“安装”。\n" +
            "    7. 点击“确定”关闭软件。\n"
            + "    8. 如果CreamInstaller导致您安装的游戏出现问题，那就返回\n"
            + "        转到第5步，选择需要恢复的游戏，然后单击“卸载”。\n\n"
            + "注意：该软件不会自动下载或安装真实的DLC;正如软件标题所示，该项目\n"
            + "只是个DLC Unlucker安装程序。如果您想要解锁 DLC 的游戏尚未安装 DLC，就像\n"
            + "大多数游戏，您必须自己寻找、下载安装到游戏目录中。包括手动安装新的\n"
            + "DLC 以及在游戏更新后自己手动更新之前安装的 DLC。\n\n"
            + $"如果想获得可靠和快速的帮助，所有错误、崩溃和其他问题请看[GitHub Issues]({repository}/issues) 页面！\n\n"
            + $"但是：请阅读与您的问题相对应的[FAQ entry]({repository}#faq--common-issues) 和/或[template issue]({repository}/issues/new/choose)（如果存在）！另请注意 [GitHub Issues]({repository}/issues)\n"
            + "页面不是个人帮助热线，而是用于程序本身的错误/崩溃/问题等。如果您发布的问题是脑残问题你就可以死妈了\n"
            + "已经在 FAQ 中进行了解释，我将关闭你的issue,你的问题我会忽略。\n\n"
            + "SteamCMD 安装和 appinfo 缓存可以在 [C:\\ProgramData\\CreamInstaller]() 中找到。\n"
            + $"启动时会自动检测更新，并会从 [GitHub]({repository})下载文件进行更新。\n"
            + $"软件源码和其他调用可以在[GitHub]({repository})上找到。");
    }

    private void OnActivation(object sender, EventArgs args) => Activate();

    internal void BringToFrontWithoutActivation()
    {
        bool topMost = TopMost;
        NativeImports.SetWindowPos(Handle, NativeImports.HWND_TOPMOST, 0, 0, 0, 0,
            NativeImports.SWP_NOACTIVATE | NativeImports.SWP_SHOWWINDOW | NativeImports.SWP_NOMOVE |
            NativeImports.SWP_NOSIZE);
        if (!topMost)
            NativeImports.SetWindowPos(Handle, NativeImports.HWND_NOTOPMOST, 0, 0, 0, 0,
                NativeImports.SWP_NOACTIVATE | NativeImports.SWP_SHOWWINDOW | NativeImports.SWP_NOMOVE |
                NativeImports.SWP_NOSIZE);
    }

    internal void InheritLocation(Form fromForm)
    {
        if (fromForm is null)
            return;
        int X = fromForm.Location.X + fromForm.Size.Width / 2 - Size.Width / 2;
        int Y = fromForm.Location.Y + fromForm.Size.Height / 2 - Size.Height / 2;
        Location = new(X, Y);
    }

    private void OnKeyPress(object s, KeyPressEventArgs e)
    {
        if (e.KeyChar != 'S')
            return; // Shift + S
        UpdateBounds();
        Rectangle bounds = Bounds;
        using Bitmap bitmap = new(Size.Width - 14, Size.Height - 7);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        using EncoderParameters encoding = new(1);
        using EncoderParameter encoderParam = new(Encoder.Quality, 100L);
        encoding.Param[0] = encoderParam;
        graphics.CopyFromScreen(new(bounds.Left + 7, bounds.Top), Point.Empty, new(Size.Width - 14, Size.Height - 7));
        Clipboard.SetImage(bitmap);
        e.Handled = true;
    }
}