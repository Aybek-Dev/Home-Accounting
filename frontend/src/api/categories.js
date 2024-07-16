export const getCategories = async () => {
    try {
      const response = await fetch('https://localhost:7193/category', {
        method: 'GET',
        credentials: 'include', 
      });
      if (!response.ok) {
        throw new Error('Failed to fetch categories');
      }
      const data = await response.json();
      return data;
    } catch (error) {
      console.error('Error fetching categories:', error);
      return [];
    }
  };
  
  export const deleteCategory = async (id) => {
    try {
      const response = await fetch(`https://localhost:7193/category/${id}`, {
        method: 'DELETE',
        credentials: 'include', 
      });
      if (!response.ok) {
        throw new Error('Failed to delete category');
      }
      return true;
    } catch (error) {
      console.error('Error deleting category:', error);
      return false;
    }
  };
  
  export const createCategory = async (name) => {
    try {
      const response = await fetch('https://localhost:7193/category', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name }),
        credentials: 'include', 
      });
      if (!response.ok) {
        throw new Error('Failed to create category');
      }
      return true;
    } catch (error) {
      console.error('Error creating category:', error);
      return false;
    }
  };
  