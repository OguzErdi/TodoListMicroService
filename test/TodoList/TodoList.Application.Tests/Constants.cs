using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Core.Entities;
using TodoList.Core.Models;

namespace TodoList.Application.UnitTests
{
    public class Constants
    {
        public static readonly string TestUser = "Test User";
        public static readonly string TestUserEmptyList = "Test User Empty List";
        public static readonly string TestContent = "Test Content";

        public static readonly TodoItem TodoItem = new TodoItem(TestContent, TestUser, new DateTime(2020, 1, 1), false);
        public static readonly TodoItemModel TodoItemModel = new TodoItemModel(TestContent, TestUser, new DateTime(2020, 1, 1), false);

        public static readonly List<TodoItemModel> TodoItemList = new List<TodoItemModel>() { TodoItemModel, TodoItemModel, TodoItemModel, TodoItemModel };
    }
}
