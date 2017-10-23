<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <a href="#"><strong><i class="glyphicon glyphicon-envelope"></i>News</strong></a>

    <div class="row">
        <div class="col-md-12">
            <ul class="list-group">
                <li class="list-group-item">
                    <div class="alert alert-info" role="alert">
                        <span class="glyphicon glyphicon-info-sign"></span>Your Branch has/have
                        <asp:Label ID="lblSancCustCount" runat="server" Text="0" CssClass="badge"> </asp:Label>
                        customer information found for sanction screening similiarity
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <a href="#"><strong><i class="glyphicon glyphicon-dashboard"></i>My Dashboard </strong>
    </a>

    <div class="row">
        <!-- center left-->
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>
                        Notices</h4>
                </div>
                <div class="panel-body">
                    This Sanctions List Search application ("Sanctions List Search") is designed to
                    facilitate the use of the Specially Designated Nationals and Blocked Persons list
                    ("SDN List") and all other non-SDN lists, including the Foreign Sanctions Evaders
                    List, the Non-SDN Iran Sanctions Act List, the Part 561 list, the Sectoral Sanctions
                    Identifications List and the Non-SDN Palestinian Legislative Council List. Given
                    the number of lists that now reside in the Sanctions List Search tool, it is strongly
                    recommended that users pay close attention to the program codes associated with
                    each returned record. These program codes indicate how a true hit on a returned
                    value should be treated. The Sanctions List Search tool uses approximate string
                    matching to identify possible matches between word or character strings as entered
                    into Sanctions List Search, and any name or name component as it appears on the
                    SDN List and/or the various non-SDN lists. Sanctions List Search has a slider-bar
                    that may be used to set a threshold (i.e., a confidence rating) for the closeness
                    of any potential match returned as a result of a user's search. Sanctions List Search
                    will detect certain misspellings or other incorrectly entered text, and will return
                    near, or proximate, matches, based on the confidence rating set by the user via
                    the slider-bar. OFAC does not provide recommendations with regard to the appropriateness
                    of any specific confidence rating. Sanctions List Search is one tool offered to
                    assist users in utilizing the SDN List and/or the various non-SDN lists; use of
                    Sanctions List Search is not a substitute for undertaking appropriate due diligence.
                    The use of Sanctions List Search does not limit any criminal or civil liability
                    for any act undertaken as a result of, or in reliance on, such use.
                </div>
            </div>
            <!--/panel-->
        </div>
        <!--/col-span-6-->
    </div>
    <!--/row-->
    <hr>
</asp:Content>
