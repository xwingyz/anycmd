﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Engine.Ac.Abstractions;
    using Events;

    public class RoleRolePrivilegeAddedEvent : DomainEvent
    {
        public RoleRolePrivilegeAddedEvent(PrivilegeBigramBase source)
            : base(source)
        {
        }
    }
}
