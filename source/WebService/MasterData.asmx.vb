Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://electra-ai.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class MasterData
    Inherits System.Web.Services.WebService
    Dim connect As New Connection()
    <WebMethod()> _
    Public Function GetBusinessPartner(CardType As String) As DataSet
        Try
            Dim dt As New DataSet("OCRD")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OCRD(CardType)
            Else
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP("Select CardCode,CardName from OCRD Where CardType='" + CardType + "'")
            End If

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetItemMasterData() As DataSet
        Try
            Dim dt As New DataSet("OITM")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OITM
            Else
                connect.setDB()                
                dt = connect.ObjectGetAll_Query_SAP("Select ItemCode,ItemName from OITM")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetWarehouse() As DataSet
        Try
            Dim dt As New DataSet("OWHS")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OWHS
            Else                
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP("Select WhsCode,WhsName from OWHS")
            End If          
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetTaxGroup(ByVal Category As String) As DataSet
        Try
            Dim dt As New DataSet("OVTG")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OTVG
            Else                
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP("Select Code,Name,rate from OVTG where Category='" + Category + "'")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetEmployeeMasterData() As DataSet
        Try
            Dim dt As New DataSet("OHEM")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OHEM
            Else
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP("Select empID Code,LastName,firstName,MiddleName from ohem")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetSalesBuyerMasterData() As DataSet
        Try
            Dim dt As New DataSet("OSLP")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OSLP
            Else
                connect.setDB()                
                dt = connect.ObjectGetAll_Query_SAP("select SlpCode Code,SlpName Name from OSLP order by SlpName")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetAccountMasterData(Filter As String) As DataSet
        Try
            Dim dt As New DataSet("OACT")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OACT
            Else
                Dim str As String
                str = "select AcctCode,AcctName,FrgnName from OACT where Postable='Y'"
                Select Case Filter
                    Case "Revenue"
                        str = str + " and ActType='I'"
                    Case "AR"
                        str = str + " and LocManTran='Y' and GroupMask=1"
                    Case "AP"
                        str = str + " and LocManTran='Y' and GroupMask=2"
                End Select
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP(str)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetContactPerson(ByVal CardCode As String) As DataSet
        Try
            Dim dt As New DataSet("OCPR")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OCPR(CardCode)
            Else
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP("select CntctCode Code,Name FirstName,'' LastName, 0 IsDefault from OCPR where CardCode='" + CardCode + "'")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetProject() As DataSet
        Try
            Dim dt As New DataSet("OPRJ")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OPRJ
            Else
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP("select PrjCode,PrjName from OPRJ ")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetShippingType() As DataSet
        Try
            Dim dt As New DataSet("OSHP")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OSHP
            Else
                connect.setDB()
                dt = connect.ObjectGetAll_Query_SAP("select TrnspCode,TrnspName from OSHP")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetBPCurrency(CardCode As String) As DataSet
        Try
            Dim dt As New DataSet("OCRD")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_BPCurrency(CardCode)
            Else
                connect.setDB()
                Dim str As String = "select T1.CurrCode from ocrd T0 "
                str = str + " full join OCRN T1 oN T0.Currency=T1.CurrCode or T0.Currency='##'"
                str = str + " where T0.cardcode='" + CardCode + "'"
                dt = connect.ObjectGetAll_Query_SAP(str)
            End If

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class