Imports MySql.Data.MySqlClient
Imports System.Threading
Public Class Dashboard
    Dim BestsellerDataTable As DataTable
    Dim BestsellerDataAdapter As MySqlDataAdapter
    Dim BestsellerDataReader As MySqlDataReader
    Dim cmdchart As MySqlCommand
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Chart1.Series("Products").Points.Clear()
            'Chart2.Series("Sales").Points.Clear()
            'bestseller()
            Label5.Text = count("report_id", "reports")
            Label7.Text = count("food_id", "foods")
            Label9.Text = sum("total", "reports WHERE DATE(transaction_date) = CURRENT_DATE()")
            Label8.Text = count("user_id", "users")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub bestseller()
        Try

            GetConnected()
            query = "SELECT product_name, product_quantity, SUM(product_total) as TotalSales FROM report_detail GROUP BY product_name ORDER by product_quantity DESC LIMIT 5"
            cmdchart = New MySqlCommand(query, conn)
            BestsellerDataReader = cmdchart.ExecuteReader()
            While BestsellerDataReader.Read
                Chart1.Series("Products").Points.AddXY(BestsellerDataReader.GetString("product_name"), BestsellerDataReader.GetInt32("product_quantity"))
                Chart2.Series("Sales").Points.AddXY(BestsellerDataReader.GetString("product_name"), BestsellerDataReader.GetInt32("product_quantity"))
            End While
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub loadData()
        Try

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class