namespace SunSet.Core.Common;

/// <summary>
/// Specifies a custom event type for a class.
/// </summary>
/// <remarks>This attribute is used to associate a specific event type with a class.  The event type is defined as
/// a string and must be provided when applying the attribute.</remarks>
/// <param name="eventType"></param>
[AttributeUsage(AttributeTargets.Class)]
internal class CustomEventAttribute(string eventType) : Attribute
{
    /// <summary>
    /// Gets the type of the event represented by this instance.
    /// </summary>
    public string EventType { get; } = eventType ?? throw new ArgumentNullException(nameof(eventType));
}
