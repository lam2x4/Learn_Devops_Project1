import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const getTodos = async ({ search = '', status = 'all', sortBy = 'createdAt', sortOrder = 'desc', page = 1, pageSize = 10 } = {}) => {
    const response = await api.get('/todos/search', {
        params: { search, status, sortBy, sortOrder, page, pageSize },
    });
    return response.data; // { items, totalCount, page, pageSize, totalPages }
};

export const createTodo = async (todo) => {
    const response = await api.post('/todos', todo);
    return response.data;
};

export const updateTodo = async (id, todo) => {
    const response = await api.put(`/todos/${id}`, todo);
    return response.data;
};

export const deleteTodo = async (id) => {
    const response = await api.delete(`/todos/${id}`);
    return response.data;
};

export default api;
