<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="compareControl.ascx.cs" Inherits="WgbSalesUpload.MASTER_UI.compareControl" %>







<style type="text/css">
    .auto-style1 {
        color: #FFFFFF;
        background-color: inherit;
    }
    .auto-style2 {
        height: 23px;
    }
    .auto-style3 {
        width: 35%;
        height: 23px;
    }
    .auto-style4 {
        width: 20%;
        height: 23px;
    }
    .auto-style5 {
        height: 26px;
    }
    .auto-style6 {
        font-weight: bold;
        color: #FFFFFF;
    }
</style>







<div class ="trigger open">

   <asp:Panel ID ="mainpanel" runat ="server" Width ="100%" CssClass="auto-style1">

<table   style=" background:inherit; width:100%; height: auto;  caption-side: top; empty-cells: hide; table-layout: inherit;">
    <tr style="border:1px; border-color:black;">
        <td style ="text-align :left; vertical-align : top; width :35%; border:1px; border-color:black;">
            <asp:Label ID="ShopCodeLabel" runat="server" style="font-weight: 700" Text="No Shop Selected" CssClass="auto-style1"></asp:Label>
        </td>
         <td style ="text-align :left; vertical-align : top; width :20%">
             <telerik:RadNumericTextBox ID="MasterBalanceLabel" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :left; vertical-align : top; width :20%">
             <telerik:RadNumericTextBox ID="ShopBalanceLabel" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :left; vertical-align : top; width :20%">
             <telerik:RadNumericTextBox ID="DeficitLabel" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :right; vertical-align : top; width :5%">
             <div class="button" style ="text-align:center">
                <asp:Button runat ="server" ID="toggleButton"   style="width :60%;" UseSubmitBehavior="false" CausesValidation ="false" text ="more" OnClientClick = "return false;" CssClass="button"></asp:Button>
       </div></td>
       </tr>
    </table>
       </asp:Panel>
    </div>
<div class="cnt">
     <asp:Panel ID ="hiddenpanel" runat ="server" Width ="100%" style="color: #DADBDC; background-color:#404040">
        <table style="width:100%">
    <tr>
        <td style ="border-style: none; border-width: inherit; height:auto; background-color: inherit;">
          <div style="height: auto">
      <table style="width:100%;">
               <tr style="border: thin double #C0C0C0; font-weight: 700; color: #000000; text-decoration: underline; font-style: italic; background-color: #C0C0C0">
                     <th class="auto-style1" style="background-color: black">
            <asp:Label ID="Label5" runat="server" style="font-weight: 700; color: #FFFFFF;" Text="Shop Details"></asp:Label>
        </th>
         <th class="auto-style1" style="background-color: black">
            <asp:Label ID="Label6" runat="server" style="font-weight: 700; color: #FFFFFF;" Text="Master Summary"></asp:Label>
        </th>
         <th class="auto-style1" style="background-color: black">
            <asp:Label ID="Label7" runat="server" style="font-weight: 700; color: #FFFFFF;" Text="Shop Summary"></asp:Label>
        </th>
         <th class="auto-style1" style="background-color: black">
            <asp:Label ID="Label8" runat="server" style="font-weight: 700; color: #FFFFFF;" Text="Deficits(Master - Shop)"></asp:Label>
        </th>
         <th style ="background-color: black; ">
         </th>
                        </tr>
                    <tr>
                        <td style ="width: 35%;" colspan="1">
            <asp:Label ID="Label1" runat="server" CssClass="auto-style6">Sales</asp:Label>
        </td>
         <td style ="width: 20%;" >
             <telerik:RadNumericTextBox ID="summarymastersales" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="width: 20%;" >
             <telerik:RadNumericTextBox ID="summaryshopsales" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="width: 20%;" >
             <telerik:RadNumericTextBox ID="summarydeficitsales" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :right; vertical-align : top; " >
       
              </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="auto-style3">
            <asp:Label ID="Label9" runat="server" CssClass="auto-style6">Winnings</asp:Label>
        </td>
         <td class="auto-style4" >
             <telerik:RadNumericTextBox ID="summarymasterwinnings" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td class="auto-style4" >
             <telerik:RadNumericTextBox ID="summaryshopwinnings" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td class="auto-style4" >
             <telerik:RadNumericTextBox ID="summarydeficitwinnings" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :right; vertical-align : top; " class="auto-style2" >
       
              </td>
                    </tr><tr>
                        <td colspan="1" class="auto-style5" >
            <asp:Label ID="Label13" runat="server" CssClass="auto-style6">Cancelled</asp:Label>
        </td>
         <td class="auto-style5" >
             <telerik:RadNumericTextBox ID="summarymastercancelled" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td class="auto-style5" >
             <telerik:RadNumericTextBox ID="summaryshopcancelled" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td class="auto-style5">
             <telerik:RadNumericTextBox ID="summarydeficitcancelled" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :right; vertical-align : top; " class="auto-style5" >
       
              </td>
                    </tr>
                    <tr>
                        <td colspan="1" >
            <asp:Label ID="Label2" runat="server" CssClass="auto-style6">Balance</asp:Label>
        </td>
         <td >
             <telerik:RadNumericTextBox ID="summarymasterBalance" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td >
             <telerik:RadNumericTextBox ID="summaryshopBalance" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td >
             <telerik:RadNumericTextBox ID="summarydeficitBalance" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :right; vertical-align : top; " >
       
              </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="auto-style2" >
            <asp:Label ID="Label11" runat="server" CssClass="auto-style6">Commision</asp:Label>
        </td>
         <td class="auto-style2" >
             <telerik:RadNumericTextBox ID="summarymastercommision" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td class="auto-style2" >
             <telerik:RadNumericTextBox ID="summaryshopcommision" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td class="auto-style2" >
             <telerik:RadNumericTextBox ID="summarydeficitcommision" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :right; vertical-align : top; " class="auto-style2" >
       
              </td>
                    </tr>
                    <tr>
                        <td colspan="1" >
            <asp:Label ID="Label16" runat="server" CssClass="auto-style6">Net Balance</asp:Label>
        </td>
         <td >
             <telerik:RadNumericTextBox ID="summarymasternetbal" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td >
             <telerik:RadNumericTextBox ID="summaryshopnetbal" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td >
             <telerik:RadNumericTextBox ID="summarydeficitnetbal" Runat="server" Culture="yo-NG" DbValueFactor="1" Enabled="False" LabelWidth="64px" MinValue="0" Type="Currency" Value="0" Width="160px">
                 <NegativeStyle Resize="None" />
                 <NumberFormat ZeroPattern="₦ n" />
                 <EmptyMessageStyle Resize="None" />
                 <ReadOnlyStyle Resize="None" />
                 <FocusedStyle Resize="None" />
                 <DisabledStyle Resize="None" />
                 <InvalidStyle Resize="None" />
                 <HoveredStyle Resize="None" />
                 <EnabledStyle Resize="None" />
             </telerik:RadNumericTextBox>
        </td>
         <td style ="text-align :right; vertical-align : top; " >
       
              </td>
                    </tr>
                </table>

            </div>

        </td>
        
    </tr>
</table>
       </asp:Panel>
    </div>

 