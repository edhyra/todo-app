using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Presentation.Controllers;
using TodoManagementApp.Domain.Enums;

namespace TodoManagementApp.Presentation.Forms
{
    public class ManagerDashboard : Form
    {
        private readonly Manager _manager = null!;
        private ListBox lbEmployees = null!;
        private Button btnAddEmployee = null!, btnCreateTodo = null!, btnRefresh = null!, btnCancelTodo = null!, btnTerminateEmployee = null!;
        private ListView lvTodos = null!;
        private readonly ManagerController _managerController = new ManagerController();

        public ManagerDashboard(Manager manager)
        {
            _manager = manager;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            Text = $"Manager Dashboard - {_manager?.Name}";
            Width = 1100;
            Height = 700;
            InitializeComponents();
            LoadEmployees();
            LoadTodos();
        }

        private void InitializeComponents()
        {
            var lbl = new Label() { Text = $"Welcome, {_manager?.Name}", Left = 10, Top = 10, Width = 400 };
            Controls.Add(lbl);
            lbEmployees = new ListBox() { Left = 10, Top = 40, Width = 300, Height = 500, SelectionMode = SelectionMode.MultiExtended };
            lbEmployees.SelectedIndexChanged += (s, e) => OnEmployeeSelectionChanged();
            btnAddEmployee = new Button() { Text = "Add Employee", Left = 320, Top = 40, Width = 120 };
            btnCreateTodo = new Button() { Text = "Create Todo", Left = 320, Top = 80, Width = 120 };
            btnRefresh = new Button() { Text = "Refresh", Left = 320, Top = 120, Width = 120 };
            btnCancelTodo = new Button() { Text = "Cancel Todo", Left = 320, Top = 160, Width = 120 };
            btnTerminateEmployee = new Button() { Text = "Terminate Employee", Left = 320, Top = 200, Width = 120 };
            btnAddEmployee.Click += BtnAddEmployee_Click;
            btnCreateTodo.Click += BtnCreateTodo_Click;
            btnRefresh.Click += (s, e) => { LoadEmployees(); LoadTodos(); };
            btnCancelTodo.Click += BtnCancelTodo_Click;
            btnTerminateEmployee.Click += BtnTerminateEmployee_Click;
            Controls.Add(lbEmployees);
            Controls.Add(btnAddEmployee);
            Controls.Add(btnCreateTodo);
            Controls.Add(btnRefresh);
            Controls.Add(btnCancelTodo);
            Controls.Add(btnTerminateEmployee);

            lvTodos = new ListView() { Left = 460, Top = 40, Width = 600, Height = 500, View = View.Details, FullRowSelect = true };
            lvTodos.Columns.Add("Employee", 180);
            lvTodos.Columns.Add("Content", 300);
            lvTodos.Columns.Add("Status", 80);
            lvTodos.Columns.Add("Created", 120);
            Controls.Add(lvTodos);
        }

        private void OnEmployeeSelectionChanged()
        {
            if (lbEmployees.SelectedItems.Count == 0) LoadTodos();
            else
            {
                var selected = new List<string>();
                foreach (var item in lbEmployees.SelectedItems)
                    if (item is EmployeeListItem li) selected.Add(li.Employee.Id.ToString());
                LoadTodos(selected);
            }
        }

        private void LoadEmployees()
        {
            lbEmployees.Items.Clear();
            var employees = _managerController.GetEmployees(_manager.Id.ToString());
            foreach (var e in employees)
            {
                lbEmployees.Items.Add(new EmployeeListItem(e));
            }
        }

        private void LoadTodos(List<string>? employeeObjectIds = null)
        {
            lvTodos.Items.Clear();
            var todos = (employeeObjectIds != null && employeeObjectIds.Any())
                ? employeeObjectIds.SelectMany(id => _managerController.GetTodosByEmployee(id)).ToList()
                : _managerController.GetTodosByManager(_manager.Id.ToString());
            foreach (var t in todos)
            {
                var emp = _managerController.GetEmployeeById(t.EmployeeId.ToString());
                var empName = emp != null ? emp.Name : t.EmployeeId.ToString();
                var item = new ListViewItem(new[] { empName, t.Content, t.Status.ToString(), t.CreatedAt.ToLocalTime().ToString() }) { Tag = t };
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

        private void BtnAddEmployee_Click(object? sender, EventArgs e)
        {
            var form = new AddEmployeeForm(_manager.Id.ToString());
            form.ShowDialog();
            LoadEmployees();
        }

        private void BtnCreateTodo_Click(object? sender, EventArgs e)
        {
            var selected = new List<Employee>();
            foreach (var item in lbEmployees.SelectedItems)
            {
                if (item is EmployeeListItem li) selected.Add(li.Employee);
            }
            var form = new CreateTodoForm(_manager.Id.ToString(), selected);
            form.ShowDialog();
            LoadTodos();
        }

        private void BtnCancelTodo_Click(object? sender, EventArgs e)
        {
            if (lvTodos.SelectedItems.Count == 0) { MessageBox.Show("Select a todo to cancel."); return; }
            var item = lvTodos.SelectedItems[0];
            if (item.Tag is TodoManagementApp.Domain.Entities.TodoTask todo)
            {
                _managerController.CancelTodo(todo.Id.ToString());
                LoadTodos();
            }
        }

        private void BtnTerminateEmployee_Click(object? sender, EventArgs e)
        {
            if (lbEmployees.SelectedItems.Count == 0) { MessageBox.Show("Select an employee to terminate."); return; }
            if (lbEmployees.SelectedItems.Count > 1) { MessageBox.Show("Please select only one employee to terminate."); return; }
            var sel = lbEmployees.SelectedItems[0];
            if (sel is EmployeeListItem li)
            {
                var emp = li.Employee;
                var confirm = MessageBox.Show($"Are you sure you want to terminate {emp.Name}?", "Confirm Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    _managerController.TerminateEmployee(emp.Id.ToString());
                    LoadEmployees();
                    LoadTodos();
                }
            }
        }
    }

    internal class EmployeeListItem
    {
        public Employee Employee { get; }
        public EmployeeListItem(Employee emp) { Employee = emp; }
        public override string ToString() => $"{Employee.Name} ({Employee.EmployeeId}) - {(Employee.Active ? "Active" : "Inactive")}";
    }
}
