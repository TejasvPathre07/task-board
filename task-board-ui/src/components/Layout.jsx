export default function Layout({ children }) {
  return (
    <div className="container mt-4">
      <h2 className="text-center mb-4">Task Board App</h2>
      {children}
    </div>
  );
}