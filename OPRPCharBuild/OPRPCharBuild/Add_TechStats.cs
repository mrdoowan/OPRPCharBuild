﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
    public partial class Add_TechStats : Form
    {
        // Member variables
        private bool button_clicked;
        private int totBuff;
        private int totDebuff;

        public Add_TechStats() {
            button_clicked = false;
            InitializeComponent();
        }

        public string LoadDialog(string statOpt, ref Stats techStats, string textbox) {
            switch (statOpt) {

            }
            this.ShowDialog();
            if (button_clicked) {

            }
            else {
                return textbox;
            }
        }

        #region Event Handlers

        private void button_OK_Click(object sender, EventArgs e) {
            this.Close();
            button_clicked = true;
        }

        private void numericUpDown_Str_ValueChanged(object sender, EventArgs e) {

        }

        private void numericUpDown_Spe_ValueChanged(object sender, EventArgs e) {

        }

        private void numericUpDown_Sta_ValueChanged(object sender, EventArgs e) {

        }

        private void numericUpDown_Acc_ValueChanged(object sender, EventArgs e) {

        }

        private void comboBox_Range_SelectedIndexChanged(object sender, EventArgs e) {

        }

        #endregion
    }
}