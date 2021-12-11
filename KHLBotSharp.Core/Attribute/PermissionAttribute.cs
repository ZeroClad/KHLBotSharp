using System;

namespace KHLBotSharp.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PermissionAttribute:Attribute
    {
        public PermissionAttribute(params uint[] roleId)
        {
            this.RoleId = roleId;
        }

        public uint[] RoleId { get; private set; }
    }
}
