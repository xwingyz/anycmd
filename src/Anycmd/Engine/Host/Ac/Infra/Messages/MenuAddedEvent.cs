﻿
namespace Anycmd.Engine.Host.Ac.Infra.Messages
{
    using Engine.Ac.Abstractions.Infra;
    using InOuts;
    using Model;

    public class MenuAddedEvent : EntityAddedEvent<IMenuCreateIo>
    {
        public MenuAddedEvent(MenuBase source, IMenuCreateIo input)
            : base(source, input)
        {
        }
    }
}