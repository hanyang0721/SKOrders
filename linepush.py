from linebot import LineBotApi
from linebot.models import TextSendMessage, ImageSendMessage
from linebot.exceptions import LineBotApiError
import DBconnect

CHANNEL_ACCESS_TOKEN = ""
to = ""

line_bot_api = LineBotApi(CHANNEL_ACCESS_TOKEN)

try:
    database = None
    database = DBconnect.DBconnect('localhost', 'Stock', 'trader', 'trader')
    database.Connect()
    result = database.GetNotifyOrders()
    for msg in result:
        if msg[7] == 'Alarm':
            line_bot_api.push_message(to, TextSendMessage(text='AlarmTime: ' + str(msg[2].replace(microsecond=0)) + ' Alarm Message:' + str(msg[6])))
        if msg[7] == 'Order':
            line_bot_api.push_message(to, TextSendMessage(text='NEW ORDER: ' + str(msg[2]) + ' BuyOrSell:' + str(msg[3]) + ' Size:' + str(msg[4]) + ' Price:' + str(msg[5]) + ' Type:' + str(msg[6]) + ' Strategy: ' + str(msg[1])))
        database.UpdateNotifyOrders(msg[0])
except LineBotApiError as e:
    # error handle
    print(e.message)
    raise e

