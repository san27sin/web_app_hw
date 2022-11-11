namespace web_app_hw.Services
{
    public interface IRepository<T,TId>//общий интерфейс репозитория
    {
        IList<T> GetAll();
        T GetById(TId id);
        int Create(T data);
        bool Update(T data);
        bool Delete(TId id);
    }
}
