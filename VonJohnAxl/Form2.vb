Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim table As String = "users WHERE username = '" & TextBox1.Text & "' AND password = '" & TextBox2.Text & "' "
        Dim flds As String = "*"
        Login(flds, table)
        If accountexist = True Then
            MessageBox.Show("Successfully Login!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClientUsername = dt(0)(1)
            ClientPassword = dt(0)(2)
            ClientFullname = dt(0)(3)
            ClientEmail = dt(0)(4)
            ClientContact = dt(0)(5)
            ClientRole = dt(0)(6)
            ClientID = dt(0)(0)
            Form3.Show()
            Close()
        Else
            MessageBox.Show("Account doesn't exist!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub
End Class