<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TwitterMap.aspx.cs" Inherits="comScoreSocialDashboard.TwitterMap"
    MasterPageFile="~/Master.master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderBody" ID="Content1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <script type="text/javascript">
            try {
                ace.settings.check('breadcrumbs', 'fixed');
            } catch (e) { }
        </script>

        <ul class="breadcrumb">
            <li>
                <i class="icon-home home-icon"></i>
                <a href="#">Home</a>
            </li>
            <li class="active">Dashboard</li>
        </ul>
        <!-- .breadcrumb -->

        <div class="nav-search" id="nav-search">
            <form class="form-search">
                <span class="input-icon">
                    <input type="text" placeholder="Search ..." class="nav-search-input" id="nav-search-input" autocomplete="off" />
                    <i class="icon-search nav-search-icon"></i>
                </span>
            </form>
        </div>
        <!-- #nav-search -->
    </div>




    <div class="page-content">
        <div class="page-header">
            <h1>Twitter Map
								<small>
                                    <i class="icon-double-angle-right"></i>
                                    Twitter + Google Map
                                </small>
            </h1>
        </div>
            <div class="row">
            <div class="col-xs-12">

            <div id="map-canvas"></div>
            </div>
            </div>
        </div>
     

    <!-- /.page-content -->


	
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderScripts" ID="Content2" runat="server">
    <script src="js/addmarkers.js"></script>
    <script src="js/geolocation.js"></script>
    

</asp:Content>
