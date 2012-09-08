Public Class Simulation

    Public Function Simulate_OCRD(CardType As String) As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("CardCode", GetType(String))
        dtJE.Tables(0).Columns.Add("CardName", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("CardCode") = "C00001"
        dr("CardName") = "Nguyen Van A"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("CardCode") = "C00002"
        dr("CardName") = "Nguyen Van B"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OITM() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("ItemCode", GetType(String))
        dtJE.Tables(0).Columns.Add("ItemName", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("ItemCode") = "ITM00001"
        dr("ItemName") = "Bia 3333"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("ItemCode") = "ITM00002"
        dr("ItemName") = "Bia Tiger"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OWHS() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("WhsCode", GetType(String))
        dtJE.Tables(0).Columns.Add("WhsName", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("WhsCode") = "01"
        dr("WhsName") = "warehouse 1"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("WhsCode") = "02"
        dr("WhsName") = "warehouse 2"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OTVG() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("Code", GetType(String))
        dtJE.Tables(0).Columns.Add("Name", GetType(String))
        dtJE.Tables(0).Columns.Add("Rate", GetType(Double))
        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("Code") = "P1"
        dr("Name") = "VAT IN 10"
        dr("Name") = "10"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("Code") = "P2"
        dr("Name") = "VAT IN 5"
        dr("Name") = "5"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OHEM() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("Code", GetType(String))
        dtJE.Tables(0).Columns.Add("FirstName", GetType(String))
        dtJE.Tables(0).Columns.Add("MidName", GetType(String))
        dtJE.Tables(0).Columns.Add("LastName", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("Code") = "1"
        dr("FirstName") = "TRUONG"
        dr("MidName") = "THAI"
        dr("LastName") = "THUY"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("Code") = "2"
        dr("FirstName") = "PHAM"
        dr("MidName") = "THI"
        dr("LastName") = "LIEU"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OSLP() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("Code", GetType(String))
        dtJE.Tables(0).Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("Code") = "1"
        dr("Name") = "TRUONG THUY"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("Code") = "2"
        dr("Name") = "PHAM LIEU"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OACT() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("AcctCode", GetType(String))
        dtJE.Tables(0).Columns.Add("AcctName", GetType(String))
        dtJE.Tables(0).Columns.Add("FrgName", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("AcctCode") = "155"
        dr("AcctName") = "Tai khoan Kho"
        dr("FrgName") = "Inventory Account"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("AcctCode") = "155"
        dr("AcctName") = "Tai khoan Kho"
        dr("FrgName") = "Inventory Account"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OCPR(ByVal CardCode As String) As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("IsDefault", GetType(String))
        dtJE.Tables(0).Columns.Add("Code", GetType(String))
        dtJE.Tables(0).Columns.Add("FirstName", GetType(String))
        dtJE.Tables(0).Columns.Add("LastName", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("IsDefault") = "Y"
        dr("Code") = "1"
        dr("FirstName") = "TRUONG"
        dr("MidName") = "THAI"
        dr("LastName") = "THUY"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("IsDefault") = "N"
        dr("Code") = "2"
        dr("FirstName") = "PHAM"
        dr("MidName") = "THI"
        dr("LastName") = "LIEU"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OPRJ() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("PrjCode", GetType(String))
        dtJE.Tables(0).Columns.Add("PrjName", GetType(String))        

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("PrjCode") = "110"
        dr("PrjName") = "Project 1"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("PrjCode") = "220"
        dr("PrjName") = "Project 2"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_OSHP() As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("TrnspCode", GetType(String))
        dtJE.Tables(0).Columns.Add("TrnspName", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("TrnspCode") = "1"
        dr("TrnspName") = "Giao hang - Thu tien"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("TrnspCode") = "2"
        dr("TrnspName") = "Thu Tien - Giao hang"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_BPCurrency(CardCode) As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("CurrCode", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("CurrCode") = "VND"
        dtJE.Tables(0).Rows.Add(dr)

        dr = dtJE.Tables(0).NewRow
        dr("CurrCode") = "USD"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_Promotion(UserID As String, ItemCode As String, CardCode As String, _
                                 Quantity As Double, DocDate As Date, Amount As Double) As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("ID", GetType(String))
        dtJE.Tables(0).Columns.Add("ProCode", GetType(String))
        dtJE.Tables(0).Columns.Add("ProName", GetType(String))
        dtJE.Tables(0).Columns.Add("ProValue", GetType(Double))
        dtJE.Tables(0).Columns.Add("ProQty", GetType(Double))
        dtJE.Tables(0).Columns.Add("ProTrf", GetType(String))
        dtJE.Tables(0).Columns.Add("ItemCode", GetType(String))
        dtJE.Tables(0).Columns.Add("ItemName", GetType(String))
        dtJE.Tables(0).Columns.Add("Condition", GetType(Integer))
        dtJE.Tables(0).Columns.Add("IsReplace", GetType(Integer))
        dtJE.Tables(0).Columns.Add("Sole", GetType(Double))
        dtJE.Tables(0).Columns.Add("ChkCond", GetType(Integer))
        dtJE.Tables(0).Columns.Add("HeadDscAmt", GetType(Double))
        dtJE.Tables(0).Columns.Add("HeadDscPer", GetType(Double))
        dtJE.Tables(0).Columns.Add("WhsCode", GetType(String))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("ID") = "1"
        dr("ProCode") = "Scenario1"
        dr("ProName") = "Mua 5 tang 1, nhan tien hoac san pham"
        dr("ProValue") = 8163
        dr("ProQty") = 1
        dr("ProTrf") = 0
        dr("ItemCode") = "ITM00001"
        dr("ItemName") = "Mi 3M"
        dr("Condition") = 1
        dr("IsReplace") = 0
        dr("Sole") = 0
        dr("ChkCond") = 1
        dr("HeadDscAmt") = 0
        dr("HeadDscPer") = 0
        dr("WhsCode") = "02"
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
    Public Function Simulate_GetDefaultInfo(UserID As String, ByVal cardCode As String, ByVal itemCode As String, _
                                       ByVal amount As Single, ByVal refDate As Date) As DataSet
        Dim dtJE = New DataSet
        dtJE.Tables.Add()
        dtJE.Tables(0).Columns.Add("UnitPrice", GetType(String))
        dtJE.Tables(0).Columns.Add("Discount", GetType(String))
        dtJE.Tables(0).Columns.Add("PriceAfDi", GetType(String))
        dtJE.Tables(0).Columns.Add("WhsCode", GetType(String))
        dtJE.Tables(0).Columns.Add("TaxCode", GetType(String))
        dtJE.Tables(0).Columns.Add("TaxRate", GetType(Double))

        Dim dr As DataRow
        dr = dtJE.Tables(0).NewRow
        dr("UnitPrice") = 200000
        dr("Discount") = 3.5
        dr("PriceAfDi") = 193000
        dr("WhsCode") = "01"
        dr("TaxCode") = "S10"
        dr("TaxRate") = 10
        dtJE.Tables(0).Rows.Add(dr)

        Return dtJE
    End Function
End Class
