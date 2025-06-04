namespace SunSet.Commands;

[AttributeUsage(AttributeTargets.Method)]
public class HelpTextAttribute(string text) : Attribute
{
    public string Text = text;
}
