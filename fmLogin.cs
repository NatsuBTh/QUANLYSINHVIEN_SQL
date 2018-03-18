using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class fmLogin : Form
    {
        public fmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
                e.Cancel = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text;
            string passWord = txtPassword.Text;
            if(Login(userName, passWord))
            {
                fmHome fmHome = new fmHome();
                MessageBox.Show("Đăng nhập thành công");
                this.Hide();

                fmHome.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo");
            }
            
        }

        bool Login(string userName, string passWord)
        {
            return Account.Instance.Login(userName, passWord);
        }

        private void fmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            //khi bấm enter sẽ tự động bấm button đăng nhập
            if(e.KeyCode == Keys.Enter)
            {
                string userName = txtUser.Text;
                string passWord = txtPassword.Text;
                if (Login(userName, passWord))
                {
                    fmHome fmHome = new fmHome();
                    this.Hide();
                    fmHome.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo");
                }
            }
        }
    }
}
