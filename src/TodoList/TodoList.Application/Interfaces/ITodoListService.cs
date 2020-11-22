using ResultTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Core.Entities;
using TodoList.Core.Models;

namespace TodoList.Application.Interfaces
{
    public interface ITodoListService
    {
        Task<IResult> AddTodoItem(string username, TodoItem todoItem);
        Task<IDataResult<TodoItem>> GetTodoItem(string id);
        Task<IDataResult<List<TodoItemModel>>> GetTodoItemList(string username);
        Task<IResult> RemoveTodoItem(string guid);
        Task<IResult> UpdateTodoItem(string guid, string username, TodoItem todoItem);
    }
}
