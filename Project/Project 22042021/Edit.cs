using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_22042021
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
            DB db = new DB();
            db.openConnection();
            var name = new MySqlCommand("SELECT MAX("+uwu.iddelete+") FROM" + "`"+uwu.perem+"`", db.getConnection());
            var id = name.ExecuteScalar();
            for (int i = 1; i <= Convert.ToInt32(id); i++)
            {
                MySqlDataAdapter ad = new MySqlDataAdapter();
                DataTable stol = new DataTable();
                MySqlCommand cringe = new MySqlCommand("Select `id_pc` from `pc acc` WHERE `id_pc` = "+i, db.getConnection());
                ad.SelectCommand = cringe;
                ad.Fill(stol);
                if (stol.Rows.Count>0) 
                comboBox1.Items.Add(i);
            }
            switch (uwu.perem)
            {
                case "pc acc":
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("id_cond");
                    comboBox2.Items.Add("id_dept");
                    break;
                case "SOFTWARE":
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("softname");
                    comboBox2.Items.Add("softtype");
                    comboBox2.Items.Add("id_pc");
                    comboBox2.Items.Add("license_start");
                    comboBox2.Items.Add("license_end");
                    break;
                case "HARDWARE":
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("id_hardtype");
                    comboBox2.Items.Add("id_cond");
                    comboBox2.Items.Add("qty");
                    comboBox2.Items.Add("model");
                    break;
                case "PERI":
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("id_peritype");
                    comboBox2.Items.Add("id_cond");
                    comboBox2.Items.Add("qty");
                    comboBox2.Items.Add("id_model");
                    break;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand name = new MySqlCommand("UPDATE `"+uwu.perem+"` Set `"+comboBox2.Text+"` = '"+textBox3.Text+"' WHERE "+uwu.iddelete+" = "+ comboBox1.Text , db.getConnection());
            name.ExecuteNonQuery();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        Point lastPoint;

        private void Edit_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Edit_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
