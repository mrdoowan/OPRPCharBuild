namespace OPRPCharBuild
{
	partial class Sheet
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sheet));
			this.richTextBox_Template = new System.Windows.Forms.RichTextBox();
			this.label_Title = new System.Windows.Forms.Label();
			this.button_Close = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// richTextBox_Template
			// 
			this.richTextBox_Template.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox_Template.Font = new System.Drawing.Font("Courier New", 9F);
			this.richTextBox_Template.Location = new System.Drawing.Point(12, 43);
			this.richTextBox_Template.Name = "richTextBox_Template";
			this.richTextBox_Template.ReadOnly = true;
			this.richTextBox_Template.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richTextBox_Template.Size = new System.Drawing.Size(510, 623);
			this.richTextBox_Template.TabIndex = 0;
			this.richTextBox_Template.Text = "";
			this.richTextBox_Template.MouseClick += new System.Windows.Forms.MouseEventHandler(this.richTextBox_Template_MouseClick);
			// 
			// label_Title
			// 
			this.label_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
			this.label_Title.Location = new System.Drawing.Point(12, 9);
			this.label_Title.Name = "label_Title";
			this.label_Title.Size = new System.Drawing.Size(510, 31);
			this.label_Title.TabIndex = 4;
			this.label_Title.Text = "Character Template Sheet";
			this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button_Close
			// 
			this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button_Close.Location = new System.Drawing.Point(422, 672);
			this.button_Close.Name = "button_Close";
			this.button_Close.Size = new System.Drawing.Size(100, 28);
			this.button_Close.TabIndex = 5;
			this.button_Close.Text = "Close";
			this.button_Close.UseVisualStyleBackColor = true;
			this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.Location = new System.Drawing.Point(27, 672);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(389, 25);
			this.label1.TabIndex = 6;
			this.label1.Text = "Reminder: This template only serves as a Base Foundation";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Sheet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 712);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button_Close);
			this.Controls.Add(this.label_Title);
			this.Controls.Add(this.richTextBox_Template);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Sheet";
			this.Text = "Sheet Generated";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox_Template;
		private System.Windows.Forms.Label label_Title;
		private System.Windows.Forms.Button button_Close;
		private System.Windows.Forms.Label label1;
	}
}