using MediatR;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using System.Net;
using System;
using PetPals_BackEnd_Group_9;
using Microsoft.EntityFrameworkCore;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class DeleteServiceHandler : IRequestHandler<DeleteServiceQuery, DeleteServiceResult>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<DeleteServiceHandler> _logger;

        public DeleteServiceHandler(PetPalsDbContext context, ILogger<DeleteServiceHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DeleteServiceResult> Handle(DeleteServiceQuery request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == request.ServiceId, cancellationToken);

            if (service == null)
            {
                _logger.LogWarning("Service {ServiceId} not found", request.ServiceId);
                return DeleteServiceResult.Failure(HttpStatusCode.NotFound, "Service not found", "The requested service ID does not exist in the database.");
            }

            service.IsRemoved = true;

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Service {ServiceId} has been removed", service.ServiceId);

            return DeleteServiceResult.Success();
        }
    }
}
