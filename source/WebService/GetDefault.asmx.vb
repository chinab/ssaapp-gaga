﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class GetDefault
    Inherits System.Web.Services.WebService
#Region "Default Infor"
    '- Goi ws de lay default vendor/customer
    '- default date = today
    '- Sau khi chon Item: hien thi ItemCode, Description, Quantity=1,  goi ws de lay gia tri default
    '- Doi so luong: goi ws de lay gia tri default
#End Region
    <WebMethod()> _
    Public Function GetDefaultLineInfo(UserID As String, ByVal cardCode As String, ByVal itemCode As String, ByVal Quantity As Double, _
                                    ByVal refDate As Date) As DataSet
        If PublicVariable.Simulate Then
            Dim a As New Simulation
            Return a.Simulate_GetDefaultInfo(UserID, cardCode, itemCode, Quantity, refDate)
        Else
            Dim a As New SAP_Functions
            Return a.GetDefaultLineInfo(UserID, cardCode, itemCode, Quantity, refDate)
        End If
    End Function
    
    <WebMethod()> _
    Public Function GetDefaultBP(UserID As String, CardType As String) As DataSet
        If PublicVariable.Simulate Then
            Dim a As New Simulation
            Return a.Simulate_GetDeafaultBP(UserID, CardType)
        Else
            Dim a As New SAP_Functions
            Return a.GetDefaultBP(UserID, CardType)
        End If
    End Function
#Region "Apply Promotion"
    '- Neu WS GetPromotion tra ve nhieu hon 1 record thi show page "Promotion Selection" de chon 1, sau do theo logic duoi.

    '- Neu WS GetPromotion Tra ve 1 record thi theo logic duoi.

    'Logic Apply Promotion:
    '1. Discount By Promotion = HeadDscAmt + HeadDscPer*UnitPrice/100 + ProValue
    '2. Unit Price = Unit Price - Discount By Promotion
    '3. So le = field Sole
    '4. ProCode = field ProCode
    '5. Neu ProQty>0: them 1 dong vao grid
    '    Item Code= Item Code cua dong apply
    '    Description = Description cua dong apply
    '    Quantity=ProQty
    '    Unit Price,Discount,Discount By Promotion  = 0
    '    Warehouse = WS GetPromotionWarehouse
    '    U_ProLine = Y
    '    ProCode = field ProCode
    '    So le = field Sole
#End Region
    <WebMethod()> _
    Public Function GetPromotion(UserID As String, ItemCode As String, CardCode As String, _
                                 Quantity As Double, DocDate As Date, Amount As Double) As DataSet
        If PublicVariable.Simulate Then
            Dim a As New Simulation
            Return a.Simulate_Promotion(UserID, ItemCode, CardCode, Quantity, DocDate, Amount)
        Else
            Dim a As New SAP_Functions
            Return a.GetPromotion(UserID, ItemCode, CardCode, Quantity, DocDate, Amount)
        End If              
    End Function
    <WebMethod()> _
    Public Function GetCopyFromTo(Type As Integer, ObjType As String) As DataSet
        Dim a As New SAP_Functions
        'Type=1: Copy To
        'Type=2: Copy From
        'ObjType=22: Purchase Order
        Return a.GetGopyFromTo(Type, ObjType)
    End Function
    <WebMethod()> _
    Public Function GetConnection() As Integer
        Dim connect As New Connection()
        connect.setDB()
        If Not connect.connectDB() Then
            Return -1
        Else
            Return 0
        End If
    End Function

    <WebMethod()> _
    Public Function GetLoginInfo(UserID As String) As DataSet
        If PublicVariable.Simulate Then
            Dim a As New Simulation
            Return a.Simulate_GetDeafaultBP(UserID, "")
        Else
            Dim a As New SAP_Functions
            Return a.GetLoginInfo(UserID)
        End If
    End Function
    <WebMethod()> _
    Public Function GetOpenDocument(UserID As String, CardCode As String, DocType As String) As DataSet
        If PublicVariable.Simulate Then
            Dim a As New Simulation
            Return a.Simulate_GetDeafaultBP(UserID, "")
        Else
            Dim dt As DataSet
            Dim connect As New Connection()
            connect.setDB()
            Dim str As String
            str = "select DocEntry,DocDate,DocDueDate,Comments,DocTotal from "
            Select Case DocType
                Case "17"
                    str = str + "ORDR"
                Case "15"
                    str = str + "ODLN"
            End Select
            str = str + " where DocStatus='O' and CardCode='" + CardCode + "'"
            dt = connect.ObjectGetAll_Query_SAP(str)
            Return dt
        End If
    End Function

    <WebMethod()> _
    Public Function CreateUDF() As String
        If PublicVariable.Simulate Then
            Dim a As New Simulation
            Return ""
        Else
            Dim a As New SAP_Functions
            Return a.CreateUDF("ORDR", "UserID", "UserID", SAPbobsCOM.BoFieldTypes.db_Alpha, 30, "")
        End If
    End Function
End Class