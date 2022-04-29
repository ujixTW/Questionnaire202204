<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Questionnaire202204.SystemAdmin.Detail" %>

<%@ Register Src="~/ShareControls/ucDeleteButton.ascx" TagPrefix="uc1" TagName="ucDeleteButton" %>
<%@ Register Src="~/ShareControls/ucPageChange.ascx" TagPrefix="uc1" TagName="ucPageChange" %>



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
        #questionContent {
            padding: 0px 50px;
        }

        #questionEditArea {
            height: 45%;
            width: 100%;
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

        .listQusType {
            max-height: 100px;
            overflow-y: scroll;
            position: absolute;
        }

        /*問題CSS結束*/

        /*填寫資料CSS開始*/

        #userAnswerContent {
            padding: 20px 100px 20px 30px;
        }

        .userAnswerDetail {
            width: 100%;
        }

        /*填寫資料CSS結束*/
        /*統計頁CSS開始*/

        #statisticsContent {
            padding: 10px 30px;
        }

        /*統計頁CSS結束*/

        #myTabContent {
            padding: 10px;
            border: 3px solid #000;
            border-radius: 0 0 3px 3px;
            border-top: 0;
        }

        #myTab {
            border-bottom: 3px solid #000;
            border-radius: 3px;
        }

            #myTab > li {
                margin: 0 -3px -3px 0;
                border: 3px solid #000;
                border-radius: 5px 5px 0 0;
                background-color: #ddd;
            }

                #myTab > li > .active {
                    background-color: #fff;
                    margin: 0 0 -3px 0;
                    border-bottom: 3px solid #fff;
                }
        /*警告文字*/
        .errorMsg {
            color: #f88;
        }

        .btnActionBar .nav-item {
            margin: 10px 10px 10px 40%;
        }



        /*清單固定格式*/
        .tableList {
            border: 3px solid #000;
            width: 100%;
        }

            .tableList th {
                border-left: 3px solid #000;
                padding: 0px 3px;
            }

            .tableList td {
                border-left: 3px solid #000;
                padding: 3px;
            }

            .tableList > thead tr {
                background-color: #bbb;
            }

            .tableList > tbody tr {
                background-color: #ddd;
            }

            .tableList tr:nth-child(even) {
                background-color: #fff;
            }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--標籤--%>
    <ul class="nav nav-tabs" id="myTab" role="tablist">
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="questionnaireContent-tab" data-bs-toggle="tab" data-bs-target="#questionnaireContent" type="button" role="tab" aria-controls="questionnaireContent" aria-selected="true" onclick="MyTab_Click('Questionnaire')">
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
        <asp:Literal ID="ltlIsAnsweredMsg" runat="server" Visible="false"><p class="errorMsg">已有人填寫問卷，編輯功能已關閉。</p></asp:Literal>

        <%--問卷--%>
        <div class="tab-pane fade" id="questionnaireContent" role="tabpanel" aria-labelledby="questionnaireContent-tab">

            <table>
                <tr>
                    <td class="txtInputTitle">問題名稱</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireTitle" CssClass="txtInput" runat="server" MaxLength="50" OnTextChanged="txtQuestionnaire_TextChanged" AutoPostBack="true"></asp:TextBox>

                        <asp:Literal ID="ltlQuestionnaireTitleMsg" runat="server" Visible="false">
                            <p class="errorMsg">
                                此欄位為必填。
                            </p>
                        </asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">描述內容</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireContent" CssClass="txtContentInput" runat="server" TextMode="MultiLine" OnTextChanged="txtQuestionnaire_TextChanged" AutoPostBack="true"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">開始時間</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireStartDate" CssClass="txtInput" runat="server" OnTextChanged="txtQuestionnaire_TextChanged" AutoPostBack="true"></asp:TextBox>

                        <asp:Literal ID="ltlQuestionnaireStartDateMsg" runat="server" Visible="false">
                            <p class="errorMsg">
                            日期格式錯誤，請輸入包含年月日的日期，且日期介於今日即結束日期之間(若有填入結束時間)，並以 - / 空格 任一種符號隔開年月日。
                            </p>
                        </asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle">結束時間</td>
                    <td>
                        <asp:TextBox ID="txtQuestionnaireEndDate" CssClass="txtInput" runat="server" OnTextChanged="txtQuestionnaire_TextChanged" AutoPostBack="true"></asp:TextBox>

                        <br />
                        <asp:Literal ID="ltlQuestionnaireEndDateMsg" runat="server" Visible="false">
                            <p class="errorMsg">
                                日期格式錯誤，請輸入包含年月日且大於開始時間的日期，並以 - / 空格 任一種符號隔開年月日。
                            </p>
                        </asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="txtInputTitle"></td>
                    <td>

                        <asp:CheckBox ID="checkIsEnable" runat="server" OnCheckedChanged="checkIsEnable_CheckedChanged" />
                        已啟用
                    </td>
                </tr>
            </table>

            <%--確認按鈕--%>
            <div class="btnActionBar">
                <ul class="nav justify-content-end">
                    <li class="nav-item">
                        <asp:Button ID="btnQuestionnaireCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                    </li>
                    <li class="nav-item">
                        <asp:Button ID="btnQuestionnaireSave" runat="server" Text="送出" OnClick="btnSave_Click" />
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
                            <asp:DropDownList ID="listCommonlyQuestionType" runat="server" CssClass="listQusType" OnSelectedIndexChanged="listCommonlyQuestionType_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>問題</td>
                        <td>
                            <asp:TextBox ID="txtQuestionContent" runat="server"></asp:TextBox>&emsp;
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
                            (多個答案以 ; 分隔)&emsp;
                            <asp:Button ID="btnAddQuestion" runat="server" Text="加入" OnClick="btnAddQuestion_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <uc1:ucDeleteButton runat="server" ID="ucDeleteButton" />
            <%--選擇或刪除問題的區域--%>
            <div id="questionChoiseArea">

                <table class="tableList">
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
                    </tbody>
                </table>
            </div>

            <%--確認按鈕--%>
            <div class="btnActionBar">
                <ul class="nav justify-content-end">
                    <li class="nav-item">
                        <asp:Button ID="btnQuestionListCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                    </li>
                    <li class="nav-item">
                        <asp:Button ID="btnQuestionListSave" runat="server" Text="送出" OnClick="btnSave_Click" />
                    </li>
                </ul>
            </div>

        </div>

        <%--填寫資料--%>
        <div class="tab-pane fade" id="userAnswerContent" role="tabpanel" aria-labelledby="userAnswerContent-tab">

            <%--匯出按鈕--%>
            <asp:Button ID="btnOutPutUserData" runat="server" Text="匯出" OnClick="btnOutPutUserData_Click" />
            <asp:Literal ID="ltlUserAnswerOutPutSuccessMsg" runat="server" Visible="false"><span class="errorMsg">輸出成功!</span></asp:Literal>
            <asp:Literal ID="ltlUserAnswerOutPutFailMsg" runat="server" Visible="false"><span class="errorMsg">沒有資料!</span></asp:Literal>
            <%--使用者資料清單--%>
            <div id="userAnswerListArea">
            </div>

            <%--頁數切換--%>
            <uc1:ucPageChange runat="server" ID="ucPageChange" />

        </div>

        <%--統計--%>
        <div class="tab-pane fade" id="statisticsContent" role="tabpanel" aria-labelledby="statisticsContent-tab">
        </div>

        <asp:Literal ID="ltlSaveFailMsg" runat="server" Visible="false"><div style="text-align:right; padding:5px 10px;"><p>儲存失敗!</p></div></asp:Literal>
        <asp:Literal ID="ltlSaveMsg" runat="server" Visible="false"><div style="text-align:right; padding:5px 10px;"><p>儲存成功!</p></div></asp:Literal>
    </div>


    <script>
        var questionnaireID ="<%=this.QuestionnaireID%>";
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
            window.location.href = `Detail.aspx?ID=${questionnaireID}&State=${tabName}`;
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
                    var tittleID ='<%=this.txtQuestionnaireTitle.ClientID%>';
                    var contentID ='<%=this.txtQuestionnaireContent.ClientID%>';
                    var startDateID ='<%=this.txtQuestionnaireStartDate.ClientID%>';
                    var endDateID ='<%=this.txtQuestionnaireEndDate.ClientID%>';
                    var checkBoxID = '<%=this.checkIsEnable.ClientID%>';
                    //問卷標題
                    $(`#${tittleID}`).attr("value", objData.Title);
                    //問卷描述
                    $(`#${contentID}`).empty();
                    $(`#${contentID}`).append(objData.Briefly);
                    //開始時間
                    $(`#${startDateID}`).attr("value", objData.StartTimeText);
                    //結束時間
                    $(`#${endDateID}`).attr("value", objData.EndTimeText);
                    //是否啟用
                    $(`#${checkBoxID}`).attr("checked", objData.IsEnable);
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

                    //變更問題清單
                    var qusListText = "";
                    if (objData.length != 0) {
                        for (var item of objData) {
                            var isRequiredCheckText = "";
                            if (item.IsRequired) {
                                isRequiredCheckText = " checked='checked'";
                            }
                            qusListText += `
                                        <tr>
                                            <td>
                                                <input type="checkbox" name="checkboxQus" value="${item.QuestionID}" /></td>
                                            <td>${item.NO}</td>
                                            <td>${item.ShortQuestionContent}</td>
                                            <td>${item.QusTypeText}</td>
                                            <td>
                                                <input type="checkbox" ${isRequiredCheckText} onclick="return false" /></td>
                                            <td>
                                                <a href="Detail.aspx?ID=${questionnaireID}&State=${state}&QusID=${item.QuestionID}">編輯</a>
                                            </td>
                                        </tr>
                                        `;
                        }
                    } else {
                        qusListText = `<tr><td align='center' colspan='6'>查無資料</td></tr>`;
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
            ResetTabState();
            //切換為指定TAB
            SetTab(state);

            if ('<%=string.IsNullOrEmpty(this.Request.QueryString["UserID"])%>' === 'True') {
                UserAnswerList();
            } else {
                UserAnswerDetail();
            }
        }
        //填寫資料 清單列表
        function UserAnswerList() {
            var postData = {
                "questionnaireID": questionnaireID,
                "page": '<%=this.PageIndex%>',
                "pageSize":<%=this.PageSize%>
            }
            $.ajax({
                url: `../API/QuestionnaireDetailHandler.ashx?Page=${state}`,
                method: "POST",
                data: postData,
                success: function (objData) {
                    var listText = "";
                    var tableText = "";
                    //變更問題清單
                    if (objData.length != 0) {

                        for (var item of objData) {

                            tableText += `
                                        <tr>
                                            <td>${item.NO}</td>
                                            <td>${item.Name}</td>
                                            <td>${item.CreateTimeText}</td>
                                            <td>
                                                <a href="Detail.aspx?ID=${questionnaireID}&State=${state}&UserID=${item.UserID}">前往</a>
                                            </td>
                                        </tr>
                                        `;
                        }
                    } else {
                        tableText = `<tr><td align='center' colspan='4'>查無資料</td></tr>`;
                    }
                    //變更整個填寫資料頁簽畫面
                    listText = `
                <br />
                <br />
                <table class="tableList">
                    <%--表格標題--%>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>姓名&emsp;</th>
                            <th>填寫時間&emsp;&emsp;</th>
                            <th>觀看細節</th>
                        </tr>
                    </thead>
                    <%--表格內容--%>
                    <tbody id="tbodyUserAnsList">
                        ${tableText}
                    </tbody>
                </table>`;

                    $("#userAnswerListArea").empty();
                    $("#userAnswerListArea").append(listText);

                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        }
        //填寫資料 詳細資訊
        function UserAnswerDetail() {

            var postData = {
                "questionnaireID": questionnaireID,
                "page": '<%=this.PageIndex%>',
                "userID":'<%=this.Request.QueryString["UserID"]%>'
            }
            $.ajax({
                url: `../API/QuestionnaireDetailHandler.ashx?Page=${state}&Detail=UserData`,
                method: "POST",
                data: postData,
                success: function (objDataList) {
                    var userData = objDataList.userData;
                    var questionList = objDataList.questionList;
                    var userAnswerList = objDataList.userAnswerList;

                    var userDataText = `
                        <table class="userAnswerDetail">
                            <tr>
                                <td>姓名</td>
                                <td>
                                    <input type="text" readonly="readonly" value="${userData.Name}" />
            
                                </td>
                                <td>手機</td>
                                <td>
                                    <input type="text" readonly="readonly" value="${userData.Mobile}" />
            
                                </td>
                            </tr>
                            <tr>
                                <td>Email</td>
                                <td>
                                    <input type="text" readonly="readonly" value="${userData.Email}" />
            
                                </td>
                                <td>年齡</td>
                                <td>
                                    <input type="text" readonly="readonly" value="${userData.Age}" />
            
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td colspan="2">填寫時間:${userData.CreateTimeText}</td>
                            </tr>
                        </table>`;
                    var questionText = "";
                    for (var i = 0; i < questionList.length; i++) {
                        //判斷該題是否為必填
                        var isRequiredText = "";
                        if (questionList[i].IsRequired === true && questionList[i].QusType < 5) {
                            isRequiredText = "(必填欄位)";
                        } else if (questionList[i].IsRequired === true && questionList[i].QusType >= 5) {
                            isRequiredText = "(必填)";
                        } else {
                            isRequiredText = "";
                        }

                        //暫存該題答案列表
                        var userAnswerDataList = [];
                        for (var j = 0; j < userAnswerList.length; j++) {
                            if (userAnswerList[j].QuestionID === questionList[i].QuestionID && questionList[i].QusType < 5) {

                                userAnswerDataList[0] = userAnswerList[j].Answer;
                                j = userAnswerList.length + 10;

                            } else if (userAnswerList[j].QuestionID === questionList[i].QuestionID && questionList[i].QusType >= 5) {

                                userAnswerDataList[userAnswerList[j].OptionNO] = userAnswerList[j].Answer;
                            }
                        }

                        //判斷該題題型以顯示對應格式
                        var qusTypeText = "";
                        switch (questionList[i].QusTypeText) {
                            case "文字方塊":
                            case "文字方塊 (數字)":
                            case "文字方塊(Email)":
                            case "文字方塊 (日期)":
                                qusTypeText = `<input type="text" readonly="readonly" value="${userAnswerDataList[0]}" /><br/>`;
                                break;
                            case "單選方塊":
                                var optionText = `${questionList[i].OptionContent}`.split(';');
                                for (var j = 0; j < optionText.length; j++) {
                                    if (userAnswerDataList[j + 1] === 'true') {
                                        qusTypeText += `<input type="radio" checked="checked" onclick="return false" />${optionText[j]}<br/>`;
                                    } else {
                                        qusTypeText += `<input type="radio" onclick="return false" />${optionText[j]}<br/>`;
                                    }
                                }
                                break;
                            case "複選方塊":
                                var optionText = `${questionList[i].OptionContent}`.split(';');
                                for (var j = 0; j < optionText.length; j++) {
                                    if (userAnswerDataList[j + 1] === 'true') {
                                        qusTypeText += `<input type="checkbox" checked="checked" onclick="return false" />${optionText[j]}<br/>`;
                                    } else {
                                        qusTypeText += `<input type="checkbox" onclick="return false" />${optionText[j]}<br/>`;
                                    }
                                }
                                break;
                        }

                        questionText += `
                            <%--題目--%>
                            <p>${questionList[i].NO}.${questionList[i].QuestionContent}${isRequiredText}</p>
                            <%--答案--%>
                            ${qusTypeText}<br />
                            `;
                    }
                    //變更整個填寫資料頁簽畫面
                    var detailText = `
                        <div id="userDataArea">
                            ${userDataText}
                        </div>
                        <div id="userAnswerDetailArea">${questionText}</div>`;


                    $("#userAnswerListArea").empty();
                    $("#userAnswerListArea").append(detailText);
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
                success: function (objDataList) {
                    ResetTabState();
                    //切換為指定TAB
                    SetTab(state);

                    var questionList = objDataList.questionList;
                    var answerStatisticsList = objDataList.answerStatisticsList;

                    var statisticsDataText = "";
                    //判斷該題題型以顯示對應格式
                    var qusTypeText = "";
                    for (var i = 0; i < questionList.length; i++) {

                        //判斷該題是否為必填
                        var isRequiredText = "";
                        if (questionList[i].IsRequired === true && questionList[i].QusType < 5) {
                            isRequiredText = "(必填欄位)";
                        } else if (questionList[i].IsRequired === true && questionList[i].QusType >= 5) {
                            isRequiredText = "(必填)";
                        } else {
                            isRequiredText = "";
                        }

                        switch (questionList[i].QusTypeText) {
                            case "文字方塊":
                            case "文字方塊 (數字)":
                            case "文字方塊(Email)":
                            case "文字方塊 (日期)":
                                qusTypeText = "-<br/>";
                                break;
                            case "單選方塊":
                                qusTypeText = CreateStatisticsOptionText(i, questionList, answerStatisticsList);
                                break;
                            case "複選方塊":
                                qusTypeText = CreateStatisticsOptionText(i, questionList, answerStatisticsList);
                                break;
                        }
                        statisticsDataText += `
                            <%--題目--%>
                            <p>${questionList[i].NO}.${questionList[i].QuestionContent}${isRequiredText}</p>
                            <%--答案--%>
                            ${qusTypeText}
                            `;

                    }
                    statisticsDataText = (answerStatisticsList.length === 0) ? "<p class='errorMsg'>目前無人作答</p>" : statisticsDataText;

                    $("#statisticsContent").empty();
                    $("#statisticsContent").append(statisticsDataText);
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        }
        //用作統計頁面編輯單一問題選項統計文字，輸出字串
        //i 目前為第幾題, questionList 問題清單, answerStatisticsList 使用者回答統計資料清單
        function CreateStatisticsOptionText(i, questionList, answerStatisticsList) {
            //選項文字陣列
            var optionText = `${questionList[i].OptionContent}`.split(';');
            var questionID = `${questionList[i].QuestionID}`;
            var choiseNumList = [];
            var choiseCount = 0;
            var qusTypeText = "";
            for (let j = 0; j < answerStatisticsList.length; j++) {
                //將問題選擇數量對上對應的問題選項
                if (answerStatisticsList[j].QuestionID === questionID) {
                    choiseNumList[answerStatisticsList[j].OptionNO - 1] = answerStatisticsList[j].AnswerStatistics;
                    choiseCount += answerStatisticsList[j].AnswerStatistics;
                }
            }

            for (var j = 0; j < optionText.length; j++) {
                if (!choiseNumList[j]) {
                    choiseNumList[j] = 0;
                }
                var choisePercent = (choiseCount <= 0) ? 0 : Math.round(choiseNumList[j] / choiseCount * 10000) / 100;
                qusTypeText += `${optionText[j]} ${choisePercent}% (${choiseNumList[j]})<br/>`;
            }
            return qusTypeText;
        }

    </script>

</asp:Content>
