﻿
namespace Anycmd.Engine.Host.Ac.InOuts
{
    using Model;
    using System;

    /// <summary>
    /// 表示该接口的实现类是更新系统功能时的输入或输出参数类型。
    /// </summary>
    public interface IFunctionUpdateIo : IEntityUpdateInput
    {
        string Code { get; }
        bool IsManaged { get; }
        int IsEnabled { get; }
        string Description { get; }
        Guid DeveloperId { get; }
        int SortCode { get; }
    }
}
