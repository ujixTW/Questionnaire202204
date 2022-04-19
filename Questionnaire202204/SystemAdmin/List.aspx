﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Questionnaire202204.SystemAdmin.List" %>

<%@ Register Src="~/ShareControls/ucPageChange.ascx" TagPrefix="uc1" TagName="ucPageChange" %>
<%@ Register Src="~/ShareControls/ucDeleteButton.ascx" TagPrefix="uc1" TagName="ucDeleteButton" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*搜尋欄位*/
        .SearchBar {
            border: 3px solid #000;
            padding: 10px 30px;
            width: 90%;
        }

            .SearchBar td {
                padding: 5px;
            }

            .SearchBar input[type=text] {
                width: 100%;
                border: 3px solid #000;
            }
        /*新增/刪除按鈕*/
        .btnImage {
            width: 25px;
            height: 25px;
            margin: 3px 10px;
            border:0px;
            background:none;
        }

        /*問卷清單*/
        .tableQusList {
            border: 3px solid #000;
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

        .DeleteAddBtnBar {
            padding: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--搜尋欄位--%>
    <div class="SearchBar">
        <table>
            <tbody>
                <tr>
                    <td>問卷標題</td>
                    <%--標題文字搜尋--%>
                    <td colspan="2">
                        <asp:TextBox ID="txtSearchText" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>開始／結束</td>
                    <%--開始時間--%>
                    <td>
                        <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
                    </td>
                    <%--結束時間--%>
                    <td>
                        <asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
                    </td>
                </tr>

            </tbody>
        </table>

    </div>

    <%--新增/刪除按鈕--%>
    <div class="DeleteAddBtnBar">
        <br />
        <%--刪除按鍵--%>
        <uc1:ucDeleteButton runat="server" id="ucDeleteButton" />

        <%--新增按鍵--%>
        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Image/plus-lg.svg" CssClass="btnImage" OnClick="btnAdd_Click" />

    </div>

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
                            <td><a href="Detail.aspx?ID=<%# Eval("QuestionnaireID") %>&State=Questionnaire"><%# Eval("Title") %></a></td>
                            <td><%# Eval("IsEnableText") %></td>
                            <td><%# Eval("StartTimeText") %></td>
                            <td><%# Eval("EndTimeText") %></td>
                            <td><a href="Detail.aspx?ID=<%#Eval("QuestionnaireID") %>&State=Statistics">前往</a></td>
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
