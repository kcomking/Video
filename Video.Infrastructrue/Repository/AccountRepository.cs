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
   public class AccountRepository: EfRepository<Account>,IAccountRepository,IUserPasswordStore<Account>
    {
        


        public AccountRepository(DBContext context) : base(context)
        {
            
        }

        public async Task<PaginatedList<Account>> GetPagesAsync(QueryParameters parameters)
        {
           
           var query = _dbSet.AsQueryable();
               query = query.ApplySort(parameters.OrderBy);

            var count = await query.CountAsync();
            var data = await query
                .Skip(parameters.PageIndex * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
            return new PaginatedList<Account>(parameters.PageIndex, parameters.PageSize, count, data);
        }

        public Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Name);
        }

        public Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            user.Name = userName;
            Update(user);

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Name.ToLower());
        }

        public Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            await InsertAsync(user);
            return await Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            Update(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            Remove(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = GetByKey(int.Parse(userId));
            return Task.FromResult(user);
        }

        public Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = QueryAll().FirstOrDefault(u => u.Name.ToUpper() == normalizedUserName.ToUpper());
            return Task.FromResult(user);
        }

        public Task SetPasswordHashAsync(Account user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            Update(user);

            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        public void Dispose()
        {
             
        }
    }
}
