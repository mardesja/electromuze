<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pricing.aspx.cs" Inherits="DemoWebApp.Pricing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>PRICING PLANS CHART</h2>
        <p class="lead">Get your private account and download music</p>
        
        <table>
            <tr>
                <td style="width:50%;font-size:small;vertical-align:top">
                    <ul>
                        <li>FTP download, Web Interface and Mobile App (IOS and Android)</li>
                        <li>320kps MP3 bitrate</li>
                        <li>More than 4 GB daily fresh music</li>
                        <li>2 years of archive</li>
                        <li>Various music genre</li>
                    </ul>
                </td>
                
                <td style="width:50%;font-size:small;vertical-align:top">
                    <ul>
                        <li>Classified by date and genre</li>
                        <li>Support</li>
                        <li>No waiting time for download</li>
                        <li>No captcha</li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>

    <div class="row">
        <div class="col-md-5">
            <h2>Free Account</h2>
            <b>Try our service before get a unlimited download registration</b><br /><br />

            <ul>
                <li>Slow download (10kb/s)</li>
                <li>Maximum 5 traks daily</li>
                <li>Listen tracks only on Download Web Interface</li>
                <li>Registration never expire</li>
            </ul>

            <p>
                <a class="btn btn-default" href="/">Start Now download now &raquo;</a>
            </p>
        </div>
        <div class="col-md-5">
            <h2>$18.00 USD / month</h2>

            <ul>
                <li>Fast download</li>
                <li>Unlimited traffic</li>
                <li>Unlimited download</li>
                <li>Listen tracks direct on Web site via MP3 player and Download Web Interface</li>
            </ul>

            <p>
                <a class="btn btn-default" href="/">Subscribe and start download Now &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
