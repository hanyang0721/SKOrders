# SKOrders" 

群益自動下單機(需搭配python backtrader https://www.backtrader.com/)
適用於5分K或更長策略

# 工作環境
群益API版本 2.13.16
Python v.3.6.8
SQL Server 2016
Windows 10 Professional

# 步驟
設定Appconfig裡的username, password, futureaccount(例F020xxxxxx), python執行檔路徑, 策略路徑.
在工作排程(Task scheduler)設定時間啟用, 程式開啟後會帶入config內的參數做自動登入

# 原理
將寫好的backtrader策略放入C#, 利用process啟起來. 寫入db紀錄後馬上將OrderLog撈出送訊號, 整個流程約1秒內可完成.
timer預設第一次執行時間為早上8:50,  並在收盤前3秒執行策略判斷是否有當沖需求 
目前下單皆為市價單
