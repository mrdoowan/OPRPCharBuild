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
			this.button1 = new System.Windows.Forms.Button();
			this.richTextBox1_Desc = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.richTextBox2_Primary = new System.Windows.Forms.RichTextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(463, 283);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 28);
			this.button1.TabIndex = 9;
			this.button1.Text = "Add";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// richTextBox1_Desc
			// 
			this.richTextBox1_Desc.Location = new System.Drawing.Point(127, 48);
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
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 17);
			this.label2.TabIndex = 7;
			this.label2.Text = "Description:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "Name:";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
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
			this.comboBox1.Location = new System.Drawing.Point(127, 11);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(147, 24);
			this.comboBox1.TabIndex = 10;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			this.comboBox1.TextUpdate += new System.EventHandler(this.comboBox1_TextUpdate);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(456, 14);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(75, 21);
			this.checkBox1.TabIndex = 11;
			this.checkBox1.Text = "Primary";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 172);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 17);
			this.label3.TabIndex = 12;
			this.label3.Text = "Primary Bonus:";
			// 
			// richTextBox2_Primary
			// 
			this.richTextBox2_Primary.Location = new System.Drawing.Point(127, 172);
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
			this.label4.Location = new System.Drawing.Point(166, 289);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(290, 17);
			this.label4.TabIndex = 14;
			this.label4.Text = "You can cancel changes by closing the form.";
			// 
			// Add_Profession
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 322);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.richTextBox2_Primary);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button1);
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

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1_Desc;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox richTextBox2_Primary;
		private System.Windows.Forms.Label label4;
	}
}