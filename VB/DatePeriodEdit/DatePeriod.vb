Imports System
Imports System.Collections
Imports System.Globalization

Namespace DatePeriodEdit_NS
	Public Class Period
		Implements IComparable

'INSTANT VB NOTE: The variable begin was renamed since Visual Basic does not allow variables and other class members to have the same name:
'INSTANT VB NOTE: The variable end was renamed since Visual Basic does not allow variables and other class members to have the same name:
		Private begin_Renamed, end_Renamed As Date
		Public Sub New(ByVal begin As Date, ByVal [end] As Date)
			Me.begin_Renamed = begin.Date
			Me.end_Renamed = EndOfDay([end])
		End Sub
		Public Sub New(ByVal [date] As Date)
			Me.New([date], [date])
		End Sub
		Public Shared Function EndOfDay(ByVal [date] As Date) As Date
			Return [date].Date.AddDays(1).AddSeconds(-1)
		End Function
		Public Shared Function BeginOfDay(ByVal [date] As Date) As Date
			Return [date].Date
		End Function
		Public Property Begin() As Date
			Get
				Return begin_Renamed
			End Get
			Set(ByVal value As Date)
				If Begin <> value Then
					begin_Renamed = value.Date
				End If
			End Set
		End Property
		Public Property [End]() As Date
			Get
				Return end_Renamed
			End Get
			Set(ByVal value As Date)
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
				Dim dates(1) As Date
				Dim i As Integer = 0
				For Each dateStr As String In sides
					If i > 1 Then
						Continue For
					End If
					Dim stringDate As String = dateStr.Trim()
					success = success AndAlso Date.TryParse(stringDate, format, DateTimeStyles.None, dates(i))
					i += 1
				Next dateStr
				If success Then
					If dates(0) <= dates(1) Then
						Return New Period(dates(0), dates(1))
					End If
				End If
			Else
				Dim dt As Date
				If Date.TryParse(str, format, DateTimeStyles.None, dt) Then
					Return New Period(dt)
				End If
			End If
			Return Nothing
		End Function
	End Class
	Public Class PeriodsSet
		Implements IConvertible

