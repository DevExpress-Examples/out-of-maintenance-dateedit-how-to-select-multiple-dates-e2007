namespace DatePeriodEdit_NS
{
    partial class Form1
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
            DatePeriodEdit_NS.PeriodsSet periodsSet1 = new DatePeriodEdit_NS.PeriodsSet();
            this.datePeriodEdit1 = new DatePeriodEdit_NS.DatePeriodEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDatePeriodEdit1 = new DatePeriodEdit_NS.RepositoryItemDatePeriodEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDatePeriodEdit2 = new DatePeriodEdit_NS.RepositoryItemDatePeriodEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.multiselectComboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.showWeekLevelCheckEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lowLevelComboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.hightLevelComboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.storeModeComboBoxEdit4 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.separatorCharTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.showWeekNumbersCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.datePeriodEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datePeriodEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit2.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiselectComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWeekLevelCheckEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowLevelComboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hightLevelComboBoxEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storeModeComboBoxEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorCharTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWeekNumbersCheckEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // datePeriodEdit1
            // 
            this.datePeriodEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.datePeriodEdit1.EditValue = periodsSet1;
            this.datePeriodEdit1.Location = new System.Drawing.Point(0, 0);
            this.datePeriodEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.datePeriodEdit1.Name = "datePeriodEdit1";
            this.datePeriodEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datePeriodEdit1.Properties.EditFormat.FormatString = "d";
            this.datePeriodEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datePeriodEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.datePeriodEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.datePeriodEdit1.Size = new System.Drawing.Size(598, 22);
            this.datePeriodEdit1.TabIndex = 0;
            this.datePeriodEdit1.EditValueChanged += new System.EventHandler(this.datePeriodEdit1_EditValueChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridControl1.Location = new System.Drawing.Point(0, 172);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDatePeriodEdit1,
            this.repositoryItemDatePeriodEdit2});
            this.gridControl1.Size = new System.Drawing.Size(598, 348);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Name";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "PeriodSet";
            this.gridColumn2.ColumnEdit = this.repositoryItemDatePeriodEdit1;
            this.gridColumn2.FieldName = "PeriodsSet";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // repositoryItemDatePeriodEdit1
            // 
            this.repositoryItemDatePeriodEdit1.AutoHeight = false;
            this.repositoryItemDatePeriodEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDatePeriodEdit1.EditFormat.FormatString = "d";
            this.repositoryItemDatePeriodEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDatePeriodEdit1.Name = "repositoryItemDatePeriodEdit1";
            this.repositoryItemDatePeriodEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemDatePeriodEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Periods string";
            this.gridColumn3.ColumnEdit = this.repositoryItemDatePeriodEdit2;
            this.gridColumn3.FieldName = "PeriodsString";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // repositoryItemDatePeriodEdit2
            // 
            this.repositoryItemDatePeriodEdit2.AutoHeight = false;
            this.repositoryItemDatePeriodEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDatePeriodEdit2.EditFormat.FormatString = "d";
            this.repositoryItemDatePeriodEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDatePeriodEdit2.Name = "repositoryItemDatePeriodEdit2";
            this.repositoryItemDatePeriodEdit2.PeriodsStoreMode = DatePeriodEdit_NS.StoreMode.String;
            this.repositoryItemDatePeriodEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemDatePeriodEdit2.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // multiselectComboBoxEdit1
            // 
            this.multiselectComboBoxEdit1.Location = new System.Drawing.Point(12, 52);
            this.multiselectComboBoxEdit1.Name = "multiselectComboBoxEdit1";
            this.multiselectComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.multiselectComboBoxEdit1.Properties.DropDownRows = 3;
            this.multiselectComboBoxEdit1.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.multiselectComboBoxEdit1.Size = new System.Drawing.Size(162, 22);
            this.multiselectComboBoxEdit1.TabIndex = 2;
            this.multiselectComboBoxEdit1.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(116, 16);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "MultiselectBehaviour";
            // 
            // showWeekLevelCheckEdit1
            // 
            this.showWeekLevelCheckEdit1.Location = new System.Drawing.Point(395, 91);
            this.showWeekLevelCheckEdit1.Name = "showWeekLevelCheckEdit1";
            this.showWeekLevelCheckEdit1.Properties.Caption = "ShowWeekLevel";
            this.showWeekLevelCheckEdit1.Size = new System.Drawing.Size(135, 21);
            this.showWeekLevelCheckEdit1.TabIndex = 4;
            this.showWeekLevelCheckEdit1.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(205, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 16);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "LowLevel";
            // 
            // lowLevelComboBoxEdit2
            // 
            this.lowLevelComboBoxEdit2.Location = new System.Drawing.Point(205, 52);
            this.lowLevelComboBoxEdit2.Name = "lowLevelComboBoxEdit2";
            this.lowLevelComboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lowLevelComboBoxEdit2.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.lowLevelComboBoxEdit2.Size = new System.Drawing.Size(162, 22);
            this.lowLevelComboBoxEdit2.TabIndex = 6;
            this.lowLevelComboBoxEdit2.EditValueChanged += new System.EventHandler(this.lowLevelComboBoxEdit2_EditValueChanged);
            // 
            // hightLevelComboBoxEdit3
            // 
            this.hightLevelComboBoxEdit3.Location = new System.Drawing.Point(397, 52);
            this.hightLevelComboBoxEdit3.Name = "hightLevelComboBoxEdit3";
            this.hightLevelComboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.hightLevelComboBoxEdit3.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.hightLevelComboBoxEdit3.Size = new System.Drawing.Size(162, 22);
            this.hightLevelComboBoxEdit3.TabIndex = 8;
            this.hightLevelComboBoxEdit3.EditValueChanged += new System.EventHandler(this.hightLevelComboBoxEdit3_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(397, 30);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 16);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "HightLevel";
            // 
            // storeModeComboBoxEdit4
            // 
            this.storeModeComboBoxEdit4.Location = new System.Drawing.Point(12, 102);
            this.storeModeComboBoxEdit4.Name = "storeModeComboBoxEdit4";
            this.storeModeComboBoxEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.storeModeComboBoxEdit4.Size = new System.Drawing.Size(162, 22);
            this.storeModeComboBoxEdit4.TabIndex = 9;
            this.storeModeComboBoxEdit4.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 80);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(98, 16);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "PeriodStoreMode";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(205, 81);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(86, 16);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Separator char";
            // 
            // separatorCharTextEdit
            // 
            this.separatorCharTextEdit.Location = new System.Drawing.Point(205, 102);
            this.separatorCharTextEdit.Name = "separatorCharTextEdit";
            this.separatorCharTextEdit.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.separatorCharTextEdit.Properties.Mask.EditMask = ".";
            this.separatorCharTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.separatorCharTextEdit.Properties.Mask.ShowPlaceHolders = false;
            this.separatorCharTextEdit.Properties.ValidateOnEnterKey = true;
            this.separatorCharTextEdit.Size = new System.Drawing.Size(162, 22);
            this.separatorCharTextEdit.TabIndex = 12;
            this.separatorCharTextEdit.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // showWeekNumbersCheckEdit
            // 
            this.showWeekNumbersCheckEdit.Location = new System.Drawing.Point(395, 118);
            this.showWeekNumbersCheckEdit.Name = "showWeekNumbersCheckEdit";
            this.showWeekNumbersCheckEdit.Properties.Caption = "ShowWeekNumbers";
            this.showWeekNumbersCheckEdit.Size = new System.Drawing.Size(143, 21);
            this.showWeekNumbersCheckEdit.TabIndex = 13;
            this.showWeekNumbersCheckEdit.EditValueChanged += new System.EventHandler(this.EditValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 520);
            this.Controls.Add(this.showWeekNumbersCheckEdit);
            this.Controls.Add(this.separatorCharTextEdit);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.storeModeComboBoxEdit4);
            this.Controls.Add(this.hightLevelComboBoxEdit3);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lowLevelComboBoxEdit2);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.showWeekLevelCheckEdit1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.multiselectComboBoxEdit1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.datePeriodEdit1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datePeriodEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datePeriodEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit2.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDatePeriodEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiselectComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWeekLevelCheckEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowLevelComboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hightLevelComboBoxEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storeModeComboBoxEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorCharTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showWeekNumbersCheckEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatePeriodEdit datePeriodEdit1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private RepositoryItemDatePeriodEdit repositoryItemDatePeriodEdit1;
        private RepositoryItemDatePeriodEdit repositoryItemDatePeriodEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit multiselectComboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit showWeekLevelCheckEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit lowLevelComboBoxEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit hightLevelComboBoxEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit storeModeComboBoxEdit4;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit separatorCharTextEdit;
        private DevExpress.XtraEditors.CheckEdit showWeekNumbersCheckEdit;

    }
}

