﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetSinglePetHandler : IRequestHandler<GetSinglePetQuery, GetSinglePetResponse>
    {
        private readonly PetPalsDbContext _context;

        public GetSinglePetHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<GetSinglePetResponse> Handle(GetSinglePetQuery request, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets
                .Include(p => p.Species)
                .Include(p => p.Owner)
                .Where(p => p.Slug == request.Slug)
                .Select(p => new GetSinglePetResponse
                {
                    PetId = p.PetId,
                    Name = p.Name,
                    Breed = p.Breed,
                    Age = p.Age,
                    Gender = p.Gender,
                    Description = p.Description,
                    Status = p.Status,
                    Price = p.Price,
                    Owner = p.Owner,
                    Species = p.Species,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (pet == null)
            {
                Log.Information("Pet not found: {Slug}", request.Slug);
                throw new NotFoundException($"Pet with slug '{request.Slug}' not found.");
            }

            return pet;
        }

    }
}
