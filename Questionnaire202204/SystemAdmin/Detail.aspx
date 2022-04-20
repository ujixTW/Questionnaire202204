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
            <button class="nav-link active" id="questionnaireContent-tab" data-bs-toggle="tab" data-bs-target="#questionnaireContent" type="button" role="tab" aria-controls="questionnaireContent" aria-selected="true" onclick="MyTab_Click('Questionnaire')">
                問卷
            </button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="questionContent-tab" data-bs-toggle="tab" data-bs-target="#questionContent" type="button" role="tab" aria-controls="questionContent" aria-selected="false" onclick="MyTab_Click('Question')">
                問題
            </button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="userAnswerContent-tab" data-bs-toggle="tab" data-bs-target="#userAnswerContent" type="button" role="tab" aria-controls="userAnswerContent" aria-selected="false" onclick="MyTab_Click('UserAnswer')">
                填寫資料
            </button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="statisticsContent-tab" data-bs-toggle="tab" data-bs-target="#statisticsContent" type="button" role="tab" aria-controls="statisticsContent" aria-selected="false" onclick="MyTab_Click('Statistics')">
                統計
            </button>
        </li>
    </ul>

    <%--內容--%>
    <div class="tab-content" id="myTabContent">

        <%--問卷--%>
        <div class="tab-pane fade show active" id="questionnaireContent" role="tabpanel" aria-labelledby="questionnaireContent-tab">

            <table>
                <tr>
                    <td class="txtInputTitle">問題名稱</td>
                    <td>
                        <%--<asp:TextBox ID="txtQuestionnaireTitle" CssClass="txtInput" runat="server"></asp:TextBox>--%>
                        <input type="text" id="txtQuestionnaireTitle" class="txtInput" />
                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">描述內容</td>
                    <td>
                        <%--<asp:TextBox ID="txtQuestionnaireContent" CssClass="txtContentInput" runat="server" TextMode="MultiLine"></asp:TextBox>--%>
                        <textarea id="txtQuestionnaireContent" class="txtContentInput"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">開始時間</td>
                    <td>
                        <%--<asp:TextBox ID="txtQuestionnaireStartDate" CssClass="txtInput" runat="server"></asp:TextBox>--%>
                        <input type="text" id="txtQuestionnaireStartDate" class="txtInput" />

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">結束時間</td>
                    <td>
                        <%--<asp:TextBox ID="txtQuestionnaireEndDate" CssClass="txtInput" runat="server"></asp:TextBox>--%>
                        <input type="text" id="txtQuestionnaireEndDate" class="txtInput" />

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle"></td>
                    <td>
                        <input type="checkbox" id="checkIsEnable" />
                        已啟用
                    </td>
                </tr>
            </table>

            <%--確認按鈕--%>
            <div class="btnActionBar">
                <ul>
                    <li>
                        <asp:Button ID="btnQuestionnaireCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                    </li>
                    <li>
                        <asp:Button ID="btnQuestionnaireSave" runat="server" Text="確定" OnClick="btnSave_Click" />
                    </li>
                </ul>
            </div>

        </div>

        <%--問題--%>
        <div class="tab-pane fade" id="questionContent" role="tabpanel" aria-labelledby="questionContent-tab">

            <%--新增、詳細編輯問題的區域--%>
            <div id="questionEditArea">
                <table>
                    <tr class="lg">
                        <td>種類</td>
                        <td>
                            <asp:DropDownList ID="listCommonlyQuestionType" runat="server"></asp:DropDownList>
                            <div class="dropdown">
                                <button class="btn btn-secondary dropdown-toggle" type="button" id="btnCommonlyQuestionType" data-bs-toggle="dropdown" aria-expanded="false">
                                    自訂問題
                                </button>
                                <ul id="ulCommonlyQuestion" class="dropdown-menu" aria-labelledby="btnCommonlyQuestionType">
                                    <li><a class="dropdown-item" href="#">Action</a></li>
                                    <li><a class="dropdown-item" href="#">Another action</a></li>
                                    <li><a class="dropdown-item" href="#">Something else here</a></li>
                                </ul>
                            </div>
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
            <br />
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
                    <tbody id="tbodyQuestionList">
                        <%--<asp:Repeater ID="rptQusList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" name="checkboxQus" value="<%# Eval("QuestionID") %>" /></td>
                                    <td><%# Eval("NO") %></td>
                                    <td><%# Eval("ShortQuestionContent") %></td>
                                    <td><%# Eval("QusTypeText") %></td>
                                    <td>
                                        <input type="checkbox" checked="<%# Eval("IsRequired") %>" onclick="return false" /></td>
                                    <td></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>--%>
                        <%--查無資料--%>
                        <%--<tr id="trNoData" runat="server" visible="false">
                            <td align="center" colspan="6">查無資料</td>
                        </tr>--%>
                    </tbody>
                </table>
            </div>

            <%--確認按鈕--%>
            <div class="btnActionBar">
                <ul>
                     <li>
                        <asp:Button ID="btnQuestionListCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                    </li>
                    <li>
                        <asp:Button ID="btnQuestionListSave" runat="server" Text="確定" />
                    </li>
                </ul>
            </div>

        </div>

        <%--填寫資料--%>
        <div class="tab-pane fade" id="userAnswerContent" role="tabpanel" aria-labelledby="userAnswerContent-tab">
            <h1>填寫資料</h1>

        </div>

        <%--統計--%>
        <div class="tab-pane fade" id="statisticsContent" role="tabpanel" aria-labelledby="statisticsContent-tab">
            <h1>統計</h1>

        </div>
    </div>

    <script>
        var questionnaireID ="<%=this.questionnaireID%>";
        var state = "<%=this.Request.QueryString["State"]%>";
        
        $(document).ready(function () {
            //初次進入時判斷並顯示對應畫面
            switch (state) {
                case "Questionnaire":
                    QuestionnairePage();
                    break;
                case "Question":
                    QuestionPage();
                    break;
                case "UserAnswer":
                    UserAnswer();
                    break;
                case "Statistics":
                    Statistics();
                    break;
                default:
                    //狀況全不符合，自動導向問卷畫面
                    QuestionnairePage();
                    return;
            }
        });

        //按下不同Tab時判斷並顯示對應畫面
        function MyTab_Click(tabName) {
            switch (tabName) {
                case "Questionnaire":
                    QuestionnairePage();
                    break;
                case "Question":
                    QuestionPage();
                    break;
                case "UserAnswer":
                    UserAnswer();
                    break;
                case "Statistics":
                    Statistics();
                    break;
                default:
                    //狀況全不符合，自動導向問卷畫面
                    QuestionnairePage();
                    return;
            }
        }

        //重置所有Tab狀態
        function ResetTabState() {
            //重置Class
            $("#questionnaireContent-tab").attr("class", "nav-link");
            $("#questionContent-tab").attr("class", "nav-link");
            $("#userAnswerContent-tab").attr("class", "nav-link");
            $("#statisticsContent-tab").attr("class", "nav-link");

            $("#questionnaireContent").attr("class", "tab-pane fade");
            $("#questionContent").attr("class", "tab-pane fade");
            $("#userAnswerContent").attr("class", "tab-pane fade");
            $("#statisticsContent").attr("class", "tab-pane fade");

            //重置aria-selected
            $("#questionnaireContent-tab").attr("aria-selected", "false");
            $("#questionContent-tab").attr("aria-selected", "false");
            $("#userAnswerContent-tab").attr("aria-selected", "false");
            $("#statisticsContent-tab").attr("aria-selected", "false");

        }
        //啟用特定Tab
        function SetTab(pageState) {
            var idText = "";
            switch (pageState) {
                case "Questionnaire":
                    idText = "questionnaireContent";
                    break;
                case "Question":
                    idText = "questionContent";
                    break;
                case "UserAnswer":
                    idText = "userAnswerContent";
                    break;
                case "Statistics":
                    idText = "statisticsContent";
                    break;
            }
            $(`#${idText}-tab`).attr("class", "nav-link active");
            $(`#${idText}`).attr("class", "tab-pane fade show active");
            $(`#${idText}-tab`).attr("aria-selected", "true");
        }
        //問卷
        function QuestionnairePage() {
            state = "Questionnaire";
            var postData = {
                "questionnaireID": questionnaireID
            }
            $.ajax({
                url: `../API/QuestionnaireDetailHandler.ashx?Page=${state}`,
                method: "POST",
                data: postData,
                success: function (objData) {
                    ResetTabState();
                    //切換為指定TAB
                    SetTab(state);

                    //問卷標題
                    $("#txtQuestionnaireTitle").attr("value", objData.Title);
                    //問卷描述
                    $("#txtQuestionnaireContent").empty();
                    $("#txtQuestionnaireContent").append(objData.Briefly);
                    //開始時間
                    $("#txtQuestionnaireStartDate").attr("value", objData.StartTimeText);
                    //結束時間
                    $("#txtQuestionnaireEndDate").attr("value", objData.EndTimeText);
                    //是否啟用
                    $("#checkIsEnable").attr("checked", objData.IsEnable);
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }

            });
        }
        //問題
        function QuestionPage() {
            state = "Question";
            var postData = {
                "questionnaireID": questionnaireID
            }
            $.ajax({
                url: `../API/QuestionnaireDetailHandler.ashx?Page=${state}`,
                method: "POST",
                data: postData,
                success: function (objData) {
                    ResetTabState();
                    //切換為指定TAB
                    SetTab(state);

                    //變更DropDownList
                    var qusCommonlyDropDownText = "";
                    for (var item in objData) {
                        qusCommonlyDropDownText += `<li><a class="dropdown-item" href="#">${item}</a></li>`
                    }

                    //變更問題清單
                    var qusListText = "";
                    if (objData.Count != 0) {
                        for (var item in objData) {
                            qusListText += `
                                        <tr>
                                            <td>
                                                <input type="checkbox" name="checkboxQus" value="${item.QuestionID}" /></td>
                                            <td>${item.NO}</td>
                                            <td>${item.ShortQuestionContent}</td>
                                            <td>${item.QusTypeText}</td>
                                            <td>
                                                <input type="checkbox" checked="${item.IsRequired}" onclick="return false" /></td>
                                            <td></td>
                                        </tr>
                                        `;
                        }
                    } else {
                        qusListText = `
                                    <tr runat="server" visible="false">
                                        <td align="center" colspan="6">查無資料</td>
                                    </tr>
                                    `;
                    }
                    $("#tbodyQuestionList").empty();
                    $("#tbodyQuestionList").append(qusListText);

                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        }
        //填寫資料
        function UserAnswer() {
            state = "UserAnswer";
            var postData = {
                "questionnaireID": questionnaireID
            }
            $.ajax({
                url: `../API/QuestionnaireDetailHandler.ashx?Page=${state}`,
                method: "POST",
                data: postData,
                success: function (objData) {
                    ResetTabState();
                    //切換為指定TAB
                    SetTab(state);



                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        }
        //統計
        function Statistics() {
            state = "Statistics";
            var postData = {
                "questionnaireID": questionnaireID
            }
            $.ajax({
                url: `../API/QuestionnaireDetailHandler.ashx?Page=${state}`,
                method: "POST",
                data: postData,
                success: function (objData) {
                    ResetTabState();
                    //切換為指定TAB
                    SetTab(state);



                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        }

        
    </script>

</asp:Content>
