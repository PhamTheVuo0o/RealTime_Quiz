using AppCore.Core.API.Application.Commands;
using AppCore.Core.API.Application.Responses;
using AppCore.Infrastructure.RestClient;
using AutoMapper;
using MediatR;
using AppCore.Core.Infrastructure;
using AppCore.Core.Infrastructure.Queries.Requests;
using AppCore.Infrastructure.Cache.Contracts;
using AppCore.Infrastructure.Extensions;
using AppCore.Infrastructure.MQTTClient.Contracts;
using AppCore.Core.Domain.Models;
using Elasticsearch.Net.Specification.IndexLifecycleManagementApi;

namespace AppCore.Core.API.Application.Handlers
{
    public class SubmitAnswerHandler : BaseClient<SubmitAnswerResponse>,
        IRequestHandler<SubmitAnswerCommand, SubmitAnswerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;
        private readonly IMQTTClientFeature _mQTTClientFeature;
        public SubmitAnswerHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheManager cacheManager,
            IMQTTClientFeature mQTTClientFeature,
            IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cacheManager = cacheManager;
            _mQTTClientFeature = mQTTClientFeature;
        }
        public async Task<SubmitAnswerResponse> Handle(SubmitAnswerCommand request, CancellationToken cancellationToken)
        {
            //Remove Duplicate
            //string keynameWithUserId = $"key_{request.UserId}_{request.QuizId}_{request.QuestionId}";
            //string cachedDataWithUserId = _cacheManager.Get<string>(keynameWithUserId);
            //if (cachedDataWithUserId.IsContain(cachedDataWithUserId)) { return new SubmitAnswerResponse() { IsSuccess = true}; }
            //_cacheManager.Set(keynameWithUserId, keynameWithUserId);

            var dbRequest = _mapper.Map<SubmitAnswerDbRequest>(request);
            // Get data from cache
            string keyname = $"key_{request.QuizId}_{request.QuestionId}_{request.Answer}";
            string cachedData = _cacheManager.Get<string>(keyname);

            dbRequest.IsRightWithCache = cachedData.IsContain(cachedData);

            var result = await _unitOfWork.userAnswerRepository.SubmitAnswerDb(dbRequest);
            var response = _mapper.Map<SubmitAnswerResponse>(result);

            

            if ((result != null) && (result.Data.IsRight))
            {
                // Publish Rank Lish
                var isConnected = await _mQTTClientFeature.Connect();
                if (isConnected)
                {
                    var data = await _unitOfWork.userAnswerRepository.GetListUserAnswerByQuizIdDb(new GetListUserAnswerByQuizIdDbRequest() { QuizId = request.QuizId});
                    if (data != null)
                    {
                        response.IsSuccess = await _mQTTClientFeature.Publish<List<UserAnswerModel>>($"{request.QuizId.ToString()}_Rank", data.Data.Ranks);
                        
                    }
                }
            }

            // Store to cache if not have
            if ((result != null) && (result.Data.IsRight) && (!dbRequest.IsRightWithCache))
            {
                _cacheManager.Set(keyname, keyname);
            }

            return response;
        }
    }
}
