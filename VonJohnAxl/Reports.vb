Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Public Class Reports
    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.TabPages(0).Text = "Daily Transactions"
        TabControl1.TabPages(1).Text = "Users"
        loadReports()
        loadUsers()
        Label2.Text = SumOfColumnsToDecimal(DataGridView2, 4)
    End Sub

    Private Sub loadReports()
        Try
            GetConnected()
            query = "SELECT * FROM reports WHERE DATE(transaction_date) = CURRENT_DATE()"
            da = New MySqlDataAdapter(query, conn)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
            da.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    Private Sub loadUsers()
        Try
            GetConnected()
            query = "SELECT * FROM users "
            da = New MySqlDataAdapter(query, conn)
            dt = New DataTable
            da.Fill(dt)
            DataGridView3.DataSource = dt
            conn.Close()
            da.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Label2.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString
        Dim TransactionNumber = DataGridView1.SelectedRows(0).Cells(1).Value
        Try
            GetConnected()
            query = "SELECT * FROM report_detail WHERE transaction_number = '" & TransactionNumber & "'"
            da = New MySqlDataAdapter(query, conn)
            dt = New DataTable
            da.Fill(dt)
            DataGridView2.DataSource = dt
            conn.Close()
            da.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private WithEvents printdoc As PrintDocument = New PrintDocument
    Private PrintPreviewDialog1 As New PrintPreviewDialog
    Dim a
    Dim b
    Dim total
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If DataGridView1.Rows.Count > 0 Then
            Label2.Text = SumOfColumnsToDecimal(DataGridView2, 4)
            a = 115
            total = Label2.Text
            b = 0
            Try
                For i As Integer = 0 To DataGridView1.Rows.Count - 1 Step +1
                    printdoc.DefaultPageSettings.PaperSize = New PaperSize("Custom", 200, 400 + b)
                    b += 10
                Next
                PrintPreviewDialog1.Document = printdoc
                PrintPreviewDialog1.ShowDialog()
            Catch exp As Exception
                MessageBox.Show("An error occurred while trying to load the " &
                    "document for Print Preview. Make sure you currently have " &
                    "access to a printer. A printer must be localconnected and " &
                    "accessible for Print Preview to work.", Me.Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MsgBox("Select Transaction First!")
        End If
    End Sub
    Private Sub pdoc_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printdoc.PrintPage
        a = 0
        Dim font As New Font("Bahnschrift Regular", 7)
        Dim font1 As New Font("Bahnschrift  Regular", 7)
        Dim font2 As New Font("Bahnschrift Regular", 9)
        Dim font3 As New Font("Bahnschrift Regular", 12)
        Dim brandfont As New Font("Bahnschrift Regular", 8)
        '    e.Graphics.DrawRectangle(Pens.Black, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height)
        Dim shopnameX As Integer = 10, shopnameY As Integer = 20
        Dim StrRight As New StringFormat()
        'Receipt Header

        Dim sngCenterPagebrand As Single
        sngCenterPagebrand = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Anna's karinderya", brandfont).Width / 2)
        e.Graphics.DrawString("Anna's karinderya", brandfont, Brushes.Black, sngCenterPagebrand, 10)

        Dim sngCenterPageVatReg As Single
        sngCenterPageVatReg = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN " & "0000-0000-0000-0000", brandfont).Width / 2)
        e.Graphics.DrawString("VAT REG TIN " & "0000-0000-0000-0000", font, Brushes.Black, sngCenterPageVatReg, 21)

        Dim sngCenterPageaddbrgy As Single
        sngCenterPageaddbrgy = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Sanpablo Street" & " Brgy." & "Tutuban", brandfont).Width / 2)
        e.Graphics.DrawString("Sanpablo Street" & " Brgy." & "Tutuban", font, Brushes.Black, sngCenterPageaddbrgy, 31)

        Dim sngCenterPagemun As Single
        sngCenterPagemun = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Bulacan" & ", " & "Pandi", brandfont).Width / 2)
        e.Graphics.DrawString("Bulacan" & ", " & "Pandi", font, Brushes.Black, sngCenterPagemun, 41)

        Dim sngCenterPagetel As Single
        sngCenterPagetel = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("044-718-1227", brandfont).Width / 2)
        e.Graphics.DrawString("044-718-1227", font, Brushes.Black, sngCenterPagetel, 51)

        e.Graphics.DrawString("Name: ", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + 50))
        e.Graphics.DrawLine(Pens.Black, 37, 77, 180, 77)
        e.Graphics.DrawString("Tin:", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + 60))
        e.Graphics.DrawLine(Pens.Black, 25, 87, 180, 87)
        e.Graphics.DrawString("Address:", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + 70))
        e.Graphics.DrawLine(Pens.Black, 46, 97, 180, 97)
        e.Graphics.DrawString("Business Style:", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + 80))
        e.Graphics.DrawLine(Pens.Black, 70, 107, 180, 107)
        'Items
        Dim format1st As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
        Dim abc As Integer = 0
        For i As Integer = 0 To DataGridView2.Rows.Count - 1 Step +1
            Dim rect1st As RectangleF = New RectangleF(10.0F, 115 + abc, 173.0F, 100.0F)
            Dim price = Convert.ToDecimal(DataGridView2.Rows(i).Cells(4).FormattedValue).ToString("0.00")
            '=========================================================================================================================================================
            e.Graphics.DrawString(DataGridView2.Rows(i).Cells(2).Value & " ", font, Brushes.Black, rect1st)
            e.Graphics.DrawString(price, font, Brushes.Black, rect1st, format1st)
            a += 10
            abc += 10
            '=========================================================================================================================================================
        Next
        a += 120
        Dim format As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
        Dim rect3 As RectangleF = New RectangleF(10.0F, a, 173.0F, 100.0F)
        e.Graphics.DrawString("AMOUNT DUE:", font2, Brushes.Black, rect3)
        e.Graphics.DrawString("P" & total, font2, Brushes.Black, rect3, format)
        'Cash
        Dim aNumber As Double = DataGridView1.SelectedRows(0).Cells(5).Value
        Dim cash = String.Format("{0:n2}", aNumber)
        Dim rect4 As RectangleF = New RectangleF(10.0F, a + 15, 173.0F, 100.0F)
        e.Graphics.DrawString("CASH:", font2, Brushes.Black, rect4)
        e.Graphics.DrawString("P" & cash, font2, Brushes.Black, rect4, format)
        'Change
        Dim aNumber1 As Double = DataGridView1.SelectedRows(0).Cells(6).Value
        Dim change = String.Format("{0:n2}", aNumber1)
        Dim rect5 As RectangleF = New RectangleF(10.0F, a + 25, 173.0F, 100.0F)
        e.Graphics.DrawString("CHANGE:", font2, Brushes.Black, rect5)
        e.Graphics.DrawString("P" & change, font2, Brushes.Black, rect5, format)
        'Vatable
        Dim vatable = Math.Round(total / 1.12, 2)
        e.Graphics.DrawString("**************************************************", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 27))
        Dim rect6 As RectangleF = New RectangleF(10.0F, a + 52, 173.0F, 100.0F)
        e.Graphics.DrawString("     Vatable", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 32))
        e.Graphics.DrawString("    " & vatable, font1, Brushes.Black, rect6, format)
        'Vat Exempt Sales
        Dim rect7 As RectangleF = New RectangleF(10.0F, a + 62, 173.0F, 100.0F)
        e.Graphics.DrawString("     Vat Exempt Sales", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 42))
        e.Graphics.DrawString("    " & "0.00", font1, Brushes.Black, rect7, format)
        'Zero Rated Sales
        Dim rect8 As RectangleF = New RectangleF(10.0F, a + 72, 173.0F, 100.0F)
        e.Graphics.DrawString("     Zero Rated Sales", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 52))
        e.Graphics.DrawString("    " & "0.00", font1, Brushes.Black, rect8, format)
        'VAT
        Dim rect9 As RectangleF = New RectangleF(10.0F, a + 82, 173.0F, 100.0F)
        e.Graphics.DrawString("     VAT", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 62))
        e.Graphics.DrawString("    " & Math.Round(total - vatable, 2), font1, Brushes.Black, rect9, format)
        'Total
        Dim rect10 As RectangleF = New RectangleF(10.0F, a + 92, 173.0F, 100.0F)
        e.Graphics.DrawString("     Total", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 72))
        e.Graphics.DrawString("    " & total, font1, Brushes.Black, rect10, format)
        'INFO
        e.Graphics.DrawString("**************************************************", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 82))
        e.Graphics.DrawString("Transaction Type: " & "Dine-in", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 90))
        e.Graphics.DrawString("Total Item(s): " & DataGridView1.Rows.Count, font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 100))
        e.Graphics.DrawString("Store No: " & "1", font, Brushes.Black, New PointF(shopnameX + 110, shopnameY + a + 100))
        '=========================================================================================================================================================
        e.Graphics.DrawString("Cashier: " & ClientFullname, font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 110))
        '=========================================================================================================================================================
        e.Graphics.DrawString("Date & Time: " & DataGridView1.SelectedRows(0).Cells(2).Value.ToString , font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 120))
        e.Graphics.DrawString("Trans ID: " & DataGridView1.SelectedRows(0).Cells(1).Value, font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 130))
        e.Graphics.DrawString("This serves as your Sales Invoice", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 140))
        e.Graphics.DrawString("SI No:", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 150))
        e.Graphics.DrawString("**************************************************", font, Brushes.Black, New PointF(shopnameX + 0, shopnameY + a + 160))


    End Sub
End Class