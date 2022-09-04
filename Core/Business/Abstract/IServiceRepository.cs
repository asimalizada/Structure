using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;

namespace Core.Business.Abstract
{
    public interface IServiceRepository<T> where T : class, IEntity, new()
    {
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
        IResult DeleteAll();
        IDataResult<List<T>> GetAll();
        IDataResult<T> Get(int id);
    }
}
