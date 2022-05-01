//.NET
global using System;
global using Microsoft.Extensions.Options;

//Nuget packages
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using FluentValidation;
global using FluentValidation.AspNetCore;

global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;

global using Microsoft.OpenApi.Models;

global using Eindopdracht.Models;
global using Eindopdracht.Repositories;
global using Eindopdracht.Services;
global using Eindopdracht.Validators;
global using Eindopdracht.Configuration;
global using Eindopdracht.Context;
global using Eindopdracht.GraphQL.Queries;
global using Eindopdracht.GraphQL.Mutations;