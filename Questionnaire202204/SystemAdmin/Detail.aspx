<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Questionnaire202204.SystemAdmin.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*問卷CSS*/
        #questionnaireContent td {
            padding: 10px;
            vertical-align: top;
        }

        #questionnaireContent table {
            padding: 10px;
            width: 100%;
        }

        .txtInputTitle {
            width: 100px;
        }

        #questionnaireContent .txtInput {
            width: 80%;
        }

        #questionnaireContent .txtContentInput {
            width: 90%;
            height: 100px;
            resize: none;
        }
        /*問卷CSS結束*/

        /*問題CSS*/
        #questionEditArea {
            height: 45%;
            width: 100%;
            padding: 4px 10px 0px 50px;
        }

            #questionEditArea td {
                padding: 0px 10px;
            }

            #questionEditArea .lg {
                vertical-align: top;
                height: 100px;
            }

        #questionChoiseArea {
            height: 45%;
            width: 100%;
        }

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

        /*問題CSS結束*/

        #myTabContent {
            padding: 10px;
        }

        /*確認、取消按鍵區域*/
        .btnActionBar ul li {
            display: inline;
            float: left;
            width: 50%;
            height: 10%;
            text-align: right;
            padding: 0px 10px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--標籤--%>
    <ul class="nav nav-tabs" id="myTab" role="tablist">
        <li class="nav-item" role="presentation">
            <button class="nav-link active" id="questionnaireContent-tab" data-bs-toggle="tab" data-bs-target="#questionnaireContent" type="button" role="tab" aria-controls="home" aria-selected="true">問卷</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="questionContent-tab" data-bs-toggle="tab" data-bs-target="#questionContent" type="button" role="tab" aria-controls="profile" aria-selected="false">問題</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="userAnswerContent-tab" data-bs-toggle="tab" data-bs-target="#userAnswerContent" type="button" role="tab" aria-controls="contact" aria-selected="false">填寫資料</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="statisticsContent-tab" data-bs-toggle="tab" data-bs-target="#statisticsContent" type="button" role="tab" aria-controls="contact" aria-selected="false">統計</button>
        </li>
    </ul>
    <%--內容--%>
    <div class="tab-content" id="myTabContent">

        <%--問卷--%>
        <div class="tab-pane fade show active" id="questionnaireContent" role="tabpanel" aria-labelledby="home-tab">

            <table>
                <tr>
                    <td class="txtInputTitle">問題名稱</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireTitle" CssClass="txtInput" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">描述內容</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireContent" CssClass="txtContentInput" runat="server" TextMode="MultiLine"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">開始時間</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireStartDate" CssClass="txtInput" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">結束時間</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireEndDate" CssClass="txtInput" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle"></td>
                    <td>
                        <input type="checkbox" id="checkIsEnable" runat="server" />
                        已啟用
                    </td>
                </tr>
            </table>

            <%--確認按鈕--%>
            <div class="btnActionBar">
                <ul>
                    <li>
                        <asp:Button ID="btnCancel" runat="server" Text="取消" />
                    </li>
                    <li>
                        <asp:Button ID="btnSave" runat="server" Text="確定" />
                    </li>
                </ul>
            </div>

        </div>

        <%--問題--%>
        <div class="tab-pane fade" id="questionContent" role="tabpanel" aria-labelledby="profile-tab">

            <%--新增、詳細編輯問題的區域--%>
            <div id="questionEditArea">
                <table>
                    <tr class="lg">
                        <td>種類</td>
                        <td>
                            <asp:DropDownList ID="listCommonlyQuestionType" runat="server"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>問題</td>
                        <td>
                            <asp:TextBox ID="txtQuestionContent" runat="server"></asp:TextBox>&emsp;
                            <asp:DropDownList ID="listQuestionType" runat="server"></asp:DropDownList>&emsp;
                            <input type="checkbox" id="checkIsRequired" runat="server" />&nbsp;
                            必填
                        </td>
                    </tr>

                    <tr>
                        <td>回答</td>
                        <td>
                            <asp:TextBox ID="txtQuestionOption" runat="server"></asp:TextBox>&emsp;
                            (多個答案以 ; 分隔)&emsp;
                            <asp:Button ID="btnAddQuestion" runat="server" Text="加入" />
                        </td>
                    </tr>
                </table>
            </div>

            <%--選擇或刪除問題的區域--%>
            <div id="questionChoiseArea">
                <table class="tableQusList">
                    <%--表格標題--%>
                    <thead>
                        <tr>
                            <th>&nbsp;</th>
                            <th>#</th>
                            <th>問題&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;</th>
                            <th>種類&emsp;&emsp;</th>
                            <th>必填</th>
                            <th>&emsp;&emsp;</th>
                        </tr>
                    </thead>
                    <%--表格內容--%>
                    <tbody>
                        <asp:Repeater ID="rptQusList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><input type="checkbox" name="checkboxQus" value="<%# Eval("QuestionID") %>" /></td>
                                    <td><%# Eval("NO") %></td>
                                    <td><%# Eval("ShortQuestionContent") %></td>
                                    <td><%# Eval("QusTypeText") %></td>
                                    <td><input type="checkbox" checked="<%# Eval("IsRequired") %>" onclick="return false" /></td>
                                    <td></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--查無資料--%>
                        <tr id="trNoData" runat="server" visible="false">
                            <td align="center" colspan="6">查無資料</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <%--確認按鈕--%>
            <div class="btnActionBar">
                <ul>
                    <li>
                        <asp:Button ID="Button1" runat="server" Text="取消" />
                    </li>
                    <li>
                        <asp:Button ID="Button2" runat="server" Text="確定" />
                    </li>
                </ul>
            </div>

        </div>

        <%--填寫資料--%>
        <div class="tab-pane fade" id="userAnswerContent" role="tabpanel" aria-labelledby="contact-tab">
            <h1>填寫資料</h1>

        </div>

        <%--統計--%>
        <div class="tab-pane fade" id="statisticsContent" role="tabpanel" aria-labelledby="contact-tab">
            <h1>統計</h1>

        </div>
    </div>


</asp:Content>
