Imports MySql.Data.MySqlClient
Public Class frmManageUsers
    Dim con As New MySqlConnection("host=localhost;user=root;password=;port=3306;database=db_contactsystem;")
    Dim sIndex, IntUserID As Integer

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        AddUser()
    End Sub




    Public Sub AddUser()
        Dim sql As String
        Dim ds As New DataSet
        Dim da As MySqlDataAdapter

        If MsgBox("Add " & txtUsername.Text & "?", MsgBoxStyle.YesNo, "Confirm Entry") = MsgBoxResult.Yes Then


            con.Open()

            Sql = "SELECT * FROM tbl_users"
            da = New MySqlDataAdapter(Sql, con)
            da.Fill(ds, "New User")
            con.Close()


            Dim dsNewRow As DataRow

            dsNewRow = ds.Tables("New User").NewRow()
            dsNewRow.Item("username") = txtUsername.Text
            dsNewRow.Item("password") = txtPassword.Text


            ds.Tables("New User").Rows.Add(dsNewRow)
            da.Update(ds, "New User")
            con.Close()
            MsgBox("The User '" & txtUsername.Text & "' has been added to the Database.", MsgBoxStyle.Information, txtUsername.Text)


        Else

        End If
    End Sub

    Public Sub SelectionChangeQuery()
        LoadDetails()
    End Sub

    'LOAD USER DETALS
    Public Sub LoadDetails()
        sIndex = dg_viewUsers.CurrentRow.Index
        IntUserID = dg_viewUsers.Item("UID", sIndex).Value

        btnEdit.Enabled = True

        Dim ds_populate As New DataSet
        Dim da_populate As MySqlDataAdapter

        Try
            con.Open()

            Dim sql_populate As String
            sql_populate = "SELECT * FROM tbl_users WHERE user_id ='" & IntUserID & "';"
            da_populate = New MySqlDataAdapter(sql_populate, con)
            da_populate.Fill(ds_populate, "UserInfo")

            'Loads Supplier Information
            txtUsername.Text = ds_populate.Tables("UserInfo").Rows(0).Item("username").ToString
            txtPassword.Text = ds_populate.Tables("UserInfo").Rows(0).Item("password").ToString
            txtPassword2.Text = ds_populate.Tables("UserInfo").Rows(0).Item("password").ToString
            txtfname.Text = ds_populate.Tables("UserInfo").Rows(0).Item("fname").ToString
            txtlname.Text = ds_populate.Tables("UserInfo").Rows(0).Item("lname").ToString


            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dg_viewUsers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_viewUsers.CellContentClick
        SelectionChangeQuery()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        btnUpdate.Enabled = True
        btnSubmit.Enabled = False

    End Sub

    Private Sub frmManageUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        queryUserDataGrid()
    End Sub





End Class