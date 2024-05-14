using Microsoft.EntityFrameworkCore;
using Models;
using ApiNFe.Context;
using ApiNFe.Repositories.Interfaces;


namespace ApiNFe.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _dbContext;

        public TodoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Todo>> GetAll()
        {
            return await _dbContext.Todos.ToListAsync();
        }

        public async Task Add(Todo todo)
        {
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();
        }

        //public async ValueTask<Todo?> Find(int id)
        //{
        //    return await _dbContext.Todos.FindAsync(id);
        //}

        //public async Task Update(Todo todo)
        //{
        //    _dbContext.Todos.Update(todo);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task Remove(Todo todo)
        //{
        //    _dbContext.Todos.Remove(todo);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public Task<List<Todo>> GetIncompleteTodos()
        //{
        //    return _dbContext.Todos.Where(t => t.IsDone == false).ToListAsync();
        //}
    }
}
