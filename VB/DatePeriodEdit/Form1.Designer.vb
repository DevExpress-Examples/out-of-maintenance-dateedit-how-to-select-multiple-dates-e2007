Imports Microsoft.VisualBasic
Imports System
Namespace DatePeriodEdit_NS
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim periodsSet1 As New DatePeriodEdit_NS.PeriodsSet()
			Me.datePeriodEdit1 = New DatePeriodEdit_NS.DatePeriodEdit()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.gridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.gridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.repositoryItemDatePeriodEdit1 = New DatePeriodEdit_NS.RepositoryItemDatePeriodEdit()
			Me.gridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.repositoryItemDatePeriodEdit2 = New DatePeriodEdit_NS.RepositoryItemDatePeriodEdit()
			Me.gridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.multiselectComboBoxEdit1 = New DevExpress.XtraEditors.ComboBoxEdit()
			Me.labelControl1 = New DevExpress.XtraEditors.LabelControl()
			Me.showWeekLevelCheckEdit1 = New DevExpress.XtraEditors.CheckEdit()
			Me.labelControl2 = New DevExpress.XtraEditors.LabelControl()
			Me.lowLevelComboBoxEdit2 = New DevExpress.XtraEditors.ComboBoxEdit()
			Me.hightLevelComboBoxEdit3 = New DevExpress.XtraEditors.ComboBoxEdit()
			Me.labelControl3 = New DevExpress.XtraEditors.LabelControl()
			Me.storeModeComboBoxEdit4 = New DevExpress.XtraEditors.ComboBoxEdit()
			Me.labelControl4 = New DevExpress.XtraEditors.LabelControl()
			Me.labelControl5 = New DevExpress.XtraEditors.LabelControl()
			Me.separatorCharTextEdit = New DevExpress.XtraEditors.TextEdit()
			Me.showWeekNumbersCheckEdit = New DevExpress.XtraEditors.CheckEdit()
			CType(Me.datePeriodEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.datePeriodEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.repositoryItemDatePeriodEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.repositoryItemDatePeriodEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.repositoryItemDatePeriodEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.repositoryItemDatePeriodEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView2, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.multiselectComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.showWeekLevelCheckEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.lowLevelComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.hightLevelComboBoxEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.storeModeComboBoxEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.separatorCharTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.showWeekNumbersCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' datePeriodEdit1
			' 
			Me.datePeriodEdit1.Dock = System.Windows.Forms.DockStyle.Top
			Me.datePeriodEdit1.EditValue = periodsSet1
			Me.datePeriodEdit1.Location = New System.Drawing.Point(0, 0)
			Me.datePeriodEdit1.Margin = New System.Windows.Forms.Padding(4)
			Me.datePeriodEdit1.Name = "datePeriodEdit1"
			Me.datePeriodEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.datePeriodEdit1.Properties.EditFormat.FormatString = "d"
			Me.datePeriodEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
			Me.datePeriodEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
			Me.datePeriodEdit1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton()})
			Me.datePeriodEdit1.Size = New System.Drawing.Size(598, 22)
			Me.datePeriodEdit1.TabIndex = 0
