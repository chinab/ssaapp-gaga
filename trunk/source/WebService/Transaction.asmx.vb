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
    <WebMethod()> _
    Public Function GetMarketingDocument(DocType As String, DocEntry As Integer) As String
        'First: DocEntry=1
        'Last: Set Docentry=-1

        Try
            Dim sStr As String = ""
            If PublicVariable.Simulate Then
                sStr = "Operation Successfull!"
            Else
                Dim oDocment As SAPbobsCOM.Documents
                If Connection.bConnect = False Then
                    connect.setDB()
                    If Not connect.connectDB() Then
                        Return "Can't connect to SAP"
                    End If
                End If
                If DocEntry = -1 Then
                    Dim dt As DataSet
                    'hardcode opor
                    dt = connect.ObjectGetAll_Query_SAP("select MAX(DocEntry) DocEntry from OPOR")
                    If dt.Tables(0).Rows.Count > 0 Then
                        DocEntry = dt.Tables(0).Rows(0).Item("DocEntry")
                    End If
                End If
                PublicVariable.oCompany.XMLAsString = True
                oDocment = PublicVariable.oCompany.GetBusinessObject(DocType)
                If oDocment.GetByKey(DocEntry) Then
                    oDocment.SaveXML(sStr)
                    Return sStr
                Else
                    Return ""
                End If
            End If
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function
End Class