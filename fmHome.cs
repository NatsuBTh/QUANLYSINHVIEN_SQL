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
    public partial class fmHome : Form
    {
        public fmHome()
        {
            InitializeComponent();
            LoadDanhSachSinhVien();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            fmLogin fmlogin = new fmLogin();
            this.Hide();
            fmlogin.Show();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized; //hạ cửa sổ xuống thanh taskbar
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            fmSearch fmSearch = new fmSearch();
            fmSearch.ShowDialog();
        }

        void LoadDanhSachSinhVien()
        {
            string query = "SELECT * FROM SINH_VIEN";
            Database db = new Database();
            dtgvSinhVien.DataSource = db.ExcuteQuery(query);
        }

       
        private void fmHome_Load(object sender, EventArgs e)
        {
            
            LoadDanhSachSinhVien();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMSSV.Text == "" && txtName.Text != "" && cbSex.SelectedItem != null && txtClass.Text != "" && cbIndustry.SelectedItem != null)
            {
                MessageBox.Show("mã số sinh viên không được để trống");
            }
            if(txtMSSV.Text != "" && txtName.Text == "" && cbSex.SelectedItem != null && txtClass.Text != "" && cbIndustry.SelectedItem != null)
            {
                MessageBox.Show("tên sinh viên không được để trống");
            }
            if(txtMSSV.Text != "" && txtName.Text != "" && cbSex.SelectedItem == null && txtClass.Text != "" && cbIndustry.SelectedItem != null)
            {
                MessageBox.Show("chưa chọn giới tính");
            }
            if(txtMSSV.Text != "" && txtName.Text != "" && cbSex.SelectedItem != null && txtClass.Text == "" && cbIndustry.SelectedItem != null)
            {
                MessageBox.Show("tên lớp không được để trống");
            }
            if(txtMSSV.Text != "" && txtName.Text != "" && cbSex.SelectedItem != null && txtClass.Text != "" && cbIndustry.SelectedItem == null)
            {
                MessageBox.Show("chưa chọn ngành học");
            }
            if(txtMSSV.Text == "" && txtName.Text == "" && cbSex.SelectedItem == null && txtClass.Text == "" && cbIndustry.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo");
            }
            if(txtMSSV.Text != "" && txtName.Text != "" && cbSex.SelectedItem != null && txtClass.Text != "" && cbIndustry.SelectedItem != null)
            {
                Database db = new Database();
                db.xulydulieu("INSERT INTO SINH_VIEN(mssv, hoten, gioitinh, ngaysinh, lop, nganhhoc)VALUES('"+ txtMSSV.Text +"',N'"+ txtName.Text +"',N'"+ cbSex.Text +"','"+ dtpkDate.Text +"','"+ txtClass.Text +"',N'"+ cbIndustry.Text +"')");
                
                MessageBox.Show("Thêm thành công");
                fmHome_Load(sender, e);
                SetNull();
            }
        }

        private void dtgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //khi click vào 1 row trong datagridview sẽ hiển thị lên các textbox, combobox, datetimepicker
            int i;
            i = dtgvSinhVien.CurrentRow.Index;
            txtMSSV.Text = dtgvSinhVien.Rows[i].Cells[0].Value.ToString();
            txtName.Text = dtgvSinhVien.Rows[i].Cells[1].Value.ToString();
            cbSex.Text = dtgvSinhVien.Rows[i].Cells[2].Value.ToString();
            dtpkDate.Text =  dtgvSinhVien.Rows[i].Cells[3].Value.ToString();
            txtClass.Text = dtgvSinhVien.Rows[i].Cells[4].Value.ToString();
            cbIndustry.Text = dtgvSinhVien.Rows[i].Cells[5].Value.ToString();
        }

        private void txtMSSV_KeyPress(object sender, KeyPressEventArgs e)
        {

            //kiểm tra
            if (char.IsSymbol(e.KeyChar) || //Ký tự đặc biệt
                char.IsWhiteSpace(e.KeyChar) || //Khoảng cách
                char.IsPunctuation(e.KeyChar)) //Dấu chấm                
            {
                e.Handled = true; //Không cho thể hiện lên TextBox
                MessageBox.Show("Vui lòng không nhập ký tự đặc biệt, khoảng cách và dấu chấm");
            }

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {

            //kiểm tra
            if (char.IsSymbol(e.KeyChar) || //Ký tự đặc biệt
                char.IsPunctuation(e.KeyChar) || //dấu chấm
                char.IsNumber(e.KeyChar)) //số               
            {
                e.Handled = true; //Không cho thể hiện lên TextBox
                MessageBox.Show("Vui lòng không nhập các ký tự đặc biệt, dấu chấm và số");
            }

            
        }

        private void txtClass_KeyPress(object sender, KeyPressEventArgs e)
        {
            //in hoa tất cả kí tự trong textbox
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());

            //kiểm tra
            if (char.IsSymbol(e.KeyChar) || //Ký tự đặc biệt
                char.IsWhiteSpace(e.KeyChar) || //Khoảng cách
                char.IsPunctuation(e.KeyChar)) //Dấu chấm                
            {
                e.Handled = true; //Không cho thể hiện lên TextBox
                MessageBox.Show( "Vui lòng không nhập ký tự đặc biệt, khoảng cách và dấu chấm");
            }
        }

        void SetNull()
        {
            txtMSSV.Text = "";
            txtName.Text = "";
            cbSex.SelectedItem = null;
            dtpkDate.Text = DateTime.Now.ToString();
            txtClass.Text = "";
            cbIndustry.SelectedItem = null;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            int kq = db.xulydulieu("UPDATE SINH_VIEN SET hoten=N'"+ txtName.Text +"', gioitinh=N'"+ cbSex.Text +"', ngaysinh='"+ dtpkDate.Text +"',lop='"+ txtClass.Text +"',nganhhoc=N'"+ cbIndustry.Text +"' WHERE mssv='"+ txtMSSV.Text +"'");
            MessageBox.Show("Sửa thành công");
            LoadDanhSachSinhVien();
            SetNull();
  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            int kq = db.xulydulieu("DELETE FROM SINH_VIEN WHERE mssv='" + txtMSSV.Text + "'");
            if (kq > 0)
            {
                MessageBox.Show("Xóa thành công");
                LoadDanhSachSinhVien();
                SetNull();
            }
            else
            {
                MessageBox.Show("Xóa không thành công, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
