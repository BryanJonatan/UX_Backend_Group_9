﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Handlers;
using PetPals_BackEnd_Group_9.Helpers;
using PetPals_BackEnd_Group_9.Models;
using System.Net;

public class EditServiceHandler : IRequestHandler<EditServiceCommand, EditServiceResult>
{
    private readonly PetPalsDbContext _context;
    private readonly ILogger<EditServiceHandler> _logger;

    public EditServiceHandler(PetPalsDbContext context, ILogger<EditServiceHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EditServiceResult> Handle(EditServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _context.Services.Include(s => s.Provider).Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.ServiceId == request.ServiceId, cancellationToken);

        if (service == null)
        {
            throw new CustomException("The requested service does not exist.", HttpStatusCode.NotFound);
        }

        if (service.Provider == null)
        {
            throw new CustomException("Provider information is missing.", HttpStatusCode.InternalServerError);
        }

        if (service.Category == null)
        {
            throw new CustomException("Category information is missing.", HttpStatusCode.InternalServerError);
        }

        service.Name = request.Name;
        service.CategoryId = request.CategoryId;
        service.Description = request.Description;
        service.Price = request.Price;
        service.Address = request.Address;
        service.City = request.City;
        service.UpdatedAt = DateTimeOffset.UtcNow;
        service.UpdatedBy = service.Provider.Name; // Automatically set UpdatedBy to Provider Name
        service.CreatedBy = service.Provider.Name; // Automatically set CreatedBy to Provider Name
        service.Slug = await SlugHelper.GenerateUniqueSlugAsync(request.Name, _context.Services);

        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Service {ServiceId} updated successfully by Provider {UpdatedBy}", service.ServiceId, service.UpdatedBy);

        return EditServiceResult.Success();
    }
}