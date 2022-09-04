using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Business.Abstract;
using Core.Constants;
using Core.CrossCuttingConcerns.Validation;
using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using FluentValidation;

namespace Core.Business.Concrete
{
    public class ManagerRepositoryBase<TEntity, TDal> : IServiceRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TDal : class, IEntityRepository<TEntity>
    {
        private readonly TDal _dal;
        private IValidator _validator;
        private readonly string _addMessage = String.Empty;
        private readonly string _updateMessage = String.Empty;
        private readonly string _deleteMessage = String.Empty;
        private readonly string _deleteAllMessage = String.Empty;
        private readonly string _getMessage = String.Empty;
        private readonly string _getAllMessage = String.Empty;

        private ManagerRepositoryBase(TDal dal)
        {
            _dal = dal;
        }

        protected ManagerRepositoryBase(TDal dal, string addMessage = Messages.Added, string updateMessage = Messages.Updated, string deleteMessage = Messages.Deleted, string deleteAllMessage = Messages.AllDeleted, string getMessage = Messages.DataFound, string getAllMessage = Messages.DataFound)
            : this(dal)
        {
            _addMessage = addMessage;
            _updateMessage = updateMessage;
            _deleteMessage = deleteMessage;
            _deleteAllMessage = deleteAllMessage;
            _getMessage = getMessage;
            _getAllMessage = getAllMessage;
        }

        [CacheRemoveAspect("get")]
        public virtual IResult Add(TEntity entity)
        {
            ValidationTool.Validate(_validator, entity);
            _dal.Add(entity);
            return new SuccessResult(_addMessage);
        }

        [CacheRemoveAspect("get")]
        public virtual IResult Delete(TEntity entity)
        {
            _dal.Delete(entity);
            return new SuccessResult(_deleteMessage);
        }

        [CacheRemoveAspect("get")]
        public virtual IResult DeleteAll()
        {
            _dal.DeleteAll();
            return new SuccessResult(_deleteAllMessage);
        }

        [CacheAspect]
        public virtual IDataResult<TEntity> Get(int id)
        {
            return new SuccessDataResult<TEntity>(_dal.Get(e => e.Id == id), _getMessage);
        }

        [CacheAspect]
        public virtual IDataResult<List<TEntity>> GetAll()
        {
            return new SuccessDataResult<List<TEntity>>(_dal.GetAll(), _getAllMessage);
        }

        [CacheRemoveAspect("get")]
        public virtual IResult Update(TEntity entity)
        {
            ValidationTool.Validate(_validator, entity);
            _dal.Update(entity);
            return new SuccessResult(_updateMessage);
        }

        protected void SetValidator(IValidator validator)
        {
            _validator = validator;
        }
    }
}
