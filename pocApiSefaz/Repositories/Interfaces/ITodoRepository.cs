using Models;

namespace pocApiSefaz.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAll();

        Task Add(Todo todo);

        //Task<List<Todo>> GetIncompleteTodos();

        //ValueTask<Todo?> Find(int id);


        //Task Update(Todo todo);

        //Task Remove(Todo todo);
    }
}
