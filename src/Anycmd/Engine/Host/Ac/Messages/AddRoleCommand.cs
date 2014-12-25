﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Commands;
    using InOuts;
    using Model;

    public class AddRoleCommand : AddEntityCommand<IRoleCreateIo>, ISysCommand
    {
        public AddRoleCommand(IRoleCreateIo input)
            : base(input)
        {

        }
    }
}
