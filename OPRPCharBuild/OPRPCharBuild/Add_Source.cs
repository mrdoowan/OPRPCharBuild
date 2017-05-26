using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
    public partial class Add_Source : Form
    {
        public Add_Source() {
            InitializeComponent();
            button_clicked = false;
        }

        // Button mechanics
        private bool button_clicked;
        private void button_OK_Click(object sender, EventArgs e) {
            // Check if completely empty
            if (string.IsNullOrWhiteSpace(textBox_URLSource.Text) && string.IsNullOrWhiteSpace(textBox_TitleSource.Text) &&
                numericUpDown_SDSource.Value == 0 && numericUpDown_BeliSource.Value == 0 &&
                string.IsNullOrWhiteSpace(richTextBox_NoteSource.Text)) {
                MessageBox.Show("Can't submit an empty Source.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                this.Close();
                button_clicked = true;
            }
        }

        // Adds a new source by prompting the Dialog Box
        public void new_Dialog(ref DataGridView dgv, ref Dictionary<string, Source> sourceDict, 
            ref bool dateStampBool, bool dateNAFormat) {
            checkBox_Datestamp.Checked = dateStampBool;
            this.ShowDialog();
            if (button_clicked) {
                try {
                    DateTime date = dateTimePicker_DateStamp.Value.Date;
                    bool noDate = checkBox_Datestamp.Checked;
                    string title = textBox_TitleSource.Text;
                    string URL = textBox_URLSource.Text;
                    int SD = (int)numericUpDown_SDSource.Value;
                    int beli = (int)numericUpDown_BeliSource.Value;
                    string notes = richTextBox_NoteSource.Text;
                    Source add_Source = new Source(date, noDate, title, URL, SD, beli, notes);
                    try { sourceDict.Add(title, add_Source); }
                    catch {
                        MessageBox.Show("Can't add two titles of the same name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.Close();
                    // Add into dgv
                    CultureInfo culture = dateNAFormat ? new CultureInfo("en-US") : new CultureInfo("en-GB");
                    string date_str = (!noDate) ? date.ToString("d", culture) : "";
                    string SD_str = (SD > 0) ? SD.ToString() : "";
                    string beli_str = (beli != 0) ? beli.ToString("N0") : "";
                    DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                    dgv.Rows.Insert(0, button, date_str, title, URL,
                        SD_str, beli_str, notes);
                    dgv.Rows[0].Cells[0].Value = "X"; // Setting text
                    dateStampBool = checkBox_Datestamp.Checked;    
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Double clicking should load the Dialog of the row's information
        public void edit_Dialog(ref DataGridView dgv, ref Dictionary<string, Source> sourceDict, 
            bool dateNAFormat) {
            this.Text = "Edit Source";
            // Load edit from row
            string title = dgv.SelectedRows[0].Cells[2].Value.ToString();
            string date_str = dgv.SelectedRows[0].Cells[1].Value.ToString();
            Source edit_Source = sourceDict[title];
            textBox_URLSource.Text = edit_Source.URL;
            textBox_TitleSource.Text = edit_Source.title;
            numericUpDown_SDSource.Value = edit_Source.SD;
            numericUpDown_BeliSource.Value = edit_Source.beli;
            richTextBox_NoteSource.Text = edit_Source.notes;
            if (string.IsNullOrWhiteSpace(date_str)) { dateTimePicker_DateStamp.Value = edit_Source.date; }
            else { checkBox_Datestamp.Checked = false; }
            this.ShowDialog();
            if (button_clicked) {
                // Remove initial item from Dictionary
                sourceDict.Remove(title);
                try {
                    DateTime new_date = dateTimePicker_DateStamp.Value.Date;
                    bool noDate = checkBox_Datestamp.Checked;
                    CultureInfo culture = dateNAFormat ? new CultureInfo("en-US") : new CultureInfo("en-GB");
                    string new_title = textBox_TitleSource.Text;
                    string new_URL = textBox_URLSource.Text;
                    int new_SD = (int)numericUpDown_SDSource.Value;
                    int new_beli = (int)numericUpDown_BeliSource.Value;
                    string new_note = richTextBox_NoteSource.Text;
                    // Add to dgv
                    dgv.SelectedRows[0].Cells[1].Value = (!noDate) ? new_date.ToString("d", culture) : "";
                    dgv.SelectedRows[0].Cells[2].Value = new_title;
                    dgv.SelectedRows[0].Cells[3].Value = new_URL;
                    dgv.SelectedRows[0].Cells[4].Value = (new_SD > 0) ? new_SD.ToString() : "";
                    dgv.SelectedRows[0].Cells[5].Value = (new_beli != 0) ? new_beli.ToString("N0") : "";
                    dgv.SelectedRows[0].Cells[6].Value = richTextBox_NoteSource.Text;
                    // Add to Dict
                    Source new_source = new Source(new_date, noDate, new_title, new_URL, new_SD, new_beli, new_note);
                    sourceDict.Add(new_title, new_source);
                }
                catch (Exception ex) {
                    sourceDict.Add(title, edit_Source); // Re-add
                    dgv.SelectedRows[0].Cells[1].Value = date_str;
                    dgv.SelectedRows[0].Cells[2].Value = edit_Source.title;
                    dgv.SelectedRows[0].Cells[3].Value = edit_Source.URL;
                    dgv.SelectedRows[0].Cells[4].Value = edit_Source.SD.ToString();
                    dgv.SelectedRows[0].Cells[5].Value = edit_Source.beli.ToString();
                    dgv.SelectedRows[0].Cells[6].Value = edit_Source.notes;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
