using SunSet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Database.Managers;

public partial class Group
{
    public Group? ParentGroup
    {
        get
        {
            return ParentGroupName == null ? null : GetGroup(ParentGroupName);
        }
        set
        {
            ParentGroupName = value?.Name;
        }
    }

    public virtual HashSet<string> GroupPermissions
    {
        get => [.. Permission.Split(',').Where(p => !p.StartsWith('!')).Select(p => p.Trim())];
        set => Permission = string.Join(',', value);
    }

    public virtual HashSet<string> AllPermissions
    {
        get
        {
            var permissions = new HashSet<string>(GroupPermissions);
            var group = this;
            while (group.ParentGroup != null)
            {
                group = group.ParentGroup;
                permissions.UnionWith(group.GroupPermissions);
            }
            return permissions;
        }
    }

    public virtual HashSet<string> NegatePermissions
    {
        get => [.. Permission.Split(',').Where(p => p.StartsWith('!')).Select(p => p.Trim().TrimStart('!'))];
        set => Permission = string.Join(',', value.Select(p => '!' + p).Concat(GroupPermissions));
    }

    public virtual bool HasPermission(string permission)
    {
        bool negated = false;
        if (string.IsNullOrEmpty(permission) || (RealHasPermission(permission, ref negated) && !negated))
        {
            return true;
        }

        if (negated)
            return false;

        string[] nodes = permission.Split('.');
        for (int i = nodes.Length - 1; i >= 0; i--)
        {
            nodes[i] = "*";
            if (RealHasPermission(string.Join(".", nodes, 0, i + 1), ref negated))
            {
                return !negated;
            }
        }
        return false;
    }

    private bool RealHasPermission(string permission, ref bool negated)
    {
        negated = false;
        if (string.IsNullOrEmpty(permission))
            return true;

        Group? cur = this;
        List<Group> traversed = [];
        while (cur != null)
        {
            if (cur.NegatePermissions.Contains(permission))
            {
                negated = true;
                return false;
            }
            if (cur.AllPermissions.Contains(permission))
                return true;
            if (traversed.Contains(cur))
            {
                throw new InvalidOperationException("Infinite group parenting ({0})".SFormat(cur.Name));
            }
            traversed.Add(cur);
            cur = cur.ParentGroup;
        }
        return false;
    }
}