'			Me.datePeriodEdit1.EditValueChanged += New System.EventHandler(Me.datePeriodEdit1_EditValueChanged);
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.gridControl1.Location = New System.Drawing.Point(0, 172)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() { Me.repositoryItemDatePeriodEdit1, Me.repositoryItemDatePeriodEdit2})
			Me.gridControl1.Size = New System.Drawing.Size(598, 348)
			Me.gridControl1.TabIndex = 1
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1, Me.gridView2})
			' 
			' gridView1
			' 
			Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.gridColumn1, Me.gridColumn2, Me.gridColumn3})
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			' 
			' gridColumn1
			' 
			Me.gridColumn1.Caption = "Name"
			Me.gridColumn1.FieldName = "Name"
			Me.gridColumn1.Name = "gridColumn1"
			Me.gridColumn1.Visible = True
			Me.gridColumn1.VisibleIndex = 0
			' 
			' gridColumn2
			' 
			Me.gridColumn2.Caption = "PeriodSet"
			Me.gridColumn2.ColumnEdit = Me.repositoryItemDatePeriodEdit1
			Me.gridColumn2.FieldName = "PeriodsSet"
			Me.gridColumn2.Name = "gridColumn2"
			Me.gridColumn2.Visible = True
			Me.gridColumn2.VisibleIndex = 1
			' 
			' repositoryItemDatePeriodEdit1
			' 
			Me.repositoryItemDatePeriodEdit1.AutoHeight = False
			Me.repositoryItemDatePeriodEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.repositoryItemDatePeriodEdit1.EditFormat.FormatString = "d"
			Me.repositoryItemDatePeriodEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
			Me.repositoryItemDatePeriodEdit1.Name = "repositoryItemDatePeriodEdit1"
			Me.repositoryItemDatePeriodEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
			Me.repositoryItemDatePeriodEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton()})
			' 
			' gridColumn3
			' 
			Me.gridColumn3.Caption = "Periods string"
			Me.gridColumn3.ColumnEdit = Me.repositoryItemDatePeriodEdit2
			Me.gridColumn3.FieldName = "PeriodsString"
			Me.gridColumn3.Name = "gridColumn3"
			Me.gridColumn3.Visible = True
			Me.gridColumn3.VisibleIndex = 2
			' 
			' repositoryItemDatePeriodEdit2
			' 
			Me.repositoryItemDatePeriodEdit2.AutoHeight = False
			Me.repositoryItemDatePeriodEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.repositoryItemDatePeriodEdit2.EditFormat.FormatString = "d"
			Me.repositoryItemDatePeriodEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
			Me.repositoryItemDatePeriodEdit2.Name = "repositoryItemDatePeriodEdit2"
			Me.repositoryItemDatePeriodEdit2.PeriodsStoreMode = DatePeriodEdit_NS.StoreMode.String
			Me.repositoryItemDatePeriodEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
			Me.repositoryItemDatePeriodEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton()})
			' 
			' gridView2
			' 
			Me.gridView2.GridControl = Me.gridControl1
			Me.gridView2.Name = "gridView2"
			' 
			' multiselectComboBoxEdit1
			' 
			Me.multiselectComboBoxEdit1.Location = New System.Drawing.Point(12, 52)
			Me.multiselectComboBoxEdit1.Name = "multiselectComboBoxEdit1"
			Me.multiselectComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.multiselectComboBoxEdit1.Properties.DropDownRows = 3
			Me.multiselectComboBoxEdit1.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value
			Me.multiselectComboBoxEdit1.Size = New System.Drawing.Size(162, 22)
			Me.multiselectComboBoxEdit1.TabIndex = 2
'			Me.multiselectComboBoxEdit1.EditValueChanged += New System.EventHandler(Me.EditValueChanged);
			' 
			' labelControl1
			' 
			Me.labelControl1.Location = New System.Drawing.Point(12, 29)
			Me.labelControl1.Name = "labelControl1"
			Me.labelControl1.Size = New System.Drawing.Size(116, 16)
			Me.labelControl1.TabIndex = 3
			Me.labelControl1.Text = "MultiselectBehaviour"
			' 
			' showWeekLevelCheckEdit1
			' 
			Me.showWeekLevelCheckEdit1.Location = New System.Drawing.Point(395, 91)
			Me.showWeekLevelCheckEdit1.Name = "showWeekLevelCheckEdit1"
			Me.showWeekLevelCheckEdit1.Properties.Caption = "ShowWeekLevel"
			Me.showWeekLevelCheckEdit1.Size = New System.Drawing.Size(135, 21)
			Me.showWeekLevelCheckEdit1.TabIndex = 4
