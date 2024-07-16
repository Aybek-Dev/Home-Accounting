import React, { useEffect, useState } from 'react';
import { getCategories, deleteCategory, createCategory } from '../api/categories';
import CategoryForm from '../components/CategoryForm';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';

const Categories = () => {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await getCategories();
        setCategories(data);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    };

    fetchCategories();
  }, []);

  const handleDelete = async (id) => {
    try {
      const success = await deleteCategory(id);
      if (success) {
        setCategories(categories.filter((category) => category.id !== id));
      } else {
        alert('Failed to delete category');
      }
    } catch (error) {
      console.error('Error deleting category:', error);
      alert('Failed to delete category');
    }
  };

  const handleCreateCategory = async (name) => {
    try {
      const success = await createCategory(name);
      if (success) {
        const updatedCategories = await getCategories();
        setCategories(updatedCategories);
        alert('Category created successfully');
      } else {
        alert('Failed to create category');
      }
    } catch (error) {
      console.error('Error creating category:', error);
      alert('Failed to create category');
    }
  };

  return (
    <div className="container">
      <h2>Categories</h2>
      <CategoryForm onCreateCategory={handleCreateCategory} />
      <ListGroup>
        {categories.map((category) => (
          <ListGroup.Item key={category.id} className="d-flex justify-content-between align-items-center">
            {category.name}
            <Button variant="danger" onClick={() => handleDelete(category.id)}>Delete</Button>
          </ListGroup.Item>
        ))}
      </ListGroup>
    </div>
  );
};

export default Categories;
