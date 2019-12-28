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


"""
#圖片訊息
# ImageSendMessage物件中的輸入
# original_content_url 以及 preview_image_url都要寫才不會報錯。
#輸入的網址要是一個圖片，應該說只能是一個圖片，不然不會報錯但是傳過去是灰色不能用的圖
line_bot_api = LineBotApi(CHANNEL_ACCESS_TOKEN)
image_url = "https://i.imgur.com/eTldj2E.png?1"
try:
    line_bot_api.push_message(to, ImageSendMessage(original_content_url=image_url, preview_image_url=image_url))
except LineBotApiError as e:
    # error handle
    raise e
"""
