using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace VRMS___Management__12_01_21_
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //DATABASE
        OdbcConnection con = new OdbcConnection("dsn=indb");

        //TIME AND DATE
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            timer.Start();
            //lblTime.Text = DateTime.Now.ToString("hh:mm tt");
            lblDate.Text = DateTime.Now.ToString("MM - dd - yyyy");
            //lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        //TIMER
        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer.Start();
        }

        //CANCEL
        private void btnCancel_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        //FORM LOAD
        private void Login_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }

        //SHOW PASSWORD - CHECKBOX
        private void showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (showpass.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        //SHOW PASSWORD - LABEL
        private void lblShow_Click(object sender, EventArgs e)
        {
            if (showpass.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        //LOGIN
        private void btnLogin_Click(object sender, EventArgs e)
        {
            VRMS___Management__12_01_21_.Dashboard s = new Dashboard();
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT fullname,t_admin,admin_id,statuss FROM t_acc_admin WHERE username='" + txtUser.Text + "' AND ppassword='" + txtPass.Text + "';", con);
                OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                lblName.Text = dt.Rows[0][0].ToString();
                lblAccess.Text = dt.Rows[0][1].ToString();
                s.lblCurrent.Text = dt.Rows[0][0].ToString();
                lblAdminID.Text = dt.Rows[0][2].ToString();
                s.lblAdminID.Text = dt.Rows[0][2].ToString();
                lblStatus.Text = dt.Rows[0][3].ToString();
                con.Close();

                if (lblStatus.Text == "ONLINE")
                {
                    MessageBox.Show("This account is already login", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUser.Text = "";
                    txtPass.Text = "";
                }
                else
                {
                    if (lblAccess.Text == "OSAS")
                    {
                        try
                        {
                            s.Show();
                            this.Hide();
                            con.Open();
                            OdbcCommand cmd1 = new OdbcCommand();
                            cmd1 = con.CreateCommand();
                            cmd1.CommandText = "INSERT INTO loghistory(a_id,fullname,a_type,timee,eventt)VALUES(?,?,?,?,?)";
                            cmd1.Parameters.Add("@a_id", OdbcType.VarChar).Value = dt.Rows[0][2].ToString();
                            cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = dt.Rows[0][0].ToString();
                            cmd1.Parameters.Add("@a_type", OdbcType.VarChar).Value = "OSAS";
                            cmd1.Parameters.Add("@timee", OdbcType.VarChar).Value = lblDate.Text + " " + lblTime.Text;
                            cmd1.Parameters.Add("@eventt", OdbcType.VarChar).Value = "LOGIN";
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            login();
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //ENTER BUTTON
        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                VRMS___Management__12_01_21_.Dashboard s = new Dashboard();
                try
                {
                    OdbcCommand cmd = new OdbcCommand("SELECT fullname,t_admin,admin_id,statuss FROM t_acc_admin WHERE username='" + txtUser.Text + "' AND ppassword='" + txtPass.Text + "';", con);
                    OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adptr.Fill(dt);
                    lblName.Text = dt.Rows[0][0].ToString();
                    lblAccess.Text = dt.Rows[0][1].ToString();
                    s.lblCurrent.Text = dt.Rows[0][0].ToString();
                    lblAdminID.Text = dt.Rows[0][2].ToString();
                    s.lblAdminID.Text = dt.Rows[0][2].ToString();
                    lblStatus.Text = dt.Rows[0][3].ToString();
                    con.Close();

                    if (lblStatus.Text == "ONLINE")
                    {
                        MessageBox.Show("This account is already login", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Text = "";
                        txtPass.Text = "";
                    }
                    else
                    {
                        if (lblAccess.Text == "OSAS")
                        {
                            try
                            {
                                s.Show();
                                this.Hide();
                                con.Open();
                                OdbcCommand cmd1 = new OdbcCommand();
                                cmd1 = con.CreateCommand();
                                cmd1.CommandText = "INSERT INTO loghistory(a_id,fullname,a_type,timee,eventt)VALUES(?,?,?,?,?)";
                                cmd1.Parameters.Add("@a_id", OdbcType.VarChar).Value = dt.Rows[0][2].ToString();
                                cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = dt.Rows[0][0].ToString();
                                cmd1.Parameters.Add("@a_type", OdbcType.VarChar).Value = "OSAS";
                                cmd1.Parameters.Add("@timee", OdbcType.VarChar).Value = lblDate.Text + " " + lblTime.Text;
                                cmd1.Parameters.Add("@eventt", OdbcType.VarChar).Value = "LOGIN";
                                cmd1.ExecuteNonQuery();
                                con.Close();
                                login();
                            }
                            catch (Exception ex)
                            {
                                con.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //LOGIN FUNCTION
        public void login()
        {
            try
            {
                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "UPDATE t_acc_admin SET statuss='ONLINE' WHERE admin_id='" + lblAdminID.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                this.Hide();
                lblName.Text = "";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //KEYUP ENTER
        private void txtpass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                VRMS___Management__12_01_21_.Dashboard s = new Dashboard();
                try
                {
                    OdbcCommand cmd = new OdbcCommand("SELECT fullname,t_admin,admin_id,statuss FROM t_acc_admin WHERE username='" + txtUser.Text + "' AND ppassword='" + txtPass.Text + "';", con);
                    OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adptr.Fill(dt);
                    lblName.Text = dt.Rows[0][0].ToString();
                    lblAccess.Text = dt.Rows[0][1].ToString();
                    s.lblCurrent.Text = dt.Rows[0][0].ToString();
                    lblAdminID.Text = dt.Rows[0][2].ToString();
                    s.lblAdminID.Text = dt.Rows[0][2].ToString();
                    lblStatus.Text = dt.Rows[0][3].ToString();
                    con.Close();

                    if (lblStatus.Text == "ONLINE")
                    {
                        MessageBox.Show("This account is already login", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Text = "";
                        txtPass.Text = "";
                    }
                    else
                    {
                        if (lblAccess.Text == "OSAS")
                        {
                            try
                            {
                                s.Show();
                                this.Hide();
                                con.Open();
                                OdbcCommand cmd1 = new OdbcCommand();
                                cmd1 = con.CreateCommand();
                                cmd1.CommandText = "INSERT INTO loghistory(a_id,fullname,a_type,timee,eventt)VALUES(?,?,?,?,?)";
                                cmd1.Parameters.Add("@a_id", OdbcType.VarChar).Value = dt.Rows[0][2].ToString();
                                cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = dt.Rows[0][0].ToString();
                                cmd1.Parameters.Add("@a_type", OdbcType.VarChar).Value = "OSAS";
                                cmd1.Parameters.Add("@timee", OdbcType.VarChar).Value = lblDate.Text + " " + lblTime.Text;
                                cmd1.Parameters.Add("@eventt", OdbcType.VarChar).Value = "LOGIN";
                                cmd1.ExecuteNonQuery();
                                con.Close();
                                login();
                            }
                            catch (Exception ex)
                            {
                                con.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }




    }
}
