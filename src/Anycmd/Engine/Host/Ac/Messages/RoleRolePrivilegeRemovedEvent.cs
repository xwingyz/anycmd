﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Engine.Ac.Abstractions;
    using Events;

    public class RoleRolePrivilegeRemovedEvent : DomainEvent
    {
        public RoleRolePrivilegeRemovedEvent(PrivilegeBigramBase source)
            : base(source)
        {
        }
    }
}
