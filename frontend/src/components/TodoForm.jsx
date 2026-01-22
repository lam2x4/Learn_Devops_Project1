import { useState } from 'react';

const TodoForm = ({ onAdd }) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [priority, setPriority] = useState(1); // 0: Low, 1: Medium, 2: High, 3: Urgent

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!title.trim()) return;
        onAdd({ title, description, priority: parseInt(priority) });
        setTitle('');
        setDescription('');
        setPriority(1);
    };

    return (
        <form onSubmit={handleSubmit} className="todo-form">
            <input
                type="text"
                placeholder="What needs to be done?"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                className="input-title"
                required
            />
            <input
                type="text"
                placeholder="Description (optional)"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                className="input-description"
            />
            <select
                value={priority}
                onChange={(e) => setPriority(e.target.value)}
                className="select-priority"
            >
                <option value={0}>🟢 Low Priority</option>
                <option value={1}>🟡 Medium Priority</option>
                <option value={2}>🟠 High Priority</option>
                <option value={3}>🔴 Urgent</option>
            </select>
            <button type="submit" className="btn-add">
                Add Task
            </button>
        </form>
    );
};

export default TodoForm;
