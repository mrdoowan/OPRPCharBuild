namespace OPRPCharBuild
{
    partial class Add_Source
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Source));
            this.numericUpDown_BeliSource = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Datestamp = new System.Windows.Forms.CheckBox();
            this.richTextBox_NoteSource = new System.Windows.Forms.RichTextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.numericUpDown_SDSource = new System.Windows.Forms.NumericUpDown();
            this.label55 = new System.Windows.Forms.Label();
            this.dateTimePicker_DateStamp = new System.Windows.Forms.DateTimePicker();
            this.label54 = new System.Windows.Forms.Label();
            this.textBox_TitleSource = new System.Windows.Forms.TextBox();
            this.textBox_URLSource = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BeliSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SDSource)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_BeliSource
            // 
            this.numericUpDown_BeliSource.Increment = new decimal(new int[] {
            250000,
            0,
            0,
            0});
            this.numericUpDown_BeliSource.Location = new System.Drawing.Point(269, 47);
            this.numericUpDown_BeliSource.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown_BeliSource.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDown_BeliSource.Name = "numericUpDown_BeliSource";
            this.numericUpDown_BeliSource.Size = new System.Drawing.Size(118, 23);
            this.numericUpDown_BeliSource.TabIndex = 107;
            this.numericUpDown_BeliSource.ThousandsSeparator = true;
            // 
            // checkBox_Datestamp
            // 
            this.checkBox_Datestamp.AutoSize = true;
            this.checkBox_Datestamp.Checked = true;
            this.checkBox_Datestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Datestamp.Location = new System.Drawing.Point(62, 112);
            this.checkBox_Datestamp.Name = "checkBox_Datestamp";
            this.checkBox_Datestamp.Size = new System.Drawing.Size(124, 21);
            this.checkBox_Datestamp.TabIndex = 106;
            this.checkBox_Datestamp.Text = "Add Datestamp";
            this.checkBox_Datestamp.UseVisualStyleBackColor = true;
            // 
            // richTextBox_NoteSource
            // 
            this.richTextBox_NoteSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_NoteSource.Location = new System.Drawing.Point(472, 46);
            this.richTextBox_NoteSource.Name = "richTextBox_NoteSource";
            this.richTextBox_NoteSource.Size = new System.Drawing.Size(325, 83);
            this.richTextBox_NoteSource.TabIndex = 105;
            this.richTextBox_NoteSource.Text = "";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(420, 49);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(46, 17);
            this.label60.TabIndex = 104;
            this.label60.Text = "Note: ";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(224, 49);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(39, 17);
            this.label59.TabIndex = 103;
            this.label59.Text = "Beli: ";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(21, 49);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(35, 17);
            this.label56.TabIndex = 102;
            this.label56.Text = "SD: ";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_SDSource
            // 
            this.numericUpDown_SDSource.Location = new System.Drawing.Point(62, 47);
            this.numericUpDown_SDSource.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_SDSource.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_SDSource.Name = "numericUpDown_SDSource";
            this.numericUpDown_SDSource.Size = new System.Drawing.Size(57, 23);
            this.numericUpDown_SDSource.TabIndex = 101;
            this.numericUpDown_SDSource.ThousandsSeparator = true;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(10, 83);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(46, 17);
            this.label55.TabIndex = 100;
            this.label55.Text = "Date: ";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateTimePicker_DateStamp
            // 
            this.dateTimePicker_DateStamp.Location = new System.Drawing.Point(62, 83);
            this.dateTimePicker_DateStamp.Name = "dateTimePicker_DateStamp";
            this.dateTimePicker_DateStamp.Size = new System.Drawing.Size(241, 23);
            this.dateTimePicker_DateStamp.TabIndex = 99;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(13, 12);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(43, 17);
            this.label54.TabIndex = 98;
            this.label54.Text = "Title: ";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_TitleSource
            // 
            this.textBox_TitleSource.Location = new System.Drawing.Point(62, 12);
            this.textBox_TitleSource.Name = "textBox_TitleSource";
            this.textBox_TitleSource.Size = new System.Drawing.Size(325, 23);
            this.textBox_TitleSource.TabIndex = 97;
            // 
            // textBox_URLSource
            // 
            this.textBox_URLSource.Location = new System.Drawing.Point(472, 12);
            this.textBox_URLSource.Name = "textBox_URLSource";
            this.textBox_URLSource.Size = new System.Drawing.Size(325, 23);
            this.textBox_URLSource.TabIndex = 96;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(422, 12);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(44, 17);
            this.label44.TabIndex = 95;
            this.label44.Text = "URL: ";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(722, 135);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 108;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // Add_Source
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 166);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.numericUpDown_BeliSource);
            this.Controls.Add(this.checkBox_Datestamp);
            this.Controls.Add(this.richTextBox_NoteSource);
            this.Controls.Add(this.label60);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.label56);
            this.Controls.Add(this.numericUpDown_SDSource);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.dateTimePicker_DateStamp);
            this.Controls.Add(this.label54);
            this.Controls.Add(this.textBox_TitleSource);
            this.Controls.Add(this.textBox_URLSource);
            this.Controls.Add(this.label44);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(825, 205);
            this.MinimumSize = new System.Drawing.Size(825, 205);
            this.Name = "Add_Source";
            this.Text = "Add Source";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BeliSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SDSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown_BeliSource;
        private System.Windows.Forms.CheckBox checkBox_Datestamp;
        private System.Windows.Forms.RichTextBox richTextBox_NoteSource;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.NumericUpDown numericUpDown_SDSource;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.DateTimePicker dateTimePicker_DateStamp;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox textBox_TitleSource;
        private System.Windows.Forms.TextBox textBox_URLSource;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Button button_OK;
    }
}