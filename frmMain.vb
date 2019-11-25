Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Public Class frmMain
    Dim sIndex, supplierID As Integer


    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        queryDataGrid()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If Not Regex.Match(txtSearch.Text, "^[A-Za-z ]*$", RegexOptions.IgnoreCase).Success Then
            txtSearch.Text = txtSearch.Text.Remove(txtSearch.TextLength - 1, 1)
        Else
            queryDataGrid()

        End If
    End Sub

    Private Sub dg_viewContacts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_viewContacts.CellContentClick
        sIndex = dg_viewContacts.CurrentRow.Index
        supplierID = dg_viewContacts.Item("ID", sIndex).Value
        Dim ds_populate As New DataSet
        Dim da_populate As MySqlDataAdapter
        Dim con As New MySqlConnection("host=localhost;user=root;password=;port=3306;database=db_contactsystem;")

        Try
            con.Open()

            Dim sql_populate As String
            sql_populate = "SELECT * FROM tbl_suppliers WHERE supp_ID ='" & supplierID & "';"
            da_populate = New MySqlDataAdapter(sql_populate, con)
            da_populate.Fill(ds_populate, "SupplierInfo")

            'Loads Supplier Information
            txtSupName.Text = ds_populate.Tables("SupplierInfo").Rows(0).Item("supp_name").ToString
            txtSupContact1.Text = ds_populate.Tables("SupplierInfo").Rows(0).Item("supp_con1").ToString
            txtSupContact2.Text = ds_populate.Tables("SupplierInfo").Rows(0).Item("supp_con2").ToString
            txtSupContact3.Text = ds_populate.Tables("SupplierInfo").Rows(0).Item("supp_con3").ToString
            txtSupAddress.Text = ds_populate.Tables("SupplierInfo").Rows(0).Item("supp_address").ToString

            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class