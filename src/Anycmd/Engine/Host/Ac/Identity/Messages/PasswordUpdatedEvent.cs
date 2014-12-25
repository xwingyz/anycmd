﻿
namespace Anycmd.Engine.Host.Ac.Identity.Messages
{
    using Engine.Ac.Abstractions.Identity;
    using Events;

    /// <summary>
    /// 
    /// </summary>
    public class PasswordUpdatedEvent : DomainEvent
    {
        public PasswordUpdatedEvent(AccountBase source)
            : base(source)
        {
            this.Password = source.Password;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; private set; }
    }
}
