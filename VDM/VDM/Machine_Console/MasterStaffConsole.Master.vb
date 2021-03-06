﻿Imports System.Data.SqlClient

Public Class MasterStaffConsole
    Inherits System.Web.UI.MasterPage
    Dim BL As New VDM_BL

    Private ReadOnly Property KO_ID As Integer
        Get
            Return Session("KO_ID")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoginInfo()
            GetMachineInfo()
        End If
    End Sub

    Private Sub LoginInfo()
        If Not IsNumeric(Session("FULL_NAME")) Then
            lblLoginName.Text = Session("FULL_NAME")
        Else
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Private Sub GetMachineInfo()
        Dim DT As DataTable = BL.GetList_Kiosk(KO_ID)
        If DT.Rows.Count > 0 Then
            lblMachine_Name.Text = DT.Rows(0).Item("KO_CODE").ToString()
            lblMachine_Location.Text = DT.Rows(0).Item("SITE_CODE").ToString()
            If DT.Rows(0).Item("ZONE").ToString() <> "" Then
                lblMachine_Zone.Text = DT.Rows(0).Item("ZONE").ToString()
            Else
                spanZone.Visible = False
            End If

            '--Set Shift
            Dim DT_Shift As DataTable = BL.Get_Kiosk_Current_Shift(KO_ID)
            Dim SHIFT_ID As Integer = 0
            Dim SHIFT_Status As VDM_BL.ShiftStatus = VDM_BL.ShiftStatus.Unknown


            If DT_Shift.Rows.Count = 0 Then
                SHIFT_Status = VDM_BL.ShiftStatus.Close
                Session("SHIFT_ID") = 0
                lblHeader_Shift.Text = "CLOSE"
                lblHeader_Shift.ForeColor = Drawing.Color.Red
                lblHeader_Shift.BorderColor = Drawing.Color.Red
            ElseIf IsDBNull(DT_Shift.Rows(0).Item("Close_Time")) Then
                SHIFT_Status = VDM_BL.ShiftStatus.Open
                Session("SHIFT_ID") = DT_Shift.Rows(0).Item("SHIFT_ID")
                lblHeader_Shift.Text = "OPEN"
                lblHeader_Shift.ForeColor = Drawing.Color.Green
                lblHeader_Shift.BorderColor = Drawing.Color.SeaGreen
                Dim _Time As DateTime = DT_Shift.Rows(0).Item("Open_Time")
                lblHeader_Shift_Time.Text = " ตั้งแต่ " & _Time.ToString("dd-MMM-yyyy") & " (" & ReportFriendlyTime(DateDiff(DateInterval.Minute, _Time, Now)) & ")"
            Else
                SHIFT_Status = VDM_BL.ShiftStatus.Close
                Session("SHIFT_ID") = DT_Shift.Rows(0).Item("SHIFT_ID")
                lblHeader_Shift.Text = "CLOSE"
                lblHeader_Shift.ForeColor = Drawing.Color.Red
                lblHeader_Shift.BorderColor = Drawing.Color.Red
                Dim _Time As DateTime = DT_Shift.Rows(0).Item("Close_Time")
                lblHeader_Shift_Time.Text = " ตั้งแต่ " & _Time.ToString("dd-MMM-yyyy") & " (" & ReportFriendlyTime(DateDiff(DateInterval.Minute, _Time, Now)) & ")"
            End If

            Session("SHIFT_Status") = SHIFT_Status

        Else
            Session.Abandon()
            Response.Redirect("Login.aspx")
        End If


    End Sub

    Private Sub lnkLogout_ServerClick(sender As Object, e As EventArgs) Handles lnkLogout.ServerClick
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub
End Class