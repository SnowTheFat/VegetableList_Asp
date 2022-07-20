<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="VGTB_SHOP_LIST.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TAF VGTB APP</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <link href="~/cabbage_vegetables.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<script type="text/javascript" language="javascript">      
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
  $(document).ready(function () {
        $('#<%=VegetableList.ClientID%>').chosen()
    });
</script>

<body>
    <style>
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


        .insert, .saveReccord {
            padding: 10px;
            font-weight: bold;
            color: white;
            font-size: 22px;
            background-color: lightseagreen;
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

        .right_align {
            text-align: right;
        }

        input, select {
            color: darkslateblue;
            border-radius: 10px;
            border-color: darkgrey;
            font-size: 18px;
        }

        .ddl option {
            font-size: 20px;
        }
    </style>

    <form id="form1" runat="server">
        <div class="container-fluid">

            <%--Header--%>
            <div class="container-fluid head">
                <asp:ImageButton ID="Backbutton" ImageUrl="Images/undo.png" Height="35" runat="server" OnClick="Backbutton_Click" />
                <div class="Text-heading">
                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Logo2.png" Height="45" Width="55" />
                    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
                </div>
                <div style="float: right;">
                    <asp:Label ID="LblUser" runat="server" Font-Size="18px" Style="color: mediumblue" Font-Underline="True"></asp:Label>
                    <asp:ImageButton ID="Btnlogout" runat="server" OnClick="BtnLogout_Click"
                        ImageUrl="Images/logout.png" Width="40px" ToolTip="ออกจากระบบ" />
                </div>

            </div>
            <br />

            <%--insert data--%>
            <div class="container-fluid insert">

                <%--dropdown vegetable list--%>
                <asp:Label ID="ListLabel" runat="server" Text="รายการ" CssClass="input"></asp:Label>
                <asp:DropDownList ID="VegetableList" runat="server" Width="340px" Font-Size="18px">
                </asp:DropDownList>

                <%--insert price --%>
                <asp:Label ID="PriceLabel" runat="server" Text="ราคา" CssClass="input"></asp:Label>
                <asp:TextBox ID="txtPrice" runat="server" onkeypress="return isNumber(event)" Style="text-align: right" Width="100px"></asp:TextBox>


                <%--insert weight --%>
                <asp:Label ID="WeightLabel" runat="server" Text="น้ำหนัก" CssClass="input"></asp:Label>
                <asp:TextBox ID="txtWeight" runat="server" onkeypress="return isNumber(event)" Style="text-align: right" Width="100px"></asp:TextBox>

                <%--select date--%>
                <asp:Label ID="Label1" runat="server" Text="วันที่"></asp:Label>
                <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                <asp:ImageButton ID="btnDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png" OnClick="btnDate_Click" ToolTip="เลือกวันที่" Style="border-radius: unset" />
                <asp:Calendar ID="Calendar1" PopupButtonID="btnDate" runat="server"
                    OnSelectionChanged="Calendar1_SelectionChanged"
                    Visible="False"
                    BackColor="White"
                    BorderColor="#3366CC"
                    BorderWidth="1px"
                    CellPadding="1"
                    DayNameFormat="Shortest"
                    Font-Names="Verdana"
                    Font-Size="8pt"
                    ForeColor="#003399"
                    Height="200px" Width="220px" OnDayRender="Calendar1_DayRender">
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px"
                        Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>


                <%--button save data--%>
                <div style="float: right;">
                    <asp:ImageButton ID="ImageButton1" runat="server"
                        ImageUrl="Images/diskette.png" Width="40px" OnClick="ButtonSenddata_Click" ToolTip="บันทึกข้อมูล" />
                    <asp:ImageButton ID="Viewdata" runat="server"
                        ImageUrl="Images/searchdata.png" Width="40px" border-radius="unset" OnClick="BTNVIEW_Click" ToolTip="ค้นหาข้อมูล" />
                </div>
            </div>
            <br />


            <%--dispaly on input data--%>
            <div runat="server" id="disPlay" class="container-fluid DisplayInput">
                <asp:GridView ID="GridView_LIST" runat="server"
                    Width="100%"
                    AutoGenerateColumns="False"
                    BackColor="White"
                    BorderColor="#DEDFDE"
                    BorderStyle="None"
                    BorderWidth="1px"
                    CellPadding="4"
                    Font-Size="Large"
                    OnRowDataBound="GridView_LIST_RowDataBound"
                    ShowFooter="True"
                    ForeColor="Black"
                    GridLines="Vertical"
                    AllowPaging="true" PageSize="20">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                <asp:Label ID="lblDTID" runat="server" Text='<%#Eval("DETAILKEY")%>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="รายการ">
                            <ItemTemplate>
                                <asp:Label ID="lblList" runat="server" Text='<%#Eval("ProductList")%>'></asp:Label>
                                <asp:DropDownList ID="ddlList" runat="server" Visible="false"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ราคา">
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                <asp:TextBox ID="txtPrice" onkeypress="return isNumber(event)" Style="text-align: right"
                                    runat="server" Text='<%#Eval("Price") %>' Visible="false" OnTextChanged="txtPrice_TextChanged" />
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="น้ำหนักซื้อ">
                            <ItemTemplate>
                                <asp:Label ID="lblWeight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                <asp:TextBox ID="txtWeight" onkeypress="return isNumber(event)" Style="text-align: right"
                                    runat="server" Text='<%#Eval("Weight") %>' Visible="false" OnTextChanged="txtWeight_TextChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="จำนวนเงิน">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalPrice" runat="server" Style="text-align: right" Text='<%#Eval("Totalpricre")%>'></asp:Label>
                                <asp:TextBox ID="txtTotalPrice" onkeypress="return isNumber(event)" Style="text-align: right" ReadOnly="true"
                                    runat="server" Text='<%#Eval("Totalpricre") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="แก้ไข">
                            <ItemTemplate>
                                <asp:Button ID="BtnEdit" runat="server" Text="แก้ไข"
                                    Style="width: 100px; height: 30px; color: white; background-color: darkorange; font-size: 15px; border-color: white;"
                                    OnClick="BtnEdit_Click" />
                                <asp:Button ID="BtnSave" runat="server" Text="บันทึก"
                                    Style="width: 100px; height: 30px; color: white; background-color: forestgreen; font-size: 15px; border-color: white;"
                                    Visible="false" OnClick="BtnSave_Click" />
                                &nbsp;
                                <asp:Button ID="BtnCancel" runat="server" Text="ยกเลิก" OnClick="BtnCancel_Click"
                                    Style="width: 100px; height: 30px; color: white; background-color: tomato; font-size: 15px; border-color: white;"
                                    Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ลบ">
                            <ItemTemplate>
                                <asp:Button ID="BtnDelete" runat="server" Text="ลบ"
                                    Style="width: 100px; height: 30px; color: white; background-color: orangered; font-size: 15px; border-color: white;"
                                    OnClientClick="return confirm('Are you sure you want to delete?');" OnClick="BtnDelete_Click" />
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
                <asp:ImageButton ID="btnPrint" ImageUrl="Images/printer.png" Width="40px" ToolTip="Print" runat="server" OnClick="btnReport_Click" />

                <%--end nput data--%>
            </div>
        </div>
     
    </form>
</body>
</html>
