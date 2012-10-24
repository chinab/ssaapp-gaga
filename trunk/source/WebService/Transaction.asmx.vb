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
    Public Function CreateMarketingDocument(ByVal strXml As String, UserID As String, DocType As String, Key As String, IsUpdate As Boolean) As DataSet
        Dim b As New SAP_Functions
        Try
            Dim sStr As String = "Operation Completed Successfully!"
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                Return a.Simulate_CreateTransaction()
            Else

                Dim oDocment
                Select Case DocType
                    Case "30"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.JournalEntries)
                    Case "97"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.SalesOpportunities)
                    Case "191"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.ServiceCalls)
                    Case "33"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.Contacts)
                    Case Else
                        oDocment = DirectCast(oDocment, SAPbobsCOM.Documents)
                End Select

                If Connection.bConnect = False Then
                    connect.setDB(UserID)
                    If Not connect.connectDB() Then
                        Return b.ReturnMessage(-1, "Connect SAP failed")
                    End If
                End If
                PublicVariable.oCompany.XMLAsString = True
                oDocment = PublicVariable.oCompany.GetBusinessObjectFromXML(strXml, 0)
                If IsUpdate Then
                    If oDocment.GetByKey(Key) Then
                        oDocment.Browser.ReadXML(strXml, 0)
                        lErrCode = oDocment.Update()
                    Else
                        Return b.ReturnMessage(-1, "Record not found!")
                    End If
                Else
                    lErrCode = oDocment.Add()
                End If

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
        Try
            Dim sStr As String = ""
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                Return a.Simulate_OPOR
            Else
                Dim oDocment
                Select Case DocType
                    Case "30"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.JournalEntries)
                    Case "97"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.SalesOpportunities)
                    Case "191"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.ServiceCalls)
                    Case "33"
                        oDocment = DirectCast(oDocment, SAPbobsCOM.Contacts)
                    Case Else
                        oDocment = DirectCast(oDocment, SAPbobsCOM.Documents)
                End Select
                '-----------------TEST--------------------
                'oDocment = DirectCast(oDocment, SAPbobsCOM.Contacts)

                If Connection.bConnect = False Then
                    connect.setDB(UserID)
                    If Not connect.connectDB() Then
                        Return "Can't connect to SAP"
                    End If
                End If
                PublicVariable.oCompany.XMLAsString = True
                PublicVariable.oCompany.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ValidNodesOnly

                oDocment = PublicVariable.oCompany.GetBusinessObject(DocType)
                '-----------------TEST--------------------
                'oDocment = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oContacts)
                If oDocment.GetByKey(DocEntry) Then
                    oDocment.SaveXML(sStr)
                    Return sStr
                Else
                    Return "Error: docentry not found"
                End If
            End If
        Catch ex As Exception
            Return ex.ToString
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
                Dim KeyName As String = "DocEntry"
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
                    Case "33" 'Sales opportunity
                        HeaderTableName = "OCLG"
                        LineTableName1 = "OCLG"
                        KeyName = "ClgCode"
                End Select
                Dim ds As New DataSet("Document")
                Dim dt1 As New DataTable
                connect.setDB(UserID)
                dt1 = connect.ObjectGetAll_Query_SAP("Select * from " + HeaderTableName + " where " + KeyName + "=" + CStr(DocEntry)).Tables(0)
                dt1.TableName = HeaderTableName
                ds.Tables.Add(dt1.Copy)

                If HeaderTableName <> LineTableName1 Then
                    Dim dt2 As New DataTable
                    connect.setDB(UserID)
                    dt2 = connect.ObjectGetAll_Query_SAP("Select * from " + LineTableName1 + " where DocEntry=" + CStr(DocEntry)).Tables(0)
                    dt2.TableName = LineTableName1
                    ds.Tables.Add(dt2.Copy)
                End If
               
                Return ds
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    
End Class