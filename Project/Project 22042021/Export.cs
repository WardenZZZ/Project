using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using Application = Microsoft.Office.Interop.Excel.Application;
using Word = Microsoft.Office.Interop.Word;



namespace Project_22042021
{
    public partial class Export : Form
    {
        private Application ExcelApp;
        private Workbook ExcelWorkBook;
        private Worksheet ExcelWorkSheet;
        DataGridView _dataGridView;
        Word.Document oDoc = new Word.Document();
        

        string path1;
        public Export(DataGridView dataGridView)
        {
            InitializeComponent();
            this._dataGridView = dataGridView;

        }
        public void sokrash()
        {
            DB db = new DB();
            System.Data.DataTable table = new System.Data.DataTable();
            MySqlDataAdapter ad = new MySqlDataAdapter();
            MySqlCommand prekol = new MySqlCommand(uwu.com, db.getConnection());
            ad.SelectCommand = prekol;
            ad.Fill(table);
            _dataGridView.DataSource = table;

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox1.CheckState = 0;
            if ((folderBrowserDialog1.ShowDialog()) == DialogResult.OK && (checkBox2.Checked = true))
            {
                path1 = folderBrowserDialog1.SelectedPath;

            }

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

        private void CloseWord()
        {
            if (oDoc != null)
            {
                int WordProcessId = -1;
                GetWindowThreadProcessId(ExcelApp.Hwnd, ref WordProcessId);

                oDoc = null;
                try
                {
                    Process process = Process.GetProcessById(WordProcessId);
                    process.Kill();
                }
                finally { }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);


        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            if ((checkBox1.Checked == true) && (checkBox2.Checked == false))
            {
                int RowCount = _dataGridView.Rows.Count;
                int ColumnCount = _dataGridView.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = _dataGridView.Rows[r].Cells[c].Value;
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
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = _dataGridView.Columns[c].HeaderText;
                }
                oDoc.Application.Selection.Tables[1].set_Style("Таблица-сетка 4");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text ="Отчёт по таблице " + uwu.perem;
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                oDoc.SaveAs2(Path.Combine(path1, textBox1.Text));

                oDoc.Close();
                Word.Application ioi = new Word.Application();
                ioi.Quit();
                try
                {
                    foreach (Process proc in Process.GetProcessesByName("WINWORD"))
                    {
                        proc.Kill();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //Excel
            if (checkBox2.Checked == true)
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
                    for (int i = 0; i < _dataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < _dataGridView.ColumnCount; j++)
                        {
                            ExcelApp.Cells[i + 1, j + 1] = _dataGridView.Rows[i].Cells[j].Value;
                        }
                    }
                    ExcelWorkBook.SaveAs(Path.Combine(path1, textBox1.Text));
                    CloseExcel();
                }
        }

        private void Export_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            }
        }
        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox1.CheckState = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            path1 = folderBrowserDialog1.SelectedPath;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox2.CheckState = 0;
        }

        private void Export_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new System.Drawing.Point(e.X, e.Y);

        }

        System.Drawing.Point lastPoint;

        private void Export_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
