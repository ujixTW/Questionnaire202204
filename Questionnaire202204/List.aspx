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
                    <th>&emsp;</th>
                    <th>#&emsp;&emsp;</th>
                    <th>問卷&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;</th>
                    <th>狀態&nbsp;</th>
                    <th>開始時間</th>
                    <th>結束時間</th>
                    <th>觀看統計</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptQusList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <input type="checkbox" name="checkboxQus" value="<%#Eval("QuestionnaireID") %>" /></td>
                            <td><%# Eval("NO") %></td>
                            <td>
                                <a href="Form.aspx?ID=<%# Eval("QuestionnaireID") %>" onclick="return false"><%# Eval("Title") %></a>
                            </td>
                            <td><%# Eval("VoteStateText") %></td>
                            <td><%# Eval("StartTimeText") %></td>
                            <td><%# Eval("EndTimeText") %></td>
                            <td><a href="Stastic.aspx?ID=<%#Eval("QuestionnaireID") %>">前往</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

                <%--查無資料--%>
                <tr id="trNoData" runat="server" visible="false">
                    <td align="center" colspan="7">查無資料</td>
                </tr>
            </tbody>

        </table>
        <uc1:ucPageChange runat="server" ID="ucPageChange" />
    </div>

</asp:Content>
