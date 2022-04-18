<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Questionnaire202204.SystemAdmin.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*搜尋欄位*/
        .SearchBar {
            border: 3px solid #000;
            padding: 10px 30px;
            width: 90%;
        }
        .SearchBar td{
            padding:5px;
        }
        .SearchBar input[type=text]{
            width:100%;
            border:3px solid #000;
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
                        <asp:Button ID="btnSearch" runat="server" Text="搜尋" />
                    </td>
                </tr>

            </tbody>
        </table>

    </div>
    <div class="DeleteAddBtnBar">
        <br />
        <%--刪除按鍵--%>
        <button id="btnDelete" class="btn btn-link">
            <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z" />
            </svg>
        </button>
        <%--新增按鍵--%>
        <button id="btnAdd" class="btn btn-link">
            <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-plus-lg" viewBox="0 0 16 16">
                <path fill-rule="evenodd" d="M8 2a.5.5 0 0 1 .5.5v5h5a.5.5 0 0 1 0 1h-5v5a.5.5 0 0 1-1 0v-5h-5a.5.5 0 0 1 0-1h5v-5A.5.5 0 0 1 8 2Z" />
            </svg>
        </button>

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
                                <input type="checkbox" name="checkboxQus" /></td>
                            <td><%# Eval("NO") %></td>
                            <td><a href="Detail.aspx?ID=<%# Eval("QuestionnaireID") %>&State=1"><%# Eval("Title") %></a></td>
                            <td><%# Eval("IsEnableText") %></td>
                            <td><%# Eval("StartTime") %></td>
                            <td><%# Eval("EndTime") %></td>
                            <td><a href="Detail.aspx?ID=<%# Eval("QuestionnaireID") %>&State=4">前往</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>

        </table>

    </div>
    <script>

</script>
</asp:Content>
