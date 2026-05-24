using System;
using System.Drawing;
using System.Windows.Forms;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Presentation.Controllers;
using TodoManagementApp.Domain.Enums;

namespace TodoManagementApp.Presentation.Forms
{
    public class EmployeeDashboard : Form
    {
        private readonly Employee _employee = null!;
        private ListView lvTodos = null!;
        private readonly EmployeeController _employeeController = new EmployeeController();

        public EmployeeDashboard(Employee employee)
        {
            _employee = employee;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            Text = $"Employee Dashboard - {_employee?.Name}";
            Width = 900;
            Height = 600;
            InitializeComponents();
            LoadTodos();
        }

        private void InitializeComponents()
        {
            var lbl = new Label() { Text = $"Welcome, {_employee?.Name}", Left = 10, Top = 10, Width = 400 };
            Controls.Add(lbl);
            lvTodos = new ListView() { Left = 10, Top = 40, Width = 860, Height = 480, View = View.Details, FullRowSelect = true };
            lvTodos.Columns.Add("Content", 560);
            lvTodos.Columns.Add("Status", 120);
            lvTodos.Columns.Add("Created", 160);
            lvTodos.DoubleClick += LvTodos_DoubleClick;
            Controls.Add(lvTodos);
        }

        private void LoadTodos()
        {
            lvTodos.Items.Clear();
            var todos = _employeeController.GetTodos(_employee.Id.ToString());
            foreach (var t in todos)
            {
                var item = new ListViewItem(new[] { t.Content, t.Status.ToString(), t.CreatedAt.ToLocalTime().ToString() }) { Tag = t };
                item.BackColor = StatusColor(t.Status);
                lvTodos.Items.Add(item);
            }
        }

        private Color StatusColor(TodoStatus status)
        {
            return status switch
            {
                TodoStatus.Active => Color.LightBlue,
                TodoStatus.InProgress => Color.LightYellow,
                TodoStatus.Done => Color.LightGreen,
                TodoStatus.Cancelled => Color.LightCoral,
                TodoStatus.Terminated => Color.LightCoral,
                _ => Color.White,
            };
        }

        private void LvTodos_DoubleClick(object? sender, EventArgs e)
        {
            if (lvTodos.SelectedItems.Count == 0) return;
            var item = lvTodos.SelectedItems[0];
            if (item.Tag is TodoManagementApp.Domain.Entities.TodoTask todo)
            {
                if (todo.Status == TodoStatus.Cancelled) { MessageBox.Show("This todo is cancelled and cannot be changed."); return; }
                if (todo.Status == TodoStatus.Active)
                    _employeeController.UpdateTodoStatus(todo.Id.ToString(), TodoStatus.InProgress);
                else if (todo.Status == TodoStatus.InProgress)
                    _employeeController.UpdateTodoStatus(todo.Id.ToString(), TodoStatus.Done);
                else if (todo.Status == TodoStatus.Done) { MessageBox.Show("Todo already Done."); return; }
                LoadTodos();
            }
        }
    }
}
