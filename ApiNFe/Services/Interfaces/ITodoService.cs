using Models;
using ApiNFe.DTOs;

namespace ApiNFe.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDTO>> GetAll();

        Task Add(Todo todo);

        //Task<List<Todo>> GetIncompleteTodos();

        //ValueTask<Todo?> Find(int id);


        //Task Update(Todo todo);

        //Task Remove(Todo todo);
    }
}
