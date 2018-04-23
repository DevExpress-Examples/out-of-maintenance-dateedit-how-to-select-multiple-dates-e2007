Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraEditors.Registrator
Imports System.ComponentModel
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Calendar
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.Utils.Serializing
Imports System.Globalization
Imports DevExpress.Utils

Namespace DatePeriodEdit_NS
	<UserRepositoryItem("RegisterDatePeriodEdit")> _
	Public Class RepositoryItemDatePeriodEdit
		Inherits RepositoryItemDateEdit
		Private optionsSelections As OptionsSelection
		Private periodsStoreMode_Renamed As StoreMode
		Private separatorChar_Renamed As Char = ","c
		Shared Sub New()
			RegisterDatePeriodEdit()
		End Sub
		Public Sub New()
			optionsSelections = New OptionsSelection()
			TextEditStyle = TextEditStyles.DisableTextEditor
		End Sub
		Public Const DatePeriodEditName As String = "DatePeriodEdit"
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return DatePeriodEditName
			End Get
		End Property
		Public Shared Sub RegisterDatePeriodEdit()
			EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(DatePeriodEditName, GetType(DatePeriodEdit), GetType(RepositoryItemDatePeriodEdit), GetType(DateEditViewInfo), New ButtonEditPainter(), True))
		End Sub
		<Description("Gets or sets how the editor store periods selected via the calendar ."), Category(CategoryName.Format), DefaultValue(StoreMode.Default)> _
		Public Overridable Property PeriodsStoreMode() As StoreMode
			Get
				Return periodsStoreMode_Renamed
			End Get
			Set(ByVal value As StoreMode)
				If PeriodsStoreMode = value Then
					Return
				End If
				Me.periodsStoreMode_Renamed = value
			End Set
		End Property
		<Description("Gets or sets the character separating periods"), Category(CategoryName.Format), DefaultValue(","c)> _
		Public Overridable Property SeparatorChar() As Char
			Get
				Return Me.separatorChar_Renamed
			End Get
			Set(ByVal value As Char)
				If SeparatorChar = value Then
					Return
				End If
				Me.separatorChar_Renamed = value
				OnPropertiesChanged()
			End Set
		End Property
		<Browsable(False)> _
		Public Overrides ReadOnly Property Mask() As DevExpress.XtraEditors.Mask.MaskProperties
			Get
				Return MyBase.Mask
			End Get
		End Property
		<Browsable(False)> _
		Public Overrides ReadOnly Property EditFormat() As FormatInfo
			Get
				Return MyBase.DisplayFormat
			End Get
		End Property
		<Browsable(False)> _
		Public Shadows ReadOnly Property VistaEditTime() As DefaultBoolean
			Get
				Return MyBase.VistaEditTime
			End Get
		End Property
		<Browsable(False)> _
		Public Shadows ReadOnly Property VistaDisplayMode() As DefaultBoolean
			Get
				Return MyBase.VistaDisplayMode
			End Get
		End Property
		<Browsable(False)> _
		Public Shadows ReadOnly Property EditMask() As String
			Get
				Return MyBase.EditMask
			End Get
		End Property
		<Description("Provides access to the settings used to selection."), Category(CategoryName.Properties), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property OptionsSelection() As OptionsSelection
			Get
				Return optionsSelections
			End Get
		End Property
		Public Overrides Sub Assign(ByVal item As RepositoryItem)
			MyBase.Assign(item)
			Dim source As RepositoryItemDatePeriodEdit = TryCast(item, RepositoryItemDatePeriodEdit)
			Me.optionsSelections = source.OptionsSelection
			Me.separatorChar_Renamed = source.SeparatorChar
			Me.periodsStoreMode_Renamed = source.PeriodsStoreMode
		End Sub
		Protected Overrides Function IsNullValue(ByVal editValue As Object) As Boolean
			If TypeOf editValue Is PeriodsSet Then
				Return (CType(editValue, PeriodsSet)).Periods.Count = 0
			End If
			If TypeOf editValue Is String Then
				Dim [set] As PeriodsSet = PeriodsSet.Parse(CStr(editValue))
				If [set] IsNot Nothing Then
					Return [set].Periods.Count = 0
				End If
			End If
			Return False
		End Function
		Public Overrides Overloads Function GetDisplayText(ByVal format As FormatInfo, ByVal editValue As Object) As String
			Dim displayText As String = String.Empty
			Dim [set] As PeriodsSet = TryCast(editValue, PeriodsSet)
			If [set] IsNot Nothing Then
				displayText = [set].ToString(format.FormatString, SeparatorChar)
			Else
				If TypeOf editValue Is String Then
					displayText = PeriodsSet.Parse(CStr(editValue)).ToString(format.FormatString, SeparatorChar)
				End If
			End If
			Dim e As New CustomDisplayTextEventArgs(editValue, displayText)
			If format IsNot EditFormat Then
				RaiseCustomDisplayText(e)
			End If
			Return e.DisplayText
		End Function

	End Class
	Public Class DatePeriodEdit
		Inherits DateEdit
		Shared Sub New()
			RepositoryItemDatePeriodEdit.RegisterDatePeriodEdit()
		End Sub
		Public Sub New()
			MyBase.New()
			EditValue = New PeriodsSet()
		End Sub
		Public Overrides Property EditValue() As Object
			Get
				Dim value As PeriodsSet = TryCast(MyBase.EditValue, PeriodsSet)
				If Properties.PeriodsStoreMode = StoreMode.String Then
					If value IsNot Nothing Then
						Return value.ToString()
					Else
						Return String.Empty
					End If
				Else
					If value IsNot Nothing Then
						Return value
					Else
						Return New PeriodsSet()
					End If
				End If
			End Get
			Set(ByVal value As Object)
				If TypeOf value Is PeriodsSet Then
					MyBase.EditValue = value
					Return
				End If
				If TypeOf value Is String Then
					MyBase.EditValue = PeriodsSet.Parse(CStr(value))
					Return
				End If
				MyBase.EditValue = value
			End Set
		End Property
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemDatePeriodEdit.DatePeriodEditName
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Shadows ReadOnly Property Properties() As RepositoryItemDatePeriodEdit
			Get
				Return TryCast(MyBase.Properties, RepositoryItemDatePeriodEdit)
			End Get
		End Property
		Protected Overrides Function CreatePopupForm() As PopupBaseForm
			Return New VistaPopupDatePeriodEditForm(Me)
		End Function
		Protected Overrides Function ExtractParsedValue(ByVal e As ConvertEditValueEventArgs) As Object
			Return e.Value
		End Function
	End Class
	Public Class VistaPopupDatePeriodEditForm
		Inherits VistaPopupDateEditForm
		Public Sub New(ByVal ownerEdit As DateEdit)
			MyBase.New(ownerEdit)
		End Sub
		Protected Overrides Function CreateCalendar() As DateEditCalendar
			Dim c As VistaDateEditCalendar = New VistaDatePeriodEditCalendar(OwnerEdit.Properties, OwnerEdit.EditValue)
			AddHandler c.OkClick, AddressOf OnOkClick
			Return c
		End Function
		Public Overrides ReadOnly Property ResultValue() As Object
			Get
				Return Calendar.TotalPeriods.GetCopy()
			End Get
		End Property
		Public Shadows Overridable ReadOnly Property Calendar() As VistaDatePeriodEditCalendar
			Get
				Return TryCast(MyBase.Calendar, VistaDatePeriodEditCalendar)
			End Get
		End Property
	End Class
	Public Class VistaDatePeriodEditCalendar
		Inherits VistaDateEditCalendar
		Private totalPeriods_Renamed, totalPeriodsCopy As PeriodsSet
		Private allowMark As Boolean
		Private viewLevel_Renamed As ViewLevel
		Public Sub New(ByVal item As RepositoryItemDateEdit, ByVal editDate As Object)
			MyBase.New(item, editDate)
			Selection.Clear()
			Multiselect = True
			Dim editValue As PeriodsSet = TryCast(Properties.OwnerEdit.EditValue, PeriodsSet)
			totalPeriods_Renamed = New PeriodsSet()
			viewLevel_Renamed = GetNewLevel(Properties.OptionsSelection.DefaultLevel, Properties.OptionsSelection.DefaultLevel)
			CreatePrevImage(False)
		End Sub
		Public Overrides Sub ResetState(ByVal editDate As Object, ByVal dt As DateTime)
			UpdateTotalPeriods(editDate)
			MyBase.ResetState(editDate, dt)
			If TotalPeriods.Periods.Count = 0 Then
				DateTime = DateTime.Now
			Else
				DateTime = TotalPeriods(0).Begin
			End If
			ViewLevel = GetNewLevel(ViewLevel, ViewLevel)
		End Sub
		Public Overridable Shadows Property DateTime() As DateTime
			Get
				Return MyBase.DateTime.Date
			End Get
			Set(ByVal value As DateTime)
				MyBase.DateTime = value.Date
			End Set
		End Property
		Protected Overridable Sub UpdateTotalPeriods(ByVal value As Object)
			Dim editValue As PeriodsSet = TryCast(value, PeriodsSet)
			TotalPeriods.Periods.Clear()
			If editValue IsNot Nothing Then
				TotalPeriods = editValue.GetCopy()
			Else
				If TypeOf value Is String Then
					TotalPeriods = PeriodsSet.Parse(CStr(value))
				End If
			End If
		End Sub
		Protected Overridable ReadOnly Property CtrlKeyPressed() As Boolean
			Get
				Return (System.Windows.Forms.Control.ModifierKeys And Keys.Control) <> 0
			End Get
		End Property
		Protected Overrides Sub OnDateTimeCommit(ByVal value As Object, ByVal cleared As Boolean)
		End Sub
		Protected Friend Overridable Shadows ReadOnly Property Properties() As RepositoryItemDatePeriodEdit
			Get
				Return TryCast(MyBase.Properties, RepositoryItemDatePeriodEdit)
			End Get
		End Property
		Protected Friend Overridable Function GetSwitchState() As Boolean
			Return SwitchState
		End Function
		Protected Overrides Function CreateInfoArgs() As DateEditInfoArgs
			Return New VistaDatePeriodEditInfoArgs(Me)
		End Function
		Protected Overrides Function CreatePainter() As DateEditPainter
			Return New VistaDatePeriodEditPainter(Me)
		End Function
		Protected Overrides Function CreateSelectionState() As DateEditCalendarStateBase
			Return New VistaDatePeriodEditCalendarSelectState(Me)
		End Function
		Public Overridable Property TotalPeriods() As PeriodsSet
			Get
				Return totalPeriods_Renamed
			End Get
			Set(ByVal value As PeriodsSet)
				totalPeriods_Renamed = value
			End Set
		End Property
		Protected Friend Overridable Function GetDayCells() As DayNumberCellInfoCollection
			Return Calendars(0).DayCells
		End Function
		Protected Overrides Sub OnMoveVertical(ByVal dir As Integer)
		End Sub
		Protected Overrides Sub OnMoveHorizontal(ByVal dir As Integer)
		End Sub
		Protected Overrides Sub SetViewCore(ByVal v As DateEditCalendarViewType)
		End Sub
		Public Overrides Property View() As DateEditCalendarViewType
			Get
				Return ConvertViewLevelToView(ViewLevel)
			End Get
			Set(ByVal value As DateEditCalendarViewType)
			End Set
		End Property
		Protected Overrides Sub SetSelection(ByVal [date] As DateTime)
		End Sub
		Protected Overrides Overloads Sub SetSelectionRange(ByVal [date] As DateTime)
		End Sub
		Public Overridable Property ViewLevel() As ViewLevel
			Get
				Return viewLevel_Renamed
			End Get
			Set(ByVal value As ViewLevel)
				Dim newValue As ViewLevel = GetNewLevel(value, ViewLevel)
				Dim oldValue As ViewLevel = ViewLevel
				If oldValue = newValue Then
					Return
				End If
				Dim oldView, newView As DateEditCalendarViewType
				If oldValue = ViewLevel.Days AndAlso newValue = ViewLevel.Weeks Then
					oldView = DateEditCalendarViewType.MonthInfo
					newView = DateEditCalendarViewType.YearInfo
				Else
					oldView = ConvertViewLevelToView(oldValue)
					newView = ConvertViewLevelToView(newValue)
				End If
				OnViewChanging(oldView, newView)
				viewLevel_Renamed = newValue
				OnViewChanged(oldView, newView)
			End Set
		End Property
		Protected Overridable Function GetNewLevel(ByVal newLevel As ViewLevel, ByVal currentLevel As ViewLevel) As ViewLevel
			Dim lowLevel As ViewLevel = Properties.OptionsSelection.LowLevel
			Dim highLevel As ViewLevel = Properties.OptionsSelection.HightLevel
			If (Not Properties.OptionsSelection.ShowWeekLevel) Then
				If lowLevel = ViewLevel.Weeks Then
					lowLevel = ViewLevel.Months
				End If
				If highLevel = ViewLevel.Weeks Then
					highLevel = ViewLevel.Days
				End If
			End If
			If lowLevel > highLevel Then
				Return currentLevel
			End If
			If newLevel < lowLevel Then
				Return lowLevel
			End If
			If newLevel > highLevel Then
				Return highLevel
			End If
			Return newLevel
		End Function
		Public Overridable Sub ViewLevelUp()
			If ViewLevel = ViewLevel.Days Then
				If Properties.OptionsSelection.ShowWeekLevel Then
					ViewLevel = ViewLevel.Weeks
				Else
					ViewLevel = ViewLevel.Months
				End If
			ElseIf ViewLevel = ViewLevel.Weeks Then
				ViewLevel = ViewLevel.Months
			Else
				ViewLevel = ViewLevel.Years
			End If
		End Sub
		Public Overridable Sub ViewLevelDown()
			If ViewLevel = ViewLevel.Years Then
				ViewLevel = ViewLevel.Months
			ElseIf ViewLevel = ViewLevel.Months Then
				If Properties.OptionsSelection.ShowWeekLevel Then
					ViewLevel = ViewLevel.Weeks
				Else
					ViewLevel = ViewLevel.Days
				End If
			Else
				ViewLevel = ViewLevel.Days
			End If
		End Sub

		Protected Overridable Function ConvertViewLevelToView(ByVal level As ViewLevel) As DateEditCalendarViewType
			If level = ViewLevel.Days Then
				Return DateEditCalendarViewType.MonthInfo
			End If
			If level = ViewLevel.Weeks Then
				Return DateEditCalendarViewType.MonthInfo
			End If
			If level = ViewLevel.Months Then
				Return DateEditCalendarViewType.YearInfo
			End If
			If level = ViewLevel.Years Then
				Return DateEditCalendarViewType.YearsInfo
			End If
			Return DateEditCalendarViewType.YearsInfo
		End Function
		Protected Overridable Sub MarkSelectedDay()
			If Selection.Count = 0 Then
				Return
			End If
			MarkPeriod(Selection(0), Selection(1))
		End Sub
		Protected Overridable Sub MarkPeriod(ByVal begin As DateTime, ByVal [end] As DateTime)
			If Properties.OptionsSelection.MultiselectBehaviour = MultiselectBehaviour.Merging Then
				TotalPeriods.MergeWith(begin, [end])
			ElseIf Properties.OptionsSelection.MultiselectBehaviour = MultiselectBehaviour.Intersection Then
				TotalPeriods.IntersectWith(begin, [end])
			ElseIf Properties.OptionsSelection.MultiselectBehaviour = MultiselectBehaviour.Disabled Then
				If (Not TotalPeriods.ContainPeriod(begin, [end])) Then
					TotalPeriods.Periods.Clear()
				End If
				TotalPeriods.IntersectWith(begin, [end])
			End If
			UpdateSelection()
			Selection.Clear()
		End Sub
		Protected Friend Overridable Sub UpdateSelection()
			UpdateExistingCellsState()
			Invalidate()
		End Sub
		Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
			MyBase.OnMouseDown(e)
			Dim hit As CalendarHitInfo = GetHitInfo(e)
			totalPeriodsCopy = totalPeriods_Renamed.GetCopy()
			If (Not CtrlKeyPressed) Then
				If (hit.InfoType = CalendarHitInfoType.Item) OrElse hit.InfoType = CalendarHitInfoType.WeekNumber OrElse hit.InfoType = CalendarHitInfoType.Unknown Then
					TotalPeriods.Periods.Clear()
				End If
			End If
		End Sub
		Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
			allowMark = True
			MyBase.OnMouseUp(e)
			If (Not allowMark) Then
				Return
			End If
			MarkSelectedDay()
		End Sub
		Protected Overrides Sub OnItemClick(ByVal hitInfo As CalendarHitInfo)
			Dim cell As DayNumberCellInfo = TryCast(hitInfo.HitObject, DayNumberCellInfo)
			If cell IsNot Nothing Then
				ChangeDateOnItemClick(cell)
				If ViewLevel = Properties.OptionsSelection.LowLevel OrElse (CtrlKeyPressed AndAlso Properties.OptionsSelection.MultiselectBehaviour <> MultiselectBehaviour.Disabled) Then
					MarkItemOnClick(cell)
				Else
					TotalPeriods = totalPeriodsCopy.GetCopy()
					ViewLevelDown()
				End If
			End If
		End Sub
		Protected Overridable Sub MarkItemOnClick(ByVal cell As DayNumberCellInfo)
			Dim begin As DateTime = CalcPeriodBeginDateTime(cell.Date)
			If ViewLevel = ViewLevel.Days Then
				MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel))
			ElseIf ViewLevel = ViewLevel.Weeks Then
				MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel))
			ElseIf ViewLevel = ViewLevel.Months Then
				MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel))
			ElseIf ViewLevel = ViewLevel.Years Then
				MarkPeriod(begin, CalcPeriodEndDateTime(begin, ViewLevel))
			End If
		End Sub
		Protected Overridable Sub ChangeDateOnItemClick(ByVal cell As DayNumberCellInfo)

			If viewLevel_Renamed = ViewLevel.Weeks Then
				Return
			End If
			Dim maxDate As DateTime = DateTime
			If cell.Date.Month <> DateTime.Month Then
				maxDate = cell.Date
			Else
				maxDate = CalcPeriodEndDateTime(cell.Date, ViewLevel)
			End If
			If viewLevel_Renamed < ViewLevel.Months Then
				If DateTime.Month < maxDate.Month Then
					If DateTime.Year = maxDate.Year Then
						OnRightArrowClick()
					Else
						OnLeftArrowClick()
					End If
				ElseIf DateTime.Month > maxDate.Month Then
					If DateTime.Year = maxDate.Year Then
						OnLeftArrowClick()
					Else
						OnRightArrowClick()
					End If
				End If
				Return
			End If
			If ViewLevel = ViewLevel.Days Then
				DateTime = New DateTime(cell.Date.Year, cell.Date.Month, CorrectDay(DateTime.Year, cell.Date.Month, cell.Date.Day), 0, 0, 0)
			ElseIf ViewLevel = ViewLevel.Weeks Then
				DateTime = New DateTime(cell.Date.Year, cell.Date.Month, CorrectDay(DateTime.Year, cell.Date.Month, DateTime.Day), 0, 0, 0)
			ElseIf ViewLevel = ViewLevel.Months Then
				DateTime = New DateTime(DateTime.Year, cell.Date.Month, CorrectDay(DateTime.Year, cell.Date.Month, DateTime.Day), 0, 0, 0)
			ElseIf ViewLevel = ViewLevel.Years Then
				DateTime = New DateTime(cell.Date.Year, DateTime.Month, CorrectDay(cell.Date.Year, DateTime.Month, DateTime.Day), 0, 0, 0)
			End If
		End Sub
		Protected Overrides Sub ProcessClick(ByVal hit As CalendarHitInfo)
			allowMark = False
			MyBase.ProcessClick(hit)
			If hit.InfoType = CalendarHitInfoType.WeekNumber Then
				onWeekNuberClick(hit)
			End If
		End Sub
		Protected Overrides Sub IncView()
			ViewLevelUp()
		End Sub
		Protected Overrides Sub DecView()
			ViewLevelDown()
		End Sub
		Protected Overridable Sub onWeekNuberClick(ByVal hit As CalendarHitInfo)
			Dim week As DayNumberCellInfo = TryCast(hit.HitObject, DayNumberCellInfo)
			If week IsNot Nothing AndAlso Properties.OptionsSelection.MultiselectBehaviour <> MultiselectBehaviour.Disabled Then
				MarkPeriod(week.Date, week.Date.AddDays(7).AddSeconds(-1))
			End If
		End Sub
		Protected Overrides Sub OnClearClick()
			TotalPeriods.Periods.Clear()
			UpdateExistingCellsState()
			Invalidate()
		End Sub
		Protected Overrides Sub OnCancelClick()
			Properties.OwnerEdit.CancelPopup()
		End Sub
		Public Overridable Function CalcPeriodBeginDateTime(ByVal begin As DateTime) As DateTime
			Return begin.Date
		End Function
		Public Overridable Function CalcPeriodEndDateTime(ByVal begin As DateTime, ByVal level As ViewLevel) As DateTime
			Dim [end] As DateTime = begin
			If level = ViewLevel.Days Then
				[end] = [end].AddDays(1)
				[end] = [end].AddSeconds(-1)
			ElseIf level = ViewLevel.Weeks Then
				[end] = [end].AddDays(7)
				[end] = [end].AddSeconds(-1)
			ElseIf level = ViewLevel.Months Then
				[end] = [end].AddMonths(1)
				[end] = [end].AddSeconds(-1)
			ElseIf level = ViewLevel.Years Then
				[end] = [end].AddYears(1)
				[end] = [end].AddSeconds(-1)
			End If
			Return [end]
		End Function
		Protected Overrides Function GetStartSelectionByState(ByVal [date] As DateTime) As DateTime
			If ViewLevel = ViewLevel.Weeks Then
				Return GetFirstDayOfTheWeek([date])
			End If
			Return MyBase.GetStartSelectionByState([date])
		End Function
		Protected Overrides Function GetEndSelectionByState(ByVal dt As DateTime) As DateTime
			If ViewLevel = ViewLevel.Weeks Then
				Return GetLastDayOfTheWeek(dt)
			End If
			Return MyBase.GetEndSelectionByState(dt)
		End Function
		Protected Overridable Function GetFirstDayOfTheWeek(ByVal [date] As DateTime) As DateTime
			Dim dt As New DateTime([date].Year, [date].Month, [date].Day, 0, 0, 0)
			Do While dt.DayOfWeek <> FirstDayOfWeek

				dt = dt.AddDays(-1)
			Loop
			Return dt
		End Function
		Protected Overridable Function GetLastDayOfTheWeek(ByVal [date] As DateTime) As DateTime
			Dim dt As DateTime = GetFirstDayOfTheWeek([date])
			dt = dt.AddDays(7).AddSeconds(-1)
			Return dt
		End Function

	End Class
	Public Class VistaDatePeriodEditInfoArgs
		Inherits VistaDateEditInfoArgs
		Public Sub New(ByVal calendar As DateEditCalendarBase)
			MyBase.New(calendar)
		End Sub

		Protected Overrides Function IsMultiselectDateSelected(ByVal cell As DayNumberCellInfo) As Boolean
			Dim selected As Boolean = MyBase.IsMultiselectDateSelected(cell)
			Dim patchCell As CustomDayNumberCellInfo = TryCast(cell, CustomDayNumberCellInfo)
			If patchCell IsNot Nothing Then
				Dim [end] As DateTime = Calendar.CalcPeriodEndDateTime(cell.Date, Calendar.ViewLevel)
				patchCell.Marked = Calendar.TotalPeriods.ContainPeriod(cell.Date, [end])
				patchCell.ContainMark = Calendar.TotalPeriods.ContainPartOfPeriod(cell.Date, [end])

				If selected Then
					If Calendar.Properties.OptionsSelection.MultiselectBehaviour = MultiselectBehaviour.Merging OrElse (Calendar.Properties.OptionsSelection.MultiselectBehaviour = MultiselectBehaviour.Intersection AndAlso patchCell.Marked = True) Then
						patchCell.ContainMark = False
					End If
					If patchCell.Marked AndAlso Calendar.Properties.OptionsSelection.MultiselectBehaviour = MultiselectBehaviour.Intersection Then
						 selected = False
					End If
					patchCell.Marked = False
				End If
			End If
			Return selected
		End Function
		Protected Overrides Function IsDateActive(ByVal cell As DayNumberCellInfo) As Boolean
			If Calendar.ViewLevel = ViewLevel.Weeks Then
				Return True
			End If
			Return MyBase.IsDateActive(cell)
		End Function
		Protected Overrides Overloads Sub CalcItemsInfo()
			If Calendar.ViewLevel = ViewLevel.Weeks Then
				CalcWeekItemsInfo()
			Else
				MyBase.CalcItemsInfo()
			End If
		End Sub
		Protected Overridable Sub CalcWeekItemsInfo()
			DayCells.Clear()
			WeekCells.Clear()
			Dim rect As New Rectangle(New Point(DateClientRect.X + DistanceFromLeftToCell, DateClientRect.Y), New Size((DateClientRect.Width - 4) / 2, DateClientRect.Height \ 3))
			Dim currInfo As DayNumberCellInfo
			For row As Integer = 0 To 2
				For col As Integer = 0 To 1
					currInfo = CreateWeekCellInfo(row, col)
					If currInfo IsNot Nothing Then
						currInfo.SetAppearance(Appearance)
						currInfo.TextBounds = CalcCellTextRect(currInfo.Text, rect)
						currInfo.Bounds = rect
						DayCells.Add(currInfo)
					End If
					rect.Offset(rect.Width, 0)
				Next col
				rect.X = DateClientRect.X + DistanceFromLeftToCell
				rect.Offset(0, rect.Height)
			Next row
			UpdateExistingCellsState()
		End Sub
		Protected Overridable Function CreateWeekCellInfo(ByVal row As Integer, ByVal col As Integer) As DayNumberCellInfo
			Dim currInfo As DayNumberCellInfo
			Dim dt As DateTime = FirstVisibleDate.AddDays(14 * row + 7 * col)
			currInfo = New CustomDayNumberCellInfo(dt)
			Dim endDay As DateTime = currInfo.Date.AddDays(6)
			Dim dateSeparator As String = " "
			currInfo.Text = Calendar.DateFormat.GetAbbreviatedMonthName(currInfo.Date.Month) & dateSeparator & currInfo.Date.Day & " - " & Calendar.DateFormat.GetAbbreviatedMonthName(endDay.Month) & dateSeparator & endDay.Day
			Return currInfo
		End Function
		Public Shadows Overridable ReadOnly Property Calendar() As VistaDatePeriodEditCalendar
			Get
				Return TryCast(MyBase.Calendar, VistaDatePeriodEditCalendar)
			End Get
		End Property
		Protected Overrides Function CreateDayCell(ByVal [date] As DateTime) As DayNumberCellInfo
			Return New CustomDayNumberCellInfo([date])
		End Function
		Protected Overrides Function CreateMonthCellInfo(ByVal row As Integer, ByVal col As Integer) As DayNumberCellInfo
			Dim oldInfo As DayNumberCellInfo
			oldInfo = MyBase.CreateMonthCellInfo(row, col)
			If oldInfo Is Nothing Then
				Return oldInfo
			End If
			Dim patchedInfo As New CustomDayNumberCellInfo(oldInfo.Date)
			patchedInfo.Text = oldInfo.Text
			Return patchedInfo
		End Function
		Protected Overrides Function CreateYearCellInfo(ByVal row As Integer, ByVal col As Integer) As DayNumberCellInfo
			Dim oldInfo As DayNumberCellInfo
			oldInfo = MyBase.CreateYearCellInfo(row, col)
			If oldInfo Is Nothing Then
				Return oldInfo
			End If
			Dim patchedInfo As New CustomDayNumberCellInfo(oldInfo.Date)
			patchedInfo.Text = oldInfo.Text
			Return patchedInfo
		End Function
		Public Overrides Function GetHitInfo(ByVal e As MouseEventArgs) As CalendarHitInfo
			Dim baseHitInfo As CalendarHitInfo = MyBase.GetHitInfo(e)
			If baseHitInfo.InfoType <> CalendarHitInfoType.Unknown Then
				Return baseHitInfo
			End If

			If OkButtonRect.Contains(e.Location) Then
				baseHitInfo.InfoType = CalendarHitInfoType.Ok
				baseHitInfo.Bounds = OkButtonRect
			ElseIf CancelButtonRect.Contains(e.Location) Then
				baseHitInfo.InfoType = CalendarHitInfoType.Cancel
				baseHitInfo.Bounds = CancelButtonRect
			End If
			If ShowWeekNumbers Then
				For i As Integer = 0 To WeekCells.Count - 1
					If WeekCells(i).Bounds.Contains(e.Location) Then
						Dim [date] As New DateTime(DayCells(0).Date.Year, DayCells(0).Date.Month, DayCells(0).Date.Day, 0, 0, 0)
						[date] = [date].AddDays(7 * i)
						Dim cell As New DayNumberCellInfo([date])
						baseHitInfo.InfoType = CalendarHitInfoType.WeekNumber
						baseHitInfo.HitObject = cell
					End If
				Next i
			End If
			Return baseHitInfo
		End Function
		Protected Overrides Sub CalcHeaderInfo()
			MyBase.CalcHeaderInfo()
			Dim indent As Integer = GetButtonRect(Rectangle.Empty).Width \ 2
			ClearRect = New Rectangle(LeftArrowInfo.Bounds.Right + indent, Content.Bottom + IndentFromDateInfoToClearText, ClearRect.Width, ClearRect.Height)
			OkRect = New Rectangle(LeftArrowInfo.Bounds.Right + (RightArrowInfo.Bounds.X - LeftArrowInfo.Bounds.Right - OkRect.Width) / 2, Content.Bottom + IndentFromDateInfoToClearText, OkRect.Width, OkRect.Height)
			CancelRect = New Rectangle(RightArrowInfo.Bounds.X - indent - CancelRect.Right, Content.Bottom + IndentFromDateInfoToClearText, CancelRect.Width, CancelRect.Height)
			OkButtonRect = GetButtonRect(OkRect)
			CancelButtonRect = GetButtonRect(CancelRect)
			ClearButtonRect = GetButtonRect(ClearRect)
		End Sub
	End Class
	Public Class VistaDatePeriodEditPainter
		Inherits VistaDateEditPainter
		Public Sub New(ByVal calendar As DateEditCalendarBase)
			MyBase.New(calendar)
		End Sub
		Protected Overrides Sub DrawDayCell(ByVal info As CalendarObjectInfoArgs, ByVal cell As DayNumberCellInfo)
			Dim isDrawn As Boolean = False
			Dim patchCell As CustomDayNumberCellInfo = TryCast(cell, CustomDayNumberCellInfo)
			If patchCell IsNot Nothing Then
				isDrawn = DrawPatchedCell(info, patchCell)
			End If
			If (Not isDrawn) Then
				MyBase.DrawDayCell(info, cell)
			End If
		End Sub
		Protected Overridable Function DrawPatchedCell(ByVal info As CalendarObjectInfoArgs, ByVal cell As CustomDayNumberCellInfo) As Boolean
			cell.Today = cell.Marked
			MyBase.DrawDayCell(info, cell)
			If (Not cell.Marked) Then
				If cell.ContainMark Then
					MarkCellContent(info, cell)
				End If
			End If
			Return True
		End Function
		Protected Overrides Sub DrawWeekdaysAbbreviation(ByVal info As CalendarObjectInfoArgs)
			If (CType(info.Calendar, VistaDatePeriodEditCalendar)).ViewLevel = ViewLevel.Weeks Then
				Return
			End If
			MyBase.DrawWeekdaysAbbreviation(info)
		End Sub
		Protected Overridable Sub MarkCellContent(ByVal info As CalendarObjectInfoArgs, ByVal cell As DayNumberCellInfo)
			Dim width As Integer = cell.Bounds.Width \ 3, height As Integer = cell.Bounds.Height \ 3
			Dim r As New Rectangle(cell.Bounds.Location, New Size(width, height))
			r.Offset(width * 2, height * 2)
			Dim icon As New DayNumberCellInfo(cell.Date)
			icon.Today = True
			icon.Bounds = r
			icon.Text = String.Empty
			icon.Selected = True
			MyBase.DrawDayCell(info, icon)
		End Sub
		Protected Overrides Sub DrawHeader(ByVal info As CalendarObjectInfoArgs)
			MyBase.DrawHeader(info)
			Dim vdi As VistaDateEditInfoArgs = TryCast(info, VistaDateEditInfoArgs)
			If vdi Is Nothing Then
				Return
			End If
			DrawOk(vdi)
			DrawCancel(vdi)
		End Sub
	End Class
	Public Class VistaDatePeriodEditCalendarSelectState
		Inherits VistaDateEditCalendarSelectState
		Public Sub New(ByVal control As DateEditCalendarBase)
			MyBase.New(control)
		End Sub
		Public Overridable ReadOnly Property DatePeriodCalendar() As VistaDatePeriodEditCalendar
			Get
				Return TryCast(VistaCalendar, VistaDatePeriodEditCalendar)
			End Get
		End Property
		Protected Overrides Overloads Sub UpdateSelection(ByVal e As MouseEventArgs)
			If DatePeriodCalendar.Properties.OptionsSelection.MultiselectBehaviour = MultiselectBehaviour.Disabled Then
				Return
			End If
			Dim oldSelectionCount As Integer = DatePeriodCalendar.Selection.Count
			MyBase.UpdateSelection(e)
			If oldSelectionCount <> DatePeriodCalendar.Selection.Count AndAlso DatePeriodCalendar.Selection.Count = 0 Then
				DatePeriodCalendar.UpdateSelection()
			End If
		End Sub
		Protected Overrides ReadOnly Property ShiftKeyPressed() As Boolean
			Get
				Return False
			End Get
		End Property
		Protected Overrides Sub FindMinMaxDateInRect(ByVal rect As Rectangle, ByRef minDate As DateTime, ByRef maxDate As DateTime, ByVal inverse As Boolean)
			Dim down, up As Point
			If inverse Then
				down = New Point(rect.Left, rect.Bottom)
				up = New Point(rect.Right, rect.Top)
			Else
				up = rect.Location
				down = New Point(rect.Right, rect.Bottom)
			End If
			Dim minCell, maxCell As DayNumberCellInfo
			minCell = GetCellByPoint(down, False)
			maxCell = GetCellByPoint(up, False)
			minDate = DateTime.MaxValue
			maxDate = DateTime.MinValue
			If minCell IsNot Nothing AndAlso maxCell IsNot Nothing Then
				If maxCell IsNot minCell Then
					If minCell.Date < maxCell.Date Then
						minDate = minCell.Date
						maxDate = maxCell.Date
					Else
						maxDate = minCell.Date
						minDate = maxCell.Date
					End If
				End If
			End If
		End Sub
		Protected Overrides Function GetCellByPoint(ByVal pt As Point, ByVal nearestLeft As Boolean) As DayNumberCellInfo
			For Each cell As DayNumberCellInfo In DatePeriodCalendar.GetDayCells()
				If cell.Bounds.Contains(pt) Then
					Return cell
				End If
			Next cell
			Return Nothing
		End Function
	End Class
	Public Class CustomDayNumberCellInfo
		Inherits DayNumberCellInfo
		Private marked_Renamed As Boolean
		Private containMark_Renamed As Boolean
		Public Sub New(ByVal [date] As DateTime)
			MyBase.New([date])
			marked_Renamed = False
			containMark_Renamed = False
		End Sub
		Public Property Marked() As Boolean
			Get
				Return marked_Renamed
			End Get
			Set(ByVal value As Boolean)
				marked_Renamed = value
			End Set
		End Property
		Public Property ContainMark() As Boolean
			Get
				Return containMark_Renamed
			End Get
			Set(ByVal value As Boolean)
				containMark_Renamed = value
			End Set
		End Property
	End Class
	<TypeConverter(GetType(ExpandableObjectConverter))> _
	Public Class OptionsSelection
		Private multiselectBehaviour_Renamed As MultiselectBehaviour
		Private lowLevel_Renamed, highLevel, defaultLevel_Renamed As ViewLevel
		Private showWeekLevel_Renamed As Boolean
		Public Sub New()
			multiselectBehaviour_Renamed = MultiselectBehaviour.Merging
			lowLevel_Renamed = ViewLevel.Days
			highLevel = ViewLevel.Years
			defaultLevel_Renamed = ViewLevel.Days
			showWeekLevel_Renamed = False
		End Sub
		<Description("Allow chose multiselection behaviour."), Category(CategoryName.Properties), DefaultValue(MultiselectBehaviour.Merging)> _
		Public Property MultiselectBehaviour() As MultiselectBehaviour
			Get
				Return multiselectBehaviour_Renamed
			End Get
			Set(ByVal value As MultiselectBehaviour)
				multiselectBehaviour_Renamed = value
			End Set
		End Property
		<Description("Allow chose weather week level will be shown."), Category(CategoryName.Properties), DefaultValue(False)> _
		Public Property ShowWeekLevel() As Boolean
			Get
				Return showWeekLevel_Renamed
			End Get
			Set(ByVal value As Boolean)
				showWeekLevel_Renamed = value
			End Set
		End Property
		<Description("Allow chose the lowest navigation level."), Category(CategoryName.Properties), DefaultValue(ViewLevel.Days)> _
		Public Property LowLevel() As ViewLevel
			Get
				Return lowLevel_Renamed
			End Get
			Set(ByVal value As ViewLevel)
				lowLevel_Renamed = value
			End Set
		End Property
		<Description("Allow chose the higest navigation level."), Category(CategoryName.Properties), DefaultValue(ViewLevel.Years)> _
		Public Property HightLevel() As ViewLevel
			Get
				Return highLevel
			End Get
			Set(ByVal value As ViewLevel)
				highLevel = value
			End Set
		End Property
		<Description("Allo chose the first shoun level."), Category(CategoryName.Properties), DefaultValue(ViewLevel.Days)> _
		Public Property DefaultLevel() As ViewLevel
			Get
				Return defaultLevel_Renamed
			End Get
			Set(ByVal value As ViewLevel)
				defaultLevel_Renamed = value
			End Set
		End Property
	End Class
	Public Enum MultiselectBehaviour
		Merging
		Intersection
		Disabled
	End Enum
	Public Enum ViewLevel
		Days
		Weeks
		Months
		Years
	End Enum
	Public Enum StoreMode
		[Default]
		PeriodsSet
		[String]
	End Enum
End Namespace
