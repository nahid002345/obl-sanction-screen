<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Index.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="OBL_Screening._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%--<div class="container">
        <div class="row">
            <div class="col-lg-12">
                <img class="img-responsive" src="Images/obllogo.gif" alt="" />
            </div>
        </div>
    </div>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <a href="#"><strong><i class="glyphicon glyphicon-dashboard"></i>My Dashboard</strong></a>
    <hr>
    <div class="row">
        <!-- center left-->
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>
                        Notices</h4>
                </div>
                <div class="panel-body">
                    <div class="alert alert-info">
                        <button type="button" class="close" data-dismiss="alert">
                            ×</button>
                        This is a dismissable alert.. just sayin'.
                    </div>
                    This is a dashboard-style layout that uses Bootstrap 3. You can use this template
                    as a starting point to create something more unique.
                    <br>
                    <br>
                    Visit the Bootstrap Playground at <a href="http://bootply.com">Bootply</a> to tweak
                    this layout or discover more useful code snippets.
                </div>
            </div>
            <!--/panel-->
        </div>
        <!--/col-span-6-->
    </div>
    <!--/row-->
    <hr>
    <a href="#"><strong><i class="glyphicon glyphicon-comment"></i>Discussions</strong></a>
    <hr>
    <div class="row">
        <div class="col-md-12">
            <ul class="list-group">
                <li class="list-group-item"><a href="#"><i class="glyphicon glyphicon-flash"></i><small>
                    (3 mins ago)</small> The 3rd page reports don't contain any links. Does anyone know
                    why..</a></li>
                <li class="list-group-item"><a href="#"><i class="glyphicon glyphicon-flash"></i><small>
                    (1 hour ago)</small> Hi all, I've just post a report that show the relationship
                    betwe..</a></li>
                <li class="list-group-item"><a href="#"><i class="glyphicon glyphicon-heart"></i><small>
                    (2 hrs ago)</small> Paul. That document you posted yesterday doesn't seem to contain
                    the over..</a></li>
                <li class="list-group-item"><a href="#"><i class="glyphicon glyphicon-heart-empty"></i>
                    <small>(4 hrs ago)</small> The map service on c243 is down today. I will be fixing
                    the..</a></li>
                <li class="list-group-item"><a href="#"><i class="glyphicon glyphicon-heart"></i><small>
                    (yesterday)</small> I posted a new document that shows how to install the services
                    layer..</a></li>
                <li class="list-group-item"><a href="#"><i class="glyphicon glyphicon-flash"></i><small>
                    (yesterday)</small> ..</a></li>
            </ul>
        </div>
    </div>
    
    <!-- /Main -->
    <footer class="text-center">This Bootstrap 3 dashboard layout is compliments of <a href="http://www.bootply.com/85850"><strong>Bootply.com</strong></a></footer>
    <!-- /.modal -->
</asp:Content>
