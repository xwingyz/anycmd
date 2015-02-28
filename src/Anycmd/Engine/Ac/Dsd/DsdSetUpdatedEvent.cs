﻿
namespace Anycmd.Engine.Ac.Dsd
{
    using Events;

    public sealed class DsdSetUpdatedEvent : DomainEvent
    {
        public DsdSetUpdatedEvent(IAcSession acSession, DsdSetBase source, IDsdSetUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        public IDsdSetUpdateIo Output { get; private set; }
        internal bool IsPrivate { get; set; }
    }
}