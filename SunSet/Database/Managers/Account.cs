using LinqToDB;
using LinqToDB.Mapping;
using SunSet.Enumerates;
using SunSet.Events;

namespace SunSet.Database.Managers;

[Table("Account")]
public class Account : RecordBase<Account>
{
    [PrimaryKey, Column("userId"), NotNull]
    public long UserId { get; set; } = 0L;

    [Column("group")]
    public string GroupName { get; set; } = "default";

    public static Context AccountContext { get; } = GetContext("Account");

    public Group AccountGroup
    {
        get => Group.GetGroup(GroupName) ?? Group.GetGroup("default")!;
        set => GroupName = value?.Name ?? "default";
    }

    public bool HasPermission(string permission)
    { 
        return OperateHandler.PermissionEvent(this, permission) switch
        {
            UserPermissionType.Granted => true,
            UserPermissionType.Denied => AccountGroup.HasPermission(permission),
            UserPermissionType.Unhandled => false,
            _ => throw new InvalidOperationException("Unknown permission type returned from event handler.")
        }; 
    }

    public static Account GetAccount(long userId)
    {
        return AccountContext.Records.FirstOrDefault(a => a.UserId == userId) 
            ?? new Account { UserId = userId, GroupName = "default" };
    }

    public static void InsertAccount(uint userid, string groupName)
    {
        var account = new Account { UserId = userid, GroupName = groupName };
        AccountContext.Insert(account);
    }

    public static void DeleteAccount(uint userid)
    { 
        var account = AccountContext.Records.FirstOrDefault(x => x.UserId == userid) 
            ?? throw new NullReferenceException("Account not Exist");
        AccountContext.Delete(account);
    }

    public static void MoveAccountGroup(uint userid, string groupName)
    {
        var account = AccountContext.Records.FirstOrDefault(x => x.UserId == userid) 
            ?? throw new NullReferenceException("Account not Exist");
        if (Group.GetGroup(groupName) == null)
        {
            throw new NullReferenceException("Group not Exist");
        }
        account.GroupName = groupName;
        AccountContext.Update(account);
    }   
}

