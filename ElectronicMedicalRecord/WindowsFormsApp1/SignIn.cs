using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (this.txb_AccountNum.Text.Trim() == "")
            {
                MessageBox.Show("注册账号不能为空！");
                this.txb_AccountNum.Focus();
                return;
            }
            if (this.txb_Password.Text.Trim() == "")
            {
                MessageBox.Show("注册密码不能为空！");
                this.txb_Password.Focus();
                return;
            }
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString =
                ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText =
                "INSERT tb_User (No,Password) VALUES (@No,HASHBYTES('MD5',@Password));";
            sqlCommand.Parameters.AddWithValue("@No", this.txb_AccountNum.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Password", this.txb_Password.Text.Trim());
            sqlCommand.Parameters["@Password"].SqlDbType = SqlDbType.VarChar;
            int rowAffected = 0;
            try
            {
                sqlConnection.Open();
                rowAffected = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number==2627)
                {
                    MessageBox.Show("您注册的账号已经存在，请重新输入！\n");
                }
            }
            finally
            {
                sqlConnection.Close();
            }
            if (rowAffected==1)
            {
                MessageBox.Show("注册成功！");
            }
            else
            {
                MessageBox.Show("注册失败！");
            }
        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {
            if (this.txb_AccountNum.Text.Trim()=="")
            {
                MessageBox.Show("登录账号不能为空！");
                this.txb_AccountNum.Focus();
                return;
            }
            if (this.txb_Password.Text.Trim()=="")
            {
                MessageBox.Show("登录密码不能为空！");
                this.txb_Password.Focus();
                return;
            }
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString =
                ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText =
                "SELECT COUNT(1) FROM tb_User WHERE No=@No AND Password=HASHBYTES('MD5',@Password);";
            sqlCommand.Parameters.AddWithValue("@No", this.txb_AccountNum.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@Password", this.txb_Password.Text.Trim());
            sqlCommand.Parameters["@Password"].SqlDbType = SqlDbType.VarChar;
            sqlConnection.Open();
            int rowAffected = (int)sqlCommand.ExecuteScalar();
            sqlConnection.Close();
            if (rowAffected == 1) 
            {
                MessageBox.Show("登录成功！");
            }
            else
	       {
                MessageBox.Show("登录失败！");        
            }
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
