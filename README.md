# GraphQLDemo

## Running App

- clone repo
- run `cd GraphQLDemo/GraphQL`
- run `dotnet run` (you must have dotnet 6.0+ SDK installed)

## GraphQL Queries

The demo app builds a very small database with statements and organisations. Below are some example GraphQL queries you can use:

```graphql
{
  organisations {
    nodes {
      companyId,
      name,
      statements {
        id,
        year
      }
    }
  }
}
```

## Authorisation

In the code, an organisation's address is restricted to user's authorised as admin.

```c#
[Authorize(Roles = new string[] { "Admin" })]
public string Address { get; set; } = default!;
```

To be allowed to run the following query:

```graphql
{
  organisations {
    nodes {
      companyId,
      name,
      address,
      statements {
        id,
        year
      }
    }
  }
}
```

You need to create a token and add it as an authorisation header (using bearer).
To do this, go to `/api/jwt/admin` and this will generate a [JSON Web Token](https://jwt.io).
You can use token in your request.

## Paging

You can page your requests using the following queries, see linked documentation (at bottom) for more info.

This gets the first organisation. You will note a token/cursor that is returned.

```graphql
{
  organisations(first:1) {
    nodes {
      companyId,
      name,
      address,
      statements {
        id,
        year
      }
    }
    pageInfo {
      hasNextPage
      endCursor
    }
  }
}
```

You can use this token in subsequent queries:

```graphql
{
  organisations(after: "MA==", first:1) {
    nodes {
      companyId,
      name,
      address,
      statements {
        id,
        year
      }
    }
    pageInfo {
      hasNextPage
      endCursor
    }
  }
}
```

## Where 

The following query filters an organisation on its name:

```graphql
{
  organisations (where: {name: { contains: "Big"}}) {
    nodes {
      id,
      companyId,
      name,
      statements {
        id,
        year
      }
    }
  }
}
```

## Links

- [Getting started with GraphQL in .NET - YouTube] (https://www.youtube.com/watch?v=qrh97hToWpM)
- [Hot Chocolate Graph QL Framework](https://chillicream.com/docs/hotchocolate)
