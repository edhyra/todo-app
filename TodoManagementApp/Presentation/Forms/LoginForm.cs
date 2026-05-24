using System;
using System.Windows.Forms;
using TodoManagementApp.Presentation.Controllers;
using TodoManagementApp.Presentation.Forms;

namespace TodoManagementApp.Presentation.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtAccessCode = null!;
        private Button btnLogin = null!;
        private readonly AuthController _authController = new AuthController();

        public LoginForm()
        {
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            Text = "Login - Access Code";
            Width = 420;
            Height = 150;
            InitializeComponents();
            this.AcceptButton = btnLogin;
        }

        private void InitializeComponents()
        {
            var lbl = new Label() { Text = "Access Code:", Left = 10, Top = 20, Width = 100 };
            txtAccessCode = new TextBox() { Left = 120, Top = 18, Width = 260 };
            btnLogin = new Button() { Text = "Login", Left = 120, Top = 50, Width = 100 };
            btnLogin.Click += BtnLogin_Click;
            Controls.Add(lbl);
            Controls.Add(txtAccessCode);
            Controls.Add(btnLogin);
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            var code = txtAccessCode.Text?.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please enter AccessCode.");
                return;
            }
            var (role, employee, manager) = _authController.Login(code);
            if (role == "SEED")
            {
                var mc = new ManagerCreationForm();
                mc.ShowDialog();
                return;
            }

            if (role == "Manager" && manager is not null)
            {
                var md = new ManagerDashboard(manager);
                md.Show();
                this.Hide();
                return;
            }

            if (role == "Employee" && employee is not null)
            {
                var ed = new EmployeeDashboard(employee);
                ed.Show();
                this.Hide();
                return;
            }

            MessageBox.Show("AccessCode not recognized or user inactive.");
        }
    }
}
