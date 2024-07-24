import React, { useState, useEffect } from 'react';
import { filterTransactions } from '../api/transactions';
import { getCategories } from '../api/categories';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const TransactionFilter = ({ onFilter }) => {
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [transactionType, setTransactionType] = useState('');
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

  const handleFilter = async (e) => {
    e.preventDefault();
    const selectedCategory = categories.find(category => category.id === categoryId);
    const filterParams = {
      startDate: startDate ? new Date(startDate).toISOString() : null,
      endDate: endDate ? new Date(endDate).toISOString() : null,
      category: selectedCategory,
      transactionType: transactionType !== '' ? parseInt(transactionType) : null,
    };

    const filteredTransactions = await filterTransactions(filterParams);
    onFilter(filteredTransactions);
  };

  return (
    <Form onSubmit={handleFilter} style={{ marginBottom: '20px' }}>
      <Row>
      <Col>
        <Form.Group className="mb-3 me-3">
          <Form.Label>Дата начала</Form.Label>
          <Form.Control
            type="date"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
          />
        </Form.Group>
        </Col>
        <Col>
        <Form.Group className="mb-3 me-3">
          <Form.Label>Дата окончания</Form.Label>
          <Form.Control
            type="date"
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
          />
        </Form.Group>
        </Col>
        <Col>
        <Form.Group className="mb-3 me-3">
          <Form.Label>Категория</Form.Label>
          <Form.Select value={categoryId} onChange={(e) => setCategoryId(e.target.value)} required>
            <option value="">Выберите категорию</option>
            {categories.map((category) => (
              <option key={category.id} value={category.id}>{category.name}</option>
            ))}
          </Form.Select>
        </Form.Group>
        </Col>
        <Col>
        <Form.Group className="mb-3 me-3">
          <Form.Label>Тип транзакции</Form.Label>
          <Form.Select value={transactionType} onChange={(e) => setTransactionType(e.target.value)}>
            <option value=''>Все</option>
            <option value={0}>Расход</option>
            <option value={1}>Доход</option>
          </Form.Select>
        </Form.Group>
        </Col>
      </Row>
      <Button variant="primary" type="submit" className="float-end">Фильтровать</Button>
    </Form>
  );
};

export default TransactionFilter;
