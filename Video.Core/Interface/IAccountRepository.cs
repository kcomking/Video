using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Video.Core.Entities;
using Video.Core.Pages;

namespace Video.Core.Interface
{
  public  interface IAccountRepository:IRepository<Account>
  {
      Task<PaginatedList<Account>> GetPagesAsync(QueryParameters parameters);
  }
}
