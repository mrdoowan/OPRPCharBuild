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
        private bool majorBuff;

        // Used to transfer across variables
        private int rank;
        private int power;
        private string statOpt;

        private const string SINGLE = "Single";

        public Add_TechStats(string opt_, int rank_, int pow_, string range_) {
            button_clicked = false;
            InitializeComponent();
            // Set Variables
            statOpt = opt_;
            this.Text = opt_;
            rank = rank_;
            power = pow_;
            // Add to Combobox
            comboBox_AoE.Items.AddRange(new string[] {
                SINGLE,
                Database.EFF_SHAOE,
                Database.EFF_MDAOE,
                Database.EFF_LOAOE
            });
            if (range_ == Database.EFF_SHAOE) { comboBox_AoE.Text = range_; }
            else if (range_ == Database.EFF_MDAOE) { comboBox_AoE.Text = range_; }
            else if (range_ == Database.EFF_LOAOE) { comboBox_AoE.Text = range_; }
        }

        public string LoadDialog(ref Stats techStats, string textbox) {
            if (statOpt == Database.BUF_CRITHI || statOpt == Database.BUF_ANASTR ||
                statOpt == Database.BUF_QUICKS) { label_Rank.Text = power + " Power"; }
            else { label_Rank.Text = "Rank " + rank; }
            label_Rank.Text += " Tech for " + statOpt; 
            StatAlter alterSetting = Database.getStatAlter(statOpt);
            if (alterSetting == null) { return textbox; }
            // Establish Major Buff setting
            majorBuff = alterSetting.majorBuff;
            if (majorBuff) { label_MajorBuff.Visible = true; }
            // Set techStats
            numericUpDown_Str.Value = techStats.strength;
            numericUpDown_Spe.Value = techStats.speed;
            numericUpDown_Sta.Value = techStats.stamina;
            numericUpDown_Acc.Value = techStats.accuracy;
            // Establish Duration
            if (alterSetting.durTable) {
                label_PostDur.Visible = true;
                if (statOpt == Database.BUF_FOOD) {
                    label_PostDur.Text = "24 Hour Duration";
                }
                else {
                    int duration = 3;
                    if (rank >= 44) { duration = 6; }
                    else if (rank >= 28) { duration = 5; }
                    else if (rank >= 14) { duration = 4; }
                    label_PostDur.Text = duration + " Round Duration";
                }
            }
            // Set calculations and total Buff/Debuff
            setCalcAndLabels(alterSetting);
            // Food is Long AoE, so disable
            if (statOpt == Database.BUF_DRUG) { comboBox_AoE.Text = SINGLE; }
            else if (statOpt == Database.BUF_FOOD) { comboBox_AoE.Text = Database.EFF_LOAOE; }
            if (statOpt == Database.BUF_FOOD || statOpt == Database.BUF_DRUG) { comboBox_AoE.Enabled = false; }
            // Load Template
            this.ShowDialog();
            if (button_clicked) {
                // Modify techStats
                techStats.statsName = statOpt;
                techStats.duration = label_PostDur.Text;
                techStats.strength = (int)numericUpDown_Str.Value;
                techStats.speed = (int)numericUpDown_Spe.Value;
                techStats.stamina = (int)numericUpDown_Sta.Value;
                techStats.accuracy = (int)numericUpDown_Acc.Value;
                // Return with generated String
                return techStats.getTechString();
            }
            else {
                return textbox;
            }
        }

        #region Helper Functions

        // Calculates based on Standard Table
        // HELPER function for calcBuffNum()
        private double calcStdTable(int rankBuff, string statOpt) {
            double decRank = rankBuff;
            // Willpower Buff Table (one Tier lower)
            if (statOpt == Database.BUF_WILLPO) {
                if (rankBuff >= 44) { return decRank * 0.85; }
                else if (rankBuff >= 28) { return decRank * 0.70; }
                else if (rankBuff >= 14) { return decRank * 0.60; }
                // Should never get here
            }
            // Obs Haki Buff Table (one Tier higher)
            else if (statOpt == Database.BUF_OBHAKI) {
                if (rankBuff >= 28) { return decRank; }
                else if (rankBuff >= 14) { return decRank * 0.85; }
                else { return decRank * 0.70; }
            }
            // Standard Table
            if (rankBuff >= 44 || statOpt == Database.BUF_ZOAN || 
                statOpt == Database.BUF_SULONG) {
                return decRank;
            }
            else if (rankBuff >= 28) { return decRank * 0.85; }
            else if (rankBuff >= 14) { return decRank * 0.70; }
            else { return decRank * 0.60; }
        }

        private int calcBuffNum(int rankBuff, bool buff, string AoE) {
            double tblNum = (statOpt == Database.BUF_CRITHI ||
                statOpt == Database.BUF_ANASTR ||
                statOpt == Database.BUF_QUICKS) ? calcStdTable(power, statOpt) : calcStdTable(rankBuff, statOpt);
            // Is this a Life Return Debuff?
            if (statOpt == Database.BUF_LIFRET && !buff) {
                return (int)(tblNum * 0.50);
            }
            else if ((AoE == Database.EFF_SHAOE && statOpt != Database.BUF_CQHAKI) ||
                (AoE == Database.EFF_MDAOE && statOpt == Database.BUF_CQHAKI)) {
                return (int)(tblNum * 0.80);
            }
            else if ((AoE == Database.EFF_MDAOE && statOpt != Database.BUF_CQHAKI) ||
                (AoE == Database.EFF_LOAOE && statOpt == Database.BUF_CQHAKI)) {
                return (int)(tblNum * 0.60);
            }
            else if (AoE == Database.EFF_LOAOE || statOpt == Database.BUF_FOOD) {
                return (int)(tblNum * 0.40);
            }
            return (int)tblNum;
        }

        private string calcBuffString(int rankBuff, bool buff, string AoE, int final) {
            string calc = (statOpt == Database.BUF_CRITHI ||
                statOpt == Database.BUF_ANASTR ||
                statOpt == Database.BUF_QUICKS) ? power.ToString() : rankBuff.ToString();
            calc += " * ";
            // ----- First Multiplier
            // Willpower Buff Table (one Tier lower)
            if (statOpt == Database.BUF_WILLPO) {
                if (rankBuff >= 44) { calc += "85%"; }
                else if (rankBuff >= 28) { calc += "70%"; }
                else if (rankBuff >= 14) { calc += "60%"; }
                // Should never get here
            }
            // Obs Haki Buff Table (one Tier higher)
            else if (statOpt == Database.BUF_OBHAKI) {
                if (rankBuff >= 28) { calc += "100%"; }
                else if (rankBuff >= 14) { calc += "85%"; }
                else { calc += "70%"; }
            }
            // Standard Table
            else if (statOpt == Database.BUF_ZOAN || rankBuff >= 44) { calc += "100%"; }
            else if (rankBuff >= 28) { calc += "85%"; }
            else if (rankBuff >= 14) { calc += "70%"; }
            else { calc += "60%"; }
            // ----- Second Multiplier
            // Is this a Life Return debuff?
            if (statOpt == Database.BUF_LIFRET && !buff) { calc += " * 50%"; }
            // Apply any AoE
            else if (AoE == Database.EFF_SHAOE && statOpt == Database.BUF_CQHAKI) {
                calc += " * 100%";
            }
            else if ((AoE == Database.EFF_SHAOE && statOpt != Database.BUF_CQHAKI) ||
                (AoE == Database.EFF_MDAOE && statOpt == Database.BUF_CQHAKI)) {
                calc += " * 80%";
            }
            else if ((AoE == Database.EFF_MDAOE && statOpt != Database.BUF_CQHAKI) ||
                (AoE == Database.EFF_LOAOE && statOpt == Database.BUF_CQHAKI)) {
                calc += " * 60%";
            }
            else if (AoE == Database.EFF_LOAOE || statOpt == Database.BUF_FOOD) {
                calc += " * 40%";
            }
            // Append the Final number
            calc += " = " + final;
            return calc;
        }

        // If the numbers add up correctly, enable the Load Button
        private bool checkValid() {
            int str = (int)numericUpDown_Str.Value;
            int spe = (int)numericUpDown_Spe.Value;
            int sta = (int)numericUpDown_Sta.Value;
            int acc = (int)numericUpDown_Acc.Value;
            if (statOpt == Database.BUF_OBHAKI && 
                sta != 0 && acc != 0) { return false; }
            if (majorBuff) {
                // Following rules of Major Buffs
                // Even number
                if (totBuff % 2 == 0 && (str > (totBuff / 2) ||
                    spe > (totBuff / 2) || sta > (totBuff / 2) ||
                    acc > (totBuff / 2))) {
                    return false;
                }
                // Odd number
                else if (totBuff % 2 == 1 && (str > (totBuff / 2 + 1) ||
                    spe > (totBuff / 2 + 1) || sta > (totBuff / 2 + 1) ||
                    acc > (totBuff / 2 + 1))) {
                    return false;
                }
            }
            int currBuff = 0, currDebuff = 0;
            if (str > 0) { currBuff += str; }
            else { currDebuff += (str * -1); }
            if (spe > 0) { currBuff += spe; }
            else { currDebuff += (spe * -1); }
            if (sta > 0) { currBuff += sta; }
            else { currDebuff += (sta * -1); }
            if (acc > 0) { currBuff += acc; }
            else { currDebuff += (acc * -1); }
            if (currBuff == totBuff && currDebuff == totDebuff) { return true; }
            else { return false; }
        }

        private void setCalcAndLabels(StatAlter alterSetting) {
            // Set calculations and total Buff/Debuff
            if (alterSetting.type == StatAlter.BUFF || alterSetting.type == StatAlter.STANCE) {
                totBuff = calcBuffNum(rank, true, comboBox_AoE.Text);
                label_TotBuff.Text = "Total Buff: " + totBuff;
                textBox_BuffCalc.Text = calcBuffString(rank, true, comboBox_AoE.Text, totBuff);
            }
            else {
                totBuff = 0;
                label_TotBuff.Text = "Total Buff: " + totBuff;
                textBox_BuffCalc.Text = "0 = 0";
            }
            if (alterSetting.type == StatAlter.DEBUFF || alterSetting.type == StatAlter.STANCE) {
                totDebuff = calcBuffNum(rank, false, comboBox_AoE.Text);
                label_TotDebuff.Text = "Total Debuff: " + totDebuff;
                textBox_DebuffCalc.Text = calcBuffString(rank, false, comboBox_AoE.Text, totDebuff);
            }
            else {
                totDebuff = 0;
                label_TotDebuff.Text = "Total Debuff: " + totDebuff;
                textBox_DebuffCalc.Text = "0 = 0";
            }
        }

        #endregion

        #region Event Handlers

        private void button_OK_Click(object sender, EventArgs e) {
            this.Close();
            button_clicked = true;
        }

        private void numericUpDown_Str_ValueChanged(object sender, EventArgs e) {
            if (checkValid()) { button_OK.Enabled = true; }
            else { button_OK.Enabled = false; }
        }

        private void numericUpDown_Spe_ValueChanged(object sender, EventArgs e) {
            if (checkValid()) { button_OK.Enabled = true; }
            else { button_OK.Enabled = false; }
        }

        private void numericUpDown_Sta_ValueChanged(object sender, EventArgs e) {
            if (checkValid()) { button_OK.Enabled = true; }
            else { button_OK.Enabled = false; }
        }

        private void numericUpDown_Acc_ValueChanged(object sender, EventArgs e) {
            if (checkValid()) { button_OK.Enabled = true; }
            else { button_OK.Enabled = false; }
        }

        private void comboBox_Range_SelectedIndexChanged(object sender, EventArgs e) {
            StatAlter alterSetting = Database.getStatAlter(statOpt);
            setCalcAndLabels(alterSetting);
            if (checkValid()) { button_OK.Enabled = true; }
            else { button_OK.Enabled = false; }
        }

        #endregion
    }
}
