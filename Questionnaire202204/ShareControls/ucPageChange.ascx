<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPageChange.ascx.cs" Inherits="Sakei.ShareControls.ucPageChange" %>

<nav aria-label="Page navigation example">
    <ul class="pagination justify-content-center">
        <li class="page-item" >
            <a runat="server" id="aLinkFirst" class="page-link" href="#">第一頁</a>
        </li>
        <li class="page-item">
            <a runat="server" id="aLinkPage1" class="page-link" href="#">1</a>
        </li>
        <li class="page-item active" aria-current="page">
            <a runat="server" id="aLinkPage2" class="page-link" href="">2</a>
        </li>
        <li class="page-item">
            <a runat="server" id="aLinkPage3" class="page-link" href="#">3</a>
        </li>
        <li class="page-item">
            <a runat="server" id="aLinkLast" class="page-link" href="#">最末頁</a>
        </li>
    </ul>
</nav>
