namespace SunSet.Core.Common;

/// <summary>
/// 初始化一个新的 MikyEventTypeAttribute 实例
/// </summary>
/// <param name="eventType">事件类型</param>
[AttributeUsage(AttributeTargets.Class)]
internal class CustomEventAttribute(string eventType) : Attribute
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public string EventType { get; } = eventType ?? throw new ArgumentNullException(nameof(eventType));
}
