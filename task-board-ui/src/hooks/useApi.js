import { useState } from "react";

export default function useApi(apiFunc) {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const request = async (...args) => {
    setLoading(true);
    try {
      const response = await apiFunc(...args);
      setData(response.data);
      setError(null);
    } catch (err) {
      setError(err.response?.data || "Error");
    } finally {
      setLoading(false);
    }
  };

  return { data, loading, error, request };
}