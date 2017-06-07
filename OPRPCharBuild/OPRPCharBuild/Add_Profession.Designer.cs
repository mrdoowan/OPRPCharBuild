namespace OPRPCharBuild
{
	partial class Add_Profession
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Profession));
            this.button_Add = new System.Windows.Forms.Button();
            this.richTextBox1_Desc = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Prof = new System.Windows.Forms.ComboBox();
            this.checkBox_Primary = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox2_Primary = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Custom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(466, 325);
            this.button_Add.Margin = new System.Windows.Forms.Padding(4);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(100, 28);
            this.button_Add.TabIndex = 9;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1_Desc
            // 
            this.richTextBox1_Desc.Location = new System.Drawing.Point(130, 90);
            this.richTextBox1_Desc.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1_Desc.Name = "richTextBox1_Desc";
            this.richTextBox1_Desc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1_Desc.Size = new System.Drawing.Size(435, 116);
            this.richTextBox1_Desc.TabIndex = 8;
            this.richTextBox1_Desc.Text = "";
            this.richTextBox1_Desc.TextChanged += new System.EventHandler(this.richTextBox1_Desc_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name:";
            // 
            // comboBox_Prof
            // 
            this.comboBox_Prof.FormattingEnabled = true;
            this.comboBox_Prof.Items.AddRange(new object[] {
            "Weapon Specialist",
            "Martial Artist",
            "Marksman",
            "Smith",
            "Carpenter",
            "Inventor",
            "Chef",
            "Entertainer",
            "Doctor",
            "Assassin",
            "Thief",
            "Merchant"});
            this.comboBox_Prof.Location = new System.Drawing.Point(130, 11);
            this.comboBox_Prof.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Prof.Name = "comboBox_Prof";
            this.comboBox_Prof.Size = new System.Drawing.Size(147, 24);
            this.comboBox_Prof.TabIndex = 10;
            this.comboBox_Prof.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox_Prof.TextUpdate += new System.EventHandler(this.comboBox1_TextUpdate);
            // 
            // checkBox_Primary
            // 
            this.checkBox_Primary.AutoSize = true;
            this.checkBox_Primary.Location = new System.Drawing.Point(456, 14);
            this.checkBox_Primary.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_Primary.Name = "checkBox_Primary";
            this.checkBox_Primary.Size = new System.Drawing.Size(75, 21);
            this.checkBox_Primary.TabIndex = 11;
            this.checkBox_Primary.Text = "Primary";
            this.checkBox_Primary.UseVisualStyleBackColor = true;
            this.checkBox_Primary.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 214);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Primary Bonus:";
            // 
            // richTextBox2_Primary
            // 
            this.richTextBox2_Primary.Enabled = false;
            this.richTextBox2_Primary.Location = new System.Drawing.Point(130, 214);
            this.richTextBox2_Primary.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox2_Primary.Name = "richTextBox2_Primary";
            this.richTextBox2_Primary.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2_Primary.Size = new System.Drawing.Size(435, 106);
            this.richTextBox2_Primary.TabIndex = 13;
            this.richTextBox2_Primary.Text = "";
            this.richTextBox2_Primary.TextChanged += new System.EventHandler(this.richTextBox2_Primary_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(290, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "You can cancel changes by closing the form.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Custom Name:";
            // 
            // textBox_Custom
            // 
            this.textBox_Custom.Location = new System.Drawing.Point(130, 49);
            this.textBox_Custom.Name = "textBox_Custom";
            this.textBox_Custom.Size = new System.Drawing.Size(147, 23);
            this.textBox_Custom.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(283, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Leave blank if no custom";
            // 
            // Add_Profession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 366);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_Custom);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTextBox2_Primary);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox_Primary);
            this.Controls.Add(this.comboBox_Prof);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.richTextBox1_Desc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Add_Profession";
            this.Text = "Add Profession";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_Add;
		private System.Windows.Forms.RichTextBox richTextBox1_Desc;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox_Prof;
		private System.Windows.Forms.CheckBox checkBox_Primary;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox richTextBox2_Primary;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Custom;
        private System.Windows.Forms.Label label6;
    }
}