using System;

namespace KHLBotSharp.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PermissionAttribute:Attribute
    {
        public PermissionAttribute(params uint[] roleId)
        {
            if(roleId.Length == 0)
            {
                throw new ArgumentException("No role id was set for permission! This will cause command will never triggered!");
            }
            this.RoleId = roleId;
        }

        public uint[] RoleId { get; private set; }
    }
}
