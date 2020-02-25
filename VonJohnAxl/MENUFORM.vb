Imports System.Windows.Forms

Public Class MENUFORM

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub


    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer

    Private Sub StatusBarToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim newMDIchild As New Reports()
        If Application.OpenForms().OfType(Of Reports).Any Then
        Else
            'btndefaut(defaultcolor:=Button5)
            'btncolor(changecolor:=Button5)
            'formclose(closeform:=ManageProducts)
            newMDIchild.MdiParent = Me
            newMDIchild.ShowIcon = False
            newMDIchild.Show()
        End If
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newMDIchild As New Reports()
        If Application.OpenForms().OfType(Of Reports).Any Then
        Else
            btndefaut(defaultcolor:=Button1)
            btncolor(changecolor:=Button1)
            formclose(closeform:=Reports)
            newMDIchild.MdiParent = Me
            newMDIchild.ShowIcon = False
            newMDIchild.Show()
        End If
    End Sub
    Private Sub MENUFORM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Form3.Enabled = True
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub MENUFORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim newMDIchild As New Dashboard()
        If Application.OpenForms().OfType(Of Dashboard).Any Then
        Else
            btndefaut(defaultcolor:=Button3)
            btncolor(changecolor:=Button3)
            formclose(closeform:=Dashboard)
            newMDIchild.MdiParent = Me
            newMDIchild.ShowIcon = False
            newMDIchild.Show()
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim newMDIchild As New Dashboard()
        If Application.OpenForms().OfType(Of Dashboard).Any Then
        Else
            btndefaut(defaultcolor:=Button3)
            btncolor(changecolor:=Button3)
            formclose(closeform:=Dashboard)
            newMDIchild.MdiParent = Me
            newMDIchild.ShowIcon = False
            newMDIchild.Show()
        End If
    End Sub
    Public Sub btncolor(ByVal changecolor As Button)
        changecolor.ForeColor = Color.Black
        changecolor.BackColor = Color.White
    End Sub

    Public Sub btndefaut(ByVal defaultcolor As Button)
        For Each P As Control In Controls
            If TypeOf P Is Panel Then
                For Each ctrl As Control In P.Controls
                    If TypeOf ctrl Is Button Then
                        If ctrl.Name <> defaultcolor.Name Then
                            CType(ctrl, Button).ForeColor = Color.White
                            CType(ctrl, Button).BackColor = Color.FromArgb(243, 119, 54)
                        End If
                    End If
                Next
            End If
        Next
    End Sub
    Public Sub formclose(ByVal closeform As Form)
        Try
            For Each P As Control In Controls
                For Each ctrl As Control In P.Controls
                    If TypeOf ctrl Is Form Then
                        If ctrl.Name <> closeform.Name Then
                            CType(ctrl, Form).FindForm.Hide()
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
