﻿
namespace Anycmd.Engine.Host.Edi.Messages
{
    using Commands;
    using InOuts;
    using Model;

    public class AddNodeElementActionCommand : AddEntityCommand<INodeElementActionCreateIo>, ISysCommand
    {
        public AddNodeElementActionCommand(INodeElementActionCreateIo input)
            : base(input)
        {

        }
    }
}
