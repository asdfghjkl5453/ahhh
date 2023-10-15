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
using static System.Runtime.CompilerServices.RuntimeHelpers;


namespace GradingSys
{
    public partial class frmEnrollUI : Form
    {
       
        public frmEnrollUI()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void loadStudent()
        {
            try
            {
                SqlDataReader dr;
                cboSelect.Items.Clear();
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand("select concat(lrn , ' | '  , lname , ', ' , fname , ' ' , mname) as student from tblStudent",cn))
                        {                         
                            cn.Open();
                            dr = cm.ExecuteReader();
                            while (dr.Read())
                            {
                                cboSelect.Items.Add(dr[0]);
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

        private void cboSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            try
            {
                SqlDataReader dr;
                cboSelect.Items.Clear();
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand("select * from tblStudent where concat(lrn , ' | '  , lname , ', ' , fname , ' ' , mname) = @student", cn))
                        {
                            cn.Open();
                            cm.Parameters.AddWithValue("@student", cboSelect.Text);
                            dr = cm.ExecuteReader();
                            while (dr.Read())
                            {
                                txtSno.Text = dr[0].ToString();
                                txtLname.Text = dr[1].ToString();
                                txtFname.Text = dr[2].ToString();
                                txtMname.Text = dr[3].ToString();
                                txtProgram.Text = dr[4].ToString();
                            }
                            dr.Close();
                            cn.Close();
                        }
                    }
                }
                loadStudent();
                getRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboAy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = false;
                SqlDataReader dr;
                using (SqlConnection cn = new SqlConnection(Database.db))
                {
                    using (SqlCommand cm = new SqlCommand("select * from tblGrade where lrn =@lrn and ccode = @ccode and ay=@ay",cn))
                    {
                        cn.Open();
                        cm.Parameters.AddWithValue("@lrn", txtSno.Text);
                        cm.Parameters.AddWithValue("@ccode", cboCode.Text);
                        cm.Parameters.AddWithValue("@ay", cboAy.Text);
                        dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            found = true;
                        }
                        dr.Close();
                        cn.Close();
                    }
                }

                if(found == true)
                {
                    MessageBox.Show("Unable to saved!", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to save this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand())
                        {
                            cn.Open();
                            cm.Connection = cn;
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "sp_classlist_add";
                            cm.Parameters.AddWithValue("@lrn", txtSno.Text);
                            cm.Parameters.AddWithValue("@ccode", cboCode.Text);
                            cm.Parameters.AddWithValue("@ay", cboAy.Text);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Record has been successfully saved!", "Record saved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            getRecords();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void getRecords()
        {
            try
            {
                SqlDataReader dr;
                dataGridView1.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(Database.db))
                {
                    using (SqlCommand cm = new SqlCommand("select id, ay, ccode from tblgrade where ay = @ay and lrn = @lrn", cn))
                    {
                        cn.Open();
                        cm.Parameters.AddWithValue("@ay", cboAy.Text);
                        cm.Parameters.AddWithValue("lrn", txtSno.Text);
                        dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);
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

        private void cboAy_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRecords();
        }
    }
}
