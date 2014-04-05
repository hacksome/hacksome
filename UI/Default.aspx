<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="comScoreSocialDashboard.Default"
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
            <h1>Dashboard
								<small>
                                    <i class="icon-double-angle-right"></i>
                                    overview &amp; stats
                                </small>
            </h1>
        </div>
        <!-- /.page-header -->

        <div class="row">
            <div class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->

                <div class="row">
                    <div class="space-6"></div>

                    <div class="col-sm-5 infobox-container">
                        <div class="widget-box">
                            <div class="widget-header widget-header-flat widget-header-small">
                                <h5>
                                    <i class="icon-signal"></i>
                                    Trend 
                                </h5>

                                <div class="widget-toolbar no-border">
                                   
                                        This Week
								  
                                </div>
                            </div>

                            <div class="widget-body">
                                <div class="widget-main">
                                    <div id="trendchart-placeholder"></div>
                          
                                </div>
                                <!-- /widget-main -->
                            </div>
                            <!-- /widget-body -->
                        </div>
                        <!-- /widget-box -->
                        
                    </div>

                    <div class="vspace-sm"></div>

                    <div class="col-sm-5">
                        <div class="widget-box">
                            <div class="widget-header widget-header-flat widget-header-small">
                                <h5>
                                    <i class="icon-signal"></i>
                                    Delicious Pie 
                                </h5>

                                <div class="widget-toolbar no-border">
                                   
                                        This Week
								  
                                </div>
                            </div>

                            <div class="widget-body">
                                <div class="widget-main">
                                    <div id="piechart-placeholder"></div>
                          
                                </div>
                                <!-- /widget-main -->
                            </div>
                            <!-- /widget-body -->
                        </div>
                        <!-- /widget-box -->
                    </div>
                    <!-- /span -->
                </div>
                <!-- /row -->

                
                <div class="hr hr32 hr-dotted"></div>

                <div class="row">
                    <div class="col-sm-5">
                        
                        <div class="widget-box transparent" id="recent-box">
                            <div class="widget-header">
                                <h4 class="lighter smaller">
                                    <i class="icon-rss orange"></i>
                                    Project Popularity Ranking
                                </h4>

                                <div class="widget-toolbar no-border">
                                </div>
                            </div>

                            <div class="widget-body">
                                <div class="widget-main padding-4">
                                <div class="table-responsive">
											<table id="projectrankingtable" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
														<th>Name</th>
														<th>Score</th>
														<th class="hidden-480">Tweets #</th>
														<th class="hidden-480">Index</th>
													</tr>
												</thead>

												<tbody id="projectranking">
                                                </tbody>
                                               </table>
                                </div>
                                
                                </div>
                                <!-- /widget-main -->
                            </div>
                            <!-- /widget-body -->
                        </div>
                        <!-- /widget-box -->
                    </div>
                    <!-- /span -->

                    <div class="col-sm-6">
                        <div class="widget-box ">
                            <div class="widget-header">
                                <h4 class="lighter smaller">
                                    <i class="icon-comment blue"></i>
                                    Tweeters and Tweets
                                </h4>
                            </div>

                            <div class="widget-body">
                                <div id="tweets" class="widget-main no-padding">
                                
                             
                                </div>
                                <!-- /widget-main -->
                            </div>
                            <!-- /widget-body -->
                        </div>
                        <!-- /widget-box -->
                    </div>
                    <!-- /span -->
                </div>
                <!-- /row -->

                <!-- PAGE CONTENT ENDS -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.page-content -->


	
</asp:Content>
