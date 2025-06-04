using SunSet.Database.Managers;
using SunSet.Enumerates;

namespace SunSet.EventArgs;

public class PermissionEventArgs(Account account, string perm, UserPermissionType userPermissionType) : System.EventArgs
{
    public Account Account { get; } = account;

    public string permission { get; } = perm;

    public UserPermissionType UserPermissionType { get; set; } = userPermissionType;
}
