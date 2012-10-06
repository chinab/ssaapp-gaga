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
    Public Function CreateMarketingDocument(ByVal strXml As String, UserID As String) As DataSet
        Dim b As New SAP_Functions
        Try
            Dim sStr As String = "Operation Completed Successfully!"
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                Return a.Simulate_CreateTransaction()
            Else

                Dim oSO As SAPbobsCOM.Documents
                If Connection.bConnect = False Then
                    connect.setDB(UserID)
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
                    connect.setDB(UserID)
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
                    'oDocment.Browser.Recordset

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
    Public Function GetSalesOpportunity(OpprId As Integer, UserID As String) As String
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
                Dim oDocment As SAPbobsCOM.SalesOpportunities
                If Connection.bConnect = False Then
                    connect.setDB(UserID)
                    If Not connect.connectDB() Then
                        Return "Can't connect to SAP"
                    End If
                End If
                If OpprId = 0 Then
                    Dim b As New SAP_Functions
                    ' OpprId = b.GetMaxDocEntry(DocType, UserID)
                End If
                PublicVariable.oCompany.XMLAsString = True
                PublicVariable.oCompany.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ValidNodesOnly
                oDocment = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oSalesOpportunities)
                If oDocment.GetByKey(OpprId) Then
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
    <WebMethod()> _
    Public Function CreateOpportunity(ByVal strXml As String, UserID As String) As DataSet
        Dim b As New SAP_Functions
        Try
            Dim sStr As String = "Operation Completed Successfully!"
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                Return a.Simulate_CreateTransaction()
            Else

                Dim oSO As SAPbobsCOM.SalesOpportunities
                If Connection.bConnect = False Then
                    connect.setDB(UserID)
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
    Public Function GetMarketingDocument_ReturnDS(DocType As String, DocEntry As Integer, UserID As String) As DataSet
        Try
            Dim sStr As String = ""
            If PublicVariable.Simulate Then
                Return Nothing
            Else
                Dim connect As New Connection()

                If DocEntry = 0 Then
                    Dim b As New SAP_Functions
                    connect.setDB(UserID)
                    DocEntry = b.GetMaxDocEntry(DocType, UserID)
                End If
                Dim HeaderTableName As String = ""
                Dim LineTableName1 As String = ""

                Select Case DocType
                    Case "22"
                        HeaderTableName = "OPOR"
                        LineTableName1 = "POR1"
                    Case "19"
                        HeaderTableName = "ORPC"
                        LineTableName1 = "RPC1"
                    Case "20"
                        HeaderTableName = "OPDN"
                        LineTableName1 = "PDN1"
                    Case "21"
                        HeaderTableName = "ORPD"
                        LineTableName1 = "RPD1"
                    Case "22"
                        HeaderTableName = "OPOR"
                        LineTableName1 = "POR1"
                    Case "13"
                        HeaderTableName = "OINV"
                        LineTableName1 = "INV1"
                    Case "14"
                        HeaderTableName = "ORIN"
                        LineTableName1 = "RIN1"
                    Case "15"
                        HeaderTableName = "ODLN"
                        LineTableName1 = "DLN1"
                    Case "97" 'Sales opportunity
                        HeaderTableName = "OOPR"
                        LineTableName1 = "OPR1"

                End Select
                Dim ds As New DataSet("Document")
                Dim dt1 As New DataTable
                connect.setDB(UserID)
                dt1 = connect.ObjectGetAll_Query_SAP("Select * from " + HeaderTableName + " where DocEntry=" + CStr(DocEntry)).Tables(0)
                dt1.TableName = HeaderTableName

                Dim dt2 As New DataTable
                connect.setDB(UserID)
                dt2 = connect.ObjectGetAll_Query_SAP("Select * from " + LineTableName1 + " where DocEntry=" + CStr(DocEntry)).Tables(0)
                dt2.TableName = LineTableName1

                ds.Tables.Add(dt1.Copy)
                ds.Tables.Add(dt2.Copy)

                Return ds
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    
End Class