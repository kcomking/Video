using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Video.Core.Interface;

namespace Video.Infrastructrue.Database
{
   public class UnitOfWork:IUnitOfWork
    {
        private readonly DBContext _dbContext;

        public UnitOfWork(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
