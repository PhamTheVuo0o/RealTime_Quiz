import { BaseResponse } from '@ShareModels'
export interface SubmitAnswerResponse extends BaseResponse<SubmitAnswerData> { }

export interface SubmitAnswerData {
    core: number
    isRight: boolean
}
