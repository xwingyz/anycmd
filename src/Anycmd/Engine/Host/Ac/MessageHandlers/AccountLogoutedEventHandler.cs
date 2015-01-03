﻿
namespace Anycmd.Engine.Host.Ac.MessageHandlers
{
    using Anycmd.Rdb;
    using Bus;
    using Dapper;
    using Engine.Ac;
    using Engine.Ac.Messages.Identity;
    using Logging;
    using System;
    using System.Data;
    using Util;

    public class AccountLogoutedEventHandler : IHandler<AccountLogoutedEvent>
    {
        private readonly IAcDomain _host;

        public AccountLogoutedEventHandler(IAcDomain host)
        {
            this._host = host;
        }

        public void Handle(AccountLogoutedEvent message)
        {
            var visitingLogId = message.UserSession.GetData<Guid?>("UserContext_Current_VisitingLogId");
            if (!visitingLogId.HasValue) return;
            var dbId = new Guid("67E6CBF4-B481-4DDD-9FD9-1F0E06E9E1CB");
            RdbDescriptor db;
            if (!_host.Rdbs.TryDb(dbId, out db))
            {
                //throw new CoreException("意外的数据库标识" + dbId);
                return;
            }
            using (var conn = db.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                conn.Execute("update [VisitingLog] set StateCode=@StateCode,ReasonPhrase=@ReasonPhrase,Description=@Description,VisitedOn=@VisitedOn where Id=@Id", new
                {
                    Id = visitingLogId.Value,
                    StateCode = (int)VisitState.LogOut,
                    ReasonPhrase = VisitState.LogOut.ToName(),
                    Description = "退出成功",
                    VisitedOn = SystemTime.Now()
                });
            }
        }
    }
}
