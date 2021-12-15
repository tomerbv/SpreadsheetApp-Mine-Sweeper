using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpreadsheetApp
{
    public partial class SpreadsheetApp : Form
    {
        static SharableSpreadSheet SSS;
        static bool PRESSED = false;
        static bool[,] found;
        public SpreadsheetApp()
        {
            InitializeComponent();
        }

        private void Initiate_Click(object sender, EventArgs e)
        {
            if (PRESSED)
                MessageBox.Show("There is no escape from the minefield");
            else
            {
                int nRow;
                int nCol;
                while (true)
                {
                    String row = Interaction.InputBox("Enter the desired row size for your Spread Sheet");
                    if (!row.All(char.IsDigit) || row == null || row == "")
                        MessageBox.Show("Please enter a possitive number");
                    else
                    {
                        nRow = Int32.Parse(row);
                        if (nRow < 1)
                            MessageBox.Show("Please enter a possitive number");
                        else
                            break;
                    }
                }

                while (true)
                {
                    String col = Interaction.InputBox("Enter the desired column size for your Spread Sheet");
                    if (!col.All(char.IsDigit) || col == null || col == "")
                        MessageBox.Show("It has to be a possitive number");
                    else
                    {
                        nCol = Int32.Parse(col);
                        if (nCol < 1)
                            MessageBox.Show("It has to be a possitive number");
                        else
                            break;
                    }
                }

                SSS = new SharableSpreadSheet(nRow, nCol);
                SSS.FillWithGarbage();
                UpdateValues(nRow, nCol);
            }

        }

        private void Load_Click(object sender, EventArgs e)
        {
            if (PRESSED)
                MessageBox.Show("There is no escape from the minefield");
            else
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                OFD.Filter = "dat files (*.dat)|*.dat";
                OFD.FilterIndex = 3;
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    String filePath = OFD.FileName;
                    SharableSpreadSheet temp = new SharableSpreadSheet(2, 2);
                    if (temp.load(filePath))
                    {
                        SSS = temp;
                        int row = 0, col = 0;
                        SSS.getSize(ref row, ref col);
                        UpdateValues(row, col);
                    }
                    else
                        MessageBox.Show("Failed to load the specified file");

                }
                else
                    MessageBox.Show("Failed to load the specified file");
            }


        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (PRESSED)
                MessageBox.Show("There is no escape from the minefield");
            else if (SSS == null)
                MessageBox.Show("First initiate or load a Spread Sheet");
            else
            {
                String FileName;
                while (true)
                {
                    FileName = Interaction.InputBox("Enter the desired name for your Spread Sheet");
                    if (FileName == null || FileName == "")
                        MessageBox.Show("Please enter a valid name");
                    else
                        break;
                }
                FileName = FileName + ".dat";
                if (SSS.save(FileName))
                    MessageBox.Show("File saved succesfully");
                else
                    MessageBox.Show("Failed to save the specified file");
            }




        }

        private void SetCell_Click(object sender, EventArgs e)
        {
            if (PRESSED)
                MessageBox.Show("There is no escape from the minefield");
            else if (SSS == null)
                MessageBox.Show("First initiate or load a Spread Sheet");
            else
            {
                String str, row, col;
                int nRow = 0, nCol = 0, rownum, colnum;
                SSS.getSize(ref nRow, ref nCol);
                while (true)
                {
                    row = Interaction.InputBox("Enter the desired row index for the cell you wish to set");
                    if (!row.All(char.IsDigit) || row == null || row == "")
                        MessageBox.Show("Please enter a possitive number in the dimensions range");
                    else
                    {
                        rownum = Int32.Parse(row);
                        if (rownum < 1 || rownum > nRow)
                            MessageBox.Show("Please enter a possitive number in the dimensions range");
                        else
                            break;
                    }
                }

                while (true)
                {
                    col = Interaction.InputBox("Enter the desired column index for the cell you wish to set");
                    if (!col.All(char.IsDigit) || col == null || col == "")
                        MessageBox.Show("Please enter a possitive number in the dimensions range");
                    else
                    {
                        colnum = Int32.Parse(col);
                        if (colnum < 1 || colnum > nCol)
                            MessageBox.Show("Please enter a possitive number in the dimensions range");
                        else
                            break;
                    }
                }
                while (true)
                {
                    str = Interaction.InputBox("Enter the desired string you wish to set");
                    if (str == null || str == "")
                        MessageBox.Show("Please enter a valid string");
                    else
                        break;
                }
                if (SSS.setCell(rownum, colnum, str))
                {
                    UpdateValues(nRow, nCol);
                    MessageBox.Show("String set to cell: [" + col + "," + row + "] succesfully");
                }


                else
                    MessageBox.Show("Failed to set the string to cell");
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (PRESSED)
                MessageBox.Show("There is no escape from the minefield");
            else if (SSS == null)
                MessageBox.Show("First initiate or load a Spread Sheet");
            else
            {
                String str = "";
                int row = 0, col = 0;
                while (true)
                {
                    str = Interaction.InputBox("Enter the desired string you wish to search");
                    if (str == null || str == "")
                        MessageBox.Show("Please enter a valid string");
                    else
                        break;
                }
                if (SSS.searchString(str, ref row, ref col))
                    MessageBox.Show("String " + "\"" + str + "\" found in cell: [" + col.ToString() + "," + row.ToString() + "]");
                else
                    MessageBox.Show("Could not find string");
            }
        }

        private void GetCell_Click(object sender, EventArgs e)
        {
            if (PRESSED)
                MessageBox.Show("There is no escape from the minefield");
            else if (SSS == null)
                MessageBox.Show("First initiate or load a Spread Sheet");
            else
            {
                String str, row, col;
                int nRow = 0, nCol = 0, rownum, colnum;
                SSS.getSize(ref nRow, ref nCol);
                while (true)
                {
                    row = Interaction.InputBox("Enter the desired row index for the cell you wish to get");
                    if (!row.All(char.IsDigit) || row == null || row == "")
                        MessageBox.Show("Please enter a possitive number in the dimensions range");
                    else
                    {
                        rownum = Int32.Parse(row);
                        if (rownum < 1 || rownum > nRow)
                            MessageBox.Show("Please enter a possitive number in the dimensions range");
                        else
                            break;
                    }
                }

                while (true)
                {
                    col = Interaction.InputBox("Enter the desired column index for the cell you wish to get");
                    if (!col.All(char.IsDigit) || col == null || col == "")
                        MessageBox.Show("Please enter a possitive number in the dimensions range");
                    else
                    {
                        colnum = Int32.Parse(col);
                        if (colnum < 1 || colnum > nCol)
                            MessageBox.Show("Please enter a possitive number in the dimensions range");
                        else
                            break;
                    }
                }
                str = SSS.getCell(rownum, colnum);
                if (!(str == null))
                {
                    MessageBox.Show("String found in cell [" + col + "," + row + "]: \"" + str + "\"");
                }
                else
                    MessageBox.Show("Cell [" + col + "," + row + "] is empty");
            }
        }

        private void UpdateValues(int row, int col)
        {
            dataGridView1.RowCount = row;
            dataGridView1.ColumnCount = col;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = SSS.getCell(i + 1, j + 1);
                }
            }
        }
        private void DONOTPRESS_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkRed;
            Random random = new Random();
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 10;
            for (int i = 0; i < 10; i++)
            {
                dataGridView1.Columns[i].Width = 45;
                dataGridView1.Rows[i].Height = 45;
                for (int j = 0; j < 10; j++)
                {
                    if (random.Next(1, 7) == 1)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = "¤";
                    }
                    else
                        dataGridView1.Rows[i].Cells[j].Value = "";
                    dataGridView1.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;

                }
            }
            found = new bool[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != "¤")
                    {
                        dataGridView1.Rows[i].Cells[j].Value = CloseBombNum(i, j);
                        found[i, j] = false;
                    }
                    else
                        found[i, j] = true;
                }
            }
            dataGridView1.Size = new Size(455, 455);
            dataGridView1.Location = new Point(250, 80);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SlateGray;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.MultiSelect = false;
            PRESSED = true;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!PRESSED)
                return;

            if(e.Button == MouseButtons.Right)
            {
                if ((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "¤"))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkOrange;
                    
                }
                else
                {
                    MessageBox.Show("Game Over");
                    FinishGame();
                }
                
            }
            else
            {
                if ((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "¤"))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.OrangeRed;
                    MessageBox.Show("Game Over");
                    FinishGame();
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.SlateGray;
                    found[e.RowIndex, e.ColumnIndex] = true;
                }
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (!found[i, j])
                            return;
                    }
                }
                MessageBox.Show("Congratulations You Win!");
                FinishGame();
            }
            

        }
        private void FinishGame()
        {
            this.BackColor = Color.SteelBlue;
            PRESSED = false;
            dataGridView1.Dispose();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(13, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(1043, 487);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.Controls.Add(this.dataGridView1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            if (SSS != null)
            {
                int row = 0, col = 0;
                SSS.getSize(ref row, ref col);
                UpdateValues(row, col);
            }
        }

        private void dataGridView1_CellRightMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
        } 
            private String CloseBombNum(int i, int j)
        {
            int sum = 0;
            if(i == 0)
            {
                if(j == 0)
                {
                    if (dataGridView1.Rows[i + 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i + 1].Cells[j + 1].Value == "¤")
                        sum++;
                }
                else if(j == 9)
                {
                    if (dataGridView1.Rows[i + 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i + 1].Cells[j - 1].Value == "¤")
                        sum++;
                }
                else
                {
                    if (dataGridView1.Rows[i + 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i + 1].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i + 1].Cells[j - 1].Value == "¤")
                        sum++;
                }
            }
            else if(i == 9)
            {
                if (j == 0)
                {
                    if (dataGridView1.Rows[i - 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j + 1].Value == "¤")
                        sum++;
                }
                else if (j == 9)
                {
                    if (dataGridView1.Rows[i - 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j - 1].Value == "¤")
                        sum++;
                }
                else
                {
                    if (dataGridView1.Rows[i - 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j - 1].Value == "¤")
                        sum++;
                }
            }
            else
            {
                if (j == 0)
                {
                    if (dataGridView1.Rows[i + 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i + 1].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j + 1].Value == "¤")
                        sum++;
                }
                else if (j == 9)
                {
                    if (dataGridView1.Rows[i + 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i + 1].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j - 1].Value == "¤")
                        sum++;
                }
                else
                {
                    if (dataGridView1.Rows[i].Cells[j + 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i + 1].Cells[j].Value == "¤")
                        sum++;

                    if (dataGridView1.Rows[i + 1].Cells[j + 1].Value == "¤")
                        sum++;

                    if (dataGridView1.Rows[i + 1].Cells[j - 1].Value == "¤")
                        sum++;
                    if (dataGridView1.Rows[i - 1].Cells[j].Value == "¤")
                        sum++;

                    if (dataGridView1.Rows[i - 1].Cells[j + 1].Value == "¤")
                        sum++;

                    if (dataGridView1.Rows[i - 1].Cells[j - 1].Value == "¤")
                        sum++;
                }
                
            }
            return sum.ToString();
        }

    }
}
