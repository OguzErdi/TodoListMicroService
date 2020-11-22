using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TodoList.Core.Entities;

namespace TodoList.Application.UnitTests.TestData.Services.TodoListServiceTestData.Methods
{
    public class GetTodoItemListData
    {
        public static IEnumerable ReturnList
        {
            get
            {
                yield return new object[] { Constants.TestUser };
            }
        }

        public static IEnumerable ReturnEmptyList
        {
            get
            {
                yield return new object[] { Constants.TestUserEmptyList };
            }
        }
    }
}
