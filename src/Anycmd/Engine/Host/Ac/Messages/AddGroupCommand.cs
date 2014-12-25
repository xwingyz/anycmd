﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Commands;
    using InOuts;
    using Model;

    public class AddGroupCommand : AddEntityCommand<IGroupCreateIo>, ISysCommand
    {
        public AddGroupCommand(IGroupCreateIo input)
            : base(input)
        {

        }
    }
}
