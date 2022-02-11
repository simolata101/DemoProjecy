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
    public partial class SOwner : Form
    {
        public SOwner()
        {
            InitializeComponent();
        }
        OdbcConnection con = new OdbcConnection("dsn=indb");

        private void SOwner_Load(object sender, EventArgs e)
        {
            display();
        }
        public void display()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT qrtext as `VEHICLE ID`,o_id as `OWNER ID`, platen as `PLATE NUMBER`, type as `TYPE OF VEHICLE`, brand as `BRAND` FROM t_car_info;", con);
                OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                DataSet ds = new DataSet();
                adptr.Fill(ds, "Empty");
                dgvRegVec.DataSource = ds.Tables[0];
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
            //OdbcCommand commands = new OdbcCommand("select * from t_car_info where platen like '" + txtSearch.Text + "%' OR o_id like '" + txtSearch.Text + "%' OR type like '" + txtSearch.Text + "%'", cons);
            OdbcCommand commands = new OdbcCommand("SELECT qrtext, o_id, platen, type, brand FROM t_car_info WHERE platen LIKE '" + txtSearch.Text + "%' OR o_id LIKE '" + txtSearch.Text + "%' OR type LIKE '" + txtSearch.Text + "%' OR qrtext LIKE '" + txtSearch.Text + "%'", cons);
            OdbcDataAdapter adptrr = new OdbcDataAdapter(commands);
            DataTable dt = new DataTable();
            adptrr.Fill(dt);
            dgvRegVec.DataSource = dt;
            con.Close();

            dgvRegVec.Columns[0].HeaderText = "VEHICLE ID";
            dgvRegVec.Columns[1].HeaderText = "OWNER ID";
            dgvRegVec.Columns[2].HeaderText = "PLATE NUMBER";
            dgvRegVec.Columns[3].HeaderText = "TYPE OF VEHICLE";
        }
        }

       
    }
   

