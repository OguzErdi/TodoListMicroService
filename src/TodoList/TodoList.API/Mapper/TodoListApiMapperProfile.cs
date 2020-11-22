using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.API.ViewModels;
using TodoList.Core.Entities;
using TodoList.Core.Models;

namespace TodoList.API.Mapper
{
    public class TodoListApiMapperProfile : Profile
    {
        public TodoListApiMapperProfile()
        {
            CreateMap<TodoItem, TodoItemViewModel>().ReverseMap();
            CreateMap<TodoItemModel, TodoItemViewModel>().ReverseMap();
        }
    }
}
