﻿Imports VDM
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Public Class VDM_BL

    Public ConnectionString As String = ConnectionStrings("ConnectionString").ConnectionString
    Public LogConnectionString As String = ConnectionStrings("LogConnectionString").ConnectionString
    Public ServerMapPath As String = AppSettings("ServerMapPath").ToString
    Public PicturePath As String = AppSettings("PicturePath").ToString
    Public Product_Critical_Percent As Integer = 30 '----------- Level ที่สีแดงใน Product Stock -----------
    Public SIM_Critical_Percent As Integer = 30 '----------- Level ที่สีแดงใน Product Stock -----------

    Public Sub ExecuteNonQuery(ByVal CommandText As String)
        Dim Command As New SqlCommand
        Dim Conn As New SqlConnection(ConnectionString)

        Conn.Open()
        With Command
            .Connection = Conn
            .CommandText = CommandText
            .ExecuteNonQuery()
            .Dispose()
        End With
        Conn.Close()
        Conn.Dispose()
    End Sub

    Public Sub ExecuteNonQuery_Log(ByVal CommandText As String)
        Dim Command As New SqlCommand
        Dim Conn As New SqlConnection(LogConnectionString)

        Conn.Open()
        With Command
            .Connection = Conn
            .CommandText = CommandText
            .ExecuteNonQuery()
            .Dispose()
        End With
        Conn.Close()
        Conn.Dispose()
    End Sub

    Public Function GetNewPrimaryID(ByVal TableName As String, ByVal PrimaryKeyName As String) As Integer
        Dim SQL As String = "SELECT IsNull(MAX(" & PrimaryKeyName & "),0)+1 FROM " & TableName
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function

    Public Enum UILanguage
        TH = 1
        EN = 2
        CN = 3
        JP = 4
        KR = 5
        RS = 6
    End Enum

    Public Enum Device
        CashIn = 1
        Cash20 = 2
        Cash100 = 3
        CoinIn = 4
        Coin1 = 5
        Coin5 = 6
        Printer = 7
        Passport = 8
        Camera = 9
        ProductShelf = 10
        SIMDispenser1 = 11
        SIMDispenser2 = 12
        SIMDispenser3 = 13
        FrontQRReader = 15
        InternalQRReader = 16

    End Enum

    Public Enum DeviceType
        CashIn = 1
        CashOut = 2
        CoinIn = 3
        CoinOut = 4
        Printer = 5
        PassportScanner = 6
        IPCamera = 7
        SIMDispenser = 8
        BarcodeReader = 9
        ProductShelf = 10

    End Enum

    Public Enum StockMovementType
        CheckIn = 1
        CheckOut = 2
        Sell = 3
        ChangeSlot = 4
    End Enum

    Public Enum ShiftStatus
        OnGoing = -1
        Close = 0
        Open = 1
        Unknown = 99
    End Enum


    Public Function Get_Language_Code(ByVal Language As UILanguage) As String
        Select Case Language
            Case UILanguage.TH
                Return "TH"
            Case UILanguage.EN
                Return "EN"
            Case UILanguage.CN
                Return "CN"
            Case UILanguage.JP
                Return "JP"
            Case UILanguage.KR
                Return "KR"
            Case UILanguage.RS
                Return "RS"
            Case Else
                Return ""
        End Select
    End Function

    Public Function Get_Language_Name(ByVal Language As UILanguage) As String
        Select Case Language
            Case UILanguage.TH
                Return "Thai"
            Case UILanguage.EN
                Return "English"
            Case UILanguage.CN
                Return "Chinese"
            Case UILanguage.JP
                Return "Japanese"
            Case UILanguage.KR
                Return "Korian"
            Case UILanguage.RS
                Return "Russian"
            Case Else
                Return ""
        End Select
    End Function

    Public Function Get_Site_Icon_Path(ByVal SITE_ID As Integer) As String
        Return PicturePath & "\Site\" & SITE_ID
    End Function

    Public Function Get_Site_Icon(ByVal SITE_ID As Integer) As Byte()
        Dim FilePath As String = Get_Site_Icon_Path(SITE_ID)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_Site_Icon(ByVal SITE_ID As Integer, ByVal B As Byte())
        Dim FilePath As String = Get_Site_Icon_Path(SITE_ID)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_Site_Icon(ByVal SITE_ID As Integer)
        Dim FilePath As String = Get_Site_Icon_Path(SITE_ID)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_Brand_Logo_Path(ByVal BRAND_ID As Integer) As String
        Return PicturePath & "\Brand\" & BRAND_ID
    End Function

    Public Function Get_Brand_Logo(ByVal BRAND_ID As Integer) As Byte()
        Dim FilePath As String = Get_Brand_Logo_Path(BRAND_ID)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_Brand_Logo(ByVal BRAND_ID As Integer, ByVal B As Byte())
        Dim FilePath As String = Get_Brand_Logo_Path(BRAND_ID)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_Brand_Logo(ByVal BRAND_ID As Integer)
        Dim FilePath As String = Get_Brand_Logo_Path(BRAND_ID)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_Product_Picture_Path(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage) As String
        Return PicturePath & "\Product\" & PRODUCT_ID & "_" & Get_Language_Code(Language)
    End Function

    Public Function Get_Product_Picture(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage) As Byte()
        Dim FilePath As String = Get_Product_Picture_Path(PRODUCT_ID, Language)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_Product_Picture(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage, ByVal B As Byte())
        Dim FilePath As String = Get_Product_Picture_Path(PRODUCT_ID, Language)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_Product_Picture(ByVal PRODUCT_ID As Integer, ByVal Language As UILanguage)
        Dim FilePath As String = Get_Product_Picture_Path(PRODUCT_ID, Language)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_SIM_Package_Picture_Path(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As String
        Return PicturePath & "\SIM\Package\" & SIM_ID & "_" & Get_Language_Code(Language)
    End Function

    Public Function Get_SIM_Package_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As Byte()
        Dim FilePath As String = Get_SIM_Package_Picture_Path(SIM_ID, Language)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_SIM_Package_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage, ByVal B As Byte())
        Dim FilePath As String = Get_SIM_Package_Picture_Path(SIM_ID, Language)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_SIM_Package_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage)
        Dim FilePath As String = Get_SIM_Package_Picture_Path(SIM_ID, Language)
        DeleteFile(FilePath)
    End Sub

    Public Function Get_SIM_Detail_Picture_Path(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As String
        Return PicturePath & "\SIM\Detail\" & SIM_ID & "_" & Get_Language_Code(Language)
    End Function

    Public Function Get_SIM_Detail_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage) As Byte()
        Dim FilePath As String = Get_SIM_Detail_Picture_Path(SIM_ID, Language)
        Return ReadFile(FilePath)
    End Function

    Public Sub Save_SIM_Detail_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage, ByVal B As Byte())
        Dim FilePath As String = Get_SIM_Detail_Picture_Path(SIM_ID, Language)
        SaveFile(FilePath, B)
    End Sub

    Public Sub Delete_SIM_Detail_Picture(ByVal SIM_ID As Integer, ByVal Language As UILanguage)
        Dim FilePath As String = Get_SIM_Detail_Picture_Path(SIM_ID, Language)
        DeleteFile(FilePath)
    End Sub

    Public Sub Bind_DDL_Province(ByRef ddl As DropDownList, Optional ByVal PROVINCE_CODE As String = "")

        Dim SQL As String = "Select * FROM MS_PROVINCE ORDER BY PROVINCE_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("PROVINCE_NAME"), DT.Rows(i).Item("PROVINCE_CODE"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = PROVINCE_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Amphur(ByRef ddl As DropDownList, ByVal PROVINCE_CODE As String, Optional ByVal AMPHUR_CODE As String = "")

        Dim SQL As String = "SELECT * FROM MS_AMPHUR WHERE PROVINCE_CODE='" & PROVINCE_CODE.Replace("'", "''") & "' ORDER BY AMPHUR_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("AMPHUR_NAME"), DT.Rows(i).Item("AMPHUR_CODE"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = AMPHUR_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Tumbol(ByRef ddl As DropDownList, ByVal AMPHUR_CODE As String, Optional ByVal TUMBOL_CODE As String = "")

        Dim SQL As String = "SELECT * FROM MS_TUMBOL WHERE AMPHUR_CODE='" & AMPHUR_CODE.Replace("'", "''") & "' ORDER BY TUMBOL_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("TUMBOL_NAME"), DT.Rows(i).Item("TUMBOL_CODE"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0

        For i As Integer = 1 To ddl.Items.Count - 1
            If ddl.Items(i).Value = TUMBOL_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Site(ByRef ddl As DropDownList, Optional ByVal SITE_ID As Integer = 0)

        Dim SQL As String = "Select * FROM MS_Site WHERE Active_Status=1 ORDER BY SITE_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("SITE_NAME"), DT.Rows(i).Item("SITE_ID"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 1 To ddl.Items.Count - 1
            If ddl.Items(i).Value = SITE_ID Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Brand(ByRef ddl As DropDownList, Optional ByVal BRAND_CODE As String = "")

        Dim SQL As String = "Select * FROM MS_Brand ORDER BY BRAND_CODE"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("BRAND_NAME"), DT.Rows(i).Item("BRAND_ID"))
            ddl.Items.Add(Item)
        Next
        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = BRAND_CODE Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

    Public Sub Bind_DDL_Spec(ByRef ddl As DropDownList, ByRef Language As UILanguage, Optional ByVal SPEC_ID As String = "", Optional ByVal Add_SPEC As Boolean = False)
        Dim SQL As String = "Select SPEC_ID, " & vbLf
        Select Case Language
            Case 1
                SQL &= " SPEC_NAME_TH SPEC_NAME " & vbLf
            Case 2
                SQL &= " SPEC_NAME_EN SPEC_NAME " & vbLf
            Case 3
                SQL &= " SPEC_NAME_CH SPEC_NAME " & vbLf
            Case 4
                SQL &= " SPEC_NAME_JP SPEC_NAME " & vbLf
            Case 5
                SQL &= " SPEC_NAME_KR SPEC_NAME " & vbLf
            Case 6
                SQL &= " SPEC_NAME_RS SPEC_NAME " & vbLf
        End Select
        SQL &= "  FROM MS_Spec ORDER BY SPEC_NAME"

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("...", ""))
        For i As Integer = 0 To DT.Rows.Count - 1
            Dim Item As New ListItem(DT.Rows(i).Item("SPEC_NAME"), DT.Rows(i).Item("SPEC_ID"))
            ddl.Items.Add(Item)
        Next
        If Add_SPEC Then
            ddl.Items.Add(New ListItem("+ Spec", "-1"))
        End If

        ddl.SelectedIndex = 0
        For i As Integer = 0 To ddl.Items.Count - 1
            If ddl.Items(i).Value = SPEC_ID Then
                ddl.SelectedIndex = i
                Exit For
            End If
        Next

    End Sub

