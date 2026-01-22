const TodoItem = ({ todo, onToggle, onDelete }) => {
    const getPriorityLabel = (priority) => {
        const labels = {
            0: { text: 'Low', emoji: '🟢', class: 'priority-low' },
            1: { text: 'Medium', emoji: '🟡', class: 'priority-medium' },
            2: { text: 'High', emoji: '🟠', class: 'priority-high' },
            3: { text: 'Urgent', emoji: '🔴', class: 'priority-urgent' }
        };
        return labels[priority] || labels[1];
    };

    const priorityInfo = getPriorityLabel(todo.priority);

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
                    <div className="todo-header">
                        <h3 className="todo-title">{todo.title}</h3>
                        <span className={`priority-badge ${priorityInfo.class}`}>
                            {priorityInfo.emoji} {priorityInfo.text}
                        </span>
                    </div>
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
