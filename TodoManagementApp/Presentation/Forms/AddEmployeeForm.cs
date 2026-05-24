using System;
using System.Windows.Forms;
using TodoManagementApp.Presentation.Controllers;

namespace TodoManagementApp.Presentation.Forms
{
    public class AddEmployeeForm : Form
    {
        private TextBox txtName = null!;
        private TextBox txtId = null!;
        private Button btnCreate = null!;
        private readonly string _managerObjectId;
        private readonly ManagerController _managerController;

        public AddEmployeeForm(string managerObjectId) : this(managerObjectId, null) { }

        public AddEmployeeForm(string managerObjectId, ManagerController? managerController)
        {
            _managerObjectId = managerObjectId;
            _managerController = managerController ?? new ManagerController();
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            Text = "Add Employee";
            Width = 420;
            Height = 180;
            InitializeComponents();
            this.AcceptButton = btnCreate;
        }

        private void InitializeComponents()
        {
            var lblName = new Label() { Text = "Name:", Left = 10, Top = 20, Width = 100 };
            txtName = new TextBox() { Left = 120, Top = 18, Width = 260 };
            var lblId = new Label() { Text = "Employee Id:", Left = 10, Top = 60, Width = 100 };
            txtId = new TextBox() { Left = 120, Top = 58, Width = 260 };
            btnCreate = new Button() { Text = "Create Employee", Left = 120, Top = 96, Width = 140 };
            btnCreate.Click += BtnCreate_Click;
            Controls.Add(lblName); Controls.Add(txtName);
            Controls.Add(lblId); Controls.Add(txtId);
            Controls.Add(btnCreate);
        }

        internal static bool HasEmptyFields(string? name, string? id)
        {
            return string.IsNullOrEmpty(name?.Trim()) || string.IsNullOrEmpty(id?.Trim());
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            var name = txtName.Text?.Trim();
            var id = txtId.Text?.Trim();
            if (HasEmptyFields(name, id))
            {
                MessageBox.Show("All fields required.");
                return;
            }
            var (employee, accessCode) = _managerController.AddEmployee(_managerObjectId, name!, id!);
            var acf = new AccessCodeForm(accessCode);
            acf.ShowDialog();
            this.Close();
        }
    }
}