#Region "Product"

    Public Function Get_Product_Info_From_ID(ByVal PRODUCT_ID As Integer) As DataTable
        Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT WHERE PRODUCT_ID=" & PRODUCT_ID
        Dim DA As New SqlDataAdapter(Sql, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function Get_Product_Info_From_Code(ByVal PRODUCT_CODE As String) As DataTable
        Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT WHERE PRODUCT_CODE='" & PRODUCT_CODE.Replace("'", "''") & "'"
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Class BarcodeScanResult
        Public PRODUCT_ID As Integer = 0
        Public PRODUCT_CODE As String = ""
        Public DISPLAY_NAME As String = ""
        Public IS_SERIAL As Boolean = False
        Public TYPE As String = ""

        Public WIDTH As Integer = 0
        Public HEIGHT As Integer = 0
        Public DEPTH As Integer = 0

        Public SERIAL_NO As String = ""

        Public Result As Boolean = False
        Public Message As String = "Not found"

    End Class

    Public Function Get_Product_Barcode_scan_Result(ByVal Shop_Code As String, ByVal Search As String) As BarcodeScanResult
        Dim Result As New BarcodeScanResult

        Dim SQL As String = "SELECT PRODUCT_ID,PRODUCT_CODE,DISPLAY_NAME,ISNULL(IS_SERIAL,0) IS_SERIAL,GS1,TYPE" & vbLf
        SQL &= ", WIDTH, HEIGHT, DEPTH, SERIAL_NO, SLOT_ID" & vbLf
        SQL &= " From VW_PRODUCT_FOR_SCAN" & vbLf
        SQL &= "WHERE PRODUCT_CODE='" & Search.Replace("'", "''") & "' OR GS1 ='" & Search.Replace("'", "''") & "'"

        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count > 0 Then
            Result.PRODUCT_ID = DT.Rows(0).Item("PRODUCT_ID")
            Result.PRODUCT_CODE = DT.Rows(0).Item("PRODUCT_CODE").ToString
            Result.DISPLAY_NAME = DT.Rows(0).Item("DISPLAY_NAME").ToString
            Result.IS_SERIAL = DT.Rows(0).Item("IS_SERIAL")
            Result.TYPE = DT.Rows(0).Item("TYPE").ToString
            If Not IsDBNull(DT.Rows(0).Item("WIDTH")) Then Result.WIDTH = DT.Rows(0).Item("WIDTH")
            If Not IsDBNull(DT.Rows(0).Item("HEIGHT")) Then Result.HEIGHT = DT.Rows(0).Item("HEIGHT")
            If Not IsDBNull(DT.Rows(0).Item("DEPTH")) Then Result.DEPTH = DT.Rows(0).Item("DEPTH")

            If DT.Rows(0).Item("IS_SERIAL") Then
                '---------------- เป็น Product Serial แต่ไม่ได้ยิง Serial มา --------------
                Result.Result = False
                Result.Message = "Product นี้ต้องยิง Serial"
                Return Result
            Else
                Result.Result = True
                Result.Message = "Success"
                Return Result
            End If
        Else
            '----------- Get From TSM : Hardcode For Test--------------
            Result.IS_SERIAL = True
            Result.SERIAL_NO = Search
            Result.PRODUCT_CODE = Get_TSM_Product_Code_From_Serial("", Search) '---------------  ตรวจสอบเลข Serial กับ TSM ---------

            If Result.PRODUCT_CODE = "" Then
                Result.Result = False
                Result.Message = "ไม่พบเลข Barcode"
                Return Result
            Else
                SQL = "SELECT PRODUCT_ID,PRODUCT_CODE,DISPLAY_NAME,IS_SERIAL,GS1,TYPE" & vbLf
                SQL &= ",WIDTH, HEIGHT, DEPTH, SERIAL_NO, SLOT_ID" & vbLf
                SQL &= " From VW_PRODUCT_FOR_SCAN" & vbLf
                SQL &= "WHERE PRODUCT_CODE='" & Result.PRODUCT_CODE.Replace("'", "''") & "'"
                DT = New DataTable
                DA = New SqlDataAdapter(SQL, ConnectionString)
                DA.Fill(DT)
                If DT.Rows.Count = 0 Then
                    Result.Result = False
                    Result.Message = "ไม่พบ Product Code"
                    Return Result
                Else
                    Result.PRODUCT_ID = DT.Rows(0).Item("PRODUCT_ID")
                    Result.DISPLAY_NAME = DT.Rows(0).Item("DISPLAY_NAME").ToString
                    Result.TYPE = DT.Rows(0).Item("TYPE").ToString
                    If Not IsDBNull(DT.Rows(0).Item("WIDTH")) Then Result.WIDTH = DT.Rows(0).Item("WIDTH")
                    If Not IsDBNull(DT.Rows(0).Item("HEIGHT")) Then Result.HEIGHT = DT.Rows(0).Item("HEIGHT")
                    If Not IsDBNull(DT.Rows(0).Item("DEPTH")) Then Result.DEPTH = DT.Rows(0).Item("DEPTH")

                    '---------------- ตรวจสอบว่ามีอยู่ใน Stock ปัจจุบันหรือไม่----------------
                    DT.DefaultView.RowFilter = "SERIAL_NO='" & Search.Replace("'", "''") & "'"
                    If DT.DefaultView.Count > 0 Then
                        Result.Result = False
                        Result.Message = "สินค้านี้มีอยู่ใน Stock แล้ว (ซ้ำ)"
                        Return Result
                    Else
                        Result.Result = True
                        Result.Message = "Success"
                        Return Result
                    End If
                End If
            End If
        End If

    End Function

    Public Function Get_TSM_Product_Code_From_Serial(ByVal Shop_Code As String, ByVal SERIAL_NO As String) As String
        '----------- Get From TSM : Hardcode For Test--------------
        Dim SQL As String = "SELECT PRODUCT_CODE,SERIAL_NO FROM TMP_Serial" & vbLf
        SQL &= "WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "'"

        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        If DT.Rows.Count = 0 Then
            Return ""
        Else
            Return DT.Rows(0).Item("PRODUCT_CODE").ToString
        End If
    End Function

    Public Sub Save_Product_Movement_Log(ByVal SHIFT_ID As Integer, ByVal SHIFT_STATUS As ShiftStatus, ByVal PRODUCT_ID As Integer, ByVal SERIAL_NO As String, ByVal MOVE_ID As StockMovementType, ByVal MOVE_FROM As String, ByVal MOVE_FROM_SLOT As Integer, ByVal MOVE_TO As String, ByVal MOVE_TO_SLOT As Integer, ByVal REMARK As String, ByVal MOVE_BY As Integer, ByVal MOVE_TIME As DateTime)
        Dim SQL As String = "Select TOP 0 * FROM TB_PRODUCT_MOVEMENT"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, LogConnectionString)
        DA.Fill(DT)
        Dim DR As DataRow = DT.NewRow

        DR("HIS_ID") = GetNewPrimaryLogID("TB_PRODUCT_MOVEMENT", "HIS_ID")

        If SHIFT_ID <> 0 Then DR("SHIFT_ID") = SHIFT_ID
        If SHIFT_STATUS <> ShiftStatus.Unknown Then DR("SHIFT_STATUS") = SHIFT_STATUS

        DR("PRODUCT_ID") = PRODUCT_ID
        DR("SERIAL_NO") = SERIAL_NO
        DR("MOVE_ID") = MOVE_ID
        DR("MOVE_FROM") = MOVE_FROM
        If MOVE_FROM_SLOT <> 0 Then DR("MOVE_FROM_SLOT") = MOVE_FROM_SLOT
        DR("MOVE_TO") = MOVE_TO
        If MOVE_TO_SLOT <> 0 Then DR("MOVE_TO_SLOT") = MOVE_TO_SLOT
        DR("REMARK") = REMARK
        DR("MOVE_BY") = MOVE_BY
        DR("MOVE_TIME") = MOVE_TIME

        DT.Rows.Add(DR)
        Dim cmd As New SqlCommandBuilder(DA)
        DA.Update(DT)
    End Sub

    Public Function GetNewPrimaryLogID(ByVal TableName As String, ByVal PrimaryKeyName As String) As Integer
        Dim SQL As String = "SELECT IsNull(MAX(" & PrimaryKeyName & "),0)+1 FROM " & TableName
        Dim DA As New SqlDataAdapter(SQL, LogConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT.Rows(0).Item(0)
    End Function

#End Region

#Region "Kiosk Management"

    Public Sub Bind_Product_Shelf_Layout(ByRef Shelf As UC_Product_Shelf, ByVal KO_ID As Integer)

        Shelf.ResetDimension()
        Shelf.ClearAllFloor()
        '--------------- BindShelf ----------------
        Dim SQL As String = "Select * FROM TB_PRODUCT_SHELF WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        If DT.Rows.Count = 0 Then Exit Sub

        Shelf.SHELF_ID = DT.Rows(0).Item("SHELF_ID")
        Shelf.SHELF_DEPTH = DT.Rows(0).Item("DEPTH")
        Shelf.SHELF_WIDTH = DT.Rows(0).Item("WIDTH")
        Shelf.SHELF_HEIGHT = DT.Rows(0).Item("HEIGHT")

        '------------ Bind Floor -----------------
        SQL = "Select FLOOR_ID, HEIGHT, POS_Y, " & vbLf
        SQL &= "0 HighLight, CAST(1 As BIT) ShowFloorName, CAST(1 As BIT) ShowMenu, NULL SlotDatas " & vbLf
        SQL &= "FROM TB_PRODUCT_FLOOR" & vbLf
        SQL &= "WHERE SHELF_ID=" & Shelf.SHELF_ID & vbLf
        SQL &= " ORDER BY FLOOR_ORDER" & vbLf
        DT = New DataTable
        DA = New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For f As Integer = 0 To DT.Rows.Count - 1

            Dim FLOOR_ID As Integer = DT.Rows(f).Item("FLOOR_ID")
            Dim FLOOR_HEIGHT As Integer = DT.Rows(f).Item("HEIGHT")
            Dim POS_Y As Integer = DT.Rows(f).Item("POS_Y")

            Shelf.AddFloor(FLOOR_ID, FLOOR_HEIGHT, POS_Y, False, True, Nothing, True, f)
            Dim Floor As UC_Product_Floor = Shelf.Floors(f)
            '-------------- Bind Slot --------------
            SQL = "Select * FROM TB_PRODUCT_SLOT " & vbLf
            SQL &= "WHERE FLOOR_ID=" & FLOOR_ID & vbLf
            SQL &= "ORDER BY SLOT_ORDER"
            Dim ST = New DataTable
            DA = New SqlDataAdapter(SQL, ConnectionString)
            DA.Fill(ST)

            For s As Integer = 0 To ST.Rows.Count - 1
                Dim SLOT_ID As Integer = ST.Rows(s).Item("SLOT_ID")
                Dim POS_X As Integer = ST.Rows(s).Item("POS_X")
                Dim SLOT_WIDTH As Integer = ST.Rows(s).Item("WIDTH")
                Floor.AddSlot(SLOT_ID, Chr(Asc("A") + f) & "-" & s + 1, POS_X, SLOT_WIDTH, 0, "", 0, "", Drawing.Color.White, Drawing.Color.White, False)
            Next

        Next
    End Sub

    Public Sub Bind_Product_Shelf_Stock(ByRef Shelf As UC_Product_Shelf, ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT PRODUCT_ID,PRODUCT_CODE,PRODUCT_NAME,SERIAL_NO,SLOT_NAME" & vbLf
        SQL &= "FROM VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Bind_Product_Shelf_Stock(Shelf, DT)
    End Sub


    Public Sub Bind_Product_Shelf_Stock(ByRef Shelf As UC_Product_Shelf, ByVal STOCK_DATA As DataTable, Optional ByVal VW_ALL_PRODUCT As DataTable = Nothing)

        If IsNothing(VW_ALL_PRODUCT) Then
            Dim SQL As String = "SELECT * FROM VW_ALL_PRODUCT"
            Dim DA As New SqlDataAdapter(SQL, ConnectionString)
            VW_ALL_PRODUCT = New DataTable
            DA.Fill(VW_ALL_PRODUCT)
        End If


        Dim DT As DataTable = STOCK_DATA.Copy
        Dim Slots As List(Of UC_Product_Slot) = Shelf.Slots
        For i As Integer = 0 To Shelf.Slots.Count - 1
            DT.DefaultView.RowFilter = "SLOT_NAME='" & Slots(i).SLOT_NAME & "'"
            If DT.DefaultView.Count = 0 Then
                Slots(i).PRODUCT_ID = 0
                Slots(i).PRODUCT_CODE = ""
                Slots(i).PRODUCT_QUANTITY = 0
                Slots(i).ShowQuantity = False
                Slots(i).ShowMask = True
                Slots(i).MaskContent = "<b class='text-default'>EMPTY</b>"
            Else
                Slots(i).PRODUCT_ID = DT.DefaultView(0).Item("PRODUCT_ID")
                Slots(i).PRODUCT_CODE = DT.DefaultView(0).Item("PRODUCT_CODE")
                Slots(i).PRODUCT_QUANTITY = DT.DefaultView.Count
                '----------- Calculate Percent -------------
                VW_ALL_PRODUCT.DefaultView.RowFilter = "PRODUCT_ID=" & Slots(i).PRODUCT_ID
                Dim DV As DataRowView = VW_ALL_PRODUCT.DefaultView(0)

                If Not IsDBNull(DV("DEPTH")) AndAlso DV("DEPTH") > 0 Then
                    Dim MaxQuantity As Integer = Math.Floor(Shelf.SHELF_DEPTH / DV("DEPTH"))
                    Dim Percent As Integer = Math.Floor((Slots(i).PRODUCT_QUANTITY * 100) / MaxQuantity)
                    Slots(i).PRODUCT_LEVEL_PERCENT = Percent & "%"
                    If Percent <= Product_Critical_Percent Then
                        Slots(i).PRODUCT_LEVEL_COLOR = Drawing.Color.Red
                    Else
                        Slots(i).PRODUCT_LEVEL_COLOR = Drawing.Color.Green
                    End If
                    Slots(i).QUANTITY_BAR_COLOR = Drawing.Color.FromKnownColor(Drawing.KnownColor.Silver)
                    Slots(i).ShowQuantity = True
                    Slots(i).ShowMask = False
                    Slots(i).MaskContent = ""
                Else
                    Slots(i).ShowProductCode = True
                    Slots(i).ShowQuantity = False
                    Slots(i).ShowMask = True
                    Slots(i).MaskContent = "<small class='text-warning'> Dimension is not set</small>"
                End If
            End If
        Next
    End Sub



    Public Function GetList_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " Select KO.KO_ID, KO.KO_CODE, KO.SITE_ID, Site.SITE_CODE, KO.ZONE, KO.IP, KO.Mac   " & vbLf
        SQL &= " , KO.IsOnline, COUNT(SLOT.PRODUCT_ID) Total_Product, KO.Active_Status " & vbLf
        SQL &= " FROM MS_KIOSK KO           " & vbLf
        SQL &= " INNER JOIN MS_Site Site On KO.SITE_ID = Site.SITE_ID" & vbLf
        SQL &= " LEFT JOIN VW_KIOSK_SLOT SLOT On  KO.KO_ID=SLOT.KO_ID         " & vbLf
        SQL &= " " & vbLf
        If KO_ID <> 0 Then
            SQL &= " WHERE KO.KO_ID=" & KO_ID & vbLf
        End If
        SQL &= " GROUP BY KO.KO_ID, KO.KO_CODE, KO.SITE_ID, Site.SITE_CODE, KO.ZONE, KO.IP, KO.Mac, KO.IsOnline, KO.Active_Status" & vbLf
        SQL &= " ORDER BY KO.KO_ID" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Kiosk_Device(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " Select " & vbLf
        SQL &= " K.KO_ID, D.D_ID, D.D_Name, D.DT_ID, DT.DT_Name, " & vbLf
        SQL &= " D.Unit_Value, " & vbLf
        SQL &= " ISNULL(D.Max_Qty, D.Max_Qty) Max_Qty, " & vbLf
        SQL &= " ISNULL(D.Warning_Qty, D.Warning_Qty) Warning_Qty, " & vbLf
        SQL &= " ISNULL(D.Critical_Qty, D.Critical_Qty) Critical_Qty,KD.Current_Qty,DT.Movement_Direction," & vbLf
        SQL &= " DS.DS_ID,DS.DS_Name,DS.IsProblem,KD.Update_Time,D.D_Order,Icon_White,Icon_Red,Icon_Green" & vbLf
        SQL &= " FROM " & vbLf
        SQL &= " MS_KIOSK K " & vbLf
        SQL &= " INNER JOIN MS_DEVICE D On K.Active_Status=1 And D.Active_Status=1" & vbLf
        SQL &= " INNER JOIN MS_DEVICE_TYPE DT On D.DT_ID= DT.DT_ID " & vbLf
        SQL &= " LEFT JOIN TB_KIOSK_DEVICE KD On D.D_ID=KD.D_ID And KD.KO_ID=K.KO_ID" & vbLf
        SQL &= " LEFT JOIN MS_DEVICE_STATUS DS On KD.DS_ID=DS.DS_ID" & vbLf
        SQL &= "                             And D.DT_ID=DS.DT_ID" & vbLf
        If KO_ID <> 0 Then
            SQL &= " WHERE K.KO_ID=" & KO_ID & vbLf
        End If
        SQL &= " ORDER BY D.D_Order" & vbLf

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DT.TableName = "Data"
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Device_Status(Optional ByVal DT_ID As Integer = 0, Optional ByVal DS_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= " Select DT_ID, DS_ID, DS_Name, IsProblem " & vbLf
        SQL &= " FROM MS_DEVICE_STATUS " & vbLf

        Dim Filter As String = ""
        If DT_ID <> 0 Then
            Filter &= " DT_ID = " & DT_ID & " And "
        End If
        If DS_ID <> 0 Then
            Filter &= " DS_ID = " & DS_ID & " And "
        End If
        If Filter <> "" Then
            Filter = " WHERE " & Filter.Substring(0, Filter.Length - 4)
        End If
        SQL &= Filter

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Sub Drop_Kiosk(ByVal KO_ID As Integer)
        '--------- Delete Relation---------
        Drop_PRODUCT_SHELF(KO_ID)
        Drop_KIOSK_DEVICE(KO_ID)
        Drop_SHIFT(KO_ID)

        Dim SQL As String = "DELETE FROM MS_KIOSK WHERE KO_ID=" & KO_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SERIAL_NO As String)
        Dim SQL As String = "Select SLOT_ID FROM TB_PRODUCT_SERIAL" & vbLf
        SQL &= "WHERE SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "'"
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_STOCK_SERIAL(DT.Rows(0).Item("SLOT_ID"), SERIAL_NO)
        Next
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SLOT_ID As Integer)
        Dim SQL As String = "SELECT SERIAL_NO FROM TB_PRODUCT_SERIAL" & vbLf
        SQL &= "WHERE SLOT_ID=" & SLOT_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_STOCK_SERIAL(SLOT_ID, DT.Rows(0).Item("SERIAL"))
        Next
    End Sub

    Public Sub Drop_PRODUCT_STOCK_SERIAL(ByVal SLOT_ID As Integer, ByVal SERIAL_NO As String)
        Dim SQL As String = "DELETE FROM TB_PRODUCT_SERIAL" & vbLf
        SQL &= "WHERE SLOT_ID=" & SLOT_ID & " AND SERIAL_NO='" & SERIAL_NO.Replace("'", "''") & "'"
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_SLOT(ByVal FLOOR_ID As Integer, ByVal SLOT_ID As Integer)
        Drop_PRODUCT_STOCK_SERIAL(SLOT_ID)
        Dim SQL As String = "DELETE FROM TB_PRODUCT_SLOT WHERE FLOOR_ID=" & FLOOR_ID & " AND SLOT_ID=" & SLOT_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_SLOT(ByVal FLOOR_ID As Integer)
        Dim SQL As String = "SELECT SLOT_ID FROM TB_PRODUCT_SLOT WHERE FLOOR_ID=" & FLOOR_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_SLOT(FLOOR_ID, DT.Rows(i).Item("SLOT_ID"))
        Next
    End Sub

    Public Sub Drop_PRODUCT_FLOOR(ByVal SHELF_ID As Integer, ByVal FLOOR_ID As Integer)
        Drop_PRODUCT_SLOT(FLOOR_ID)

        Dim SQL As String = "DELETE FROM TB_PRODUCT_FLOOR" & vbLf
        SQL &= "WHERE SHELF_ID=" & SHELF_ID & " AND FLOOR_ID=" & FLOOR_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_PRODUCT_FLOOR(ByVal SHELF_ID As Integer)
        Dim SQL As String = "SELECT FLOOR_ID FROM TB_PRODUCT_FLOOR WHERE SHELF_ID=" & SHELF_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_PRODUCT_FLOOR(SHELF_ID, DT.Rows(i).Item("FLOOR_ID"))
        Next
    End Sub

    Public Sub Drop_PRODUCT_SHELF(ByVal KO_ID As Integer)
        Drop_PRODUCT_FLOOR(KO_ID)

        Dim SQL As String = "DELETE FROM TB_PRODUCT_SHELF" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_KIOSK_DEVICE(ByVal KO_ID As Integer, ByVal D_ID As Integer)
        Dim SQL As String = "DELETE FROM TB_KIOSK_DEVICE" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID & " AND D_ID=" & D_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_KIOSK_DEVICE(ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT D_ID FROM TB_KIOSK_DEVICE WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)

        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_KIOSK_DEVICE(KO_ID, DT.Rows(i).Item("D_ID"))
        Next
    End Sub

    Public Sub Drop_SHIFT(ByVal KO_ID As Integer, ByVal SHIFT_ID As Integer)
        '--------- Delete Relation If Exists---------
        Dim SQL As String = "DELETE FROM TB_SHIFT" & vbLf
        SQL &= "WHERE KO_ID=" & KO_ID & " AND SHIFT_ID=" & SHIFT_ID
        ExecuteNonQuery(SQL)
    End Sub

    Public Sub Drop_SHIFT(ByVal KO_ID As Integer)
        Dim SQL As String = "SELECT SHIFT_ID FROM TB_SHIFT WHERE KO_ID=" & KO_ID
        Dim DT As New DataTable
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        DA.Fill(DT)
        For i As Integer = 0 To DT.Rows.Count - 1
            Drop_SHIFT(KO_ID, DT.Rows(i).Item("SHIFT_ID"))
        Next
    End Sub

#End Region


#Region "Shift"

    Public Function Get_Kiosk_Current_Shift(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select TOP 1 SHIFT_ID" & vbLf
        SQL &= "  ,KO_ID" & vbLf
        SQL &= "  ,SHIFT_Y,SHIFT_M,SHIFT_D,SHIFT_N,KO_CODE" & vbLf
        SQL &= "  ,dbo.FN_SHIFT_CODE(SHIFT_Y,SHIFT_M,SHIFT_D,ISNULL(KO_CODE,dbo.FN_KO_CODE(KO_ID)),SHIFT_N)    SHIFT_CODE" & vbLf
        SQL &= "  ,Open_Time,Open_By , USER_Open.FIRST_NAME+' '+ USER_Open.LAST_NAME Open_By_Name" & vbLf
        SQL &= "  ,Close_Time,Close_By  , USER_Close.FIRST_NAME+' '+ USER_Close.LAST_NAME Close_By_Name" & vbLf
        SQL &= "  From TB_SHIFT" & vbLf
        SQL &= "  Left Join MS_USER USER_Open On USER_Open.USER_ID=TB_SHIFT.Open_By" & vbLf
        SQL &= "  Left Join MS_USER USER_Close ON USER_Close.USER_ID=TB_SHIFT.Close_By" & vbLf
        SQL &= "  Where KO_ID=" & KO_ID & vbLf
        SQL &= "  Order By SHIFT_ID" & vbLf

        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DT.TableName = "Data"
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetKiosk_Current_OTY(ByRef KO_ID As Integer, ByRef D_ID As Integer, ByRef Unit_Value As Integer) As Integer
        Dim Current_QTY As Integer = 0
        Dim SQL As String = ""

        SQL &= "  --หา Stock ของ CoinIn CashIn para(KO_ID,D_ID,Unit_Value) " & vbLf

        SQL &= "  Select TOP 1 TB_TRANSACTION_STOCK.TXN_ID " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.D_ID " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.Unit_Value " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.BEFORE_QUANTITY " & vbLf
        SQL &= "      ,TB_TRANSACTION_STOCK.DIFF_QUANTITY " & vbLf
        SQL &= "    ,(TB_TRANSACTION_STOCK.BEFORE_QUANTITY)+ (TB_TRANSACTION_STOCK.DIFF_QUANTITY*MS_DEVICE_TYPE.Movement_Direction) CURRENT_QTY " & vbLf
        SQL &= "  From TB_TRANSACTION_STOCK " & vbLf
        SQL &= "  Left Join TB_SERVICE_TRANSACTION ON TB_SERVICE_TRANSACTION.TXN_ID=TB_TRANSACTION_STOCK.TXN_ID " & vbLf
        SQL &= "  Left Join MS_DEVICE ON MS_DEVICE.D_ID=TB_TRANSACTION_STOCK.D_ID " & vbLf
        SQL &= "  Left Join MS_DEVICE_TYPE ON MS_DEVICE_TYPE.DT_ID=MS_DEVICE.DT_ID  " & vbLf
        SQL &= "  WHERE TB_SERVICE_TRANSACTION.KO_ID =" & KO_ID & "   " & vbLf
        SQL &= "  And TB_TRANSACTION_STOCK.D_ID=" & D_ID & " " & vbLf
        SQL &= "  And TB_TRANSACTION_STOCK.Unit_Value=" & Unit_Value & "  " & vbLf
        SQL &= "  ORDER BY TB_TRANSACTION_STOCK.TXN_ID DESC " & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        If DT.Rows.Count > 0 Then
            Current_QTY = Val(DT.Rows(0).Item("CURRENT_QTY").ToString.Replace(",", ""))
        End If
        Return Current_QTY
    End Function

    Public Function CheckDevice_Status(ByRef KO_ID As Integer, ByRef D_ID As Integer) As Integer
        Dim DS_ID As Integer = 1







        Return DS_ID
    End Function


#End Region

#Region "SetUnit_Value CoinIn CashIn"


    Public Function GetCoinIn_List() As DataTable
        Dim DT_CoinIn As New DataTable
        DT_CoinIn.Columns.Add("Unit_Value")
        DT_CoinIn.Columns.Add("Icon_Green")
        DT_CoinIn.Columns.Add("Active_Status", GetType(Boolean))

        DT_CoinIn.Rows.Add(1, "images/Icon/Green/coin1.png", 1)
        DT_CoinIn.Rows.Add(2, "images/Icon/Green/coin2.png", 1)
        DT_CoinIn.Rows.Add(5, "images/Icon/Green/coin5.png", 1)
        DT_CoinIn.Rows.Add(10, "images/Icon/Green/coin10.png", 1)

        DT_CoinIn.DefaultView.RowFilter = "Active_Status=1"
        Return DT_CoinIn
    End Function

    Public Function GetCashIn_List() As DataTable
        Dim DT_CashIn As New DataTable
        DT_CashIn.Columns.Add("Unit_Value")
        DT_CashIn.Columns.Add("Icon_Green")
        DT_CashIn.Columns.Add("Active_Status", GetType(Boolean))

        DT_CashIn.Rows.Add(20, "images/Icon/Green/cash20.png", 1)
        DT_CashIn.Rows.Add(50, "images/Icon/Green/cash50.png", 1)
        DT_CashIn.Rows.Add(100, "images/Icon/Green/cash100.png", 1)
        DT_CashIn.Rows.Add(500, "images/Icon/Green/cash500.png", 1)
        DT_CashIn.Rows.Add(1000, "images/Icon/Green/cash1000.png", 1)

        DT_CashIn.DefaultView.RowFilter = "Active_Status=1"
        Return DT_CashIn
    End Function

#End Region


#Region "Kiosk"

    Public Function GetList_Current_Brand_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select DISTINCT MS_BRAND.BRAND_ID , MS_BRAND.BRAND_NAME" & vbLf
        SQL &= "   From VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "   INNER Join MS_BRAND ON MS_BRAND.BRAND_ID=VW_CURRENT_PRODUCT_STOCK.BRAND_ID" & vbLf
        SQL &= "   WHERE KO_ID =" & KO_ID & " And MS_BRAND.Active_Status = 1" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function GetList_Current_Category_Kiosk(Optional ByVal KO_ID As Integer = 0) As DataTable
        Dim SQL As String = ""
        SQL &= "   Select   DISTINCT MS_PRODUCT_CAT.CAT_ID  , MS_PRODUCT_CAT.CAT_NAME " & vbLf
        SQL &= "   From VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "   INNER Join MS_PRODUCT_CAT ON MS_PRODUCT_CAT.CAT_ID =VW_CURRENT_PRODUCT_STOCK.CAT_ID" & vbLf
        SQL &= "   WHERE KO_ID = " & KO_ID & "" & vbLf
        SQL &= "   UNION ALL" & vbLf
        SQL &= "   -- กรณี Product ไม่ได้ Set Category" & vbLf
        SQL &= "   Select   DISTINCT 0 CAT_ID  , 'Other' CAT_NAME " & vbLf
        SQL &= "   From VW_CURRENT_PRODUCT_STOCK" & vbLf
        SQL &= "   Where KO_ID =" & KO_ID & " And VW_CURRENT_PRODUCT_STOCK.CAT_ID Is NULL" & vbLf


        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

    Public Function Get_Show_Default_Product(Optional ByVal MODEL As String = "") As DataTable
        Dim SQL As String = ""
        SQL &= "   Select * " & vbLf
        SQL &= "   From VW_ALL_PRODUCT" & vbLf
        If MODEL <> "" Then
            SQL &= "  WHERE MODEL='" & MODEL & "'" & vbLf
        End If
        SQL &= "  ORDER BY PRODUCT_ID" & vbLf
        Dim DA As New SqlDataAdapter(SQL, ConnectionString)
        Dim DT As New DataTable
        DA.Fill(DT)
        Return DT
    End Function

#End Region

End Class
