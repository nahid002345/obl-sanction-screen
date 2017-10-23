<%@ Page Title="CTPC Search Sanction List" Language="C#" MasterPageFile="~/SiteMaster.master"
    AutoEventWireup="true" CodeFile="CTPCSearchSanctionList.aspx.cs" Inherits="CTPCSearchSanctionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            var msg = $("#lblMsg").text();
            if ($("#lblMsg").text() != '')
                $("#loginAlert").css("display", "block");
            else
                $("#loginAlert").css("display", "none");

            var isVisible = $("#divAdvSearch").is(":visible");
            var txtAddressVal = $('#MainContent_txtAddress').val();
            var ddlCountryVal = $('#MainContent_ddlCountry').val();
            var txtDOBVal = $('#MainContent_txtDOB').val();
            if (txtAddressVal != '' || ddlCountryVal != '0' || txtDOBVal != '') {
                $("#divAdvSearch").toggleClass("collapse in collapse");
                $('#btnAdvSearch').text('Hide Advance Search');
            }
            else {
                //$("#divAdvSearch").toggleClass("collapse collapse in");
            }

            //            var referInfo = $('#MainContent_lblSearchID').val();
            //            if (referInfo != '' && referInfo.length != 0 ) {
            //                $("#divACOpen").toggleClass("collapse in collapse");
            //                $('#MainContent_btnReferInfo').text('Hide ReferInfo');
            //            }
            $('#MainContent_txtDOB').datepicker({
                dateFormat: 'dd-MM-yyyy',
                changeMonth: true,
                changeYear: true,
                altField: "#MainContent_txtDOBHidden",
                altFormat: "yy-mm-dd"
            });

            $('#MainContent_txtPercentage').slider({
                formatter: function (value) {
                    return 'Current value: ' + value;
                }
            });
            $("#MainContent_txtPercentage").on("slide", function (slideEvt) {
                $("#lblPNumber").text(slideEvt.value);
            });
            var SliderVal = $("#MainContent_txtPercentage").val();
            if (SliderVal != null && SliderVal != '') {
                $('#MainContent_txtPercentage').slider('setValue', SliderVal)
                $("#lblPNumber").text(SliderVal);
            }
            else {
                $('#MainContent_txtPercentage').val() = 80;
                $('#MainContent_txtPercentage').slider('setValue', SliderVal)
                $("#lblPNumber").text(SliderVal);
            }

        });

        function gridViewChange() {
        }

        function ShowHideAdvanceSearch() {
            var isVisible = $("#divAdvSearch").is(":visible");

            if (isVisible == false) {
                $('#btnAdvSearch').text('Hide Advance Search');
            }
            else {
                $('#btnAdvSearch').text('Show Advance Search');
            }

        }

        function ShowHideReferInfo() {
            var isVisible = $("#divACOpen").is(":visible");
            if (isVisible == false) {
                $('#MainContent_btnReferInfo').text('Hide ReferInfo');
            }
            else {
                $('#MainContent_btnReferInfo').text('Show ReferInfo');
            }

        }

        function ShowHideCaseInfo() {
            var isVisible = $("#CaseInfoDiv").is(":visible");
            if (isVisible == false) {
                $('#MainContent_btnCaseInfo').text('Hide CaseInfo');
            }
            else {
                $('#MainContent_btnCaseInfo').text('Show CaseInfo');
            }

        }

        $("#MainContent_ddlRefStatus").change(function () {
            var statusVal = this.value;
            if (statusVal == '2')
                $('select option:last').val()
        });
        
    </script>
