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
  const [filterStatus, setFilterStatus] = useState("");

  const loadTasks = async () => {
    try {
      const res = await API.get(`/projects/${id}/tasks`);
      setTasks(res.data.data || []);
    } catch {
      alert("Error loading tasks");
    }
  };

  useEffect(() => {
    loadTasks();
  }, [id]);

  const addTask = async () => {
    if (!title) {
      alert("Enter task title");
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
    if (!window.confirm("Delete task?")) return;

    await API.delete(`/tasks/${taskId}`);
    loadTasks();
  };

  const filteredTasks = tasks.filter((t) => {
    if (filterStatus === "") return true;
    return t.status == filterStatus;
  });

  return (
    <Layout>
      <h4>Tasks</h4>

      {/* Add Task */}
      <div className="card p-3 mb-3">
        <input
          className="form-control mb-2"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Task title"
        />

        <select
          className="form-select mb-2"
          onChange={(e) => setStatus(e.target.value)}
        >
          <option value="0">Todo</option>
          <option value="1">In Progress</option>
          <option value="2">Done</option>
        </select>

        <button className="btn btn-primary" onClick={addTask}>
          Add Task
        </button>
      </div>

      {/* Filter */}
      <select
        className="form-select mb-3"
        onChange={(e) => setFilterStatus(e.target.value)}
      >
        <option value="">All</option>
        <option value="0">Todo</option>
        <option value="1">In Progress</option>
        <option value="2">Done</option>
      </select>

      {filteredTasks.length === 0 && <p>No tasks</p>}

      {filteredTasks.map((t) => (
        <div className="card p-2 mb-2" key={t.id}>
          <strong>{t.title}</strong>

          <div>
            <small>Status: {t.status}</small>
          </div>

          <div className="mt-2">
            <button
              className="btn btn-info btn-sm me-2"
              onClick={() => navigate(`/task/${t.id}`)}
            >
              View
            </button>

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
