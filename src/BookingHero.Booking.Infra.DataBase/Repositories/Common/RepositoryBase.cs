using BookingHero.Booking.Core.Entities.Common;
using BookingHero.Booking.Core.Repositories.Common;
using BookingHero.Booking.Infra.DataBase.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingHero.Booking.Infra.DataBase.Repositories.Common
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IAggregrateRoot
    {
        protected readonly BookingContext _dbContext;

        protected DbSet<TEntity> SetEntity() => _dbContext.Set<TEntity>();

        protected RepositoryBase(BookingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> GetByIdAsync(Guid id) => await SetEntity().FindAsync(id);

        public IEnumerable<TEntity> GetAll() => SetEntity().AsEnumerable();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression) => SetEntity().Where(expression);

        public void Add(TEntity entity) => SetEntity().Add(entity);
        public void Update(TEntity entity) => SetEntity().Update(entity);

        public void AddRange(IEnumerable<TEntity> entities) => SetEntity().AddRange(entities);

        public void Remove(TEntity entity) => SetEntity().Remove(entity);        

        public void RemoveRange(IEnumerable<TEntity> entities) => SetEntity().RemoveRange(entities);

        public int SaveChanges() => _dbContext.SaveChanges();
    }
}
