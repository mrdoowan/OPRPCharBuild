namespace OPRPCharBuild
{
    partial class Add_TechStats
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_Str = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Spe = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_Sta = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_Acc = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label_TotBuff = new System.Windows.Forms.Label();
            this.label_TotDebuff = new System.Windows.Forms.Label();
            this.textBox_BuffCalc = new System.Windows.Forms.TextBox();
            this.textBox_DebuffCalc = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.comboBox_Range = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label_MajorBuff = new System.Windows.Forms.Label();
            this.label_PostDur = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Str)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Spe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Sta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Acc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rank # Tech for (REASON)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Strength:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_Str
            // 
            this.numericUpDown_Str.Location = new System.Drawing.Point(101, 66);
            this.numericUpDown_Str.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Str.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Str.Name = "numericUpDown_Str";
            this.numericUpDown_Str.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_Str.TabIndex = 5;
            this.numericUpDown_Str.ValueChanged += new System.EventHandler(this.numericUpDown_Str_ValueChanged);
            // 
            // numericUpDown_Spe
            // 
            this.numericUpDown_Spe.Location = new System.Drawing.Point(101, 94);
            this.numericUpDown_Spe.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Spe.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Spe.Name = "numericUpDown_Spe";
            this.numericUpDown_Spe.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_Spe.TabIndex = 7;
            this.numericUpDown_Spe.ValueChanged += new System.EventHandler(this.numericUpDown_Spe_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Speed:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_Sta
            // 
            this.numericUpDown_Sta.Location = new System.Drawing.Point(101, 122);
            this.numericUpDown_Sta.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Sta.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Sta.Name = "numericUpDown_Sta";
            this.numericUpDown_Sta.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_Sta.TabIndex = 9;
            this.numericUpDown_Sta.ValueChanged += new System.EventHandler(this.numericUpDown_Sta_ValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Stamina:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_Acc
            // 
            this.numericUpDown_Acc.Location = new System.Drawing.Point(101, 150);
            this.numericUpDown_Acc.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Acc.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_Acc.Name = "numericUpDown_Acc";
            this.numericUpDown_Acc.Size = new System.Drawing.Size(48, 22);
            this.numericUpDown_Acc.TabIndex = 11;
            this.numericUpDown_Acc.ValueChanged += new System.EventHandler(this.numericUpDown_Acc_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "Accuracy:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_TotBuff
            // 
            this.label_TotBuff.Location = new System.Drawing.Point(155, 92);
            this.label_TotBuff.Name = "label_TotBuff";
            this.label_TotBuff.Size = new System.Drawing.Size(200, 24);
            this.label_TotBuff.TabIndex = 12;
            this.label_TotBuff.Text = "Total Buff:";
            this.label_TotBuff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_TotDebuff
            // 
            this.label_TotDebuff.Location = new System.Drawing.Point(155, 148);
            this.label_TotDebuff.Name = "label_TotDebuff";
            this.label_TotDebuff.Size = new System.Drawing.Size(200, 24);
            this.label_TotDebuff.TabIndex = 13;
            this.label_TotDebuff.Text = "Total Debuff:";
            this.label_TotDebuff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_BuffCalc
            // 
            this.textBox_BuffCalc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_BuffCalc.Location = new System.Drawing.Point(158, 68);
            this.textBox_BuffCalc.Name = "textBox_BuffCalc";
            this.textBox_BuffCalc.ReadOnly = true;
            this.textBox_BuffCalc.Size = new System.Drawing.Size(197, 15);
            this.textBox_BuffCalc.TabIndex = 16;
            this.textBox_BuffCalc.Text = "CALCULATION";
            this.textBox_BuffCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_DebuffCalc
            // 
            this.textBox_DebuffCalc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_DebuffCalc.Location = new System.Drawing.Point(158, 124);
            this.textBox_DebuffCalc.Name = "textBox_DebuffCalc";
            this.textBox_DebuffCalc.ReadOnly = true;
            this.textBox_DebuffCalc.Size = new System.Drawing.Size(197, 15);
            this.textBox_DebuffCalc.TabIndex = 17;
            this.textBox_DebuffCalc.Text = "CALCULATION";
            this.textBox_DebuffCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_OK
            // 
            this.button_OK.Enabled = false;
            this.button_OK.Location = new System.Drawing.Point(280, 181);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 18;
            this.button_OK.Text = "Load";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // comboBox_Range
            // 
            this.comboBox_Range.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Range.FormattingEnabled = true;
            this.comboBox_Range.Items.AddRange(new object[] {
            "",
            "Short AoE",
            "Medium AoE",
            "Long AoE"});
            this.comboBox_Range.Location = new System.Drawing.Point(101, 36);
            this.comboBox_Range.Name = "comboBox_Range";
            this.comboBox_Range.Size = new System.Drawing.Size(162, 24);
            this.comboBox_Range.TabIndex = 19;
            this.comboBox_Range.SelectedIndexChanged += new System.EventHandler(this.comboBox_Range_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "Range:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_MajorBuff
            // 
            this.label_MajorBuff.ForeColor = System.Drawing.Color.OrangeRed;
            this.label_MajorBuff.Location = new System.Drawing.Point(269, 36);
            this.label_MajorBuff.Name = "label_MajorBuff";
            this.label_MajorBuff.Size = new System.Drawing.Size(86, 24);
            this.label_MajorBuff.TabIndex = 21;
            this.label_MajorBuff.Text = "[MAJOR]";
            this.label_MajorBuff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_MajorBuff.Visible = false;
            // 
            // label_PostDur
            // 
            this.label_PostDur.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label_PostDur.Location = new System.Drawing.Point(15, 181);
            this.label_PostDur.Name = "label_PostDur";
            this.label_PostDur.Size = new System.Drawing.Size(259, 24);
            this.label_PostDur.TabIndex = 22;
            this.label_PostDur.Text = "Has a # Post Duration";
            this.label_PostDur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_PostDur.Visible = false;
            // 
            // Add_TechStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 213);
            this.Controls.Add(this.label_PostDur);
            this.Controls.Add(this.label_MajorBuff);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox_Range);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_DebuffCalc);
            this.Controls.Add(this.textBox_BuffCalc);
            this.Controls.Add(this.label_TotDebuff);
            this.Controls.Add(this.label_TotBuff);
            this.Controls.Add(this.numericUpDown_Acc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown_Sta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown_Spe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_Str);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Add_TechStats";
            this.Text = "Stat and Debuff Tech";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Str)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Spe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Sta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Acc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_Str;
        private System.Windows.Forms.NumericUpDown numericUpDown_Spe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_Sta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown_Acc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_TotBuff;
        private System.Windows.Forms.Label label_TotDebuff;
        private System.Windows.Forms.TextBox textBox_BuffCalc;
        private System.Windows.Forms.TextBox textBox_DebuffCalc;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.ComboBox comboBox_Range;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_MajorBuff;
        private System.Windows.Forms.Label label_PostDur;
    }
}