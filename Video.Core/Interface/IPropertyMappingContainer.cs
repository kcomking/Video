﻿using Video.Core.Entities;

namespace Video.Core.Interface
{
    public interface IPropertyMappingContainer
    {
        void Register<T>() where T : IPropertyMapping, new();
        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : Entity;
        bool ValidateMappingExistsFor<TSource, TDestination>(string fields) where TDestination : Entity;
    }
}