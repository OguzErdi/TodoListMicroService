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
using TodoList.Core.Models;

namespace TodoList.Application.UnitTests.Services.TodoListServiceTest.Methods
{
    [TestFixture]
    public class GetTodoItemList : TodoListServiceBaseTest
    {
        [SetUp]
        public void Init()
        {
            mockTodoItemRepository.Setup(x => x.GetTodoItemList(It.IsAny<string>()))
            .Returns((string username) =>
            {
                List<TodoItemModel> todoItemList = new List<TodoItemModel>();
                if (username.Equals(Constants.TestUser))
                {
                    todoItemList = Constants.TodoItemList;
                }

                return Task.FromResult(todoItemList);
            });
        }

        [Test, TestCaseSource(typeof(GetTodoItemListData), "ReturnList")]
        public async Task ReturnList(string username)
        {
            //arrange
            var todoListService = new TodoListService(
                mockTodoItemRepository.Object,
                mockLogger.Object);

            //act
            var result = await todoListService.GetTodoItemList(username);

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.GetTodoItemList, result.Message);
            Assert.AreEqual(Constants.TodoItemList, result.Data);
        }

        [Test, TestCaseSource(typeof(GetTodoItemListData), "ReturnEmptyList")]
        public async Task ReturnEmptyList(string username)
        {
            //arrange
            var todoListService = new TodoListService(
                mockTodoItemRepository.Object,
                mockLogger.Object);

            //act
            var result = await todoListService.GetTodoItemList(username);

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.GetTodoItemList, result.Message);
            Assert.AreEqual(new List<TodoItemModel>(), result.Data);
        }
    }
}
