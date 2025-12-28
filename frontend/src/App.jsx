import { useState, useEffect } from 'react';
import { getTodos, createTodo, updateTodo, deleteTodo } from './services/api';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';
import './index.css';

function App() {
  const [todos, setTodos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchTodos();
  }, []);

  const fetchTodos = async () => {
    try {
      const data = await getTodos();
      setTodos(data);
      setError(null);
    } catch (err) {
      setError('Failed to fetch todos. Is the backend running?');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleAddTodo = async (newTodo) => {
    try {
      const created = await createTodo(newTodo);
      setTodos([created, ...todos]);
    } catch (err) {
      setError('Failed to create todo.');
      console.error(err);
    }
  };

  const handleToggleTodo = async (id, isCompleted) => {
    try {
      const todoToUpdate = todos.find((t) => t.id === id);
      await updateTodo(id, { ...todoToUpdate, isCompleted });
      setTodos(
        todos.map((t) =>
          t.id === id ? { ...t, isCompleted } : t
        )
      );
    } catch (err) {
      setError('Failed to update todo.');
      console.error(err);
    }
  };

  const handleDeleteTodo = async (id) => {
    if (!window.confirm('Are you sure?')) return;
    try {
      await deleteTodo(id);
      setTodos(todos.filter((t) => t.id !== id));
    } catch (err) {
      setError('Failed to delete todo.');
      console.error(err);
    }
  };

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>🚀 Task Master</h1>
        <p>Stay organized, stay productive.</p>
      </header>

      <div className="main-content">
        <TodoForm onAdd={handleAddTodo} />

        {error && <div className="error-message">{error}</div>}

        {loading ? (
          <div className="loading">Loading tasks...</div>
        ) : (
          <TodoList
            todos={todos}
            onToggle={handleToggleTodo}
            onDelete={handleDeleteTodo}
          />
        )}
      </div>
    </div>
  );
}

export default App;
