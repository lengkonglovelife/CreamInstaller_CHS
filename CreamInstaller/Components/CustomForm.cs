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
            "自动遍历Steam、Epic和Ubisoft的游戏DLC\n"
            + "解析SteamCMD、Steam Store和Epic Games Store获取游戏DLC\n"
            + "利用获取的信息解锁DLC\n\n"
            + $"应用使用最新的[CreamAPI](https://cs.rin.ru/forum/viewtopic.php?f=29&t=70576) by [deadmau5](https://cs.rin.ru/forum/viewtopic.php?f=29&t=70576). 最新的 [Koaloader]({acidicoala}/Koaloader), [ScreamAPI]({acidicoala}/ScreamAPI), [Uplay R1\n"
            + $"Unlocker]({acidicoala}/UplayR1Unlocker) 还有 [Uplay R2 Unlocker]({acidicoala}/UplayR2Unlocker), 全靠 [acidicoala]({acidicoala}). 上述工具已经全部集成到应用,无需额外下载\n"
            + ""
            + "如何使用:\n" + "    1. 选择游戏\n"
            + "            应用会自动遍历.\n"
            + "    2. 等待自动下载SteamWork (Steam游戏).\n"
            + "    3. 等待应用找到DLC信息\n"
            + "             第一次运行可能需要较久的时间\n"
            + "    4. 选择你需要解锁的DLC\n"
            + "           我们没有对全部游戏进行测试\n"
            + "    5. 选择时候使用Koaloader,如果是就选择代理Dll\n"
            + "            如果默认 \'version.dll\' 无法使用, 就打开[here](https://cs.rin.ru/forum/viewtopic.php?p=2552172#p2552172)寻找能用的\n"
            + "    6. 点击 \"安装\" \n"
            + "    7. 点击 \"OK\"关闭应用\n"
            + "    8.如果解锁工具使游戏启动失败,就重新启动应用程序卸载解锁工具\n"
            + "        转到第五步,选择需要恢复的游戏进行卸载\n\n"
            + "注意: 程序不会帮你下载真实的DLC文件,比如(地平线5的车辆DLC包等)\n"
            + "他只是个解锁工具,如果你想解锁游戏没安装的DLC 例如地平线5\n"
            + "你需要自己去寻找DLC的资源包\n"
            + "如果DLC更新你需要手动安装更新后的DLC\n\n"
            + $"任何错误提交到GITHUB Issues 页面[GitHub Issues]({repository}/issues) \n\n"
            + $"不过: 请阅读 [FAQ entry]({repository}#faq--common-issues) 还有 [template issue]({repository}/issues/new/choose) [GitHub\n"
            + $"Issues]({repository}/issues) 页面不是你倒垃圾的页面,也不是你彰显你低智商的舞台,而是针对各类bug等问题的页面,你的任何傻逼死妈问题,请不要在此页面进行发布,否则你的亲妈会被大运撞死\n"
            + "如果你的问题已经在FAQ Template issues 作了解答，亲自己杀掉亲妈且关闭问题线程 \n"
            + "我不会解答此问题.\n\n"
            + "SteamCMD 的缓存目录: [C:\\ProgramData\\CreamInstaller]().\n"
            + $"程序会自动从项目检查更新 [GitHub]({repository}) \n"
            + $"源代码可以在我的github找到[GitHub]({repository}).");
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