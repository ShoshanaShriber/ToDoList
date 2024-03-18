import axios from 'axios';
axios.defaults.baseURL = 'http://localhost:5157/';
export default {
  getTasks: async () => {
    const result = await axios.get(`/`)    
    return result.data;
  },
  addTask: async(name)=>{
  
    const result = await axios.post(`add/${name}`)    
    return result.data;
  },

  setCompleted: async(id, isComplete)=>{

    const result = await axios.put(`put/${id}/${isComplete}`)    
    return {};
  },

  deleteTask:async(id)=>{

    const result = await axios.delete(`delete/${id}`)    
    return result.data;
  }
};
//טיפול בשגיאות
axios.interceptors.response.use(
  response => response,
  error => {
    console.error('Error:', error);
    return Promise.reject(error);
  }
);
