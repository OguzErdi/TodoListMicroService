using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.API.ViewModels
{
    public class TodoItemViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime? Time { get; set; }
        public bool IsDone { get; set; }

        public class TodoItemViewModelValidator : AbstractValidator<TodoItemViewModel>
        {
            public TodoItemViewModelValidator()
            {
                RuleFor(x => x.Content).NotEmpty();
            }
        }
    }
}
