namespace web_app_hw.Services
{
    public interface IRepository<T,TId,R>//общий интерфейс репозитория
    {
        IList<T> GetAll();
        T GetById(TId id);
        int Create(T data);
        bool Update(R data, int id);
        bool Delete(TId id);
    }
}
