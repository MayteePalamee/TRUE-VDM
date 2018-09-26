﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UC_SIM_Dispenser.ascx.vb" Inherits="VDM.UC_SIM_Dispenser" %>
<%@ Register Src="~/Machine_Console/UC_SIM_Slot.ascx" TagPrefix="uc1" TagName="UC_SIM_Slot" %>

<asp:Repeater ID="rptSlot" runat="server" >
    <ItemTemplate>
        <uc1:UC_SIM_Slot runat="server" id="Slot" />
    </ItemTemplate>
</asp:Repeater>
<asp:Label ID="lblProperty" runat="server" SIM_Height="2" KO_ID="0"></asp:Label>