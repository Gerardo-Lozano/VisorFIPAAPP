Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connection_sql()

        Try
            'Variables para el rango de fechas
            Dim Fecha_1 As String
            Dim Fecha_2 As String

            Fecha_1 = DateTimePicker3.Text
            Fecha_2 = DateTimePicker4.Text

            'Leer tabla de proveedores

            Dim consulta As String
            Dim lista As String

            'Control de consulta dependiendo el reporte seleccionado como mixto, entradas o salidas

            'consulta = "SELECT * FROM Registros_Filling"
            consulta = "SELECT * FROM Registros_Filling WHERE fecha between '" & Fecha_1 & "' and '" & Fecha_2 & "' ORDER BY WO ASC"
            'MsgBox("el script es:  " & consulta)
            adapt = New Odbc.OdbcDataAdapter(consulta, ConexionSQL.con)
            ConexionSQL.reg = New DataSet
            adapt.Fill(reg, "Tabla1")
            lista = reg.Tables("Tabla1").Rows.Count
            DataGridView1.DataSource = reg.Tables("Tabla1")
        Catch ex As Exception
            MsgBox("No se puede leer información" & ex.Message)

        End Try

        con.Close()

        Button2.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        llenarExcel(DataGridView1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        connection_sql()

        Try

            'Leer tabla de proveedores

            Dim consulta As String
            Dim lista As String

            'Control de consulta dependiendo el reporte seleccionado como mixto, entradas o salidas

            'consulta = "SELECT * FROM Registros_Filling"
            consulta = "SELECT * FROM Registros_Filling ORDER BY WO ASC"
            'MsgBox("el script es:  " & consulta)
            adapt = New Odbc.OdbcDataAdapter(consulta, ConexionSQL.con)
            ConexionSQL.reg = New DataSet
            adapt.Fill(reg, "Tabla1")
            lista = reg.Tables("Tabla1").Rows.Count
            DataGridView1.DataSource = reg.Tables("Tabla1")
        Catch ex As Exception
            MsgBox("No se puede leer información" & ex.Message)

        End Try

        con.Close()

        Button2.Visible = True
    End Sub
End Class
