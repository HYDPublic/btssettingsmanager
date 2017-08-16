using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace BTSSettingsManager
{
    /// <summary>
    /// Settings Manager Form
    /// </summary>
    public partial class Manager : Form
    {
        #region Constants

        /// <summary>
        /// Constant value for the Key column index.
        /// </summary>
        private const int KeyColumnIndex = 0;

        /// <summary>
        /// Constant value for the Value column index.
        /// </summary>
        private const int ValueColumnIndex = 1;

        #endregion

        #region Properties

        /// <summary>
        /// Indicate if there are settings loaded.
        /// </summary>
        private bool HasSettingsLoaded;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Manager form.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public Manager(string[] args)
        {
            this.HasSettingsLoaded = false;
            InitializeComponent();

            if (args.Length > 0)
            {
                this.txtApplicationName.Text = args.First();
                this.btnLoad_Click(this, new EventArgs());
            }
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Display a message in the status strip.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        /// <param name="isError">Indicates if the message to be displayed is an error.</param>
        private void DisplayStatusStripMessage(string message, bool isError)
        {
            if (isError)
                this.lblStatusStrip.ForeColor = Color.Maroon;
            else
                this.lblStatusStrip.ForeColor = Color.Black;

            this.lblStatusStrip.Text = message;
        }

        /// <summary>
        /// Check if the DataGridView dgvSettings will have double keys during the CellValidating event.
        /// </summary>
        /// <param name="newKeyValue">New key value.</param>
        /// <param name="currentRowIndex">Index of the current row being changed. Used to avoid self false positives.</param>
        /// <returns>True, if the DataGridView will have double keys. False otherwise.</returns>
        private bool dgvSettings_HasDoubleKey(string newKeyValue, int currentRowIndex)
        {
            foreach (DataGridViewRow row in this.dgvSettings.Rows)
            {
                DataGridViewCell keyCell = row.Cells[Manager.KeyColumnIndex];

                if (row.Index != currentRowIndex && newKeyValue.CompareTo(keyCell.Value) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Manager_Load event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void Manager_Load(object sender, EventArgs e)
        {
            this.DisplayStatusStripMessage(string.Empty, false);
            this.Manager_Resize(this, new EventArgs());
        }

        /// <summary>
        /// Manager_Resize event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void Manager_Resize(object sender, EventArgs e)
        {
            this.lblStatusStrip.Size = this.Size.Width < 180 ? new Size(0, this.lblStatusStrip.Size.Height) : new Size(this.Size.Width - 180, this.lblStatusStrip.Size.Height);
        }

        #endregion

        #region DataGridView Events

        /// <summary>
        /// dgvSettings_CellValueChanged event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void dgvSettings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Track changes only when settings are loaded.
            if (this.HasSettingsLoaded)
            {
                // Value column data validation
                if (e.ColumnIndex == Manager.ValueColumnIndex)
                {
                    string key = Convert.ToString(this.dgvSettings.Rows[e.RowIndex].Cells[Manager.KeyColumnIndex].Value);
                    string value = Convert.ToString(this.dgvSettings.Rows[e.RowIndex].Cells[Manager.ValueColumnIndex].Value);

                    if (SSOSettingsManager.Instance.UpdateSetting(key, value))
                    {
                        this.DisplayStatusStripMessage(string.Format("'{0}' setting value updated.", key), false);
                    }
                    else
                    {
                        this.DisplayStatusStripMessage(string.Format("'{0}' setting value was not be updated.", key), true);
                    }
                }
            }
        }

        /// <summary>
        /// dgvSettings_CellValidating event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void dgvSettings_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.dgvSettings.IsCurrentCellDirty)
            {
                // Key column data validation
                if (e.ColumnIndex == Manager.KeyColumnIndex)
                {
                    string newKey = Convert.ToString(e.FormattedValue);
                    
                    if (string.IsNullOrWhiteSpace(newKey))
                    {
                        this.dgvSettings.Rows[e.RowIndex].ErrorText = "Setting name cannot be empty.";
                        e.Cancel = true;
                    }
                    else if (this.dgvSettings_HasDoubleKey(newKey, e.RowIndex))
                    {
                        this.dgvSettings.Rows[e.RowIndex].ErrorText = "Setting name must be unique within the collection.";
                        e.Cancel = true;
                    }
                    else
                    {
                        string oldKey = Convert.ToString(this.dgvSettings.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                        if (string.IsNullOrWhiteSpace(oldKey)) // Add Key
                        {
                            if (SSOSettingsManager.Instance.AddSetting(newKey, string.Empty))
                            {
                                this.DisplayStatusStripMessage(string.Format("'{0}' setting added to the collection.", newKey), false);
                                e.Cancel = false;
                            }
                            else
                            {
                                this.dgvSettings.Rows[e.RowIndex].ErrorText = "Error occurred while adding new setting.";
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            if (SSOSettingsManager.Instance.ReplaceSetting(oldKey, newKey))
                            {
                                this.DisplayStatusStripMessage(string.Format("Setting name updated. New name: {0}. Old name: {1}.", newKey, oldKey), false);
                                e.Cancel = false;
                            }
                            else
                            {
                                this.dgvSettings.Rows[e.RowIndex].ErrorText = "Error occurred while updating the setting name.";
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// dgvSettings_CellEndEdit event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void dgvSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvSettings.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        /// <summary>
        /// dgvSettings_MouseUp event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void dgvSettings_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hitTestInfo = this.dgvSettings.HitTest(e.X, e.Y);

                if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
                {
                    this.dgvSettings.CurrentCell = this.dgvSettings.Rows[hitTestInfo.RowIndex].Cells[Manager.KeyColumnIndex];
                    this.cmsDeleteMenu.Show(Cursor.Position);
                }
            }
        }

        #endregion

        #region ContextMenu Events

        /// <summary>
        /// deleteRowToolStripMenuItem_Click event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = this.dgvSettings.CurrentRow;

            if (!currentRow.IsNewRow)
            {
                string settingToRemove = Convert.ToString(currentRow.Cells[Manager.KeyColumnIndex].Value);

                if (SSOSettingsManager.Instance.RemoveSetting(settingToRemove))
                {
                    this.dgvSettings.Rows.Remove(currentRow);
                    this.DisplayStatusStripMessage(string.Format("'{0}' setting was removed from the collection.", settingToRemove), false);
                }
                else
                {
                    this.DisplayStatusStripMessage("Error occurred while removing the setting.", false);      
                }
            }
        }

        #endregion

        #region TextBox Events

        /// <summary>
        /// txtApplicationName_TextChanged event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void txtApplicationName_TextChanged(object sender, EventArgs e)
        {
            if (this.HasSettingsLoaded)
            {
                SSOSettingsManager.Instance.Clear();
                this.dgvSettings.Rows.Clear();

                this.HasSettingsLoaded = false;
                this.dgvSettings.AllowUserToAddRows = false;
            }
        }

        /// <summary>
        /// txtApplicationName_KeyDown event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void txtApplicationName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLoad_Click(this, new EventArgs());
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.txtApplicationName.Text = string.Empty;
            }
        }

        #endregion

        #region Load Events

        /// <summary>
        /// btnLoad_Click event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtApplicationName.Text))
            {
                this.DisplayStatusStripMessage("To load settings, an application name must be informed.", true);
                return;
            }

            SSOSettingsManager.Instance.Clear();
            this.dgvSettings.Rows.Clear();
            
            this.HasSettingsLoaded = false;
            this.dgvSettings.AllowUserToAddRows = false;
            this.btnLoad.Enabled = false;

            this.bgwLoad.RunWorkerAsync(this.txtApplicationName.Text);
        }

        /// <summary>
        /// bgwLoad_DoWork event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            SSOSettingsManager.Instance.Load(Convert.ToString(e.Argument));

            int progressIndex = 0;
            foreach (var property in SSOSettingsManager.Instance.Settings)
            {
                this.bgwLoad.ReportProgress(100 * progressIndex / SSOSettingsManager.Instance.Settings.Count, property);
                progressIndex++;
            }
        }

        /// <summary>
        /// bgwLoad_ProgressChanged event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwLoad_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgStatusStrip.Visible = true;
            this.prgStatusStrip.Value = e.ProgressPercentage;

            int line = this.dgvSettings.Rows.Add();

            KeyValuePair<string, string> entry = (KeyValuePair<string, string>)e.UserState;

            this.dgvSettings.Rows[line].Cells[Manager.KeyColumnIndex].Value = entry.Key;
            this.dgvSettings.Rows[line].Cells[Manager.ValueColumnIndex].Value = entry.Value;
        }

        /// <summary>
        /// bgwLoad_RunWorkerCompleted event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.DisplayStatusStripMessage(string.Format("Load settings failed. {0}.", e.Error.Message), true);

                SSOSettingsManager.Instance.Clear();
                this.dgvSettings.Rows.Clear();
            }
            else
            {
                this.dgvSettings.Sort(this.dgvSettings.Columns[Manager.KeyColumnIndex], ListSortDirection.Ascending);
                
                this.DisplayStatusStripMessage("Settings loaded.", false);
                
                this.HasSettingsLoaded = true;
                this.dgvSettings.AllowUserToAddRows = true;
            }

            this.prgStatusStrip.Visible = false;
            this.btnLoad.Enabled = true;
        }

        #endregion

        #region Save Events

        /// <summary>
        /// btnSave_Click event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.HasSettingsLoaded)
            {
                this.DisplayStatusStripMessage("Load settings for one application before saving.", true);
                return;
            }

            this.btnSave.Enabled = false;

            this.bgwSave.RunWorkerAsync(this.txtApplicationName.Text);
        }

        /// <summary>
        /// bgwSave_DoWork event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwSave_DoWork(object sender, DoWorkEventArgs e)
        {
            this.bgwSave.ReportProgress(0, null);

            SSOSettingsManager.Instance.Save(e.Argument.ToString());
            
            this.bgwSave.ReportProgress(100, null);
        }

        /// <summary>
        /// bgwSave_ProgressChanged event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwSave_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgStatusStrip.Visible = true;
            this.prgStatusStrip.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// bgwSave_RunWorkerCompleted event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwSave_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.DisplayStatusStripMessage(string.Format("Save settings failed. {0}.", e.Error.Message), true);
            }
            else
            {
                this.DisplayStatusStripMessage("Settings saved.", false);
            }
            
            this.prgStatusStrip.Visible = false;
            this.btnSave.Enabled = true;
        }
        
        #endregion

        #region Export Events

        /// <summary>
        /// btnExport_Click event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!this.HasSettingsLoaded)
            {
                this.DisplayStatusStripMessage("Load settings for one application before exporting.", true);
                return;
            }

            this.btnExport.Enabled = false;

            this.bgwExport.RunWorkerAsync(null);
        }

        /// <summary>
        /// bgwExport_DoWork event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            this.bgwExport.ReportProgress(0, null);

            settings ssoSettings = new settings();
            List<settingsProperty> ssoSettingsPropertyList = new List<settingsProperty>();

            int progressIndex = 0;
            foreach (var setting in SSOSettingsManager.Instance.Settings)
            {
                ssoSettingsPropertyList.Add(
                    new settingsProperty()
                    {
                        name = setting.Key,
                        Value = setting.Value
                    }
                );

                this.bgwExport.ReportProgress(100 * progressIndex / SSOSettingsManager.Instance.Settings.Count, null);
                progressIndex++;
            }

            ssoSettings.property = ssoSettingsPropertyList.ToArray();

            XDocument xmlSettings = XDocument.Parse(SerializationHelper.Instance.Serialize<settings>(ssoSettings));

            e.Result = xmlSettings;

            this.bgwExport.ReportProgress(100, null);
        }

        /// <summary>
        /// bgwExport_ProgressChanged event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgStatusStrip.Visible = true;
            this.prgStatusStrip.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// bgwExport_RunWorkerCompleted event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.DisplayStatusStripMessage(string.Format("Export settings failed. {0}.", e.Error.Message), true);
            }
            else
            {
                SaveFileDialog saveDialog = new SaveFileDialog();

                saveDialog.Title = "Save as";
                saveDialog.Filter = "Xml files (*.xml)|*.xml";
                saveDialog.FileName = this.txtApplicationName.Text;
                saveDialog.AddExtension = true;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    XDocument xmlSettings = (XDocument)e.Result;
                    xmlSettings.Save(saveDialog.FileName);

                    this.DisplayStatusStripMessage(string.Format("Settings exported to {0}.", saveDialog.FileName), false);
                }
                else
                {
                    this.DisplayStatusStripMessage("Export cancelled by user.", false);
                }
            }

            this.prgStatusStrip.Visible = false;
            this.btnExport.Enabled = true;
        }
        
        #endregion

        #region Import Events

        /// <summary>
        /// btnImport_Click event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!this.HasSettingsLoaded)
            {
                this.DisplayStatusStripMessage("Load settings for one application before importing.", true);
                return;
            }

            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Title = "Open file";
            openDialog.Filter = "Xml files (*.xml)|*.xml";
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                this.btnImport.Enabled = false;

                
                this.bgwImport.RunWorkerAsync(openDialog.FileName);
            }
            else
            {
                this.DisplayStatusStripMessage("Import cancelled by user.", false); 
            }
        }

        /// <summary>
        /// bgwImport_DoWork event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            XDocument xmlImportedSettings = XDocument.Load((string)e.Argument);

            settings ssoSettings = SerializationHelper.Instance.Deserialize<settings>(xmlImportedSettings.ToString());

            if (ssoSettings != null)
            {
                if (ssoSettings.property != null)
                {
                    this.Invoke((Action)(() => { this.dgvSettings.Rows.Clear(); }));

                    int progressIndex = 0;
                    foreach (var ssoProperty in ssoSettings.property)
                    {
                        this.bgwImport.ReportProgress(100 * progressIndex / ssoSettings.property.Length, ssoProperty);
                        progressIndex++;
                    }

                    e.Result = ssoSettings;
                }
                else
                {
                    throw new XmlException("There is an error in the XML document. Property list cannot be empty.");
                }
            }
            else
            {
                throw new XmlException("There is an error in the XML document.");
            }
        }

        /// <summary>
        /// bgwImport_ProgressChanged event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgStatusStrip.Visible = true;
            this.prgStatusStrip.Value = e.ProgressPercentage;

            int line = this.dgvSettings.Rows.Add();

            settingsProperty ssoProperty = (settingsProperty)e.UserState;

            this.dgvSettings.Rows[line].Cells[Manager.KeyColumnIndex].Value = ssoProperty.name;
            this.dgvSettings.Rows[line].Cells[Manager.ValueColumnIndex].Value = ssoProperty.Value;
        }

        /// <summary>
        /// bgwImport_RunWorkerCompleted event implementation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void bgwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.DisplayStatusStripMessage(string.Format("Import settings failed. {0}.", e.Error.Message), true);
            }
            else
            {
                SSOSettingsManager.Instance.Load((settings)e.Result);
                
                this.dgvSettings.Sort(this.dgvSettings.Columns[Manager.KeyColumnIndex], ListSortDirection.Ascending);

                this.DisplayStatusStripMessage("Settings imported.", false);
            }

            this.prgStatusStrip.Visible = false;
            this.btnImport.Enabled = true;
        }

        #endregion
    }
}