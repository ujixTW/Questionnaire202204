<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="Questionnaire202204.Stastic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #statisticsContent > div {
            margin-bottom: 30px;
        }
    </style>

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
                            <div>
                                <p>${questionList[i].NO}.${questionList[i].QuestionContent}${isRequiredText}</p>
                                <%--答案--%>
                                ${qusTypeText}
                            </div>
                            `;

                    }

                    $("#statisticsContent").empty();
                    $("#statisticsContent").append(statisticsDataText);
                },
                error: function (msg) {
                    console.log(msg);
                    alert("連線失敗，請聯絡管理員。");
                }
            });

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
            for (let item of answerStatisticsList) {
                //將問題選擇數量對上對應的問題選項
                if (item.QuestionID === questionID) {
                    choiseNumList[item.OptionNO - 1] = item.AnswerStatistics;
                    choiseCount += item.AnswerStatistics;
                }
            }
            let qusPercentTotal = "";
            //let dataList = [];
            //for (let j = 0; j < optionText.length; j++) {
            //    let choisePercent = (choiseCount <= 0) ? 0 : Math.round(choiseNumList[j] / choiseCount * 10000) / 100;
            //    let data = {
            //        label: `${optionText[j]}:${choisePercent}% (${choiseNumList[j]})`,
            //        data: choisePercent
            //    };
            //    dataList[j] = data;
            //}
            //let options = {
            //    series: {
            //        pie: { show: true }
            //    },
            //    legend: {
            //        show: false
            //    }
            //};
            //qusPercentTotal += `<div>${qusTypeText}</div>`;

            for (let j = 0; j < optionText.length; j++) {
                let choisePercent = (choiseCount <= 0) ? 0 : Math.round(choiseNumList[j] / choiseCount * 10000) / 100;
                qusTypeText += `
                    <tr>
                        <td colspan='2'>${optionText[j]}</td>
                    </tr>
                    <tr>
                        <td style="width:70%; padding-left:50px;">
                            <div style="width:100%; height:30px; border:3px solid #000;"><div style="width:${choisePercent}%; height:100%; background-color:#aaa;"></div></div>
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


    </script>

</asp:Content>
