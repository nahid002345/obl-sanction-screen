<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OBL Screening</title>
    <link href="~/Styles/BootStrap/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/BootStrap/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/site.css" rel="Stylesheet" type="text/css" />
    <link href="~/Styles/custom.css" rel="Stylesheet" type="text/css" />
    <link href="~/Styles/jquery-ui-1.11.3.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/jquery-ui.min-1.11.3.css" rel="stylesheet" type="text/css" />
    <%--jquery version 1.11.2--%>
    <script language="javascript" type="text/javascript" src="Scripts/jquery-1.11.2.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/jquery-1.11.2.min.js"></script>
    <%--jquery bootstrap--%>
    <script language="javascript" type="text/javascript" src="Scripts/bootstrap.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <%--<script language="javascript" type="text/javascript" src="Scripts/npm.js"></script>--%>
    <script language="javascript" type="text/javascript" src="Scripts/custom.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/respond.js"></script>
    <%--jquery UI 1.11.3--%>
    <script type="text/javascript" src="Scripts/jquery-ui-1.11.3.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui.min-1.11.3.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var msg = $("#lblMsg").text();
            if ($("#lblMsg").text() != '')
                $("#loginAlert").css("display", "block");
            else
                $("#loginAlert").css("display", "none");
        });
    </script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-lg-12" style="padding: 10px 10px 10px 10px;">
                <img class="img-responsive" src="Images/obllogo.png" alt="" />
            </div>
        </div>
    </div>
    <form id="frmLogin" runat="server" class="form-horizontal" role="form">
    <div>
        <div class="container">
            <div id="loginbox" style="margin-top: 50px;" class="mainbox col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title">
                            Log In</div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <%--                        <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                        </div>--%>
                        <asp:ValidationSummary runat="server" ID="ValidationSummary1" DisplayMode="BulletList"
                            ShowMessageBox="False" ShowSummary="True" CssClass="alert alert-danger" />
                        <div style="margin-bottom: 25px" class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" placeholder="Employee ID"></asp:TextBox>
                        </div>
                        <div style="margin-bottom: 25px" class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password"
                                name="password" TextMode="Password"></asp:TextBox>
                            <%--<input id="txtPassword" type="password" class="form-control" name="password" placeholder="password">--%>
                        </div>
                        <!-- Button -->
                        <div style="margin-bottom: 0px; float: right;" class="input-group">
                            <asp:Button ID="btnLogIn" runat="server" Text="Login" CssClass="btn btn-success"
                                OnClick="btnLogIn_Click" OnClientClick="javascript:showAlert();" />
                        </div>
                    </div>
                    <div class="alert alert-danger" id="loginAlert" role="alert" style="display: none;">
                        <div id="rfvMsg" style="display: none;">
                            <asp:RequiredFieldValidator ID="rfvLoginID" runat="server" ControlToValidate="txtEmpId"
                                ErrorMessage="Employee ID is required." ToolTip="Employee ID is required."></asp:RequiredFieldValidator><br />
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Password is required." ToolTip="Password is required."></asp:RequiredFieldValidator>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
        <div class="col-lg-12">
            <div class="alert alert-warning alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <div style="padding-left: 35%;">
                    Request to Operate application by <a href="Browser/SetupChrome.exe" class="alert-link">
                        <i class="glyphicon glyphicon-download-alt"></i> Google Chrome</a> or <a href="Browser/SetupFirefox.exe"
                            class="alert-link"><i class="glyphicon glyphicon-download-alt"></i> Mozilla Firefox</a></div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
