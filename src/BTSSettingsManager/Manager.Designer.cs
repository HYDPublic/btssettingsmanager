namespace BTSSettingsManager
{
    partial class Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manager));
            this.sspStatusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgStatusStrip = new System.Windows.Forms.ToolStripProgressBar();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grpManager = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.colKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgwLoad = new System.ComponentModel.BackgroundWorker();
            this.bgwSave = new System.ComponentModel.BackgroundWorker();
            this.bgwExport = new System.ComponentModel.BackgroundWorker();
            this.bgwImport = new System.ComponentModel.BackgroundWorker();
            this.cmsDeleteMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sspStatusStrip.SuspendLayout();
            this.grpManager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.cmsDeleteMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sspStatusStrip
            // 
            this.sspStatusStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sspStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusStrip,
            this.prgStatusStrip});
            this.sspStatusStrip.Location = new System.Drawing.Point(0, 715);
            this.sspStatusStrip.Name = "sspStatusStrip";
            this.sspStatusStrip.Size = new System.Drawing.Size(1016, 26);
            this.sspStatusStrip.TabIndex = 0;
            this.sspStatusStrip.Text = "sspStatusStrip";
            // 
            // lblStatusStrip
            // 
            this.lblStatusStrip.AutoSize = false;
            this.lblStatusStrip.Name = "lblStatusStrip";
            this.lblStatusStrip.Size = new System.Drawing.Size(300, 21);
            this.lblStatusStrip.Text = "lblStatusStrip";
            this.lblStatusStrip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // prgStatusStrip
            // 
            this.prgStatusStrip.Name = "prgStatusStrip";
            this.prgStatusStrip.Size = new System.Drawing.Size(150, 20);
            this.prgStatusStrip.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgStatusStrip.Visible = false;
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Location = new System.Drawing.Point(17, 33);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(187, 13);
            this.lblApplicationName.TabIndex = 1;
            this.lblApplicationName.Text = "SSO App Name (BizTalk App Name):";
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplicationName.Location = new System.Drawing.Point(20, 49);
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.Size = new System.Drawing.Size(952, 22);
            this.txtApplicationName.TabIndex = 2;
            this.txtApplicationName.TextChanged += new System.EventHandler(this.txtApplicationName_TextChanged);
            this.txtApplicationName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtApplicationName_KeyDown);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(897, 75);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 27);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // grpManager
            // 
            this.grpManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpManager.Controls.Add(this.btnSave);
            this.grpManager.Controls.Add(this.btnExport);
            this.grpManager.Controls.Add(this.btnImport);
            this.grpManager.Controls.Add(this.dgvSettings);
            this.grpManager.Controls.Add(this.lblApplicationName);
            this.grpManager.Controls.Add(this.btnLoad);
            this.grpManager.Controls.Add(this.txtApplicationName);
            this.grpManager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpManager.Location = new System.Drawing.Point(12, 12);
            this.grpManager.Name = "grpManager";
            this.grpManager.Size = new System.Drawing.Size(992, 690);
            this.grpManager.TabIndex = 4;
            this.grpManager.TabStop = false;
            this.grpManager.Text = "SSO Settings Manager";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(897, 647);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(101, 647);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 27);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(20, 647);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 27);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "Import...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // dgvSettings
            // 
            this.dgvSettings.AllowUserToAddRows = false;
            this.dgvSettings.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSettings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSettings.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKey,
            this.colValue});
            this.dgvSettings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSettings.Location = new System.Drawing.Point(20, 108);
            this.dgvSettings.Name = "dgvSettings";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSettings.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSettings.Size = new System.Drawing.Size(952, 533);
            this.dgvSettings.TabIndex = 5;
            this.dgvSettings.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSettings_CellEndEdit);
            this.dgvSettings.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvSettings_CellValidating);
            this.dgvSettings.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSettings_CellValueChanged);
            this.dgvSettings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvSettings_MouseUp);
            // 
            // colKey
            // 
            this.colKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colKey.HeaderText = "Key";
            this.colKey.Name = "colKey";
            this.colKey.Width = 49;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.Width = 61;
            // 
            // bgwLoad
            // 
            this.bgwLoad.WorkerReportsProgress = true;
            this.bgwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoad_DoWork);
            this.bgwLoad.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwLoad_ProgressChanged);
            this.bgwLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoad_RunWorkerCompleted);
            // 
            // bgwSave
            // 
            this.bgwSave.WorkerReportsProgress = true;
            this.bgwSave.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSave_DoWork);
            this.bgwSave.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSave_ProgressChanged);
            this.bgwSave.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSave_RunWorkerCompleted);
            // 
            // bgwExport
            // 
            this.bgwExport.WorkerReportsProgress = true;
            this.bgwExport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwExport_DoWork);
            this.bgwExport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwExport_ProgressChanged);
            this.bgwExport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwExport_RunWorkerCompleted);
            // 
            // bgwImport
            // 
            this.bgwImport.WorkerReportsProgress = true;
            this.bgwImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwImport_DoWork);
            this.bgwImport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwImport_ProgressChanged);
            this.bgwImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwImport_RunWorkerCompleted);
            // 
            // cmsDeleteMenu
            // 
            this.cmsDeleteMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowToolStripMenuItem});
            this.cmsDeleteMenu.Name = "cmsDeleteMenu";
            this.cmsDeleteMenu.Size = new System.Drawing.Size(130, 26);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.grpManager);
            this.Controls.Add(this.sspStatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Manager";
            this.Text = "SSO Settings Manager";
            this.Load += new System.EventHandler(this.Manager_Load);
            this.Resize += new System.EventHandler(this.Manager_Resize);
            this.sspStatusStrip.ResumeLayout(false);
            this.sspStatusStrip.PerformLayout();
            this.grpManager.ResumeLayout(false);
            this.grpManager.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.cmsDeleteMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sspStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar prgStatusStrip;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox grpManager;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView dgvSettings;
        private System.ComponentModel.BackgroundWorker bgwLoad;
        private System.ComponentModel.BackgroundWorker bgwSave;
        private System.ComponentModel.BackgroundWorker bgwExport;
        private System.ComponentModel.BackgroundWorker bgwImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.ContextMenuStrip cmsDeleteMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
    }
}

