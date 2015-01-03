﻿
namespace Anycmd
{
    using Engine;
    using Engine.Host.Ac.Infra;
    using Events;
    using Events.Serialization;
    using Events.Storage;
    using Model;
    using Snapshots;
    using Snapshots.Serialization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Util;

    public static class AcDomainExtension
    {
        private static readonly HashSet<EntityTypeMap> EntityTypeMaps = new HashSet<EntityTypeMap>();

        public static void Map(this IAcDomain host, EntityTypeMap map)
        {
            EntityTypeMaps.Add(map);
        }

        public static IEnumerable<EntityTypeMap> GetEntityTypeMaps(this IAcDomain host)
        {
            return EntityTypeMaps;
        }

        public static T DeserializeFromString<T>(this IAcDomain host, string value)
        {
            // TODO:移除对ServiceStack.Text的依赖
            return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
        }

        public static string SerializeToString<T>(this IAcDomain host, T value)
        {
            return ServiceStack.Text.JsonSerializer.SerializeToString<T>(value);
        }

        /// <summary>
        /// 将给定的事件发布到事件总线
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="host"></param>
        /// <param name="evnt"></param>
        public static void PublishEvent<TEvent>(this IAcDomain host, TEvent evnt) where TEvent : class, IEvent
        {
            host.EventBus.Publish(evnt);
        }

        /// <summary>
        /// 提交命令总线
        /// </summary>
        public static void CommitEventBus(this IAcDomain host)
        {
            host.EventBus.Commit();
        }

        /// <summary>
        /// 处理命令，发布并提交命令。
        /// </summary>
        /// <param name="host"></param>
        /// <param name="command"></param>
        public static void Handle(this IAcDomain host, IAnycmdCommand command)
        {
            host.CommandBus.Publish(command);
            host.CommandBus.Commit();
        }

        /// <summary>
        /// Retrieves the service of type <c>T</c> from the provider.
        /// If the service cannot be found, this method returns <c>null</c>.
        /// </summary>
        public static T GetService<T>(this IAcDomain acDomain)
        {
            return (T)acDomain.GetService(typeof(T));
        }

        /// <summary>
        /// Retrieves the service of type <c>T</c> from the provider.
        /// If the service cannot be found, a <see cref="ServiceNotFoundException"/> will be thrown.
        /// </summary>
        public static T GetRequiredService<T>(this IAcDomain acDomain)
        {
            return (T)GetRequiredService(acDomain, typeof(T));
        }

        /// <summary>
        /// Retrieves the service of type <paramref name="serviceType"/> from the provider.
        /// If the service cannot be found, a <see cref="ServiceNotFoundException"/> will be thrown.
        /// </summary>
        public static object GetRequiredService(this IAcDomain acDomain, Type serviceType)
        {
            object service = acDomain.GetService(serviceType);
            if (service == null)
                throw new ServiceNotFoundException(serviceType);
            return service;
        }

        /// <summary>
        /// 从容器中取出给定类型的服务。
        /// 如果服务未找到将返回null值。
        /// </summary>
        public static T RetrieveService<T>(this IAcDomain host)
        {
            return (T)host.GetService(typeof(T));
        }

        /// <summary>
        /// 从容器中取出给定类型的服务。
        /// 如果服务未找到将抛出<see cref="ServiceNotFoundException"/>类型异常。
        /// </summary>
        public static T RetrieveRequiredService<T>(this IAcDomain host)
        {
            return (T)RetrieveRequiredService(host, typeof(T));
        }

        /// <summary>
        /// 从容器中取出给定类型的服务。
        /// 如果服务未找到将抛出<see cref="ServiceNotFoundException"/>类型异常。
        /// </summary>
        public static object RetrieveRequiredService(this IAcDomain host, Type serviceType)
        {
            var service = host.GetService(serviceType);
            if (service == null)
                throw new ServiceNotFoundException(serviceType);
            return service;
        }

        /// <summary>
        /// Creates and initializes the domain event data object from the given domain event.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="entity">The domain event instance from which the domain event data object
        /// is created and initialized.</param>
        /// <returns>The initialized data object instance.</returns>
        public static DomainEventDataObject FromDomainEvent(this IAcDomain host, IDomainEvent entity)
        {
            var serializer = host.RetrieveRequiredService<IDomainEventSerializer>();
            var obj = new DomainEventDataObject
            {
                Branch = entity.Branch,
                Data = serializer.Serialize(entity),
                Id = entity.Id,
                AssemblyQualifiedEventType =
                    string.IsNullOrEmpty(entity.AssemblyQualifiedEventType)
                        ? entity.GetType().AssemblyQualifiedName
                        : entity.AssemblyQualifiedEventType,
                Timestamp = entity.Timestamp,
                Version = entity.Version,
                SourceId = entity.Source.Id,
                AssemblyQualifiedSourceType = entity.Source.GetType().AssemblyQualifiedName
            };
            return obj;
        }

        /// <summary>
        /// Converts the domain event data object to its corresponding domain event entity instance.
        /// </summary>
        /// <returns>The domain event entity instance that is converted from the current domain event data object.</returns>
        public static IDomainEvent ToDomainEvent(this IAcDomain host, DomainEventDataObject from)
        {
            if (string.IsNullOrEmpty(from.AssemblyQualifiedEventType))
                throw new InvalidDataException("form.AssemblyQualifiedTypeName");
            if (from.Data == null || from.Data.Length == 0)
                throw new InvalidDataException("Data");

            var serializer = host.RetrieveRequiredService<IDomainEventSerializer>();
            var type = Type.GetType(from.AssemblyQualifiedEventType);
            var ret = (IDomainEvent)serializer.Deserialize(type, from.Data);
            return ret;
        }

        /// <summary>
        /// Extracts the snapshot from the current snapshot data object.
        /// </summary>
        /// <returns>The snapshot instance.</returns>
        public static ISnapshot ExtractSnapshot(this IAcDomain host, SnapshotDataObject dataObject)
        {
            try
            {
                var serializer = host.RetrieveRequiredService<ISnapshotSerializer>();
                var snapshotType = Type.GetType(dataObject.SnapshotType);
                if (snapshotType == null)
                    return null;
                return (ISnapshot)serializer.Deserialize(snapshotType, dataObject.SnapshotData);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Creates the snapshot data object from the aggregate root.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="aggregateRoot">The aggregate root for which the snapshot is being created.</param>
        /// <returns>The snapshot data object.</returns>
        public static SnapshotDataObject CreateFromAggregateRoot(this IAcDomain host, ISourcedAggregateRoot aggregateRoot)
        {
            var serializer = host.RetrieveRequiredService<ISnapshotSerializer>();

            var snapshot = aggregateRoot.CreateSnapshot();

            return new SnapshotDataObject
            {
                AggregateRootId = aggregateRoot.Id,
                AggregateRootType = aggregateRoot.GetType().AssemblyQualifiedName,
                Version = aggregateRoot.Version,
                Branch = Constants.ApplicationRuntime.DefaultBranch,
                SnapshotType = snapshot.GetType().AssemblyQualifiedName,
                Timestamp = snapshot.Timestamp,
                SnapshotData = serializer.Serialize(snapshot)
            };
        }
    }
}
