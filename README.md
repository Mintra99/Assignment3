# Film API

This project/assignment is a part of the Fullstack course by Noroff. The given challenge was divided into two parts:

- An application that should be constructed in ASP.NET Core and comprise of a database made in SQL Server through EF Core with a RESTful API to allow users to manipulate the data.
- Create a Web API using the ASP.NET Core application that was made from the previous part.

## Usage

```bash
git clone git@github.com:Mintra99/Assignment3.git
cd Assignment3
```

in [appsettings.json](/appsettings.json) DefaultConnection replace the `"Server=YourServerName; ..."` with your sql server name

```dotnet
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run
```
should be running on `http://localhost:5298` (might be different) \
`http://localhost:5298/swagger` for swagger documentation

## Project structure

The project is structured in the following way:

### Controllers
- CharacerController.cs
- FranchiseController.cs
- MovieController.cs

### Data
- **Dtos**
    - **Characters**
        - CharacterDTO.cs
        - CharacterPostDTO.cs
        - CharacterPutDTO.cs
    - **Franchise**
        - FranchiseDTO.cs
        - FranchisePostDto.cs
        - FranchisePutDto.cs
    - **Movies**
        - MovieDTO.cs
        - MoviePostDto.cs
        - MoviePutDto.cs
- **Models**
    - Character.cs
    - Franchise.cs
    - Movie.cs
- DbContext.cs


### Helpers
- NotFoundResponse.cs
    - **Exceptions** 
        - EntityNotFoundException.cs

### Services
- **Characters**
    - CharacterService.cs
    - ICharacterService.cs
- **Franchises**
    - FranchiseService.cs
    - IFranchiseService.cs
- **Mappers**
    - CharacterProfile.cs
    - FranchiseProfile.cs
    - MovieProfile.cs
- **Movies**
    - IMovieService.cs
    - MovieService.cs
- ICRUDService.cs

## Contributors

This project was made as a collaboration between the following people:

- [Minh Christian Tran](https://github.com/Mintra99)
- [Anders Wiik](https://github.com/andyret26)