'			Me.showWeekLevelCheckEdit1.EditValueChanged += New System.EventHandler(Me.EditValueChanged);
			' 
			' labelControl2
			' 
			Me.labelControl2.Location = New System.Drawing.Point(205, 30)
			Me.labelControl2.Name = "labelControl2"
			Me.labelControl2.Size = New System.Drawing.Size(52, 16)
			Me.labelControl2.TabIndex = 5
			Me.labelControl2.Text = "LowLevel"
			' 
			' lowLevelComboBoxEdit2
			' 
			Me.lowLevelComboBoxEdit2.Location = New System.Drawing.Point(205, 52)
			Me.lowLevelComboBoxEdit2.Name = "lowLevelComboBoxEdit2"
			Me.lowLevelComboBoxEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.lowLevelComboBoxEdit2.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value
			Me.lowLevelComboBoxEdit2.Size = New System.Drawing.Size(162, 22)
			Me.lowLevelComboBoxEdit2.TabIndex = 6
'			Me.lowLevelComboBoxEdit2.EditValueChanged += New System.EventHandler(Me.lowLevelComboBoxEdit2_EditValueChanged);
			' 
			' hightLevelComboBoxEdit3
			' 
			Me.hightLevelComboBoxEdit3.Location = New System.Drawing.Point(397, 52)
			Me.hightLevelComboBoxEdit3.Name = "hightLevelComboBoxEdit3"
			Me.hightLevelComboBoxEdit3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.hightLevelComboBoxEdit3.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value
			Me.hightLevelComboBoxEdit3.Size = New System.Drawing.Size(162, 22)
			Me.hightLevelComboBoxEdit3.TabIndex = 8
'			Me.hightLevelComboBoxEdit3.EditValueChanged += New System.EventHandler(Me.hightLevelComboBoxEdit3_EditValueChanged);
			' 
			' labelControl3
			' 
			Me.labelControl3.Location = New System.Drawing.Point(397, 30)
			Me.labelControl3.Name = "labelControl3"
			Me.labelControl3.Size = New System.Drawing.Size(58, 16)
			Me.labelControl3.TabIndex = 7
			Me.labelControl3.Text = "HightLevel"
			' 
			' storeModeComboBoxEdit4
			' 
			Me.storeModeComboBoxEdit4.Location = New System.Drawing.Point(12, 102)
			Me.storeModeComboBoxEdit4.Name = "storeModeComboBoxEdit4"
			Me.storeModeComboBoxEdit4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.storeModeComboBoxEdit4.Size = New System.Drawing.Size(162, 22)
			Me.storeModeComboBoxEdit4.TabIndex = 9
'			Me.storeModeComboBoxEdit4.EditValueChanged += New System.EventHandler(Me.EditValueChanged);
			' 
			' labelControl4
			' 
			Me.labelControl4.Location = New System.Drawing.Point(12, 80)
			Me.labelControl4.Name = "labelControl4"
			Me.labelControl4.Size = New System.Drawing.Size(98, 16)
			Me.labelControl4.TabIndex = 10
			Me.labelControl4.Text = "PeriodStoreMode"
			' 
			' labelControl5
			' 
			Me.labelControl5.Location = New System.Drawing.Point(205, 81)
			Me.labelControl5.Name = "labelControl5"
			Me.labelControl5.Size = New System.Drawing.Size(86, 16)
			Me.labelControl5.TabIndex = 11
			Me.labelControl5.Text = "Separator char"
			' 
			' separatorCharTextEdit
			' 
			Me.separatorCharTextEdit.Location = New System.Drawing.Point(205, 102)
			Me.separatorCharTextEdit.Name = "separatorCharTextEdit"
			Me.separatorCharTextEdit.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None
			Me.separatorCharTextEdit.Properties.Mask.EditMask = "."
			Me.separatorCharTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
			Me.separatorCharTextEdit.Properties.Mask.ShowPlaceHolders = False
			Me.separatorCharTextEdit.Properties.ValidateOnEnterKey = True
			Me.separatorCharTextEdit.Size = New System.Drawing.Size(162, 22)
			Me.separatorCharTextEdit.TabIndex = 12
