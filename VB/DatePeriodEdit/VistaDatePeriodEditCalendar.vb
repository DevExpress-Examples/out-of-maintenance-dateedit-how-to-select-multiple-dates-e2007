Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DatePeriodEdit_NS
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Calendar
Imports System.Windows.Forms

Namespace DatePeriodEdit
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
		Private isNeedDateChanged As Boolean = True
		Public Overrides Sub ResetState(ByVal editDate As Object, ByVal dt As DateTime)
			UpdateTotalPeriods(editDate)
			MyBase.ResetState(editDate, dt)
			isNeedDateChanged = False
			If TotalPeriods.Periods.Count = 0 Then
				DateTime = DateTime.Now
			Else
				DateTime = TotalPeriods(0).Begin
			End If
			isNeedDateChanged = True
			ViewLevel = GetNewLevel(ViewLevel, ViewLevel)
		End Sub

		Protected Overrides Sub OnDateTimeChanged(ByVal value As DateTime)
			If isNeedDateChanged Then
				MyBase.OnDateTimeChanged(value)
			End If
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
		Protected Overrides Sub SetSelectionRange(ByVal [date] As DateTime)
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
		'protected override DateEditCalendarViewType DecView() { ViewLevelDown(); }
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
End Namespace
