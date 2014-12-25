﻿
namespace Anycmd.Engine.Host.Ac.InOuts
{
    using Model;

    /// <summary>
    /// 创建静态职责分离角色集时的输入或输出参数类型。
    /// </summary>
    public class SsdSetCreateIo : EntityCreateInput, IInputModel, ISsdSetCreateIo
    {
        public string Name { get; set; }

        public int SsdCard { get; set; }

        public int IsEnabled { get; set; }

        public string Description { get; set; }
    }
}
