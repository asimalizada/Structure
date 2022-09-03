using Core.Business.Abstract;
using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Business.Concrete
{
    public class ManagerRepositoryBase<TEntity, TDal> : IServiceRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TDal : class, IEntityRepository<TEntity>, new()
    {
        private readonly TDal _dal;
        private readonly string _addMessage = String.Empty;
        private readonly string _updateMessage = String.Empty;
        private readonly string _deleteMessage = String.Empty;
        private readonly string _getMessage = String.Empty;
        private readonly string _getAllMessage = String.Empty;

        public ManagerRepositoryBase(TDal dal)
        {
            _dal = dal;
        }

        public ManagerRepositoryBase(TDal dal, string addMessage, string updateMessage, string deleteMessage) : this(dal)
        {
            _addMessage = addMessage;
            _updateMessage = updateMessage;
            _deleteMessage = deleteMessage;
        }

        public ManagerRepositoryBase(TDal dal, string addMessage, string updateMessage, string deleteMessage, string getMessage, string getAllMessage) 
            : this(dal, addMessage, updateMessage, deleteMessage)
        {
            _getMessage = getMessage;
            _getAllMessage = getAllMessage;
        }

        public IResult Add(TEntity entity)
        {
            _dal.Add(entity);
            return new SuccessResult(_addMessage);
        }

        public IResult Delete(TEntity entity)
        {
            _dal.Delete(entity);
            return new SuccessResult(_deleteMessage);
        }

        public IDataResult<TEntity> Get(int id)
        {
            return new SuccessDataResult<TEntity>(_dal.Get(e => e.Id == id), _getMessage);
        }

        public IDataResult<List<TEntity>> GetAll()
        {
            return new SuccessDataResult<List<TEntity>>(_dal.GetAll(), _getAllMessage);
        }

        public IResult Update(TEntity entity)
        {
            _dal.Update(entity);
            return new SuccessResult(_updateMessage);
        }
    }
}
