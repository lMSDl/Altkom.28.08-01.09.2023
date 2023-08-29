using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class CrudService<T> : ICrudService<T> where T : Entity
    {
        private readonly ICollection<T> _entities;

        public CrudService(EntityFaker<T> faker) : this(faker, 120) { 
        }
        public CrudService(EntityFaker<T> faker, int count)
        {
            _entities = faker.Generate(count);
        }


        public Task<T> CreateAsync(T entity)
        {
            entity.Id = _entities.Max(x => x.Id) + 1;
            _entities.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            if(entity is not null)
                _entities.Remove(entity);
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(_entities.ToList().AsEnumerable());
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult( _entities.SingleOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(int id, T entity)
        {
            if (_entities.All(x => x.Id != id))
                throw new Exception("Not found");
            await DeleteAsync(id);
            entity.Id = id;
            _entities.Add(entity);
        }
    }
}