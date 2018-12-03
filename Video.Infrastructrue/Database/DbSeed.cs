using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Video.Core.Entities;
using Video.Core.Enum;

namespace Video.Infrastructrue.Database
{
   public class DbSeed
    {
        public static async Task SeedAsync(DBContext db,ILoggerFactory loggerFactory,int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                if (!db.Accounts.Any())
                {
                    db.Accounts.AddRange(new List<Account>
                    {
                        new Account
                        {
                            Name="lilei",
                            Phone="18881868368",
                            Password ="123456",
                            VipType = VipType.Vip,
                            VipExpirationDate = DateTime.MaxValue,
                            IsGeneralAgent = true, 
                            FreeVideoCount = 5,
                            Balance = 100,
                            PromoCode = "!",
                            Remark = "" 

                        },
                        new Account
                        {
                            Name="hanmeimei",
                            Phone="19991868368",
                            Password ="123456",
                            VipType = VipType.MonthVip,
                            VipExpirationDate = DateTime.MaxValue,
                            IsGeneralAgent = true,
                            PromoCode = "!",
                            FreeVideoCount = 5,
                            Balance = 100,
                            
                            Remark = ""

                        }
                    });
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
