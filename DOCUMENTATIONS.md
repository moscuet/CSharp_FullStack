## Introduction

This documentation will help you get familiar with the resources of the base_url API and show you how to make different queries, so that you can get the most out of it.

## REST API

Base url: https://base_url.com/api/v1

The base url contains information about all available API's resources. All requests are GET requests and go over https. All responses will return data in json.

Below is the list of our top-level apis. For detailed endpoints, see each resource section.

```
https://base_url.com/api
{
  "products": "https://base_url.com/api/v1/products",
  "categories":"https://base_url.com/api/v1/categories",
  "users": "https://base_url.com/api/v1/users",
  "addresses": "https://base_url.com/api/v1/addresses",
  "reviews":"https://base_url.com/api/v1/reviews",
  "orders":"https://base_url.com/api/v1/orders",
  "auth":"https://base_url.com/api/v1/orders",
}
```

## Errors

The api uses conventional HTTP response codes to indicate the success or failure of an API request. In general: Codes in the 2xx range indicate success. Codes in the 4xx range indicate an error that failed given the information provided (e.g., a required parameter was omitted, a charge failed, etc.). Codes in the 5xx range indicate an error with servers (these are rare).

### HTTP STATUS CODE SUMMARY

| Code | Status         | Meaning                                                                  |
| ---- | -------------- | ------------------------------------------------------------------------ |
| 200  | OK             | Everything worked as expected.                                           |
| 400  | Bad Request    | The request was unacceptable, often due to missing a required parameter. |
| 401  | Unauthorized   | No valid API key provided.                                               |
| 402  | Request Failed | The parameters were valid but the request failed.                        |
| 403  | Forbidden      | The API key doesn’t have permissions to perform the request.             |
| 404  | Not Found      | The requested resource doesn’t exist.                                    |
| 500  | Server Errors  | Something went wrong on the server.                                      |

## Pagination

All top-level API resources have support for bulk fetches through get all API methods. The API will automatically paginate the responses if no parameter is provided.

### Parameters

| Key            | Is optional | Default | Description                                                                                                                                                                                                                                                                      |
| -------------- | ----------- | ------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| limit          | yes         | 10      | This specifies a limit on the number of objects to return, ranging between 1 and 100.                                                                                                                                                                                            |
| starting_after | yes         | 0       | A cursor to use in pagination. starting_after is an object ID that defines your place in the list. For example, if you make a list request and receive 100 objects, ending with obj_foo, your subsequent call can include starting_after=100 to fetch the next page of the list. |

### Response format

#### info

Pagination metadata, including the following:

| Key          | Type | Description                                                                                                        |
| ------------ | ---- | ------------------------------------------------------------------------------------------------------------------ |
| total_amount | int  | The count of all the items                                                                                         |
| pages        | int  | The amount of total pages                                                                                          |
| current_page | int  | Current requested page                                                                                             |
| has_more     | bool | Whether or not there are more elements available after this set. If false, this set comprises the end of the list. |

#### data

An array containing the actual response elements, paginated by any request parameters.

A sample response:

```
https://base_url.com/api/v1/products
{
  "info": {
    "total_amount": 826,
    "pages": 42,
    "current_page":1,
    "has_more":true,
  },
  "data": [
    // ...
  ]
}
```

You can access different pages with the page parameter. If you don't specify any page, the first page will be shown. For example, in order to access page 2, add ?page=2 to the end of the URL.

```
/api/v1/products?page=19
{
  "info": {
    "total_amount": 826,
    "pages": 42,
    "current_page":19,
    "has_more":true,
  },
  "data": [
    // ...
  ]
}

```

## Versioning

The current version is v1, realeased at:placeholder.

For information on all API updates, view our API changelog.

# Core resources


## Auth JWT

```
ENDPOINTS
[POST]api/v1/auth/login
[POST]api/v1/auth/refresh-token
[GET]api/v1/auth/profile
```

### Login a user

You can login a user by sending a POST request to `/auth/login/`

**Returns**

Returns an access and refresh JWT token.

> Info: The access token is valid for 20 days, and the refresh token is valid for 10 hours.

Request:

```
[POST] api/v1/auth/login
# Body
{
  "email": "john@mail.com",
  "password": "changeme"
}
```

Response:

