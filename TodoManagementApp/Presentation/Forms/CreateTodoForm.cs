using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TodoManagementApp.Presentation.Controllers;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.Presentation.Forms
{
    public class CreateTodoForm : Form
    {
        private TextBox txtContent = null!;
        private ListBox lbEmployees = null!;
        private CheckBox chkAll = null!;
        private Button btnCreate = null!;
        private readonly string _managerObjectId;
        private readonly List<Employee>? _initialEmployees;
        private readonly ManagerController _managerController;

        public CreateTodoForm(string managerObjectId, List<Employee>? employees = null, ManagerController? managerController = null)
        {
            _managerObjectId = managerObjectId;
            _initialEmployees = employees ?? new List<Employee>();
            _managerController = managerController ?? new ManagerController();
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            Text = "Create Todo";
            Width = 600;
            Height = 480;
            InitializeComponents();
            this.AcceptButton = btnCreate;
            LoadEmployees();
        }

        private void InitializeComponents()
        {
            var lbl = new Label() { Text = "Content (max 256 chars):", Left = 10, Top = 10, Width = 200 };
            txtContent = new TextBox() { Left = 10, Top = 30, Width = 560, Height = 120, Multiline = true }; 
            chkAll = new CheckBox() { Text = "Assign to all employees", Left = 10, Top = 160, Width = 200 };
            lbEmployees = new ListBox() { Left = 10, Top = 190, Width = 560, Height = 200, SelectionMode = SelectionMode.MultiExtended };
            btnCreate = new Button() { Text = "Create Todo", Left = 10, Top = 400, Width = 120 };
            btnCreate.Click += BtnCreate_Click;
            Controls.Add(lbl); Controls.Add(txtContent); Controls.Add(chkAll); Controls.Add(lbEmployees); Controls.Add(btnCreate);
        }

        private void LoadEmployees()
        {
            lbEmployees.Items.Clear();
            List<Employee> list;
            if (_initialEmployees != null && _initialEmployees.Count > 0)
                list = _initialEmployees;
            else
                list = _managerController.GetEmployees(_managerObjectId);

            foreach (var e in list)
            {
                lbEmployees.Items.Add(new EmployeeListItem(e));
            }
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            var content = txtContent.Text?.Trim();
            if (string.IsNullOrEmpty(content)) { MessageBox.Show("Content is required."); return; }
            List<string> targets;
            if (chkAll.Checked)
            {
                var all = _managerController.GetEmployees(_managerObjectId);
                targets = all.Select(em => em.Id.ToString()).ToList();
            }
            else
            {
                targets = new List<string>();
                foreach (var item in lbEmployees.SelectedItems)
                {
                    if (item is EmployeeListItem eli) targets.Add(eli.Employee.Id.ToString());
                }
            }

            if (targets.Count == 0) { MessageBox.Show("Select at least one employee or choose assign-all."); return; }

            _managerController.CreateTodoForEmployees(_managerObjectId, targets, content);
            MessageBox.Show("Todo(s) created.");
            this.Close();
        }
    }

    // EmployeeListItem helper is defined in ManagerDashboard; reuse it here.
}
