Notes:

Webhook setup:
1. Get your token from botfather in telegram
2. Post to 
"https://api.telegram.org/bot<token>/setWebhook?url=https://pullonkaulademofunctionapp.azurewebsites.net/api/<endpoint>&allowed_updates=["message"]"
3. You should now get post messages from messages!

Setting up telegram API key for sending messages:
1. Go to your Function App -> Platform features -> Application settings
2. Create a setting under "Application settings" called "TelegramBotApiKey"
3. Set its value to the key given by botfather
4. Save it




OPTIONAL:

If you want to publish local.settings.json to Azure you can use this. Not needed in this example really

Publishing settings:
1. Install Azure CLI https://docs.microsoft.com/en-us/cli/azure/get-started-with-azure-cli?view=azure-cli-latest
2. npm install -g azure-functions-core-tools
3. func azure functionapp publish YourFuncAppName --publish-local-settings -i



