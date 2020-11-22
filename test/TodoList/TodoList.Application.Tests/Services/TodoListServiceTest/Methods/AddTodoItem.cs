using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Services;
using TodoList.Application.UnitTests.TestData.Services.TodoListServiceTestData.Methods;
using TodoList.Core.Entities;

namespace TodoList.Application.UnitTests.Services.TodoListServiceTest.Methods
{
    [TestFixture]
    public class AddTodoItem : TodoListServiceBaseTest
    {
        [Test, TestCaseSource(typeof(AddTodoItemData), "ReturnTrue")]
        public async Task ReturnTrue(string username, TodoItem todoItem)
        {
            //arrange
            mockTodoItemRepository.Setup(x => x.UpsertTodoItem(It.IsAny<string>(), It.IsAny<TodoItem>()))
            .Returns((string Id, TodoItem todoItem) => 
            {
                return Task.FromResult(true);
            });

            var todoListService = new TodoListService(
                mockTodoItemRepository.Object,
                mockLogger.Object);

            //act
            var result = await todoListService.AddTodoItem(username, todoItem);

            //assert
            Assert.IsTrue(result.Success);
        }
    }
}
