﻿<%@ Page Title="Employee Refer Info List" Language="C#" MasterPageFile="~/SiteMaster.master"
    AutoEventWireup="true" CodeFile="ReferInfoList.aspx.cs" Inherits="ReferInfoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#referTab a").click(function (e) {
                e.preventDefault();
                $(this).tab('show');
            });

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

            $('#MainContent_txtTaskFromDate').datepicker({
                dateFormat: 'dd-MM-yyyy',
                changeMonth: true,
                changeYear: true,
                altField: "#MainContent_txtDOBHidden",
                altFormat: "yy-mm-dd"
            }).on('changeDate', function (e) {
                $(this).datepicker('hide');
            });

            $('#MainContent_txtTaskToDate').datepicker({
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
            <div class="bs-example">
                <ul class="nav nav-tabs" data-tabs="tabs" id="referTab">
                    <li class="active"><a data-toggle="tab" href="#referList">Refer List</a></li>
                    <li><a data-toggle="tab" href="#reqReferList">Requested Refer</a></li>
                </ul>
                <div class="tab-content">
                    <div id="referList" class="tab-pane fade in active">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                Refer Task List
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
                                    <asp:Label ID="Label4" runat="server" Text="From Date" Width="15%"> </asp:Label>
                                    <asp:TextBox ID="txtTaskFromDate" runat="server" CssClass="form-control" Width="25%"
                                        placeholder="Choose From Date"></asp:TextBox>
                                    <asp:Label ID="Label5" runat="server" Text=" To Date" Width="15%"> </asp:Label>
                                    <asp:TextBox ID="txtTaskToDate" runat="server" CssClass="form-control" Width="25%"
                                        placeholder="Choose To Date"></asp:TextBox>
                                    <asp:Button ID="btnSearchTask" runat="server" Text="Search" CssClass="btn btn-success"
                                        Width="10%" OnClick="btnSearchTask_Click" />
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <span class="glyphicon glyphicon-list-alt"></span>Search Refer Task List
                                    <div style="float: right;">
                                        <asp:Label ID="lblTaskCount" runat="server" Text=""> </asp:Label><asp:Label ID="Label7"
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
                                        <asp:GridView ID="gvReferTask" runat="server" CssClass="table table-hover table-bordered table-condensed"
                                            GridLines="None" AutoGenerateColumns="False" ShowHeader="True" EmptyDataText="No data found!"
                                            DataKeyNames="ID" ShowHeaderWhenEmpty="True" Font-Size="16px" AllowPaging="True"
                                            PageSize="20" OnPageIndexChanging="gvReferTask_PageIndexChanging" OnRowDataBound="gvReferTask_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="10%" Visible="false" />
                                                <%--<asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="10%" />--%>
                                                <asp:TemplateField HeaderText="Detail" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%--<a href="SearchSanctionList.aspx?<%# Eval("SearchDetail")%>&SRCHID=<%# Eval("SearchRefNo")%>"
                                                class="btn btn-success">SHOW</a>--%>
                                                        <a href="<%# (Eval("LCRefNo") != null) ? string.Format("CTPCSearchSanctionList.aspx?{0}&SRCHID={1}",Eval("SearchDetail"), Eval("SearchRefNo")): string.Format("SearchSanctionList.aspx?{0}&SRCHID={1}",Eval("SearchDetail"), Eval("SearchRefNo"))%>"
                                                            class="btn btn-success">SHOW</a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="INITIATORNAME" HeaderText="REQUESTED BY" ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="SearchRefNo" HeaderText="Reference No" ItemStyle-Width="10%" />
                                                <%--<asp:BoundField DataField="ACOpenBy" HeaderText="A/C Open By" ItemStyle-Width="20%" />--%>
                                                <asp:BoundField DataField="EmpBranchCode" HeaderText="Branch Code" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="SearchDate" HeaderText="Date" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="SearchDetail" HeaderText="Search Detail" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="LASTREFERSTATUS" HeaderText="Status" ItemStyle-Width="10%" />
                                                <%--<asp:BoundField DataField="SearchCompIP" HeaderText="IP Address" ItemStyle-Width="10%" />--%>
                                            </Columns>
                                            <RowStyle CssClass="cursor-pointer" />
                                            <PagerSettings PageButtonCount="5" />
                                            <PagerStyle CssClass="pagination-lg" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <!-- Button -->
                        </div>
                    </div>
                    <div id="reqReferList" class="tab-pane fade">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Requested Refer List
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
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span class="glyphicon glyphicon-list-alt"></span>Search Requested Refer List
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
                                        <asp:GridView ID="gvRequestedRefer" runat="server" CssClass="table table-hover table-bordered table-condensed"
                                            GridLines="None" AutoGenerateColumns="False" ShowHeader="True" EmptyDataText="No data found!"
                                            DataKeyNames="ID" ShowHeaderWhenEmpty="True" Font-Size="16px" AllowPaging="True"
                                            PageSize="20" OnPageIndexChanging="gvRequestedRefer_PageIndexChanging" OnRowDataBound="gvRequestedRefer_RowDataBound">
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
                                                <%--<asp:BoundField DataField="INITIATORNAME" HeaderText="REQUESTED BY" ItemStyle-Width="15%" />--%>
                                                <asp:BoundField DataField="SearchRefNo" HeaderText="Reference No" ItemStyle-Width="10%" />
                                                <%--<asp:BoundField DataField="ACOpenBy" HeaderText="A/C Open By" ItemStyle-Width="20%" />--%>
                                                <asp:BoundField DataField="EmpBranchCode" HeaderText="Branch Code" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="SearchDate" HeaderText="Date" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="SearchDetail" HeaderText="Search Detail" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="ReferStatus" HeaderText="Status" ItemStyle-Width="10%" />
                                                <%--<asp:BoundField DataField="SearchCompIP" HeaderText="IP Address" ItemStyle-Width="10%" />--%>
                                            </Columns>
                                            <RowStyle CssClass="cursor-pointer" />
                                            <PagerSettings PageButtonCount="5" />
                                            <PagerStyle CssClass="pagination-lg" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <!-- Button -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>