﻿
namespace Anycmd.Engine.Host.Edi.Messages
{
    using Engine.Edi.Abstractions;
    using Events;
    using InOuts;

    /// <summary>
    /// 
    /// </summary>
    public class NodeElementCareAddedEvent : DomainEvent
    {
        public NodeElementCareAddedEvent(NodeElementCareBase source, INodeElementCareCreateIo output)
            : base(source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        public INodeElementCareCreateIo Output { get; private set; }
    }
}
