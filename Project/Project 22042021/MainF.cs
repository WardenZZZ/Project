using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Project_22042021
{
    public partial class MainF : Form
    {
        public MainF()
        {

            Form gen = new Export(this.dataGridView1);
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }
        private void MainF_Load(object sender, EventArgs e)
        {

        }

        //Переменные
        #region variables
        private Application ExcelApp;
        private Workbook ExcelWorkBook;
        private Worksheet ExcelWorkSheet;
        string filename;
        #endregion
        //Таблицы
        #region tables
        private void периферияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkboxenabled();
            uwu.perem = "PERI";
            uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond";
            uwu.slave = "( id_peri>1000 ";
            uwu.temp = uwu.com;
            uwu.privslave = uwu.slave;
            checkBox1.Tag = " OR c.condname = 'Активное'";
            checkBox2.Tag = " OR c.condname = 'Неактивное'";
            checkBox3.Tag = " OR c.condname = 'На складе'";
            checkBox4.Tag = " OR c.condname = 'Ремонт'";
            checkBox5.Tag = " OR c.condname = 'Списано'";
            sokrash();

            switchmeth();
        }

        private void pCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkboxenabled();
            uwu.perem = "pc acc";
            uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept ";
            uwu.temp = uwu.com;
            sokrash();
            uwu.slave = "( id_pc>1000 ";
            uwu.privslave = uwu.slave;
            checkBox1.Tag = " OR c.condname = 'Активное'";
            checkBox2.Tag = " OR c.condname = 'Неактивное'";
            checkBox3.Tag = " OR c.condname = 'На складе'";
            checkBox4.Tag = " OR c.condname = 'Ремонт'";
            checkBox5.Tag = " OR c.condname = 'Списано'";

            switchmeth();
        }

        private void пОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkboxenabled();
            uwu.perem ="SOFTWARE";
            uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc";
             uwu.temp = uwu.com;
            sokrash();
            dataGridView1.Columns[4].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "yyyy-MM-dd";
            uwu.slave = "( id_soft>1000 ";
            uwu.privslave = uwu.slave;
            checkBox1.Tag = " OR ss.Name = 'Специализированное'";
            checkBox2.Tag = " OR ss.Name = 'Общее'";
            checkBox3.Tag = " OR DATEDIFF(license_end,CURRENT_DATE)<=0";
            checkBox4.Tag = " OR DATEDIFF(license_end,CURRENT_DATE)<=30 and DATEDIFF(license_end,CURRENT_DATE)>0  ";
            switchmeth();

        }


        private void комплектующиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkboxenabled();
            uwu.perem = "HARDWARE";
            uwu.com = "SELECT h.id_hardware, hd.name, c.condname, h.qty, h.model FROM `hardware` h, `hardtypes` hd, `cond` c WHERE h.id_hardtype = hd.id_hardtype AND h.id_cond = c.id_cond";
            uwu.temp = uwu.com;
            uwu.slave = "( id_hardware>1000 ";
            uwu.privslave = uwu.slave;
            checkBox1.Tag = " OR c.condname = 'Активное'";
            checkBox2.Tag = " OR c.condname = 'Неактивное'";
            checkBox3.Tag = " OR c.condname = 'На складе'";
            checkBox4.Tag = " OR c.condname = 'Ремонт'";
            checkBox5.Tag = " OR c.condname = 'Списано'";
            sokrash();

            switchmeth();

        }
        #endregion
        //Методы
        #region methods
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        public void sokrash()
        {                    
            DB db = new DB();
            System.Data.DataTable table = new System.Data.DataTable();
            MySqlDataAdapter ad = new MySqlDataAdapter();
            MySqlCommand prekol = new MySqlCommand(uwu.com,db.getConnection());
            ad.SelectCommand = prekol;

             ad.Fill(table);

            dataGridView1.DataSource = table;
        }

        public void sokrash2( string kol)
        {     
            uwu.slave += kol;
        }
        public void filtr(System.Windows.Forms.CheckBox checkBox)
        {
            if (checkBox.Checked==false)            
                sokrash();          
            else
              if (checkBox.Checked == true)
                sokrash2(Convert.ToString(checkBox.Tag));
        }
        public void checkboxenabled()
        {
            checkBox1.CheckState = 0;
            checkBox2.CheckState = 0;
            checkBox3.CheckState = 0;
            checkBox4.CheckState = 0;
            checkBox5.CheckState = 0;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox1.Show();
            checkBox2.Show();
            checkBox3.Show();
            checkBox4.Show();
            checkBox5.Show();
            comboBox1.Show();
            comboBox1.Text = "";
        }

        public void switchmeth()
        {
            switch (uwu.perem)
            {
                case "pc acc":
                    uwu.iddelete = "id_pc";
                    checkBox1.Text = "Активное";
                    checkBox2.Text = "Неактивное";
                    checkBox3.Text = "На складе";
                    checkBox4.Text = "Ремонт";
                    checkBox5.Text = "Списано";
                    comboBox1.Show();
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Brazil");
                    comboBox1.Items.Add("Podolsk");
                    comboBox1.Items.Add("Redgrave");
                    comboBox1.Items.Add("Ukraine");
                    pCToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
                    пОToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    комплектующиеToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    периферияToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    отделToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    break;
                case "SOFTWARE":
                    uwu.iddelete = "id_soft";
                    checkBox1.Text = "Специализированное";
                    checkBox2.Text = "Общее";
                    checkBox3.Text = "Лицензия \n закончилась";
                    checkBox4.Text = "Лицензия закончится \n в течение 30 дней";
                    checkBox5.Text = "";
                    checkBox5.Hide();
                    comboBox1.Hide();
                    pCToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    пОToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
                    комплектующиеToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    периферияToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    отделToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    break;
                case "HARDWARE":
                    uwu.iddelete = "id_hardware";
                    checkBox1.Text = "Активное";
                    checkBox2.Text = "Неактивное";
                    checkBox3.Text = "На складе";
                    checkBox4.Text = "Ремонт";
                    checkBox5.Text = "Списано";
                    comboBox1.Show();
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("CPU");
                    comboBox1.Items.Add("GPU");
                    comboBox1.Items.Add("RAM");
                    comboBox1.Items.Add("Hardrive");
                    comboBox1.Items.Add("Motherboard");
                    comboBox1.Items.Add("PowerSupply");
                    comboBox1.Items.Add("Fan");
                    pCToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    пОToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    комплектующиеToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
                    периферияToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    отделToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    break;
                case "PERI":
                    uwu.iddelete = "id_peri";
                    checkBox1.Text = "Активное";
                    checkBox2.Text = "Неактивное";
                    checkBox3.Text = "На складе";
                    checkBox4.Text = "Ремонт";
                    checkBox5.Text = "Списано";
                    comboBox1.Show();
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("mouse");
                    comboBox1.Items.Add("keyboard");
                    comboBox1.Items.Add("headphones");
                    comboBox1.Items.Add("microphone");
                    comboBox1.Items.Add("Chair");
                    comboBox1.Items.Add("RGB tape");
                    comboBox1.Items.Add("gamepad");
                    comboBox1.Items.Add("webcam");
                    pCToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    пОToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    комплектующиеToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    периферияToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
                    отделToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    break;
                case "dept":
                    uwu.iddelete = "id_dept";
                    checkBox1.Hide();
                    checkBox2.Hide();
                    checkBox3.Hide();
                    checkBox4.Hide();
                    checkBox5.Hide();
                    comboBox1.Hide();
                    pCToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    пОToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    комплектующиеToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    периферияToolStripMenuItem.BackColor = SystemColors.GradientInactiveCaption;
                    отделToolStripMenuItem.BackColor = SystemColors.GradientActiveCaption;
                    break;
            }
        }
        public void word()
        {
                    Word.Document oDoc = new Word.Document();
            int RowCount = dataGridView1.Rows.Count;
            int ColumnCount = dataGridView1.Columns.Count;
            Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

            //add rows
            int r = 0;
            for (int c = 0; c <= ColumnCount - 1; c++)
            {
                for (r = 0; r <= RowCount - 1; r++)
                {
                    DataArray[r, c] = dataGridView1.Rows[r].Cells[c].Value;
                } //end row loop
            } //end column loop

            oDoc.Application.Visible = true;

            oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


            dynamic oRange = oDoc.Content.Application.Selection.Range;
            string oTemp = "";
            for (r = 0; r <= RowCount - 1; r++)
            {
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oTemp = oTemp + DataArray[r, c] + "\t";

                }
            }

            oRange.Text = oTemp;

            object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
            object ApplyBorders = true;
            object AutoFit = true;
            object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

            oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                  Type.Missing, Type.Missing, ref ApplyBorders,
                                  Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

            oRange.Select();

            oDoc.Application.Selection.Tables[1].Select();
            oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
            oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
            oDoc.Application.Selection.Tables[1].Rows[1].Select();
            oDoc.Application.Selection.InsertRowsAbove(1);
            oDoc.Application.Selection.Tables[1].Rows[1].Select();
            oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
            oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
            oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;
            for (int c = 0; c <= ColumnCount - 1; c++)
            {
                oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = dataGridView1.Columns[c].HeaderText;
            }
            oDoc.Application.Selection.Tables[1].set_Style("Таблица-сетка 4");
            oDoc.Application.Selection.Tables[1].Rows[1].Select();
            oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
            {
                Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                headerRange.Text = "Отчёт";
                headerRange.Font.Size = 16;
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            }
            oDoc.SaveAs2(Path.Combine(filename, textBox1.Text));

            oDoc.Close();
            Word.Application ioi = new Word.Application();
            ioi.Quit();
            if (oDoc != null)
            {
                try
                {
                    foreach (Process proc in Process.GetProcessesByName("WINWORD"))
                    {
                        proc.Kill();
                    }
                }
                finally { }
            }
        }
        //Excel
        public void Excel()
            {
                ExcelApp = new Application
                {
                    DisplayAlerts = false
                };

                const string template = "template.xlsx";

                ExcelWorkBook = ExcelApp.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));

                ExcelWorkSheet = ExcelWorkBook.ActiveSheet as Worksheet;

                sokrash();
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                    }
                }
                ExcelWorkBook.SaveAs(Path.Combine(filename, textBox1.Text));
                CloseExcel();
            }
        private void CloseExcel()
        {
            if (ExcelApp != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(ExcelApp.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(ExcelWorkSheet);
                ExcelWorkBook.Close();
                Marshal.ReleaseComObject(ExcelWorkBook);
                ExcelApp.Quit();
                Marshal.ReleaseComObject(ExcelApp);

                ExcelApp = null;
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally { }
            }
        }
    #endregion
    //Департаменты
    #region depts
    private void название1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uwu.perem = "DEPARTMENT BRAZIL";
            uwu.com = "SELECT DISTINCT d.id_dept, d.deptname, d.office, p.id_pc, c.condname, s.softname FROM `dept` d, `pc acc` p, `cond` c, software s WHERE d.deptname = " + "'" + название1ToolStripMenuItem + "'" + " AND p.id_dept = d.id_dept AND p.id_cond = c.id_cond AND s.id_pc = p.id_pc";

            sokrash();
        }

        private void название2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uwu.perem = "DEPARTMENT PODOLSK";
            uwu.com = "SELECT d.id_dept, d.deptname, d.office, p.id_pc, c.condname, s.softname FROM `dept` d, `pc acc` p, `cond` c, software s WHERE d.deptname = " + "'" + название2ToolStripMenuItem + "'" + " AND p.id_dept = d.id_dept AND p.id_cond = c.id_cond AND s.id_pc = p.id_pc";
            sokrash();
        }

        private void redgraveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uwu.perem = "DEPARTMENT REDGRAVE";
            uwu.com = "SELECT d.id_dept, d.deptname, d.office, p.id_pc, c.condname, s.softname FROM dept d, `pc acc` p, cond c, software s WHERE d.deptname = " + "'" + redgraveToolStripMenuItem + "'" + " AND p.id_dept = d.id_dept AND p.id_cond = c.id_cond AND s.id_pc = p.id_pc";
            sokrash();
        }

        private void ukraineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uwu.perem = "DEPARTMENT UKRAINE";
            uwu.com = "SELECT d.id_dept, d.deptname, d.office, p.id_pc, c.condname, s.softname FROM dept d, `pc acc` p, cond c, software s WHERE d.deptname = " + "'" + ukraineToolStripMenuItem + "'" + " AND p.id_dept = d.id_dept AND p.id_cond = c.id_cond AND s.id_pc = p.id_pc";
            sokrash();
        }
        private void отделToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uwu.perem = "dept";
            switchmeth();
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            Login logf = new Login();

            this.Close();

            logf.Show();

        }
        private void button5_Click(object sender, EventArgs e)
        {

            Form gen = new Export(this.dataGridView1);

            gen.Show();
        }

        private void Обновить_Click(object sender, EventArgs e)
        {
            uwu.slave = uwu.privslave;
            sokrash();
            checkboxenabled();
            switchmeth();
        }

        public void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                int i = 0;
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox5.Text))
                            {
                                dataGridView1.Rows[i].Cells[j].Selected = true;
                                break;
                            }
                }
            }
            else
            {
                dataGridView1.ClearSelection();
            }
        }

        private void Добавить_Click(object sender, EventArgs e)
        {
            Add add = new Add(dataGridView1);
            add.Show();

        }
        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            filtr(checkBox2);
            checkBox2.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {            
            filtr(checkBox1);
            checkBox1.Enabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            filtr(checkBox3);
            checkBox3.Enabled = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            filtr(checkBox4);
            checkBox4.Enabled = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            filtr(checkBox5);
            checkBox5.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uwu.slave = uwu.privslave;
            sokrash();
            checkboxenabled();
            switchmeth();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            switch (uwu.perem)
            {
                case "pc acc":
                    if (comboBox1.Text == "")
                    {
                        uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept AND (" + uwu.slave + " ) " + " ) ";
                    }
                    else if (checkBox1.Checked == false & checkBox2.Checked == false & checkBox3.Checked == false & checkBox4.Checked == false & checkBox5.Checked == false)
                    {
                        uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept AND ( d.deptname = '" + comboBox1.Text + "' ) ";

                    }
                    else
                    {
                        uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept AND ( d.deptname = '" + comboBox1.Text + "' AND " + uwu.slave + " ) " + " ) ";
                    }
                    break;
                case "SOFTWARE":
                    if (comboBox1.Text == "")
                    {
                        uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc AND ( " + uwu.slave + " ) " + " ) ";
                    }
                    break;
                case "HARDWARE":
                    if (comboBox1.Text == "")
                    {
                        uwu.com = "SELECT h.id_hardware, hd.name, c.condname, h.qty, h.model FROM `hardware` h, `hardtypes` hd, `cond` c WHERE h.id_hardtype = hd.id_hardtype AND h.id_cond = c.id_cond AND ( " + uwu.slave + " ) " + " ) ";
                    }
                    else if (checkBox1.Checked == false & checkBox2.Checked == false & checkBox3.Checked == false & checkBox4.Checked == false & checkBox5.Checked == false)
                    {
                        uwu.com = "SELECT h.id_hardware, hd.name, c.condname, h.qty, h.model FROM `hardware` h, `hardtypes` hd, `cond` c WHERE h.id_hardtype = hd.id_hardtype AND h.id_cond = c.id_cond AND ( hd.name = '" + comboBox1.Text + "' ) ";

                    }
                    else
                    {
                        uwu.com = "SELECT h.id_hardware, hd.name, c.condname, h.qty, h.model FROM `hardware` h, `hardtypes` hd, `cond` c WHERE h.id_hardtype = hd.id_hardtype AND h.id_cond = c.id_cond AND ( hd.name = '" + comboBox1.Text + "' AND " + uwu.slave + " ) " + " ) ";
                    }
                    break;
                case "PERI":
                    if (comboBox1.Text == "")
                    {
                        uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND ( " + uwu.slave + " ) " + " ) ";
                    }
                    else if (checkBox1.Checked == false & checkBox2.Checked == false & checkBox3.Checked == false & checkBox4.Checked == false & checkBox5.Checked == false)
                    {
                        uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND ( pt.name = '" + comboBox1.Text + "' ) ";

                    }
                    else
                    {
                        uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND ( pt.name = '" + comboBox1.Text + "' AND " + uwu.slave + " ) " + " ) ";
                    }
                    break;
            }
            sokrash();
            uwu.com = uwu.temp;
        }

        private void срокЛицензииИстекаетЧерез30ДнейToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void эксельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog2.ShowDialog();
                filename = folderBrowserDialog2.SelectedPath;
                uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc AND DATEDIFF(license_end, CURRENT_DATE)<= 30 and DATEDIFF(license_end, CURRENT_DATE)> 0";
                sokrash();
                Excel();
                CloseExcel();
            }
            catch (Exception)
            {
                CloseExcel();
            }
        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void вордToolStripMenuItem_Click(object sender, EventArgs e)
        {
                folderBrowserDialog2.ShowDialog();
                filename = folderBrowserDialog2.SelectedPath;
                uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc AND DATEDIFF(license_end, CURRENT_DATE)<= 30 and DATEDIFF(license_end, CURRENT_DATE)> 0";
                sokrash();
                word();
        }

        private void эксельToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept and c.condname = 'Ремонт' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept and c.condname = 'Ремонт' ";
            sokrash();
            word();
        }

        private void эксельToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept and c.condname = 'На складе' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_pc, c.condname , d.deptnameFROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept and c.condname = 'На складе' ";
            sokrash();
            word();
        }

        private void эксельToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept AND c.condname = 'Активное' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_pc, c.condname , d.deptname FROM `pc acc` p, `cond` c, `dept` d WHERE p.id_cond = c.id_cond AND p.id_dept = d.id_dept and c.condname = 'Активное' ";
            sokrash();
            word();
        }

        private void эксельToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND c.condname = 'Ремонт' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND c.condname = 'Ремонт' ";
            sokrash();
            word();
        }

        private void эксельToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND c.condname = 'На складе' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND c.condname = 'На складе' ";
            sokrash();
            word();
        }

        private void эксельToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND c.condname = 'Активное' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT p.id_peri, pt.name, c.condname, p.qty, p.model FROM `peri` p, `peritypes` pt, `cond` c WHERE p.id_peritype = pt.id_peritype AND p.id_cond = c.id_cond AND c.condname = 'Активное' ";
            sokrash();
            word();
        }

        private void эксельToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc AND ss.Name = 'Специализированное' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc AND ss.Name = 'Специализированное' ";
            sokrash();
            word();
        }

        private void эксельToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc AND ss.Name = 'Общее' ";
            sokrash();
            Excel();
            CloseExcel();
        }

        private void вордToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            filename = folderBrowserDialog2.SelectedPath;
            uwu.com = "SELECT s.id_soft, s.softname, ss.Name, p.id_pc, s.license_start, s.license_end FROM `software` s, `softtypes` ss, `pc acc` p WHERE s.softtype = ss.id_typesoft AND s.id_pc = p.id_pc AND ss.Name = 'Общее' ";
            sokrash();
            word();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                отчётыToolStripMenuItem.Enabled = false;

            }
            else
            {
                отчётыToolStripMenuItem.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            int rows = dataGridView1.CurrentCell.RowIndex;
            string rowsid = dataGridView1.Rows[rows].Cells[0].Value.ToString();
            MySqlCommand command = new MySqlCommand("DELETE FROM `" + uwu.perem + "` WHERE " + " `" + uwu.iddelete + "` = " + (rowsid), db.getConnection());
            db.openConnection();
            command.ExecuteNonQuery();
            dataGridView1.Rows.RemoveAt(rows);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Edit ed = new Edit();
            ed.Show();
        }

        private void MainF_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
