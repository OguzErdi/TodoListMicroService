using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Constants;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Application.UnitTests.TestData.Services.TodoListServiceTestData.Methods;
using TodoList.Core.Entities;

namespace TodoList.Application.UnitTests.Services.TodoListServiceTest.Methods
{
    [TestFixture]
    public class UpdateTodoItem : TodoListServiceBaseTest
    {
        [Test, TestCaseSource(typeof(UpdateTodoItemData), "ReturnTrue")]
        public async Task ReturnTrue(string id, string username, TodoItem todoItem)
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
            var result = await todoListService.UpdateTodoItem(id, username, todoItem);

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.TodoItemUpdated, result.Message);
        }
    }
}
