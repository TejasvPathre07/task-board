import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import API from "../services/api";
import Layout from "../components/Layout";

export default function ProjectBoard() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [tasks, setTasks] = useState([]);
  const [title, setTitle] = useState("");
  const [status, setStatus] = useState(0);
  const [loading, setLoading] = useState(false);

  const loadTasks = async () => {
    setLoading(true);
    try {
      const res = await API.get(`/projects/${id}/tasks`);
      setTasks(res.data.data || []);
    } catch {
      alert("Error loading tasks");
    }
    setLoading(false);
  };

  useEffect(() => {
    loadTasks();
    // eslint-disable-next-line
  }, [id]);

  const addTask = async () => {
    if (!title.trim()) {
      alert("Task title required");
      return;
    }

    await API.post(`/projects/${id}/tasks`, {
      title,
      description: "",
      priority: 1,
      status
    });

    setTitle("");
    loadTasks();
  };

  const deleteTask = async (taskId) => {
    await API.delete(`/tasks/${taskId}`);
    loadTasks();
  };

  return (
    <Layout>
      <h4 className="mb-3">Tasks</h4>

      {/* Add Task */}
      <div className="card p-3 mb-3">
        <div className="d-flex">
          <input
            className="form-control"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Task title"
          />

          <select
            className="form-select ms-2"
            onChange={(e) => setStatus(e.target.value)}
          >
            <option value="0">Todo</option>
            <option value="1">In Progress</option>
            <option value="2">Done</option>
          </select>

          <button className="btn btn-primary ms-2" onClick={addTask}>
            Add
          </button>
        </div>
      </div>

      {/* Loading */}
      {loading && <div className="alert alert-info">Loading...</div>}

      {/* Empty */}
      {!loading && tasks.length === 0 && (
        <p className="text-muted">No tasks found</p>
      )}

      {/* Task List */}
      {tasks.map((t) => (
        <div className="card p-2 mb-2" key={t.id}>
          <strong>{t.title}</strong>

          <div className="mt-2">
            {/* View Task (for comments) */}
            <button
              className="btn btn-info btn-sm me-2"
              onClick={() => navigate(`/task/${t.id}`)}
            >
              View
            </button>

            {/* Delete */}
            <button
              className="btn btn-danger btn-sm"
              onClick={() => deleteTask(t.id)}
            >
              Delete
            </button>
          </div>
        </div>
      ))}
    </Layout>
  );
}