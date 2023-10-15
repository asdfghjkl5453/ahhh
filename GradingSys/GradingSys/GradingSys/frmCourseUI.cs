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
    public partial class frmCourseUI : Form
    {
        frmCourse f;
        public frmCourseUI(frmCourse f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand())
                        {
                            cn.Open();
                            cm.Connection = cn;
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "sp_course_add";
                            cm.Parameters.AddWithValue("@ccode", txtCode.Text);
                            cm.Parameters.AddWithValue("@cdesc", txtDescription.Text);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Record has been successfully saved!", "Record saved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            f.getRecords();
                            button3_Click(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtDescription.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtCode.Enabled = true;
            txtCode.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand())
                        {
                            cn.Open();
                            cm.Connection = cn;
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "sp_course_update";
                            cm.Parameters.AddWithValue("@ccode", txtCode.Text);
                            cm.Parameters.AddWithValue("@cdesc", txtDescription.Text);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Record has been successfully updated!", "Record saved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            f.getRecords();
                            this.Dispose();
                        }
                    }
                }
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
