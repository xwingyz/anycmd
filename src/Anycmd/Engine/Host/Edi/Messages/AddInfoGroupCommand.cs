﻿
namespace Anycmd.Engine.Host.Edi.Messages
{
    using Commands;
    using InOuts;
    using Model;

    public class AddInfoGroupCommand : AddEntityCommand<IInfoGroupCreateIo>, ISysCommand
    {
        public AddInfoGroupCommand(IInfoGroupCreateIo input)
            : base(input)
        {

        }
    }
}
