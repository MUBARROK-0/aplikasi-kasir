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
        If ComboBox1.Items.Count = 0 Then
            ComboBox1.Items.Add("kecil")
            ComboBox1.Items.Add("sedang")
            ComboBox1.Items.Add("besar")
        End If
    End Sub

    Sub Koneksi()
        conn = New OdbcConnection("DSN=aplikasi_kasir") ' Ganti dengan DSN yang Anda buat
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub

    Sub TampilBarang()
        da = New OdbcDataAdapter("SELECT * FROM tb_pembeli", conn)
        ds = New DataSet()
        da.Fill(ds, "tb_pembeli")
        DataGridView1.DataSource = ds.Tables("tb_pembeli")
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

        ' Cek stok barang
        Dim stok As Integer = CekStok(id, varian, ukuran)
        If stok < jumlah Then
            MessageBox.Show("Maaf stok tidak mencukupi", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Kurangi stok barang
        cmd = New OdbcCommand("UPDATE tb_pembeli SET stok_minuman = stok_minuman - ? WHERE id_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        cmd.Parameters.AddWithValue("stok", jumlah)
        cmd.Parameters.AddWithValue("id", id)
        cmd.Parameters.AddWithValue("varian", varian)
        cmd.Parameters.AddWithValue("ukuran", ukuran)
        cmd.ExecuteNonQuery()


        ' Refresh data grid
        Call TampilBarang()
        ' Tampilkan harga di DataGridView2
        Call TampilHarga(id, varian, ukuran)
    End Sub

    Function MerkExists(ByVal merk As String, ByVal varian As String, ByVal ukuran As String) As Boolean
        Dim exists As Boolean = False
        cmd = New OdbcCommand("SELECT COUNT(*) FROM tb_pembeli WHERE merk_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        cmd.Parameters.AddWithValue("merk", merk)
        cmd.Parameters.AddWithValue("varian", varian)
        cmd.Parameters.AddWithValue("ukuran", ukuran)
        exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        Return exists
    End Function

    Function CekStok(ByVal id As Integer, ByVal varian As String, ByVal ukuran As String) As Integer
        Dim stok As Integer = 0
        cmd = New OdbcCommand("SELECT stok_minuman FROM tb_pembeli WHERE id_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        cmd.Parameters.AddWithValue("id", id)
        cmd.Parameters.AddWithValue("varian", varian)
        cmd.Parameters.AddWithValue("ukuran", ukuran)
        dr = cmd.ExecuteReader()
        If dr.Read() Then
            stok = Convert.ToInt32(dr("stok_minuman"))
        End If
        dr.Close()
        Return stok
    End Function

    Sub TampilHarga(ByVal id As Integer, ByVal varian As String, ByVal ukuran As String)
        da = New OdbcDataAdapter("SELECT harga_minuman FROM tb_pembeli WHERE id_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        da.SelectCommand.Parameters.AddWithValue("id", id)
        da.SelectCommand.Parameters.AddWithValue("varian", varian)
        da.SelectCommand.Parameters.AddWithValue("ukuran", ukuran)
        ds = New DataSet()
        da.Fill(ds, "harga_minuman")
        DataGridView2.DataSource = ds.Tables("harga_minuman")
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("kecil")
            ComboBox1.SelectedItem = "kecil"
            MessageBox.Show("Varian kaleng hanya tersedia ukuran kecil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("kecil")
            ComboBox1.Items.Add("sedang")
            ComboBox1.Items.Add("besar")
        End If
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class
