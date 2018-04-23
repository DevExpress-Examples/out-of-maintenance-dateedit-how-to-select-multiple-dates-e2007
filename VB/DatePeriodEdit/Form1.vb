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
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			myUsers.Add(New User("Antuan", "Acapulco", 23))
			myUsers.Add(New User("Bill", "Brucel", 17))
			myUsers.Add(New User("Charli", "Chikago", 45))
			myUsers.Add(New User("Denn", "Denver", 20))
			myUsers.Add(New User("Eva", "Everton", 23))
			gridControl1.DataSource = myUsers
		End Sub
	End Class
	Public Class User
'INSTANT VB NOTE: The variable name was renamed since Visual Basic does not allow variables and other class members to have the same name:
'INSTANT VB NOTE: The variable city was renamed since Visual Basic does not allow variables and other class members to have the same name:
'INSTANT VB NOTE: The variable periodsString was renamed since Visual Basic does not allow variables and other class members to have the same name:
		Private name_Renamed, city_Renamed, periodsString_Renamed As String
'INSTANT VB NOTE: The variable age was renamed since Visual Basic does not allow variables and other class members to have the same name:
		Private age_Renamed As Integer
'INSTANT VB NOTE: The variable periodsSet was renamed since Visual Basic does not allow variables and other class members to have the same name:
		Private periodsSet_Renamed As PeriodsSet
		Public Sub New(ByVal name As String, ByVal city As String, ByVal age As Integer)
			Me.name_Renamed = name
			Me.city_Renamed = city
			Me.age_Renamed = age
			periodsSet_Renamed = New PeriodsSet()
			periodsSet_Renamed.MergeWith(Date.Today, Date.Today)
		   ' periodsSet.MergeWith(DateTime.Today.AddDays(5), DateTime.Today.AddDays(8));
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