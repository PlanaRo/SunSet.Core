namespace SunSet.Commands;

[AttributeUsage(AttributeTargets.Method)]
public class SubCommandAttribute(string subname, int length = 1) : Attribute
{
    public string Subname = subname;

    public int Length = length;
}
