namespace web_app_hw.Services
{
    public interface IRepository<T,TId>//общий интерфейс репозитория
    {
        IList<T> GetAll();
        T GetById(TId id);
        int Create(T data);
        void Update(T data);
        void Delete(TId id);
    }
}
