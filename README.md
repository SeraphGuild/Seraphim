# Seraphim
Contains the backend code that powers Seraph's discord bot

## Refreshing deployment service principal creds

If credentials for the service principal that is used for akv and deployment needs to be updated, run the following command:

`az ad sp credential reset --name srphm-sp-dev`

Make sure you update the environment variable and the credentials you use to authenticate with AKV in the app settings

Be aware that the azure credential used in github actions expect an object of the following format, so you'll need to rename some fields and add the subscription id

```json
{
  "clientId": "",
  "clientSecret": "",
  "subscriptionId": "",
  "tenantId": ""
}
```