Imports MySql.Data.MySqlClient
Public Class frmMain
    Public Sub queryDataGrid()
        Try
            Dim con As New MySqlConnection("host=localhost;user=root;password=;port=3306;database=db_contactsystem;")
            con.Open()
            Dim gds As New DataSet
            Dim gdt As New DataTable
            Dim dt_query As String
            dt_query = "SELECT supp_name AS 'Supplier', supp_con1 AS 'Contact No', supp_address AS 'Address'  FROM tbl_suppliers;"
            gds.Tables.Add(gdt)

            Dim gda As New MySqlDataAdapter
            'cbSby.Text = "Title"

            gda = New MySql.Data.MySqlClient.MySqlDataAdapter(dt_query, con)
            gda.Fill(gdt)

            dg_viewContacts.DataSource = gdt.DefaultView

            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        queryDataGrid()
    End Sub
End Class