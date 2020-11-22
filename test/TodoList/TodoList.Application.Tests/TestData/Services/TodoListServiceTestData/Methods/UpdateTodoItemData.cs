using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TodoList.Core.Entities;

namespace TodoList.Application.UnitTests.TestData.Services.TodoListServiceTestData.Methods
{
    public class UpdateTodoItemData
    {
        public static IEnumerable ReturnTrue
        {
            get
            {
                yield return new object[] { "123", Constants.TestUser, new TodoItem(Constants.TestContent, Constants.TestUser, null, false) };
            }
        }
    }
}
