import { useState, useEffect } from 'react';
import { getTodos, createTodo, updateTodo, deleteTodo } from './services/api';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';
import './index.css';

function App() {
  const [todos, setTodos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [filter, setFilter] = useState('all');
  const [searchTerm, setSearchTerm] = useState('');
  const [page, setPage] = useState(1);
  const [pageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);

  useEffect(() => {
    fetchTodos({ reset: false });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [filter, searchTerm, page]);

  const fetchTodos = async ({ reset = false } = {}) => {
    if (reset) setPage(1);
    try {
      const data = await getTodos({
        search: searchTerm,
        status: filter,
        sortBy: 'createdAt',
        sortOrder: 'desc',
        page: reset ? 1 : page,
        pageSize,
      });
      setTodos(data.items ?? []);
      setTotalPages(data.totalPages ?? 1);
      setTotalCount(data.totalCount ?? data.items?.length ?? 0);
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
      // Refresh from server to keep pagination in sync
      await fetchTodos({ reset: true });
    } catch (err) {
      setError('Failed to create todo.');
      console.error(err);
    }
  };

  const handleToggleTodo = async (id, isCompleted) => {
    try {
      const todoToUpdate = todos.find((t) => t.id === id);
      await updateTodo(id, { ...todoToUpdate, isCompleted });
      await fetchTodos();
    } catch (err) {
      setError('Failed to update todo.');
      console.error(err);
    }
  };

  const handleDeleteTodo = async (id) => {
    if (!window.confirm('Are you sure?')) return;
    try {
      await deleteTodo(id);
      await fetchTodos();
    } catch (err) {
      setError('Failed to delete todo.');
      console.error(err);
    }
  };

  const handleClearCompleted = async () => {
    const completedIds = todos.filter((t) => t.isCompleted).map((t) => t.id);
    if (completedIds.length === 0) return;
    if (!window.confirm(`Clear ${completedIds.length} completed task(s)?`)) return;
    try {
      await Promise.all(completedIds.map((id) => deleteTodo(id)));
      await fetchTodos();
    } catch (err) {
      setError('Failed to clear completed tasks.');
      console.error(err);
    }
  };

  const completedCount = todos.filter((t) => t.isCompleted).length;
  const activeCount = totalCount - completedCount;

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>🚀 Task Master</h1>
        <p>Stay organized, stay productive.</p>
      </header>

      <div className="main-content">
        <TodoForm onAdd={handleAddTodo} />

        <div className="toolbar">
          <div className="filters">
            {['all', 'active', 'completed'].map((f) => (
              <button
                key={f}
                className={`btn-filter ${filter === f ? 'active' : ''}`}
                onClick={() => setFilter(f)}
                type="button"
              >
                {f.charAt(0).toUpperCase() + f.slice(1)}
              </button>
            ))}
          </div>

          <input
            type="search"
            placeholder="Search tasks..."
            value={searchTerm}
            onChange={(e) => {
              setSearchTerm(e.target.value);
              setPage(1);
            }}
            className="input-search"
          />

          <div className="toolbar-actions">
            <div className="counts">
              <span>{activeCount} open</span>
              <span>{completedCount} done</span>
              <span>{totalCount} total</span>
            </div>
            <button
              className="btn-secondary"
              type="button"
              onClick={handleClearCompleted}
              disabled={completedCount === 0}
            >
              Clear completed
            </button>
          </div>
        </div>

        {error && <div className="error-message">{error}</div>}

        {loading ? (
          <div className="loading">Loading tasks...</div>
        ) : (
          <>
            <TodoList
              todos={todos}
              onToggle={handleToggleTodo}
              onDelete={handleDeleteTodo}
            />
            {totalPages > 1 && (
              <div className="pagination">
                <button
                  className="btn-secondary"
                  type="button"
                  onClick={() => setPage((p) => Math.max(1, p - 1))}
                  disabled={page === 1}
                >
                  Prev
                </button>
                <span className="page-info">Page {page} / {totalPages}</span>
                <button
                  className="btn-secondary"
                  type="button"
                  onClick={() => setPage((p) => Math.min(totalPages, p + 1))}
                  disabled={page === totalPages}
                >
                  Next
                </button>
              </div>
            )}
          </>
        )}
      </div>
    </div>
  );
}

export default App;
