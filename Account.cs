using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class Account
    {
        private static Account instance;

        public static Account Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Account();
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }
        private Account()
        {

        }

        public bool Login(string userName, string passWord)
        {
            string query = "SELECT * FROM ACCOUNT WHERE username='"+userName+"' AND password='"+passWord+"'";
            DataTable result = Database.Instance.ExcuteQuery(query);
            return result.Rows.Count > 0;

        }
    }
}
