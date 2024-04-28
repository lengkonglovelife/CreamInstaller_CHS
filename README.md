我不是原作者，任何代码问题别来我这边提！谢谢！

### CreamInstaller：自动 DLC 解锁安装程序和配置生成器

![程序预览图](https://img2.imgtp.com/2024/04/28/yGVho5it.png)

###### **注意：** 这只是预览图像； 这不是支持的游戏或配置的列表！

##### 该程序使用最新版本的 [Koaloader](https://github.com/acidicoala/Koaloader)、[SmokeAPI](https://github.com/acidicoala/SmokeAPI)、[ScreamAPI]( https://github.com/acidicoala/ScreamAPI)、[Uplay R1 Unlocker](https://github.com/acidicoala/UplayR1Unlocker) 和 [Uplay R2 Unlocker](https://github.com/acidicoala/UplayR2Unlocker) ，全部由精彩的 [acidicoala](https://github.com/acidicoala) 提供，并且全部从上面的帖子下载并嵌入到程序本身中； 您无需进一步下载！
---
＃＃＃＃ 描述：
自动查找用户计算机上所有已安装的 Steam、Epic 和 Ubisoft 游戏及其各自的 DLC 相关 DLL 位置，
解析SteamCMD、Steam Store和Epic Games Store以获取用户选择的游戏的DLC，然后提供非常简单的图形界面
利用收集到的信息来维护 DLC 解锁器。

该程序的主要功能是**自动生成并安装 DLC 解锁器**
用户选择的游戏和 DLC； 但是，通过使用**右键单击上下文菜单**，用户还可以：
* 自动修复Paradox Launcher
* 在记事本中打开解析的 Steam 和/或 Epic Games appinfo(++)
* 刷新解析的 Steam 和/或 Epic Games 应用程序信息
* 在资源管理器中打开游戏根目录和重要的DLL目录
* 在默认浏览器中打开SteamDB、ScreamDB、Steam Store、Epic Games Store、Steam Community、Ubisoft Store和官方游戏网站链接（如果适用）

---
＃＃＃＃ 特征：
* 每当选择 Steam 游戏时，都会根据需要自动下载和安装 SteamCMD。 *用于收集应用程序信息，例如名称、buildid、dlc 列表、仓库等*
* 自动收集和缓存所有选定的 Steam 和 Epic 游戏及其 DLC 的信息。
* Koaloader、SmokeAPI、ScreamAPI、Uplay R1 Unlocker 和 Uplay R2 Unlocker 的自动 DLL 安装和配置生成。
* 自动卸载 Koaloader、CreamAPI、SmokeAPI、ScreamAPI、Uplay R1 Unlocker 和 Uplay R2 Unlocker 的 DLL 和配置。
* 自动修复 Paradox Launcher（并通过右键单击上下文菜单“修复”选项手动修复）。 *当您安装了 CreamAPI、SmokeAPI 或 ScreamAPI 且启动器更新时。*

---
＃＃＃＃ 安装：
1. 单击[此处](https://github.com/lengkonglovelife/CreamInstaller-CHS/releases/tag/release)从[GitHub](https://github.com/lengkonglovelife/CreamInstaller-CHS)下载最新版本 ）。
2. 将 ZIP 文件中的可执行文件解压到计算机上所需的任何位置。 *它是完全独立的。*

如果程序似乎无法启动，请尝试下载并安装 [.NET Desktop Runtime 8.0.4](https://download.visualstudio.microsoft.com/download/pr/c1d08a81-6e65-4065-b606-ed1127a954d3/14fe55b8a73ebba2b05432b162ab3aa8/windowsdesktop-runtime-8.0.4-win-x64.exe）
并重新启动计算机。 请注意，该程序目前仅支持 Windows 8+ 64 位计算机，因为 .NET 7+ 不再支持 Windows 7。

---
＃＃＃＃ 用法：
1. 启动可执行程序。 *如果未启动，请阅读上面的“安装”部分。*
2. 选择程序应扫描哪些程序和/或游戏以查找 DLC。 *该程序自动从 Steam、Epic 和 Ubisoft 目录收集所有已安装的游戏。*
3. 等待程序下载并安装SteamCMD（如果您选择Steam 游戏）。 *非常快，取决于互联网速度。*
4. 等待程序收集并缓存所选游戏的信息和 DLC。 *首次运行可能需要很长时间，具体取决于您选择的游戏数量以及它们拥有的 DLC 数量。*
5. **仔细**选择您想要解锁的游戏 DLC。 *显然没有任何 DLC 解锁器针对每款游戏进行测试！*
6. 选择是否使用 Koaloader 安装，如果是，则还要选择要使用的代理 DLL。 *如果默认 version.dll 不起作用，请参阅[此处](https://cs.rin.ru/forum/viewtopic.php?p=2552172#p2552172) 找到一个能起作用的版本。*
7. 单击“**生成并安装**”按钮。
8. 单击**确定**按钮关闭程序。
9. 如果任何 DLC 解锁器导致您安装的任何游戏出现问题，只需返回步骤 5 并选择您希望**恢复**更改的游戏，然后单击 **卸载** 这次按钮。

##### **注意：** 该程序不会自动为您下载或安装实际的 DLC 文件； 正如程序标题所述，该程序只是一个 *DLC Unlocker* 安装程序。 如果您想要解锁 DLC 的游戏尚未安装 DLC（大多数游戏都是这种情况），您必须找到、下载这些内容并将其安装到您的游戏中