'INSTANT VB NOTE: The variable periods was renamed since Visual Basic does not allow variables and other class members to have the same name:
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
		Public Sub IntersectWith(ByVal begin As Date, ByVal [end] As Date)
			If begin.Date > [end].Date Then
				Return
			End If
			begin = Period.BeginOfDay(begin)
			[end] = Period.EndOfDay([end])
			For i As Integer = 0 To Periods.Count - 1
				If begin <= Me(i).Begin AndAlso [end] >= Me(i).End Then
					Dim oldBegin As Date = Me(i).Begin, oldEnd As Date = Me(i).End
					periods_Renamed.RemoveAt(i)
					IntersectWith(begin, oldBegin.AddSeconds(-1))
					IntersectWith(oldEnd.AddSeconds(1), [end])
					Return
				End If
			Next i
			For Each dp As Period In periods_Renamed
				If begin > dp.Begin AndAlso [end] < dp.End Then
					Dim periodEnd As Date = dp.End
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
					Dim oldEnd As Date = Me(i_2).End
					Me(i_2).End = begin.AddSeconds(-1)
					begin = oldEnd.AddSeconds(1)
				End If
				If [end] >= Me(i_2).Begin AndAlso [end] <= Me(i_2).End Then
					Dim oldBegin As Date = Me(i_2).Begin
					Me(i_2).Begin = [end].AddSeconds(1)
					[end] = oldBegin.AddSeconds(-1)
				End If
			Next i_2
			Add(New Period(begin, [end]))
		End Sub
		Public Sub MergeWith(ByVal begin As Date, ByVal [end] As Date)
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
		Public Overridable Function ContainPeriod(ByVal begin As Date, ByVal [end] As Date) As Boolean
			For i As Integer = 0 To Periods.Count - 1
				If begin >= Me(i).Begin AndAlso [end] <= Me(i).End Then
					Return True
				End If
			Next i
			Return False
		End Function
		Public Overridable Function ContainPartOfPeriod(ByVal begin As Date, ByVal [end] As Date) As Boolean
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
'INSTANT VB NOTE: The variable period was renamed since it may cause conflicts with calls to static members of the user-defined type with this name:
			For Each period_Renamed As Period In periods_Renamed
				result.Add(period_Renamed)
			Next period_Renamed
			Return result
		End Function
		Public Overridable Overloads Function ToString(ByVal format As IFormatProvider, ByVal separator As Char) As String
			Dim str As String = String.Empty
			For Each dp As Period In periods_Renamed
				str = str & dp.ToString(format) & separator.ToString() & " "
			Next dp
			If str.Length > 2 Then
				str = str.Remove(str.Length - 2)
			End If
			Return str
		End Function
		Public Overridable Overloads Function ToString(ByVal formatString As String, ByVal separator As Char) As String
			Dim str As String = String.Empty
			For Each dp As Period In periods_Renamed
				str = str & dp.ToString(formatString) & separator.ToString() & " "
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

		#Region "IConvertible        "

		Public Function GetTypeCode() As TypeCode Implements IConvertible.GetTypeCode
			Throw New NotImplementedException()
		End Function

		Public Function ToBoolean(ByVal provider As IFormatProvider) As Boolean Implements IConvertible.ToBoolean
			Throw New NotImplementedException()
		End Function

		Public Function ToChar(ByVal provider As IFormatProvider) As Char Implements IConvertible.ToChar
			Throw New NotImplementedException()
		End Function

		Public Function ToSByte(ByVal provider As IFormatProvider) As SByte Implements IConvertible.ToSByte
			Throw New NotImplementedException()
		End Function

		Public Function ToByte(ByVal provider As IFormatProvider) As Byte Implements IConvertible.ToByte
			Throw New NotImplementedException()
		End Function

		Public Function ToInt16(ByVal provider As IFormatProvider) As Short Implements IConvertible.ToInt16
			Throw New NotImplementedException()
		End Function

		Public Function ToUInt16(ByVal provider As IFormatProvider) As UShort Implements IConvertible.ToUInt16
			Throw New NotImplementedException()
		End Function

		Public Function ToInt32(ByVal provider As IFormatProvider) As Integer Implements IConvertible.ToInt32
			Throw New NotImplementedException()
		End Function

		Public Function ToUInt32(ByVal provider As IFormatProvider) As UInteger Implements IConvertible.ToUInt32
			Throw New NotImplementedException()
		End Function

		Public Function ToInt64(ByVal provider As IFormatProvider) As Long Implements IConvertible.ToInt64
			Throw New NotImplementedException()
		End Function

		Public Function ToUInt64(ByVal provider As IFormatProvider) As ULong Implements IConvertible.ToUInt64
			Throw New NotImplementedException()
		End Function

		Public Function ToSingle(ByVal provider As IFormatProvider) As Single Implements IConvertible.ToSingle
			Throw New NotImplementedException()
		End Function

		Public Function ToDouble(ByVal provider As IFormatProvider) As Double Implements IConvertible.ToDouble
			Throw New NotImplementedException()
		End Function

		Public Function ToDecimal(ByVal provider As IFormatProvider) As Decimal Implements IConvertible.ToDecimal
			Throw New NotImplementedException()
		End Function

		Public Function ToDateTime(ByVal provider As IFormatProvider) As Date Implements IConvertible.ToDateTime
			Throw New NotImplementedException()
		End Function

		Public Overloads Function ToString(ByVal provider As IFormatProvider) As String Implements IConvertible.ToString
			Return Me.ToString()
		End Function

		Public Function ToType(ByVal conversionType As Type, ByVal provider As IFormatProvider) As Object Implements IConvertible.ToType
			Throw New NotImplementedException()
		End Function
		#End Region
	End Class
End Namespace
