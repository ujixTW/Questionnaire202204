<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="Questionnaire202204.Stastic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <script>
        var questionnaireID = "<%=this.QuestionnaireID%>";


        $(document).ready(function () {
            let postData = {
                "questionnaireID": questionnaireID
            };
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=Stastic",
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

    </script>

</asp:Content>
