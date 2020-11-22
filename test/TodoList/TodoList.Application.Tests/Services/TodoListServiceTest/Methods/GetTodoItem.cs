using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Constants;
using TodoList.Application.Services;
using TodoList.Application.UnitTests.TestData.Services.TodoListServiceTestData.Methods;
using TodoList.Core.Entities;

namespace TodoList.Application.UnitTests.Services.TodoListServiceTest.Methods
{
    [TestFixture]
    public class GetTodoItem : TodoListServiceBaseTest
    {
        [SetUp]
        public void Init()
        {
            mockTodoItemRepository.Setup(x => x.GetTodoItem(It.IsAny<string>()))
            .Returns((string id) =>
            {
                TodoItem todoItem;
                if (id.Equals("123"))
                {
                    todoItem = Constants.TodoItem;
                }
                else
                {
                    todoItem = null;
                }

                return Task.FromResult<TodoItem>(todoItem);
            });
        }


        [Test, TestCaseSource(typeof(GetTodoItemData), "ExistTodoItem_ReturnTodoItem")]
        public async Task ExistTodoItem_ReturnTodoItem(string id)
        {
            //arrange
            var todoListService = new TodoListService(
                mockTodoItemRepository.Object,
                mockLogger.Object);

            //act
            var result = await todoListService.GetTodoItem(id);


            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.ThereIsTodoItem, result.Message);
            Assert.AreEqual(Constants.TodoItem, result.Data);
        }

        [Test, TestCaseSource(typeof(GetTodoItemData), "NotExistTodoItem_ReturnFalse")]
        public async Task NotExistTodoItem_ReturnFalse(string id)
        {
            //arrange
            var todoListService = new TodoListService(
                mockTodoItemRepository.Object,
                mockLogger.Object);

            //act
            var result = await todoListService.GetTodoItem(id);


            //assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ThereIsNoTodoItem, result.Message);
        }
    }
}
