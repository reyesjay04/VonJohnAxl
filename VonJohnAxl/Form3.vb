Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With DataGridViewOrders
            .Columns.Item(3).DefaultCellStyle.Format = "0.00##"
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
        TextBox2.Text = Format(Now(), "yyyyMMddHHmmss")
        listviewproductsshow()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox3.Text = Val(TextBox1.Text) - Val(TextBoxGRANDTOTAL.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox3.Text < 0 Then
            MsgBox("Insufficient Balance!")
        Else
            transaction()
            MessageBox.Show("Thank you", "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Public Sub transaction()
        Try
            table = "reports"
            fields = "`transaction_number`, `user_id`, `total`, `cash_change`, `active`, `cash`, `transaction_date`"
            values = "('" & TextBox2.Text & "', '" & ClientID & "', " & TextBoxGRANDTOTAL.Text & ", " & TextBox3.Text & "," & 1 & ", " & TextBox1.Text & ", '" & Format(Now(), "yyyy-MM-dd hh:ss:mm") & "')"
            insert(table, fields, values)
            With DataGridViewOrders
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    table = "report_detail"
                    fields = "`transaction_number`, `product_name`, `product_quantity`, `product_price`, `product_total`, `active`"
                    values = "('" & TextBox2.Text & "', '" & .Rows(i).Cells(0).Value & "', " & .Rows(i).Cells(1).Value & ", " & .Rows(i).Cells(2).Value & "," & .Rows(i).Cells(3).Value & ", " & 1 & ")"
                    insert(table, fields, values)
                Next
            End With
            TextBox2.Text = Format(Now(), "yyyyMMddHHmmss")
            DataGridViewOrders.Rows.Clear()
            TextBoxGRANDTOTAL.Text = SumOfColumnsToDecimal(DataGridViewOrders, 3)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Enabled = False
        MENUFORM.Show()
    End Sub
End Class