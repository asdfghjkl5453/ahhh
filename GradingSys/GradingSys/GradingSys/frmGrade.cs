using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradingSys
{
    public partial class frmGrade : Form
    {
        public frmGrade()
        {
            InitializeComponent();
        }

        private void cboCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void getRecords()
        {
            try
            {
                SqlDataReader dr;
                dataGridView1.Rows.Clear();
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand("select * from vw_Grade where ay = @ay and ccode = @ccode", cn))
                        {
                            cn.Open();
                            cm.Parameters.AddWithValue("@ay", cboAy.Text);
                            cm.Parameters.AddWithValue("@ccode", cboCode.Text);
                            dr = cm.ExecuteReader();
                            while (dr.Read())
                            {
                                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3],dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12]);
                            }
                            dr.Close();
                            cn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadAy()
        {
            try
            {
                SqlDataReader dr;
                cboAy.Items.Clear();
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand("select * from tblAy", cn))
                        {
                            cn.Open();
                            dr = cm.ExecuteReader();
                            while (dr.Read())
                            {
                                cboAy.Items.Add(dr[0]);
                            }
                            dr.Close();
                            cn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadCourse()
        {
            try
            {
                SqlDataReader dr;
                cboCode.Items.Clear();
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand("select * from tblCourse", cn))
                        {
                            cn.Open();
                            dr = cm.ExecuteReader();
                            while (dr.Read())
                            {
                                cboCode.Items.Add(dr[0]);
                            }
                            dr.Close();
                            cn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboAy_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRecords();
        }

        private void cboCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRecords();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    {
                        using (SqlConnection cn = new SqlConnection(Database.db))
                        {
                            using (SqlCommand cm = new SqlCommand("update tblGrade set prelim=@prelim, midterm=@midterm, prefi=@prefi, final=@final where id = @id", cn))
                            {
                                cn.Open();
                                cm.Parameters.AddWithValue("@prelim", double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));
                                cm.Parameters.AddWithValue("@midterm", double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString()));
                                cm.Parameters.AddWithValue("@prefi", double.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString()));
                                cm.Parameters.AddWithValue("@final", double.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString()));
                                cm.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value.ToString());
                                cm.ExecuteNonQuery();
                                cn.Close();
                            }
                        }
                    }
                }
                MessageBox.Show("Grade has been successfully saved!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
