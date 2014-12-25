﻿
namespace Anycmd.Engine.Ac.Abstractions
{
    using System;

    /// <summary>
    /// 组。
    /// 一个组是一个地图，整个森林中的树木是一个集合，树木类型的一个组则是一张记录树木位置的坐标图，图上的树木是整个森林的一个子集。
    /// 用户绘制一张记录树木的位置的坐标图，然后可以把这张图交给某个工人，从而工人按图索骥去伐木。
    /// 不在图上的树木可能是不允许砍伐的。Group跟手工仓库的区别是一个Group里的资源的类型都是一样的，而手工仓库里的资源可以不是同一类型的
    /// （手工仓库中的一条资源记录需要ObjectType+ObjectId两个字段来标识而组只需一个字段）。
    /// <remarks>
    /// 不添加岗位模型，岗位是一种有组织结构的工作组。可以为岗位授权，既然岗位是工作组，
    /// 那么自动已经可以授权。不添加专门的职务模型。职务是字典，通过系统字典表现职务。
    /// </remarks>
    /// </summary>
    public interface IGroup
    {
        /// <summary>
        /// 
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// 组所属组织结构，如果该属性有指向组织结构的值的话该组就是绑定了组织结构的，比如岗位就属于绑定了组织结构的组。绑定了组织结构的工作组中的资源只能来自于这个组织结构和其子组织结构。
        /// <remarks>
        /// 工作组是跨组织结构的资源组，组中的资源不只来自一个组织结构。
        /// </remarks>
        /// </summary>
        string OrganizationCode { get; }
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        string CategoryCode { get; }

        int SortCode { get; }

        int IsEnabled { get; }

        DateTime? CreateOn { get; }
    }
}
