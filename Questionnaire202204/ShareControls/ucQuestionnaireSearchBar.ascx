<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucQuestionnaireSearchBar.ascx.cs" Inherits="Questionnaire202204.ShareControls.ucQuestionnaireSearchBar" %>

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
                        <asp:Literal ID="ltlSearchErrorMsg" runat="server" Visible="false">搜尋格式錯誤!</asp:Literal>
                    </td>
                </tr>

            </tbody>
        </table>

    </div>