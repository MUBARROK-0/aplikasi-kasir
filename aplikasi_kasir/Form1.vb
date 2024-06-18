Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Form1

    Dim conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet

    Private isResetting As Boolean = False
    Private originalid As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call TampilBarang()

        If ComboBox1.Items.Count = 0 Then
            ComboBox1.Items.Add("kecil")
            ComboBox1.Items.Add("sedang")
            ComboBox1.Items.Add("besar")
        End If

        ' Set AutoGenerateColumns to False
        DataGridView2.AutoGenerateColumns = False
        ' Add the Harga column
        Dim hargaColumn As New DataGridViewTextBoxColumn()
        hargaColumn.Name = "harga_minuman"
        hargaColumn.HeaderText = "Harga"
        hargaColumn.DataPropertyName = "harga_minuman"
        DataGridView2.Columns.Add(hargaColumn)

        DataGridView2.ColumnHeadersVisible = False

        If Integer.TryParse(TextBox1.Text, originalid) Then
            FocusRowById(originalid)
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

    Sub TampilHarga(ByVal id As Integer, ByVal varian As String, ByVal ukuran As String)
        da = New OdbcDataAdapter("SELECT harga_minuman FROM tb_pembeli WHERE id_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        da.SelectCommand.Parameters.AddWithValue("id", id)
        da.SelectCommand.Parameters.AddWithValue("varian", varian)
        da.SelectCommand.Parameters.AddWithValue("ukuran", ukuran)
        ds = New DataSet()
        da.Fill(ds, "harga_minuman")
        DataGridView2.DataSource = ds.Tables("harga_minuman")
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
        ' Cek apakah ukuran tersedia
        If Not UkuranExists(id, varian, ukuran) Then
            MessageBox.Show("Maaf ukuran tersebut tidak tersedia", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

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

        ' Hitung total harga dan tampilkan
        Dim hargaSatuan As Decimal = GetHargaSatuan(id, varian, ukuran)
        Dim totalHarga As Decimal = hargaSatuan * jumlah
        TextBox4.Text = totalHarga.ToString("C")

        ' Refresh data grid
        Call TampilBarang()
        ' Tampilkan harga di DataGridView2
        Call TampilHarga(id, varian, ukuran)

        If originalid = id Then
            FocusRowById(id)
        Else
            originalid = id
        End If
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

    Function GetVarian(ByVal id As Integer) As String
        Dim varian As String = ""
        cmd = New OdbcCommand("SELECT varian_minuman FROM tb_pembeli WHERE id_minuman = ?", conn)
        cmd.Parameters.AddWithValue("id", id)
        dr = cmd.ExecuteReader()
        If dr.Read() Then
            varian = dr("varian_minuman").ToString()
        End If
        dr.Close()
        Return varian
    End Function

    Function GetUkuran(ByVal id As Integer) As String
        Dim ukuran As String = ""
        cmd = New OdbcCommand("SELECT ukuran_minuman FROM tb_pembeli WHERE id_minuman = ?", conn)
        cmd.Parameters.AddWithValue("id", id)
        dr = cmd.ExecuteReader()
        If dr.Read() Then
            ukuran = dr("ukuran_minuman").ToString()
        End If
        dr.Close()
        Return ukuran
    End Function

    Function GetHargaSatuan(ByVal id As Integer, ByVal varian As String, ByVal ukuran As String) As Decimal
        Dim harga As Decimal = 0
        cmd = New OdbcCommand("SELECT harga_minuman FROM tb_pembeli WHERE id_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        cmd.Parameters.AddWithValue("id", id)
        cmd.Parameters.AddWithValue("varian", varian)
        cmd.Parameters.AddWithValue("ukuran", ukuran)
        dr = cmd.ExecuteReader()
        If dr.Read() Then
            harga = Convert.ToDecimal(dr("harga_minuman"))
        End If
        dr.Close()
        Return harga
    End Function

    Function UkuranExists(ByVal id As Integer, varian As String, ByVal ukuran As String) As Boolean
        Dim exists As Boolean = False
        cmd = New OdbcCommand("SELECT COUNT(*) FROM tb_pembeli WHERE id_minuman = ? AND varian_minuman = ? AND ukuran_minuman = ?", conn)
        cmd.Parameters.AddWithValue("id", id)
        cmd.Parameters.AddWithValue("varian", varian)
        cmd.Parameters.AddWithValue("ukuran", ukuran)
        exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
        Return exists
    End Function

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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            Dim id As Integer = Integer.Parse(TextBox1.Text)
            Dim varian As String = GetVarian(id)
            Dim ukuran As String = GetUkuran(id)
            ' Ambil harga minuman untuk id tertentu dengan varian dan ukuran yang sesuai
            Call TampilHarga(id, varian, ukuran)
            FocusRowById(id)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If isResetting Then Return ' Jika sedang reset, keluar dari subroutine

        Dim varian As String = ""
        If RadioButton1.Checked Then
            varian = "botol"
        ElseIf RadioButton2.Checked Then
            varian = "kaleng"
        End If

        If ComboBox1.SelectedItem IsNot Nothing Then
            Dim ukuran As String = ComboBox1.SelectedItem.ToString()

            If Not String.IsNullOrEmpty(TextBox1.Text) Then
                Dim id As Integer = Integer.Parse(TextBox1.Text)
                If Not UkuranExists(id, varian, ukuran) Then
                    MessageBox.Show("Maaf ukuran yang dipilih tidak sesuai dengan id yang dipilih", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Else
            MessageBox.Show("Pilih ukuran terlebih dahulu", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Set isResetting to True
        isResetting = True

        ' Reset TextBoxes
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        TextBox3.Text = String.Empty
        TextBox4.Text = String.Empty

        ' Reset ComboBox
        ComboBox1.SelectedIndex = -1

        ' Reset RadioButtons
        RadioButton1.Checked = False
        RadioButton2.Checked = False

        ' Clear DataGridViews
        DataGridView2.DataSource = Nothing

        ' Optionally, you might want to reset any other controls or variables

        ' Set isResetting back to False
        isResetting = False

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        ' Pastikan TextBox2 tidak kosong
        If Not String.IsNullOrEmpty(TextBox2.Text) Then
            ' Cek apakah TextBox1 juga tidak kosong (ID barang terisi)
            If Not String.IsNullOrEmpty(TextBox1.Text) Then
                Dim id As Integer = Integer.Parse(TextBox1.Text)
                Dim varian As String = ""
                ' Tentukan varian berdasarkan RadioButton yang dipilih
                If RadioButton1.Checked Then
                    varian = "botol"
                ElseIf RadioButton2.Checked Then
                    varian = "kaleng"
                End If
                Dim ukuran As String = ComboBox1.SelectedItem.ToString()

                ' Hitung total harga
                Dim jumlah As Integer = Integer.Parse(TextBox2.Text)
                Dim hargaSatuan As Decimal = GetHargaSatuan(id, varian, ukuran)
                Dim totalHarga As Decimal = hargaSatuan * jumlah

                ' Tampilkan total harga di TextBox4
                TextBox4.Text = totalHarga.ToString("C")
            Else
                MessageBox.Show("Mohon masukkan ID barang terlebih dahulu", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub FocusRowById(id As Integer)
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Convert.ToInt32(row.Cells("id_minuman").Value) = id Then
                DataGridView1.ClearSelection()
                row.Selected = True
                DataGridView1.FirstDisplayedScrollingRowIndex = row.Index
                Exit For
            End If
        Next
    End Sub
End Class
