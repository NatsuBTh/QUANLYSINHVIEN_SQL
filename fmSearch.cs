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
    public partial class fmSearch : Form
    {
        public fmSearch()
        {
            InitializeComponent();
        }

        Database db = new Database();
        DataTable dt;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       
        private void dtgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fmSearch_Load(object sender, EventArgs e)
        {
            dtgvSinhVien.DataSource = db.LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin tìm kiếm", "Thông báo");
            }
            else
            {
                dt = new DataTable();
                dt = db.TimKiem(txtSearch.Text.Trim());//vì hàm này cần truyền vào 1 biến
                if(dt.Rows.Count > 0)
                {
                    dtgvSinhVien.DataSource = dt;
                }
                else
                {
                    MessageBox.Show(txtSearch.Text + " không có trong dữ liệu", "Thông báo");
                    fmSearch_Load(null, null); //nếu không có thì load lại
                    txtSearch.Text = "";
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsSymbol(e.KeyChar) || //Ký tự đặc biệt
                char.IsPunctuation(e.KeyChar)  //dấu chấm
                ) //số               
            {
                e.Handled = true; //Không cho thể hiện lên TextBox
                MessageBox.Show("Vui lòng không nhập các ký tự đặc biệt, dấu chấm");
            }
        }
    }
}
