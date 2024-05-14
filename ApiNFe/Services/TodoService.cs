using Models;
using ApiNFe.Repositories.Interfaces;
using ApiNFe.Services.Interfaces;
using ApiNFe.DTOs;
using AutoMapper;

namespace ApiNFe.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoDTO>> GetAll()
        {
            var todos = await _todoRepository.GetAll();
            return _mapper.Map<IEnumerable<TodoDTO>>(todos);
        }

        public async Task Add(Todo todo)
        {
            await _todoRepository.Add(todo);
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
