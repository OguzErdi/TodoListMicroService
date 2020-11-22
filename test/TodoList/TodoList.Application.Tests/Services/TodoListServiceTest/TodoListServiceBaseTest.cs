using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Application.Services;
using TodoList.Core.Repositories;

namespace TodoList.Application.UnitTests.Services.TodoListServiceTest
{
    public abstract class TodoListServiceBaseTest
    {
        protected readonly Mock<ITodoItemRepository> mockTodoItemRepository;
        protected readonly Mock<ILogger<TodoListService>> mockLogger;

        public TodoListServiceBaseTest()
        {
            mockTodoItemRepository = new Mock<ITodoItemRepository>();
            mockLogger = new Mock<ILogger<TodoListService>>();
        }
    }
}
