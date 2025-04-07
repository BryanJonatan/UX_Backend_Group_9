﻿using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public record EditPetsCommand(int PetId, string Name, string Breed, int SpeciesId, decimal Age, string Gender, string Description, decimal Price) : IRequest<EditPetsResult>;
}