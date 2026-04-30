import { BrowserRouter, Routes, Route } from "react-router-dom";
import Dashboard from "./pages/Dashboard";
import ProjectBoard from "./pages/ProjectBoard";
import TaskDetail from "./pages/TaskDetail"; 

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/project/:id" element={<ProjectBoard />} />
        <Route path="/task/:id" element={<TaskDetail />} />
      </Routes>
    </BrowserRouter>
  );
}


export default App;
