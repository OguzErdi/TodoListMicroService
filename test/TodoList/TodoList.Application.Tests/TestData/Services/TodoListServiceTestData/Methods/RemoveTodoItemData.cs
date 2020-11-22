using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TodoList.Core.Entities;

namespace TodoList.Application.UnitTests.TestData.Services.TodoListServiceTestData.Methods
{
    public class RemoveTodoItemData
    {
        public static IEnumerable ExistTodoItem_ReturnTrue
        {
            get
            {
                yield return new object[] { "123" };
            }
        }

        public static IEnumerable NotExistTodoItem_ReturnFalse
        {
            get
            {
                yield return new object[] { "111" };
            }
        }
    }
}
