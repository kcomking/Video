namespace Video.Core.Interface
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);

    }
}
