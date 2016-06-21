namespace OPRPCharBuild
{
	partial class SelectOptions
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectOptions));
			this.comboBox_Options = new System.Windows.Forms.ComboBox();
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.label_Name = new System.Windows.Forms.Label();
			this.button_Custom = new System.Windows.Forms.Button();
			this.button_OK = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label_MaxRank = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboBox_Options
			// 
			this.comboBox_Options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Options.FormattingEnabled = true;
			this.comboBox_Options.Location = new System.Drawing.Point(12, 42);
			this.comboBox_Options.Name = "comboBox_Options";
			this.comboBox_Options.Size = new System.Drawing.Size(260, 21);
			this.comboBox_Options.TabIndex = 0;
			this.comboBox_Options.SelectedIndexChanged += new System.EventHandler(this.comboBox_Options_SelectedIndexChanged);
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(118, 69);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(154, 20);
			this.textBox_Name.TabIndex = 1;
			// 
			// label_Name
			// 
			this.label_Name.Location = new System.Drawing.Point(12, 69);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(100, 20);
			this.label_Name.TabIndex = 2;
			this.label_Name.Text = "Name:";
			this.label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button_Custom
			// 
			this.button_Custom.Location = new System.Drawing.Point(12, 126);
			this.button_Custom.Name = "button_Custom";
			this.button_Custom.Size = new System.Drawing.Size(75, 23);
			this.button_Custom.TabIndex = 3;
			this.button_Custom.Text = "Customize";
			this.toolTip1.SetToolTip(this.button_Custom, "You need the Rokushiki Master Trait to use this.");
			this.button_Custom.UseVisualStyleBackColor = true;
			this.button_Custom.Click += new System.EventHandler(this.button_Custom_Click);
			// 
			// button_OK
			// 
			this.button_OK.Location = new System.Drawing.Point(197, 126);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(75, 23);
			this.button_OK.TabIndex = 4;
			this.button_OK.Text = "Add";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 50;
			// 
			// label_MaxRank
			// 
			this.label_MaxRank.ForeColor = System.Drawing.Color.Red;
			this.label_MaxRank.Location = new System.Drawing.Point(12, 9);
			this.label_MaxRank.Name = "label_MaxRank";
			this.label_MaxRank.Size = new System.Drawing.Size(260, 30);
			this.label_MaxRank.TabIndex = 5;
			this.label_MaxRank.Text = "Label";
			this.label_MaxRank.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SelectOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 161);
			this.Controls.Add(this.label_MaxRank);
			this.Controls.Add(this.button_OK);
			this.Controls.Add(this.button_Custom);
			this.Controls.Add(this.label_Name);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.comboBox_Options);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SelectOptions";
			this.Text = "SelectOptions";
			this.Load += new System.EventHandler(this.SelectOptions_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox_Options;
		private System.Windows.Forms.TextBox textBox_Name;
		private System.Windows.Forms.Label label_Name;
		private System.Windows.Forms.Button button_Custom;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button_OK;
		private System.Windows.Forms.Label label_MaxRank;
	}
}