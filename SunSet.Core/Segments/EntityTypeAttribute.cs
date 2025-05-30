using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Core.Segments;

[AttributeUsage(AttributeTargets.Class)]
internal class EntityTypeAttribute(string type) : Attribute
{
    public string Type { get; } = type ?? throw new ArgumentNullException(nameof(type));
}
