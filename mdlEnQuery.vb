Imports MySql.Data.MySqlClient
Module mdlEnQuery
    Dim con As New MySqlConnection("host=localhost;user=root;password=;port=3306;database=db_contactsystem;")
    Public Sub queryDataGrid()
        Try

            con.Open()
            Dim gds As New DataSet
            Dim gdt As New DataTable
            Dim dt_query As String
            dt_query = "SELECT supp_ID AS 'ID', supp_name AS 'Supplier', supp_con1 AS 'Contact No', supp_address AS 'Address'  FROM tbl_suppliers;"
            gds.Tables.Add(gdt)

            Dim gda As New MySqlDataAdapter
            'cbSby.Text = "Title"

            gda = New MySql.Data.MySqlClient.MySqlDataAdapter(dt_query, con)
            gda.Fill(gdt)


            Dim dv As New DataView(gds.Tables(0))
            dv.RowFilter = "Supplier LIKE '%" & frmMain.txtSearch.Text & "%'"

            frmMain.dg_viewContacts.DataSource = dv
            'dg_viewContacts.DataSource = gdt.DefaultView

            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'This Function is to populate the data when the Data Grid is Selected

    Dim ds_populate As New DataSet
    Dim da_populate As MySqlDataAdapter
    Public Sub SelectDataPopulate(ByVal suppID As Integer)
        Try
            con.Open()

            Dim sql_populate As String
            sql_populate = "SELECT * FROM tbl_suppliers WHERE supp_ID ='" & suppID & "';"
            da_populate = New MySqlDataAdapter(sql_populate, con)
            da_populate.Fill(ds_populate, "SupplierInfo")

            'Loads Supplier Information
            frmMain.txtSupName.Text = ds_populate.Tables("SupplierInfo").Rows(0).Item("supp_name")

            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module
