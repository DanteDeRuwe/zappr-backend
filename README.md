# Zappr - Backend Repo

> readme update: 22/05/2020

## Over het project

Zappr is een app om je favoriete TV series nooit uit het oog te verliezen!

Dit is de repository voor de abckend-code, gebruik makende van .NET Core 3.1 en GraphQL. De frontend-code kan gevonden worden in [deze repo](https://github.com/Web-IV/1920-b1-fe-dantederuwe-hogent).

Dit project is enkel voor educatieve doeleinden!

## Over mij

Dante De Ruwe - Student Toegepaste Informatica aan de Hogeschool Gent.

Vragen? [Stuur me een email](mailto:dante.deruwe@student.hogent.be) of [submit een issue](https://github.com/Web-IV/1920-b1-be-dantederuwe-hogent/issues) op deze repository.

## De GraphQL API
Deze backend doet dienst als een GraphQL API. Er is 1 endpoint: `https://localhost:5001/graphql`.

> Meer info over GraphQL [hier](https://graphql.org/)

Wanneer je de applicatie opstart en navigeert naar https://localhost:5001/, kunt u de GraphQL-playground zien. Dit is zoals Postman: je kunt er requests mee sturen naar de graphql API.

[![](https://i.imgur.com/qbf7ihU.png)](https://i.imgur.com/qbf7ihU.png)

Aan de rechterkant kan je ook de docs en het schema van de GraphQL API nalezen. 


### GraphQL Schema


```graphql
type CommentType {
  author: UserType
  id: Int!
  text: String!
}

type EpisodeType {
  airDate: String
  airTime: String
  id: Int!
  image: String
  name: String
  number: Int
  runtime: Int
  season: Int
  series: SeriesType
  summary: String
}

type Mutation {
  seriesMutation: SeriesMutation
  userMutation: UserMutation
}

type Query {
  seriesQuery: SeriesQuery
  userQuery: UserQuery
}


type SeriesMutation {
  addComment(
    seriesId: Int!
    commentText: String!
    authorUserId: Int!
  ): SeriesType
  addRating(
    seriesId: Int!
    ratingPercentage: Int!
    authorUserId: Int!
  ): SeriesType
}

type SeriesQuery {
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
  averageRating: Decimal
  comments: [CommentType]
  description: String
  ended: Boolean
  genres: [String]
  id: Int!
  imageUrl: String
  name: String
  network: String
  numberOfSeasons: Int
  officialSite: String
  premiered: String
}

input UserInput {
  fullName: String!
  email: String!
  password: String!
}

type UserMutation {
  addFavoriteSeries(seriesId: Int!): UserType
  addSeriesToWatchList(seriesId: Int!): UserType
  addWatchedEpisode(episodeId: Int!): UserType
  login(email: String!, password: String!): String
  register(user: UserInput!): UserType
  removeFavoriteSeries(seriesId: Int!): UserType
  removeSeriesFromWatchList(seriesId: Int!): UserType
  removeWatchedEpisode(episodeId: Int!): UserType
}

type UserQuery {
  all: [UserType]
  get(id: Int = null): UserType
  me: UserType
}

type UserType {
  email: String!
  favoriteSeries: [SeriesType]
  fullName: String!
  id: Int!
  ratedSeries: [SeriesType]
  watchedEpisodes: [EpisodeType]
  watchListedSeries: [SeriesType]
}
```

### Docs
Deze zijn het best te bekijken door de applicatie te runnen en de docs te bekijken op de playground zoals eerder vermeld. Ik heb geprobeerd bij de meeste velden een beschrijving toe te voegen, dus neem zeker eens een kijkje!


### Voorbeelden
Hieronder enkele voorbeelden van queries en mutations die je kan uitvoeren.

#### Een serie opzoeken
```graphql
query {
  seriesQuery{
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

Je kan `search` ook vervangen door `singlesearch`, dan gaat de API op zoek naar de beste match. De search is ook fuzzy, dus normaalgezien zou de API ook naast enkele typfoutjes moeten kunnen kijken.
 
#### De series opzoeken die deze week worden uitgezonden
```graphql
query{
  seriesQuery{
    schedule(country:"US", start: 0, numberofdays:7){
      name
    }
  }
}
```

Parameters:
- `country` de ISO landcode
- `start`: uitgedrukt in het aantal dagen vanaf vandaag 
- `numberofdays`: vanzelfsprekend




#### Zich registreren
```graphql
mutation($user: UserInput!) {
  userMutation {
    register(user: $user) {
      fullName
      email
    }
  }
}
```
Query variabelen (onderaan in de playground in te vullen, zie [hier](https://i.imgur.com/kxaDn2e.png))
```json
{
   "user": {
      "fullName":"Richard Hendricks",
      "email":"richard@piedpiper.com",
      "password":"pipernet"
   }
}
```

#### Inloggen
```graphql
mutation {
  userMutation {
    login(email:"richard@piedpiper.com", password:"pipernet")
  }
}
```

Dit geeft een string terug, dit is de JWT-token. 
> **Voor wat volgt moet je deze token toevoegen aan de HTTP-headers van de playground (tabblaadje naast "QUERY VARIABLES")!!!**
>```jsonld
>{
>  "Authorization": "Bearer *token hier*"
> }
> ```



#### De ingelogde gebruiker opvragen (token nodig)
```graphql
query {
  userQuery {
    me {
      fullName
      email
      favoriteSeries {
        name
      }
      watchedEpisodes {
        name
        summary
      }
    }
  }
}
```

Er waren nog veel meer velden te querien op deze gebruiker, dit is slechts een voorbeeld; zie schema.


#### Favoriete serie toevoegen (token nodig)
```graphql
mutation {
  userMutation {
    addFavoriteSeries(seriesId: 10371) {
      fullName
      favoriteSeries {
        name
      }
    }
  }
}
```

Deze mutation geeft een UserType terug. Hierop kunnen we dan meteen weer vragen wat de favoriete series zijn, om zo te zien of de mutation correct werd uitgevoerd.

#### Overige
Zie docs en schema. Comments, ratings, watched episodes, watchlist... er is nog veel mogelijk.

## Klassediagram

> 💡 **Tip**: klik op de afbeelding om ze in volledige grootte te zien.

[![](https://i.imgur.com/de6U9N0.png)](https://i.imgur.com/de6U9N0.png)


## Lijst van technologieën
- .NET Core 3.1
- GraphQL
- GraphQL-dotnet incl authorization
- Identity Framework
- Entity Framework Core
- Microsoft SQL Server
- BCrypt
- Newtonsoft JSON

## Met dank aan
> Voor zowel front- als backend
- De lectoren WEB4 voor de slides, codevoorbeelden, feedback en support
- [TVMaze API](https://www.tvmaze.com/api) voor de data
- [Stackoverflow](https://stackoverflow.com/), met in het bijzonder aan [dglozano](https://stackoverflow.com/users/10648865/dglozano), die antwoordde op [mijn vraag](https://stackoverflow.com/questions/60832540/ef-core-multiple-many-to-many-relationships-between-the-same-entities)
- De graphQL-dotnet [Github](https://github.com/graphql-dotnet/graphql-dotnet) en [docs](https://graphql-dotnet.github.io/), met in het bijzonder [deze issue-thread](https://github.com/graphql-dotnet/authorization/issues/63#issuecomment-553877731)
- De [Apollo-angular docs](https://www.apollographql.com/docs/angular/)
- De [ngx-drag-scroll npm library](https://ngx-drag-scroll.fanjin.io/)
- Talloze YouTubers
- Talloze andere blogs, fora, websites...




*PS: De naam Zappr komt van 'zapper' aka afstandsbediening aka zoals hier in WVL: "baksje"*
