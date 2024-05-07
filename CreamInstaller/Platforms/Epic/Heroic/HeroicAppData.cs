using Newtonsoft.Json;

namespace CreamInstaller.Platforms.Epic.Heroic;

public class HeroicInstall
{
    [JsonProperty("安装路径")] public string InstallPath { get; set; }
}

public class HeroicAppData
{
    [JsonProperty("安装")] public HeroicInstall Install { get; set; }

    [JsonProperty("名字")] public string Namespace { get; set; }

    [JsonProperty("标题")] public string Title { get; set; }
}