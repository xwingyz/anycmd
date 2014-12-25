﻿
namespace Anycmd.Engine.Ac.Abstractions.Identity
{
    using Model;

    /// <summary>
    /// 表示该接口的实现类是账户
    /// </summary>
    public interface IAccount : IEntity
    {
        /// <summary>
        /// 数字标识
        /// <remarks>
        /// 数字标识作为对人类友好的标识提供给外部。如审计系统。审批工作流中的角色采用数字标识。
        /// </remarks>
        /// </summary>
        int NumberId { get; }

        /// <summary>
        /// 登录名
        /// </summary>
        string LoginName { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 编码
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 
        /// </summary>
        string Email { get; }

        /// <summary>
        /// 
        /// </summary>
        string Qq { get; }

        /// <summary>
        /// 
        /// </summary>
        string Mobile { get; }
    }
}
