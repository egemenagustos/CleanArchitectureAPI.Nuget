using MediatR;
using System.Transactions;

namespace Core.Application.Pipelines.Transaction
{
    public class TranscationScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ITransactionalRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //TransactionScopeAsyncFlowOption.Enabled : Asenkron olarak çalıştığımız için enabled ediyoruz.
            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);

            TResponse response;

            try
            {
                response = await next();
                transactionScope.Complete();
            }
            catch (Exception excepiton)
            {
                transactionScope.Dispose();
                throw;
            }

            return response;
        }
    }
}
