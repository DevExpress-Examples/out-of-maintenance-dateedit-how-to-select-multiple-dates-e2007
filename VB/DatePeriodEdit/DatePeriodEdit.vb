Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports System
Imports System.ComponentModel

Namespace DatePeriodEdit_NS
	<UserRepositoryItem("RegisterDatePeriodEdit")>
	Public Class RepositoryItemDatePeriodEdit
		Inherits RepositoryItemDateEdit

	   Public Const DatePeriodEditName As String = "DatePeriodEdit"
	   Private _separatorChar As Char = ","c

	   Shared Sub New()
		   RegisterDatePeriodEdit()
	   End Sub

	   Public Sub New()
		   TextEditStyle = TextEditStyles.DisableTextEditor
		   ShowOk = DefaultBoolean.True
	   End Sub

	   Public Overrides ReadOnly Property EditorTypeName() As String
		   Get
			   Return DatePeriodEditName
		   End Get
	   End Property

	   <Description("Gets or sets the character separating periods"), Category(CategoryName.Format), DefaultValue(","c)>
	   Public Property SeparatorChar() As Char
		   Get
			   Return _separatorChar
		   End Get
		   Set(ByVal value As Char)
			   If SeparatorChar = value Then
				   Return
			   End If
			   _separatorChar = value
			   OnPropertiesChanged()
		   End Set
	   End Property

	   Public Shared Sub RegisterDatePeriodEdit()
		   EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(DatePeriodEditName, GetType(DatePeriodEdit), GetType(RepositoryItemDatePeriodEdit), GetType(DateEditViewInfo), New ButtonEditPainter(), True))
	   End Sub

	   Public Overrides Sub Assign(ByVal item As RepositoryItem)
		   MyBase.Assign(item)
		   Dim source = TryCast(item, RepositoryItemDatePeriodEdit)
		   If source IsNot Nothing Then
			   _separatorChar = source.SeparatorChar
		   End If
	   End Sub

	   Protected Overrides Function IsNullValue(ByVal editValue As Object) As Boolean
		   Dim value = TryCast(editValue, PeriodsSet)
		   If value IsNot Nothing Then
			   Return value.Periods.Count = 0
		   End If
		   Dim s = TryCast(editValue, String)
		   If s Is Nothing Then
			   Return False
		   End If

		   Return PeriodsSet.Parse(s).Periods.Count = 0
	   End Function

	   Public Overrides Function GetDisplayText(ByVal format As FormatInfo, ByVal editValue As Object) As String
		   Dim displayText = String.Empty
'INSTANT VB NOTE: The variable period was renamed since it may cause conflicts with calls to static members of the user-defined type with this name:
		   Dim period_Renamed = TryCast(editValue, PeriodsSet)
		   If period_Renamed IsNot Nothing Then
			   displayText = period_Renamed.ToString(format.FormatString, SeparatorChar)
		   Else
			   Dim s = TryCast(editValue, String)
			   If s IsNot Nothing Then
				   displayText = PeriodsSet.Parse(s).ToString(format.FormatString, SeparatorChar)
			   End If
		   End If
		   Dim e = New CustomDisplayTextEventArgs(editValue, displayText)
		   If format IsNot EditFormat Then
			   RaiseCustomDisplayText(e)
		   End If
		   Return e.DisplayText
	   End Function
	End Class
	Public Class PopupDatePeriodEditForm
		Inherits PopupDateEditForm

		Public Sub New(ByVal ownerEdit As DateEdit)
			MyBase.New(ownerEdit)
		End Sub

		Public Overrides Sub ShowPopupForm()
			MyBase.ShowPopupForm()
			SetSelectedRange()
		End Sub

		Protected Overrides Sub AssignCalendarProperties()
			MyBase.AssignCalendarProperties()
			Calendar.SelectionMode = CalendarSelectionMode.Multiple
			Calendar.CaseWeekDayAbbreviations = TextCaseMode.SentenceCase
			Calendar.ShowMonthHeaders = False
			Calendar.SyncSelectionWithEditValue = False
		End Sub

		Private Sub SetSelectedRange()
			Dim value = TryCast(OwnerEdit.EditValue, PeriodsSet)
			If value IsNot Nothing AndAlso value.Periods.Count > 0 Then
				Calendar.EditValue = DirectCast(value.Periods(value.Periods.Count - 1), Period).End
				Calendar.SelectedRanges.BeginUpdate()
				Calendar.SelectedRanges.Clear()
'INSTANT VB NOTE: The variable period was renamed since it may cause conflicts with calls to static members of the user-defined type with this name:
				For Each period_Renamed As Period In value.Periods
					Calendar.SelectedRanges.Add(New DateRange(period_Renamed.Begin, period_Renamed.End))
				Next period_Renamed
				Calendar.SelectedRanges.EndUpdate()
			Else
				Calendar.EditValue = OwnerEdit.DateTime
			End If
		End Sub

	End Class
	Public Class DatePeriodEdit
		Inherits DateEdit

		 Shared Sub New()
			 RepositoryItemDatePeriodEdit.RegisterDatePeriodEdit()
		 End Sub

	   Public Sub New()
		   EditValue = New PeriodsSet()
	   End Sub

	   Public Overrides Property EditValue() As Object
		   Get
			   Dim value = TryCast(MyBase.EditValue, PeriodsSet)
			   Return If(value, New PeriodsSet())
		   End Get
		   Set(ByVal value As Object)
			   If TypeOf value Is PeriodsSet Then
				   MyBase.EditValue = value
				   Return
			   End If
			   Dim s = TryCast(value, String)
			   If s IsNot Nothing Then
				   MyBase.EditValue = PeriodsSet.Parse(s)
			   End If
		   End Set
	   End Property

	   Public Overrides ReadOnly Property EditorTypeName() As String
		   Get
			   Return RepositoryItemDatePeriodEdit.DatePeriodEditName
		   End Get
	   End Property

	   <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
	   Public Shadows Property Properties() As RepositoryItemDatePeriodEdit
		   Get
			   Return TryCast(MyBase.Properties, RepositoryItemDatePeriodEdit)
		   End Get
		   Set(ByVal value As RepositoryItemDatePeriodEdit)
		   End Set
	   End Property

	   Protected Overrides Sub AcceptPopupValue(ByVal val As Object)
		   If PopupForm IsNot Nothing Then
'INSTANT VB NOTE: The variable calendar was renamed since Visual Basic does not handle local variables named the same as class members well:
			   Dim calendar_Renamed = CType(PopupForm, PopupDatePeriodEditForm).Calendar
			   Dim periods = New PeriodsSet()
			   For Each range In calendar_Renamed.SelectedRanges
					   periods.Periods.Add(New Period(range.StartDate, range.EndDate.AddSeconds(-1)))
			   Next range
			   If val Is Nothing AndAlso calendar_Renamed.SelectedRanges.Count = 0 Then
					periods.Periods.Add(New Period(Date.MinValue))
			   End If

			   EditValue = periods
			   calendar_Renamed.SelectedRanges.Clear()
		   End If
	   End Sub

	   Protected Overrides Function CreatePopupForm() As PopupBaseForm
		   Return New PopupDatePeriodEditForm(Me)
	   End Function
	   Protected Overrides Function ExtractParsedValue(ByVal e As ConvertEditValueEventArgs) As Object
		   Return e.Value
	   End Function
	End Class

End Namespace
