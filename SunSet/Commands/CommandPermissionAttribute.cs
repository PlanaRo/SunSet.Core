using SunSet.Database.Managers;

namespace SunSet.Commands;

[AttributeUsage(AttributeTargets.Method)]
public class CommandPermissionAttribute(params string[] perm) : Attribute
{
    public string[] Permissions = perm;

    public bool DetectAll = false;

    public bool DetectPermission(Account account)
    {
        return DetectAll ? Permissions.All(account.HasPermission) : Permissions.Any(account.HasPermission);
    }
}
