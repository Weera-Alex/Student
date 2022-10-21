Imports System.Data.SqlClient

Public Class Form1

    Private cmd As New SqlCommand
    Private ReadOnly cn As New SqlConnection("Data Source=.;Initial Catalog=scream;Integrated Security=True;Pooling=False")
    Private adapt As SqlDataAdapter
    Private dt As DataTable

    Public Sub New()
        InitializeComponent()
        DisplayData()
    End Sub

    Public Sub DisplayData() 'display all request list of current user
        cn.Open()
        Dim dt As New DataTable()
        adapt = New SqlDataAdapter("select * from BatteryTable", cn)
        adapt.Fill(dt)
        DataGridView1.DataSource = dt
        cn.Close()
    End Sub

    Public Sub cleartextboxes()
        id.Text = ""
        studentname.Text = ""
        Title.Text = ""
        Genre.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If studentname.Text <> "" AndAlso Title.Text <> "" AndAlso Genre.Text <> "" Then
            Dim code As String
            Dim random As New Random()
            Dim value As Integer
            value = random.Next(0, 99999)
            studentname.Text.ToUpper()
            If studentname.Text.Count > 0 Then
                Dim remove As String = id.Text.ToUpper.Replace(" ", "")
                remove = remove.Replace("A", "").Replace("E", "").Replace("I", "").Replace("O", "").Replace("U", "").Replace("1", "").
                    Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").
                    Replace("8", "").Replace("9", "").Replace("0", "")
                code = Format(Date.Now, "yyyy") & remove & value
                id.Text = code
            End If
            cmd = New SqlCommand("insert into BatteryTable values(@id,@studentname,@title,@genre)", cn)
            cn.Open()
            cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id.Text
            cmd.Parameters.Add("@studentname", SqlDbType.NVarChar).Value = studentname.Text
            cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = Title.Text
            cmd.Parameters.Add("@genre", SqlDbType.NVarChar).Value = Genre.Text
            cmd.ExecuteNonQuery()
            cn.Close()
            DisplayData()
            cleartextboxes()
            MessageBox.Show("added succesfully")
        Else
            MessageBox.Show("fill all fields", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If id.Text <> "" AndAlso studentname.Text <> "" AndAlso Title.Text <> "" AndAlso Genre.Text <> "" Then
            cmd = New SqlCommand("update BatteryTable set Id=@studentId,Name=@studentName,
            Title=@title,Genre=@genre where Id=@studentId", cn)
            cn.Open()
            cmd.Parameters.Add("@studentId", SqlDbType.NVarChar).Value = id.Text
            cmd.Parameters.Add("@studentName", SqlDbType.NVarChar).Value = studentname.Text
            cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = Title.Text
            cmd.Parameters.Add("@genre", SqlDbType.NVarChar).Value = Genre.Text
            cmd.ExecuteNonQuery()
            MessageBox.Show("edited succesfully")
            cn.Close()
            DisplayData()
            cleartextboxes()
        Else
            MessageBox.Show("fill all fields")
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        id.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()
        studentname.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString()
        Title.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString()
        Genre.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If id.Text <> "" Then

            cmd = New SqlCommand("delete BatteryTable where id=@Id", cn)
            cn.Open()
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id.Text
            cmd.ExecuteNonQuery()
            cn.Close()
            DisplayData()
            cleartextboxes()
            MessageBox.Show("deleted succesfully")
        Else
            MessageBox.Show("fill all fields")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox5.Text IsNot String.Empty Then
            cn.Open()
            adapt = New SqlDataAdapter("Select * from BatteryTable where title like '%' + @search +  '%'
            Or genre Like '%' + @search +  '%' Or Id Like '' + @search +  '%'", cn)
            adapt.SelectCommand.Parameters.Add("@search", SqlDbType.NVarChar).Value = TextBox5.Text
            dt = New DataTable()
            adapt.Fill(dt)
            DataGridView1.DataSource = dt
            cn.Close()
        Else
            MessageBox.Show("fill all fields")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DisplayData()
    End Sub

End Class