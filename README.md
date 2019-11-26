## 群益自動下單機 SKOrders 

群益自動下單機 </br>
需搭配python backtrader <https://www.backtrader.com/> </br>
適用於5分K或更長策略. 不適用於tick級的操作策略

### 工作環境
* 群益API 版本2.13.16, 必要環境輔助安裝工具 (Visual C++ 可轉發套件) https://www.microsoft.com/en-us/download/confirmation.aspx?id=26999
* Python v.3.6.8
* SQL Server Developer 2016
* Windows 10 Pro
* pip install line-bot-sdk

### 功能
1. 透過timer固定時間執行策略運算, 由策略更新Order資料庫, 程式隨即對透過群益API下單
2. 內建預設8:50會執行第一次, timer interval為5分
3. 提供line下單提醒(目前僅由database order筆數判斷, 之後會新增回報功能比對)
4. 參數檔execonfig, 提供使用者自訂username, password, 策略script路徑

### 更新
<i>2019-09-25</i>
1. 改用AccurateTimer(multimedia timer)降低內建C# timer導致的時間差.
2. 新增skip order功能, 當訊號發出時不做下單動作, 僅發line提醒</br>

<i>2019-10-22</i>
1. 使用multithreading方式支援多策略下單 
2. 使用highest priority避免時間差.

### 使用方式
設定Appconfig裡的username, password, futureaccount(例F020xxxxxx), python執行檔路徑, 策略路徑.  
設定linepush裡的兩個token  
CHANNEL_ACCESS_TOKEN = ""  
to = ""  
line的設定可參考這篇https://xiaosean.github.io/chatbot/2018-04-19-LineChatbot_usage/  
工作排程(Task scheduler)設定時間啟用, 程式開啟後會帶入config內的參數做自動登入  
程式每固定時間重新運算後至寫入db紀錄後馬上將OrderLog撈出送訊號, 整個流程約1秒內可完成.  

### 注意事項
* 群益API版本必須一致, 否則程式可能開不起來
* 必須有群益期貨帳號, 並且開通 API 使用權限後才能使用(通常為申請API隔日生效)
* Line push功能需有自己的line@, Channel Access Token (https://developers.line.biz)
* 必須啟用Windows Time service做time syncronization, 如用vmware可使用vmtool

### 程式畫面
![image](https://github.com/hanyang0721/SKOrders/blob/master/SKOrder.PNG)

