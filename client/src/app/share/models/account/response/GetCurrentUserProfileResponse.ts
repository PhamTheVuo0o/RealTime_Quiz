import { BaseResponse } from '@ShareModels'

export interface GetCurrentUserProfile {
  firstName: string
  lastName: string
  avatar: string
  userType: number
  isInternal: boolean
  isNotActive: boolean
  token: string
}

export interface GetCurrentUserProfileResponse extends BaseResponse<GetCurrentUserProfile> {}