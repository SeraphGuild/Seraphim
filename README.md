# Seraphim
Contains the backend code that powers Seraph's discord bot

## Refreshing deployment service principal creds

If credentials for the service principal that is used for akv and deployment needs to be updated, run the following command:

`az ad sp credential reset --name srphm-sp-dev`

Make sure you update the environment variable and the credentials you use to authenticate with AKV in the app settings