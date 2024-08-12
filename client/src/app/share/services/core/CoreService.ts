import { 
    GetListQuestionByQuizIdRequest, 
    SubmitAnswerRequest,
    GetListQuestionByQuizIdResponse,
    GetListQuizResponse,
    SubmitAnswerResponse
  } from "@ShareModels";
  import {BaseServices} from "@ShareServices";
  import { API_URL } from '@ShareConstants';
  
  const basePath:string = '/Data/';
  
  export const GetListQuestionByQuizId = async (body:GetListQuestionByQuizIdRequest) => {
    return BaseServices.post<GetListQuestionByQuizIdResponse>(
      `${API_URL.Core}${basePath}GetListQuestionByQuizId`, body
    );
  };

  export const SubmitAnswer = async (body:SubmitAnswerRequest) => {
    return BaseServices.post<SubmitAnswerResponse>(
      `${API_URL.Core}${basePath}SubmitAnswer`, body
    );
  };
  
  export const GetListQuiz = async () => {
    return BaseServices.get<GetListQuizResponse>(
      `${API_URL.Core}${basePath}GetListQuiz`
    );
  };
  
  export const CoreService = {
    GetListQuestionByQuizId,
    SubmitAnswer,
    GetListQuiz,
  };
  
  export default CoreService;