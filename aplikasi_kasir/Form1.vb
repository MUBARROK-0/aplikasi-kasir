Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Form1

    Dim conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call TampilBarang()
    End Sub

    Sub Koneksi()
        Conn = New OdbcConnection("DSN=db_kasir") ' Ganti dengan DSN yang Anda buat
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
    End Sub

    Sub TampilBarang()
        Da = New OdbcDataAdapter("SELECT * FROM tb_barang", Conn)
        Ds = New DataSet()
        Da.Fill(Ds, "tb_barang")
        DataGridView1.DataSource = Ds.Tables("tb_barang")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim id As Integer = Integer.Parse(TextBox1.Text)
        Dim jumlah As Integer = Integer.Parse(TextBox2.Text)

        ' Kurangi stok barang
        cmd = New OdbcCommand("UPDATE tb_barang SET stok_barang = stok_barang - ? WHERE id_barang = ?", Conn)
        cmd.Parameters.AddWithValue("stok", jumlah)
        cmd.Parameters.AddWithValue("id", id)
        cmd.ExecuteNonQuery()

        ' Refresh data grid
        Call TampilBarang()
    End Sub
End Class
