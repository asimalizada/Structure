using Core.Entities.Abstract;
using Core.Features.Results.Abstract;

namespace Core.Business.Abstract
{
    public interface IServiceRepository<T> where T : class, IEntity, new()
    {
        IDataResult<T> Add(T entity);
        IDataResult<T> Update(T entity);
        IDataResult<T> Delete(T entity);
        IResult DeleteAll();
        IDataResult<List<T>> GetAll();
        IDataResult<T> Get(int id);
    }
}