</asp:Content>
<asp:Content ID="chpSearch" ContentPlaceHolderID="MainContent" runat="Server">
    <a href="SearchSanctionList.aspx"><strong><i class="glyphicon glyphicon-list-alt"></i>
        Sanction List </strong></a>
    <div class="row">
        <!-- center left-->
        <div class="col-md-12">
            <div class="alert alert-danger" id="loginAlert" role="alert" style="display: none;">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    Look Up
                </div>
                <div class="panel-body">
                    <div style="margin-bottom: 10px; width:50%; float: left;" class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>
                        <asp:TextBox ID="txtSrchName" runat="server" CssClass="form-control" placeholder="Search by Name"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 10px; width: 45%; float: right;" class="form-inline">
                        <asp:Label ID="Label11" runat="server" Text="LC Ref No" Width="20%"> </asp:Label>
                        <asp:TextBox ID="txtLCRefNo" runat="server" CssClass="form-control" placeholder="Enter LC Reference No"
                            Width="75%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtLCRefNo"
                            runat="server" Text="*" ErrorMessage="Please Enter LC Reference No" ValidationGroup="valSearch"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div style="margin-bottom: 10px; width: 100%;" class="input-group">
                        <asp:Label ID="Label6" runat="server" Text="Name Score" Width="10%"> </asp:Label>
                        <asp:TextBox ID="txtPercentage" runat="server" Text="" Width="500px" data-slider-min="70"
                            data-slider-max="100" data-slider-step="1" data-slider-value="80" data-slider-id="ex1Slider"></asp:TextBox>
                        <span id="lblPercentage">Value: <span id="lblPNumber">80</span></span>
                    </div>
                    <%--<div style="margin-bottom: 10px; width: 100%; float: left;" class="form-inline">
                        <asp:Label ID="Label12" runat="server" Text="LC Ref No" Width="10%"> </asp:Label>
                        <asp:TextBox ID="txtLCRefNo" runat="server" CssClass="form-control" placeholder="Enter LC Reference No"
                            Width="55%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtLCRefNo"
                            runat="server" Text="*" ErrorMessage="Please Enter LC Reference No" ValidationGroup="valSearch"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>--%>
                    <div style="margin-bottom: 10px; width: 50%; float: left;" class="form-inline">
                        <asp:Label ID="Label1" runat="server" Text="SDN Type" Width="20%"> </asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSdnType" placeholder="Select SDN Type"
                            Width="75%" />
                    </div>
                    <div style="margin-bottom: 10px; width: 50%; float: right;" class="form-inline">
                        <asp:Label ID="Label2" runat="server" Text="Purpose" Width="20%"> </asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPurpose" placeholder="Select Purpose"
                            Width="75%">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="CTPC" Value="CTPC"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator13" Display="Dynamic"
                            ControlToValidate="ddlPurpose" runat="server" Text="*" ErrorMessage="Please Select Purpose"
                            ValidationGroup="valSearch" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div id="divAdvSearch" class="collapse">
                        <div>
                            <div style="margin-bottom: 10px; width: 50%; float: left;" class="form-inline">
                                <asp:Label ID="Label3" runat="server" Text="Address" Width="20%"> </asp:Label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Search by Address"
                                    Width="75%"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 10px; width: 50%; float: right;" class="form-inline">
                                <asp:Label ID="Label4" runat="server" Text="Country" Width="20%"> </asp:Label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCountry" placeholder="Select Country"
                                    Width="79%" />
                            </div>
                        </div>
                        <div>
                            <div style="margin-bottom: 10px; width: 50%; float: left;" class="form-inline">
                                <asp:Label ID="Label5" runat="server" Text="Date of Birth" Width="20%"> </asp:Label>
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control clsDatePicker" placeholder="Date of Birth"
                                    Width="75%"></asp:TextBox>
                            </div>
                            <%--<div style="margin-bottom: 10px; width: 50%; float: right;" class="form-inline">
                                <asp:Label ID="Label6" runat="server" Text="Name Score" Width="25%"> </asp:Label>
                                <asp:TextBox ID="txtPercentage" runat="server" Text="" Width="300px" data-slider-min="50"
                                    data-slider-max="100" data-slider-step="1" data-slider-value="70" data-slider-id="ex1Slider"></asp:TextBox>
                                <span id="lblPercentage">Value: <span id="lblPNumber">70</span></span>
                            </div>--%>
                        </div>
                    </div>
                    <div style="margin-bottom: 0px; float: left;" class="input-group">
                        <a href="#divACOpen" id="btnReferInfo" class="btn btn-info" data-toggle="collapse"
                            runat="server" onclick="javascript:ShowHideReferInfo()">Refer Information </a>
                    </div>
                    <div style="margin-bottom: 0px; float: right;" class="input-group">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success"
                            OnClick="btnSearch_Click" OnClientClick="javascript:gridViewChange();" ValidationGroup="valSearch" />
                        <a href="#divAdvSearch" id="btnAdvSearch" class="btn btn-success" data-toggle="collapse"
                            onclick="javascript:ShowHideAdvanceSearch()">Show Advance Search </a>
                    </div>
                </div>
                <div id="divACOpen" class="collapse">
                    <div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Enter Refer Information
                            </div>
                            <div class="panel-body">
                                <asp:Label ID="lblSearchID" runat="server" Width="20%" Visible="false"> </asp:Label>
                                <asp:Label ID="lblReferID" runat="server" Width="20%" Visible="false"> </asp:Label>
                                <div style="margin-bottom: 10px; width: 50%; float: left;" class="form-inline">
                                    <asp:Label ID="Label8" runat="server" Text="Refer To" Width="20%"> </asp:Label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlReferTo" placeholder="Select Refer To"
                                        Width="75%" />
                                    <asp:RequiredFieldValidator InitialValue="0" ID="referToVal" Display="Dynamic" ControlToValidate="ddlReferTo"
                                        runat="server" Text="*" ErrorMessage="Please Select Refer To" ValidationGroup="valReferInfo"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div style="margin-bottom: 10px; width: 50%; float: left;" class="form-inline">
                                    <asp:Label ID="Label9" runat="server" Text="Refer Status" Width="20%"> </asp:Label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRefStatus" placeholder="Select Refer To"
                                        Width="75%">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Request" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Recommend" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Approve" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Refuse" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" ID="referStatusVal" Display="Dynamic"
                                        ControlToValidate="ddlRefStatus" runat="server" Text="*" ErrorMessage="Please Select Refer Status"
                                        ValidationGroup="valReferInfo" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div style="margin-bottom: 10px; width: 50%; float: left;" class="form-inline">
                                    <asp:Label ID="Label10" runat="server" Text="Refer Remarks" Width="30%"> </asp:Label>
                                    <asp:TextBox ID="txtReferRemark" runat="server" TextMode="MultiLine" CssClass="form-control"
                                        placeholder="Enter Refer Remarks (if any)" Enabled="true" Width="60%"></asp:TextBox>
                                </div>
                                <div style="margin-bottom: 10px; width: 50%; float: left;" class="form-inline">
                                    <asp:Label ID="Label7" runat="server" Text="A/C Open By" Width="20%"> </asp:Label>
                                    <asp:TextBox ID="txtACOpen" runat="server" CssClass="form-control" placeholder="Enter Info of A/C Open By"
                                        Enabled="false" Width="75%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtACOpen"
                                        Enabled="false" ErrorMessage="*" ForeColor="Red" ToolTip="Required" ValidationGroup="valReferInfo"></asp:RequiredFieldValidator>
                                </div>
                                <div style="margin-bottom: 10px; width: 10%; float: right;" class="form-inline">
                                    <asp:Button ID="btnSuspSave" runat="server" Text="Save" CssClass="btn btn-info" ValidationGroup="valReferInfo"
                                        OnClick="btnSuspSave_Click" Enabled="false" />
                                </div>
                                <div style="margin-bottom: 0px; float: right;" class="input-group">
                                    <a href="#CaseInfoDiv" id="btnCaseInfo" class="btn btn-info" data-toggle="collapse"
                                        runat="server" onclick="javascript:ShowHideCaseInfo()">Case Information </a>
                                </div>
                            </div>
                        </div>
                        <div id="CaseInfoDiv" class="collapse">
                            <asp:GridView ID="gvCaseInfo" runat="server" CssClass="table table-hover table-bordered table-condensed"
                                AutoGenerateColumns="false" ShowHeader="True" EmptyDataText="No data found!"
                                DataKeyNames="ID" ShowHeaderWhenEmpty="True" Font-Size="16px" AllowPaging="false"
                                PageSize="20" OnPageIndexChanging="gvCaseInfo_PageIndexChanging" OnRowDataBound="gvCaseInfo_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="10%" Visible="false" />
                                    <asp:BoundField DataField="RFNAME" HeaderText="REFER_FROM" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="RTNAME" HeaderText="REFER_TO" ItemStyle-Width="5%" />
                                    <asp:BoundField DataField="RTDEPT" HeaderText="REFER_TO_DEPT" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="REFERDATE" HeaderText="REFER_DATE" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="REFERSTATUS" HeaderText="REFER_STATUS" ItemStyle-Width="20%" />
                                    <%--<asp:BoundField DataField="ISOPEN" HeaderText="ACTIVE?" ItemStyle-Width="10%" />--%>
                                </Columns>
                                <RowStyle CssClass="cursor-pointer" />
                                <PagerSettings PageButtonCount="5" />
                                <PagerStyle CssClass="pagination-lg" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="alert alert-success" id="MsgSuccess" role="alert" style="display: none;
                    width: 100%;">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <asp:Label ID="lblRefNo" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                </div>
                <!-- Button -->
            </div>
            <!--Search panel-->
            <!--grid panel-->
            <div class="panel panel-default">
                <div class="panel-heading" style="height: 50px;">
                    <div style="float: left;">
                        <span class="glyphicon glyphicon-list-alt"></span>Look Up Results</div>
                    <div style="float: right;">
                        <asp:Label ID="lblResultCount" runat="server" Text="(0 result shown)"> </asp:Label>
                        <span class="glyphicon glyphicon-stats"></span>
                    </div>
                </div>
                <div class="panel-body">
                    <div>
                        <table id="gridSanctionList_header" class="table table-hover table-bordered table-condensed"
                            cellspacing="0" style="border-collapse: collapse; font-size: 16px; width: 99.5%;
                            margin-bottom: 2px;">
                            <tbody>
                                <tr>
                                    <th scope="col" style="width: 20%;">
                                        Name
                                    </th>
                                    <th scope="col" style="width: 52%;">
                                        Address
                                    </th>
                                    <%--                                    <th scope="col" style="width: 16.6%;">
                                        P Type
                                    </th>--%>
                                    <th scope="col" style="width: 10%;">
                                        SDN Type
                                    </th>
                                    <th scope="col" style="width: 10%;">
                                        List Type
                                    </th>
                                    <%--
                                    <th scope="col" style="width: 7%;">
                                        Score
                                    </th>--%>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="max-height: 300px; overflow: scroll; overflow-x: hidden;">
                        <asp:GridView ID="gridSanctionList" runat="server" CssClass="table table-hover table-bordered table-condensed"
                            GridLines="None" AutoGenerateColumns="False" ShowHeader="false" EmptyDataText="No data found!"
                            ShowHeaderWhenEmpty="false" AllowPaging="false" Font-Size="16px" OnPageIndexChanging="gridSanctionList_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="SDN_ID" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <a href="SanctionDetails.aspx?Id=<%# Eval("SDNENTRY_ID")%> &Type=<%# Eval("LISTED_BY")%> &SType=<%# Eval("SDNTYPE")%>&SRCHID=<%=lblSearchID.Text%>&MOD=CTPC">
                                            <%# Eval("NAME")%></a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="NAME" HeaderText="Name" ItemStyle-Width="20%" />--%>
                                <asp:BoundField DataField="ADDRESS" HeaderText="Address" ItemStyle-Width="40%" />
                                <%--<asp:BoundField DataField="PTYPE" HeaderText="P Type" ItemStyle-Width="15%" />--%>
                                <asp:BoundField DataField="SDNTYPE" HeaderText="SDN Type" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="LISTED_BY" HeaderText="List Type" ItemStyle-Width="7%" />
                                <%--<asp:BoundField DataField="MATCHING_PERCENTAGE" HeaderText="Score" ItemStyle-Width="5%" />--%>
                            </Columns>
                            <RowStyle CssClass="cursor-pointer" />
                            <PagerSettings PageButtonCount="5" />
                            <PagerStyle CssClass="pagination-lg" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <!--/col-span-6-->
    </div>
</asp:Content>
