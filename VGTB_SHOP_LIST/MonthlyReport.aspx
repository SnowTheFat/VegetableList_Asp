<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlyReport.aspx.cs" Inherits="VGTB_SHOP_LIST.MonthlyReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TAF VGTB APP</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <link href="~/cabbage_vegetables.ico" rel="shortcut icon" type="image/x-icon" />
</head>

<body>
    <style>
        /* width */
        ::-webkit-scrollbar {
            border-radius: 10px;
            width: 10px;
        }

        /* Track */
        ::-webkit-scrollbar-track {
            background: #f1f1f1;
        }

        /* Handle */
        ::-webkit-scrollbar-thumb {
            background: #888;
        }

            /* Handle on hover */
            ::-webkit-scrollbar-thumb:hover {
                background: #555;
            }

        body {
            background-image: url("Images/TAF_BG2.gif");
            background-color: #cccccc;
        }

        .head {
            border-radius: 10px;
            font-family: Tahoma;
            background-color: lightseagreen;
            padding: 14px;
            font-weight: bold;
            border-color: #92a8d1;
            box-shadow: 3px 3px 3px 3px #888888;
        }

        .Text-heading {
            display: inline !important;
            color: white;
            font-size: 28px;
            text-shadow: 2px 2px #888888;
            padding: 14px;
        }

        .insert {
            padding: 10px;
            font-weight: bold;
            color: black;
            font-size: 22px;
            background-color: #BDDEFF;
            border-radius: 10px;
            box-shadow: 3px 3px 3px 3px #888888;
        }

        .DisplayInput {
            padding: 10px;
            background-color: powderblue;
            border-radius: 10px;
            box-shadow: 3px 3px 3px 3px #888888;
            text-align: center;
        }

        th {
            text-align: center !important;
        }

        button, html input[type="button"], input[type="reset"], input[type="submit"] {
            width: 100px;
            /*height: 38px;*/
            color: White;
            background-color: #3D79B6;
            border-color: #3D79B6;
            font-size: 15px;
            border-radius: 10px
        }

        .right_align {
            text-align: right;
        }

        input, select {
            color: darkslateblue;
            border-radius: 10px;
            border-color: darkgrey;
        }
    </style>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="container-fluid head">
                <asp:ImageButton ID="Backbutton" ImageUrl="Images/undo.png" Height="35"   runat="server" OnClick="Backbutton_Click" />
                <div class="Text-heading">
                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Logo2.png" Height="45" Width="55" />
                    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
                </div>
                <span style="float: right;">
                    <%--  <asp:Image ID="Image2" ImageUrl="Images/user.png" Width="40px" runat="server" />--%>
                    <asp:Label ID="LblUser" runat="server" Font-Size="18px" Style="color: mediumblue" Font-Underline="True"></asp:Label>
                    <asp:ImageButton ID="Btnlogout" runat="server" OnClick="BtnLogout_Click"
                        ImageUrl="Images/logout.png" Width="40px" ToolTip="ออกจากระบบ" />
                </span>
            </div>
            <br />

              <%--Select Monthly --%>
            <div class="container-fluid insert" style="text-align: center">             
                <asp:Label ID="Label1" runat="server" Text="เลือกเดือนและปีที่ต้องการพิมพ์รีพอร์ต"></asp:Label>
                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" DataTextField="MonthName"
                    DataValueField="MonthNumber" >
                </asp:DropDownList>

                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" >
                </asp:DropDownList>
                <%-- <asp:TextBox ID="SelectMonth" runat="server" TextMode="Month" OnTextChanged="SelectMonth_TextChanged"></asp:TextBox>--%>
                <asp:ImageButton ID="BtnSearch" runat="server" 
                     ImageUrl="Images/searchdata.png" Width="40px"
                    OnClick="BtnSearch_Click" ToolTip="ค้นหาข้อมูล" />
            </div>
            <br />

            <div runat="server" id="disPlay"  class="container-fluid DisplayInput">                            
                    <asp:GridView ID="GridView_RPT" runat="server"
                    Width="100%"
                    AutoGenerateColumns="False"
                    BackColor="White"
                    BorderColor="#DEDFDE"
                    BorderStyle="None"
                    BorderWidth="1px"
                    CellPadding="4"
                    Font-Size="Large"                  
                    ShowFooter="True"
                    ForeColor="Black"
                    GridLines="Vertical"
                    AllowPaging="true" PageSize="20" 
                    OnRowDataBound="GridView_RPT_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                              <%--  <asp:Label ID="lblDTID" runat="server" Text='<%#Eval("DETAILKEY")%>' Visible="false"></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รายการ">
                            <ItemTemplate>
                                <asp:Label ID="lblList" runat="server" Text='<%#Eval("ProductList")%>'></asp:Label>                                                              
                            </ItemTemplate>
                        </asp:TemplateField>                    
                        <asp:TemplateField HeaderText="น้ำหนักซื้อ">
                            <ItemTemplate>
                                <asp:Label ID="lblWeight" runat="server" Text='<%#Eval("TotalWeight")%>'></asp:Label>                              
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="จำนวนเงิน">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalPrice" runat="server" Style="text-align: right" Text='<%#Eval("Totalprice")%>'></asp:Label>                              
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>

                 <br />
                <asp:ImageButton ID="btnPrint" ImageUrl="Images/printer.png" Width="40px" ToolTip="Print" runat="server" OnClick="btnPrint_Click" />

            </div>



        </div>
    </form>
</body>
</html>
