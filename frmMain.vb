Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Public Class frmMain
    Dim sIndex, supplierID As Integer
    Dim con As New MySqlConnection("host=localhost;user=root;password=;port=3306;database=db_contactsystem;")
    Dim int_button As Integer
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        queryDataGrid()
        frmLogin.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If Not Regex.Match(txtSearch.Text, "^[A-Za-z-0-9 ]*$", RegexOptions.IgnoreCase).Success Then
            txtSearch.Text = txtSearch.Text.Remove(txtSearch.TextLength - 1, 1)
        Else
            queryDataGrid()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        EntrySubmit()
        int_button = 0
    End Sub

    Private Sub dg_viewContacts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_viewContacts.CellClick
        SelectionChangeQuery()
        btnDelete.Enabled = True
    End Sub



    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        txtSupName.ReadOnly = False
        txtSupContact1.ReadOnly = False
        txtSupContact2.ReadOnly = False
        txtSupContact3.ReadOnly = False
        txtSupAddress.ReadOnly = False
        btnUpdate.Enabled = True
        btnEdit.Enabled = False
        btnAddSupp.Enabled = False
        btnDelete.Enabled = False
        int_button = 0
    End Sub

    'Update Function
    Private Sub EntrySubmit()
        If MsgBox("Update this record?", MsgBoxStyle.YesNo, "Confirm Entry") = MsgBoxResult.Yes Then

            Dim ds As New DataSet
            Dim da As MySqlDataAdapter
            Dim sql As String

            con.Open()

            sql = "SELECT * FROM tbl_suppliers WHERE supp_ID=" & supplierID
            da = New MySqlDataAdapter(sql, con)
            da.Fill(ds, "Update Supplier")
            con.Close()

            Dim cb As New MySqlCommandBuilder(da)


            ds.Tables("Update Supplier").Rows(0).Item("supp_name") = txtSupName.Text
            ds.Tables("Update Supplier").Rows(0).Item("supp_con1") = txtSupContact1.Text
            ds.Tables("Update Supplier").Rows(0).Item("supp_con2") = txtSupContact2.Text
            ds.Tables("Update Supplier").Rows(0).Item("supp_con3") = txtSupContact3.Text
            ds.Tables("Update Supplier").Rows(0).Item("supp_address") = txtSupAddress.Text


            da.Update(ds, "Update Supplier")

            MsgBox("The Supplier Information of '" & txtSupName.Text & "' has been updated to the Database.", MsgBoxStyle.Information)

            txtSupName.ReadOnly = True
            txtSupContact1.ReadOnly = True
            txtSupContact2.ReadOnly = True
            txtSupContact3.ReadOnly = True
            txtSupAddress.ReadOnly = True

            btnEdit.Enabled = False
            btnUpdate.Enabled = False
            btnDelete.Enabled = False

            queryDataGrid()
            LoadDetails()
            btnAddSupp.Enabled = True
        End If
    End Sub

    'Info Load When Cell is Selected

    Public Sub LoadDetails()
        sIndex = dg_viewContacts.CurrentRow.Index
        supplierID = dg_viewContacts.Item("ID", sIndex).Value
        Dim ds_populate As New DataSet
        Dim da_populate As MySqlDataAdapter
        btnEdit.Enabled = True

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

    Private Sub btnAddSupp_Click(sender As Object, e As EventArgs) Handles btnAddSupp.Click

        If int_button = 0 Then
            txtSupName.ReadOnly = False
            txtSupContact1.ReadOnly = False
            txtSupContact2.ReadOnly = False
            txtSupContact3.ReadOnly = False
            txtSupAddress.ReadOnly = False

            txtSupName.Text = ""
            txtSupContact1.Text = ""
            txtSupContact2.Text = ""
            txtSupContact3.Text = ""
            txtSupAddress.Text = ""

            btnEdit.Enabled = False
            btnUpdate.Enabled = False
            btnDelete.Enabled = False
            int_button = 1

            dg_viewContacts.Enabled = False
        Else
            AddRecord()
            dg_viewContacts.Enabled = True
            queryDataGrid()
            int_button = 0
        End If



    End Sub

    'ADD RECORD
    Public Sub AddRecord()
        If MsgBox("Add " & txtSupName.Text & "?", MsgBoxStyle.YesNo, "Confirm Entry") = MsgBoxResult.Yes Then

            Dim ds As New DataSet
            Dim da As MySqlDataAdapter
            Dim sql As String

            con.Open()

            sql = "SELECT * FROM tbl_suppliers"
            da = New MySqlDataAdapter(sql, con)
            da.Fill(ds, "New Supplier")
            con.Close()

            Dim cb As New MySqlCommandBuilder(da)
            Dim dsNewRow As DataRow

            dsNewRow = ds.Tables("New Supplier").NewRow()
            dsNewRow.Item("supp_name") = txtSupName.Text
            dsNewRow.Item("supp_con1") = txtSupContact1.Text
            dsNewRow.Item("supp_con2") = txtSupContact2.Text
            dsNewRow.Item("supp_con3") = txtSupContact3.Text
            dsNewRow.Item("supp_address") = txtSupAddress.Text

            ds.Tables("New Supplier").Rows.Add(dsNewRow)
            da.Update(ds, "New Supplier")

            MsgBox("The Supplier '" & txtSupName.Text & "' has been added to the Database.", MsgBoxStyle.Information, txtSupName.Text)

            txtSupName.ReadOnly = True
            txtSupContact1.ReadOnly = True
            txtSupContact2.ReadOnly = True
            txtSupContact3.ReadOnly = True
            txtSupAddress.ReadOnly = True

            int_button = 0
        Else
            int_button = 1
        End If
    End Sub

    Public Sub SelectionChangeQuery()
        LoadDetails()
        txtSupName.ReadOnly = True
        txtSupContact1.ReadOnly = True
        txtSupContact2.ReadOnly = True
        txtSupContact3.ReadOnly = True
        txtSupAddress.ReadOnly = True
        int_button = 0
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteRecord()
    End Sub

    'DeleteRecord
    Public Sub DeleteRecord()
        supplierID = dg_viewContacts.Item("ID", sIndex).Value
        If MsgBox("Delete " & txtSupName.Text & "?", MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
            Dim sql_str As String
            sql_str = "DELETE FROM tbl_suppliers WHERE supp_ID='" & supplierID & "'"
            Dim SQLcomm As MySqlCommand = New MySqlCommand(sql_str, con)

            con.Open()
            SQLcomm.ExecuteNonQuery()
            con.Close()

            MsgBox("Record Deleted Sucessfully", MsgBoxStyle.Information)
            queryDataGrid()
            int_button = 0
            btnDelete.Enabled = False
        Else
            int_button = 1
        End If
    End Sub

End Class