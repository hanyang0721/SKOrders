## SKOrders 

群益自動下單機 </br>
需搭配python backtrader <https://www.backtrader.com/> </br>
適用於5分K或更長策略

## 工作環境
群益API版本 2.13.16 
Python v.3.6.8
SQL Server 2016
Windows 10 Professional

## 功能
1. 透過timer固定時間執行策略運算, 由策略更新Order資料庫, 程式隨即對透過群益API下單
2. 內建預設8:50會執行第一次, timer interval為5分
3. 提供line下單提醒(目前僅由database order筆數判斷, 之後會新增回報功能比對)
4. 參數檔execonfig

## 使用方式
設定Appconfig裡的username, password, futureaccount(例F020xxxxxx), python執行檔路徑, 策略路徑.
在工作排程(Task scheduler)設定時間啟用, 程式開啟後會帶入config內的參數做自動登入

## 程式流程 
將寫好的backtrader策略放入C#, 利用process啟起來. 寫入db紀錄後馬上將OrderLog撈出送訊號, 整個流程約1秒內可完成.

##
![https://photos.app.goo.gl/ycwEehiw1Aff3r9J8]

