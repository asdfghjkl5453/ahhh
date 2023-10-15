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
    public partial class frmAY : Form
    {
        String _code="";
        public frmAY()
        {
            InitializeComponent();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save this academic year?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    String _code = txtCode.Text + " " + cboTerm.Text;
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand("insert into tblay(code, year, term)values(@code, @year, @term)",cn))
                        {
                            cn.Open();
                            cm.Parameters.AddWithValue("@code", _code);
                            cm.Parameters.AddWithValue("@year", txtCode.Text);
                            cm.Parameters.AddWithValue("@term", cboTerm.Text);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Record has been successfully saved!", "Record saved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            getRecords();
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
            cboTerm.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtCode.Focus();
        }

        public void getRecords()
        {
            try
            {
                SqlDataReader dr;
                dataGridView1.Rows.Clear();
                using (SqlConnection cn = new SqlConnection(Database.db))
                {
                    using (SqlCommand cm = new SqlCommand("select * from tblay", cn))
                    {
                        cn.Open();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update this academic year?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                 
                    using (SqlConnection cn = new SqlConnection(Database.db))
                    {
                        using (SqlCommand cm = new SqlCommand("update tblay set year=@year, term=@term where code =@code", cn))
                        {
                            cn.Open();                            
                            cm.Parameters.AddWithValue("@year", txtCode.Text);
                            cm.Parameters.AddWithValue("@term", cboTerm.Text);
                            cm.Parameters.AddWithValue("@code", _code);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Record has been successfully saved!", "Record saved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            getRecords();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;

            try
            {
                if (colname == "colEdit")
                {
                    
                    _code = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtCode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cboTerm.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    btnSave.Enabled = false;
                    txtCode.Enabled = true;
               
                }
                else if (colname == "colDelete")
                {
                    if (MessageBox.Show("Do you want to delete this record?", "Delete record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection cn = new SqlConnection(Database.db))
                        {
                            using (SqlCommand cm = new SqlCommand("delete from tblay where code=@code", cn))
                            {
                                cn.Open();
                                cm.Parameters.AddWithValue("@code", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
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
