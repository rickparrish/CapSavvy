/*

CapSavvy, a program to track bandwidth usage for customers of TekSavvy and their resellers
Copyright (C) 2014 Adam Zey, Darryl Tam

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

*/

namespace CapSavvy
{
    partial class CapSavvy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CapSavvy));
            this.Textbox_Username = new System.Windows.Forms.TextBox();
            this.Button_Check = new System.Windows.Forms.Button();
            this.sysTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuSysTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.Button_Colour = new System.Windows.Forms.Button();
            this.Timer_Update = new System.Windows.Forms.Timer(this.components);
            this.Label_History = new System.Windows.Forms.Label();
            this.Label_HistoryData = new System.Windows.Forms.Label();
            this.DataGrid_Stats = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RadioButton_TotalPredicted = new System.Windows.Forms.RadioButton();
            this.RadioButton_UpDown = new System.Windows.Forms.RadioButton();
            this.RadioButton_DownUp = new System.Windows.Forms.RadioButton();
            this.RadioButton_Total = new System.Windows.Forms.RadioButton();
            this.Text_Colour = new System.Windows.Forms.Label();
            this.Button_Background = new System.Windows.Forms.Button();
            this.Checkbox_Transparent = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RadioButton_DownPredicted = new System.Windows.Forms.RadioButton();
            this.RadioButton_Down = new System.Windows.Forms.RadioButton();
            this.Checkbox_Peak = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Label_format = new System.Windows.Forms.Label();
            this.contextMenuSysTray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_Stats)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Textbox_Username
            // 
            this.Textbox_Username.Location = new System.Drawing.Point(6, 19);
            this.Textbox_Username.MaxLength = 256;
            this.Textbox_Username.Name = "Textbox_Username";
            this.Textbox_Username.Size = new System.Drawing.Size(200, 20);
            this.Textbox_Username.TabIndex = 2;
            this.Textbox_Username.Leave += new System.EventHandler(this.Textbox_Username_Leave);
            // 
            // Button_Check
            // 
            this.Button_Check.Location = new System.Drawing.Point(506, 28);
            this.Button_Check.Name = "Button_Check";
            this.Button_Check.Size = new System.Drawing.Size(76, 23);
            this.Button_Check.TabIndex = 1;
            this.Button_Check.Text = "Refresh";
            this.Button_Check.UseVisualStyleBackColor = true;
            this.Button_Check.Click += new System.EventHandler(this.Button_Check_Click);
            // 
            // sysTrayIcon
            // 
            this.sysTrayIcon.ContextMenuStrip = this.contextMenuSysTray;
            this.sysTrayIcon.Visible = true;
            this.sysTrayIcon.DoubleClick += new System.EventHandler(this.sysTrayIcon_DoubleClick);
            // 
            // contextMenuSysTray
            // 
            this.contextMenuSysTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator2,
            this.settingsInfoToolStripMenuItem,
            this.updateNowToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuSysTray.Name = "contextMenuSysTray";
            this.contextMenuSysTray.ShowImageMargin = false;
            this.contextMenuSysTray.ShowItemToolTips = false;
            this.contextMenuSysTray.Size = new System.Drawing.Size(127, 104);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // settingsInfoToolStripMenuItem
            // 
            this.settingsInfoToolStripMenuItem.Name = "settingsInfoToolStripMenuItem";
            this.settingsInfoToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.settingsInfoToolStripMenuItem.Text = "&Settings/Info...";
            this.settingsInfoToolStripMenuItem.Click += new System.EventHandler(this.settingsInfoToolStripMenuItem_Click);
            // 
            // updateNowToolStripMenuItem
            // 
            this.updateNowToolStripMenuItem.Name = "updateNowToolStripMenuItem";
            this.updateNowToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.updateNowToolStripMenuItem.Text = "&Update Now";
            this.updateNowToolStripMenuItem.Click += new System.EventHandler(this.updateNowToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.White;
            // 
            // Button_Colour
            // 
            this.Button_Colour.Location = new System.Drawing.Point(74, 16);
            this.Button_Colour.Name = "Button_Colour";
            this.Button_Colour.Size = new System.Drawing.Size(73, 23);
            this.Button_Colour.TabIndex = 1;
            this.Button_Colour.Text = "Text";
            this.Button_Colour.UseVisualStyleBackColor = true;
            this.Button_Colour.Click += new System.EventHandler(this.Button_Colour_Click);
            // 
            // Timer_Update
            // 
            this.Timer_Update.Interval = 1;
            this.Timer_Update.Tick += new System.EventHandler(this.Timer_Update_Tick);
            // 
            // Label_History
            // 
            this.Label_History.AutoSize = true;
            this.Label_History.Location = new System.Drawing.Point(75, 89);
            this.Label_History.Name = "Label_History";
            this.Label_History.Size = new System.Drawing.Size(0, 13);
            this.Label_History.TabIndex = 7;
            // 
            // Label_HistoryData
            // 
            this.Label_HistoryData.AutoSize = true;
            this.Label_HistoryData.Location = new System.Drawing.Point(131, 89);
            this.Label_HistoryData.Name = "Label_HistoryData";
            this.Label_HistoryData.Size = new System.Drawing.Size(0, 13);
            this.Label_HistoryData.TabIndex = 9;
            // 
            // DataGrid_Stats
            // 
            this.DataGrid_Stats.AllowUserToAddRows = false;
            this.DataGrid_Stats.AllowUserToDeleteRows = false;
            this.DataGrid_Stats.AllowUserToResizeColumns = false;
            this.DataGrid_Stats.AllowUserToResizeRows = false;
            this.DataGrid_Stats.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGrid_Stats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGrid_Stats.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DataGrid_Stats.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DataGrid_Stats.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid_Stats.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid_Stats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_Stats.ColumnHeadersVisible = false;
            this.DataGrid_Stats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGrid_Stats.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGrid_Stats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGrid_Stats.EnableHeadersVisualStyles = false;
            this.DataGrid_Stats.GridColor = System.Drawing.SystemColors.Control;
            this.DataGrid_Stats.Location = new System.Drawing.Point(12, 155);
            this.DataGrid_Stats.Name = "DataGrid_Stats";
            this.DataGrid_Stats.ReadOnly = true;
            this.DataGrid_Stats.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid_Stats.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGrid_Stats.RowHeadersVisible = false;
            this.DataGrid_Stats.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            this.DataGrid_Stats.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGrid_Stats.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DataGrid_Stats.ShowCellErrors = false;
            this.DataGrid_Stats.ShowCellToolTips = false;
            this.DataGrid_Stats.ShowEditingIcon = false;
            this.DataGrid_Stats.ShowRowErrors = false;
            this.DataGrid_Stats.Size = new System.Drawing.Size(607, 128);
            this.DataGrid_Stats.StandardTab = true;
            this.DataGrid_Stats.TabIndex = 10;
            this.DataGrid_Stats.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 95;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 95;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 95;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 95;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 95;
            // 
            // RadioButton_TotalPredicted
            // 
            this.RadioButton_TotalPredicted.AutoSize = true;
            this.RadioButton_TotalPredicted.Checked = true;
            this.RadioButton_TotalPredicted.Location = new System.Drawing.Point(6, 12);
            this.RadioButton_TotalPredicted.Name = "RadioButton_TotalPredicted";
            this.RadioButton_TotalPredicted.Size = new System.Drawing.Size(99, 17);
            this.RadioButton_TotalPredicted.TabIndex = 11;
            this.RadioButton_TotalPredicted.TabStop = true;
            this.RadioButton_TotalPredicted.Text = "Total/Predicted";
            this.RadioButton_TotalPredicted.UseVisualStyleBackColor = true;
            this.RadioButton_TotalPredicted.CheckedChanged += new System.EventHandler(this.RadioButton_TotalPredicted_CheckedChanged);
            // 
            // RadioButton_UpDown
            // 
            this.RadioButton_UpDown.AutoSize = true;
            this.RadioButton_UpDown.Location = new System.Drawing.Point(111, 12);
            this.RadioButton_UpDown.Name = "RadioButton_UpDown";
            this.RadioButton_UpDown.Size = new System.Drawing.Size(72, 17);
            this.RadioButton_UpDown.TabIndex = 13;
            this.RadioButton_UpDown.Text = "Up/Down";
            this.RadioButton_UpDown.UseVisualStyleBackColor = true;
            this.RadioButton_UpDown.CheckedChanged += new System.EventHandler(this.RadioButton_UpDown_CheckedChanged);
            // 
            // RadioButton_DownUp
            // 
            this.RadioButton_DownUp.AutoSize = true;
            this.RadioButton_DownUp.Location = new System.Drawing.Point(111, 35);
            this.RadioButton_DownUp.Name = "RadioButton_DownUp";
            this.RadioButton_DownUp.Size = new System.Drawing.Size(72, 17);
            this.RadioButton_DownUp.TabIndex = 15;
            this.RadioButton_DownUp.Text = "Down/Up";
            this.RadioButton_DownUp.UseVisualStyleBackColor = true;
            this.RadioButton_DownUp.CheckedChanged += new System.EventHandler(this.RadioButton_DownUp_CheckedChanged);
            // 
            // RadioButton_Total
            // 
            this.RadioButton_Total.AutoSize = true;
            this.RadioButton_Total.Location = new System.Drawing.Point(6, 35);
            this.RadioButton_Total.Name = "RadioButton_Total";
            this.RadioButton_Total.Size = new System.Drawing.Size(49, 17);
            this.RadioButton_Total.TabIndex = 14;
            this.RadioButton_Total.Text = "Total";
            this.RadioButton_Total.UseVisualStyleBackColor = true;
            this.RadioButton_Total.CheckedChanged += new System.EventHandler(this.RadioButton_Total_CheckedChanged);
            // 
            // Text_Colour
            // 
            this.Text_Colour.BackColor = System.Drawing.Color.Black;
            this.Text_Colour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Text_Colour.ForeColor = System.Drawing.Color.White;
            this.Text_Colour.Location = new System.Drawing.Point(6, 16);
            this.Text_Colour.Name = "Text_Colour";
            this.Text_Colour.Size = new System.Drawing.Size(62, 23);
            this.Text_Colour.TabIndex = 16;
            this.Text_Colour.Text = "123.45";
            this.Text_Colour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_Background
            // 
            this.Button_Background.Location = new System.Drawing.Point(153, 16);
            this.Button_Background.Name = "Button_Background";
            this.Button_Background.Size = new System.Drawing.Size(109, 23);
            this.Button_Background.TabIndex = 17;
            this.Button_Background.Text = "Background";
            this.Button_Background.UseVisualStyleBackColor = true;
            this.Button_Background.Click += new System.EventHandler(this.Button_Background_Click);
            // 
            // Checkbox_Transparent
            // 
            this.Checkbox_Transparent.AutoSize = true;
            this.Checkbox_Transparent.Checked = true;
            this.Checkbox_Transparent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Checkbox_Transparent.Location = new System.Drawing.Point(68, 53);
            this.Checkbox_Transparent.Name = "Checkbox_Transparent";
            this.Checkbox_Transparent.Size = new System.Drawing.Size(144, 17);
            this.Checkbox_Transparent.TabIndex = 18;
            this.Checkbox_Transparent.Text = "Transparent Background";
            this.Checkbox_Transparent.UseVisualStyleBackColor = true;
            this.Checkbox_Transparent.CheckedChanged += new System.EventHandler(this.Checkbox_Transparent_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadioButton_DownPredicted);
            this.groupBox1.Controls.Add(this.RadioButton_Down);
            this.groupBox1.Controls.Add(this.Checkbox_Peak);
            this.groupBox1.Controls.Add(this.RadioButton_UpDown);
            this.groupBox1.Controls.Add(this.RadioButton_DownUp);
            this.groupBox1.Controls.Add(this.RadioButton_TotalPredicted);
            this.groupBox1.Controls.Add(this.RadioButton_Total);
            this.groupBox1.Location = new System.Drawing.Point(286, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 84);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View";
            // 
            // RadioButton_DownPredicted
            // 
            this.RadioButton_DownPredicted.AutoSize = true;
            this.RadioButton_DownPredicted.Location = new System.Drawing.Point(189, 12);
            this.RadioButton_DownPredicted.Name = "RadioButton_DownPredicted";
            this.RadioButton_DownPredicted.Size = new System.Drawing.Size(103, 17);
            this.RadioButton_DownPredicted.TabIndex = 24;
            this.RadioButton_DownPredicted.Text = "Down/Predicted";
            this.RadioButton_DownPredicted.UseVisualStyleBackColor = true;
            this.RadioButton_DownPredicted.CheckedChanged += new System.EventHandler(this.RadioButton_DownPredicted_CheckedChanged);
            // 
            // RadioButton_Down
            // 
            this.RadioButton_Down.AutoSize = true;
            this.RadioButton_Down.Location = new System.Drawing.Point(189, 35);
            this.RadioButton_Down.Name = "RadioButton_Down";
            this.RadioButton_Down.Size = new System.Drawing.Size(53, 17);
            this.RadioButton_Down.TabIndex = 25;
            this.RadioButton_Down.Text = "Down";
            this.RadioButton_Down.UseVisualStyleBackColor = true;
            this.RadioButton_Down.CheckedChanged += new System.EventHandler(this.RadioButton_Down_CheckedChanged);
            // 
            // Checkbox_Peak
            // 
            this.Checkbox_Peak.AutoSize = true;
            this.Checkbox_Peak.Location = new System.Drawing.Point(6, 58);
            this.Checkbox_Peak.Name = "Checkbox_Peak";
            this.Checkbox_Peak.Size = new System.Drawing.Size(139, 17);
            this.Checkbox_Peak.TabIndex = 23;
            this.Checkbox_Peak.Text = "Show Peak Usage Only";
            this.Checkbox_Peak.UseVisualStyleBackColor = true;
            this.Checkbox_Peak.CheckedChanged += new System.EventHandler(this.Checkbox_Peak_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Text_Colour);
            this.groupBox2.Controls.Add(this.Button_Colour);
            this.groupBox2.Controls.Add(this.Button_Background);
            this.groupBox2.Controls.Add(this.Checkbox_Transparent);
            this.groupBox2.Location = new System.Drawing.Point(12, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 84);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Colour";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Textbox_Username);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 47);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Username / API Key";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Label_format);
            this.groupBox4.Location = new System.Drawing.Point(230, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(270, 47);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Username Format";
            // 
            // Label_format
            // 
            this.Label_format.AutoSize = true;
            this.Label_format.Location = new System.Drawing.Point(6, 22);
            this.Label_format.Name = "Label_format";
            this.Label_format.Size = new System.Drawing.Size(37, 13);
            this.Label_format.TabIndex = 0;
            this.Label_format.Text = "          ";
            // 
            // CapSavvy
            // 
            this.ClientSize = new System.Drawing.Size(593, 256);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Label_HistoryData);
            this.Controls.Add(this.Label_History);
            this.Controls.Add(this.Button_Check);
            this.Controls.Add(this.DataGrid_Stats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CapSavvy";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuSysTray.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_Stats)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Textbox_Username;
        private System.Windows.Forms.Button Button_Check;
        private System.Windows.Forms.NotifyIcon sysTrayIcon;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button Button_Colour;
        private System.Windows.Forms.Timer Timer_Update;
        private System.Windows.Forms.Label Label_History;
        private System.Windows.Forms.Label Label_HistoryData;
        private System.Windows.Forms.DataGridView DataGrid_Stats;
        private System.Windows.Forms.ContextMenuStrip contextMenuSysTray;
        private System.Windows.Forms.ToolStripMenuItem settingsInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RadioButton RadioButton_TotalPredicted;
        private System.Windows.Forms.RadioButton RadioButton_UpDown;
        private System.Windows.Forms.RadioButton RadioButton_DownUp;
        private System.Windows.Forms.RadioButton RadioButton_Total;
        private System.Windows.Forms.Label Text_Colour;
        private System.Windows.Forms.Button Button_Background;
        private System.Windows.Forms.CheckBox Checkbox_Transparent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Checkbox_Peak;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Label_format;
        private System.Windows.Forms.RadioButton RadioButton_DownPredicted;
        private System.Windows.Forms.RadioButton RadioButton_Down;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}

