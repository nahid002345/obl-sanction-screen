<%@ Page Title="Customer Sanction Screening List" Language="C#" MasterPageFile="~/SiteMaster.master"
    AutoEventWireup="true" CodeFile="CustSancScreening.aspx.cs" Inherits="CustSancScreening" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
                    <div class="form-inline">
                        <asp:Label ID="Label3" runat="server" Text="Customer NO" Width="15%"> </asp:Label>
                        <asp:TextBox ID="txtSrchCustNo" runat="server" CssClass="form-control" Width="25%"
                            placeholder="Enter Customer NO to search"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" Text=" Customer Name" Width="15%"> </asp:Label>
                        <asp:TextBox ID="txtSrchName" runat="server" CssClass="form-control" Width="25%"
                            placeholder="Enter Customer Name to search"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success"
                            Width="10%" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <!-- Button -->
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-list-alt"></span>Customer Information List
                    <div style="float: right;">
                        <asp:Label ID="lblResultCount" runat="server" Text=""> </asp:Label><asp:Label ID="Label1"
                            runat="server" Text=" result/s found"> </asp:Label>
                        <span class="glyphicon glyphicon-stats"></span>
                    </div>
                </div>
                <div class="panel-body">
                    <%--<div class="alert alert-info" role="alert">
                        <span class="glyphicon glyphicon-info-sign"></span>Please Remember Only <span class="glyphicon glyphicon-check">
                        </span>Checked Item Will Generate File From List. <span class="glyphicon glyphicon-alert"></span> Uncheked Item Has Already Used in File Generation.
                    </div>--%>
                    <div class="form-inline">
                        <asp:GridView ID="grvSancCustInfo" runat="server" CssClass="table table-hover table-bordered table-condensed"
                            GridLines="None" AutoGenerateColumns="False" ShowHeader="True" EmptyDataText="No data found!"
                            DataKeyNames="ID" ShowHeaderWhenEmpty="True" Font-Size="16px" AllowPaging="True" PageSize="20"
                            OnPageIndexChanging="grvSancCustInfo_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="10%" Visible="false" />
                                <asp:BoundField DataField="CUSTOMER_NO" HeaderText="CUSTOMER_NO" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="CUSTOMER_TYPE" HeaderText="TYPE" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="CUSTOMER_NAME1" HeaderText="CUSTOMER_NAME" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="LOCAL_BRANCH_CODE" HeaderText="LOCAL_BRANCH" ItemStyle-Width="10%" />
                                <asp:TemplateField HeaderText="ACTION" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <a href="SearchSanctionList.aspx?NAME=<%# Eval("CUSTOMER_NAME1")%>&PERCENTAGE=75"
                                            class="btn btn-success">SHOW</a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
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
