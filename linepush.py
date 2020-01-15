from linebot import LineBotApi
from linebot.models import TextSendMessage, ImageSendMessage
from linebot.exceptions import LineBotApiError
import DBconnect

CHANNEL_ACCESS_TOKEN = "d7bpIjXLueJZU57+8uTAh8c0Jgu56nAJbzZ+v9xdwb3oc6U4ZFeDefeMfUOZS6RCLZBWdM/FeLcdqVXtyTXmSbp6IFgaK447DgKYMwKjTQuddRL+LBcjTsi/ybgn93PC6n3Wg+CqJAGu8nkl6EVf3QdB04t89/1O/w1cDnyilFU="
to = "U534ce399cdf9e0bd77908f1f03599826"

line_bot_api = LineBotApi(CHANNEL_ACCESS_TOKEN)

try:
    database = None
    database = DBconnect.DBconnect('localhost', '', '')
    database.Connect()
    result = database.GetNotifyOrders()
    for msg in result:
        if msg[7] == 'Alarm':
            line_bot_api.push_message(to, TextSendMessage(text='AlarmTime: ' + str(msg[2].replace(microsecond=0)) + ' Alarm Message:' + str(msg[6])))
        if msg[7] == 'Order':
            line_bot_api.push_message(to, TextSendMessage(text='NEW ORDER: ' + str(msg[2]) + ' BuyOrSell:' + str(msg[3]) + ' Size:' + str(msg[4]) + ' Price:' + str(msg[5])))
        database.UpdateNotifyOrders(msg[0])
except LineBotApiError as e:
    # error handle
    print(e.message)
    raise e

