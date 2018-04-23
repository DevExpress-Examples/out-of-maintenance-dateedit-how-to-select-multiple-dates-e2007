Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections

Namespace DatePeriodEdit_NS
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private myUsers As New MyUsers()
		Private isPopulated As Boolean = False
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			myUsers.Add(New User("Antuan", "Acapulco", 23))
			myUsers.Add(New User("Bill", "Brucel", 17))
			myUsers.Add(New User("Charli", "Chikago", 45))
			myUsers.Add(New User("Denn", "Denver", 20))
			myUsers.Add(New User("Eva", "Everton", 23))
			gridControl1.DataSource = myUsers

			PopulateControls()

		End Sub

		Private Sub PopulateControls()
			multiselectComboBoxEdit1.Properties.Items.AddRange(System.Enum.GetValues(GetType(MultiselectBehaviour)))
			multiselectComboBoxEdit1.EditValue = datePeriodEdit1.Properties.OptionsSelection.MultiselectBehaviour
			lowLevelComboBoxEdit2.Properties.Items.AddRange(System.Enum.GetValues(GetType(ViewLevel)))
			lowLevelComboBoxEdit2.EditValue = datePeriodEdit1.Properties.OptionsSelection.LowLevel
			hightLevelComboBoxEdit3.Properties.Items.AddRange(System.Enum.GetValues(GetType(ViewLevel)))
			hightLevelComboBoxEdit3.EditValue = datePeriodEdit1.Properties.OptionsSelection.HightLevel
			storeModeComboBoxEdit4.Properties.Items.AddRange(System.Enum.GetValues(GetType(StoreMode)))
			storeModeComboBoxEdit4.EditValue = datePeriodEdit1.Properties.PeriodsStoreMode
			showWeekLevelCheckEdit1.Checked = datePeriodEdit1.Properties.OptionsSelection.ShowWeekLevel
			showWeekNumbersCheckEdit.Checked = datePeriodEdit1.Properties.ShowWeekNumbers
			separatorCharTextEdit.Text = datePeriodEdit1.Properties.SeparatorChar.ToString()
			isPopulated = True
			AcceptControls()
		End Sub
		Private Sub AcceptControls()
			If (Not isPopulated) Then
				Return
			End If
			Dim multiselectBehaviour As MultiselectBehaviour = CType(multiselectComboBoxEdit1.EditValue, MultiselectBehaviour)
			Dim lowLevel As ViewLevel = CType(lowLevelComboBoxEdit2.EditValue, ViewLevel)
			Dim highLevel As ViewLevel = CType(hightLevelComboBoxEdit3.EditValue, ViewLevel)
			Dim separatorChar As Char
			Dim showWeekNumbers As Boolean = showWeekNumbersCheckEdit.Checked
			Dim showWeekLevel As Boolean = showWeekLevelCheckEdit1.Checked
			If separatorCharTextEdit.Text.Length = 0 Then
				separatorChar = ControlChars.NullChar
			Else
				separatorChar = separatorCharTextEdit.Text.ToCharArray()(0)
			End If

			datePeriodEdit1.Properties.SeparatorChar = separatorChar
			datePeriodEdit1.Properties.OptionsSelection.MultiselectBehaviour = multiselectBehaviour
			datePeriodEdit1.Properties.OptionsSelection.ShowWeekLevel = showWeekLevel
			datePeriodEdit1.Properties.OptionsSelection.LowLevel = lowLevel
			datePeriodEdit1.Properties.OptionsSelection.HightLevel = highLevel
			datePeriodEdit1.Properties.ShowWeekNumbers = showWeekNumbers
			datePeriodEdit1.Properties.PeriodsStoreMode = CType(storeModeComboBoxEdit4.EditValue, StoreMode)

			repositoryItemDatePeriodEdit1.SeparatorChar = separatorChar
			repositoryItemDatePeriodEdit1.OptionsSelection.MultiselectBehaviour = multiselectBehaviour
			repositoryItemDatePeriodEdit1.OptionsSelection.ShowWeekLevel = showWeekLevel
			repositoryItemDatePeriodEdit1.OptionsSelection.LowLevel = lowLevel
			repositoryItemDatePeriodEdit1.OptionsSelection.HightLevel = highLevel
			repositoryItemDatePeriodEdit1.ShowWeekNumbers = showWeekNumbers

			repositoryItemDatePeriodEdit2.SeparatorChar = separatorChar
			repositoryItemDatePeriodEdit2.OptionsSelection.MultiselectBehaviour = multiselectBehaviour
			repositoryItemDatePeriodEdit2.OptionsSelection.ShowWeekLevel = showWeekLevel
			repositoryItemDatePeriodEdit2.OptionsSelection.LowLevel = lowLevel
			repositoryItemDatePeriodEdit2.OptionsSelection.HightLevel = highLevel
			repositoryItemDatePeriodEdit2.ShowWeekNumbers = showWeekNumbers
		End Sub


		Private Sub datePeriodEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles datePeriodEdit1.EditValueChanged
			Me.Text = datePeriodEdit1.EditValue.GetType().ToString() & ": " & datePeriodEdit1.EditValue.ToString()
		End Sub

		Private Sub EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles multiselectComboBoxEdit1.EditValueChanged, showWeekLevelCheckEdit1.EditValueChanged, storeModeComboBoxEdit4.EditValueChanged, separatorCharTextEdit.EditValueChanged, showWeekNumbersCheckEdit.EditValueChanged
			AcceptControls()
		End Sub

		Private Sub lowLevelComboBoxEdit2_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lowLevelComboBoxEdit2.EditValueChanged
			If hightLevelComboBoxEdit3.SelectedIndex < lowLevelComboBoxEdit2.SelectedIndex Then
				hightLevelComboBoxEdit3.SelectedIndex = lowLevelComboBoxEdit2.SelectedIndex
			End If
			AcceptControls()
		End Sub

		Private Sub hightLevelComboBoxEdit3_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles hightLevelComboBoxEdit3.EditValueChanged
			If hightLevelComboBoxEdit3.SelectedIndex < lowLevelComboBoxEdit2.SelectedIndex Then
				lowLevelComboBoxEdit2.SelectedIndex = hightLevelComboBoxEdit3.SelectedIndex
			End If
			AcceptControls()
		End Sub


	End Class
	Public Class User
		Private name_Renamed, city_Renamed, periodsString_Renamed As String
		Private age_Renamed As Integer
		Private periodsSet_Renamed As PeriodsSet
		Public Sub New(ByVal name As String, ByVal city As String, ByVal age As Integer)
			Me.name_Renamed = name
			Me.city_Renamed = city
			Me.age_Renamed = age
			periodsSet_Renamed = New PeriodsSet()
			periodsSet_Renamed.MergeWith(DateTime.Today, DateTime.Today)
			periodsSet_Renamed.MergeWith(DateTime.Today.AddDays(5), DateTime.Today.AddDays(8))
			periodsString_Renamed = periodsSet_Renamed.ToString()
		End Sub
		Public Property Age() As Integer
			Set(ByVal value As Integer)
				age_Renamed = value
			End Set
			Get
				Return age_Renamed
			End Get
		End Property
		Public Property Name() As String
			Set(ByVal value As String)
				name_Renamed = value
			End Set
			Get
				Return name_Renamed
			End Get
		End Property
		Public Property City() As String
			Set(ByVal value As String)
				city_Renamed = value
			End Set
			Get
				Return city_Renamed
			End Get
		End Property
		Public Property PeriodsString() As String
			Set(ByVal value As String)
				periodsString_Renamed = value
			End Set
			Get
				Return periodsString_Renamed
			End Get
		End Property
		Public Property PeriodsSet() As PeriodsSet
			Set(ByVal value As PeriodsSet)
				periodsSet_Renamed = value
			End Set
			Get
				Return periodsSet_Renamed
			End Get
		End Property
	End Class
	Public Class MyUsers
		Inherits ArrayList
		Default Public Shadows Overridable Property Item(ByVal index As Integer) As User
			Get
				Return TryCast(MyBase.Item(index), User)
			End Get
			Set(ByVal value As User)
				MyBase.Item(index) = value
			End Set
		End Property
	End Class

End Namespace