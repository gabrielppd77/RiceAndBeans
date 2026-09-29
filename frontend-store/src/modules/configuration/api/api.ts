import axios from "axios";
import { env } from "../env/env";

const api = axios.create({
  baseURL: env.baseUrl,
});

export default api;
