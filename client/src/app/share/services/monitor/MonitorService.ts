import { PingResponse } from "@ShareModels";
import {BaseServices} from "@ShareServices";
import { API_URL } from '@ShareConstants';

export const Ping = async () => {
  return BaseServices.call<PingResponse>({
    url: `${API_URL.Identity}/Ping`,
    method: "GET",
  });
};

export const MonitorService = {
  Ping,
};

export default MonitorService;