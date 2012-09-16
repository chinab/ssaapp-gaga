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
    Public Function CreateMarketingDocument(ByVal strXml As String) As DataSet
        Dim b As New SAP_Functions
        Try
            Dim sStr As String = "Operation Completed Successfully!"
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                Return a.Simulate_CreateTransaction()
            Else

                Dim oSO As SAPbobsCOM.Documents
                If Connection.bConnect = False Then
                    connect.setDB()
                    If Not connect.connectDB() Then
                        Return b.ReturnMessage(-1, "Connect SAP failed")
                    End If
                End If
                PublicVariable.oCompany.XMLAsString = True
                oSO = PublicVariable.oCompany.GetBusinessObjectFromXML(strXml, 0)
                lErrCode = oSO.Add()
                If lErrCode <> 0 Then
                    PublicVariable.oCompany.GetLastError(lErrCode, sErrMsg)
                    Return b.ReturnMessage(lErrCode, sErrMsg)
                Else
                    Return b.ReturnMessage(lErrCode, "Operation Sucessful!")
                End If
            End If

        Catch ex As Exception
            Return b.ReturnMessage(-1, ex.ToString)
        End Try
    End Function
    <WebMethod()> _
    Public Function GetMarketingDocument(DocType As String, DocEntry As Integer, UserID As String) As String
        'First: DocEntry=1
        'Last: Set Docentry=0
        'Next: current docentry+1, if current docentry is null, go last
        'Prev: current docentry-1, if current docentry is null, go first
        'DocType=22: Purchase Order
        Try
            Dim sStr As String = ""
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                Return a.Simulate_OPOR
            Else
                Dim oDocment As SAPbobsCOM.Documents
                If Connection.bConnect = False Then
                    connect.setDB()
                    If Not connect.connectDB() Then
                        Return "Can't connect to SAP"
                    End If
                End If
                If DocEntry = 0 Then
                    Dim b As New SAP_Functions
                    DocEntry = b.GetMaxDocEntry(DocType, UserID)
                End If
                PublicVariable.oCompany.XMLAsString = True
                PublicVariable.oCompany.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ValidNodesOnly
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
    <WebMethod()> _
    Public Function Insert_Promotion_Transfer(ItemCode As String, CardCode As String, Qty As Integer) As String
        'Try
        '    Dim sStr As String = "Insert Into Promotion_Transfer select '"+


        '    Return sStr
        'Catch ex As Exception
        '    Return ex.ToString
        'End Try
    End Function
End Class