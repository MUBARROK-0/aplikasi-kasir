﻿Imports System.Data.Odbc
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
        ComboBox1.Items.Add("kecil")
        ComboBox1.Items.Add("sedang")
        ComboBox1.Items.Add("besar")
    End Sub

    Sub Koneksi()
        conn = New OdbcConnection("DSN=db_kasir") ' Ganti dengan DSN yang Anda buat
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub

    Sub TampilBarang()
        da = New OdbcDataAdapter("SELECT * FROM tb_barang", conn)
        ds = New DataSet()
        Da.Fill(Ds, "tb_barang")
        DataGridView1.DataSource = Ds.Tables("tb_barang")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim id As Integer = Integer.Parse(TextBox1.Text)
        Dim jumlah As Integer = Integer.Parse(TextBox2.Text)
        Dim merk = TextBox3.Text.Trim()

        Dim varian As String = ""
        If RadioButton1.Checked Then
            varian = "botol"
        ElseIf RadioButton2.Checked Then
            varian = "kaleng"
        Else
            MessageBox.Show("Pilih Varian Minuman", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim ukuran As String = ComboBox1.SelectedItem.ToString()

        If merk <> "" Then
            If Not MerkExists(merk, varian, ukuran) Then
                MessageBox.Show("Merk tidak tersedia", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If

        ' Kurangi stok barang
        cmd = New OdbcCommand("UPDATE tb_barang SET stok_barang = stok_barang - ? WHERE id_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        cmd.Parameters.AddWithValue("stok", jumlah)
        cmd.Parameters.AddWithValue("id", id)
        cmd.Parameters.AddWithValue("varian", varian)
        cmd.Parameters.AddWithValue("ukuran", ukuran)
        cmd.ExecuteNonQuery()


        ' Refresh data grid
        Call TampilBarang()
    End Sub
    Function MerkExists(ByVal merk As String, ByVal varian As String, ByVal ukuran As String) As Boolean
        Dim exists As Boolean = False
        cmd = New OdbcCommand("SELECT COUNT(*) FROM tb_barang WHERE merk_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        cmd.Parameters.AddWithValue("merk", merk)
        cmd.Parameters.AddWithValue("varian", varian)
        cmd.Parameters.AddWithValue("ukuran", ukuran)
        exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        Return exists
    End Function
End Class
