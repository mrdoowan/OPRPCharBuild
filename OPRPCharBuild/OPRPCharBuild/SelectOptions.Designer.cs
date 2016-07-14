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
			this.button_Left = new System.Windows.Forms.Button();
			this.button_OK = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button_Mid = new System.Windows.Forms.Button();
			this.label_Msg = new System.Windows.Forms.Label();
			this.label_BottomHalf = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboBox_Options
			// 
			this.comboBox_Options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.comboBox_Options.FormattingEnabled = true;
			this.comboBox_Options.Location = new System.Drawing.Point(12, 60);
			this.comboBox_Options.Name = "comboBox_Options";
			this.comboBox_Options.Size = new System.Drawing.Size(260, 24);
			this.comboBox_Options.TabIndex = 0;
			this.comboBox_Options.SelectedIndexChanged += new System.EventHandler(this.comboBox_Options_SelectedIndexChanged);
			// 
			// textBox_Name
			// 
			this.textBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.textBox_Name.Location = new System.Drawing.Point(118, 96);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(154, 20);
			this.textBox_Name.TabIndex = 1;
			this.toolTip1.SetToolTip(this.textBox_Name, "Leave this field blank for no Customized Name");
			this.textBox_Name.WordWrap = false;
			// 
			// label_Name
			// 
			this.label_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label_Name.Location = new System.Drawing.Point(12, 87);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(100, 36);
			this.label_Name.TabIndex = 2;
			this.label_Name.Text = "Custom Name:";
			this.label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button_Left
			// 
			this.button_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button_Left.Location = new System.Drawing.Point(12, 126);
			this.button_Left.Name = "button_Left";
			this.button_Left.Size = new System.Drawing.Size(82, 25);
			this.button_Left.TabIndex = 3;
			this.button_Left.Text = "Customize";
			this.toolTip1.SetToolTip(this.button_Left, "You need the Rokushiki Master Trait to customize.");
			this.button_Left.UseVisualStyleBackColor = true;
			this.button_Left.Click += new System.EventHandler(this.button_Left_Click);
			// 
			// button_OK
			// 
			this.button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button_OK.Location = new System.Drawing.Point(192, 126);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(82, 25);
			this.button_OK.TabIndex = 4;
			this.button_OK.Text = "Add";
			this.toolTip1.SetToolTip(this.button_OK, "This will Add the default Rokushiki Technique and exit the Technique Creator");
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 50;
			// 
			// button_Mid
			// 
			this.button_Mid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button_Mid.Location = new System.Drawing.Point(100, 126);
			this.button_Mid.Name = "button_Mid";
			this.button_Mid.Size = new System.Drawing.Size(86, 25);
			this.button_Mid.TabIndex = 6;
			this.button_Mid.Text = "Reset";
			this.button_Mid.UseVisualStyleBackColor = true;
			this.button_Mid.Visible = false;
			this.button_Mid.Click += new System.EventHandler(this.button_Mid_Click);
			// 
			// label_Msg
			// 
			this.label_Msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label_Msg.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label_Msg.Location = new System.Drawing.Point(12, 9);
			this.label_Msg.Name = "label_Msg";
			this.label_Msg.Size = new System.Drawing.Size(260, 37);
			this.label_Msg.TabIndex = 5;
			this.label_Msg.Text = "Label";
			this.label_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_BottomHalf
			// 
			this.label_BottomHalf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label_BottomHalf.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label_BottomHalf.Location = new System.Drawing.Point(12, 52);
			this.label_BottomHalf.Name = "label_BottomHalf";
			this.label_BottomHalf.Size = new System.Drawing.Size(260, 25);
			this.label_BottomHalf.TabIndex = 7;
			this.label_BottomHalf.Text = "Label";
			this.label_BottomHalf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label_BottomHalf.Visible = false;
			// 
			// SelectOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 162);
			this.Controls.Add(this.button_Mid);
			this.Controls.Add(this.label_Msg);
			this.Controls.Add(this.button_OK);
			this.Controls.Add(this.button_Left);
			this.Controls.Add(this.label_Name);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label_BottomHalf);
			this.Controls.Add(this.comboBox_Options);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
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
		private System.Windows.Forms.Button button_Left;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button_OK;
		private System.Windows.Forms.Label label_Msg;
		private System.Windows.Forms.Button button_Mid;
		private System.Windows.Forms.Label label_BottomHalf;
	}
}