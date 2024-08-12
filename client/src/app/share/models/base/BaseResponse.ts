export interface BaseResponse<T> {
    isSuccess: boolean
    errorMessage: string
    errorType: number
    details: string
    data:T
  }
  