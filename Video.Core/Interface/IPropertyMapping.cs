using System.Collections.Generic;
using Video.Infrastructrue.Services;

namespace Video.Core.Interface
{
    public interface IPropertyMapping
    {
        Dictionary<string, List<MappedProperty>> MappingDictionary { get; }
    }
}