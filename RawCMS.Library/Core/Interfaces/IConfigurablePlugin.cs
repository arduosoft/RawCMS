namespace RawCMS.Library.Core.Interfaces
{
    public interface IConfigurablePlugin<T>
    {
        T GetDefaultConfig();

        void SetActualConfig(T config);
    }
}