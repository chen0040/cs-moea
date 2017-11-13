namespace MOEA.AnalyzerGUI
{
    partial class FrmAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtStatus3 = new System.Windows.Forms.TextBox();
            this.txtStatus2 = new System.Windows.Forms.TextBox();
            this.txtStatus1 = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.cboProblem = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboAlgorithm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chtParetoFront = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvParetoFront = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.wbProblem = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParetoFront)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(827, 575);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtStatus3);
            this.panel1.Controls.Add(this.txtStatus2);
            this.panel1.Controls.Add(this.txtStatus1);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnEvaluate);
            this.panel1.Controls.Add(this.cboProblem);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboAlgorithm);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 575);
            this.panel1.TabIndex = 0;
            // 
            // txtStatus3
            // 
            this.txtStatus3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus3.Location = new System.Drawing.Point(12, 263);
            this.txtStatus3.Name = "txtStatus3";
            this.txtStatus3.Size = new System.Drawing.Size(174, 20);
            this.txtStatus3.TabIndex = 3;
            // 
            // txtStatus2
            // 
            this.txtStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus2.Location = new System.Drawing.Point(12, 237);
            this.txtStatus2.Name = "txtStatus2";
            this.txtStatus2.Size = new System.Drawing.Size(174, 20);
            this.txtStatus2.TabIndex = 3;
            // 
            // txtStatus1
            // 
            this.txtStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus1.Location = new System.Drawing.Point(12, 211);
            this.txtStatus1.Name = "txtStatus1";
            this.txtStatus1.Size = new System.Drawing.Size(174, 20);
            this.txtStatus1.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(12, 158);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(174, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEvaluate.Location = new System.Drawing.Point(12, 134);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(174, 23);
            this.btnEvaluate.TabIndex = 2;
            this.btnEvaluate.Text = "Evaluate";
            this.btnEvaluate.UseVisualStyleBackColor = true;
            this.btnEvaluate.Click += new System.EventHandler(this.btnEvaluate_Click);
            // 
            // cboProblem
            // 
            this.cboProblem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboProblem.FormattingEnabled = true;
            this.cboProblem.Location = new System.Drawing.Point(12, 84);
            this.cboProblem.Name = "cboProblem";
            this.cboProblem.Size = new System.Drawing.Size(174, 21);
            this.cboProblem.TabIndex = 1;
            this.cboProblem.SelectedIndexChanged += new System.EventHandler(this.cboProblem_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Gray;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Progress:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Gray;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Command:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Gray;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Problem:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboAlgorithm
            // 
            this.cboAlgorithm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAlgorithm.FormattingEnabled = true;
            this.cboAlgorithm.Location = new System.Drawing.Point(12, 34);
            this.cboAlgorithm.Name = "cboAlgorithm";
            this.cboAlgorithm.Size = new System.Drawing.Size(174, 21);
            this.cboAlgorithm.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Algorithm:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(623, 575);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chtParetoFront);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(615, 549);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Pareto Front";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chtParetoFront
            // 
            this.chtParetoFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtParetoFront.Location = new System.Drawing.Point(3, 3);
            this.chtParetoFront.Name = "chtParetoFront";
            this.chtParetoFront.ScrollGrace = 0D;
            this.chtParetoFront.ScrollMaxX = 0D;
            this.chtParetoFront.ScrollMaxY = 0D;
            this.chtParetoFront.ScrollMaxY2 = 0D;
            this.chtParetoFront.ScrollMinX = 0D;
            this.chtParetoFront.ScrollMinY = 0D;
            this.chtParetoFront.ScrollMinY2 = 0D;
            this.chtParetoFront.Size = new System.Drawing.Size(609, 543);
            this.chtParetoFront.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvParetoFront);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(615, 549);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parent Front Tabular";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvParetoFront
            // 
            this.dgvParetoFront.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParetoFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParetoFront.Location = new System.Drawing.Point(3, 3);
            this.dgvParetoFront.Name = "dgvParetoFront";
            this.dgvParetoFront.Size = new System.Drawing.Size(609, 543);
            this.dgvParetoFront.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.wbProblem);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(615, 549);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Problem";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // wbProblem
            // 
            this.wbProblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbProblem.Location = new System.Drawing.Point(0, 0);
            this.wbProblem.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbProblem.Name = "wbProblem";
            this.wbProblem.Size = new System.Drawing.Size(615, 549);
            this.wbProblem.TabIndex = 0;
            // 
            // FrmAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 575);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmAnalyzer";
            this.Text = "MOEA Analyzer";
            this.Load += new System.EventHandler(this.FrmMOEA_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParetoFront)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ZedGraph.ZedGraphControl chtParetoFront;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboProblem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboAlgorithm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtStatus3;
        private System.Windows.Forms.TextBox txtStatus2;
        private System.Windows.Forms.TextBox txtStatus1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvParetoFront;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.WebBrowser wbProblem;

    }
}

