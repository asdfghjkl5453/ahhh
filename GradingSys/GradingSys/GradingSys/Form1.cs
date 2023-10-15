namespace GradingSys
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int intX = Screen.PrimaryScreen.Bounds.Width;
            int intY = Screen.PrimaryScreen.Bounds.Height;
            this.Width = intX - 150;
            this.Height = intY - 150;
            this.Left = 75;
            this.Top = 75;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmStudent f = new frmStudent();
            f.TopLevel = false;
            panel1.Controls.Add(f);
            f.getRecords();
            f.BringToFront();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCourse f = new frmCourse();
            f.TopLevel = false;
            panel1.Controls.Add(f);
            f.getRecords();
            f.BringToFront();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmAY f = new frmAY();
            f.TopLevel = false;
            panel1.Controls.Add(f);
            f.getRecords();
            f.BringToFront();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmEnrollUI f = new frmEnrollUI();
            f.loadStudent();
            f.loadAy();
            f.loadCourse();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmGrade f = new frmGrade();
            f.TopLevel = false;
            panel1.Controls.Add(f);
            f.loadAy();
            f.loadCourse();
            f.BringToFront();
            f.Show();
        }
    }
}