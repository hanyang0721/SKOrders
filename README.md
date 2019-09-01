## SKOrders 

群益自動下單機 </br>
需搭配python backtrader <https://www.backtrader.com/> </br>
適用於5分K或更長策略

### 工作環境
* 群益API版本 2.13.16 
* Python v.3.6.8
* SQL Server Developer 2016
* Windows 10 Professional

### 功能
1. 透過timer固定時間執行策略運算, 由策略更新Order資料庫, 程式隨即對透過群益API下單
2. 內建預設8:50會執行第一次, timer interval為5分
3. 提供line下單提醒(目前僅由database order筆數判斷, 之後會新增回報功能比對)
4. 參數檔execonfig, 提供使用者自訂username, password, 策略script路徑

### 使用方式
設定Appconfig裡的username, password, futureaccount(例F020xxxxxx), python執行檔路徑, 策略路徑.
在工作排程(Task scheduler)設定時間啟用, 程式開啟後會帶入config內的參數做自動登入
程式從策略重新計算至寫入db紀錄後馬上將OrderLog撈出送訊號, 整個流程約1秒內可完成.

### 注意事項
* 群益API版本必須一致, 否則程式可能開不起來
* 必須有群益期貨帳號, 並且開通 API 使用權限後才能使用(通常為申請API隔日生效)
* Line push功能需有自己的line@, Channel Access Token (https://developers.line.biz)

### 程式畫面
![image](https://github.com/hanyang0721/SKOrders/blob/master/SKOrder.PNG)

