<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDeleteButton.ascx.cs" Inherits="Questionnaire202204.ShareControls.ucDeleteButton" %>

<%--刪除按鈕，會刪除被勾選checkbox的資料
checkbox的name必須為 checkboxQus 
value必須為欲刪除資料的ID--%>

<style>
    .btnImage {
        width: 25px;
        height: 25px;
        margin: 3px 10px;
        border: 0px;
        background: none;
        padding:0;
        border:0;
    }

    .divDeleteMsg {
        position: absolute;
        top: 0px;
        left: 0px;
        width: 100%;
        height: 100%;
        background-color: rgb(255,255,255);
    }

        .divDeleteMsg > div {
            position: absolute;
            top: 30%;
            left: 50%;
            width: 400px;
            height: 150px;
            background-color: rgb(240,240,220);
            transform: translate(-50%,-50%);
            border-radius: 5px;
            padding: 10px;
            text-align: center;
        }

        .divDeleteMsg input[type=submit] {
            width: 100px;
            border-radius: 5px;
        }

    .btnDeleteYes {
        background-color: rgb(255,150,150);
    }
</style>

<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/trash3-fill.svg" CssClass="btnImage" OnClick="btnDelete_Click" />

<%--彈跳警告視窗--%>
<div id="divDeleteMsg" runat="server" class="divDeleteMsg" visible="false">
    <div>
        <h2>確定要刪除這些檔案嗎?</h2>
        <br />
        <asp:Button ID="btnDeleteYes" runat="server" CssClass="btnDeleteYes" Text="是" OnClick="btnDelete_Click" />
        <asp:Button ID="btnDeleteNo" runat="server" Text="否" OnClick="btnDelete_Click" />
    </div>
</div>

