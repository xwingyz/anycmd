﻿
namespace Anycmd.Engine.Host.Edi.Messages
{
    using Commands;
    using InOuts;
    using Model;

    public class AddTopicCommand : AddEntityCommand<ITopicCreateIo>, ISysCommand
    {
        public AddTopicCommand(ITopicCreateIo input)
            : base(input)
        {

        }
    }
}
