<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmPage.aspx.cs" Inherits="Questionnaire202204.ConfirmPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*下方案扭區CSS*/
        #footerBtnArea {
            text-align: right;
        }

            #footerBtnArea > input {
                margin-left: 30%;
                border: 2px solid #000;
                border-radius: 3px;
            }

        #btnSend {
            background-color: #9d9;
        }
        /*下方案扭區CSS結束*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--下方案扭區--%>
    <div id="footerBtnArea">
        <p id="pQusCount"></p>
        <input type="button" id="btnCancel" value="取消" />
        <input type="button" id="btnSend" value="送出" />
    </div>

    <script>
        var questionnaireID = "<%=this.QuestionnaireID%>";

        $(document).ready(function () {
            let postData = {
                "questionnaireID": questionnaireID
            };
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=ConfirmPage&Action=Start",
                method: "POST",
                data: postData,
                success: function (objDataList) {

                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });

        });

        $("#btnSend").click(function () {
            let postData = {
                "questionnaireID": questionnaireID
            };
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=ConfirmPage&Action=Send",
                method: "POST",
                data: postData,
                success: function (objDataList) {
                    location.href = `Stastic.aspx?ID=${questionnaireID}`;
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });

        });

        //當按下送出按鈕時
        $("#btnCancel").click(function () {

            location.href = `Form.aspx?ID=${questionnaireID}`;

        });

    </script>

</asp:Content>
