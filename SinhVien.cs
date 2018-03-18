using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class SinhVien
    {
        private static SinhVien instance;

        public static SinhVien Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SinhVien();
                }
                return SinhVien.instance;
            }

            private set
            {
                SinhVien.instance = value;
            }
        }
        private SinhVien()
        {

        }

        public bool InsertSV(string mssv, string hoten, string gioitinh, string ngaysinh, string lop, string nganhhoc)
        {
            string query =string.Format("INSERT SINH_VIEN(mssv,hoten,gioitinh,ngaysinh,lop,nganhhoc) VALUES({0},N'{1}',N'{2}',{3},{4},{5}", mssv,hoten,gioitinh,ngaysinh,lop,nganhhoc);
            DataTable result = Database.Instance.ExcuteQuery(query);
            return result.Rows.Count > 0;
        }
    }
}
