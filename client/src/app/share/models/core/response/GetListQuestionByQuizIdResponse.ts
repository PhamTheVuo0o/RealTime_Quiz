import { BaseResponse } from '@ShareModels'
export interface GetListQuestionByQuizIdResponse extends BaseResponse<GetListQuestionByQuizIdData> { }

export interface GetListQuestionByQuizIdData {
    questions: Question[]
}

export interface Question {
    quizId: string
    content: string
    answer: any
    id: string
    isDeleted: boolean
    status: number
}