```
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEsImlhdCI6MTY3Mjc2NjAyOCwiZXhwIjoxNjc0NDk0MDI4fQ.kCak9sLJr74frSRVQp0_27BY4iBCgQSmoT3vQVWKzJg",
  "refresh_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEsImlhdCI6MTY3Mjc2NjAyOCwiZXhwIjoxNjcyODAyMDI4fQ.P1_rB3hJ5afwiG4TWXLq6jOAcVJkvQZ2Z-ZZOnQ1dZw"
}
```

### Refresh the token

You can send a the refreshToken to through a POST request to `auth/refresh-token` to get a new access and refresh JWT token.

Request:

```
[POST] https://api.escuelajs.co/api/v1/auth/refresh-token
# Body
{
  "refreshToken": "{your refresh token}"
}
```

Response:

```
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEsImlhdCI6MTY3Mjc2NjAyOCwiZXhwIjoxNjc0NDk0MDI4fQ.kCak9sLJr74frSRVQp0_27BY4iBCgQSmoT3vQVWKzJg",
  "refresh_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEsImlhdCI6MTY3Mjc2NjAyOCwiZXhwIjoxNjcyODAyMDI4fQ.P1_rB3hJ5afwiG4TWXLq6jOAcVJkvQZ2Z-ZZOnQ1dZw"
}
```

### Get a user with session

You can get the profile of the current user with session by including the Authorization key in the request `Headers` to `/auth/profile`.

**Returns**

Returns the current User object. With invalid credential, it returns an error(access denied).

Request:

```
[GET] v1/auth/profile
# Headers
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
{
    "id": "1e2ddfa02610e48e983824b23ac955632",
    "email":"janedone@mail.com",
    "first_name": "Jane",
    "last_name": "Doe",
    "role": "customer",
    "avatar": "https://i.imgur.com/LDOO4Qs.jpg",
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z"
  }
```

## Users

> TBD:Some users endpoints are dependent on authencation service. Modifications are expected later.

```
ENDPOINTS
[POST]/v1/users
[PUT]/v1/users/:id
[GET]/v1/users
[GET]/v1/users/:id
[DELETE]/v1/users/:id
```

### User schema

| Key        | Type        | Description                                         |
| ---------- | ----------- | --------------------------------------------------- |
| id         | string      | The unique id of the user                           |
| first_name | string      | The first name of the user                          |
| last_name  | string      | The last name of the user                           |
| role       | string      | The role of the user('Admin' or 'SuperAdmin' or 'User')         |
| avatar     | string(url) | Link to the user's avatar(if exists)                |
| email      | string      | The unique email of the user                        |
| created_at | string      | Time at which the user was created in the database. |
| updated_at | string      | Time at which the user was updated in the database. |

### User access

`SuperAdmin` has full access to the web app.

Once role is assigned, it cannot be changed. 

Only `SuperAdmin` role can create `Admin` role.

Only `Admin` and  `SuperAdmin` roles can perform actions on `Product` and `Category`. Some actions of `Order` (change order status).




### Create a user

**Returns**

Returns the User object after successful creation. Raises an error if create parameters are invalid (for example, the email already exists).

Request:

```
[POST] /api/v1/users

# Body
{
  "email": "nico@gmail.com",
  "password": "1234",
}
```

Response:

```
{
    "id": "1e2ddfa02610e48e983824b23ac955632",
    "email":"nico@gmail.com",
    "created_at":"2017-11-04T18:48:46.250Z",
}
```

### Update a user

**Returns**

It returns the updated user object.

You can update an existing user by modifying the following fields in a PUT request:

Request:

```
[PUT] /api/v1/users/1
{
    "first_name": "John",
    "last_name": "Snow",
    "avatar":"https://i.imgur.com/LDOO4Qs.jpg"
}
```

Response:

```
{
    "id": "1e2ddfa02610e48e983824b23ac955632",
    "email":"janedone@mail.com",
    "first_name": "John",
    "last_name": "Snow",
    "role": "customer",
    "avatar": "https://i.imgur.com/LDOO4Qs.jpg",
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2024-11-04T18:48:46.250Z"
}
```

### Rerieve a user

You can get a single user by adding the id as a parameter: `/user/:id`.

> Note: An access token is required in the header.

Request:

```
[GET] /api/v1/user/1e2ddfa02610e48e983824b23ac955632
#Header
{
  "Authorization": "Bearer {your access token}"
}

```

Response:

