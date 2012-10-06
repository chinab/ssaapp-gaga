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
    Public Function GetBusinessPartner(CardType As String, UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OCRD")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OCRD(CardType)
            Else
                Dim str As String
                If CardType = "C" Then
                    str = "Select CardCode,CardName from OCRD Where CardType='C'" ' and GroupCode=102"
                Else
                    str = "Select CardCode,CardName from OCRD Where CardType='S'"
                End If
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP(str)
            End If

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetItemMasterData(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OITM")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OITM
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("Select ItemCode,ItemName from OITM order by ItemCode")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetWarehouse(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OWHS")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OWHS
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("Select WhsCode,WhsName from OWHS order by WhsCode")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetTaxGroup(ByVal Category As String, UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OVTG")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OTVG
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("Select Code,Name,rate from OVTG where Category='" + Category + "' order by Code")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetEmployeeMasterData(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OHEM")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OHEM
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("Select empID Code,LastName,firstName,MiddleName from ohem")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetSalesBuyerMasterData(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OSLP")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OSLP
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select SlpCode Code,SlpName Name from OSLP order by SlpCode")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetAccountMasterData(Filter As String, UserID As String) As DataSet
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
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP(str)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetContactPerson(ByVal CardCode As String, UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OCPR")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OCPR(CardCode)
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select CntctCode Code,Name FirstName,'' LastName, 0 IsDefault from OCPR where CardCode='" + CardCode + "'")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetProject(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OPRJ")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OPRJ
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select PrjCode,PrjName from OPRJ ")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetShippingType(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OSHP")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OSHP
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select TrnspCode,TrnspName from OSHP")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetBPCurrency(CardCode As String, UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OCRD")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_BPCurrency(CardCode)
            Else
                Dim str As String
                If CardCode = "" Then
                    str = "select T0.CurrCode from OCRN T0 "
                Else
                    str = "select T1.CurrCode from ocrd T0 "
                    str = str + " full join OCRN T1 oN T0.Currency=T1.CurrCode or T0.Currency='##'"
                    str = str + " where T0.cardcode='" + CardCode + "'"
                End If
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP(str)
            End If

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetIndicator(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OIDC")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OIDC
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("Select Code,Name from OIDC")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetPaymentTerm(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("octg")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OCTG
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select GroupNum,PymntGroup from octg order by GroupNum")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '----------------------------------------------OTHER MASTER DATA---------------------------------
    <WebMethod()> _
    Public Function GetItemGroup(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OITB")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OITB
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("Select ItmsGrpCod,ItmsGrpNam from OITB")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetPriceList(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OPLN")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OPLN
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select ListNum,ListName from opln")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetManufacture(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OMRC")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OMRC
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select FirmCode,FirmName from OMRC")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetIndustry(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OOND")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OOND
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select IndCode,IndName from OOND")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetTerritory(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OTER")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OIDC
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select T0.territryID,T0.descript,T1.descript Parent from OTER T0 left join OTER T1 on T1.territryID=T0.parent")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetDisplaySetting(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OADM")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OADM
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select CompnyName,DecSep,ThousSep,SumDec,PriceDec,QtyDec,PercentDec,RateDec,DateFormat,DateSep from OADM")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetCostCenter(DimCode As Integer, UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OPRC")
            If PublicVariable.Simulate Then
                Dim a As New Simulation
                dt = a.Simulate_OPRC
            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select PrcCode,PrcName from OPRC where DimCode=" + CStr(DimCode))
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetInformationSource(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OOSR")
            If PublicVariable.Simulate Then

            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select Num,Descript from OOSR")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetStage(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OOST")
            If PublicVariable.Simulate Then

            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select Num,Descript,CloPrcnt from OOST")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetPartners(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OPRT")
            If PublicVariable.Simulate Then

            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select PrtId,Name from OPRT")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetCompetitor(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OCMT")
            If PublicVariable.Simulate Then

            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select CompetId,Name from OCMT")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function GetLevelOfInterest(UserID As String) As DataSet
        Try
            Dim dt As New DataSet("OOIR")
            If PublicVariable.Simulate Then

            Else
                connect.setDB(UserID)
                dt = connect.ObjectGetAll_Query_SAP("select Num,Descript from OOIR")
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class