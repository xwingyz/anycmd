﻿
namespace Anycmd.Ac.ViewModels.Infra.EntityTypeViewModels
{
    using Engine.Ac;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class EntityTypeTr
    {
        private readonly IAcDomain _host;

        private EntityTypeTr(IAcDomain host)
        {
            this._host = host;
        }

        public static EntityTypeTr Create(EntityTypeState entityType)
        {
            if (entityType == null)
            {
                return null;
            }
            return new EntityTypeTr(entityType.AcDomain)
            {
                Code = entityType.Code,
                Codespace = entityType.Codespace,
                IsOrganizational = entityType.IsOrganizational,
                CreateOn = entityType.CreateOn,
                DatabaseId = entityType.DatabaseId,
                DeveloperId = entityType.DeveloperId,
                Id = entityType.Id,
                Name = entityType.Name,
                SchemaName = entityType.SchemaName,
                SortCode = entityType.SortCode,
                TableName = entityType.TableName,
                ClrTypeFullName = entityType.Map.ClrType.FullName
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Codespace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOrganizational { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid DatabaseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SchemaName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SortCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid DeveloperId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateOn { get; set; }

        public string DeveloperCode
        {
            get
            {
                AccountState developer;
                if (!_host.SysUsers.TryGetDevAccount(this.DeveloperId, out developer))
                {
                    return "无效值";
                }
                return developer.LoginName;
            }
        }

        public string ClrTypeFullName { get; set; }
    }
}
