# Simple example for creating a Telegram bot running in a Azure Function

## Prerequisites
    1. Function app for .NET core running in Azure
    2. A Telegram bot and its API key given by @BotFather

## How to get things running

Setting up Telegram API key for sending messages:
1. Go to your Function App -> Platform features -> Application settings
2. Create a setting under "Application settings" called "TelegramBotApiKey"
3. Set its value to the key given by botfather
4. Save it

### Publishing code:

1. Right click the project "CloudTgBotCore3" and select "Publish"
2. Select “New” -> Azure Functions Consumption Plan -> Select Existing
3. Search for your Function App under your Resource Group
4. Publish!
5. If VS asks for updating the function Runtime on the first deployment, click OK


### Webhook setup:

1. Get your token from botfather in Telegram
2. Post to 
```https://api.telegram.org/bot<token>/setWebhook?url=<functionAppName>/api/MyTelegramEndpoint&allowed_updates=["message"]```. Remember to replace the token and functionAppName with your personal values. Remember to include the "bot" before your token!
3. Powershell command example: ```Invoke-WebRequest -Uri "https://api.telegram.org/bot<token>/setWebhook?url=https://<functionAppName>.azurewebsites.net/api/MyTelegramEndpoint&allowed_updates=['message']" -Method POST```
4. You should now get post messages from messages!

### Security reminders!
1. If you plan to build a real application and you want to protect it, consider renaming the endpoint to something that is less obvious (change "MyTelegramEndpoint to something else").
2. Restrict IPs that are allowed to communciate with your API, IPs used by Telegram can be found at https://core.telegram.org/bots/api. Setti
3. Consider storing the bot API key in Azure Keyvault (more at https://docs.microsoft.com/en-us/azure/app-service/app-service-key-vault-references).


### OPTIONAL:

If you want to publish local.settings.json to Azure you can use this. Not needed in this example really

Publishing settings:
1. Install Azure CLI https://docs.microsoft.com/en-us/cli/azure/get-started-with-azure-cli?view=azure-cli-latest
2. npm install -g azure-functions-core-tools
3. func azure functionapp publish YourFuncAppName --publish-local-settings -i



