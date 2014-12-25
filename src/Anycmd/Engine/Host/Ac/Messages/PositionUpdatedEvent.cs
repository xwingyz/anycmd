﻿
namespace Anycmd.Engine.Host.Ac.Messages
{
    using Engine.Ac.Abstractions;
    using Events;
    using InOuts;

    public class PositionUpdatedEvent : DomainEvent
    {
        public PositionUpdatedEvent(GroupBase source, IPositionUpdateIo output)
            : base(source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        public IPositionUpdateIo Output { get; private set; }
    }
}