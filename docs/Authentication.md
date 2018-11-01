# Autentication

Authentication module use identity server 4 and expose part of its features.
Main target of RawCMS authentication is:

- identify user and use user info to make different things (i.e. profile data and feature)
- use an external identity server to validate user and get user info
- in case you do not have an identity server, rawcms can act as an identity server

Please do not consider RawCMS as an identity server: we expose only minimal feature to make the system authonumus.


## Users
when user are stored locally, they are saved into `_users` collection, with following structure

```json
{
    "_id" : ObjectId("5bb7d9dae0fb5006ec9fe4cc"),
    "Id" : null,
    "UserName" : "bob",
    "NormalizedUserName" : "BOB",
    "Email" : "test@test.it",
    "NormalizedEmail" : "test@test.it",
    "PasswordHash" : "WFla",
    "Roles" : [],
    "Metadata" : {},
    "Claims" : [],
    "_createdon" : "2018-10-05T23:38:34.5793965+02:00",
    "_modifiedon" : "2018-10-05T23:38:34.5819543+02:00"
}
```

Metadata is a custom part where you can add custom user info.

## Tests in standalone mode

### 0. Understand a little what to call
 try to hit `http://{host}/.well-known/openid-configuration` to get info about available endpoints

### 1. Get the token

POST `http://{host}/connect/token`

**Headers**
```
Content-Type:application/x-www-form-urlencoded
```
**Body**
```
grant_type:password
client_id:raw.client
client_secret:raw.secred
scope:openid
username:bob
password:XYZ
```

**Result**
```json
{
    "access_token": "....",
    "expires_in": 3600,
    "token_type": "Bearer"
}
```
### 2. check for introspection

POST `http://{host}/connect/introspect`


**Headers**
```
Authorization:Basic <xxx>
Content-Type:application/x-www-form-urlencoded
```
where \<xxx\> is the standard basic authentication using username=api resource name, password=client secret. To compute it manually, just make base64 of string "apireousource:clientsecret", in case of default values (apiresource=rawcms, clientsecret=raw.secret) is:
 `cmF3Y21zOnJhdy5zZWNyZXQ=`


**Body**
```
grant_type:password
client_id:raw.client
client_secret:raw.secred
scope:openid
username:bob
password:XYZ
```

**Result**
```json
{
    "access_token": "....",
    "expires_in": 3600,
    "token_type": "Bearer"
}
```
### 2. check for identity
POST `http://{host}/api/lambda/UserInfo`

**Headers**
```
Authorization:Bearer <sdfghjk>
```

**Response**
```json
{
    "IsAuthenticated": true,
    "nbf": "1541079731",
    "exp": "1541083331",
    "iss": "http://{host}",
    "aud": "rawcms",
    "client_id": "raw.client",
    "sub": "5bb7d830cc85173af89621d5",
    "auth_time": "1541079731",
    "idp": "local",
    "scope": "openid",
    "amr": "pwd"
}
```