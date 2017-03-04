﻿namespace OPRPCharBuild
{
	partial class Add_Trait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Trait));
            this.label1 = new System.Windows.Forms.Label();
            this.label_CustMsg = new System.Windows.Forms.Label();
            this.textBox_TraitSpec = new System.Windows.Forms.TextBox();
            this.richTextBox_TraitDesc = new System.Windows.Forms.RichTextBox();
            this.numericUpDown_TraitProf = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_TraitGen = new System.Windows.Forms.NumericUpDown();
            this.comboBox_TraitName = new System.Windows.Forms.ComboBox();
            this.button_TraitAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TraitProf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TraitGen)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label_CustMsg
            // 
            this.label_CustMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label_CustMsg.Location = new System.Drawing.Point(40, 194);
            this.label_CustMsg.Name = "label_CustMsg";
            this.label_CustMsg.Size = new System.Drawing.Size(530, 34);
            this.label_CustMsg.TabIndex = 33;
            this.label_CustMsg.Text = "You can cancel changes by closing the form.";
            this.label_CustMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_TraitSpec
            // 
            this.textBox_TraitSpec.Enabled = false;
            this.textBox_TraitSpec.Location = new System.Drawing.Point(113, 42);
            this.textBox_TraitSpec.Name = "textBox_TraitSpec";
            this.textBox_TraitSpec.Size = new System.Drawing.Size(346, 23);
            this.textBox_TraitSpec.TabIndex = 32;
            // 
            // richTextBox_TraitDesc
            // 
            this.richTextBox_TraitDesc.Location = new System.Drawing.Point(113, 75);
            this.richTextBox_TraitDesc.Name = "richTextBox_TraitDesc";
            this.richTextBox_TraitDesc.Size = new System.Drawing.Size(538, 114);
            this.richTextBox_TraitDesc.TabIndex = 31;
            this.richTextBox_TraitDesc.Text = "";
            // 
            // numericUpDown_TraitProf
            // 
            this.numericUpDown_TraitProf.Location = new System.Drawing.Point(511, 43);
            this.numericUpDown_TraitProf.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_TraitProf.Name = "numericUpDown_TraitProf";
            this.numericUpDown_TraitProf.Size = new System.Drawing.Size(31, 23);
            this.numericUpDown_TraitProf.TabIndex = 30;
            // 
            // numericUpDown_TraitGen
            // 
            this.numericUpDown_TraitGen.Location = new System.Drawing.Point(511, 13);
            this.numericUpDown_TraitGen.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDown_TraitGen.Name = "numericUpDown_TraitGen";
            this.numericUpDown_TraitGen.Size = new System.Drawing.Size(31, 23);
            this.numericUpDown_TraitGen.TabIndex = 29;
            // 
            // comboBox_TraitName
            // 
            this.comboBox_TraitName.DropDownHeight = 150;
            this.comboBox_TraitName.FormattingEnabled = true;
            this.comboBox_TraitName.IntegralHeight = false;
            this.comboBox_TraitName.Location = new System.Drawing.Point(113, 12);
            this.comboBox_TraitName.Name = "comboBox_TraitName";
            this.comboBox_TraitName.Size = new System.Drawing.Size(346, 24);
            this.comboBox_TraitName.Sorted = true;
            this.comboBox_TraitName.TabIndex = 27;
            this.comboBox_TraitName.SelectedIndexChanged += new System.EventHandler(this.comboBox_TraitName_SelectedIndexChanged);
            this.comboBox_TraitName.MouseLeave += new System.EventHandler(this.comboBox_TraitName_MouseLeave);
            this.comboBox_TraitName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.comboBox_TraitName_MouseMove);
            // 
            // button_TraitAdd
            // 
            this.button_TraitAdd.Location = new System.Drawing.Point(576, 202);
            this.button_TraitAdd.Name = "button_TraitAdd";
            this.button_TraitAdd.Size = new System.Drawing.Size(75, 23);
            this.button_TraitAdd.TabIndex = 26;
            this.button_TraitAdd.Text = "Add";
            this.button_TraitAdd.UseVisualStyleBackColor = true;
            this.button_TraitAdd.Click += new System.EventHandler(this.button_TraitAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 34;
            this.label2.Text = "Specification:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 36;
            this.label4.Text = "Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(465, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 37;
            this.label5.Text = "Gen:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(466, 45);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 17);
            this.label6.TabIndex = 38;
            this.label6.Text = "Prof:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(548, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 57);
            this.label3.TabIndex = 39;
            this.label3.Text = "If you want more than one of the same trait, increment the # of Traits.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Add_Trait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 237);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_CustMsg);
            this.Controls.Add(this.textBox_TraitSpec);
            this.Controls.Add(this.richTextBox_TraitDesc);
            this.Controls.Add(this.numericUpDown_TraitProf);
            this.Controls.Add(this.numericUpDown_TraitGen);
            this.Controls.Add(this.comboBox_TraitName);
            this.Controls.Add(this.button_TraitAdd);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Add_Trait";
            this.Text = "Add Trait";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TraitProf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TraitGen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label_CustMsg;
		private System.Windows.Forms.TextBox textBox_TraitSpec;
		private System.Windows.Forms.RichTextBox richTextBox_TraitDesc;
		private System.Windows.Forms.NumericUpDown numericUpDown_TraitProf;
		private System.Windows.Forms.NumericUpDown numericUpDown_TraitGen;
		private System.Windows.Forms.ComboBox comboBox_TraitName;
		private System.Windows.Forms.Button button_TraitAdd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
    }
}