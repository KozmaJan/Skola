using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.ViewModel
{

    public class TaskItem
    {
        public string Text { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
