  import { useEffect, useState } from "react";
  import API from "../services/api";
  import Layout from "../components/Layout";
  import { useNavigate } from "react-router-dom";

  export default function Dashboard() {
    const [projects, setProjects] = useState([]);
    const [name, setName] = useState("");
    const navigate = useNavigate();

    const loadProjects = async () => {
      try {
        const res = await API.get("/projects");
        setProjects(res.data || []);
      } catch {
        alert("Error loading projects");
      }
    };

    useEffect(() => {
      loadProjects();
    }, []);

    const addProject = async () => {
      if (!name) {
        alert("Enter project name");
        return;
      }

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
        <h4 className="mb-3">Projects</h4>

        <div className="card p-3 mb-3">
          <input
            className="form-control"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Project name"
          />
          <button className="btn btn-primary mt-2" onClick={addProject}>
            Add Project
          </button>
        </div>

        {projects.length === 0 && <p>No projects yet</p>}

        {projects.map((p) => (
          <div className="card p-2 mb-2" key={p.id}>
            <strong>{p.name}</strong>

            <div className="mt-2">
              <button
                className="btn btn-success btn-sm me-2"
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
      </Layout>
    );
  }
