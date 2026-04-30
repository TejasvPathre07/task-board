import axios from "axios";

const API = axios.create({
  baseURL: "https://localhost:44394/api", // change if needed
});

export default API;