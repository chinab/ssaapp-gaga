Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Transaction
    Inherits System.Web.Services.WebService

    Dim lRetCode As Integer
    Dim lErrCode As Integer
    Dim sErrMsg As String
    Dim connect As New Connection()

    <WebMethod()> _
    Public Function CreateMarketingDocument(ByVal strXml As String) As String
        Try
            Dim sStr As String = ""
            If PublicVariable.Simulate Then
                sStr = "Operation Successfull!"
            Else
                Dim oSO As SAPbobsCOM.Documents
                If Connection.bConnect = False Then
                    connect.setDB()
                    If Not connect.connectDB() Then
                        Return -1
                    End If
                End If
                PublicVariable.oCompany.XMLAsString = True
                oSO = PublicVariable.oCompany.GetBusinessObjectFromXML(strXml, 0)
                lErrCode = oSO.Add()
                If lErrCode <> 0 Then
                    PublicVariable.oCompany.GetLastError(lErrCode, sErrMsg)
                    Return sErrMsg
                Else                    
                    Dim a As New SAP_Functions
                    Dim DocKey As String = a.GetLastKey(0, 13)                    
                    a.Create_IncommingPayment(DocKey)
                    Return sStr
                End If
            End If
            
            Return sStr
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function

End Class