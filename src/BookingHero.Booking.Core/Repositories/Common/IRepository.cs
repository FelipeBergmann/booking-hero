using BookingHero.Booking.Core.Entities.Common;
using System.Linq.Expressions;

namespace BookingHero.Booking.Core.Repositories.Common
{
    public interface IRepository<TEntity> where TEntity : IAggregrateRoot
    {
        public Task<TEntity> GetByIdAsync(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        int SaveChanges();
    }
}
