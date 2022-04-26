<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Questionnaire202204.List" %>

<%@ Register Src="~/ShareControls/ucQuestionnaireSearchBar.ascx" TagPrefix="uc1" TagName="ucQuestionnaireSearchBar" %>
<%@ Register Src="~/ShareControls/ucPageChange.ascx" TagPrefix="uc1" TagName="ucPageChange" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*問卷清單*/
        .tableQusList {
            border: 3px solid #000;
            padding: 0px;
            width: 100%;
        }

            .tableQusList th {
                border-left: 3px solid #000;
                padding: 0px 3px;
            }

            .tableQusList td {
                border-left: 3px solid #000;
                padding: 3px;
            }

            .tableQusList > thead tr {
                background-color: #bbb;
            }

            .tableQusList > tbody tr {
                background-color: #ddd;
            }

            .tableQusList tr:nth-child(even) {
                background-color: #fff;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--搜尋欄位--%>
    <uc1:ucQuestionnaireSearchBar runat="server" ID="ucQuestionnaireSearchBar" />

    <br />

    <%--問卷列表--%>
    <div class="ListBar">
        <table class="tableQusList">
            <thead>
                <tr>
                    <th>#&emsp;&emsp;</th>
                    <th>問卷&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;</th>
                    <th>狀態&nbsp;</th>
                    <th>開始時間</th>
                    <th>結束時間</th>
                    <th>觀看統計</th>
                </tr>
            </thead>
            <tbody id="tbodyQuestionnaireList">
            </tbody>

        </table>
        <uc1:ucPageChange runat="server" ID="ucPageChange" />
    </div>
    <script>
        $(document).ready(function () {
            $.ajax({
                url: `../API/FrontHandler.ashx?Page=List`,
                method: "POST",
                success: function (objDataList) {
                    var tableText = "";
                    if (objDataList.length != 0) {
                        for (var item of objDataList) {
                            let qusLinkText = "";
                            if (item.VoteStateText === "投票中") {
                                qusLinkText = `<a href="Form.aspx?ID=${item.QuestionnaireID}">${item.Title}</a>`;
                            } else {
                                qusLinkText = `${item.Title}`;
                            }
                            tableText += `<tr>
                                              <td>${item.NO}</td>
                                              <td>
                                                  ${qusLinkText}
                                              </td>
                                              <td>${item.VoteStateText}</td>
                                              <td>${item.StartTimeText}</td>
                                              <td>${item.EndTimeText}</td>
                                              <td><a href="Stastic.aspx?ID=${item.QuestionnaireID}">前往</a></td>
                                          </tr>`;
                        }
                    } else {
                        tableText = `
                        <%--查無資料--%>
                            <tr>
                                 <td align="center" colspan="6">查無資料</td>
                            </tr>
                        `;
                    }

                    $("#tbodyQuestionnaireList").empty();
                    $("#tbodyQuestionnaireList").append(tableText);
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        });
    </script>
</asp:Content>
