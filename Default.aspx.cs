using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Npgsql;


/* Zainul Muhaimin
 * 
 * 
 * 
 */
namespace ASP_Project
{
    // Using DBHelper Class With out Keyword
    public static class DBHelper
    {

        public static bool ExecuteQuery(out DataTable dt, string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (NpgsqlConnection connStr = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString))
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new NpgsqlDataAdapter(cmd).Fill(ds);
                    cmd.Dispose();
                    cmd.Connection.Close();
                }
                catch (NpgsqlException)
                {
                    dt = null;
                    return false;
                }
                dt = ds.Tables[0];
                return true;
            }
        }

        public static bool ExecuteNonQuery(out string exception, string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
        {
            using (NpgsqlConnection connStr = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString))
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try 
                { 
                    cmd.Connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd.Connection.Close();
                    if (res == 0)
                    {
                        exception = "Not found";
                        return true;
                    }
                }
                catch (NpgsqlException ex) 
                {
                    exception = ex.Message;
                    return false; 
                }
                exception = null;
                return true;
            }
        }
    }
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Clear();
        }
        protected void Clear()
        {
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmessage.ForeColor = System.Drawing.Color.Green;
            lblmsg.Text = "";
            lblmessage.Text = "";
        }
        protected void btnInsertion_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtEmpNIM.Value) || string.IsNullOrWhiteSpace(txtEmpFname.Value) || string.IsNullOrWhiteSpace(txtEmpLname.Value))
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Tidak Boleh ada Input Kosong";
                return;
            }
            /* Insertion After Validations*/
            if (DBHelper.ExecuteNonQuery(out string ex, "Insert into mahasiswa values(@NIM,@Fname,@Lname,@Phone)", CommandType.Text, new NpgsqlParameter("@NIM", Convert.ToInt64(txtEmpNIM.Value)), new NpgsqlParameter("@Fname", txtEmpFname.Value), new NpgsqlParameter("@Lname", txtEmpLname.Value), new NpgsqlParameter("@Phone", txtEmpPhone.Value)))
            {
                lblmsg.Text = "Data Mahasiswa dengan NIM " + txtEmpNIM.Value + " Berhasil Di Simpan";
                txtEmpPhone.Value = "";
                txtEmpFname.Value = "";
                txtEmpNIM.Value = "";
                txtEmpLname.Value = "";
            }
            else 
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex;
            }
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            
            DataTable dt;
            if (DBHelper.ExecuteQuery(out dt, "Select * from mahasiswa", CommandType.Text))
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Data Mahasiswa Tidak Ditemukan";
            }
        }
        protected void btnUpdation_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtEmpNIM.Value) || string.IsNullOrWhiteSpace(txtEmpFname.Value) || string.IsNullOrWhiteSpace(txtEmpLname.Value))
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Tidak Boleh ada Input Kosong";
                return;
            }
            /* Updation After Validations*/
            if (DBHelper.ExecuteNonQuery(out string ex, "Update mahasiswa set firstname=@Fname,lastname=@Lname,phone=@Phone where nim=@NIM", CommandType.Text, new NpgsqlParameter("@NIM", Convert.ToInt64(txtEmpNIM.Value)), new NpgsqlParameter("@Fname", txtEmpFname.Value), new NpgsqlParameter("@Lname", txtEmpLname.Value), new NpgsqlParameter("@Phone", txtEmpPhone.Value)))
            {
                if (ex == null)
                {
                    lblmsg.Text = "Data Mahasiswa dengan NIM " + txtEmpNIM.Value + " Berhasil diupdate";
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Data Mahasiswa dengan NIM " + txtEmpNIM.Value + " Tidak ditemukan";
                }
                txtEmpPhone.Value = "";
                txtEmpFname.Value = "";
                txtEmpNIM.Value = "";
                txtEmpLname.Value = "";
            }
            else 
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex;
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtMhsNIM.Value))
            {
                lblmessage.ForeColor = System.Drawing.Color.Red;
                lblmessage.Text = "NIM Tidak Boleh Kosong";
                return;
            }
            if (DBHelper.ExecuteNonQuery(out string ex, "Delete from mahasiswa where nim=@NIM", CommandType.Text, new NpgsqlParameter("@NIM", Convert.ToInt64(txtMhsNIM.Value))))
            {
                if (ex == null)
                {
                    lblmessage.Text = "Data Mahasiswa dengan NIM " + txtMhsNIM.Value + " Telah Berhasil dihapus";
                }
                else 
                {
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    lblmessage.Text = "Data Mahasiswa dengan NIM " + txtMhsNIM.Value + " Tidak ditemukan";
                }
                txtMhsNIM.Value = "";
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex;
            }
        }
    }
}
