using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class Database
    {

        private static Database instance;
        

        public static Database Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Database();
                }
                return Database.instance;
            }

            private set
            {
                Database.instance = value;
            }
        }
        private string connectionStr = "Data Source=HP-PC;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        public DataTable ExcuteQuery(string query)
        {
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            connection.Close();

            return data;
        }

        public static SqlConnection connect;

        public void moketnoi()
        {
            if (Database.connect == null)
                Database.connect = new SqlConnection("Data Source=HP-PC;Initial Catalog=QuanLySinhVien;Integrated Security=True");
            if (Database.connect.State != ConnectionState.Open)
                Database.connect.Open();
        }

        public string lay1giatri(string sql)
        {
            string kq = "";
            try
            {
                moketnoi();

                SqlCommand sqlComm = new SqlCommand(sql, connect);
                SqlDataReader r = sqlComm.ExecuteReader();
                if (r.Read())
                {
                    kq = r["tong"].ToString();
                }
            }
            catch
            {

            }
            return kq;
        }


        public void dongketnoi()
        {
            if (Database.connect != null)
            {
                if (Database.connect.State == ConnectionState.Open)
                    Database.connect.Close();
            }
        }

        public void thucthicaulenhsql(string strSql)
        {
            try
            {
                moketnoi();
                SqlCommand sqlcmd = new SqlCommand(strSql, connect);
                sqlcmd.ExecuteNonQuery();
                dongketnoi();
            }
            catch
            {

            }
        }

        public DataTable getdatatable(string strSql)
        {
            try
            {
                moketnoi();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlda = new SqlDataAdapter(strSql, connect);
                sqlda.Fill(dt);
                dongketnoi();
                return dt;
            }
            catch
            {
                return null;
            }

        }

        public string GetValue(string strSql)
        {
            string temp = null;
            moketnoi();
            SqlCommand sqlcmd = new SqlCommand(strSql, connect);
            SqlDataReader sqldr = sqlcmd.ExecuteReader();
            while (sqldr.Read())
                temp = sqldr[0].ToString();
            return temp;
        }

        public DataTable bangdulieu = new DataTable();
        public DataTable laybang(string caulenh)
        {
            try
            {
                moketnoi();
                SqlDataAdapter Adapter = new SqlDataAdapter(caulenh, connect);
                DataSet ds = new DataSet();

                Adapter.Fill(bangdulieu);
            }
            catch (System.Exception)
            {
                bangdulieu = null;
            }
            finally
            {
                dongketnoi();
            }

            return bangdulieu;
        }

        public int xulydulieu(string caulenhsql)
        {
            int kq = 0;
            try
            {
                moketnoi();
                SqlCommand lenh = new SqlCommand(caulenhsql, connect);
                kq = lenh.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Thông báo lỗi ra!

                kq = 0;
            }
            finally
            {
                dongketnoi();
            }
            return kq;
        }

        //tìm kiếm
        #region TimKiem
        SqlConnection con;
        string connstr = "Data Source=HP-PC;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        DataTable dt;
        public SqlConnection MoConnect()
        {
            con = new SqlConnection(connstr);
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public SqlConnection DongConnect()
        {
            con = new SqlConnection(connstr);
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }
        public DataTable LoadData()
        {
            dt = new DataTable();
            MoConnect();
            SqlCommand command = new SqlCommand("sp_LoadSinhVien", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            DongConnect();
            return dt;
        }
        //timkiem
        public DataTable TimKiem(string ChuoiTimKiem)
        {
            MoConnect();
            dt = new DataTable();
            SqlCommand command = new SqlCommand("sp_TimKiemSinhVien", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("mssv", SqlDbType.NChar)).Value = ChuoiTimKiem;
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            DongConnect();
            return dt;
        }
        #endregion
    }
}
