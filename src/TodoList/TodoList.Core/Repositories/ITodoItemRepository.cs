using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Core.Entities;
using TodoList.Core.Models;

namespace TodoList.Core.Repositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetTodoItem(string Id);
        Task<List<TodoItemModel>> GetTodoItemList(string username);
        Task<bool> RemoveTodoItem(string Id);
        Task<bool> UpsertTodoItem(string Id, TodoItem todoItem);
    }
}
