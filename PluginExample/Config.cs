using SunSet.ConfigFiles;
using System.Text.Json.Serialization;

namespace PluginExample;

public class Config : JsonConfigBase<Config>
{
    [JsonPropertyName("禁用指令")]
    public HashSet<string> DisabledCommands { get; set; } = [];

    [JsonPropertyName("禁用权限")]
    public HashSet<string> DisabledPermissions { get; set; } = [];

    protected override string Filename => "Example";

    /// <summary>
    /// 文件不存在时，第一次加载配置调用，设置默认值
    /// </summary>
    protected override void SetDefault()
    {
        DisabledCommands =
        [
            "example_command1",
            "example_command2"
        ];
        DisabledPermissions =
        [
            "example_permission1",
            "example_permission2"
        ];
    }
}
