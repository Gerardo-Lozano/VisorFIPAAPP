Imports System.Data.Odbc
Imports System.Data.SqlClient

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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        connection_sql()

        Try

            'Leer tabla de proveedores

            Dim consulta As String
            Dim lista As String

            'Control de consulta dependiendo el reporte seleccionado como mixto, entradas o salidas

            'consulta = "SELECT * FROM Registros_Filling"
            consulta = "SELECT * FROM Rates WHERE ITEM LIKE '%" & TextBox1.Text & "%' ORDER BY ITEM ASC"
            'MsgBox("el script es:  " & consulta)
            adapt = New Odbc.OdbcDataAdapter(consulta, ConexionSQL.con)
            ConexionSQL.reg = New DataSet
            adapt.Fill(reg, "Tabla1")
            lista = reg.Tables("Tabla1").Rows.Count
            DataGridView2.DataSource = reg.Tables("Tabla1")
        Catch ex As Exception
            MsgBox("No se puede leer información" & ex.Message)

        End Try

        con.Close()

        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If (e.KeyChar = Convert.ToChar(Keys.Enter)) Then
            Button4_Click(sender, e)
        End If
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If (e.RowIndex > -1) Then
            DataGridView2.CurrentRow.Selected = True
            TextBox2.Text = DataGridView2.Rows(e.RowIndex).Cells("ITEM").FormattedValue.ToString()
            TextBox3.Text = DataGridView2.Rows(e.RowIndex).Cells("PRESENTACION").FormattedValue.ToString()
            TextBox4.Text = DataGridView2.Rows(e.RowIndex).Cells("RATE").FormattedValue.ToString()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()

        Button4_Click(sender, e)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If TextBox2.Text.Trim.Length > 0 And TextBox3.Text.Trim.Length > 0 And TextBox4.Text.Trim.Length > 0 Then

            connection_sql()

            Try

                Dim query As String
                Dim Item = TextBox2.Text
                Dim Presetnacion = TextBox3.Text
                Dim Rate = TextBox4.Text


                query = "UPDATE Rates SET
                              ITEM ='" & Item & "',
                              PRESENTACION ='" & Presetnacion & "',
                              RATE ='" & Rate & "'
                              WHERE ITEM = '" & TextBox2.Text & "'"

                Dim comando_actualizar As OdbcCommand
                comando_actualizar = New OdbcCommand(query, ConexionSQL.con)
                comando_actualizar.ExecuteNonQuery()

                Button4_Click(sender, e)

                MsgBox("Se han grabado exitosamente los registros")

            Catch ex As Exception
                MsgBox("Asegurese que el Item existe para poder actualziarlo")
            End Try

        Else
            MsgBox("Asegurese de llenar todos los campos")
        End If

        con.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox2.Text.Trim.Length > 0 And TextBox3.Text.Trim.Length > 0 And TextBox4.Text.Trim.Length > 0 Then

            connection_sql()

            Try

                Dim query As String
                Dim Item = TextBox2.Text
                Dim Presetnacion = TextBox3.Text
                Dim Rate = TextBox4.Text


                query = "INSERT INTO Rates (ITEM, PRESENTACION, RATE) VALUES ('" & Item & "','" & Presetnacion & "','" & Rate & "')"

                Dim comando_actualizar As OdbcCommand
                comando_actualizar = New OdbcCommand(query, ConexionSQL.con)
                comando_actualizar.ExecuteNonQuery()

                Button4_Click(sender, e)

                MsgBox("Se ha agregado el item " & Item & " exitosamente")
            Catch ex As Exception
                MsgBox("Surgio un problema, asegurese que el Item no exista ya, si el problema persiste, contacte al administrador")
            End Try

        Else
            MsgBox("Asegurese de llenar todos los campos")
        End If

        con.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        'Pregunta si quiere finalizar el proceso
        Dim strMsg1 As String
        Dim iResponse1 As Integer

        ' Texto en el cuadro de pregunta 
        strMsg1 = "Esta seguro que desea eliminar este registro?" & Chr(10)
        strMsg1 = strMsg1 & "Si esta seguro haga clic en SI de lo contrario haga clic en NO."

        ' Mensaje de pregutna. 
        iResponse1 = MsgBox(strMsg1, vbQuestion + vbYesNo, "ELIMINAR ITEM")

        ' Checar respuesta 
        If iResponse1 = vbNo Then
            Return
        End If

        If TextBox2.Text.Trim.Length > 0 And TextBox3.Text.Trim.Length > 0 And TextBox4.Text.Trim.Length > 0 Then

            connection_sql()

            Try

                Dim query As String
                Dim Item = TextBox2.Text
                Dim Presetnacion = TextBox3.Text
                Dim Rate = TextBox4.Text


                query = "DELETE FROM Rates WHERE ITEM ='" & TextBox2.Text & "'"

                Dim comando_actualizar As OdbcCommand
                comando_actualizar = New OdbcCommand(query, ConexionSQL.con)
                comando_actualizar.ExecuteNonQuery()

                Button4_Click(sender, e)

                MsgBox("Se ha eliminado exitosamente el registro")

            Catch ex As Exception
                MsgBox("Asegurese de seleccionar un Item a eliminar y que los campos esten llenos")
            End Try

        Else
            MsgBox("Seleccione un Item a eliminar")
        End If

        con.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Conexion_SQL_Retention()

        Try

            'Obtenemos hora y fecha
            Dim hora As String
            Dim fecha As String
            hora = Now.ToString("HH:mm:ss")
            fecha = Now.ToString("MM/dd/yyyy")

            'Leer tabla de proveedores

            Dim consulta_batch As String
            Dim lista_batch As Byte

            consulta_batch = "SELECT batch, analyst, status, approve_date, approve_hour, DATEDIFF(HOUR , CONVERT(DATETIME, approve_date) + CONVERT(DATETIME, approve_hour), GETDATE()) as [hour_lapse] FROM batch_route  WHERE status = 0"

            adaptador_a_sql_Muestras = New SqlDataAdapter(consulta_batch, Conexion_SQL_Altea.ConexionSQL_Muestras)
            Conexion_SQL_Altea.registro_a_sql_Muestras = New DataSet
            adaptador_a_sql_Muestras.Fill(registro_a_sql_Muestras, "Tabla1")
            lista_batch = registro_a_sql_Muestras.Tables("Tabla1").Rows.Count
            DataGridView1.DataSource = registro_a_sql_Muestras.Tables("Tabla1")
        Catch

        End Try

        'con.Close()

        Button2.Visible = True
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Conexion_SQL_Retention()

        Try

            'Obtenemos hora y fecha
            Dim hora As String
            Dim fecha As String
            hora = Now.ToString("HH:mm:ss")
            fecha = Now.ToString("MM/dd/yyyy")

            'Leer tabla de proveedores

            Dim consulta_batch As String
            Dim lista_batch As Byte

            consulta_batch = "SELECT * FROM [AlteaDB].[dbo].[batch_route]"

            adaptador_a_sql_Muestras = New SqlDataAdapter(consulta_batch, Conexion_SQL_Altea.ConexionSQL_Muestras)
            Conexion_SQL_Altea.registro_a_sql_Muestras = New DataSet
            adaptador_a_sql_Muestras.Fill(registro_a_sql_Muestras, "Tabla1")
            lista_batch = registro_a_sql_Muestras.Tables("Tabla1").Rows.Count
            DataGridView1.DataSource = registro_a_sql_Muestras.Tables("Tabla1")
        Catch

        End Try

        'con.Close()

        Button2.Visible = True
    End Sub
End Class
