using AppCore.Core.API.Application.Commands;
using AppCore.Core.API.Application.Responses;
using AppCore.Infrastructure.RestClient;
using AutoMapper;
using MediatR;
using AppCore.Core.Infrastructure;
using AppCore.Core.Infrastructure.Queries.Requests;

namespace AppCore.Core.API.Application.Handlers
{
    public class GetListQuestionByQuizIdHandler : BaseClient<GetListQuestionByQuizIdResponse>,
        IRequestHandler<GetListQuestionByQuizIdCommand, GetListQuestionByQuizIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetListQuestionByQuizIdHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<GetListQuestionByQuizIdResponse> Handle(GetListQuestionByQuizIdCommand request, CancellationToken cancellationToken)
        {
            var dbRequest = _mapper.Map<GetListQuestionByQuizIdDbRequest>(request);

            var result = await _unitOfWork.questionRepository.GetListQuestionByQuizIdDb(dbRequest);

            var response = _mapper.Map<GetListQuestionByQuizIdResponse>(result);

            return response;
        }
    }
}
