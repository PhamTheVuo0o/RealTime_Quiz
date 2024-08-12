import { BaseResponse } from '@ShareModels'

export interface LoginResponseData {
  token: string
  userName: string
  isNotActive: boolean
}

export interface LoginResponse extends BaseResponse<LoginResponseData> {
  }
  