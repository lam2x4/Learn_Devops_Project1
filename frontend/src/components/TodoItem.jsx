const TodoItem = ({ todo, onToggle, onDelete }) => {
    return (
        <div className={`todo-item ${todo.isCompleted ? 'completed' : ''}`}>
            <div className="todo-content">
                <input
                    type="checkbox"
                    checked={todo.isCompleted}
                    onChange={() => onToggle(todo.id, !todo.isCompleted)}
                    className="checkbox"
                />
                <div className="todo-text">
                    <h3 className="todo-title">{todo.title}</h3>
                    {todo.description && <p className="todo-description">{todo.description}</p>}
                </div>
            </div>
            <button onClick={() => onDelete(todo.id)} className="btn-delete">
                Delete
            </button>
        </div>
    );
};

export default TodoItem;
