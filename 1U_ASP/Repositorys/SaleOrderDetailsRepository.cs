using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Context;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Repositorys
{
    public class SaleOrderDetailsRepository : ISaleOrderDetailsRepository
    {
        private readonly ApplicationContext _context;
        public SaleOrderDetailsRepository(ApplicationContext context)// : base(context)
        {
            _context = context;
        }

        public Task<SaleOrderDetail> Add(SaleOrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<SaleOrderDetail> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SaleOrderDetail> Get(int id)
        {
            return await _context.SaleOrderDetails.FindAsync(id);
        }

        public Task<List<SaleOrderDetail>> GetAll()
        {
            return _context.SaleOrderDetails.ToListAsync();
        }

        public Task<List<SaleOrderDetail>> GetAllAsync()
        {
            return _context.SaleOrderDetails.ToListAsync();
        }

        public Task<SaleOrderDetail> Update(SaleOrderDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
