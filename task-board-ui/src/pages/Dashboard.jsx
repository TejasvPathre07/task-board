import { useEffect } from "react";
import API from "../services/api";
import useApi from "../hooks/useApi";

export default function Dashboard() {
  const { data, loading, error, request } = useApi(API.get);

  useEffect(() => {
    request("/dashboard");
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading dashboard</p>;

  return (
    <div>
      <h2>Dashboard</h2>
      <p>Total Projects: {data?.totalProjects}</p>
      <p>Total Tasks: {data?.totalTasks}</p>
      <p>Overdue: {data?.overdueCount}</p>
      <p>Due Soon: {data?.dueSoon}</p>
    </div>
  );
}