﻿Public Class UC_Generate_Order_Id
    Inherits System.Web.UI.UserControl

    Dim BackEndInterface As New BackEndInterface.Generate_Order_Id

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_Request_Click(sender As Object, e As EventArgs) Handles btn_Request.Click
        BackEndInterface.Get_Result(dealer.Text)

        Dim Response As New BackEndInterface.Generate_Order_Id.Response
        If Not IsNothing(Response) Then
            status.Text = Response.status
            trx_id.Text = Response.trx_id
            process_instance.Text = Response.process_instance
            response_data.Text = Response.response_data
        End If
    End Sub

End Class