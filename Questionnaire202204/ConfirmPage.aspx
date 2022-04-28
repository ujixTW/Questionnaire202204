<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmPage.aspx.cs" Inherits="Questionnaire202204.ConfirmPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*使用者資料填寫區CSS*/
        #userDataArea > table {
            width: 60%;
        }

        #userDataArea td {
            padding: 10px 0;
        }

        .userDataName {
            width: 100px;
        }

        .userDataInput > input {
            border: 0;
        }

        #userAnswerListArea > div {
            margin-bottom: 30px;
        }
        /*使用者資料填寫區CSS結束*/

        /*下方案扭區CSS*/
        #footerBtnArea {
            text-align: right;
        }

            #footerBtnArea > input {
                margin-left: 30%;
                border: 2px solid #000;
                border-radius: 3px;
            }


        .send {
            background-color: #9d9;
        }
        /*下方案扭區CSS結束*/
        /*送出、返回確認CSS*/

        #sendMsg,
        #cancelMsg {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgb(0,0,0,0.2);
            display: none;
        }

            #sendMsg > div,
            #cancelMsg > div {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%,-50%);
                width: 300px;
                height: 100px;
                background-color: #fff;
                padding: 10px;
                text-align: center;
            }
        /*送出、返回確認CSS結束*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div id="userAnswerListArea"></div>

    <%--下方案扭區--%>
    <div id="footerBtnArea">
        <p id="pQusCount"></p>
        <input type="button" id="btnCancel" value="取消" />
        <input type="button" id="btnSend" class="send" value="送出" />
    </div>
    <div id="sendMsg">
        <div>
            <p>確定要送出資料嗎?</p>
            <input type="button" id="btnSendCancel" value="取消" />
            <input type="button" id="btnSendSure" class="send" value="送出" />
        </div>
    </div>
    <div id="cancelMsg">
        <div>
            <p>即將返回填寫頁</p>
            <input type="button" id="btnCancelCancel" value="取消" />
            <input type="button" id="btnCancelSure" class="send" value="送出" />
        </div>
    </div>
    <script>
        var questionnaireID = "<%=this.QuestionnaireID%>";

        $(document).ready(function () {
            let postData = {
                "questionnaireID": questionnaireID
            };
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=ConfirmPage&Action=Start",
                method: "POST",
                data: postData,
                success: function (objDataList) {
                    let userData = objDataList.userData;
                    let questionList = objDataList.questionList;
                    let userAnswerList = objDataList.userAnswerList;

                    var userDataText = `
                        <table class="userAnswerDetail">
                            <tr>
                                <td class="userDataName">姓名</td>
                                <td class="userDataInput">
                                    <input type="text" readonly="readonly" value="${userData.Name}" />
                                </td>
                            </tr>
                            <tr>
                                <td class="userDataName">手機</td>
                                <td class="userDataInput">
                                    <input type="text" readonly="readonly" value="${userData.Mobile}" />
                                </td>
                            </tr>
                            <tr>
                                <td class="userDataName">Email</td>
                                <td class="userDataInput">
                                    <input type="text" readonly="readonly" value="${userData.Email}" />

                                </td>
                            </tr>
                            <tr>
                                <td class="userDataName">年齡</td>
                                <td class="userDataInput">
                                    <input type="text" readonly="readonly" value="${userData.Age}" />

                                </td>
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
                                qusTypeText = `<p>${userAnswerDataList[0]}</p>`;
                                break;
                            case "單選方塊":
                                var optionText = `${questionList[i].OptionContent}`.split(';');
                                for (var j = 0; j < optionText.length; j++) {
                                    if (userAnswerDataList[j + 1] === 'true') {
                                        qusTypeText += `<p>${optionText[j]}</p>`;
                                    }
                                }
                                break;
                            case "複選方塊":
                                var optionText = `${questionList[i].OptionContent}`.split(';');
                                for (var j = 0; j < optionText.length; j++) {
                                    if (userAnswerDataList[j + 1] === 'true') {
                                        qusTypeText += `<p>${optionText[j]}</p>`;
                                    }
                                }
                                break;
                        }

                        questionText += `
                            <div>
                                 <%--題目--%>
                                 <p>${questionList[i].NO}.${questionList[i].QuestionContent}${isRequiredText}</p>
                                 <%--答案--%>
                                 ${qusTypeText}
                            </div>
                            `;
                    }
                    //變更整個填寫資料頁簽畫面
                    var detailText = `
                        <div id="userDataArea">
                            ${userDataText}
                        </div>
                       ${questionText}`;


                    $("#userAnswerListArea").empty();
                    $("#userAnswerListArea").append(detailText);

                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });

        });

        //當按下送出按鈕時
        $("#btnSend").click(function () {
            $("#sendMsg").attr("style", "display:block;");

        });

        //當按下取消按鈕時
        $("#btnCancel").click(function () {
            $("#cancelMsg").attr("style", "display:block;");

        });
        //送出確認-確認
        $("#btnSendSure").click(function () {

            let postData = {
                "questionnaireID": questionnaireID
            };
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=ConfirmPage&Action=Send",
                method: "POST",
                data: postData,
                success: function (objDataList) {
                    location.href = `Stastic.aspx?ID=${questionnaireID}`;
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });

        });

        //送出確認-取消
        $("#btnSendCancel").click(function () {
            $("#sendMsg").attr("style", "display:none;");

        });

        //返回確認-確認
        $("#btnCancelSure").click(function () {

            location.href = `Form.aspx?ID=${questionnaireID}`;

        });

        //返回確認-取消
        $("#btnCancelCancel").click(function () {
            $("#cancelMsg").attr("style", "display:none;");

        });

    </script>

</asp:Content>
