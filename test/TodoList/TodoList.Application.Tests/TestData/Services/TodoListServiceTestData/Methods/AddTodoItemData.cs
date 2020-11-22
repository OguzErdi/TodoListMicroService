using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using TodoList.Core.Entities;

namespace TodoList.Application.UnitTests.TestData.Services.TodoListServiceTestData.Methods
{
    public class AddTodoItemData
    {
        public static IEnumerable ReturnTrue
        {
            get
            {
                yield return new object[] { Constants.TestUser, new TodoItem(Constants.TestContent, Constants.TestUser, null, false) };
                yield return new object[] { Constants.TestUser, new TodoItem(Constants.TestContent, Constants.TestUser, DateTime.Now, false) };
                yield return new object[] { Constants.TestUser, new TodoItem(Constants.TestContent, Constants.TestUser, null, true) };
                yield return new object[] { Constants.TestUser, new TodoItem(Constants.TestContent, Constants.TestUser, DateTime.Now, true) };
            }
        }
    }
}

