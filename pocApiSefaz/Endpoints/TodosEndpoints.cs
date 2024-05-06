using Carter;
using Models;
using pocApiSefaz.DTOs;
using pocApiSefaz.Services.Interfaces;

namespace pocApiSefaz.Endpoints
{
    public class TodosEndpoints : CarterModule
    {
        public TodosEndpoints() : base("/todos") { }


        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (ITodoService todoService) =>
            {
                var todos = await todoService.GetAll();

                return TypedResults.Ok(todos);
            });

            app.MapPost("/", async (TodoDTO todoDto, ITodoService todoService) =>
            {
                var newTodo = new Todo
                {
                    IsComplete = todoDto.IsComplete,
                    Name = todoDto.Name
                };

                await todoService.Add(newTodo);

                return TypedResults.Created($"/todos/v1/{newTodo.Id}", newTodo);
            });

            //app.MapGet("/complete", async (ISender sender) =>
            //{
            //    Result<List<GetTodosCompleteResponse>> result = await sender.Send(new GetTodosCompleteQuery());

            //    return Results.Ok(result.Value);
            //});

            //todoItems.MapGet("/{id}", todoController.GetTodo);
            //todoItems.MapPut("/{id}", todoController.UpdateTodo);
            //todoItems.MapDelete("/{id}", todoController.DeleteTodo);
        }
    }
}