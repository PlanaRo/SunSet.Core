using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Commands;

[AttributeUsage(AttributeTargets.Method)]
public class HelpTextAttribute(string text) : Attribute
{
    public string Text = text;
}
