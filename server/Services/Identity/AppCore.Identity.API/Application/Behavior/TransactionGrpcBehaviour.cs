using Grpc.Core;
using Grpc.Core.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AppCore.Identity.Infrastructure;
using AppCore.Infrastructure.Extensions;
using System.Net;

namespace AppCore.Identity.API.Application.Behavior
{
    public class TransactionGrpcBehaviour : Interceptor
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionGrpcBehaviour(IUnitOfWork unitOfWork,
            ILogger<TransactionGrpcBehaviour> logger, 
            IHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();
            try
            {
                if (_unitOfWork.HasActiveTransaction())
                {
                    return await continuation(request, context);
                }

                var strategy = _unitOfWork.GetStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await _unitOfWork.BeginTransactionAsync())
                    {
                        response = await continuation(request, context);

                        await _unitOfWork.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }
                });
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}
