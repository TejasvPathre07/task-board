import { useEffect } from "react";
import { useParams } from "react-router-dom";
import API from "../services/api";
import useApi from "../hooks/useApi";

export default function TaskDetail() {
  const { id } = useParams();

  const taskApi = useApi(API.get);
  const commentApi = useApi(API.get);

  useEffect(() => {
    taskApi.request(`/tasks/${id}`);
    commentApi.request(`/tasks/${id}/comments`);
  }, [id]);

  if (taskApi.loading) return <p>Loading...</p>;

  return (
    <div>
      <h2>{taskApi.data?.title}</h2>
      <p>{taskApi.data?.description}</p>

      <h3>Comments</h3>
      {commentApi.data?.map((c) => (
        <div key={c.id}>
          <b>{c.author}</b>: {c.body}
        </div>
      ))}
    </div>
  );
}