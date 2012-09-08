Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.SqlTypes
Public Class Connection
    Public Shared sConn As SqlConnection
    Public Shared sConnSAP As SqlConnection
    Public Shared bConnect As Boolean

    Public Sub setDB()
        Try
            Dim strConnect As String = ""
            Dim sCon As String = ""
            Dim SQLType As String = ""
            Dim MyArr As Array
            Dim sErrMsg As String = ""
            strConnect = "SAPConnect"
            sCon = System.Configuration.ConfigurationSettings.AppSettings.Get(strConnect)
            MyArr = sCon.Split(";")
            PublicVariable.oCompany.CompanyDB = MyArr(0).ToString()
            PublicVariable.oCompany.UserName = MyArr(1).ToString()
            PublicVariable.oCompany.Password = MyArr(2).ToString()
            PublicVariable.oCompany.Server = MyArr(3).ToString()
            PublicVariable.oCompany.DbUserName = MyArr(4).ToString()
            PublicVariable.oCompany.DbPassword = MyArr(5).ToString()
            PublicVariable.oCompany.LicenseServer = MyArr(6)
            SQLType = MyArr(7)
            If SQLType = 2008 Then
                PublicVariable.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008
            Else
                PublicVariable.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005
            End If
            sCon = "server= " + MyArr(3).ToString() + ";database=" + MyArr(0).ToString() + " ;uid=" + MyArr(4).ToString() + "; pwd=" + MyArr(5).ToString() + ";"
            sConnSAP = New SqlConnection(sCon)
        Catch ex As Exception
            Dim file As System.IO.StreamWriter = New System.IO.StreamWriter("C:\\Error\\Webservice_SalesCall\\SetDB.txt", True)
            file.WriteLine(ex)
            file.Close()
        End Try
    End Sub
    Public Function connectDB() As Boolean
        Try
            Dim sErrMsg As String = ""
            Dim connectOk As Integer = 0
            If PublicVariable.oCompany.Connect() <> 0 Then
                PublicVariable.oCompany.GetLastError(connectOk, sErrMsg)
                bConnect = False
                Return False
            Else                
                bConnect = True
                Return True
            End If
        Catch ex As Exception
            Dim file As System.IO.StreamWriter = New System.IO.StreamWriter("C:\\Error\\Webservice_SalesCall\\connectDB.txt", True)
            file.WriteLine(ex)
            file.Close()
            Throw ex
        End Try
    End Function
#Region "ADO SAP"
    Private Function GetConnectionString_SAP() As SqlConnection
        If sConnSAP.State = ConnectionState.Open Then
            sConnSAP.Close()
        End If
        Try
            sConnSAP.Open()
        Catch ex As Exception
            WriteLog(ex.ToString)
        End Try
        Return sConnSAP
    End Function
    Public Function ObjectGetAll_Query_SAP(ByVal QueryString As String) As DataSet
        Try

            Using myConn = GetConnectionString_SAP()
                Dim MyCommand As SqlCommand = New SqlCommand(QueryString, myConn)
                MyCommand.CommandType = CommandType.Text
                Dim da As SqlDataAdapter = New SqlDataAdapter()
                Dim mytable As DataSet = New DataSet()
                da.SelectCommand = MyCommand
                da.Fill(mytable)
                myConn.Close()
                Return mytable
            End Using
        Catch ex As Exception
            WriteLog(ex.ToString)
            Return Nothing
        End Try
    End Function
#End Region
    Public Shared Sub WriteLog(ByVal Str As String)
        Dim oWrite As IO.StreamWriter
        Dim FilePath As String
        FilePath = "C:\\SBOWS]\logfile.txt"

        If IO.File.Exists(FilePath) Then
            oWrite = IO.File.AppendText(FilePath)
        Else
            oWrite = IO.File.CreateText(FilePath)
        End If
        oWrite.Write(Now.ToString() + ":" + Str + vbCrLf)
        oWrite.Close()
    End Sub
End Class