```
{
    "id": "1e2ddfa02610e48e983824b23ac955632",
    "email":"janedone@mail.com",
    "first_name": "Jane",
    "last_name": "Doe",
    "role": "customer",
    "avatar": "https://i.imgur.com/LDOO4Qs.jpg",
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z"
  }
```

### List all users

You can access the list of users by using the `/users` endpoint.

> Note: An access token is required in the header.

Request:

```
[GET] /api/v1/users
{
  "Authorization": "Bearer {your access token}"
}

```

Response:

```
{
"info": {
    "total_amount": 826,
    "pages": 42,
    "current_page":1,
    "has_more":true,
  },
"data":{
    "id": "1e2ddfa02610e48e983824b23ac955632",
    "email":"janedone@mail.com",
    "first_name": "Jane",
    "last_name": "Doe",
    "role": "customer",
    "avatar": "https://i.imgur.com/LDOO4Qs.jpg",
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z"
  },
  // ...
}
```

### Delete a user

Permanently deletes a user. It cannot be undone. Also immediately deletes all the related data in the database.

**Returns**

Returns an object with a deleted parameter on success. If the use ID does not exist, this call raises an error.

> Note: You need to be authenticated as admin to access this endpoint. An access token is required in the header.

Request:

```
[DELETE]/api/v1/users/cus_NffrFeUfNV2Hib
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
{
    "id":"cus_NffrFeUfNV2Hib",
    "deleted":true
}
```

## Products

### ENDPOINTS

```
[POST]/v1/products
[PUT]/v1/products/:id
[GET]/v1/products
[DELETE]/v1/products/:id
```

### Product Schema

| Key         | Type     | Description                                                     |
| ----------- | -------- | --------------------------------------------------------------- |
| id          | string   | Id of each product                                              |
| name        | string   | Name of the product                                             |
| description | string   | Description about that product                                  |
| price       | double   | Price of the product, with 2 digits to the right of the decimal |
| category_id | string   | Category of that product                                        |
| images      | string[] | Images of that product                                          |
| created_at  | string   | Time and day is product is created                              |
| update_at   | string   | Time and day is product is updated                              |

### Create a Product

**Returns**

Returns the newly created product object if successful, or raises an error if the parameters are invalid.

Request:

```
[POST] /api/v1/products

{
  "name": "New Product",
  "price": 10,
  "description": "A description",
  "category_id": 1,
  "images": ["https://placeimg.com/640/480/any"]
}
```

Response:

```
{
  "title": "New Product",
  "price": 10,
  "description": "A description",
  "images": ["https://placeimg.com/640/480/any"],
  "category": {
    "id": 1,
    "name": "Clothes",
    "image": "https://api.lorem.space/image/fashion?w=640&h=480&r=4278",
    "creationAt": "2023-01-03T15:58:58.000Z",
    "updatedAt": "2023-01-03T15:58:58.000Z"
  },
  "id": 210,
  "creationAt": "2023-01-03T16:51:33.000Z",
  "updatedAt": "2023-01-03T16:51:33.000Z"
}
```

### Retrieve a product

**Parameter**

- "id" (string): id of the product

**Return**

Returns the product with the specified ID.

Request:

`[GET] /api/v1/products/:id`

Response:

```
{
"title": "New Product",
  "price": 10,
  "description": "A description",
  "images": ["https://placeimg.com/640/480/any"],
  "category": {
    "id": 1,
    "name": "Clothes",
    "image": "https://api.lorem.space/image/fashion?w=640&h=480&r=4278",
    "creationAt": "2023-01-03T15:58:58.000Z",
    "updatedAt": "2023-01-03T15:58:58.000Z"
  },
  "id": 210,
  "creationAt": "2023-01-03T16:51:33.000Z",
  "updatedAt": "2023-01-03T16:51:33.000Z"
}
```

### List all products

**Parameters**

