# Roles

[[toc]]

In order to create or manage any roles your User will have to have the role `Admin`. This role can only be aquired by requesting it from your server administrator.

:::tip Remember Authorization
Kindly remember to include the `Authorization` JWT token in the request header when making these requests by adding an `Authorization` header with the value `Bearer {token}`
:::

## Retreive all Roles
`GET` Retreive all roles Endpoint
```bash
http://microservice.test/api/auth/roles
```

`BODY` Roles Response
```json
{
    [
        { "name": "Admin" },
        { "name": "User" }
    ]
}
```

## Creating Roles
`POST` Create Role Endpoint
```bash
http://microservice.test/api/auth/roles
```

`BODY` Create Role Request
```json
{
    "name": "User"
}
```

Creating roles returns a `204 - No Content` Response if the role has been created successfully.

## Assigning Roles
`POST` Assign Role Endpoint
```bash
http://microservice.test/api/auth/users/{userId}/roles
```

`BODY` Assign Role Request
```json
{
    "name": "User"
}
```

Assign roles returns a `204 - No Content` Response if the role has been assigned successfully.