using LinqToDB;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Database.Managers;

[Table("Group")]
public partial class Group : RecordBase<Group>
{
    [PrimaryKey, Column("name"), NotNull]
    public string Name { get; set; } = "default"; // Default group name

    [Column("permission")]
    public string Permission { get; set; } = "default"; // Default permission level

    [Column("parentGroup")]
    public string? ParentGroupName { get; set; } = null; // Optional parent group for inheritance

    public Group(string name)
    {
        Name = name;
    }

    public Group()
    {
    }

    static Group()
    {
        // Ensure the default group exists
        if (GetGroup("default") == null)
        {
            GroupContext.Insert(new Group("default")
            {
                Permission = "default",
                ParentGroupName = null
            });
        }
    }

    public static Context GroupContext { get; } = GetContext("Group");

    public static Group? GetGroup(string groupName)
    {
        return GroupContext.Records.FirstOrDefault(g => g.Name == groupName);
    }

    public static void InsertGroup(string groupName, string? parent, string perms = "")
    {
        if (GetGroup(groupName) != null)
        {
            throw new InvalidOperationException($"Group '{groupName}' already exists.");
        }
        var group = new Group
        {
            Name = groupName,
            ParentGroupName = parent,
            Permission = perms
        };
        GroupContext.Insert(group);
    }

    public static void AddPermission(string groupName, string permission)
    {
        var group = GetGroup(groupName);
        if (group == null)
        {
            throw new InvalidOperationException($"Group '{groupName}' does not exist.");
        }
        var permissions = group.GroupPermissions;
        permissions.Add(permission);
        group.GroupPermissions = permissions;
        GroupContext.Update(group);
    }

    public static void RemovePermission(string groupName, string permission)
    {
        var group = GetGroup(groupName);
        if (group == null)
        {
            throw new InvalidOperationException($"Group '{groupName}' does not exist.");
        }
        var permissions = group.GroupPermissions;
        permissions.Remove(permission);
        group.GroupPermissions = permissions;
        GroupContext.Update(group);
    }

    public static void SetParentGroup(string groupName, string? parentGroupName)
    {
        var group = GetGroup(groupName);
        if (group == null)
        {
            throw new InvalidOperationException($"Group '{groupName}' does not exist.");
        }
        group.ParentGroupName = parentGroupName;
        GroupContext.Update(group);
    }

    public static void DeleteGroup(string groupName)
    {
        var group = GetGroup(groupName);
        if (group == null)
        {
            throw new InvalidOperationException($"Group '{groupName}' does not exist.");
        }
        GroupContext.Delete(group);
    }

    public static List<Group> GetAllGroups() => [.. GroupContext.Records];
}
