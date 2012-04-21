namespace MapSplitJoinTool
{
    partial class FormMain
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
            this.groupBoxSplit = new System.Windows.Forms.GroupBox();
            this.buttonSplit = new System.Windows.Forms.Button();
            this.numericUpDownSplit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonMapSplitFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMapSplitPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonJoin = new System.Windows.Forms.Button();
            this.buttonMapJoinFind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMapJoinPath = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.checkBoxSaveImages = new System.Windows.Forms.CheckBox();
            this.groupBoxSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSplit
            // 
            this.groupBoxSplit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSplit.Controls.Add(this.checkBoxSaveImages);
            this.groupBoxSplit.Controls.Add(this.buttonSplit);
            this.groupBoxSplit.Controls.Add(this.numericUpDownSplit);
            this.groupBoxSplit.Controls.Add(this.label2);
            this.groupBoxSplit.Controls.Add(this.buttonMapSplitFind);
            this.groupBoxSplit.Controls.Add(this.label1);
            this.groupBoxSplit.Controls.Add(this.textBoxMapSplitPath);
            this.groupBoxSplit.Location = new System.Drawing.Point(13, 13);
            this.groupBoxSplit.Name = "groupBoxSplit";
            this.groupBoxSplit.Size = new System.Drawing.Size(414, 112);
            this.groupBoxSplit.TabIndex = 0;
            this.groupBoxSplit.TabStop = false;
            this.groupBoxSplit.Text = "Split map:";
            // 
            // buttonSplit
            // 
            this.buttonSplit.Location = new System.Drawing.Point(145, 71);
            this.buttonSplit.Name = "buttonSplit";
            this.buttonSplit.Size = new System.Drawing.Size(75, 23);
            this.buttonSplit.TabIndex = 5;
            this.buttonSplit.Text = "Split";
            this.buttonSplit.UseVisualStyleBackColor = true;
            this.buttonSplit.Click += new System.EventHandler(this.buttonSplit_Click);
            // 
            // numericUpDownSplit
            // 
            this.numericUpDownSplit.Location = new System.Drawing.Point(145, 45);
            this.numericUpDownSplit.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDownSplit.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSplit.Name = "numericUpDownSplit";
            this.numericUpDownSplit.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownSplit.TabIndex = 4;
            this.numericUpDownSplit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Map segment size (in tiles):";
            // 
            // buttonMapSplitFind
            // 
            this.buttonMapSplitFind.Location = new System.Drawing.Point(330, 17);
            this.buttonMapSplitFind.Name = "buttonMapSplitFind";
            this.buttonMapSplitFind.Size = new System.Drawing.Size(75, 23);
            this.buttonMapSplitFind.TabIndex = 2;
            this.buttonMapSplitFind.Text = "Find map...";
            this.buttonMapSplitFind.UseVisualStyleBackColor = true;
            this.buttonMapSplitFind.Click += new System.EventHandler(this.buttonMapSplitFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Map to split:";
            // 
            // textBoxMapSplitPath
            // 
            this.textBoxMapSplitPath.Location = new System.Drawing.Point(145, 19);
            this.textBoxMapSplitPath.Name = "textBoxMapSplitPath";
            this.textBoxMapSplitPath.Size = new System.Drawing.Size(179, 20);
            this.textBoxMapSplitPath.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonJoin);
            this.groupBox1.Controls.Add(this.buttonMapJoinFind);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxMapJoinPath);
            this.groupBox1.Location = new System.Drawing.Point(13, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 77);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Join map:";
            // 
            // buttonJoin
            // 
            this.buttonJoin.Location = new System.Drawing.Point(145, 45);
            this.buttonJoin.Name = "buttonJoin";
            this.buttonJoin.Size = new System.Drawing.Size(75, 23);
            this.buttonJoin.TabIndex = 5;
            this.buttonJoin.Text = "Join";
            this.buttonJoin.UseVisualStyleBackColor = true;
            this.buttonJoin.Click += new System.EventHandler(this.buttonJoin_Click);
            // 
            // buttonMapJoinFind
            // 
            this.buttonMapJoinFind.Location = new System.Drawing.Point(330, 17);
            this.buttonMapJoinFind.Name = "buttonMapJoinFind";
            this.buttonMapJoinFind.Size = new System.Drawing.Size(75, 23);
            this.buttonMapJoinFind.TabIndex = 2;
            this.buttonMapJoinFind.Text = "Find map...";
            this.buttonMapJoinFind.UseVisualStyleBackColor = true;
            this.buttonMapJoinFind.Click += new System.EventHandler(this.buttonMapJoinFind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Map to join:";
            // 
            // textBoxMapJoinPath
            // 
            this.textBoxMapJoinPath.Location = new System.Drawing.Point(145, 19);
            this.textBoxMapJoinPath.Name = "textBoxMapJoinPath";
            this.textBoxMapJoinPath.Size = new System.Drawing.Size(179, 20);
            this.textBoxMapJoinPath.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(19, 211);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // checkBoxSaveImages
            // 
            this.checkBoxSaveImages.AutoSize = true;
            this.checkBoxSaveImages.Location = new System.Drawing.Point(235, 75);
            this.checkBoxSaveImages.Name = "checkBoxSaveImages";
            this.checkBoxSaveImages.Size = new System.Drawing.Size(87, 17);
            this.checkBoxSaveImages.TabIndex = 6;
            this.checkBoxSaveImages.Text = "Save images";
            this.checkBoxSaveImages.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 230);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Split/Join Tool";
            this.groupBoxSplit.ResumeLayout(false);
            this.groupBoxSplit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSplit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSplit;
        private System.Windows.Forms.Button buttonSplit;
        private System.Windows.Forms.NumericUpDown numericUpDownSplit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonMapSplitFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMapSplitPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonJoin;
        private System.Windows.Forms.Button buttonMapJoinFind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMapJoinPath;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox checkBoxSaveImages;
    }
}