| Category     | Parameter   | Type    | Description                                               | Required |
| ------------ | ----------- | ------- | --------------------------------------------------------- | -------- |
| Filtering    | category_id | string  | Filter products by category                               | No       |
|              | price_min   | number  | Filter products with price greater than this              | No       |
|              | price_max   | number  | Filter products with price less than this                 | No       |
| Sorting      | sort_by     | string  | Sort products by specified attribute                      | No       |
|              |             |         | - price: Sort products by their price                     |          |
|              |             |         | - name: Sort products alphabetically by name              |          |
|              |             |         | - rating: Sort products by average rating                 |          |
|              |             |         | - date_added: Sort products by date added                 |          |
|              |             |         | - popularity: Sort products by popularity(number of sold) |          |
|              | sort_order  | string  | Sort order (`asc` or `desc`)                              | No       |
| Pagination   | page        | integer | Page number for pagination                                | No       |
|              | per_page    | integer | Number of items per page                                  | No       |
| Search       | search      | string  | Search products by keyword                                | No       |
| Availability | in_stock    | boolean | Filter products by availability                           | No       |
|              |             |         | - Default: false (return products of all stocks)          | No       |
|              |             |         | - True (Only return available one)                        | No       |

**Return**

Returns a paginated list of products.

Request:

`[GET] /api/v1/products`

```
https://xxx.com/api/v1/products/?page=42
{
  "info": {
    "total_amount": 826,
    "pages": 42,
    "current_page":19,
    "has_more":true,
  },
  "data": [
    // ...
  ]
}
```

### Update a product

**Parameter**

- "id" (string): id of the product

**Return**

Return boolean true if action performed successfully.

Request:

```
[PUT] /api/v1/products/:id

# header
{
  "Authorization": "Bearer {your access token}"
}
{
  "title": "New Product",
  "price": 10,
  "description": "A description",
  "images": ["https://placeimg.com/640/480/any"],
  "category": "avaiable category Id"
}
```

Response:

`true`

### Delete a product

**Parameter**

- "id" (string): id of the product

**Return**

Return boolean true if action performed successfully.

> Note: You need to be authenticated as admin to access this endpoint. An access token is required in the header.

Request:

```
[DELETE] /api/v1/products/:id

#header
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
{
  "id":"sahidaia935953",
  "deleted":true
}
```

## Categories

```
ENDPOINT
[POST]/v1/categories
[PUT]/v1/categories/:id
[GET]/v1/categories
[GET]/v1/categories/:id
[DELETE]/v1/categories/:id
```

### Category Schema

| Key        | Type   | Description                                   |
| ---------- | ------ | --------------------------------------------- |
| id         | string | Id of each category                           |
| name       | string | Name of the category (maximum 255 characters) |
| image      | string | Image about that category                     |
| created_at | string | Time and day is category is created           |
| update_at  | string | Time and day is category is updated           |

### Create a category

**Return**

Returns the newly created category object if successful, or raises an error if the parameters are invalid.

Request:

```
[POST] /api/v1/categories
{
  "name": "New Category",
  "image": "https://placeimg.com/640/480/any"
}
```

Response:

```
{
  "id": "6"
  "name": "New Category",
  "image": "https://placeimg.com/640/480/any",
  "creationAt": "2023-01-03T16:51:33.000Z",
  "updatedAt": "2023-01-03T16:51:33.000Z"
}
```

### Update a category

**Return**

Returns boolean.

> Note: You need to be authorized to perform this action.

Request:

```
[PUT] /api/v1/categories/:id

# header
{
  "Authorization": "Bearer {your access token}"
}

{
  "name":"update",
  "image":"update image"
}
```

Response:

```
true
```

### Retrieve a category

**Return**

Returns the category with the specified ID.

Request:

`[GET] /api/v1/categories/:id`

Response:

```
{
  "id": 1,
  "name": "Clothes",
  "image": "https://api.lorem.space/image/fashion?w=640&h=480&r=4278"
}
```

### List all categories

**Parameters**

You can specify the page number using the page parameter.

**Return**

Returns a paginated list of categories.

Response:

```
{
"info": {
    "total_amount": 826,
    "pages": 42,
    "current_page":1,
    "has_more":true,
  },
"data":[
   // ...
]
}
```

### Delete a category

**Return**
Returns boolean.

Parameters: You can specify the category id. You need to be authorized to perform this action

Request:

```
[DELETE] /api/v1/categories/:id

header
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
{
  "id":"catgajs8_52893",
  "deleted":true
}
```


## Reviews

```
ENDPOINTS
[POST]/v1/reviews
[PUT]/v1/reviews/:id
[GET]/v1/reviews
[DELETE]/v1/reviews/:id
```

### Reviews schema

