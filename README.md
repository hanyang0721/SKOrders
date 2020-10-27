## 群益懶人下單機 SKOrders 

群益自動下單機 </br>
需搭配python backtrader <https://www.backtrader.com/> </br>
適用於1分K或更長策略. 不適用於短tick的操作策略. 因process cycle約需1sec(1000ms)

### 缺一不可
https://github.com/hanyang0721/SKQuotes  
https://github.com/hanyang0721/ATMMonitor  
https://github.com/hanyang0721/Stock-Database  
https://github.com/hanyang0721/Backtrader  
https://github.com/hanyang0721/SKOrders

安置完畢後需設定兩個工作排程定時啟動SKOrder& SKQoute跟ATMMonitor

### 工作環境
* 群益API 版本2.13.23(2020 June釋出), 必要環境輔助安裝工具 (Visual C++ 可轉發套件) https://www.microsoft.com/en-us/download/confirmation.aspx?id=26999
* Python v.3.6.8
* SQL Server Developer 2017
* Windows 10 Pro
* pip install line-bot-sdk

### 功能
1. 透過AccurateTimer執行策略運算, 策略運算後隨即對透過群益API下單
2. 內建預設8:46會執行第一次, timer interval為1分
3. 提供line下單提醒
4. 提供參數檔execonfig, 儲存使用者自訂username, password, 策略script路徑, 期貨帳號
5. 提供多策略下單

### 更新
<i>2019-09-25</i>
1. 改用AccurateTimer(multimedia timer)降低內建C# timer導致的時間差.
2. ~~新增skip order功能, 當訊號發出時不做下單動作, 僅發line提醒~~</br>

<i>2019-10-22</i>
1. 使用multithreading方式支援多策略下單 
2. 設定ThreadPriority.Highest避免時間差.

<i>2020-10-28</i>
1. 新增clibration function, 作用於當行情與local machine時間不準時將timer提早跑.因行情接收會有起碼300-400ms delay. 
2. 支援一分k
3. 新增成交回報, 委託表tblOrder_Ticket, 成交表tblSKOrderReply
4. 取消下單機佔據佔據行情連線數, SKCenterLib_LogInSetQuote(ID, password ,N) 代入ID及密碼, 並且設定N代表停用報價功能

### 參數說明

| Key       | Value           | 用途  |
| ------------- |:-------------:|------|
| TotalMorningStrategies      | int | 早盤策略總數 |
| TotalNightStrategies      | int      | 夜盤策略總數 |
| morning_strat | 參數路徑      |   morning_strat+int int從0開始  |
| calib_timer_enabled | int      |   是否啟用校正  |
| calib_durationms | int      |   預設一小時校正一次timer時間  |
| durationms | int      |   主要timer, 預設每分鐘run一次  |


### 使用方式
1. 設定Appconfig裡的username, password, futureaccount(例F020xxxxxx), python執行檔路徑, 策略路徑.  
2. 設定linepush裡的兩個token CHANNEL_ACCESS_TOKEN, to, line設定可參考這篇https://xiaosean.github.io/chatbot/2018-04-19-LineChatbot_usage/ 
3. 工作排程(Task scheduler)設定8:45與15:00啟powershell

### 注意事項
* 群益API版本必須一致, 否則程式可能開不起來
* 必須有群益期貨帳號, 並且開通 API 使用權限後才能使用
* Line push功能需有自己的line@, Channel Access Token (https://developers.line.biz)
* 建議啟用Windows Time service做time syncronization, 如用vmware可使用vmtool, VM可能會有時間不同步問題

### 程式畫面
![image](https://github.com/hanyang0721/image/blob/master/SKOrder.png)

