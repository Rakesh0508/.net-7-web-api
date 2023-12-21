using Microsoft.EntityFrameworkCore;
using WorldAPI.Data;
using WorldAPI.Models;

namespace WorldAPI.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _dbContext;


        public CountryRepository(ApplicationDbContext dbContext) : base (dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task Update(Country entity)
        {
            _dbContext.Countries.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
