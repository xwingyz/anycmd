﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Commands;
    using Model;
    using System;

    public class RemoveSsdRoleCommand : RemoveEntityCommand, ISysCommand
    {
        public RemoveSsdRoleCommand(Guid ssdRoleId)
            : base(ssdRoleId)
        {

        }
    }
}
