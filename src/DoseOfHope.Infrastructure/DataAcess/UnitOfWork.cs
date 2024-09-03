using DoseOfHope.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Infrastructure.DataAcess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DoseOfHopeDbContext _dbContext;
        public UnitOfWork(DoseOfHopeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task commit() => await _dbContext.SaveChangesAsync();
    }
}
