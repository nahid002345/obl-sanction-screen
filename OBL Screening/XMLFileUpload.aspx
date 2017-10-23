<%@ Page Title="XML FILE UPLOAD" Language="C#" MasterPageFile="~/SiteMaster.master"
    AutoEventWireup="true" CodeFile="XMLFileUpload.aspx.cs" Inherits="XMLFileUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //        var progress = $(".loading-progress").progressTimer({
        //            timeLimit: 10,
        //            onFinish: function () {
        //                alert('completed!');
        //            }
        //        });
        //        $.ajax({
        //            url: "XMLFileUpload.aspx/GetData"
        //        }).error(function () {
        //            progress.progressTimer('error', {
        //                errorText: 'ERROR!',
        //                onFinish: function () {
        //                    alert('There was an error processing your information!');
        //                }
        //            });
        //        }).done(function () {
        //            progress.progressTimer('complete');
        //        });

        //                $(document).ready(function () {
        //                    var progresspump = setInterval(function () {
        //                        $.ajax({
        //                            type: "POST",
        //                            url: "XMLFileUpload.aspx/GetData",
        //                            data: '{}',
        //                            contentType: "application/json; charset=utf-8",
        //                            dataType: "json",
        //                            success: function (data) {
        //                                $("#MainContent_progress").css("width", data.d + "%")
        //                                //alert(data.d);
        //                            },
        //                            failure: function (response) {
        //                                alert(response.d);
        //                            }
        //                        });
        //                    }, 1000);
        //                });

        //        function UpdateProgressBar(percentage) {
        //            //document.getElementById("#MainContent_bar1").style.width = percentage + "%";
        //            //$("#MainContent_bar1").css("width", percentage+"%");
        //        }

        $("#inputFileUpload").fileinput({ allowedFileExtensions: ["xml"] });


        var validFilesTypes = ["xml"];

        function CheckExtension(file) {

            var filePath = file.value;
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
            var isValidFile = false;

            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }

            if (!isValidFile) {
                file.value = null;
                alert("Invalid File. Valid extensions are:\n\n" + validFilesTypes.join(", "));
            }

            return isValidFile;
        }
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
                    XML File Upload
                </div>
                <div class="panel-body">
                    <div class="form-inline">
                        <asp:Label ID="Label3" runat="server" Text="Type" Width="15%"> </asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlType" placeholder="Select Type"
                            Width="25%">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="OFAC File" Value="1"></asp:ListItem>
                            <asp:ListItem Text="UN File" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator11" Display="Dynamic"
                                ValidationGroup="valXmlUpload" ControlToValidate="ddlType" runat="server" Text="*"
                                ErrorMessage="Please Select Type" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-inline">
                        <asp:Label ID="Label1" runat="server" Text=" Upload File" Width="15%"> </asp:Label>
                        <input id="inputFileUpload" runat="server" type="file" class="file" data-show-preview="false"
                            data-show-upload="false" accept=".xml" onchange="javascript:return CheckExtension(this);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inputFileUpload"
                                ErrorMessage="*" ForeColor="Red" ToolTip="Required" ValidationGroup="valXmlUpload"></asp:RequiredFieldValidator>
                        <%--<asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-success"
                            Width="5%" OnClick="btnUpload_Click" />--%>
                        <asp:Button ID="btnUploadFile" runat="server" Text="Upload File" CssClass="btn btn-success" ValidationGroup="valXmlUpload"
                            OnClick="btnUploadFile_Click" />
                    </div>

                    <%--<span>Progress</span>
                    <div class="progress progress-striped">
                        <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="0"
                            aria-valuemin="0" aria-valuemax="100" id="progress" runat="server">
                        </div>
                    </div>
                    <div class="loading-progress">
                    </div>--%>
                </div>
                <!-- Button -->
            </div>
        </div>
</asp:Content>
