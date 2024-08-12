import { BaseResponse } from '@ShareModels'
export interface GetListQuizResponse extends BaseResponse<GetListQuizData> { }

export interface GetListQuizData {
  quizs: Quiz[]
}

export interface Quiz {
  name: string
  startTime: string
  endTime: string
  limitTimeInSecond: number
  id: string
  isDeleted: boolean
  status: number
}
