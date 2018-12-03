using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Video.Core.Entities;
using Video.Core.Interface;
using Video.Core.Pages;
using Video.Infrastructrue.Database;
using Video.Infrastructrue.Extensions;
using Video.Infrastructrue.Services;

namespace Video.Infrastructrue.Repository
{
   public class RoleRepository: EfRepository<Role>,IRoleRepository, IRoleStore<Role>
    {
        


        public RoleRepository(DBContext context) : base(context)
        {
            
        }

        public async Task<PaginatedList<Role>> GetPagesAsync(QueryParameters parameters)
        {
           
           var query = _dbSet.AsQueryable();
               query = query.ApplySort(parameters.OrderBy);

            var count = await query.CountAsync();
            var data = await query
                .Skip(parameters.PageIndex * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
            return new PaginatedList<Role>(parameters.PageIndex, parameters.PageSize, count, data);
        }

        
        public void Dispose()
        {
             
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

         
    }
}
