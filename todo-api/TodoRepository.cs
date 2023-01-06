using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_api;

public class TodoRepository
{

    public Task<List<Todo>> GetAll()
    {

        return Task.FromResult
        (
            new List<Todo>
            {
                new(Guid.NewGuid(), "something coming from the back-end"),
                new(Guid.NewGuid(), "something else also coming from the back-end")
            }
        );
    }
}

public class Todo
{

    public Todo(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}