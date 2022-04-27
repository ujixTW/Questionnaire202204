<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="Questionnaire202204.Form" %>

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

        .userDataInput input {
            width: 100%;
            border: 3px solid #000;
        }
        /*使用者資料填寫區CSS結束*/
        /*問題填寫區CSS*/
        .question {
            margin: 20px 0;
        }
        /*問題填寫區CSS結束*/
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

    <%--個人資料填寫區域--%>
    <div id="userDataArea">
        <table>
            <tr>
                <td class="userDataName">姓名</td>
                <td class="userDataInput">
                    <div>
                        <input type="text" id="txtUserName" required="required" placeholder="請輸入您的姓名" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="userDataName">手機</td>
                <td class="userDataInput">
                    <div>
                        <input type="text" id="txtMobil" required="required" maxlength="10" placeholder="請輸入您的電話" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="userDataName">Email</td>
                <td class="userDataInput">
                    <div>
                        <input type="email" id="txtEmail" required="required" placeholder="請輸入您的Email" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="userDataName">年齡</td>
                <td class="userDataInput">
                    <div>
                        <input type="number" id="txtAge" required="required" min="0" max="150" placeholder="請輸入您的年齡" />
                    </div>
                </td>
            </tr>
        </table>

    </div>

    <%--問卷題目與回覆區域--%>
    <div id="questionDataArea">
    </div>

    <%--下方案扭區--%>
    <div id="footerBtnArea">
        <p id="pQusCount"></p>
        <input type="button" id="btnCancel" value="取消" />
        <input type="submit" id="btnSend" value="送出" />
    </div>

    <script>
        var questionnaireID = "<%=this.QuestionnaireID%>";

        $(document).ready(function () {
            let postData = {
                "questionnaireID": questionnaireID
            }
            //開啟問卷頁，輸出資料到畫面上
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=Form&Action=Start",
                method: "POST",
                data: postData,
                success: function (objDataList) {
                    let userData = objDataList.userData;
                    let questionList = objDataList.questionList;
                    let userAnswerList = objDataList.userAnswerList;

                    let questionText = "";
                    for (let i = 0; i < questionList.length; i++) {

                        //判斷該題是否為必填
                        let isRequiredText = "";
                        let isRequiredClassText = "";
                        if (questionList[i].IsRequired === true && questionList[i].QusType < 5) {
                            isRequiredClassText = ` required="required"`;
                            isRequiredText = "(必填欄位)";
                        } else if (questionList[i].IsRequired === true && questionList[i].QusType >= 5) {
                            isRequiredClassText = ` required="required"`;
                            isRequiredText = "(必填)";
                        } else {
                            isRequiredText = "";
                        }

                        //判斷題目類型
                        let qusInputAreaText = "";
                        if (userData == null) {
                            switch (questionList[i].QusTypeText) {
                                case "文字方塊":
                                    qusInputAreaText = `
                                    <input type="text" name="${questionList[i].QuestionID}"${isRequiredClassText} />
                                    `;
                                    break;
                                case "文字方塊 (數字)":
                                    qusInputAreaText = `
                                    <input type="number" name="${questionList[i].QuestionID}"${isRequiredClassText} />
                                    `;
                                    break;
                                case "文字方塊(Email)":
                                    qusInputAreaText = `
                                    <input type="email" name="${questionList[i].QuestionID}" placeholder="請輸入Email"${isRequiredClassText} />
                                    `;
                                    break;
                                case "文字方塊 (日期)":
                                    qusInputAreaText = `
                                    <input type="date" name="${questionList[i].QuestionID}"${isRequiredClassText} />
                                    `;
                                    break;
                                case "單選方塊":
                                    var optionText = `${questionList[i].OptionContent}`.split(';');
                                    for (let j = 0; j < optionText.length; j++) {
                                        qusInputAreaText += `
                                        <input type="radio" name="${questionList[i].QuestionID}" value="${j+1}"${isRequiredClassText} /> ${optionText[j]}<br/>
                                    `;
                                    }
                                    break;
                                case "複選方塊":
                                    var optionText = `${questionList[i].OptionContent}`.split(';');
                                    for (let j = 0; j < optionText.length; j++) {
                                        qusInputAreaText += `
                                        <input type="checkbox" name="${questionList[i].QuestionID}" value="${j + 1}" /> ${optionText[j]}<br/>
                                    `;
                                    }
                                    break;
                            }
                        }
                        else {
                            //暫存該題答案列表
                            let tempUserAnswerList = [];
                            for (let j = 0; j < userAnswerList.length; j++) {
                                if (userAnswerList[j].QuestionID === questionList[i].QuestionID && questionList[i].QusType < 5) {

                                    tempUserAnswerList[0] = userAnswerList[j].Answer;
                                    break;

                                } else if (userAnswerList[j].QuestionID === questionList[i].QuestionID && questionList[i].QusType >= 5) {

                                    tempUserAnswerList[userAnswerList[j].OptionNO] = userAnswerList[j].Answer;
                                }
                            }

                            switch (questionList[i].QusTypeText) {
                                case "文字方塊":
                                    qusInputAreaText = `
                                    <input type="text" name="${questionList[i].QuestionID}" value="${tempUserAnswerList[0]}"${isRequiredClassText} />
                                    `;
                                    break;
                                case "文字方塊 (數字)":
                                    qusInputAreaText = `
                                    <input type="number" name="${questionList[i].QuestionID}" value="${tempUserAnswerList[0]}"${isRequiredClassText} />
                                    `;
                                    break;
                                case "文字方塊(Email)":
                                    qusInputAreaText = `
                                    <input type="email" name="${questionList[i].QuestionID}" placeholder="請輸入Email" value="${tempUserAnswerList[0]}"${isRequiredClassText} />
                                    `;
                                    break;
                                case "文字方塊 (日期)":
                                    qusInputAreaText = `
                                    <input type="date" name="${questionList[i].QuestionID}" value="${tempUserAnswerList[0]}"${isRequiredClassText} />
                                    `;
                                    break;
                                case "單選方塊":
                                    var optionText = `${questionList[i].OptionContent}`.split(';');
                                    for (let j = 0; j < optionText.length; j++) {
                                        if (tempUserAnswerList[j] === 'true') {
                                            qusInputAreaText += `<input type="radio" checked="checked" name="${questionList[i].QuestionID}" value="${j + 1}"${isRequiredClassText} />${optionText[j]}<br/>`;
                                        } else {
                                            qusInputAreaText += `<input type="radio" name="${questionList[i].QuestionID}" value="${j + 1}"${isRequiredClassText} />${optionText[j]}<br/>`;
                                        }
                                    }
                                    break;
                                case "複選方塊":
                                    var optionText = `${questionList[i].OptionContent}`.split(';');
                                    for (let j = 0; j < optionText.length; j++) {
                                        if (tempUserAnswerList[j] === 'true') {
                                            qusInputAreaText += `<input type="checkbox" name="${questionList[i].QuestionID}" checked="checked" value="${j + 1}"${isRequiredClassText} />${optionText[j]}<br/>`;
                                        } else {
                                            qusInputAreaText += `<input type="checkbox" name="${questionList[i].QuestionID}" value="${j + 1}"${isRequiredClassText} />${optionText[j]}<br/>`;
                                        }
                                    }
                                    break;
                            }
                        }


                        questionText += `<div class='question'>${questionList[i].NO}.${questionList[i].QuestionContent} ${isRequiredText}<br/>`;
                        questionText += `${qusInputAreaText}</div>`;
                    }

                    $("#questionDataArea").empty();
                    $("#questionDataArea").append(questionText);

                    $("#pQusCount").empty();
                    $("#pQusCount").append(`共 ${questionList.length} 個問題`);

                    //如果使用者資料不為NULL就帶入使用者資料到對應欄位
                    if (userData != null) {
                        $("#txtUserName").val(userData.Name);
                        $("#txtMobil").val(userData.Mobile);
                        $("#txtEmail").val(userData.Email);
                        $("#txtAge").val(userData.Age);
                    }

                    if ('<%=this.NotEnable%>' === 'true') {
                        let tempInput = document.getElementsByTagName('input');
                        for (let item of tempInput) {
                            item.setAttribute('onclick', 'return false;');
                            item.setAttribute('readonly', 'readonly');
                        }
                        $("#btnSend").css("visibility", "hidden");
                    }
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        });


        //當按下送出按鈕時
        $("#form").submit(function () {
            const userInputAnswer = document.getElementsByTagName("input");
            let userAnsDataList = new Array();
            //將使用者資料放入暫存的自定義型別中
            let userData = {
                Name: userInputAnswer[2].value,
                Mobile: userInputAnswer[3].value,
                Email: userInputAnswer[4].value,
                Age: userInputAnswer[5].value
            };
            //將使用者答案放入暫存的自定義型別中
            for (let i = 6; i < userInputAnswer.length - 2; i++) {
                if (userInputAnswer[i].type === "radio" || userInputAnswer[i].type === "checkbox") {
                    let userAns = {
                        QuestionID: userInputAnswer[i].name,
                        OptionNO: userInputAnswer[i].value,
                        Answer: userInputAnswer[i].checked
                    };
                    userAnsDataList[i - 6] = userAns;
                } else {
                    let userAns = {
                        QuestionID: userInputAnswer[i].name,
                        OptionNO: 0,
                        Answer: userInputAnswer[i].value
                    };
                    userAnsDataList[i - 6] = userAns;
                }

            }

            var postData = {
                "questionnaireID": questionnaireID,
                "userData": userData,
                "userAnsDataList": userAnsDataList
            };
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=Form&Action=Send",
                method: "POST",
                data: postData,
                success: function () {
                    location.href = `ConfirmPage.aspx?ID=${questionnaireID}`;
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        });

        //當按下送出按鈕時
        $("#btnCancel").click(function () {
            $.ajax({
                url: "../API/FrontHandler.ashx?Page=Form&Action=Cancel",
                method: "POST",
                success: function () {
                    location.href = `List.aspx`;
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });
        });

    </script>
</asp:Content>
