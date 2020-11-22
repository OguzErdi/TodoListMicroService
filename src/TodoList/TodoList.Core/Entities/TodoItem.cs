using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.Core.Entities
{
    public class TodoItem
    {
        public string Content { get; set; }
        public string Owner { get; set; }
        public DateTime? Time { get; set; }
        public bool IsDone { get; set; }
        public string Type => typeof(TodoItem).Name;

        public TodoItem(string content, string owner, DateTime? time, bool isDone)
        {
            Content = content;
            Owner = owner;
            Time = time;
            IsDone = isDone;
        }
    }
}
