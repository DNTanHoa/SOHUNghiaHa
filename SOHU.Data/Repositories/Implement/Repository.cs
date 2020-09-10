using Microsoft.EntityFrameworkCore;
using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOHU.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly SOHUContext _context;

        public Repository(SOHUContext context)
        {
            _context = context;
        }

        public async Task<int> AsyncCreate(T model)
        {
            _context.Set<T>().Add(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AsyncDelete(int ID)
        {
            var existModel = await AsyncGetByID(ID);
            if (existModel != null)
            {
                _context.Set<T>().Remove(existModel);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<List<T>> AsyncGetAllToList()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result ?? new List<T>();
        }

        public async Task<T> AsyncGetByID(int ID)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(model => model.Id == ID);
        }

        public async Task<int> AsyncUpdate(int ID, T model)
        {
            var existModel = await AsyncGetByID(ID);
            if (existModel != null)
            {
                existModel = model;
                _context.Set<T>().Update(existModel);
            }
            return await _context.SaveChangesAsync();
        }

        public int Create(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges();
        }

        public int Delete(int ID)
        {
            var existModel = GetByID(ID);
            if (existModel != null)
            {
                _context.Set<T>().Remove(existModel);
            }
            return _context.SaveChanges();
        }

        public List<T> GetAllToList()
        {
            var result = _context.Set<T>().ToList();
            return result ?? new List<T>();
        }

        public T GetByID(int ID)
        {
            var result = _context.Set<T>().AsNoTracking().FirstOrDefault(model => model.Id == ID);
            return result;
        }

        public int Update(int ID, T model)
        {
            var existModel = GetByID(ID);
            if (existModel != null)
            {
                existModel = model;
                _context.Set<T>().Update(existModel);
            }
            return _context.SaveChanges();
        }

        public List<T> GetByPageAndPageSizeToList(int Page, int PageSize)
        {
            var result = _context.Set<T>().Skip(Page * PageSize).Take(PageSize).ToList();
            return result;
        }

        public int Create(T model, out T result)
        {
            _context.Set<T>().Add(model);
            var temp =  _context.SaveChanges();
            result = model;
            return temp;
        }
    }
}
