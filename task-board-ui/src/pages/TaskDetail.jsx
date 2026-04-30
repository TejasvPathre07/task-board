import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import API from "../services/api";
import Layout from "../components/Layout";

export default function TaskDetail() {
  const { id } = useParams();

  const [task, setTask] = useState(null);
  const [comments, setComments] = useState([]);
  const [text, setText] = useState("");

  const loadData = async () => {
    try {
      const t = await API.get(`/tasks/${id}`);
      const c = await API.get(`/tasks/${id}/comments`);

      setTask(t.data);
      setComments(c.data || []);
    } catch {
      alert("Error loading task");
    }
  };

  useEffect(() => {
    loadData();
  }, [id]);

  const addComment = async () => {
    if (!text) {
      alert("Enter comment");
      return;
    }

    await API.post(`/tasks/${id}/comments`, {
      author: "User",
      body: text
    });

    setText("");
    loadData();
  };

  const deleteComment = async (cid) => {
    if (!window.confirm("Delete comment?")) return;

    await API.delete(`/comments/${cid}`);
    loadData();
  };

  return (
    <Layout>
      <h4>{task?.title}</h4>
      <p>{task?.description}</p>

      <hr />

      <h5>Comments</h5>

      <input
        className="form-control mb-2"
        value={text}
        onChange={(e) => setText(e.target.value)}
        placeholder="Write comment"
      />

      <button className="btn btn-primary mb-3" onClick={addComment}>
        Add Comment
      </button>

      {comments.length === 0 && <p>No comments</p>}

      {comments.map((c) => (
        <div className="card p-2 mb-2" key={c.id}>
          <strong>{c.author}</strong>: {c.body}

          <button
            className="btn btn-danger btn-sm mt-2"
            onClick={() => deleteComment(c.id)}
          >
            Delete
          </button>
        </div>
      ))}
    </Layout>
  );
}
