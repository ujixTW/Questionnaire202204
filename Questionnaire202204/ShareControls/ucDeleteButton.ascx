<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDeleteButton.ascx.cs" Inherits="Questionnaire202204.ShareControls.ucDeleteButton" %>

<style>
    .btnImage {
            width: 25px;
            height: 25px;
            margin: 3px 10px;
            border:0px;
            background:none;
        }

        .divDeleteMsg {
            position: absolute;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            background-color: rgb(0,0,0,0.5);
        }

            .divDeleteMsg > div {
                position:absolute;
                top: 50%;
                left: 50%;
                width: 400px;
                height: 150px;
                background-color: rgb(240,240,240);
                transform:translate(-50%,-50%);
                border-radius:5px;
                padding:10px;
                text-align:center;
            }
            .divDeleteMsg input[type=submit]{
                width:100px;
                border-radius:5px;
            }
            .btnDeleteYes{
                background-color:rgb(255,150,150);
            }
</style>

<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Image/trash3-fill.svg" CssClass="btnImage" OnClick="btnDelete_Click" />
        <%--彈跳警告視窗--%>
        <div id="divDeleteMsg" runat="server" class="divDeleteMsg" visible="false" >
            <div>
                <h2>確定要刪除這些檔案嗎?</h2>
                <br />
                <asp:Button ID="btnDeleteYes" runat="server" CssClass="btnDeleteYes" Text="是" OnClick="btnDelete_Click" />
                <asp:Button ID="btnDeleteNo" runat="server" Text="否" OnClick="btnDelete_Click" />
            </div>
        </div>