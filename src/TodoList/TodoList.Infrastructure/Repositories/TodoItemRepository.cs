using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.KeyValue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Core.Entities;
using TodoList.Core.Models;
using TodoList.Core.Repositories;
using TodoList.Infrastructer.NamedProvider;

namespace TodoList.Infrastructure.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ITodoListProvider todoItemProvider;

        public TodoItemRepository(ITodoListProvider todoItemProvider)
        {
            this.todoItemProvider = todoItemProvider;
        }

        public async Task<List<TodoItemModel>> GetTodoItemList(string username)
        {
            var bucket = await todoItemProvider.GetBucketAsync();

            var query = $"SELECT content, META(d).id as id, isDone, time FROM todolist_bucket as d WHERE owner='{username}'";
            var result = await bucket.Cluster.QueryAsync<TodoItemModel>(query);

            return await result.Rows.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItem(string Id)
        {
            var collection = await GetCollectionAsync();
            TodoItem todoItem;
            try
            {
                var result = await collection.GetAsync(Id);
                todoItem = result.ContentAs<TodoItem>();
            }
            catch (DocumentNotFoundException)
            {
                return null;
            }

            return todoItem;
        }

        public async Task<bool> UpsertTodoItem(string id, TodoItem todoItem)
        {
            var collection = await GetCollectionAsync();
            try
            {
                await collection.UpsertAsync(id, todoItem);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveTodoItem(string Id)
        {
            var collection = await GetCollectionAsync();

            try
            {
                await collection.RemoveAsync(Id);
            }
            catch (DocumentNotFoundException)
            {
                return false;
            }

            return true;
        }

        private async Task<ICouchbaseCollection> GetCollectionAsync()
        {
            var result = await todoItemProvider.GetBucketAsync();

            return result.DefaultCollection();
        }
    }
}
