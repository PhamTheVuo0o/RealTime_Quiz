using AppCore.Core.API.Application.Commands;
using AppCore.Core.API.Application.Responses;
using AppCore.Infrastructure.RestClient;
using AutoMapper;
using MediatR;
using AppCore.Core.Infrastructure;

namespace AppCore.Core.API.Application.Handlers
{
    public class GetListQuizHandler : BaseClient<GetListQuizResponse>,
        IRequestHandler<GetListQuizCommand, GetListQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetListQuizHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<GetListQuizResponse> Handle(GetListQuizCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.quizRepository.GetListQuizDb();

            var response = _mapper.Map<GetListQuizResponse>(result);

            return response;
        }
    }
}
