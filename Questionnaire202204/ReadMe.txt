簡介
    此為不具使用者登入功能之動態問卷系統。
    前台:
        使用者可看見全數未被刪除的問卷，並可填寫狀態為投票中的問卷。
        不論問卷狀態，使用者皆可觀看該問卷的選擇題統計資料。
    後台:
        管理者可自由編輯、新增問卷、觀看該筆問卷的統計資料，以及編輯問卷中可以使用的常用問題。
        若已有使用者填寫問卷，則該問卷將不可編輯除是否啟用、結束時間以外的資料。
        管理者可下載具使用者填寫資料的CSV檔於D槽中的tmp資料夾。
        

環境準備
    DB設置
        DB資料夾>
            >Questionnaire.bak          資料庫備份
            >QuestionnaireData.sql      資料庫資料指令碼
            >QuestionnaireStructure.sql 資料庫資料結構指令碼
開始
    後台>
        >SystemAdmin >List.aspx     後台列表頁
    前台>
        >List.aspx                  前台列表頁


後台內頁存有session
    questionnaireData           問卷頁面資料

    questionDataList            問題頁面清單資料

    commonlyQuestionList        常用問題資料清單

    DBQuestionDataListCount     紀錄DB內問題清單長度

    userDataList                單一問卷填寫者紀錄

    userDataListCount           單一問卷填寫者數量

    IsAnswered                  是否有人已做過問卷

常用問題編輯/新增頁面存有Session
    commonlyQuestionListCount   常用問題資料總數

前台問卷列表存有Session
    
    questionnaireDataList       問卷列表頁面資料清單

前台內頁存有Session
    
    questionnaireData           問卷資料

    questionDataList            問題清單資料

    userAnswerList              使用者答案清單

    userData                    使用者資料

