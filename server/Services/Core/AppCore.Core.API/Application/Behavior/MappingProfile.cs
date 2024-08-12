using AutoMapper;
using AppCore.Core.API.Application.Commands;
using AppCore.Core.Infrastructure.Queries.Requests;
using AppCore.Core.API.Application.Responses;
using AppCore.Core.Infrastructure.Queries.Responses;

namespace AppCore.Core.API.Application.Behavior
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<GetListQuestionByQuizIdCommand, GetListQuestionByQuizIdDbRequest>();
            CreateMap<GetListQuestionByQuizIdDbResponse, GetListQuestionByQuizIdResponse>();
            CreateMap<GetListQuestionByQuizIdDb, GetListQuestionByQuizId>();

            CreateMap<GetListQuizDbResponse, GetListQuizResponse>();
            CreateMap<GetListQuizDb, GetListQuiz>();

            CreateMap<SubmitAnswerCommand, SubmitAnswerDbRequest>();
            CreateMap<SubmitAnswerDbResponse, SubmitAnswerResponse>();
            CreateMap<SubmitAnswerDb, SubmitAnswer>();
        }
    }
}
