using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Repository
{
    public class UserRepo(TaskManagerDbContext dbContext)
    {
        private readonly TaskManagerDbContext _dbContext = dbContext;

        private readonly GenericRepo<UserEntity> _genericRepo = new(dbContext);

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _genericRepo.GetAll().;
        }
    }
}
