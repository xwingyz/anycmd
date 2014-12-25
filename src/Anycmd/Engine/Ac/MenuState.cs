﻿
namespace Anycmd.Engine.Ac
{
    using Abstractions.Infra;
    using Exceptions;
    using Model;
    using System;

    /// <summary>
    /// 表示系统菜单业务实体。
    /// </summary>
    public sealed class MenuState : StateObject<MenuState>, IMenu
    {
        private IAcDomain AcDomain { get; set; }
        private Guid _appSystemId;
        private Guid? _parentId;
        private string _name;
        private string _url;
        private string _icon;
        private int _sortCode;
        private string _description;

        private MenuState(Guid id) : base(id) { }

        public static MenuState Create(IAcDomain acDomain, IMenu menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }
            if (!acDomain.AppSystemSet.ContainsAppSystem(menu.AppSystemId))
            {
                throw new ValidationException("意外的应用系统标识" + menu.AppSystemId);
            }
            return new MenuState(menu.Id)
            {
                AcDomain = acDomain,
                _appSystemId = menu.AppSystemId,
                _name = menu.Name,
                _parentId = menu.ParentId,
                _url = menu.Url,
                _icon = menu.Icon,
                _sortCode = menu.SortCode,
                _description = menu.Description
            };
        }

        public Guid AppSystemId
        {
            get { return _appSystemId; }
        }

        public Guid? ParentId
        {
            get { return _parentId; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Url
        {
            get { return _url; }
        }

        public string Icon
        {
            get { return _icon; }
        }

        public int SortCode
        {
            get { return _sortCode; }
        }

        public string Description
        {
            get { return _description; }
        }

        protected override bool DoEquals(MenuState other)
        {
            return Id == other.Id &&
                AppSystemId == other.AppSystemId &&
                ParentId == other.ParentId &&
                Name == other.Name &&
                Url == other.Url &&
                Icon == other.Icon &&
                SortCode == other.SortCode &&
                Description == other.Description;
        }
    }
}
