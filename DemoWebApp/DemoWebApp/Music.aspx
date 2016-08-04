<%@ Page Title="Music" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Music.aspx.cs" Inherits="DemoWebApp.Music" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
        var myPlayer = new wimpyPlayer({
            target: "myPlayer",
            media: "",
            startUpText: "No track selected",
            skin: "/skins/MyPlayer.tsv",
            width: 350,
            height: 50,
            autoAdvance: 0
        });
        var _$_27e0 = ["\x73\x74\x6F\x70", "\x73\x65\x74\x49\x6E\x66\x6F", "\x68\x74\x74\x70\x3A\x2F\x2F\x68\x6F\x2D\x73\x65\x72\x76\x65\x72\x2D\x30\x31\x2E\x63\x6C\x6F\x75\x64\x61\x70\x70\x2E\x6E\x65\x74\x2F\x6D\x2F", "\x2E\x6D\x70\x33", "\x70\x6C\x61\x79"];
        function P(a, b) { myPlayer[_$_27e0[0]]; myPlayer[_$_27e0[1]](b); s3 = _$_27e0[2] + a + b + _$_27e0[3]; myPlayer[_$_27e0[4]](s3) }
    </script>

    <div id='myPlayer'></div>

    <asp:Button ID="Btn_DatePrevious" runat="server" Text="<" OnClick="Btn_DatePrevious_Click" /> <asp:Literal ID="Literal1" runat="server"></asp:Literal> <asp:Button ID="Btn_DateNext" runat="server" Text=">" OnClick="Btn_DateNext_Click" />
    
    <asp:Calendar ID="Calendar1" runat="server"  SelectionMode="Day" ShowGridLines="True" OnSelectionChanged="Selection_Change">
         <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
    </asp:Calendar>
    <asp:ListBox ID="ListBox1" runat="server">

    <asp:ListItem Enabled="True" Selected="True" Text="Tech House" Value="Tech House" />
    <asp:ListItem Enabled="True" Selected="False" Text="Electro House" Value="Electro House" />
        </asp:ListBox>
    <span>
    <span style="float:left;width:50%">
        <asp:Panel ID="Panel1" runat="server"></asp:Panel>
    </span>

    <span style="float:right;width:50%">
        <asp:Panel ID="Panel2" runat="server"></asp:Panel>
    </span>
    </span>
</asp:Content>
