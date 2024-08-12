export interface SubmitAnswerRequest {
    questionId: string
    quizId: string
    userFullName: string
    answer: string
    currentCore: number
    currentTimeSecond: number
  }
  