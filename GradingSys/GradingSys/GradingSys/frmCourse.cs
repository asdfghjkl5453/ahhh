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
    public partial class frmCourse : Form
    {
        public frmCourse()
        {
            InitializeComponent();
        }

        public void getRecords()
        {
            try
            {
                SqlDataReader dr;
                dataGridView1.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(Database.db))
                {
                    using (SqlCommand cm = new SqlCommand("select * from tblcourse", cn))
                    {
                        cn.Open();
                        dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            dataGridView1.Rows.Add(dr[0], dr[1]);
                        }
                        dr.Close();
                    }
                    cn.Close();
                }
                dataGridView1.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCourseUI f = new frmCourseUI(this);
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;

            try
            {
                if (colname == "colEdit")
                {
                    frmCourseUI f = new frmCourseUI(this);
                    f.txtCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.btnSave.Enabled = false;
                    f.txtCode.Enabled = false;
                    f.ShowDialog();
                }
                else if (colname == "colDelete")
                {
                    if (MessageBox.Show("Do you want to delete this record?", "Delete record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(Database.db))
                        {
                            using (SqlCommand cm = new SqlCommand("delete from tblcourse where ccode=@ccode", cn))
                            {
                                cn.Open();
                                cm.Parameters.AddWithValue("@ccode", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                                cm.ExecuteNonQuery();
                                cn.Close();
                                MessageBox.Show("Record has been successfully deleted!", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                getRecords();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
