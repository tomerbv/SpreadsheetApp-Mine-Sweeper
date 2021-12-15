
namespace SpreadsheetApp
{
    partial class SpreadsheetApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Initiate = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.DONOTPRESS = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.Button();
            this.SetCell = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.GetCell = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Initiate
            // 
            this.Initiate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Initiate.Location = new System.Drawing.Point(13, 12);
            this.Initiate.Name = "Initiate";
            this.Initiate.Size = new System.Drawing.Size(106, 61);
            this.Initiate.TabIndex = 0;
            this.Initiate.Text = "Initiate";
            this.Initiate.UseVisualStyleBackColor = true;
            this.Initiate.Click += new System.EventHandler(this.Initiate_Click);
            // 
            // Load
            // 
            this.Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Load.Location = new System.Drawing.Point(125, 12);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(106, 61);
            this.Load.TabIndex = 1;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // DONOTPRESS
            // 
            this.DONOTPRESS.BackColor = System.Drawing.Color.DarkRed;
            this.DONOTPRESS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DONOTPRESS.ForeColor = System.Drawing.Color.White;
            this.DONOTPRESS.Location = new System.Drawing.Point(950, 12);
            this.DONOTPRESS.Name = "DONOTPRESS";
            this.DONOTPRESS.Size = new System.Drawing.Size(106, 61);
            this.DONOTPRESS.TabIndex = 2;
            this.DONOTPRESS.Text = "DO NOT PRESS";
            this.DONOTPRESS.UseVisualStyleBackColor = false;
            this.DONOTPRESS.Click += new System.EventHandler(this.DONOTPRESS_Click);
            // 
            // Search
            // 
            this.Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Search.Location = new System.Drawing.Point(573, 12);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(106, 61);
            this.Search.TabIndex = 3;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // SetCell
            // 
            this.SetCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SetCell.Location = new System.Drawing.Point(349, 12);
            this.SetCell.Name = "SetCell";
            this.SetCell.Size = new System.Drawing.Size(106, 61);
            this.SetCell.TabIndex = 4;
            this.SetCell.Text = "Set Cell";
            this.SetCell.UseVisualStyleBackColor = true;
            this.SetCell.Click += new System.EventHandler(this.SetCell_Click);
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Save.Location = new System.Drawing.Point(237, 12);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(106, 61);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // GetCell
            // 
            this.GetCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetCell.Location = new System.Drawing.Point(461, 12);
            this.GetCell.Name = "GetCell";
            this.GetCell.Size = new System.Drawing.Size(106, 61);
            this.GetCell.TabIndex = 6;
            this.GetCell.Text = "Get Cell";
            this.GetCell.UseVisualStyleBackColor = true;
            this.GetCell.Click += new System.EventHandler(this.GetCell_Click);
            // 
            // dataGridView1
            // 
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
            // 
            // SpreadsheetApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1086, 578);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.GetCell);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.SetCell);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.DONOTPRESS);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.Initiate);
            this.Name = "SpreadsheetApp";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Initiate;
        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.Button DONOTPRESS;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.Button SetCell;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button GetCell;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

