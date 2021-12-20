Public Class Form2
    Dim count = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        count += 1
        If Me.Opacity < 1 Then
            Me.Opacity += 0.05

        End If
        If count = 100 Then
            Timer1.Stop()
            Timer2.Start()
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        'Form1.Show()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        Me.Opacity -= 0.05
        If Me.Opacity = 0 Then
            Form1.Show()
            Me.Close()
            Timer2.Stop()
        End If

    End Sub
End Class