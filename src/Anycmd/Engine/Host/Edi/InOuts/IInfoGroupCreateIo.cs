﻿using System;

namespace Anycmd.Engine.Host.Edi.InOuts
{
    using Model;

    public interface IInfoGroupCreateIo : IEntityCreateInput
    {
        string Code { get; }
        string Description { get; }
        string Name { get; }
        Guid OntologyId { get; }
        int SortCode { get; }
    }
}
