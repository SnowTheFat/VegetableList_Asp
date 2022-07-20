<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="VGTB_SHOP_LIST.ViewDataPage" %>

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
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.1/css/bootstrap.min.css" integrity="sha512-6KY5s6UI5J7SVYuZB4S/CZMyPylqyyNZco376NM2Z8Sb8OxEdp02e1jkKk/wZxIEmjQ6DRCEBhni+gpr9c4tvA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.1/js/bootstrap.min.js" integrity="sha512-ewfXo9Gq53e1q1+WDTjaHAGZ8UvCWq0eXONhwDuIoaH8xz2r96uoAYaQCm1oQhnBfRXrvJztNXFsTloJfgbL5Q==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
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

        .content {
            padding: 14px;
            font-weight: bold;
            color: white;
            font-size: 28px;
            background-color: lightseagreen;
            border-radius: 10px;
            box-shadow: 3px 3px 3px 3px #888888;
        }

        .login {
            color: white;
            text-shadow: 2px 2px #888888;
            background-color: #BDDEFF;
            border-radius: 10px;
            box-shadow: 3px 3px 3px 3px #888888;
        }

        input, select {
            font-size: 18px;
            color: darkslategray;
            border-radius: 10px;
            border-color: oldlace;
        }

            .input_text:focus, input.input_text_focus {
                border-color: #646464;
                background-color: #ffffc0;
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
    </style>

    <form id="form1" runat="server">

        <div class="container-fluid">

            <div class="container-fluid head">
                <div class="Text-heading" align="left" width="40" style="top: 16px">
                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Logo2.png" Height="45" Width="55" />
                    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>

                </div>

            </div>
            <br />

            <div class="container-fluid" style="text-align: center">
                <div class="row">
                    <div class="col-lg-6 content" border-radius: 10px;">
                        <asp:Image ID="Image2" ImageUrl="Images/vegetables.png" Width="200" runat="server" />
                        <div>
                        <asp:Label ID="lblLine1" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblLine2" runat="server"></asp:Label>   
                            </div>
                    </div>

                    <%--login--%>
                    <div class="col-lg-6 login">
                        <div class="loginHead">
                            <asp:Image ID="ImageLogin" ImageUrl="Images/Logo2.png" Width="80px" runat="server" />
                            <asp:Label ID="txtLogin" runat="server" Text="Login" Style="font-size: 60px; font-weight: bold"></asp:Label>
                        </div>
                        <br />

                        <div class="username">
                            <asp:Label ID="Label1" runat="server" Text="Username"
                                Style="padding: 10px; font-weight: bold; font-size: 34px"></asp:Label>
                            <asp:TextBox ID="txtUserName" runat="server" />
                        </div>
                        <br />

                        <div class="password">
                            <asp:Label ID="Label2" runat="server" Text="Password"
                                Style="padding: 10px; font-weight: bold; font-size: 34px"></asp:Label>
                            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <br />
                        <div>

                            <asp:Button ID="btnLogin" 
                                runat="server"
                                Text="Login"
                                Style="width: 150px; 
                                height: 40px; color: white; 
                                background-color: midnightblue; 
                                font-size: 15px; 
                                border-color: white;"
                                OnClick="btnLogin_Click"  Font-Bold="True" Font-Size="X-Large" />
            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
