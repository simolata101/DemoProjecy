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
    public partial class ORegistration : Form
    {
        public ORegistration()
        {
            InitializeComponent();
        }
        OdbcConnection con = new OdbcConnection("dsn=indb");

        private void ORegistration_Load(object sender, EventArgs e)
        {
            display();
        }
        public void display()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT o_id as 'OWNER ID', school_id as 'SCHOOL ID', fullname as 'FULLNAME', Otype as 'OWNER TYPE', birthdate as 'BIRTHDATE', contact as 'CONTACT', address as 'ADDRESS',email as 'EMAIL' FROM t_register;", con);
                OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                DataSet ds = new DataSet();
                adptr.Fill(ds, "Empty");
                dgvRegOwn.DataSource = ds.Tables[0];
               
                
               
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            OdbcConnection cons = new OdbcConnection("dsn=indb");
            cons.Open();
            OdbcCommand commands = new OdbcCommand("SELECT o_id, school_id, fullname, Otype, birthdate, contact, address, email FROM t_register WHERE o_id LIKE '%" + txtSearch.Text + "%' OR school_id LIKE '%" + txtSearch.Text + "%' OR fullname LIKE '%" + txtSearch.Text + "%'", cons);
            OdbcDataAdapter adptrr = new OdbcDataAdapter(commands);
            DataTable dt = new DataTable();
            adptrr.Fill(dt);
            dgvRegOwn.DataSource = dt;
            con.Close();

            dgvRegOwn.Columns[0].HeaderText = "OWNER ID";
            dgvRegOwn.Columns[1].HeaderText = "SCHOOL ID";
            dgvRegOwn.Columns[3].HeaderText = "OWNER TYPE";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

    

           
    }
}
