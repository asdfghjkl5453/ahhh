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
namespace GradingSys
{
  
    public partial class frmStudentUI : Form
    {
        frmStudent f;
        public frmStudentUI(frmStudent f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void cboProgram_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Do you want to save this record?","Confirmation", MessageBoxButtons.YesNo , MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using(SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using(SqlCommand cm = new SqlCommand())
                        {
                            cn.Open();
                            cm.Connection = cn;
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "sp_student_add";
                            cm.Parameters.AddWithValue("@lrn", txtSno.Text);
                            cm.Parameters.AddWithValue("@lname", txtLname.Text);
                            cm.Parameters.AddWithValue("@fname", txtFname.Text);
                            cm.Parameters.AddWithValue("@mname", txtMname.Text);
                            cm.Parameters.AddWithValue("@program", cboProgram.Text);
                            cm.Parameters.AddWithValue("@address", txtAddress.Text);
                            cm.Parameters.AddWithValue("@contact", txtContact.Text);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Record has been successfully saved!", "Record saved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            f.getRecords();
                            button3_Click(sender, e);
                        }
                    }
                                                  }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtAddress.Clear();
            txtContact.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtMname.Clear();
            txtSno.Clear();
            cboProgram.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtSno.Enabled = true;
            txtSno.Focus();
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
                            cm.CommandText = "sp_student_update";
                            cm.Parameters.AddWithValue("@lrn", txtSno.Text);
                            cm.Parameters.AddWithValue("@lname", txtLname.Text);
                            cm.Parameters.AddWithValue("@fname", txtFname.Text);
                            cm.Parameters.AddWithValue("@mname", txtMname.Text);
                            cm.Parameters.AddWithValue("@program", cboProgram.Text);
                            cm.Parameters.AddWithValue("@address", txtAddress.Text);
                            cm.Parameters.AddWithValue("@contact", txtContact.Text);
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
