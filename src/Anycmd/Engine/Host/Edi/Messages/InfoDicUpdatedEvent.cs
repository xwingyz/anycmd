﻿
namespace Anycmd.Engine.Host.Edi.Messages
{
    using Engine.Edi.Abstractions;
    using Events;
    using InOuts;

    /// <summary>
    /// 
    /// </summary>
    public class InfoDicUpdatedEvent : DomainEvent
    {
        public InfoDicUpdatedEvent(InfoDicBase source, IInfoDicUpdateIo output)
            : base(source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        public IInfoDicUpdateIo Output { get; private set; }
    }
}