| Key          | Type    | Description                                           |
| ------------ | ------- | ----------------------------------------------------- |
| id           | string  | The unique id of a review object.                     |
| user_id      | string  | The reviewer's id.                                    |
| product_id   | string  | The reviewed product's id.                            |
| is_anonymous | bool    | Whether the user wants to expose it's identity        |
| content      | string  | A text content of the review.                         |
| rating       | integer | The rating the reviewer gives, from 1-5.              |
| images       | string  | User can upload images to the review.                 |
| created_at   | string  | Time at which the review was created in the database. |
| updated_at   | string  | Time at which the review was updated in the database. |

### Create a review

When you create a new review, you must specify a User and a Product on which to create it.

Other required parameters are:

- rating

**Returns**

Returns the Review object if created succussfully. Raises an error if an invalid identifier was provided.

Request:

```
[POST]api/v1/reviews
{
    "user_id":"user-sajfaskgsk82",
    "product_id":"product_dahifha39359",
    "ratings":5,
    "content":"Good product",
    "is_anonymous":true
}
```

Response:

```
{
    "id":"review_fashifa930fsa",
    "user_id":"user-sajfaskgsk82",
    "product_id":"product_dahifha39359",
    "ratings":5,
    "content":"Good product",
    "is_anonymous":true,
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z"
}
```

### Update a review

Updates the review that you specify by setting the values of the passed parameters. Any parameters that you don’t provide remain unchanged.

**Returns**

Returns the review object if the update succeeds. This call raises an error if update parameters are invalid(for example, review with the provided id not found).

Request:

```
[PUT]api/v1/reviews/review_fashifa930fsa
{
    "rating":1
}
```

Response:

```
{
    "id":"review_fashifa930fsa",
    "user_id":"user-sajfaskgsk82",
    "product_id":"product_dahifha39359",
    "ratings":1,
    "content":"Good product",
    "is_anonymous":true,
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z"
}
```

### Retrieve a review

Retrieves the details of an existing review.

**Returns**

Returns a review if you provide a valid ID. Raises an error otherwise.

Request:

`[GET]api/v1/reviews/review_fashifa930fsa`

Response:

```
{
    "id":"review_fashifa930fsa",
    "user_id":"user-sajfaskgsk82",
    "product_id":"product_dahifha39359",
    "ratings":1,
    "content":"Good product",
    "is_anonymous":true,
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z"
}
```

### List all reviews

Returns a list of all reviews of a certain product or by a certain user in the database. We return the reviews in sorted order, with the most recent reviews appearing first.

#### Parameters

> Notes: We might not support all parameters in the backend, depending on the workload.

**userId** or **productId** _must provide_

If none of the id is provided, the api returns an error response.

**sortBy** _optional,default is createdAt_

Other valid parameters are:

- rating

**orderBy** _optional,default is DESC_

Other valid parameters are:

- ASC

**startingAt** _optional,default is null_

An pointer to when the review is created. It returns results that were created after `startingAt` date(included).

**endingAt** _optional,default is null_

An pointer to when the review is created. It returns results that were created before `endingAt` date(included).

Request:

`[GET]api/v1/reviews?userId=user_faif9204&sortBy=ratings`

Response:

```
{
    "info":{
        "total_amount": 4,
        "pages": 1,
        "current_page":1,
        "has_more":false,
    },
    "data":[
        {
            "id":"review_fashifa930fsa",
            "user_id":"user-sajfaskgsk82",
            "product_id":"product_dahifha39359",
            "ratings":1,
            "content":"Good product",
            "is_anonymous":true,
            "created_at":"2017-11-04T18:48:46.250Z",
            "updated_at":"2017-11-04T18:48:46.250Z"
        },
        // ...
    ]
}
```

### Delete a review

Deletes a review from the database.

**Returns**

Returns the object id if the deletion succeeds. This call raises an error if you can’t delete the review.

Request:

`[DELETE]api/v1/reviews/review_ajksajo909`

Response:

```
{
    "id":"review_ajksajo909",
    "deleted":true
}
```

## Orders

```
ENPOINTS
[POST]api/v1/orders
[PUT]api/v1/orders/:id
[GET]api/v1/orders
[GET]api/v1/orders/:id
[GET]api/v1/orders/:id/items
[DELETE]api/v1/orders/:id
```

### Order Schema

| Key        | Type   | Description                         |
| ---------- | ------ | ----------------------------------- | 
| id         | string | The unique id of an order object    |
| user_id    | string | The orderer's id                    |
| address_id | string | Shipping address object id          |
| status     | enum   | The status of the order(Created,Processing,Completed,Cancelled) |
| created_at | string | The date which the order is created |
| updated_at | string | The date which the order is updated |

