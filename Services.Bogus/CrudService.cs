using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class CrudService<T> : ICrudService<T> where T : Entity
    {
        protected ICollection<T> Entities { get; }

        public CrudService(EntityFaker<T> faker) : this(faker, 120) { 
        }
        public CrudService(EntityFaker<T> faker, int count)
        {
            Entities = faker.Generate(count);
        }


        public Task<T> CreateAsync(T entity)
        {
            entity.Id = Entities.Max(x => x.Id) + 1;
            Entities.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            if(entity is not null)
                Entities.Remove(entity);
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(Entities.ToList().AsEnumerable());
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult( Entities.SingleOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(int id, T entity)
        {
            if (Entities.All(x => x.Id != id))
                throw new Exception("Not found");
            await DeleteAsync(id);
            entity.Id = id;
            Entities.Add(entity);
        }
    }
}