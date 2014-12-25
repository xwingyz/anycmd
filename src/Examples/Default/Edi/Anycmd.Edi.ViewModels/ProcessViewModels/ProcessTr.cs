﻿
namespace Anycmd.Edi.ViewModels.ProcessViewModels
{
    using Engine.Edi;
    using Exceptions;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ProcessTr
    {
        private readonly IAcDomain _host;

        private ProcessTr(IAcDomain host)
        {
            this._host = host;
        }

        public static ProcessTr Create(ProcessDescriptor process)
        {
            return new ProcessTr(process.Host)
            {
                CreateOn = process.Process.CreateOn,
                Id = process.Process.Id,
                IsEnabled = process.Process.IsEnabled,
                Name = process.Process.Name,
                NetPort = process.Process.NetPort,
                OntologyId = process.Process.OntologyId,
                OrganizationCode = process.Process.OrganizationCode,
                Type = process.Process.Type,
                WebApiBaseAddress = process.WebApiBaseAddress
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NetPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid OntologyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OntologyCode
        {
            get
            {
                return this.Ontology.Ontology.Code;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OntologyName
        {
            get
            {
                return this.Ontology.Ontology.Name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WebApiBaseAddress { get; set; }

        private OntologyDescriptor _ontology;

        private OntologyDescriptor Ontology
        {
            get
            {
                if (_ontology != null) return _ontology;
                if (!_host.NodeHost.Ontologies.TryGetOntology(this.OntologyId, out _ontology))
                {
                    throw new ValidationException("意外的本体标识" + this.OntologyId);
                }
                return _ontology;
            }
        }
    }
}
