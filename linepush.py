from linebot import LineBotApi
from linebot.models import TextSendMessage, ImageSendMessage
from linebot.exceptions import LineBotApiError
import DBconnect

CHANNEL_ACCESS_TOKEN = ""
to = ""

line_bot_api = LineBotApi(CHANNEL_ACCESS_TOKEN)

#文字訊息

try:
    database = None
    database = DBconnect.DBconnect('localhost', 'trader', 'trader')
    database.Connect()
    result = database.GetNotifyOrders()
    for order in result:
        database.UpdateNotifyOrders(order[0])
        line_bot_api.push_message(to, TextSendMessage(text='NEW ORDER: ' + str(order[2]) + ' BuyOrSell:' + str(order[3]) + ' Size:' + str(order[4]) + ' Price:' + str(order[5])))
except LineBotApiError as e:
    # error handle
    raise e


