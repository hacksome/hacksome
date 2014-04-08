<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TwitterSearch.aspx.cs"
    Inherits="comScoreSocialDashboard.TwitterSearch" MasterPageFile="~/Master.master" %>

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
            <li class="active">Search</li>
        </ul>
        <!-- .breadcrumb -->

        <!-- #nav-search -->
    </div>




    <div class="page-content">
        <div class="page-header">
            <h1>Twitter Battle
								<small>
                                    <i class="icon-double-angle-right"></i>
                                    Search and Compare
                                </small>
            </h1>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->

                <div class="space-6"></div>

                <div class="row">
                    <div class="col-sm-10 col-sm-offset-1">


                        <div class="widget-box transparent invoice-box">

                            <div class="widget-body">
                                <div class="widget-main padding-24">
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="row">
                                                <form class="form-search">
                                                    <span class="input-icon">
                                                        <input type="text" placeholder="Search ..." class="nav-search-input" id="search-input1" autocomplete="off" />
                                                        <i class="icon-search nav-search-icon"></i>
                                                    </span>
                                                </form>
                                            </div>

                                            <div class="row" id="searcha">

                                                <div class="col-xs-11 label label-lg label-info arrowed-in arrowed-right">
                                                    <div class="title"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <ul class="list-unstyled spaced" id="searcha_rankinginfo">

                                                </ul>

                                            </div>
                                            <div class="space-6"></div>
                                             <div class="hr hr8 hr-double hr-dotted"></div>
                                            <div class="row" >
                                                
                                                <div id="searcha_map" style="width:300px; height: 300px"></div>
                                                
                                            </div>
                                            <div class="space-6"></div>
                                             <div class="hr hr8 hr-double hr-dotted"></div>

                                            <div class="row" >
                                                <div id="searcha_tweets"></div>
                                            </div>

                                        </div>
                                        <!-- /span -->

                                        <div class="col-sm-5">
                                            <div class="row">
                                                <form class="form-search">
                                                    <span class="input-icon">
                                                        <input type="text" placeholder="Search ..." class="nav-search-input" id="search-input2" autocomplete="off" />
                                                        <i class="icon-search nav-search-icon"></i>
                                                    </span>
                                                </form>
                                            </div>

                                            <div class="row" id="searchb">
                                                <div class="col-xs-11 label label-lg label-success arrowed-in arrowed-right">
                                                    <div class="title"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <ul class="list-unstyled spaced" id="searchb_rankinginfo">

                                                </ul>

                                            </div>
                                            <div class="space-6"></div>
                                             <div class="hr hr8 hr-double hr-dotted"></div>

                                            <div class="row" >
                                                
                                                <div id="searchb_map"  style="width:300px; height: 300px"></div>
                                            </div>
                                            <div class="space-6"></div>
                                             <div class="hr hr8 hr-double hr-dotted"></div>

                                            <div class="row" >
                                                
                                                <div id="searchb_tweets"></div>
                                            </div>

                                        </div>
                                        <!-- /span -->
                                    </div>
                                    <!-- row -->

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- PAGE CONTENT ENDS -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.page-content -->


    <!-- /.page-content -->



</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderScripts" ID="Content2" runat="server">
    <script src="js/twittersearch.js" type="text/javascript"></script>
     <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDrgMqxT3DIKg4PQkuqNeqbjOCZwLQKxc0&sensor=false"></script>

    <script src="js/addmarkers.js"></script>
    <script src="js/geolocation.js"></script>
    
</asp:Content>
