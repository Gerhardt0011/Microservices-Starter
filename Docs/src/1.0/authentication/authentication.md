# Authentication Basics

[[toc]]

In order to use any of the services you will have to [authenticate yourself](#login), if you already have an account registered you can simply login to your account, alternatively you may [register for a new account](#register).

Once successfully logged in, you will receive a [JWT Token](#jwt-token) in the response body that you can use in the `headers` of subsequent requests to other services. eg: `Authorization: Bearer {token}`

:::tip JWT Token Lifetime

All JWT tokens have a **lifetime of 15 Minutes**, be sure to refresh your JWT Token using your refresh token every 15 minutes to avoid having to re-authenticate. Refresh Tokens are automatically set in your Cookies. [Learn more](#refresh-tokens) about refreshing your JWT Token.
:::

## Login

`POST` Login Endpoint
```bash
http://microservice.test/api/auth/login
```

`BODY` Login Request
```json
{
    "email": "john@example.com",
    "password": "MySuperSecurePassword@123!"
}
```

`RESPONSE` Example of successful Login Response
```json
{
    "success": true,
    "token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9....",
    "errors": []
}
```


`RESPONSE` Example of failed Login Response
```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-830ea4ee2af9b72f44ee7a355e52b981-233d6c0c434da869-00",
    "errors": {
        "Email": [
            "The Email field is not a valid e-mail address."
        ]
    }
}
```

## Register
`POST` Registration Endpoint
```bash
http://microservice.test/api/auth/register
```

`BODY` Registration Request
```json
{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "password": "MySuperSecurePassword@123!"
}
```

`RESPONSE` Example of successful Registration Response
```json
{
    "success": true,
    "token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9....",
    "errors": []
}
```

`RESPONSE` Example of failed Registration Response
```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-830ea4ee2af9b72f44ee7a355e52b981-233d6c0c434da869-00",
    "errors": {
        "Email": [
            "The Email field is not a valid e-mail address."
        ]
    }
}
```

## JWT Token

When you login, register or call the refresh endpoints you will receive a `token` as a property on the response body, this token is a JWT token that you will have to include in the `headers` section of all subsequent requests to any services. This token is used to identify you as well as your roles & permissions. This token has a lifetime of 15 Minutes and will have to be refreshed by calling the [refresh endpoint](#refresh-tokens).

In order to send this token to any of the services, simply add an `Authorization` header with the value `Bearer {token}`

Example JWT Token
```json
eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJnZXJoYXJkQGljdGdsb2JlLmNvbSIsImp0aSI6ImVlNGQ3OTc2LTFiZjMtNDljMy04YzE4LTE1YjkyYWIzOTBiMiIsImVtYWlsIjoiZ2VyaGFyZEBpY3RnbG9iZS5jb20iLCJpZCI6IjYzMDRhZDhkNmY3MGI0OGY4NjE4NGQ5OCIsIm5iZiI6MTY2MTMyOTE5NywiZXhwIjoxNjYxMzMwMDk3LCJpYXQiOjE2NjEzMjkxOTd9.jqeFPBpDqowF-VNAtZr72nJuC5CuHuYWoMMk6r_rBoEquC0ZsNZ4bv698uVx44jQRXckMaHftLw2ojpEAzXb4BLKiZve18Wkhx30z4GLOVOukkqMZFIKAuOvggCG_lpAD8tlpSf1H1rMDmw8YsSCFljU_AuvDFeguwMd9xdDr56klXO8BMUGOz3-SV3MTPwMPyI1WqsvbYlL9_Wbb8ajN69AxnN7CF4XbTtHT08R0wNR32fHW9CI8RCGH_sQFk-QQXn2zffJTFZiz94TPKHnD-G0NFTaThYx1_dCcYW3FHQrmBP-dZVlAqbp0MqQxcarFx3q3XOOjTZP5fqOFkdwbQ
```

## Refresh Tokens

Refresh tokens are used in conjunction with JWT tokens in order to re-authenticate your session automatically without you needing to re-authenticate using an email address and password again. 

Refresh tokens are returned as an http-only `Set-Cookie` and will set a cookie in your browser with the key `refreshToken`, this cookie should be sent in the headers of your request along with the JWT token in the request body in order to validate and refresh your JWT token.

:::tip Refresh token lifetime
Refresh tokens are valid for 6 Months, note that from time to time we may have to invalidate all refreshTokens, in which case you will have to re-authenticate again using the [login endpoint](#login)
:::

`POST` Refresh Token Endpoint
```bash
http://microservice.test/api/auth/refresh
```

`BODY` Refresh Token Request
```json
{
    "token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9...."
}
```

`RESPONSE` Example of failed Refresh Response
```json
{
    "success": false,
    "errors": {
        "token": [
            "The provided token is invalid."
        ]
    }
}
```

`RESPONSE` Example of successful Refresh Response
```json
{
    "success": true,
    "token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9....",
    "errors": []
}
```

## Authenticated User

You can use this endpoint to retreive information about the currently authenticated user.

:::tip Remember Authorization
Kindly remember to include the `Authorization` JWT token in the request header when making this request by adding an `Authorization` header with the value `Bearer {token}`
:::

`GET` Authenticated User Endpoint
```bash
http://microservice.test/api/auth/users/me
```

`RESPONSE` Example of successful Response
```json
{
    "id": "6304bb385f32c19150bf2913",
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com"
}
```

Failed responses will have an empty response body, and will simply return a `401 - Unauthorized` Response. If you get this response code, ensure that you have included the `Authorization` header as previously mentioned. 

If the header has been included and it still returns a 401, it may be that your token has expired, and you will have to call the [Refresh Endpoint](#refresh-tokens) in order to generate a new JWT Token.