<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CommonlyQuestionDetail.aspx.cs" Inherits="Questionnaire202204.SystemAdmin.CommonlyQuestionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*問題CSS*/

        #questionEditArea {
            height: 100%;
            width: 100%;
            padding: 30px 50px;
            border: 3px solid #000;
        }

            #questionEditArea td {
                padding: 5px 10px;
            }

            #questionEditArea .lg {
                vertical-align: top;
                height: 100px;
            }

        #questionChoiseArea {
            height: 45%;
            width: 100%;
        }

        .btnAction {
            margin: 10px 30px 0px 30px;
        }
        /*問題CSS結束*/
        /*警告文字*/
        .errorMsg {
            color: #f88;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--新增、編輯問題的區域--%>
    <div id="questionEditArea">
        <table>
            <tr>
                <td>名稱</td>
                <td>
                    <asp:TextBox ID="txtCommlyQusName" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:Literal ID="ltlCommlyQusNameMsg" runat="server" Visible="false">
                            <span class="errorMsg">
                                此欄位為必填。
                            </span>
                    </asp:Literal>
                </td>
            </tr>
            <tr>
                <td>問題</td>
                <td>
                    <asp:TextBox ID="txtQuestionContent" runat="server"></asp:TextBox>&emsp;
                    <asp:Literal ID="ltlQusContentMsg" runat="server" Visible="false">
                            <span class="errorMsg">
                                此欄位為必填。
                            </span>
                    </asp:Literal>
                            <%--問題類型選單--%>
                    <asp:DropDownList ID="listQuestionType" runat="server">
                        <asp:ListItem Selected="True" Value="1">文字方塊</asp:ListItem>
                        <asp:ListItem Value="2">文字方塊 (數字)</asp:ListItem>
                        <asp:ListItem Value="3">文字方塊(Email)</asp:ListItem>
                        <asp:ListItem Value="4">文字方塊 (日期)</asp:ListItem>
                        <asp:ListItem Value="5">單選方塊</asp:ListItem>
                        <asp:ListItem Value="6">複選方塊</asp:ListItem>
                    </asp:DropDownList>&emsp;
                    <asp:CheckBox ID="checkIsRequired" runat="server" />&nbsp;
                    必填
                </td>
            </tr>

            <tr>
                <td>回答</td>
                <td>
                    <asp:TextBox ID="txtQuestionOption" runat="server"></asp:TextBox>&emsp;
                    <asp:Literal ID="ltlQuestionOptionMsg" runat="server" Visible="false">
                            <span class="errorMsg">
                                此欄位為必填。
                            </span>
                    </asp:Literal>
                    (多個答案以 ; 分隔)
                    
                </td>
            </tr>
            <tr>
                <td>啟用</td>
                <td>
                    <asp:CheckBox ID="checkBoxIsEnable" runat="server" Checked="true" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btnAction" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="加入" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
