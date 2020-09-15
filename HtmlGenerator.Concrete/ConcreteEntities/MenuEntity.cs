﻿using System.Collections.Generic;
using HtmlGenerator.Abstractions.Interfaces;
using HtmlGenerator.Services.Concrete;

namespace HtmlGenerator.Concrete.ConcreteEntities
{
    public class MenuEntity : IEntity
    {
        public IEntity Parent { get; set; }
        public List<IEntity> ChildObjects { get; } = new List<IEntity>();
        public string DirectoryName { get; set; }

        public string FileName { get; set; } = "index.htm";
        public string Path { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public MenuEntity()
        {
        }

        public MenuEntity(string directoryName)
        {
            DirectoryName = directoryName;
            Path = Parent?.Path + directoryName;
        }

        public MenuEntity(string directoryName, string title, string subTitle)
        {
            DirectoryName = directoryName;
            Title = title;
            SubTitle = subTitle;
        }

        public void AddChild(IEntity entity)
        {
            ChildObjects.Add(entity);
            entity.Parent = this;
            entity.Path = Path + entity.DirectoryName;
        }

        public void Commit()
        {
            ConcreteService.CreateFolders(this);
            ConcreteService.CreateFiles(this);
        }
    }
}