Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Reports
    Inherits System.Web.Services.WebService
#Region "TimeSheet"
    <WebMethod()> _
    Public Function TimeSheet_OpenList(UserID As String, FromDate As Date, ToDate As Date) As DataSet
        Try
            Dim connect As New Connection()
            connect.setDB(UserID)
            Dim str As String
            str = "select * from OCLG where U_UserID ='" + UserID + "' and CntctDate between '" + FromDate.ToString + "' and '" + ToDate.ToString + "'"
            Return connect.ObjectGetAll_Query_SAP(str)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Function TimeSheet_All(UserID As String, FromDate As Date, ToDate As Date) As DataSet
        Try
            Dim connect As New Connection()
            connect.setDB(UserID)
            Dim str As String
            str = "select * from OCLG where CntctDate between '" + FromDate.ToString + "' and '" + ToDate.ToString + "'"
            Return connect.ObjectGetAll_Query_SAP(str)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Function TimeSheet_Missing(UserID As String, FromDate As Date, ToDate As Date, FilterByUser As Boolean) As DataSet
        Try
            Dim connect As New Connection()
            connect.setDB(UserID)
            Dim str As String = ""
            str = ";with months (date) AS (SELECT '" + FromDate.ToString + "' UNION ALL SELECT DATEADD(day,1,date) from months where DATEADD(day,1,date)<='" + ToDate.ToString + "')"
            If FilterByUser = True Then
                str = str + " select T0.date from months T0 left join OCLG T1 on DATEDIFF(dd,T0.date,T1.Recontact)=0 and T1.U_UserID=" + UserID + " where T1.Recontact is null"
            Else
                str = str + " select T0.date from months T0 left join OCLG T1 on DATEDIFF(dd,T0.date,T1.Recontact)=0 where T1.Recontact is null"
            End If

            Return connect.ObjectGetAll_Query_SAP(str)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
End Class