### Create an order

Must provide `User` and `Address` objects in creation.

**Returns**

The Order object if created successfully. Otherwise an Error response.

Request:

```
[POST]api/v1/orders
{
    "user_id":"user_fahfajoi99",
    "address_id":"address_daklfajiods9",
    "status":"Created"
}
```

Response:

```
{
    "id":"order_safnasjiofgioes9990",
    "user_id":"user_fahfajoi99",
    "address":{
                "id":"xxx"
                "adress":"abc",
                "phone_number":"1234",
                "postal_code":"10000",
                "user_id":"ccc",
                "country":"xxx"
                "created_at":"xxxx",
                "updated_at":"xxx"
            },
    "status":"Created",
    "items":[
              {
                "id":item_dsahiaf90922,
                "product_id":"daojijfasje95482",
                "order_id":"atw93-5ifkjs",
                "quantity":3,
                "price":2.30,
                "created_at":"2024-02-09",
                "updated_at":"2024-02-09",
              },
            ],
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z",
}
```

### Update an order

Orders can only be updated in "Created" status. Other status does not allow updating.

Updates the order that you specify by setting the values of the passed parameters. Any parameters that you don’t provide remain unchanged.

**Returns**

Returns the order object if the update succeeds. This call raises an error if update parameters are invalid(for example, order with the provided id not found).

Request:

```
[PUT]api/v1/orders/:id

{
    "address_id":"new_addressfajkfasiosfjia999"
}
```

Response:

```
{
    "id":"order_safnasjiofgioes9990",
    "user_id":"user_fahfajoi99",
    "address":{
                  "id":"xxx"
                  "adress":"abc",
                  "phone_number":"1234",
                  "postal_code":"10000",
                  "user_id":"ccc",
                  "country":"xxx"
                  "created_at":"xxxx",
                  "updated_at":"xxx"
              },
    "status":"Created",
    "items":[
              {
                "id":item_dsahiaf90922,
                "product_id":"daojijfasje95482",
                "order_id":"atw93-5ifkjs",
                "quantity":3,
                "price":2.30,
                "created_at":"2024-02-09",
                "updated_at":"2024-02-09",
              },
            ]
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z",
}
```

### Retrieve an order

Get a single order by id.

Request:

`[GET]api/v1/orders/:id`

Response:

```
{
    "id":"order_safnasjiofgioes9990",
    "user_id":"user_fahfajoi99",
    "address":{
                  "id":"xxx"
                  "adress":"abc",
                  "phone_number":"1234",
                  "postal_code":"10000",
                  "user_id":"ccc",
                  "country":"xxx"
                  "created_at":"xxxx",
                  "updated_at":"xxx"
              },
    "status":"Created",
    "items":[
              {
                "id":item_dsahiaf90922,
                "product_id":"daojijfasje95482",
                "order_id":"atw93-5ifkjs",
                "quantity":3,
                "price":2.30,
                "created_at":"2024-02-09",
                "updated_at":"2024-02-09",
              },
            ]
    "created_at":"2017-11-04T18:48:46.250Z",
    "updated_at":"2017-11-04T18:48:46.250Z",
}
```

### List all orders

List all orders of a user.

### Parameters

| Key    | Is optional | Default | description                                    |
| ------ | ----------- | ------- | ---------------------------------------------- |
| userId | no          | N/A     | A valid user id must be provided               |

**Returns**

An `info` and `data` object.

Request:
`[GET]api/v1/orders?userId=user_jfaoifaoifsjs&status=created`

Response:

```
{
    "info":{
        "total_amount": 8,
        "pages": 1,
        "current_page":1,
        "has_more":false,
    },
    "data":[
            {
            "id":"order_safnasjiofgioes9990",
            "user_id":"user_fahfajoi99",
            "address":{
                          "id":"xxx"
                          "adress":"abc",
                          "phone_number":"1234",
                          "postal_code":"10000",
                          "user_id":"ccc",
                          "country":"xxx"
                          "created_at":"xxxx",
                          "updated_at":"xxx"
                      },
            "status":"Created",
            "items":[
                    {
                      "id":item_dsahiaf90922,
                      "product_id":"daojijfasje95482",
                      "order_id":"atw93-5ifkjs",
                      "quantity":3,
                      "price":2.30,
                      "created_at":"2024-02-09",
                      "updated_at":"2024-02-09",
                    },
            ]
            "created_at":"2017-11-04T18:48:46.250Z",
            "updated_at":"2017-11-04T18:48:46.250Z",
            },
            //
            ...
        ]
}
```