'			Me.separatorCharTextEdit.EditValueChanged += New System.EventHandler(Me.EditValueChanged);
			' 
			' showWeekNumbersCheckEdit
			' 
			Me.showWeekNumbersCheckEdit.Location = New System.Drawing.Point(395, 118)
			Me.showWeekNumbersCheckEdit.Name = "showWeekNumbersCheckEdit"
			Me.showWeekNumbersCheckEdit.Properties.Caption = "ShowWeekNumbers"
			Me.showWeekNumbersCheckEdit.Size = New System.Drawing.Size(143, 21)
			Me.showWeekNumbersCheckEdit.TabIndex = 13
'			Me.showWeekNumbersCheckEdit.EditValueChanged += New System.EventHandler(Me.EditValueChanged);
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(8F, 16F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(598, 520)
			Me.Controls.Add(Me.showWeekNumbersCheckEdit)
			Me.Controls.Add(Me.separatorCharTextEdit)
			Me.Controls.Add(Me.labelControl5)
			Me.Controls.Add(Me.labelControl4)
			Me.Controls.Add(Me.storeModeComboBoxEdit4)
			Me.Controls.Add(Me.hightLevelComboBoxEdit3)
			Me.Controls.Add(Me.labelControl3)
			Me.Controls.Add(Me.lowLevelComboBoxEdit2)
			Me.Controls.Add(Me.labelControl2)
			Me.Controls.Add(Me.showWeekLevelCheckEdit1)
			Me.Controls.Add(Me.labelControl1)
			Me.Controls.Add(Me.multiselectComboBoxEdit1)
			Me.Controls.Add(Me.gridControl1)
			Me.Controls.Add(Me.datePeriodEdit1)
			Me.Margin = New System.Windows.Forms.Padding(4)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.datePeriodEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.datePeriodEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.repositoryItemDatePeriodEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.repositoryItemDatePeriodEdit1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.repositoryItemDatePeriodEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.repositoryItemDatePeriodEdit2, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView2, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.multiselectComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.showWeekLevelCheckEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.lowLevelComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.hightLevelComboBoxEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.storeModeComboBoxEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.separatorCharTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.showWeekNumbersCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents datePeriodEdit1 As DatePeriodEdit
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private gridView2 As DevExpress.XtraGrid.Views.Grid.GridView
		Private gridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
		Private gridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
		Private gridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
		Private repositoryItemDatePeriodEdit1 As RepositoryItemDatePeriodEdit
		Private repositoryItemDatePeriodEdit2 As RepositoryItemDatePeriodEdit
		Private WithEvents multiselectComboBoxEdit1 As DevExpress.XtraEditors.ComboBoxEdit
		Private labelControl1 As DevExpress.XtraEditors.LabelControl
		Private WithEvents showWeekLevelCheckEdit1 As DevExpress.XtraEditors.CheckEdit
		Private labelControl2 As DevExpress.XtraEditors.LabelControl
		Private WithEvents lowLevelComboBoxEdit2 As DevExpress.XtraEditors.ComboBoxEdit
		Private WithEvents hightLevelComboBoxEdit3 As DevExpress.XtraEditors.ComboBoxEdit
		Private labelControl3 As DevExpress.XtraEditors.LabelControl
		Private WithEvents storeModeComboBoxEdit4 As DevExpress.XtraEditors.ComboBoxEdit
		Private labelControl4 As DevExpress.XtraEditors.LabelControl
		Private labelControl5 As DevExpress.XtraEditors.LabelControl
		Private WithEvents separatorCharTextEdit As DevExpress.XtraEditors.TextEdit
		Private WithEvents showWeekNumbersCheckEdit As DevExpress.XtraEditors.CheckEdit

	End Class
End Namespace

