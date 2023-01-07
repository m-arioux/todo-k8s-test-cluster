using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace todo_api;

public class TodoRepository
{
    private readonly IMongoCollection<Todo> todoCollection;

    public TodoRepository(
        IOptions<TodoDatabaseSettings> todoDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            todoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            todoDatabaseSettings.Value.DatabaseName);

        todoCollection = mongoDatabase.GetCollection<Todo>(
            todoDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<Todo>> GetAll()
    {
        var results = await todoCollection.Find(_ => true).ToListAsync();

        if (!results.Any())
        {
            await todoCollection.InsertManyAsync(new Todo[] {
                new(Guid.NewGuid(), "something coming from the database"),
                new(Guid.NewGuid(), "something else also coming from the database")
            });

            return await GetAll();
        }

        return results;
    }
}

public class TodoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionName { get; set; } = null!;
}

public class Todo
{
    public Todo(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    [BsonId]
    public Guid Id { get; set; }

    [BsonElement]
    public string Name { get; set; }
}