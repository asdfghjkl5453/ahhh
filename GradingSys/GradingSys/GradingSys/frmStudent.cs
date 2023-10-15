using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GradingSys
{
   
    public partial class frmStudent : Form
    {
        public frmStudent()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void getRecords()
        {
            try
            {
                SqlDataReader dr;
                dataGridView1.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(Database.db))
                {
                    using(SqlCommand cm = new SqlCommand("select * from tblstudent", cn))
                    {
                        cn.Open();
                        dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);
                        }
                        dr.Close();
                    }
                    cn.Close();
                }
                dataGridView1.ClearSelection();
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmStudentUI f = new frmStudentUI(this);
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;

            try
            {
                if(colname == "colEdit")
                {
                    frmStudentUI f = new frmStudentUI(this);
                    f.txtSno.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtLname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.txtFname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    f.txtMname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    f.cboProgram.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    f.txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    f.txtContact.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    f.btnSave.Enabled = false;
                    f.txtSno.Enabled = false;
                    f.ShowDialog();
                }
                else if(colname == "colView")
                {
                    frmStudentUI f = new frmStudentUI(this);
                    f.txtSno.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtLname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.txtFname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    f.txtMname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    f.cboProgram.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    f.txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    f.txtContact.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    f.btnSave.Enabled = false;
                    f.btnUpdate.Enabled = false;
                    f.ShowDialog();
                }else if(colname == "colDelete")
                {
                    if(MessageBox.Show("Do you want to delete this record?","Delete record",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using(SqlConnection cn = new SqlConnection(Database.db))
                        {
                            using (SqlCommand cm = new SqlCommand("delete from tblstudent where lrn=@lrn", cn))
                            {
                                cn.Open();
                                cm.Parameters.AddWithValue("@lrn", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                                cm.ExecuteNonQuery();
                                cn.Close();
                                MessageBox.Show("Record has been successfully deleted!", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                getRecords();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
