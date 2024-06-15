Imports System.Data.Odbc
Module Module1
    Public Da As OdbcDataAdapter
    Public Ds As DataSet
    Public Str As String
    Public Conn As New OdbcConnection

    Public Sub Koneksi()
        Conn = New OdbcConnection("DSN=aplikasi_kasir;multipleActiveResultSets=true")
        Conn.Open()
        If Conn.State = ConnectionState.Open Then

        End If
    End Sub
End Module
