﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Commands;
    using Model;
    using System;

    public class RemovePositionCommand: RemoveEntityCommand, ISysCommand
    {
        public RemovePositionCommand(Guid groupId)
            : base(groupId)
        {

        }
    }
}
