namespace OPRPCharBuild
{
	partial class Add_Technique
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Technique));
			this.label64 = new System.Windows.Forms.Label();
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown_Rank = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox_AffectRank = new System.Windows.Forms.ComboBox();
			this.checkBox_Branched = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_TechBranched = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDown_PointsBranch = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDown_RegTP = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDown_SpTP = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.comboBox_SpTrait = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.comboBox_Type = new System.Windows.Forms.ComboBox();
			this.comboBox_Range = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.numericUpDown_Power = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.checkBox_Stats = new System.Windows.Forms.CheckBox();
			this.label13 = new System.Windows.Forms.Label();
			this.numericUpDown_PlusStats = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.numericUpDown_MinusStats = new System.Windows.Forms.NumericUpDown();
			this.label15 = new System.Windows.Forms.Label();
			this.textBox_TPMsg = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.richTextBox_Desc = new System.Windows.Forms.RichTextBox();
			this.button12 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.label_SpTPMsg = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label_MaxRank = new System.Windows.Forms.Label();
			this.checkBox_Sig = new System.Windows.Forms.CheckBox();
			this.comboBox_PlusStat = new System.Windows.Forms.ComboBox();
			this.comboBox_MinusStat = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rank)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PointsBranch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RegTP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SpTP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Power)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PlusStats)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinusStats)).BeginInit();
			this.SuspendLayout();
			// 
			// label64
			// 
			this.label64.AutoSize = true;
			this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label64.Location = new System.Drawing.Point(12, 9);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(38, 13);
			this.label64.TabIndex = 27;
			this.label64.Text = "Name:";
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(56, 6);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(292, 20);
			this.textBox_Name.TabIndex = 28;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.Location = new System.Drawing.Point(354, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 29;
			this.label1.Text = "Rank:";
			// 
			// numericUpDown_Rank
			// 
			this.numericUpDown_Rank.Location = new System.Drawing.Point(396, 6);
			this.numericUpDown_Rank.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_Rank.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown_Rank.Name = "numericUpDown_Rank";
			this.numericUpDown_Rank.Size = new System.Drawing.Size(42, 20);
			this.numericUpDown_Rank.TabIndex = 30;
			this.numericUpDown_Rank.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown_Rank.ValueChanged += new System.EventHandler(this.numericUpDown_Rank_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.Location = new System.Drawing.Point(12, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 13);
			this.label2.TabIndex = 31;
			this.label2.Text = "Trait Affecting Rank:";
			// 
			// comboBox_AffectRank
			// 
			this.comboBox_AffectRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_AffectRank.FormattingEnabled = true;
			this.comboBox_AffectRank.Items.AddRange(new object[] {
            ""});
			this.comboBox_AffectRank.Location = new System.Drawing.Point(123, 36);
			this.comboBox_AffectRank.Name = "comboBox_AffectRank";
			this.comboBox_AffectRank.Size = new System.Drawing.Size(121, 21);
			this.comboBox_AffectRank.TabIndex = 32;
			// 
			// checkBox_Branched
			// 
			this.checkBox_Branched.AutoSize = true;
			this.checkBox_Branched.Location = new System.Drawing.Point(12, 70);
			this.checkBox_Branched.Name = "checkBox_Branched";
			this.checkBox_Branched.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBox_Branched.Size = new System.Drawing.Size(72, 17);
			this.checkBox_Branched.TabIndex = 34;
			this.checkBox_Branched.Text = "Branched";
			this.checkBox_Branched.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox_Branched.UseVisualStyleBackColor = true;
			this.checkBox_Branched.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.Location = new System.Drawing.Point(111, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82, 13);
			this.label4.TabIndex = 35;
			this.label4.Text = "Branched From:";
			// 
			// textBox_TechBranched
			// 
			this.textBox_TechBranched.Enabled = false;
			this.textBox_TechBranched.Location = new System.Drawing.Point(199, 67);
			this.textBox_TechBranched.Name = "textBox_TechBranched";
			this.textBox_TechBranched.Size = new System.Drawing.Size(239, 20);
			this.textBox_TechBranched.TabIndex = 36;
			this.textBox_TechBranched.TextChanged += new System.EventHandler(this.textBox_TechBranched_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.Location = new System.Drawing.Point(105, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 13);
			this.label5.TabIndex = 37;
			this.label5.Text = "Points Branched:";
			// 
			// numericUpDown_PointsBranch
			// 
			this.numericUpDown_PointsBranch.Enabled = false;
			this.numericUpDown_PointsBranch.Location = new System.Drawing.Point(199, 98);
			this.numericUpDown_PointsBranch.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_PointsBranch.Name = "numericUpDown_PointsBranch";
			this.numericUpDown_PointsBranch.Size = new System.Drawing.Size(42, 20);
			this.numericUpDown_PointsBranch.TabIndex = 38;
			this.numericUpDown_PointsBranch.ValueChanged += new System.EventHandler(this.numericUpDown_PointsBranch_ValueChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.Location = new System.Drawing.Point(12, 130);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(92, 13);
			this.label6.TabIndex = 39;
			this.label6.Text = "Regular TP Used:";
			// 
			// numericUpDown_RegTP
			// 
			this.numericUpDown_RegTP.Location = new System.Drawing.Point(108, 128);
			this.numericUpDown_RegTP.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_RegTP.Name = "numericUpDown_RegTP";
			this.numericUpDown_RegTP.Size = new System.Drawing.Size(42, 20);
			this.numericUpDown_RegTP.TabIndex = 40;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label7.Location = new System.Drawing.Point(156, 130);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(90, 13);
			this.label7.TabIndex = 41;
			this.label7.Text = "Special TP Used:";
			// 
			// numericUpDown_SpTP
			// 
			this.numericUpDown_SpTP.Enabled = false;
			this.numericUpDown_SpTP.Location = new System.Drawing.Point(252, 128);
			this.numericUpDown_SpTP.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_SpTP.Name = "numericUpDown_SpTP";
			this.numericUpDown_SpTP.Size = new System.Drawing.Size(42, 20);
			this.numericUpDown_SpTP.TabIndex = 42;
			this.numericUpDown_SpTP.ValueChanged += new System.EventHandler(this.numericUpDown_SpTP_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label8.Location = new System.Drawing.Point(12, 160);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(129, 13);
			this.label8.TabIndex = 43;
			this.label8.Text = "Trait Used for Special TP:";
			// 
			// comboBox_SpTrait
			// 
			this.comboBox_SpTrait.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_SpTrait.Enabled = false;
			this.comboBox_SpTrait.FormattingEnabled = true;
			this.comboBox_SpTrait.Items.AddRange(new object[] {
            ""});
			this.comboBox_SpTrait.Location = new System.Drawing.Point(147, 157);
			this.comboBox_SpTrait.Name = "comboBox_SpTrait";
			this.comboBox_SpTrait.Size = new System.Drawing.Size(291, 21);
			this.comboBox_SpTrait.TabIndex = 44;
			this.comboBox_SpTrait.SelectedIndexChanged += new System.EventHandler(this.comboBox_SpTrait_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label9.Location = new System.Drawing.Point(12, 190);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(34, 13);
			this.label9.TabIndex = 45;
			this.label9.Text = "Type:";
			// 
			// comboBox_Type
			// 
			this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Type.FormattingEnabled = true;
			this.comboBox_Type.Items.AddRange(new object[] {
            "Offensive",
            "Defensive",
            "Support"});
			this.comboBox_Type.Location = new System.Drawing.Point(54, 187);
			this.comboBox_Type.Name = "comboBox_Type";
			this.comboBox_Type.Size = new System.Drawing.Size(121, 21);
			this.comboBox_Type.TabIndex = 46;
			// 
			// comboBox_Range
			// 
			this.comboBox_Range.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Range.FormattingEnabled = true;
			this.comboBox_Range.Items.AddRange(new object[] {
            "Melee",
            "Short",
            "Medium",
            "Long",
            "Extra-Long",
            "AOE"});
			this.comboBox_Range.Location = new System.Drawing.Point(317, 187);
			this.comboBox_Range.Name = "comboBox_Range";
			this.comboBox_Range.Size = new System.Drawing.Size(121, 21);
			this.comboBox_Range.TabIndex = 48;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label10.Location = new System.Drawing.Point(269, 190);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(42, 13);
			this.label10.TabIndex = 47;
			this.label10.Text = "Range:";
			// 
			// numericUpDown_Power
			// 
			this.numericUpDown_Power.Location = new System.Drawing.Point(54, 221);
			this.numericUpDown_Power.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_Power.Name = "numericUpDown_Power";
			this.numericUpDown_Power.Size = new System.Drawing.Size(42, 20);
			this.numericUpDown_Power.TabIndex = 50;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label11.Location = new System.Drawing.Point(12, 223);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 13);
			this.label11.TabIndex = 49;
			this.label11.Text = "Power:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label12.Location = new System.Drawing.Point(107, 223);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(34, 13);
			this.label12.TabIndex = 51;
			this.label12.Text = "Stats:";
			// 
			// checkBox_Stats
			// 
			this.checkBox_Stats.AutoSize = true;
			this.checkBox_Stats.Checked = true;
			this.checkBox_Stats.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Stats.Location = new System.Drawing.Point(147, 222);
			this.checkBox_Stats.Name = "checkBox_Stats";
			this.checkBox_Stats.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBox_Stats.Size = new System.Drawing.Size(46, 17);
			this.checkBox_Stats.TabIndex = 52;
			this.checkBox_Stats.Text = "N/A";
			this.checkBox_Stats.UseVisualStyleBackColor = true;
			this.checkBox_Stats.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label13.Location = new System.Drawing.Point(206, 221);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(16, 17);
			this.label13.TabIndex = 53;
			this.label13.Text = "+";
			// 
			// numericUpDown_PlusStats
			// 
			this.numericUpDown_PlusStats.Enabled = false;
			this.numericUpDown_PlusStats.Location = new System.Drawing.Point(221, 221);
			this.numericUpDown_PlusStats.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_PlusStats.Name = "numericUpDown_PlusStats";
			this.numericUpDown_PlusStats.Size = new System.Drawing.Size(42, 20);
			this.numericUpDown_PlusStats.TabIndex = 54;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label14.Location = new System.Drawing.Point(333, 221);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(13, 17);
			this.label14.TabIndex = 55;
			this.label14.Text = "-";
			// 
			// numericUpDown_MinusStats
			// 
			this.numericUpDown_MinusStats.Enabled = false;
			this.numericUpDown_MinusStats.Location = new System.Drawing.Point(348, 221);
			this.numericUpDown_MinusStats.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown_MinusStats.Name = "numericUpDown_MinusStats";
			this.numericUpDown_MinusStats.Size = new System.Drawing.Size(42, 20);
			this.numericUpDown_MinusStats.TabIndex = 56;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label15.Location = new System.Drawing.Point(12, 255);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(104, 13);
			this.label15.TabIndex = 57;
			this.label15.Text = "TP Usage Message:";
			// 
			// textBox_TPMsg
			// 
			this.textBox_TPMsg.Location = new System.Drawing.Point(119, 252);
			this.textBox_TPMsg.Name = "textBox_TPMsg";
			this.textBox_TPMsg.ReadOnly = true;
			this.textBox_TPMsg.Size = new System.Drawing.Size(319, 20);
			this.textBox_TPMsg.TabIndex = 58;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label16.Location = new System.Drawing.Point(12, 289);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(63, 13);
			this.label16.TabIndex = 59;
			this.label16.Text = "Description:";
			// 
			// richTextBox_Desc
			// 
			this.richTextBox_Desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.richTextBox_Desc.Location = new System.Drawing.Point(81, 289);
			this.richTextBox_Desc.Name = "richTextBox_Desc";
			this.richTextBox_Desc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richTextBox_Desc.Size = new System.Drawing.Size(357, 126);
			this.richTextBox_Desc.TabIndex = 60;
			this.richTextBox_Desc.Text = "";
			// 
			// button12
			// 
			this.button12.Location = new System.Drawing.Point(283, 427);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(75, 23);
			this.button12.TabIndex = 62;
			this.button12.Text = "Clear";
			this.button12.UseVisualStyleBackColor = true;
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(364, 427);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(75, 23);
			this.button11.TabIndex = 61;
			this.button11.Text = "Add";
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// label_SpTPMsg
			// 
			this.label_SpTPMsg.AutoSize = true;
			this.label_SpTPMsg.Location = new System.Drawing.Point(307, 130);
			this.label_SpTPMsg.Name = "label_SpTPMsg";
			this.label_SpTPMsg.Size = new System.Drawing.Size(100, 13);
			this.label_SpTPMsg.TabIndex = 63;
			this.label_SpTPMsg.Text = "No Special TP Trait";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(57, 432);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(220, 13);
			this.label18.TabIndex = 64;
			this.label18.Text = "You can cancel changes by closing the form.";
			// 
			// label_MaxRank
			// 
			this.label_MaxRank.Location = new System.Drawing.Point(354, 39);
			this.label_MaxRank.Name = "label_MaxRank";
			this.label_MaxRank.Size = new System.Drawing.Size(100, 13);
			this.label_MaxRank.TabIndex = 65;
			this.label_MaxRank.Text = "Max Rank is: 0";
			// 
			// checkBox_Sig
			// 
			this.checkBox_Sig.AutoSize = true;
			this.checkBox_Sig.Enabled = false;
			this.checkBox_Sig.Location = new System.Drawing.Point(250, 38);
			this.checkBox_Sig.Name = "checkBox_Sig";
			this.checkBox_Sig.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBox_Sig.Size = new System.Drawing.Size(71, 17);
			this.checkBox_Sig.TabIndex = 66;
			this.checkBox_Sig.Text = "Signature";
			this.checkBox_Sig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox_Sig.UseVisualStyleBackColor = true;
			this.checkBox_Sig.CheckedChanged += new System.EventHandler(this.checkBox_Sig_CheckedChanged);
			// 
			// comboBox_PlusStat
			// 
			this.comboBox_PlusStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_PlusStat.Enabled = false;
			this.comboBox_PlusStat.FormattingEnabled = true;
			this.comboBox_PlusStat.Items.AddRange(new object[] {
            "Str",
            "Sta",
            "Spe",
            "Acc"});
			this.comboBox_PlusStat.Location = new System.Drawing.Point(268, 220);
			this.comboBox_PlusStat.Name = "comboBox_PlusStat";
			this.comboBox_PlusStat.Size = new System.Drawing.Size(43, 21);
			this.comboBox_PlusStat.TabIndex = 67;
			// 
			// comboBox_MinusStat
			// 
			this.comboBox_MinusStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_MinusStat.Enabled = false;
			this.comboBox_MinusStat.FormattingEnabled = true;
			this.comboBox_MinusStat.Items.AddRange(new object[] {
            "Str",
            "Sta",
            "Spe",
            "Acc"});
			this.comboBox_MinusStat.Location = new System.Drawing.Point(394, 220);
			this.comboBox_MinusStat.Name = "comboBox_MinusStat";
			this.comboBox_MinusStat.Size = new System.Drawing.Size(45, 21);
			this.comboBox_MinusStat.TabIndex = 68;
			// 
			// Add_Technique
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(459, 462);
			this.Controls.Add(this.comboBox_MinusStat);
			this.Controls.Add(this.comboBox_PlusStat);
			this.Controls.Add(this.checkBox_Sig);
			this.Controls.Add(this.label_MaxRank);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label_SpTPMsg);
			this.Controls.Add(this.button12);
			this.Controls.Add(this.button11);
			this.Controls.Add(this.richTextBox_Desc);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.textBox_TPMsg);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.numericUpDown_MinusStats);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.numericUpDown_PlusStats);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.checkBox_Stats);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.numericUpDown_Power);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.comboBox_Range);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.comboBox_Type);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.comboBox_SpTrait);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.numericUpDown_SpTP);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.numericUpDown_RegTP);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.numericUpDown_PointsBranch);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox_TechBranched);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkBox_Branched);
			this.Controls.Add(this.comboBox_AffectRank);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDown_Rank);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label64);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Add_Technique";
			this.Text = "Technique Creator";
			this.Load += new System.EventHandler(this.Add_Technique_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rank)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PointsBranch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RegTP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SpTP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Power)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PlusStats)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinusStats)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.TextBox textBox_Name;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown_Rank;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox_AffectRank;
		private System.Windows.Forms.CheckBox checkBox_Branched;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_TechBranched;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDown_PointsBranch;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDown_RegTP;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDown_SpTP;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboBox_SpTrait;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboBox_Type;
		private System.Windows.Forms.ComboBox comboBox_Range;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown numericUpDown_Power;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox checkBox_Stats;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown numericUpDown_PlusStats;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown numericUpDown_MinusStats;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBox_TPMsg;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.RichTextBox richTextBox_Desc;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Label label_SpTPMsg;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label_MaxRank;
		private System.Windows.Forms.CheckBox checkBox_Sig;
		private System.Windows.Forms.ComboBox comboBox_PlusStat;
		private System.Windows.Forms.ComboBox comboBox_MinusStat;
	}
}