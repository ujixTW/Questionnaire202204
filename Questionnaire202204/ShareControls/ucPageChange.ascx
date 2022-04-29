<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPageChange.ascx.cs" Inherits="Sakei.ShareControls.ucPageChange" %>

<style>
    .separator{
        border-left:2px solid #000;
        margin-left:5px;
        padding-left:5px;
    }
</style>

<a runat="server" id="aLinkFirst" href="#"><<</a>

<a runat="server" id="aLinkPrevious" class="separator" href="#"><</a>

<a runat="server" id="aLinkPage1" class="separator" href="#">1</a>

<a runat="server" id="aLinkPage2" class="separator" href="#">2</a>

<a runat="server" id="aLinkPage3" class="separator" href="#">3</a>

<a runat="server" id="aLinkPage4" class="separator" href="#">4</a>

<a runat="server" id="aLinkPage5" class="separator" href="#">5</a>

<a runat="server" id="aLinkNext" class="separator" href="#">></a> 

<a runat="server" id="aLinkFinal" class="separator" href="#">>></a> 