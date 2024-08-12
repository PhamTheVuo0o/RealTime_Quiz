import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";
import {
  LocalStorageKeyEnum,
  RouterAppEnum,
} from '@ShareEnums';
import { UserStorage } from '@ShareStores';

axios.interceptors.request.use(function (config) {
  const token = UserStorage.getCurrentUserToken() ?? '';
  config.headers['Access-Control-Allow-Origin'] = '*';
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

axios.interceptors.response.use(
  async (response) => {
    return response;
  },
  (error: AxiosError) => {
    const { status, config } = error.response!;
    switch (status) {
      case 400:
        // not found
        if (config.method === "get") {
          console.log("Not Found");
        }
        break;
      case 401:
        {
          const rlt = UserStorage.logoutCurrentUser();
          if (rlt) {
            const navigate = useNavigate();
            navigate(RouterAppEnum.Login);
          }
          break;
        }
      // invalid token
      case 404:
        // not found
        break;
      case 500:
        // server error
        break;
    }
    return Promise.reject(error);
  }
);

async function call<T>(config: AxiosRequestConfig) {
  const res = await axios({
    ...config,
    params: { ...config?.params },
  }).then((res) => res?.data);

  return res as T;
}

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

async function get<T>(url: string) {
  const res = await axios.get<T>(url).then(responseBody)

  return res as T;
}

async function post<T>(url: string, body: object) {
  const res = await axios.post<T>(url, body).then(responseBody)

  return res as T;
}

async function put<T>(url: string, body: object) {
  const res = await axios.put<T>(url, body).then(responseBody)

  return res as T;
}

async function del<T>(url: string, body: object) {
  const res = await axios.delete<T>(url, body).then(responseBody)

  return res as T;
}

export const BaseServices = {
  call,
  get,
  post,
  put,
  del
};

export default BaseServices;
