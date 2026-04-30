import { useEffect, useState } from "react";
import API from "../services/api";
import Layout from "../components/Layout";
import { useNavigate } from "react-router-dom";

export default function Dashboard() {
  const [projects, setProjects] = useState([]);
  const [name, setName] = useState("");
  const navigate = useNavigate();

  const loadProjects = async () => {
    const res = await API.get("/projects");
    setProjects(res.data);
  };

  useEffect(() => {
    loadProjects();
  }, []);

  const addProject = async () => {
    if (!name) return;
    await API.post("/projects", { name });
    setName("");
    loadProjects();
  };

 const deleteProject = async (id) => {
  if (!window.confirm("Delete this project?")) return;

  await API.delete(`/projects/${id}`);
  loadProjects();
};

  return (
    <Layout>
      <div className="card p-3 mb-4">
        <h5>Add Project</h5>
        <div className="d-flex">
          <input
            className="form-control"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Enter project name"
          />
          <button className="btn btn-primary ms-2" onClick={addProject}>
            Add
          </button>
        </div>
      </div>

      <div className="row">
        {projects.map((p) => (
          <div className="col-md-4" key={p.id}>
            <div className="card p-3 mb-3">
              <h5>{p.name}</h5>

              <button
                className="btn btn-success btn-sm mb-2"
                onClick={() => navigate(`/project/${p.id}`)}
              >
                Open
              </button>

              <button
                className="btn btn-danger btn-sm"
                onClick={() => deleteProject(p.id)}
              >
                Delete
              </button>
            </div>
          </div>
        ))}
      </div>
    </Layout>
  );
}