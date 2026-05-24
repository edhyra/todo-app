using System;
using System.Drawing;
using System.Windows.Forms;

namespace TodoManagementApp.Presentation.Forms
{
    public class AccessCodeForm : Form
    {
        private TextBox txtAccessCode = null!;
        private Button btnCopy = null!;
        private Button btnClose = null!;
        private Label lblStatus = null!;

        public AccessCodeForm(string accessCode)
        {
            this.Font = new Font("Segoe UI", 9F);
            Text = "Access Code";
            Width = 420;
            Height = 170;
            InitializeComponents();
            txtAccessCode.Text = accessCode;
            txtAccessCode.SelectAll();
            txtAccessCode.Focus();
        }

        private void InitializeComponents()
        {
            var lbl = new Label() { Text = "Share this AccessCode with the employee:", Left = 10, Top = 10, Width = 380 };
            txtAccessCode = new TextBox() { Left = 10, Top = 32, Width = 380, ReadOnly = true };
            txtAccessCode.ForeColor = Color.Blue;
            txtAccessCode.Font = new Font(txtAccessCode.Font, FontStyle.Bold);
            txtAccessCode.Click += (s, e) => txtAccessCode.SelectAll();

            btnCopy = new Button() { Text = "Copy", Left = 10, Top = 70, Width = 100 };
            btnCopy.Click += BtnCopy_Click;
            btnClose = new Button() { Text = "Close", Left = 120, Top = 70, Width = 100 };
            btnClose.Click += (s, e) => this.Close();

            lblStatus = new Label() { Text = "", Left = 230, Top = 76, Width = 160, ForeColor = Color.Green };

            Controls.Add(lbl);
            Controls.Add(txtAccessCode);
            Controls.Add(btnCopy);
            Controls.Add(btnClose);
            Controls.Add(lblStatus);
        }

        private void BtnCopy_Click(object? sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtAccessCode.Text);
                lblStatus.Text = "Copied to clipboard";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not copy to clipboard: " + ex.Message);
            }
        }
    }
}
