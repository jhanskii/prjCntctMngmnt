﻿Imports MySql.Data.MySqlClient

Public Class frmLogin
    Dim count As Integer = 0

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'Attempt Counter
        If count < 3 Then
            If txtUsername.Text = String.Empty And txtPassword.Text = String.Empty Then
                count += 1
            Else
                'Login Process
                Dim ds As New DataSet
                Dim da As MySqlDataAdapter
                Dim sql As String
                Dim con As New MySqlConnection("host=localhost;user=root;password=;port=3306;database=db_contactsystem;")


                Try
                    con.Open()
                    sql = "SELECT * FROM tbl_users WHERE username='" & txtUsername.Text & "' AND password='" & txtPassword.Text & "'"
                    da = New MySqlDataAdapter(sql, con)
                    da.Fill(ds, "Login")
                    con.Close()

                    If ds.Tables("Login").Rows.Count = 1 Then
                        If ds.Tables("Login").Rows(0).Item("level") = "1" Then

                            frmMain.ToolStripStatusLabel1.Text = "Logged As Admin"
                            user_id_key = ds.Tables("Login").Rows(0).Item("user_id").ToString
                            If ds.Tables("Login").Rows(0).Item("level") = "1" Then
                                is_admin = True
                            End If
                            frmMain.Show()
                            Me.Hide()

                        End If
                        count += 1
                    Else
                        ts_lblStatus.Text = "Remaining attempts " & 3 - count & vbCrLf & "Please check your username/password."
                    End If

                Catch ex As Exception

                    MsgBox(ex.Message)
                    con.Close()

                End Try

            End If
            count = count + 1

        Else
            ts_lblStatus.Text = "Failed to Authenticate, now closing"
            Me.Close()


        End If
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUsername.Select()
    End Sub
End Class
