<%@ Page Title="Suspicious Search List" Language="C#" MasterPageFile="~/SiteMaster.master"
    AutoEventWireup="true" CodeFile="SuspiciousList.aspx.cs" Inherits="SuspiciousList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContent_txtFromDate').datepicker({
                dateFormat: 'dd-MM-yyyy',
                changeMonth: true,
                changeYear: true,
                altField: "#MainContent_txtDOBHidden",
                altFormat: "yy-mm-dd"
            }).on('changeDate', function (e) {
                $(this).datepicker('hide');
            });

            $('#MainContent_txtToDate').datepicker({
                dateFormat: 'dd-MM-yyyy',
                changeMonth: true,
                changeYear: true,
                altField: "#MainContent_txtDOBHidden",
                altFormat: "yy-mm-dd"
            }).on('changeDate', function (e) {
                $(this).datepicker('hide');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="chpSearch" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <!-- center left-->
        <div class="col-md-12">
            <div class="alert alert-danger" id="loginAlert" role="alert" style="display: none;
                width: 100%;">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="alert alert-success" id="MsgSuccess" role="alert" style="display: none;
                width: 100%;">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <asp:Label ID="lblSuccessMsg" runat="server" ForeColor="Green"></asp:Label>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    Search
                </div>
                <div class="panel-body">
                    <div class="alert alert-warning alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <div>
                            <span class="glyphicon glyphicon-tag"></span>Date Format MUST be <b>(DD/MM/YYYY)</b>
                        </div>
                    </div>
                    <div class="form-inline">
                        <asp:Label ID="Label3" runat="server" Text="From Date" Width="15%"> </asp:Label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="25%"
                            placeholder="Choose From Date"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" Text=" To Date" Width="15%"> </asp:Label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="25%" placeholder="Choose To Date"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success"
                            Width="10%" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <!-- Button -->
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-list-alt"></span>Search Suspicious Search List
                    <div style="float: right;">
                        <asp:Label ID="lblResultCount" runat="server" Text=""> </asp:Label><asp:Label ID="Label1"
                            runat="server" Text=" result/s found"> </asp:Label>
                        <span class="glyphicon glyphicon-stats"></span>
                    </div>
                </div>
                <div class="panel-body" style="overflow-x: scroll;">
                    <%--<div class="alert alert-info" role="alert">
                        <span class="glyphicon glyphicon-info-sign"></span>Please Remember Only <span class="glyphicon glyphicon-check">
                        </span>Checked Item Will Generate File From List. <span class="glyphicon glyphicon-alert"></span> Uncheked Item Has Already Used in File Generation.
                    </div>--%>
                    <div class="form-inline">
                        <asp:GridView ID="gvSearchLog" runat="server" CssClass="table table-hover table-bordered table-condensed"
                            GridLines="None" AutoGenerateColumns="False" ShowHeader="True" EmptyDataText="No data found!"
                            DataKeyNames="ID" ShowHeaderWhenEmpty="True" Font-Size="16px" AllowPaging="True"
                            PageSize="20" OnPageIndexChanging="gvSearchLog_PageIndexChanging" OnRowDataBound="gvSearchLog_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="10%" Visible="false" />
                                <%--<asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="10%" />--%>
                                <asp:TemplateField HeaderText="Detail" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <a href="SearchSanctionList.aspx?<%# Eval("SearchDetail")%>&SRCHID=<%# Eval("SearchRefNo")%>"
                                            class="btn btn-success">SHOW</a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SearchRefNo" HeaderText="Reference No" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="IsSuspicious" HeaderText="Suspicious?" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="ACOpenBy" HeaderText="A/C Open By" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="EmpBranchCode" HeaderText="Branch Code" ItemStyle-Width="10%" />
                                <%--<asp:BoundField DataField="Purpose" HeaderText="Purpose" ItemStyle-Width="10%" />--%>
                                <asp:BoundField DataField="SearchDate" HeaderText="    Date" ItemStyle-Width="20%" />
                                <%--<asp:BoundField DataField="SearchDetail" HeaderText="Search Detail" ItemStyle-Width="10%" />--%>
                                <%--<asp:BoundField DataField="SearchCompIP" HeaderText="IP Address" ItemStyle-Width="10%" />--%>
                            </Columns>
                            <RowStyle CssClass="cursor-pointer" />
                            <PagerSettings PageButtonCount="5" />
                            <PagerStyle CssClass="pagination-lg" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <!--/col-span-6-->
        </div>
</asp:Content>
