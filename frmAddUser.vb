Imports MySql.Data.MySqlClient
Public Class frmAddUser
    Dim con As New MySqlConnection("host=localhost;user=root;password=;port=3306;database=db_contactsystem;")


    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        AddUser()
    End Sub





    Public Sub AddUser()
        Dim sql, sql2, sql3 As String
        Dim ds, ds2, ds3 As New DataSet
        Dim da, da2, da3 As MySqlDataAdapter

        If MsgBox("Add " & txtUsername.Text & "?", MsgBoxStyle.YesNo, "Confirm Entry") = MsgBoxResult.Yes Then


            con.Open()

            Sql = "SELECT * FROM tbl_users"
            da = New MySqlDataAdapter(Sql, con)
            da.Fill(ds, "New User")
            con.Close()

            Dim cb As New MySqlCommandBuilder(da)
            Dim dsNewRow As DataRow

            dsNewRow = ds.Tables("New User").NewRow()
            dsNewRow.Item("username") = txtUsername.Text
            dsNewRow.Item("password") = txtPassword.Text


            ds.Tables("New User").Rows.Add(dsNewRow)
            da.Update(ds, "New User")
            con.Close()
            MsgBox("The User '" & txtUsername.Text & "' has been added to the Database.", MsgBoxStyle.Information, txtUsername.Text)

            con.Open()
            sql3 = "SELECT * FROM tbl_users WHERE username='" & txtUsername.Text & "' AND password='" & txtPassword.Text & "'"
            da3 = New MySqlDataAdapter(sql3, con)
            da3.Fill(ds3, "User")
            con.Close()

            Dim user_id = ds3.Tables("User").Rows(0).Item("user_id").ToString

            If ds3.Tables("User").Rows.Count = 99 Then
                Dim dsNewRow2 As DataRow
                con.Open()

                sql2 = "SELECT * FROM tbl_profile"
                da2 = New MySqlDataAdapter(sql2, con)
                da2.Fill(ds2, "Profile")
                dsNewRow2 = ds2.Tables("Profile").NewRow()
                dsNewRow2.Item("user_id") = user_id
                dsNewRow2.Item("profile_fname") = txtfname.Text
                dsNewRow2.Item("profile_lname") = txtlname.Text

                ds2.Tables("Profile").Rows.Add(dsNewRow2)
                da2.Update(ds2, "Profile")
                con.Close()
                Me.Close()

            End If

        Else

        End If
    End Sub
End Class