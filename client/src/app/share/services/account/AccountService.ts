import { 
  LoginRequest, 
  LoginResponse,
  GetCurrentUserProfileResponse,
} from "@ShareModels";
import {BaseServices} from "@ShareServices";
import { API_URL } from '@ShareConstants';

const basePath:string = '/Account/';

export const Login = async (body:LoginRequest) => {
  return BaseServices.post<LoginResponse>(
    `${API_URL.Identity}${basePath}Login`, body
  );
};

export const GetCurrentUserProfile = async () => {
  return BaseServices.get<GetCurrentUserProfileResponse>(
    `${API_URL.Identity}${basePath}GetCurrentUserProfile`
  );
};

export const AccountService = {
  Login,
  GetCurrentUserProfile,
};

export default AccountService;