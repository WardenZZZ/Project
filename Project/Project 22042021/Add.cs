using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_22042021
{
    public partial class Add : Form
    {
        DataGridView _dataGridView;
        public Add(DataGridView dataGridView)
        {
            InitializeComponent();
            this._dataGridView = dataGridView;

            Addtry();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Addtry()
        {
            DB db = new DB();
            System.Data.DataTable table = new System.Data.DataTable();
            MySqlDataAdapter ad = new MySqlDataAdapter();
            MySqlCommand prekol = new MySqlCommand(uwu.com, db.getConnection());
            ad.SelectCommand = prekol;
            ad.Fill(table);
            _dataGridView.DataSource = table;

            switch (uwu.perem)
            {
                case "pc acc":
                    comboBox3.Items.Clear();                  
                    comboBox2.Items.Clear();
                    var name = new MySqlCommand("SELECT MAX(id_cond) FROM `cond`", db.getConnection());
                    db.openConnection();

                    var id = name.ExecuteScalar();
                    for (int i = 1; i <= Convert.ToInt32(id); i++)
                    {
                        name = new MySqlCommand("SELECT condname FROM `cond` WHERE id_cond = " + i + "", db.getConnection());
                        comboBox2.Items.Add(i + " - " + Convert.ToString(name.ExecuteScalar()));
                    }

                    name = new MySqlCommand("SELECT MAX(id_dept) FROM `dept`", db.getConnection());
                    db.openConnection();

                    id = name.ExecuteScalar();
                    for (int i = 1; i <= Convert.ToInt32(id); i++)
                    {
                        name = new MySqlCommand("SELECT deptname FROM `dept` WHERE id_dept = " + i + "", db.getConnection());
                        comboBox3.Items.Add(i + " - " + Convert.ToString(name.ExecuteScalar()));
                    }
                    comboBox4.Hide();
                    label4.Location = new Point(41, 108);
                    label2.Text = "Состояние";
                    label4.Text = "Отдел";

                    textBox1.Hide();
                    textBox2.Hide();
                    textBox3.Hide();
                    textBox4.Hide();
                    textBox5.Hide();
                    label3.Hide();
                    label5.Hide();
                    label6.Hide();
                    dateTimePicker1.Hide();
                    dateTimePicker2.Hide();


                    break;
                case "SOFTWARE":
                    comboBox4.Items.Clear();
                    name = new MySqlCommand("SELECT MAX(id_typesoft) FROM `softtypes`", db.getConnection());
                    db.openConnection();

                    id = name.ExecuteScalar();
                    for (int i = 1; i <= Convert.ToInt32(id); i++)
                    {
                        name = new MySqlCommand("SELECT Name FROM `softtypes` WHERE id_typesoft = " + i + "", db.getConnection());
                        comboBox4.Items.Add(i + " - " + Convert.ToString(name.ExecuteScalar()));
                    }
                    
                    label4.Location = new Point(50, 108);
                    label2.Text = "Имя ПО";
                    label4.Text = "ПК";
                    label3.Text = "Тип ПО";
                    label5.Text = "Срок\nлицензии";

                    comboBox3.Hide();
                    comboBox2.Hide();
                    comboBox4.Show();
                    textBox1.Show();
                    textBox2.Show();
                    textBox3.Show();
                    textBox4.Hide();
                    textBox5.Hide();
                    label5.Show();
                    label6.Hide();
                    dateTimePicker1.Show();
                    dateTimePicker2.Show();
                    break;
                case "HARDWARE":
                    comboBox2.Items.Clear();
                    comboBox3.Items.Clear();
                    name = new MySqlCommand("SELECT MAX(id_hardtype) FROM `hardtypes`", db.getConnection());
                    db.openConnection();

                    id = name.ExecuteScalar();
                    for (int i = 1; i <= Convert.ToInt32(id); i++)
                    {
                        name = new MySqlCommand("SELECT name FROM `hardtypes` WHERE id_hardtype = " + i + "", db.getConnection());
                        comboBox2.Items.Add(i + " - " + Convert.ToString(name.ExecuteScalar()));
                    }

                    name = new MySqlCommand("SELECT MAX(id_cond) FROM `cond`", db.getConnection());
                    db.openConnection();

                    id = name.ExecuteScalar();
                    for (int i = 1; i <= Convert.ToInt32(id); i++)
                    {
                        name = new MySqlCommand("SELECT condname FROM `cond` WHERE id_cond = " + i + "", db.getConnection());
                        comboBox3.Items.Add(i + " - " + Convert.ToString(name.ExecuteScalar()));
                    }
                    label4.Location = new Point(12, 108);
                    label2.Text = "Комплект";
                    label4.Text = "Состояние";
                    label3.Text = "Кол-во";
                    label5.Text = "Модель";
                    comboBox2.Show();
                    comboBox3.Show();
                    comboBox4.Hide();
                    textBox3.Show();
                    textBox4.Show();
                    textBox5.Hide();
                    label6.Hide();
                    dateTimePicker1.Hide();
                    dateTimePicker2.Hide();

                    break;
                case "PERI":
                    comboBox2.Items.Clear();
                    comboBox3.Items.Clear();
                    name = new MySqlCommand("SELECT MAX(id_peritype) FROM `peritypes`", db.getConnection());
                    db.openConnection();

                    id = name.ExecuteScalar();
                    for (int i = 1; i <= Convert.ToInt32(id); i++)
                    {
                        name = new MySqlCommand("SELECT name FROM `peritypes` WHERE id_peritype = " + i + "", db.getConnection());
                        comboBox2.Items.Add(i + " - " + Convert.ToString(name.ExecuteScalar()));
                    }

                    name = new MySqlCommand("SELECT MAX(id_cond) FROM `cond`", db.getConnection());
                    db.openConnection();

                    id = name.ExecuteScalar();
                    for (int i = 1; i <= Convert.ToInt32(id); i++)
                    {
                        name = new MySqlCommand("SELECT condname FROM `cond` WHERE id_cond = " + i + "", db.getConnection());
                        comboBox3.Items.Add(i + " - " + Convert.ToString(name.ExecuteScalar()));
                    }
                    
                    label4.Location = new Point(12, 108);
                    label2.Text = "Название";
                    label4.Text = "Состояние";
                    label3.Text = "Кол-во";
                    label5.Text = "Модель";
                    comboBox2.Show();
                    comboBox3.Show();
                    comboBox4.Hide();
                    textBox3.Show();
                    textBox4.Show();
                    textBox5.Hide();
                    label6.Hide();
                    dateTimePicker1.Hide();
                    dateTimePicker2.Hide();
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (uwu.perem)
            {
                case "pc acc":
                    uwu.query = " INSERT INTO `pc acc` (id_cond,id_dept) VALUES ( " + (comboBox2.SelectedIndex + 1) + ", " + (comboBox3.SelectedIndex + 1) +" )";
                    break;
                case "SOFTWARE":
                    uwu.query = " INSERT INTO `software` (softname,id_pc,softtype,license_start,license_end) VALUES ( '" + textBox1.Text + "', " + textBox2.Text + ", " + (comboBox4.SelectedIndex + 1) + ", '" + dateTimePicker1.Text + "', '" + dateTimePicker2.Text + "' )";
                    break;
                case "HARDWARE":
                    uwu.query = " INSERT INTO `hardware` (id_hardtype,id_cond,qty,model) VALUES ( " + (comboBox2.SelectedIndex + 1) + ", " + (comboBox3.SelectedIndex + 1) + ", " + textBox3.Text + ", '" + textBox4.Text + "' )";
                    break;
                case "PERI":
                    uwu.query = " INSERT INTO `peri` (id_peritype,id_cond,qty,model) VALUES ( " + (comboBox2.SelectedIndex + 1) + ", " + (comboBox3.SelectedIndex + 1) + ", " + textBox3.Text + ", '" + textBox4.Text + "' )";
                    break;
            }
            zapol();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void zapol()
        {
            DB db = new DB();
            System.Data.DataTable table = new System.Data.DataTable();
            MySqlDataAdapter ad = new MySqlDataAdapter();
            MySqlCommand prekol = new MySqlCommand(uwu.query, db.getConnection());
            ad.SelectCommand = prekol;

            ad.Fill(table);

            _dataGridView.DataSource = table;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            textBox4.Text = dateTimePicker2.Text; 
        }

        private void Add_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        Point lastPoint;

        private void Add_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}
