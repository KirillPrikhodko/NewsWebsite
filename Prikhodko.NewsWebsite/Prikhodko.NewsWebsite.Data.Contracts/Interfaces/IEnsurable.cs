namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IEnsurable<T> where T : class
    {
        T Ensure(T item); //the method will add new item to database or return an existing one, avoiding duplicates
    }
}