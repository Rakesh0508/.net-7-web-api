using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorldAPI.Data;
using WorldAPI.Models;

namespace WorldAPI.Repository
{
    public class StatesRepository : GenericRepository<States>, IStatesRepository
    {
        private readonly ApplicationDbContext _dbContext;


        public StatesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

      
        public async Task Update(States entity)
        {
            _dbContext.States.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

}
