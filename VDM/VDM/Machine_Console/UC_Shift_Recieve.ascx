﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_Shift_Recieve.ascx.vb" Inherits="VDM.UC_Shift_Recieve" %>
<div class="card-block">
    <style type="text/css">
        .table > tbody > tr > td {
            border-color: transparent;
            height: 100px;
        }

        .table > tfoot > tr > td {
            /*border-color: transparent ;*/
            height: 100px;
        }
    </style>

    <div class="no-more-tables  ">
        <table class="table m-b-0">
            <thead>
                <tr>
                    <th></th>
                    <th>เดิม</th>
                    <th colspan="2">เอาออก</th>
                    <th colspan="2">เอาเข้า</th>
                    <th>คงเหลือ</th>
                    <th class="col-md-5">จำนวนเงิน(บาท)</th>

                </tr>
            </thead>
            <tbody>
                <tr>
                    <td data-title="Logo" colspan="2"><h4><b>เครื่องรับเหรียญ</b></h4>
                    </td>
                </tr>

                <tr>
                    <td data-title="Logo" id="td1" runat="server" style="text-align: center;">
                        <img id="imgcoin1" src="../images/Icon/green/coin1.png" width="40px">
                    </td>
                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox9" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox10" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox11" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>

                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox12" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: right;">
                        <b>
                            <asp:Label ID="Label2" runat="server" Text="100"></asp:Label></b>
                    </td>


                </tr>

                <tr>
                    <td data-title="Logo" id="td" runat="server" style="text-align: center;">
                        <img id="imgcoin5" src="../images/Icon/green/coin5.png" width="40px">
                    </td>
                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox5" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox8" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox6" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>

                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox7" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: right;">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="1,005"></asp:Label></b>
                    </td>


                </tr>

                <tr>
                    <td data-title="Logo" colspan="2"><h4><b>เครื่องรับแบงค์</b></h4>
                    </td>
                </tr>
                <tr>
                    <td data-title="Logo" id="td2" runat="server" style="text-align: center;">
                        <img id="imgcash20" src="../images/Icon/green/cash20.png" width="40px">
                    </td>
                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox13" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox14" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox15" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>

                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox16" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: right;">
                        <b>
                            <asp:Label ID="Label3" runat="server" Text="1,000"></asp:Label></b>
                    </td>


                </tr>

                <tr>
                    <td data-title="Logo" id="td3" runat="server" style="text-align: center;">
                        <img id="imgcash100" src="../images/Icon/green/cash100.png" width="40px">
                    </td>
                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox17" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox18" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>
                    <td data-title="Logo" style="padding-right: 0px;">
                        <asp:TextBox ID="TextBox19" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: left; padding-left: 0px;">
                        <button type="button" class="btn btn-dark"><span class="h4">เต็มจำนวน</span></button></td>

                    <td data-title="Logo">
                        <asp:TextBox ID="TextBox20" runat="server" class="form-control" Style="width: 80px; text-align: center;"></asp:TextBox>
                    </td>
                    <td data-title="Logo" style="text-align: right;">
                        <b>
                            <asp:Label ID="Label4" runat="server" Text="1,000"></asp:Label></b>
                    </td>


                </tr>

            </tbody>
            <tfoot>
                <tr>
                    <td colspan="7" data-title="Logo" style="text-align: right;"><b>รวม</b> </td>
                    <td data-title="Logo" style="text-align: right;">
                        <b>
                            <asp:Label ID="Label5" runat="server" Text="xx,xxx"></asp:Label></b>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>



</div>
