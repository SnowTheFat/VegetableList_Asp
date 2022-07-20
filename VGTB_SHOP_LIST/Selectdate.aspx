<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Selectdate.aspx.cs" Inherits="VGTB_SHOP_LIST.Selectdate" %>

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
            height: 80px;
            font-weight: bold;
            border-color: #92a8d1;
            box-shadow: 3px 3px 3px 3px #888888;
        }

        .Text-heading {
            color: white;
            font-size: 28px;
            text-shadow: 2px 2px #888888;
            margin: unset;
            padding: 14px;
        }

        input, button, select, textarea {
            border-radius: 10px
        }

            button, html input[type="button"], input[type="reset"], input[type="submit"] {
                width: 100px;
                /*height: 38px;*/
                color: White;
                background-color: #3D79B6;
                border-color: #3D79B6;
                font-size: large;
                border-radius: 10px
            }

        .caption {
            color: White;
            font-weight: bold;
            text-align: center;
            font-size: 30px;
            text-shadow: 2px 2px #888888;
        }
    </style>
    <form id="form1" runat="server">

        <div class="container-fluid">
            <div class="container-fluid head">
                <div class="Text-heading " align="left" width="40" style="top: 16px">
                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Logo2.png" Height="45" Width="55" />
                    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>

                   <div style="float: right; text-shadow: 0 0 black">
                    <asp:Label ID="LblUser" runat="server" Font-Size="18px" Style="color: mediumblue" Font-Underline="True"></asp:Label>
                    <asp:ImageButton ID="Btnlogout" runat="server" OnClick="BtnLogout_Click"
                        ImageUrl="Images/logout.png" Width="40px" ToolTip="ออกจากระบบ" />
                </div>
                </div>
            </div>
            <br />

            <div class="container-fluid" style="background-color: skyblue; border-radius: 10px; box-shadow: 3px 3px 3px 3px #888888;">
                <div style="display: table; margin: 0 auto;">
                    <asp:Label ID="calencarCap" CssClass="caption" runat="server" Text="เลือกวันที่ ที่ต้องการจะบันทึกข้อมูล" Font-Underline="True"></asp:Label>
                    <br />
                    <asp:Calendar ID="CalendarJob"
                        runat="server"
                        BackColor="White"
                        BorderWidth="1px"
                        DayNameFormat="Full"
                        Font-Names="Verdana"
                        Font-Size="12pt"
                        ForeColor="Black"
                        Height="220px"
                        NextPrevFormat="FullMonth"
                        OnDayRender="CalendarJob_DayRender"
                        TitleFormat="Month"
                        Width="400px"
                        CaptionAlign="Top" OnSelectionChanged="CalendarJob_SelectionChanged" Font-Bold="True">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                        <DayStyle Width="14%" BorderColor="Gray" BorderStyle="Inset" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#0000cc" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                        <TodayDayStyle BackColor="#FF3300" ForeColor="White" BorderStyle="Inset" />
                        <WeekendDayStyle BackColor="#CCCCCC" BorderStyle="Solid" />
                    </asp:Calendar>

                    <br />
                </div>
                <div style="text-align: center; font-size: 18px; color: black; font-weight: bold; padding-bottom: 14px">
                    <asp:Image ID="Image2" ImageUrl="Images/Daydata.png" Width="60" runat="server" />
                    <asp:Label ID="Label3" runat="server" Text="วันที่ ที่มีการบันทึกข้อมูลแล้ว"></asp:Label>
                
                    <asp:Image ID="Image3" ImageUrl="Images/Daynodata.png" Width="60" runat="server" />
                    <asp:Label ID="Label4" runat="server" Text="วันที่ ที่ยังไม่มีการบันทึกข้อมูล"></asp:Label>
                </div>
            </div>
            <br />

            <div class="container-fluid" style="background-color: skyblue; border-radius: 10px; padding: 10px; box-shadow: 3px 3px 3px 3px #888888;">
                <div style="text-align: center; font-size: 18px; color: black; font-weight: bold">
                    <div style="color: tomato">
                        <asp:Label ID="txtMonthRPT" runat="server" Font-Size="26px" Text="หากต้องการพิมพ์รายงานประจำเดือน ให้คลิกที่นี่!!"></asp:Label>
                        <asp:Button ID="MonthRPT" 
                            runat="server" 
                            Text="รายงานประจำเดือน"
                            Style="width: 150px; 
                            height: 40px; color: white; 
                            background-color: midnightblue; 
                            font-size: 15px; 
                            border-color: white;"
                            OnClick="ImageMonthRPT_Click" />
                    </div>
                    <asp:Label ID="lbljobPick" runat="server" Visible="False"></asp:Label>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
