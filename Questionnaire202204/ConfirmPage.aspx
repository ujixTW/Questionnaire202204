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

        #btnSend {
            background-color: #9d9;
        }
        /*下方案扭區CSS結束*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div id="userAnswerListArea"></div>

    <%--下方案扭區--%>
    <div id="footerBtnArea">
        <p id="pQusCount"></p>
        <input type="button" id="btnCancel" value="取消" />
        <input type="button" id="btnSend" value="送出" />
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
                                    <p>${userData.Name}</p>
            
                                </td>
                            </tr>
                            <tr>
                                <td class="userDataName">手機</td>
                                <td class="userDataInput">
                                    <p>${userData.Mobile}</p>
            
                                </td>
                            </tr>
                            <tr>
                                <td class="userDataName">Email</td>
                                <td class="userDataInput">
                                    <p>${userData.Email}</p>
            
                                </td>
                            </tr>
                            <tr>
                                <td class="userDataName">年齡</td>
                                <td class="userDataInput">
                                    <p>${userData.Age}</p>
            
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
                                for (var j = 0; j < optionText.length + 1; j++) {
                                    if (userAnswerDataList[j] === 'true') {
                                        qusTypeText += `<p>${optionText[j]}</p>`;
                                    }
                                }
                                break;
                            case "複選方塊":
                                var optionText = `${questionList[i].OptionContent}`.split(';');
                                for (var j = 0; j < optionText.length + 1; j++) {
                                    if (userAnswerDataList[j] === 'true') {
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

        $("#btnSend").click(function () {
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

        //當按下送出按鈕時
        $("#btnCancel").click(function () {

            location.href = `Form.aspx?ID=${questionnaireID}`;

        });

    </script>

</asp:Content>
