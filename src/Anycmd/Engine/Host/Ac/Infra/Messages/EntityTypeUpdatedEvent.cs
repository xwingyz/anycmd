﻿
namespace Anycmd.Engine.Host.Ac.Infra.Messages
{
    using Engine.Ac.Abstractions.Infra;
    using Events;
    using InOuts;

    /// <summary>
    /// 
    /// </summary>
    public class EntityTypeUpdatedEvent : DomainEvent
    {
        public EntityTypeUpdatedEvent(EntityTypeBase source, IEntityTypeUpdateIo input)
            : base(source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        public IEntityTypeUpdateIo Input { get; private set; }
    }
}
