using TodoList.Application.Interfaces;
using TodoList.Core.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ResultTypes;
using TodoList.Core.Entities;
using System.Collections.Generic;
using TodoList.Application.Constants;
using System;
using TodoList.Core.Models;

namespace TodoList.Application.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoItemRepository todoItemRepository;
        private readonly ILogger<TodoListService> logger;


        public TodoListService(ITodoItemRepository todoItemRepository, ILogger<TodoListService> logger)
        {
            this.todoItemRepository = todoItemRepository;
            this.logger = logger;
        }

        public async Task<IResult> AddTodoItem(string username, TodoItem todoItem)
        {
            var id = Guid.NewGuid().ToString();
            todoItem.Owner = username;
            var result = await this.todoItemRepository.UpsertTodoItem(id, todoItem);
            if (!result)
            {
                logger.LogInformation(Messages.TodoItemNotAdded);
                return new ErrorResult(Messages.TodoItemNotAdded);
            }

            logger.LogInformation(Messages.TodoItemAdded);
            return new SuccessResult(Messages.TodoItemAdded);
        }

        public async Task<IDataResult<TodoItem>> GetTodoItem(string id)
        {
            var item = await this.todoItemRepository.GetTodoItem(id);
            if (item == null)
            {
                logger.LogInformation(Messages.ThereIsNoTodoItem);
                return new ErrorDataResult<TodoItem>(Messages.ThereIsNoTodoItem);
            }

            logger.LogInformation(Messages.ThereIsTodoItem);
            return new SuccessDataResult<TodoItem>(item, Messages.ThereIsTodoItem);
        }

        public async Task<IDataResult<List<TodoItemModel>>> GetTodoItemList(string username)
        {
            var list = await this.todoItemRepository.GetTodoItemList(username);

            logger.LogInformation(Messages.GetTodoItemList);
            return new SuccessDataResult<List<TodoItemModel>>(list, Messages.GetTodoItemList);
        }

        public async Task<IResult> RemoveTodoItem(string id)
        {
            var result = await this.todoItemRepository.RemoveTodoItem(id);
            if (!result)
            {
                logger.LogInformation(Messages.TodoItemNotRemoved);
                return new ErrorResult(Messages.TodoItemNotRemoved);
            }

            logger.LogInformation(Messages.TodoItemRemoved);
            return new SuccessResult(Messages.TodoItemRemoved);
        }

        public async Task<IResult> UpdateTodoItem(string id, string username, TodoItem todoItem)
        {
            todoItem.Owner = username;
            var result = await this.todoItemRepository.UpsertTodoItem(id, todoItem);
            if (!result)
            {
                logger.LogInformation(Messages.TodoItemNotUpdated);
                return new ErrorResult(Messages.TodoItemNotUpdated);
            }

            logger.LogInformation(Messages.TodoItemUpdated);
            return new SuccessResult(Messages.TodoItemUpdated);
        }
    }
}
