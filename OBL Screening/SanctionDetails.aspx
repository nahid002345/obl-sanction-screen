<%@ Page Title="Sanction Detail" Language="C#" MasterPageFile="~/SiteMaster.master"
    AutoEventWireup="true" CodeFile="SanctionDetails.aspx.cs" Inherits="SanctionDetails" %>

<asp:Content ID="cphHead" ContentPlaceHolderID="HeadContent" runat="Server">

</asp:Content>
<asp:Content ID="chpBody" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">
            Sanction Details
            <%--<input class="btn btn-success" type="button" value="Back" onclick="javascript:goBack();" style="float:right; margin-top: -5px;">--%>
            <%if (pageMode == "CTPC")
              { %>
            <a class="btn btn-success" style="float: right; margin-top: -5px;" href="CTPCSearchSanctionList.aspx?<%= Session["SText"]+"SRCHID="+ Session["SRCHID"] %>">
                Back</a>
                <%}
              else
              { %>
                 <a class="btn btn-success" style="float: right; margin-top: -5px;" href="SearchSanctionList.aspx?<%= Session["SText"]+"SRCHID="+ Session["SRCHID"] %>">
                Back</a>
                <%} %>
            <%--<a class="btn btn-success" style="float:right; margin-top: -5px;" href="#" onclick="javascript:goBack();" >Back</a>--%>
            <%--<asp:HyperLink ID="lnkMyLink" runat="server" Text="Back" CssClass="btn btn-success"  NavigateUrl="SearchSanctionList.aspx?SText=<%#  + Session["SText"].ToString()%>"></asp:HyperLink>--%>
        </div>
        <div class="panel-body">
            <div class="col-md-6">
                <table class="table table-user-information" id="leftInfo">
                    <tbody>
                        <tr>
                            <td>
                                Type:
                            </td>
                            <td>
                                <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC1Row1" runat="server" Text=""></asp:Label>
                                
                            </td>
                            <td>
                                <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC1Row2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFirstName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC1Row3" runat="server" Text=""></asp:Label>
                                
                            </td>
                            <td>
                                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC1Row4" runat="server" Text=""></asp:Label>
                                
                            </td>
                            <td>
                                <asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC1Row5" runat="server" Text=""></asp:Label>
                                
                            </td>
                            <td>
                                <asp:Label ID="lblPOB" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-md-6">
                <table class="table table-user-information" id="leftInfo">
                    <tbody>
                        <tr>
                            <td>
                                List:
                            </td>
                            <td>
                                <asp:Label ID="lblList" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC2Row1" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC2Row2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNation" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblC2Row3" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCitShip" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <asp:Label ID="lblC2Row5" runat="server" Text=""></asp:Label> 
                            </td>
                            <td>
                                <asp:Label ID="lblVOwner" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <asp:Label ID="lblC2Row4" runat="server" Text=""></asp:Label> 
                            </td>
                            <td>
                                <asp:Label ID="lblRemark" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-md-12">
                <table class="table table-user-information" id="leftInfo">
                    <tbody>
                        <tr>
                            <td>
                              <b><asp:Label ID="lblC3Row1" runat="server" Text=""></asp:Label></b> 
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblComment" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <%--    <div class="panel panel-default">
        <div class="panel-body">--%>
    <asp:GridView ID="grvAKA" runat="server" CssClass="table table-hover table-bordered table-condensed"
        GridLines="None" AutoGenerateColumns="False" ShowHeader="True" ShowHeaderWhenEmpty="false"
        AllowPaging="false" Font-Size="16px" OnRowCreated="grvAKA_RowCreated">
        <Columns>
            <%--<asp:BoundField DataField="NAME" HeaderText="Name" ItemStyle-Width="20%" />
                    <asp:BoundField DataField="type" HeaderText="Type" />--%>
            <asp:BoundField DataField="category" HeaderText="Category" />
            <asp:BoundField DataField="lastName" HeaderText="Last Name" />
            <asp:BoundField DataField="firstName" HeaderText="First Name" />
        </Columns>
        <RowStyle CssClass="cursor-pointer" />
        <PagerStyle CssClass="pagination-lg" />
    </asp:GridView>
    <%--        </div>
    </div>--%>
    <%--    <div class="panel panel-default">
        <div class="panel-body">--%>
    <asp:GridView ID="grvAddress" runat="server" CssClass="table table-hover table-bordered table-condensed"
        GridLines="None" AutoGenerateColumns="False" ShowHeader="True" AllowPaging="false"
        Font-Size="16px" OnRowCreated="grvAddress_RowCreated">
        <Columns>
            <%--<asp:BoundField DataField="NAME" HeaderText="Name" ItemStyle-Width="20%" />--%>
            <asp:BoundField DataField="address1" HeaderText="Address" />
            <asp:BoundField DataField="city" HeaderText="City" />
            <asp:BoundField DataField="stateOrProvince" HeaderText="State/Province" />
            <asp:BoundField DataField="postalCode" HeaderText="Postal Code" />
            <asp:BoundField DataField="country" HeaderText="Country" />
        </Columns>
        <RowStyle CssClass="cursor-pointer" />
        <PagerStyle CssClass="pagination-lg" />
    </asp:GridView>
    <%--        </div>
    </div>--%>
    <%--    <div class="panel panel-default">
        <div class="panel-body">--%>
    <asp:GridView ID="grvCitizen" runat="server" CssClass="table table-hover table-bordered table-condensed"
        GridLines="None" AutoGenerateColumns="False" ShowHeader="True" AllowPaging="false"
        Font-Size="16px" OnRowCreated="grvCitizen_RowCreated">
        <Columns>
            <%--<asp:BoundField DataField="NAME" HeaderText="Name" ItemStyle-Width="20%" />--%>
            <asp:BoundField DataField="country" HeaderText="Country" />
            <asp:BoundField DataField="mainEntry" HeaderText="Main Entry" />
        </Columns>
        <RowStyle CssClass="cursor-pointer" />
        <PagerStyle CssClass="pagination-lg" />
    </asp:GridView>
    <%--        </div>
    </div>--%>
    <%--    <div class="panel panel-default">
        <div class="panel-body">--%>
    <asp:GridView ID="grvID" runat="server" CssClass="table table-hover table-bordered table-condensed"
        GridLines="None" AutoGenerateColumns="False" ShowHeader="True" AllowPaging="false"
        Font-Size="16px" OnRowCreated="grvID_RowCreated">
        <Columns>
            <%--<asp:BoundField DataField="NAME" HeaderText="Name" ItemStyle-Width="20%" />--%>
            <asp:BoundField DataField="idType" HeaderText="Type" />
            <asp:BoundField DataField="idNumber" HeaderText="Number" />
            <asp:BoundField DataField="idCountry" HeaderText="Country" />
            <%--<asp:BoundField DataField="issueDate" HeaderText="Issue Date" />
            <asp:BoundField DataField="expirationDate" HeaderText="Expiration Date" />--%>
        </Columns>
        <RowStyle CssClass="cursor-pointer" />
        <PagerStyle CssClass="pagination-lg" />
    </asp:GridView>
    <%--        </div>
    </div>--%>
    <asp:GridView ID="grvNationality" runat="server" CssClass="table table-hover table-bordered table-condensed"
        GridLines="None" AutoGenerateColumns="False" ShowHeader="True" AllowPaging="false"
        Font-Size="16px" OnRowCreated="grvNationality_RowCreated">
        <Columns>
            <asp:BoundField DataField="country" HeaderText="Country" />
            <asp:BoundField DataField="mainEntry" HeaderText="Main Entry" />
        </Columns>
        <RowStyle CssClass="cursor-pointer" />
        <PagerStyle CssClass="pagination-lg" />
    </asp:GridView>
    <asp:GridView ID="grvProg" runat="server" CssClass="table table-hover table-bordered table-condensed"
        GridLines="None" AutoGenerateColumns="False" ShowHeader="True" AllowPaging="false"
        Font-Size="16px" OnRowCreated="grvProg_RowCreated">
        <Columns>
            <asp:BoundField DataField="program_Text" HeaderText="Program Detail" />
        </Columns>
        <RowStyle CssClass="cursor-pointer" />
        <PagerStyle CssClass="pagination-lg" />
    </asp:GridView>
</asp:Content>
