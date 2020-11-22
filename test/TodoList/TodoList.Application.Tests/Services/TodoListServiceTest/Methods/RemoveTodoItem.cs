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
    public class RemoveTodoItem : TodoListServiceBaseTest
    {
        [SetUp]
        public void Init()
        {
            mockTodoItemRepository.Setup(x => x.RemoveTodoItem(It.IsAny<string>()))
            .Returns((string id) =>
            {
                bool result = false;
                if (id.Equals("123"))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

                return Task.FromResult<bool>(result);
            });
        }

        [Test, TestCaseSource(typeof(RemoveTodoItemData), "ExistTodoItem_ReturnTrue")]
        public async Task ExistTodoItem_ReturnTrue(string id)
        {
            //arrange
            var todoListService = new TodoListService(
                mockTodoItemRepository.Object,
                mockLogger.Object);

            //act
            var result = await todoListService.RemoveTodoItem(id);

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.TodoItemRemoved, result.Message);
        }

        [Test, TestCaseSource(typeof(RemoveTodoItemData), "NotExistTodoItem_ReturnFalse")]
        public async Task NotExistTodoItem_ReturnFalse(string id)
        {
            //arrange
            var todoListService = new TodoListService(
                mockTodoItemRepository.Object,
                mockLogger.Object);

            //act
            var result = await todoListService.RemoveTodoItem(id);

            //assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.TodoItemNotRemoved, result.Message);
        }
    }
}
