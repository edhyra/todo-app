using System;
using System.Windows.Forms;
using TodoManagementApp.Presentation.Controllers;

namespace TodoManagementApp.Presentation.Forms
{
    public class ManagerCreationForm : Form
    {
        private TextBox txtName = null!;
        private TextBox txtId = null!;
        private TextBox txtAccessCode = null!;
        private Button btnCreate = null!;
        private readonly AuthController _authController = new AuthController();

        public ManagerCreationForm()
        {
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            Text = "Create Manager (seed)";
            Width = 420;
            Height = 240;
            InitializeComponents();
            this.AcceptButton = btnCreate;
        }

        private void InitializeComponents()
        {
            var lblName = new Label() { Text = "Name:", Left = 10, Top = 20, Width = 100 };
            txtName = new TextBox() { Left = 120, Top = 18, Width = 260 };
            var lblId = new Label() { Text = "Employee Id:", Left = 10, Top = 60, Width = 100 };
            txtId = new TextBox() { Left = 120, Top = 58, Width = 260 };
            var lblCode = new Label() { Text = "New AccessCode:", Left = 10, Top = 100, Width = 100 };
            txtAccessCode = new TextBox() { Left = 120, Top = 98, Width = 260 };
            btnCreate = new Button() { Text = "Create Manager", Left = 120, Top = 140, Width = 140 };
            btnCreate.Click += BtnCreate_Click;
            Controls.Add(lblName); Controls.Add(txtName);
            Controls.Add(lblId); Controls.Add(txtId);
            Controls.Add(lblCode); Controls.Add(txtAccessCode);
            Controls.Add(btnCreate);
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            var name = txtName.Text?.Trim();
            var id = txtId.Text?.Trim();
            var code = txtAccessCode.Text?.Trim();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(code))
            {
                MessageBox.Show("All fields required.");
                return;
            }

            var manager = _authController.CreateManager(name, id, code);
            MessageBox.Show($"Manager created. Share AccessCode with manager: {code}");
            this.Close();
        }
    }
}
