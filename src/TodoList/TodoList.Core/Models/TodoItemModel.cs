using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Core.Models
{
    public class TodoItemModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime? Time { get; set; }
        public bool IsDone { get; set; }

        public TodoItemModel(string ıd, string content, DateTime? time, bool ısDone)
        {
            Id = ıd;
            Content = content;
            Time = time;
            IsDone = ısDone;
        }
    }
}
