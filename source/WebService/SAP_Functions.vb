Public Class SAP_Functions
    Public Function Create_IncommingPayment(ARDocEntry As Integer) As String
        Dim str As String = ""
        Dim RetVal As Long
        Dim lErrCode As Integer
        Dim sErrMsg As String
        Dim oPayment As SAPbobsCOM.Payments
        Dim oInvoice As SAPbobsCOM.Documents

        oPayment = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments)
        oInvoice = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices)

        oPayment.CardCode = oInvoice.CardCode
        oPayment.CashAccount = GetCashAccount()
        oPayment.DocDate = oInvoice.DocDate
        oPayment.TaxDate = oInvoice.DocDate

        oPayment.Invoices.DocEntry = ARDocEntry
        oPayment.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_Invoice
        oPayment.CashSum = oInvoice.DocTotal
        RetVal = oPayment.Add
        If RetVal <> 0 Then
            PublicVariable.oCompany.GetLastError(lErrCode, sErrMsg)
            str = sErrMsg
        End If
        Return str
    End Function   
    Public Function GetLastKey(UserID As String, ObjType As String) As String
        Try
            Dim str As String
            str = "Select top(1) LinkAct_3 ObjKey from OACP where isnull(LinkAct_3,'')<>''"
            Dim dt As DataTable
            Dim connect As New Connection()
            connect.setDB()
            dt = connect.ObjectGetAll_Query_SAP(str).Tables(0)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("ObjKey").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function GetCashAccount() As String
        Try
            Dim str As String
            str = "Select top(1) LinkAct_3 CashAccount from OACP where isnull(LinkAct_3,'')<>''"
            Dim dt As DataTable
            Dim connect As New Connection()
            connect.setDB()
            dt = connect.ObjectGetAll_Query_SAP(str).Tables(0)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("CashAccount").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetPriceAfterDiscount(ByVal cardCode As String, ByVal itemCode As String, ByVal amount As Single, ByVal refDate As Date) As Double
        Dim connect As New Connection()
        If Connection.bConnect = False Then
            connect.setDB()
            If Not connect.connectDB() Then
                Return Nothing
            End If
        End If
        Dim vObj As SAPbobsCOM.SBObob
        Dim rs As SAPbobsCOM.Recordset
        vObj = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge)
        rs = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        rs = vObj.GetItemPrice(cardCode, itemCode, amount, refDate)
        Return rs.Fields.Item("Price").Value
    End Function
    Public Function GetGrossPrice(ByVal cardCode As String, ByVal itemCode As String) As Double
        Dim connect As New Connection()
        connect.setDB()
        Dim dt As DataTable
        Dim str As String = ""
        str = " select isnull(t0.Price,0) GrossPrice from ITM1 T0 "
        str = str & " join OCRD T1 on T0.PriceList=T1.ListNum "
        str = str & " where T0.ItemCode='" + itemCode + "' and T1.CardCode='" + cardCode + "'"
        dt = connect.ObjectGetAll_Query_SAP(str).Tables(0)
        If dt.Rows.Count = 0 Then
            Return 0
        Else
            Return dt.Rows(0).Item("GrossPrice")
        End If
    End Function
    Public Function GetDefaultLineInfo(UserID As String, ByVal cardCode As String, ByVal itemCode As String, _
                                       ByVal amount As Single, ByVal refDate As Date) As DataSet
        Dim ds = New DataSet
        ds.Tables.Add()
        ds.Tables(0).Columns.Add("UnitPrice", GetType(Double))
        ds.Tables(0).Columns.Add("Discount", GetType(Double))
        ds.Tables(0).Columns.Add("PriceAfDi", GetType(Double))
        ds.Tables(0).Columns.Add("WhsCode", GetType(String))
        ds.Tables(0).Columns.Add("TaxCode", GetType(String))
        ds.Tables(0).Columns.Add("TaxRate", GetType(Double))

        Dim GrossPrice As Double = GetGrossPrice(cardCode, itemCode)
        Dim NetPrice As Double = GetPriceAfterDiscount(cardCode, itemCode, amount, refDate)
        Dim Discount As Double = 0
        Dim WhsCode As String =  GetDefaultWarehouse(UserID)
        If WhsCode = "" Then WhsCode = "01"

        If GrossPrice = 0 Then
            Discount = 0
        Else
            Discount = (GrossPrice - NetPrice) * 100 / GrossPrice
        End If

        Dim dr As DataRow
        dr = ds.Tables(0).NewRow
        dr("UnitPrice") = GrossPrice
        dr("Discount") = Discount
        dr("PriceAfDi") = NetPrice
        dr("WhsCode") = WhsCode
        dr("TaxCode") = "S10"
        dr("TaxRate") = 10
        ds.Tables(0).Rows.Add(dr)

        Return ds
    End Function
    Public Function GetDefaultVendor(UserID As String) As String
        Try
            Dim str As String
            str = "Select 'V0000001' BP"
            Dim dt As DataTable
            Dim connect As New Connection()
            connect.setDB()
            dt = connect.ObjectGetAll_Query_SAP(str).Tables(0)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("BP").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetDefaultCustomer(UserID As String) As String
        Try
            Dim str As String
            str = "Select top(1) DfltCard BP from OACP where isnull(DfltCard,'')<>''"
            Dim dt As DataTable
            Dim connect As New Connection()
            connect.setDB()
            dt = connect.ObjectGetAll_Query_SAP(str).Tables(0)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("BP").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetDefaultWarehouse(UserID As String) As String
      Dim str As String = ""
        Try
            str = "select top(1) T0.WhsCode from OWHS T0 join OLCT T1 on T0.Location=T1.Code where T1.Location='" + UserID + "' and T0.U_WhsType=1"

            Dim ors As SAPbobsCOM.Recordset
            ors = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            ors.DoQuery(str)
            If ors.RecordCount = 1 Then
                Return ors.Fields.Item("WhsCode").Value.ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function GetPromotionWarehouse(UserID As String) As String
        Dim str As String = ""
        Try
            str = "select top(1) T0.WhsCode from OWHS T0 join OLCT T1 on T0.Location=T1.Code where T1.Location='" + UserID + "' and T0.U_WhsType=2"

            Dim ors As SAPbobsCOM.Recordset
            ors = PublicVariable.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            ors.DoQuery(str)
            If ors.RecordCount = 1 Then
                Return ors.Fields.Item("WhsCode").Value.ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function ConvertRS2DT(ByVal RS As SAPbobsCOM.Recordset) As DataSet
        Dim dtTable As New DataSet
        dtTable.Tables.Add()
        Dim NewCol As DataColumn
        Dim NewRow As DataRow
        Dim ColCount As Integer
        Try
            For ColCount = 0 To RS.Fields.Count - 1
                Dim dataType As String = "System."
                Select Case RS.Fields.Item(ColCount).Type
                    Case SAPbobsCOM.BoFieldTypes.db_Alpha
                        dataType = dataType & "String"
                    Case SAPbobsCOM.BoFieldTypes.db_Date
                        dataType = dataType & "DateTime"
                    Case SAPbobsCOM.BoFieldTypes.db_Float
                        dataType = dataType & "Double"
                    Case SAPbobsCOM.BoFieldTypes.db_Memo
                        dataType = dataType & "String"
                    Case SAPbobsCOM.BoFieldTypes.db_Numeric
                        dataType = dataType & "Decimal"
                    Case Else
                        dataType = dataType & "String"
                End Select

                NewCol = New DataColumn(RS.Fields.Item(ColCount).Name, System.Type.GetType(dataType))
                dtTable.Tables(0).Columns.Add(NewCol)
            Next

            Do Until RS.EoF

                NewRow = dtTable.Tables(0).NewRow
                'populate each column in the row we're creating
                For ColCount = 0 To RS.Fields.Count - 1

                    NewRow.Item(RS.Fields.Item(ColCount).Name) = RS.Fields.Item(ColCount).Value

                Next

                'Add the row to the datatable
                dtTable.Tables(0).Rows.Add(NewRow)

                RS.MoveNext()
            Loop
            Return dtTable
        Catch ex As Exception
            MsgBox(ex.ToString & Chr(10) & "Error converting SAP Recordset to DataTable", MsgBoxStyle.Exclamation)
            Return Nothing
        End Try
    End Function
    Public Function GetPromotion(UserID As String, ItemCode As String, CardCode As String, _
                                 Quantity As Double, DocDate As Date, Amount As Double) As DataSet
        Try
            Dim str As String
            str = "exec sp_Promotion_Get '" & ItemCode & "'," & CStr(Quantity) & ",'" & CStr(DocDate) & "'," & CStr(Amount) & ",'" & CardCode & "'"
            Dim dt As DataSet
            Dim connect As New Connection()
            connect.setDB()
            dt = connect.ObjectGetAll_Query_SAP(str)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
