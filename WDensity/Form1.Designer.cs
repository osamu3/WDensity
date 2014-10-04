namespace WDensity {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.picBxFrt = new System.Windows.Forms.PictureBox();
			this.picBxBck = new System.Windows.Forms.PictureBox();
			this.trckBrScl = new System.Windows.Forms.TrackBar();
			this.trckBrHrz = new System.Windows.Forms.TrackBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdBtnBckPicScl = new System.Windows.Forms.RadioButton();
			this.rdBtnFrtPicScl = new System.Windows.Forms.RadioButton();
			this.rdBtnBckPicMv = new System.Windows.Forms.RadioButton();
			this.rdBtnFrtPicMv = new System.Windows.Forms.RadioButton();
			this.trckBrVrt = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picBxFrt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picBxBck)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trckBrScl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trckBrHrz)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trckBrVrt)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel2.Controls.Add(this.trckBrVrt);
			this.splitContainer1.Size = new System.Drawing.Size(1773, 1038);
			this.splitContainer1.SplitterDistance = 1438;
			this.splitContainer1.SplitterWidth = 7;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.picBxFrt);
			this.splitContainer2.Panel1.Controls.Add(this.picBxBck);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.trckBrScl);
			this.splitContainer2.Panel2.Controls.Add(this.trckBrHrz);
			this.splitContainer2.Size = new System.Drawing.Size(1438, 1038);
			this.splitContainer2.SplitterDistance = 844;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			// 
			// picBxFrt
			// 
			this.picBxFrt.Location = new System.Drawing.Point(472, 396);
			this.picBxFrt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.picBxFrt.Name = "picBxFrt";
			this.picBxFrt.Size = new System.Drawing.Size(325, 238);
			this.picBxFrt.TabIndex = 2;
			this.picBxFrt.TabStop = false;
			// 
			// picBxBck
			// 
			this.picBxBck.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picBxBck.Location = new System.Drawing.Point(0, 0);
			this.picBxBck.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.picBxBck.Name = "picBxBck";
			this.picBxBck.Size = new System.Drawing.Size(1438, 844);
			this.picBxBck.TabIndex = 1;
			this.picBxBck.TabStop = false;
			// 
			// trckBrScl
			// 
			this.trckBrScl.AutoSize = false;
			this.trckBrScl.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.trckBrScl.LargeChange = 1;
			this.trckBrScl.Location = new System.Drawing.Point(0, 62);
			this.trckBrScl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.trckBrScl.Maximum = 90;
			this.trckBrScl.Minimum = 10;
			this.trckBrScl.Name = "trckBrScl";
			this.trckBrScl.Size = new System.Drawing.Size(1435, 50);
			this.trckBrScl.TabIndex = 1;
			this.trckBrScl.Value = 10;
			this.trckBrScl.Scroll += new System.EventHandler(this.trckBrScl_Scroll);
			// 
			// trckBrHrz
			// 
			this.trckBrHrz.AutoSize = false;
			this.trckBrHrz.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.trckBrHrz.LargeChange = 1;
			this.trckBrHrz.Location = new System.Drawing.Point(0, 4);
			this.trckBrHrz.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.trckBrHrz.Maximum = 500;
			this.trckBrHrz.Name = "trckBrHrz";
			this.trckBrHrz.Size = new System.Drawing.Size(1433, 50);
			this.trckBrHrz.TabIndex = 0;
			this.trckBrHrz.Value = 250;
			this.trckBrHrz.Scroll += new System.EventHandler(this.trckBrHrz_Scroll);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdBtnBckPicScl);
			this.groupBox1.Controls.Add(this.rdBtnFrtPicScl);
			this.groupBox1.Controls.Add(this.rdBtnBckPicMv);
			this.groupBox1.Controls.Add(this.rdBtnFrtPicMv);
			this.groupBox1.Location = new System.Drawing.Point(83, 14);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.groupBox1.Size = new System.Drawing.Size(240, 172);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// rdBtnBckPicScl
			// 
			this.rdBtnBckPicScl.AutoSize = true;
			this.rdBtnBckPicScl.Location = new System.Drawing.Point(20, 124);
			this.rdBtnBckPicScl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rdBtnBckPicScl.Name = "rdBtnBckPicScl";
			this.rdBtnBckPicScl.Size = new System.Drawing.Size(114, 22);
			this.rdBtnBckPicScl.TabIndex = 3;
			this.rdBtnBckPicScl.Text = "縮尺：背景";
			this.rdBtnBckPicScl.UseVisualStyleBackColor = true;
			// 
			// rdBtnFrtPicScl
			// 
			this.rdBtnFrtPicScl.AutoSize = true;
			this.rdBtnFrtPicScl.Location = new System.Drawing.Point(20, 92);
			this.rdBtnFrtPicScl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rdBtnFrtPicScl.Name = "rdBtnFrtPicScl";
			this.rdBtnFrtPicScl.Size = new System.Drawing.Size(114, 22);
			this.rdBtnFrtPicScl.TabIndex = 2;
			this.rdBtnFrtPicScl.Text = "縮尺：前景";
			this.rdBtnFrtPicScl.UseVisualStyleBackColor = true;
			// 
			// rdBtnBckPicMv
			// 
			this.rdBtnBckPicMv.AutoSize = true;
			this.rdBtnBckPicMv.Checked = true;
			this.rdBtnBckPicMv.Location = new System.Drawing.Point(20, 58);
			this.rdBtnBckPicMv.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rdBtnBckPicMv.Name = "rdBtnBckPicMv";
			this.rdBtnBckPicMv.Size = new System.Drawing.Size(114, 22);
			this.rdBtnBckPicMv.TabIndex = 1;
			this.rdBtnBckPicMv.TabStop = true;
			this.rdBtnBckPicMv.Text = "移動：背景";
			this.rdBtnBckPicMv.UseVisualStyleBackColor = true;
			// 
			// rdBtnFrtPicMv
			// 
			this.rdBtnFrtPicMv.AutoSize = true;
			this.rdBtnFrtPicMv.Location = new System.Drawing.Point(20, 26);
			this.rdBtnFrtPicMv.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.rdBtnFrtPicMv.Name = "rdBtnFrtPicMv";
			this.rdBtnFrtPicMv.Size = new System.Drawing.Size(114, 22);
			this.rdBtnFrtPicMv.TabIndex = 0;
			this.rdBtnFrtPicMv.Text = "移動：前景";
			this.rdBtnFrtPicMv.UseVisualStyleBackColor = true;
			// 
			// trckBrVrt
			// 
			this.trckBrVrt.AutoSize = false;
			this.trckBrVrt.BackColor = System.Drawing.SystemColors.ControlDark;
			this.trckBrVrt.LargeChange = 1;
			this.trckBrVrt.Location = new System.Drawing.Point(5, 4);
			this.trckBrVrt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.trckBrVrt.Maximum = 200;
			this.trckBrVrt.Name = "trckBrVrt";
			this.trckBrVrt.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trckBrVrt.Size = new System.Drawing.Size(69, 1280);
			this.trckBrVrt.TabIndex = 0;
			this.trckBrVrt.Value = 100;
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1773, 1038);
			this.Controls.Add(this.splitContainer1);
			this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picBxFrt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picBxBck)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trckBrScl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trckBrHrz)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trckBrVrt)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TrackBar trckBrVrt;
        private System.Windows.Forms.TrackBar trckBrHrz;
        private System.Windows.Forms.PictureBox picBxBck;
        private System.Windows.Forms.TrackBar trckBrScl;
        private System.Windows.Forms.PictureBox picBxFrt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdBtnBckPicScl;
        private System.Windows.Forms.RadioButton rdBtnFrtPicScl;
        private System.Windows.Forms.RadioButton rdBtnBckPicMv;
        private System.Windows.Forms.RadioButton rdBtnFrtPicMv;
    }
}