### List order items

List all the items of a specific order.

**Returns**

Returns a list of `order_items` object that belongs to this order.

Request:

`[GET]api/v1/orders/:id/items`

Response:

```
[
  {
    "id":item_dsahiaf90922,
    "product_id":"daojijfasje95482",
    "order_id":"atw93-5ifkjs",
    "quantity":3,
    "price":2.30,
    "created_at":"2024-02-09",
    "updated_at":"2024-02-09",
  },
  //
  ...
]
```

### Delete an order

Only completed orders can be deleted. Cancelling orders is achieved by updating the status to "Cancelled".

> Note: An access token is required in the header. Only the authenticated user herself can delete her order.

Request:

```
[DELETE]api/v1/orders/:id
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
{
    "id":"order_dahifakj33443",
    "deleted":true
}
```

## Addresses

```
ENDPOINTs
[POST]/v1/addresses
[PUT]/v1/addresses/:id
[GET]/v1/addresses
[DELETE]/v1/addresses/:id
```

### Address Schema

| Key          | Type   | Description                                             |
| ------------ | ------ | ------------------------------------------------------- |
| id           | string | Id of each address                                      |
| address      | string | Name of the address                                     |
| phone_number | string | Phone number of user, maximum 40 character              |
| user_id      | string | Id of the user that related to this address             |
| postal_code  | string | city postal code of this address , maximum 20 character |
| country      | string | city name, maximum 28 character                         |
| created_at   | string | Time and day is address is created                      |
| update_at    | string | Time and day is address is updated                      |

### Create an address

**Return**

Returns the newly created address object if successful, or raises an error if the parameters are invalid.

Request:

```
[POST] /api/v1/addresses

{
    "address":"abc",
    "phone_number":"1234",
    "postal_code":"10000",
    "user_id":"ccc",
    "country":"xxx"
}
```

Response:

```
{
    "id":"xxx"
    "adress":"abc",
    "phone_number":"1234",
    "postal_code":"10000",
    "user_id":"ccc",
    "country":"xxx"
    "created_at":"xxxx",
    "updated_at":"xxx"
}
```

### Update an address 

>Note:This action only avaiable for the owner and admin roles.

**Parameters**

- 'id' (string, uuid fomat): id of the address in addresses table

**Return**

Return boolean

Request:

```
[PUT] /api/v1/addresses/:id

{
    "address":"abc",
    "phone_number":"1234",
    "postal_code":"10000",
    "user_id":"ccc",
    "country":"xxx"
}
header
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
true
```

### Retrieve an address 

>Note: Authorization required to perform this action.

**Parameter**

- 'id' (string, uuid fomat): id of the address in addresses table

**Return**

Returns the address with the specified ID.

Request:

```
[GET] /api/v1/addresses/:id

header
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
{
    "id":"xxx"
    "adress":"abc",
    "phone_number":"1234",
    "postal_code":"10000",
    "user_id":"ccc",
    "country":"xxx"
    "created_at":"xxxx",
    "updated_at":"xxx"
}
```

### List addresses

Get a list of addresses of a certain user. 

> Note: Authrization is required to perform this action.

**Parameters**

- 'user_id' (string,uuid): user_id from users table, need to be existed in users table

**Return**

Returns a list of addresses. 

```
[GET] /api/v1/addresses/:userId
header
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
/api/v1/addresses/userId

{
"data":[
  {
    "id":"xxx"
    "adress":"abc",
    "phone_number":"1234",
    "postal_code":"10000",
    "user_id":"ccc",
    "country":"xxx"
    "created_at":"xxxx",
    "updated_at":"xxx"
  }
  // ...
]
}
```

### Delete an address 

>Note: This action only avaiable for user owner and admin.

>Note: Address cannot be deleted if it is used in an active order.

**Parameters**

You can specify the address id that you wish to update

**Return**

Return id of the address which is deleted and status of action

Request:

```
[DELETE] /api/v1/addresses/:id

header
{
  "Authorization": "Bearer {your access token}"
}
```

Response:

```
{
  "id":"catgajs8_52893",
  "deleted":true
}
```
