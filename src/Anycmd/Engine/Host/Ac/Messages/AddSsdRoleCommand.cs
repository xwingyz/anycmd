﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Commands;
    using InOuts;
    using Model;

    public class AddSsdRoleCommand : AddEntityCommand<ISsdRoleCreateIo>, ISysCommand
    {
        public AddSsdRoleCommand(ISsdRoleCreateIo input)
            : base(input)
        {

        }
    }
}
