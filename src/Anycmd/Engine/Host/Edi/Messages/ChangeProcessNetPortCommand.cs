﻿using Anycmd.Commands;
using System;

namespace Anycmd.Engine.Host.Edi.Messages
{
    public class ChangeProcessNetPortCommand: Command, ISysCommand
    {
        public ChangeProcessNetPortCommand(Guid processId, int netPort)
        {
            this.ProcessId = processId;
            this.NetPort = netPort;
        }

        public Guid ProcessId { get; private set; }
        public int NetPort { get; private set; }
    }
}
