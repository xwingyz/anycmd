﻿
namespace Anycmd.Engine.Host.Ac.MessageHandlers
{
    using Commands;
    using Engine.Ac;
    using Exceptions;
    using Host;
    using Identity;
    using Identity.Messages;
    using Repositories;
    using System;
    using System.Linq;


    public class AddPasswordCommandHandler : CommandHandler<AssignPasswordCommand>
    {
        private readonly IAcDomain _host;

        public AddPasswordCommandHandler(IAcDomain host)
        {
            this._host = host;
        }

        public override void Handle(AssignPasswordCommand command)
        {
            var accountRepository = _host.GetRequiredService<IRepository<Account>>();
            if (string.IsNullOrEmpty(command.Input.LoginName))
            {
                throw new ValidationException("登录名不能为空");
            }
            if (string.IsNullOrEmpty(command.Input.Password))
            {
                throw new ValidationException("密码不能为空");
            }
            if (accountRepository.AsQueryable().Any(a => a.LoginName == command.Input.LoginName && a.Id != command.Input.Id))
            {
                throw new ValidationException("重复的登录名");
            }
            var entity = accountRepository.GetByKey(command.Input.Id);
            if (entity == null)
            {
                throw new NotExistException("账户不存在");
            }
            bool loginNameChanged = !string.Equals(command.Input.LoginName, entity.LoginName);
            AccountState developer;
            if (_host.SysUsers.TryGetDevAccount(command.Input.Id, out developer) && !_host.UserSession.IsDeveloper())
            {
                throw new ValidationException("对不起，您不能修改开发人员的密码。");
            }
            if (!_host.UserSession.IsDeveloper() && "admin".Equals(entity.LoginName, StringComparison.OrdinalIgnoreCase))
            {
                throw new ValidationException("对不起，您无权修改admin账户的密码");
            }
            #region 更改登录名
            if (string.IsNullOrEmpty(command.Input.LoginName))
            {
                throw new ValidationException("登录名不能为空");
            }
            if (loginNameChanged)
            {
                entity.LoginName = command.Input.LoginName;
            }
            #endregion
            if (string.IsNullOrEmpty(command.Input.Password))
            {
                throw new ValidationException("新密码不能为空");
            }
            var passwordEncryptionService = _host.GetRequiredService<IPasswordEncryptionService>();
            var newPassword = passwordEncryptionService.Encrypt(command.Input.Password);
            entity.Password = newPassword;
            entity.LastPasswordChangeOn = DateTime.Now;
            accountRepository.Update(entity);
            accountRepository.Context.Commit();
            if (loginNameChanged)
            {
                _host.EventBus.Publish(new LoginNameChangedEvent(entity));
                if (_host.SysUsers.TryGetDevAccount(entity.Id, out developer))
                {
                    _host.MessageDispatcher.DispatchMessage(new DeveloperUpdatedEvent(entity));
                }
            }
            _host.EventBus.Publish(new PasswordUpdatedEvent(entity));
            _host.EventBus.Commit();
        }
    }
}
