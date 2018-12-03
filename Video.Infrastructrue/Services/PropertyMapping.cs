using System.Collections.Generic;
using Video.Core.Entities;
using Video.Core.Interface;

namespace Video.Infrastructrue.Services
{
    public abstract class PropertyMapping<TSource, TDestination> : IPropertyMapping 
        where TDestination : Entity
    {
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(Entity.Id)] = new List<MappedProperty>
            {
                new MappedProperty { Name = nameof(Entity.Id), Revert = false}
            };
        }
    }
}
