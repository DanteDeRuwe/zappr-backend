# Zappr - Backend Repo
> readme updated: 29/03/2020

## About the project
Zappr is a web application that enhances your local TV community.
This is the repository of all the backend code, using .NET Core.

This project is for educational purposes only!

## About me
Dante De Ruwe - Student Applied Information Technology @ Ghent University College

## GraphQL API
This backend serves as a GraphQL API. It has one endpoint `https://localhost:5001/graphql`.

On startup of the .NET Application you are taken to https://localhost:5001/, where the GraphQL playground is located.

![](https://i.imgur.com/qbf7ihU.png)

On the right hand side, you can inspect the Docs and the GraphQL schema:

```graphql
type Mutation {
  users: Users
}

type Query {
  series: Series
  users: Users
}


type Series {
  get(id: Int = null): SeriesType
  schedule(
    country: String = null
    start: Int = null
    numberofdays: Int = null
  ): [SeriesType]
  search(name: String = null): [SeriesType]
  singlesearch(name: String = null): SeriesType
  today(country: String = null): [SeriesType]
}

type SeriesType {
  airTime: String
  description: String
  ended: Boolean
  genres: [String]
  id: Int!
  imageUrl: String
  name: String!
  network: String
  numberOfSeasons: Int
  officialSite: String
  premiered: String
}

input UserInput {
  fullName: String!
  email: String!
}

type Users {
  createUser(user: UserInput!): UserType
}

type UserType {
  email: String!
  favoriteSeries: [SeriesType]
  fullName: String!
  id: Int!
}
```

From this you can see the current query and mutation hierarchy. At the moment we can query `Series` and `User`s. We can also add users by using the `Users` mutation.

The data for the series comes from the [tvMaze API](https://api.tvmaze.com).

I tried to add descriptions to nearly every field, so go look around in the docs on the GraphQL playground!


### Query examples
Some code examples to play around with in the playground below:


#### Searching a series
```graphql
query {
  series{
    search(name:"De Mol"){
      airTime,
      description,
      ended,
      genres,
      id,
      imageUrl,
      name,
      network,
      numberOfSeasons,
      officialSite,
      premiered
    }
  }
}
```

You can also subsitute `search` with `singlesearch` to get only the best match.

#### Getting the TV schedule for the week in Belgium
```graphql
query{
  series{
    schedule(country:"BE", start: 0, numberofdays:7){
      name
    }
  }
}
```




#### Adding a user
```graphql
mutation($user:UserInput!){
  users{
    createUser(user:$user){
      id,
      fullName,
      email
    }
  }
}
```
the query variables:
```jsonld
{
    "user": {
      "fullName": "Dante De Ruwe",
      "email": "dantederuwe@gmail.com" 
    }
}
```

#### Getting a user by id
```graphql
query{
  users{
    get(id:1){
      id, fullName
    }
  }
}
```


## Class Diagram
> THIS CLASS DIAGRAM IS INCOMPLETE + also kind of wrong. I'm intending to fix it, I ran into some issues (and deadlines).
> See [my Stackoverflow question](https://stackoverflow.com/questions/60832540/ef-core-multiple-many-to-many-relationships-between-the-same-entities) for details about my issues


![](https://i.imgur.com/cDtfV6u.png)


## Realized requirements

The checklist provided by the lecturers spans the entire scope of the project. I'm not ticking much boxes because not everything is finished 100%. If I do tick the box that doesn't mean work is entirely finished either. 
### Domain
- [x] Min. 2 associated classes
- [ ] State and behavior
- [ ] Class diagram is made

### DAL
- [x] AppDbContext
- [x] Configurations
- [ ] Seeded Data

### ~~Controller~~ --> GraphQL
- [ ] All CRUD operations
- [x] endpoint via best practices (only 1 in GraphQL)
- [x] Only needed data (GraphQL out-of-the-box)
