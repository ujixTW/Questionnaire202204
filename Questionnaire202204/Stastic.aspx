<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="Questionnaire202204.Stastic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #statisticsContent > div {
            margin-bottom: 30px;
        }
        /*警告文字*/
        .errorMsg {
            color: #f88;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="statisticsContent">
    </div>
    <div>
        <asp:Button ID="btnSure" runat="server" Text="確定" OnClick="btnSure_Click" />
    </div>

    <script>
            var questionnaireID = "<%=this.QuestionnaireID%>";


            $(document).ready(function () {
                let postData = {
                    "questionnaireID": questionnaireID
                };
                $.ajax({
                    url: "../API/FrontHandler.ashx?Page=Stastic",
                    method: "POST",
                    data: postData,
                    success: function (objDataList) {

                        var questionList = objDataList.questionList;
                        var answerStatisticsList = objDataList.answerStatisticsList;
                        var ChartsDataList = [];

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
                                    qusTypeText = `<canvas id="pie${questionList[i].QuestionID}" style="width:100%;max-width:700px"></canvas>`;
                                    qusTypeText += CreateStatisticsOptionText(i, questionList, answerStatisticsList);
                                    ChartsDataList[ChartsDataList.length] = CreateStatisticsChartsData(i, questionList, answerStatisticsList);
                                    break;
                                case "複選方塊":
                                    qusTypeText = `<canvas id="pie${questionList[i].QuestionID}" style="width:100%;max-width:700px"></canvas>`;
                                    qusTypeText += CreateStatisticsOptionText(i, questionList, answerStatisticsList);
                                    ChartsDataList[ChartsDataList.length] = CreateStatisticsChartsData(i, questionList, answerStatisticsList);
                                    break;
                            }
                            statisticsDataText += `
                            <%--題目--%>
                            <div>
                                <p>${questionList[i].NO}.${questionList[i].QuestionContent}${isRequiredText}</p>
                                <%--答案--%>
                                ${qusTypeText}
                            </div>
                            `;

                        }

                        statisticsDataText = (objDataList.IsAnswered) ? statisticsDataText : "<p class='errorMsg'>目前無人作答</p>" ;

                        $("#statisticsContent").empty();
                        $("#statisticsContent").append(statisticsDataText);

                        CreateCharts(ChartsDataList);

                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("連線失敗，請聯絡管理員。");
                    }
                });

                function CreateCharts(ChartsDataList) {
                    //生成圓餅圖
                    for (let i = 0; i < ChartsDataList.length; i++) {
                        let questionID = ChartsDataList[i].questionID;
                        let choisePercent = ChartsDataList[i].PercentList;
                        let optionText = ChartsDataList[i].optionText;
                        let choiseNumList = ChartsDataList[i].choiseNumList;
                        let xValues = [];
                        let yValues = [];
                        let barColors = [];

                        for (let j = 0; j < optionText.length; j++) {
                            if (!choiseNumList[j]) {
                                choiseNumList[j] = 0;
                            }
                            xValues[xValues.length] = `${optionText[j]}(${choiseNumList[j]})`;
                            let r = Math.floor(Math.random() * 256);
                            let g = Math.floor(Math.random() * 256);
                            let b = Math.floor(Math.random() * 256);

                            barColors[barColors.length] = `rgb(${r},${g},${b})`;
                        }
                        for (let item of choisePercent) {
                            yValues[yValues.length] = item;
                        }

                        new Chart(`pie${questionID}`, {
                            type: "pie",
                            data: {
                                labels: xValues,
                                datasets: [{
                                    backgroundColor: barColors,
                                    data: yValues
                                }]
                            }
                        });
                    }
                }


            });

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
                let qusPercentTotal = "";


                for (let j = 0; j < optionText.length; j++) {
                    if (!choiseNumList[j]) {
                        choiseNumList[j] = 0;
                    }
                    let choisePercent = (choiseCount <= 0) ? 0 : Math.round(choiseNumList[j] / choiseCount * 10000) / 100;
                    qusTypeText += `
                    <tr>
                        <td colspan='2'>${optionText[j]}</td>
                    </tr>
                    <tr>
                        <td style="width:70%; padding-left:50px;">
                            <div style="width:100%; height:20px; border:3px solid #000;"><div style="width:${choisePercent}%; height:100%; background-color:#aaa;"></div></div>
                        </td>
                        <td style="padding-left:50px;">
                            ${choisePercent}% (${choiseNumList[j]})
                        </td>
                    </tr>
                    `;
                }
                qusPercentTotal += `<table style="width:100%;">${qusTypeText}</table>`;
                return qusPercentTotal;
            }

            //產生圓餅圖用資料
            function CreateStatisticsChartsData(i, questionList, answerStatisticsList) {
                //選項文字陣列
                var optionText = `${questionList[i].OptionContent}`.split(';');
                var questionID = `${questionList[i].QuestionID}`;
                var choiseNumList = [];
                var choiseCount = 0;
                for (let j = 0; j < answerStatisticsList.length; j++) {
                    //將問題選擇數量對上對應的問題選項
                    if (answerStatisticsList[j].QuestionID === questionID) {
                        choiseNumList[answerStatisticsList[j].OptionNO - 1] = answerStatisticsList[j].AnswerStatistics;
                        choiseCount += answerStatisticsList[j].AnswerStatistics;
                    }
                }
                //圓餅圖資料
                let PercentList = [];
                for (let j = 0; j < optionText.length; j++) {
                    if (!choiseNumList[j]) {
                        choiseNumList[j] = 0;
                    }
                    let choisePercent = (choiseCount <= 0) ? 0 : Math.round(choiseNumList[j] / choiseCount * 10000) / 100;
                    PercentList[PercentList.length] = choisePercent;
                }
                //輸出成圓餅圖用文字
                let ChartsData = {
                    questionID: questionID,
                    PercentList: PercentList,
                    optionText: optionText,
                    choiseNumList: choiseNumList
                };

                return ChartsData;
            }


    </script>



</asp:Content>
