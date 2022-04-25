<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CommonlyQuestionList.aspx.cs" Inherits="Questionnaire202204.SystemAdmin.CommonlyQuestionList" %>

<%@ Register Src="~/ShareControls/ucDeleteButton.ascx" TagPrefix="uc1" TagName="ucDeleteButton" %>
<%@ Register Src="~/ShareControls/ucPageChange.ascx" TagPrefix="uc1" TagName="ucPageChange" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        /*新增/刪除按鈕*/
        .btnImage {
            width: 25px;
            height: 25px;
            margin: 3px 10px;
            border: 0px;
            background: none;
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

    

        <%--新增/刪除按鈕--%>
    <div class="DeleteAddBtnBar">
        <br />
        <%--刪除按鍵--%>
        <uc1:ucDeleteButton runat="server" ID="ucDeleteButton" />

        <%--新增按鍵--%>
        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Image/plus-lg.svg" CssClass="btnImage" OnClick="btnAdd_Click" />

    </div>

    <table class="tableQusList">
        <thead>
            <tr>
                <th>&emsp;</th>
                <th>#&emsp;&emsp;</th>
                <th>名稱&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;</th>
                <th>類別&nbsp;</th>
                <th>啟用</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptQusList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <input type="checkbox" name="checkboxQus" value="<%#Eval("QuestionID") %>" />
                        </td>
                        <td><%# Eval("NO") %></td>
                        <td><a href="CommonlyQuestionDetail.aspx?ID=<%# Eval("QuestionID") %>"><%# Eval("Name") %></a></td>
                        <td><%# Eval("TypeText") %></td>
                        <td><%# Eval("IsEnableText") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

            <%--查無資料--%>
            <tr id="trNoData" runat="server" visible="false">
                <td align="center" colspan="5">查無資料</td>
            </tr>
        </tbody>

    </table>

    <uc1:ucPageChange runat="server" ID="ucPageChange" />

</asp:Content>
