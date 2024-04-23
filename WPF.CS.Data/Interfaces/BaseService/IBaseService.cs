namespace WPF.CS.Data.Interfaces.BaseService
{
    public interface IBaseService<T>
    {
        Task AddAsync(T entity);
    }
}
