Imports MySql.Data.MySqlClient
Imports System.IO
Module Functions
    Public query As String
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public dr As MySqlDataReader
    Public dt As DataTable
    Public conn As MySqlConnection
    Public hasrow As Integer
    Public accountexist As Boolean
    'Account Info
    Public ClientUsername As String
    Public ClientPassword As String
    Public ClientFullname As String
    Public ClientEmail As String
    Public ClientContact As String
    Public ClientRole As String
    Public ClientID As String
    'For Query
    Public table As String
    Public fields As String
    Public values As String
    Public Sub GetConnected()
        Try
            conn = New MySqlConnection
            conn.ConnectionString = "server=localhost;user id=root;password=;database=karenderya;port=3306;Allow Zero Datetime=True"
            conn.Open()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub Login(ByVal flds, ByVal tbl)
        Try
            GetConnected()
            query = "SELECT " & flds & " FROM " & tbl
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader()
            If dr.Read = True Then
                accountexist = True
                dr.Dispose()
                da = New MySqlDataAdapter(cmd)
                dt = New DataTable
                da.Fill(dt)
            Else
                accountexist = False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub listviewproductsshow()
        Try
            GetConnected()
            cmd = New MySqlCommand("SELECT `food_id` , `food_name`, `quantity`, `image`, `critical_stock` , `price` FROM foods", conn)
            With Form3
                .FlowLayoutPanel1.Controls.Clear()
                da = New MySqlDataAdapter(cmd)
                dt = New DataTable()
                da.Fill(dt)
                For Each row As DataRow In dt.Rows
                    Dim new_Button_product As New Button
                    Dim buttonname As String = row("food_name")
                    Dim newlabel As New Label
                    Dim productprice = row("price")
                    Dim productID = row("food_id")
                    With new_Button_product
                        .ForeColor = Color.White
                        .Name = buttonname
                        .Text = productprice
                        .TextImageRelation = TextImageRelation.ImageBeforeText
                        .TextAlign = ContentAlignment.TopLeft
                        .Font = New Font("Century Gothic", 11, FontStyle.Bold)
                        .BackgroundImage = Base64ToImage(row("image"))
                        .FlatStyle = FlatStyle.Flat
                        .FlatAppearance.BorderSize = 0
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Width = 166.5
                        .Height = 150
                        .Cursor = Cursors.Hand
                        With newlabel
                            .Text = buttonname
                            .Font = New Font("Century Gothic", 9, FontStyle.Bold)
                            .ForeColor = Color.White
                            .Width = 166.5
                            .Location = New Point(0, 127)
                            .BackColor = Color.SlateGray
                            .Parent = new_Button_product
                            .TextAlign = ContentAlignment.TopCenter
                        End With
                        .Controls.Add(newlabel)
                    End With
                    .FlowLayoutPanel1.Controls.Add(new_Button_product)
                    AddHandler new_Button_product.Click, AddressOf new_product_button_click
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            conn.Close()
            cmd.Dispose()
        End Try
    End Sub
    Dim total
    Public Function SumOfColumnsToDecimal(ByVal datagrid As DataGridView, ByVal celltocompute As Integer)
        With datagrid
            total = (From row As DataGridViewRow In .Rows
                     Where row.Cells(celltocompute).FormattedValue.ToString() <> String.Empty
                     Select Convert.ToDecimal(row.Cells(celltocompute).FormattedValue)).Sum.ToString("0.00")
        End With
        Return total
    End Function
    Public Sub new_product_button_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim price As String = btn.Text
        Dim name As String = btn.Name
        With Form3
            .TextBoxName.Text = name
            retrieveanddeduct(name:=name, price:=price)
            .TextBoxGRANDTOTAL.Text = SumOfColumnsToDecimal(.DataGridViewOrders, 3)
        End With
    End Sub
    Public Function ImageToBase64(ByVal image As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()
            ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function
    'TEXT TO IMAGE
    Public Function Base64ToImage(ByVal base64String As String) As Image
        ' Convert Base64 String to byte[]
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)
        ' Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length)
        Dim ConvertedBase64Image As Image = Image.FromStream(ms, True)
        Return ConvertedBase64Image
    End Function
    Public Sub retrieveanddeduct(ByVal name, ByVal price)
        Try
            With Form3
                Dim test As Boolean = False
                For Each row In .DataGridViewOrders.Rows
                    If .TextBoxName.Text = row.Cells("Column1").Value Then
                        test = True
                        Exit For
                    End If
                Next
                If test = False Then
                    .DataGridViewOrders.Rows.Add(name, 1, price, price)
                Else
                    For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                        If .TextBoxName.Text = .DataGridViewOrders.Rows(i).Cells(0).Value Then
                            .DataGridViewOrders.Rows(i).Cells(1).Value = .DataGridViewOrders.Rows(i).Cells(1).Value + 1
                            .DataGridViewOrders.Rows(i).Cells(3).Value = .DataGridViewOrders.Rows(i).Cells(1).Value * .DataGridViewOrders.Rows(i).Cells(2).Value
                        End If
                    Next
                End If
            End With
        Catch ex As Exception
            conn.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub insert(ByVal tbl, ByVal flds, ByVal value)
        Try
            GetConnected()
            query = "INSERT INTO " & tbl & " (" & flds & ") VALUES " & value
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Module
