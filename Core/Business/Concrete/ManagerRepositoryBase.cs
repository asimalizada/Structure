using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Business.Abstract;
using Core.Constants;
using Core.CrossCuttingConcerns.Validation;
using Core.DataAccess.Repositories;
using Core.Entities.Abstract;
using Core.Features.Results.Abstract;
using Core.Features.Results.Concrete;
using FluentValidation;

namespace Core.Business.Concrete
{
    public class ManagerRepositoryBase<TEntity, TRepository> : IServiceRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TRepository : class, IRepository<TEntity>
    {
        private readonly TRepository _repository;
        private IValidator _validator;
        private readonly string _addMessage;
        private readonly string _updateMessage;
        private readonly string _deleteMessage;
        private readonly string _deleteAllMessage;
        private readonly string _getMessage;
        private readonly string _getAllMessage;

        private ManagerRepositoryBase(TRepository repository)
        {
            _repository = repository;
            _addMessage = String.Empty;
            _updateMessage = String.Empty;
            _deleteMessage = String.Empty;
            _deleteAllMessage = String.Empty;
            _getMessage = String.Empty;
            _getAllMessage = String.Empty;
        }

        protected ManagerRepositoryBase(TRepository repository, string addMessage = Messages.Added, string updateMessage = Messages.Updated, 
            string deleteMessage = Messages.Deleted, string deleteAllMessage = Messages.AllDeleted, string getMessage = Messages.DataFound, 
            string getAllMessage = Messages.DataFound)
            : this(repository)
        {
            _addMessage = addMessage;
            _updateMessage = updateMessage;
            _deleteMessage = deleteMessage;
            _deleteAllMessage = deleteAllMessage;
            _getMessage = getMessage;
            _getAllMessage = getAllMessage;
        }

        [CacheRemoveAspect("get")]
        public virtual IDataResult<TEntity> Add(TEntity entity)
        {
            ValidationTool.Validate(_validator, entity);
            return new SuccessDataResult<TEntity>(_repository.Add(entity), _addMessage);
        }

        [CacheRemoveAspect("get")]
        public virtual IDataResult<TEntity> Delete(TEntity entity)
        {
            return new SuccessDataResult<TEntity>(_repository.Delete(entity), _deleteMessage);
        }

        [CacheRemoveAspect("get")]
        public virtual IResult DeleteAll()
        {
            _repository.DeleteAll();
            return new SuccessResult(_deleteAllMessage);
        }

        [CacheAspect]
        public virtual IDataResult<TEntity> Get(int id)
        {
            return new SuccessDataResult<TEntity>(_repository.Get(e => e.Id == id), _getMessage);
        }

        [CacheAspect]
        public virtual IDataResult<List<TEntity>> GetAll()
        {
            return new SuccessDataResult<List<TEntity>>(_repository.GetAll(), _getAllMessage);
        }

        [CacheRemoveAspect("get")]
        public virtual IDataResult<TEntity> Update(TEntity entity)
        {
            ValidationTool.Validate(_validator, entity);
            return new SuccessDataResult<TEntity>(_repository.Update(entity), _updateMessage);
        }

        protected void SetValidator(IValidator validator)
        {
            _validator = validator;
        }
    }
}
