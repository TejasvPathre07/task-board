import { useEffect } from "react";
import { useParams } from "react-router-dom";
import API from "../services/api";
import useApi from "../hooks/useApi";

export default function ProjectBoard() {
  const { id } = useParams();
  const { data, loading, error, request } = useApi(API.get);

  useEffect(() => {
    request(`/projects/${id}`);
  }, [id]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error</p>;

  return (
    <div>
      <h2>Project Tasks</h2>

      {data?.tasks?.map((task) => (
        <div key={task.id}>
          <h4>{task.title}</h4>
          <p>{task.status}</p>
        </div>
      ))}
    </div>
  );
}