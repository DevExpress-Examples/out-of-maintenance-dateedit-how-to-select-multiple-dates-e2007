Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Collections
Imports DevExpress.XtraEditors.Controls
Imports System.Globalization
Imports DevExpress.Data.Utils

Namespace DatePeriodEdit_NS
	Public Class Period
		Implements IComparable
		Private begin_Renamed, end_Renamed As DateTime
		Public Sub New(ByVal begin As DateTime, ByVal [end] As DateTime)
			Me.begin_Renamed = begin.Date
			Me.end_Renamed = EndOfDay([end])
		End Sub
		Public Sub New(ByVal [date] As DateTime)
			Me.New([date], [date])
		End Sub
		Public Shared Function EndOfDay(ByVal [date] As DateTime) As DateTime
			Return [date].Date.AddDays(1).AddSeconds(-1)
		End Function
		Public Shared Function BeginOfDay(ByVal [date] As DateTime) As DateTime
			Return [date].Date
		End Function
		Public Property Begin() As DateTime
			Get
				Return begin_Renamed
			End Get
			Set(ByVal value As DateTime)
				If Begin <> value Then
					begin_Renamed = value.Date
				End If
			End Set
		End Property
		Public Property [End]() As DateTime
			Get
				Return end_Renamed
			End Get
			Set(ByVal value As DateTime)
				If [End] <> value Then
					end_Renamed = EndOfDay(value)
				End If
			End Set
		End Property
		Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
			Dim dp As Period = TryCast(obj, Period)
			If dp IsNot Nothing Then
				Return Me.Begin.CompareTo(dp.Begin)
			Else
				Throw New ArgumentException("Object is not a DatePeriod")
			End If
		End Function
		Public Overrides Function ToString() As String
			If Begin.Date = [End].Date Then
				Return Begin.ToString("d")
			End If
			Return Begin.ToString("d") & " - " & [End].ToString("d")
		End Function
		Public Overridable Overloads Function ToString(ByVal formatString As String) As String
			If formatString = String.Empty Then
				Return ToString()
			End If
			If Begin.Date = [End].Date Then
				Return Begin.ToString(formatString)
			End If
			Return Begin.ToString(formatString) & " - " & [End].ToString(formatString)
		End Function
		Public Overridable Overloads Function ToString(ByVal format As IFormatProvider) As String
			If format Is Nothing Then
				Return ToString()
			End If
			If Begin.Date = [End].Date Then
				Return Begin.ToString(format)
			End If
			Return Begin.ToString(format) & " - " & [End].ToString(format)
		End Function
		Public Shared Function Parse(ByVal str As String, ByVal format As IFormatProvider) As Period
			str = str.Trim()
			If str.Contains(" - ") Then
				Dim success As Boolean = True
				Dim periodSeparators(0) As String
				periodSeparators(0) = " - "
				Dim sides() As String = String.Format("{0}", str).Split(periodSeparators, StringSplitOptions.RemoveEmptyEntries)
				Dim dates(1) As DateTime
				Dim i As Integer = 0
				For Each dateStr As String In sides
					If i > 1 Then
						Continue For
					End If
					Dim stringDate As String = dateStr.Trim()
					success = success AndAlso DateTime.TryParse(stringDate, format, DateTimeStyles.None, dates(i))
					i += 1
				Next dateStr
				If success Then
					If dates(0) <= dates(1) Then
						Return New Period(dates(0), dates(1))
					End If
				End If
			Else
				Dim dt As DateTime
				If DateTime.TryParse(str, format, DateTimeStyles.None, dt) Then
					Return New Period(dt)
				End If
			End If
			Return Nothing
		End Function
	End Class
	Public Class PeriodsSet
		Private periods_Renamed As ArrayList
		Protected Shared ReadOnly Property DefaultSeparator() As Char
			Get
				Return ","c
			End Get
		End Property
		Protected Shared ReadOnly Property InvariantCulture() As CultureInfo
			Get
				Return New CultureInfo(String.Empty)
			End Get
		End Property
		Public Sub New()
			periods_Renamed = New ArrayList()
		End Sub
		Default Public Property Item(ByVal index As Integer) As Period
			Get
				Return TryCast(periods_Renamed(index), Period)
			End Get
			Set(ByVal value As Period)
				periods_Renamed(index) = value
			End Set
		End Property
		Protected Overridable Function Add(ByVal value As Period) As Integer
			For Each dp As Period In periods_Renamed
				If dp.Begin.Date = value.End.Date.AddDays(1) Then
					dp.Begin = value.Begin
					Return periods_Renamed.IndexOf(dp)
				End If
				If dp.End.Date = value.Begin.Date.AddDays(-1) Then
					dp.End = value.End
					Return periods_Renamed.IndexOf(dp)
				End If
			Next dp
			periods_Renamed.Add(value)
			periods_Renamed.Sort()
			Return periods_Renamed.IndexOf(value)
		End Function
		Public ReadOnly Property Periods() As ArrayList
			Get
				Return periods_Renamed
			End Get
		End Property
		Public Sub IntersectWith(ByVal begin As DateTime, ByVal [end] As DateTime)
			If begin.Date > [end].Date Then
				Return
			End If
			begin = Period.BeginOfDay(begin)
			[end] = Period.EndOfDay([end])
			Dim i As Integer = 0
			Do While i < Periods.Count
				If begin <= Me(i).Begin AndAlso [end] >= Me(i).End Then
					Dim oldBegin As DateTime = Me(i).Begin, oldEnd As DateTime = Me(i).End
					periods_Renamed.RemoveAt(i)
					IntersectWith(begin, oldBegin.AddSeconds(-1))
					IntersectWith(oldEnd.AddSeconds(1), [end])
					Return
				End If
				i += 1
			Loop
			For Each dp As Period In periods_Renamed
				If begin > dp.Begin AndAlso [end] < dp.End Then
					Dim periodEnd As DateTime = dp.End
					dp.End = begin.AddSeconds(-1)
					IntersectWith([end].AddSeconds(1), periodEnd)
					Return
				End If
			Next dp
			For i_1 As Integer = 0 To Periods.Count - 1
				If begin = Me(i_1).Begin Then
					Me(i_1).Begin = [end].AddSeconds(1)
					Return
				End If
				If [end] = Me(i_1).End Then
					Me(i_1).End = begin.AddSeconds(-1)
					Return
				End If
			Next i_1
			For i_2 As Integer = 0 To Periods.Count - 1
				If begin >= Me(i_2).Begin AndAlso begin <= Me(i_2).End Then
					Dim oldEnd As DateTime = Me(i_2).End
					Me(i_2).End = begin.AddSeconds(-1)
					begin = oldEnd.AddSeconds(1)
				End If
				If [end] >= Me(i_2).Begin AndAlso [end] <= Me(i_2).End Then
					Dim oldBegin As DateTime = Me(i_2).Begin
					Me(i_2).Begin = [end].AddSeconds(1)
					[end] = oldBegin.AddSeconds(-1)
				End If
			Next i_2
			Add(New Period(begin, [end]))
		End Sub
		Public Sub MergeWith(ByVal begin As DateTime, ByVal [end] As DateTime)
			If begin.Date > [end].Date Then
				Return
			End If
			begin = Period.BeginOfDay(begin)
			[end] = Period.EndOfDay([end])
			If ContainPeriod(begin, [end]) Then
				Return
			End If
			For i As Integer = 0 To Periods.Count - 1
				If begin <= Me(i).Begin AndAlso [end] >= Me(i).End Then
					periods_Renamed.RemoveAt(i)
					MergeWith(begin, [end])
					Return
				End If
			Next i
			Dim beginPeriod As Period = Nothing, endPeriod As Period = Nothing
			For i_1 As Integer = 0 To Periods.Count - 1
				If begin >= Me(i_1).Begin AndAlso begin <= Me(i_1).End Then
					beginPeriod = Me(i_1)
				End If
				If [end] >= Me(i_1).Begin AndAlso [end] <= Me(i_1).End Then
					endPeriod = Me(i_1)
				End If
			Next i_1
			If beginPeriod IsNot Nothing AndAlso endPeriod IsNot Nothing Then
				beginPeriod.End = endPeriod.End
				periods_Renamed.Remove(endPeriod)
				Return
			End If
			If beginPeriod IsNot Nothing Then
				beginPeriod.End = [end]
				Return
			End If
			If endPeriod IsNot Nothing Then
				endPeriod.Begin = begin
				Return
			End If
			Add(New Period(begin, [end]))
		End Sub
		Public Function ContainPeriod(ByVal item As Object) As Boolean
			Dim dp As Period = TryCast(item, Period)
			If dp IsNot Nothing Then
				Return ContainPeriod(dp.Begin, dp.End)
			End If
			Return False
		End Function
		Public Overridable Function ContainPeriod(ByVal begin As DateTime, ByVal [end] As DateTime) As Boolean
			For i As Integer = 0 To Periods.Count - 1
				If begin >= Me(i).Begin AndAlso [end] <= Me(i).End Then
					Return True
				End If
			Next i
			Return False
		End Function
		Public Overridable Function ContainPartOfPeriod(ByVal begin As DateTime, ByVal [end] As DateTime) As Boolean
			If ContainPeriod(begin, [end]) Then
				Return True
			End If
			For i As Integer = 0 To Periods.Count - 1
				If (begin <= Me(i).Begin AndAlso [end] >= Me(i).Begin) OrElse (begin <= Me(i).End AndAlso [end] >= Me(i).End) Then
					Return True
				End If
			Next i
			Return False
		End Function
		Public Overridable Function GetCopy() As PeriodsSet
			Dim result As New PeriodsSet()
			For Each period As Period In periods_Renamed
				result.Add(period)
			Next period
			Return result
		End Function
		Public Overridable Overloads Function ToString(ByVal format As IFormatProvider, ByVal separator As Char) As String
			Dim str As String = String.Empty
			For Each dp As Period In periods_Renamed
				str = str & dp.ToString(format) + separator.ToString() & " "
			Next dp
			If str.Length > 2 Then
				str = str.Remove(str.Length - 2)
			End If
			Return str
		End Function
		Public Overridable Overloads Function ToString(ByVal formatString As String, ByVal separator As Char) As String
			Dim str As String = String.Empty
			For Each dp As Period In periods_Renamed
				str = str & dp.ToString(formatString) + separator.ToString() & " "
			Next dp
			If str.Length > 2 Then
				str = str.Remove(str.Length - 2)
			End If
			Return str
		End Function
		Public Overrides Function ToString() As String
			Return ToString(InvariantCulture, DefaultSeparator)
		End Function
		Public Shared Function Parse(ByVal str As String) As PeriodsSet
			Return Parse(str, InvariantCulture, DefaultSeparator)
		End Function
		Public Shared Function Parse(ByVal str As String, ByVal format As IFormatProvider, ByVal separatorChar As Char) As PeriodsSet
			Dim result As New PeriodsSet()
			Dim periodSet() As String = String.Format("{0}", str).Split(separatorChar)
			For Each periodStr As String In periodSet
				Dim dp As Period = Period.Parse(periodStr, format)
				If dp IsNot Nothing Then
					result.Add(dp)
				End If
			Next periodStr
			Return result
		End Function
	End Class
End Namespace
