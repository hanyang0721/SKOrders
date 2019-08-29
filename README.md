"# SKOrders" 

群益自動下單機(需搭配python backtrader https://www.backtrader.com/)
適用於5分K或更長策略

工作環境
Windows 10 Professional
SQL Sever 2016


原理
將寫好的backtrader策略放入C#, 利用process啟起來. 寫入db紀錄後馬上將OrderLog撈出送訊號, 整個流程約1秒內可完成.
程式有判斷是否啟動時間是8:50前, 如果是則會在8:50 run timer1, 其餘時間需手動啟動timer
