import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';

const TransactionForm = ({ categories, onCreateTransaction }) => {
  const [transactionType, setTransactionType] = useState(0);
  const [createDate, setCreateDate] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [amount, setAmount] = useState('');
  const [title, setTitle] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    const selectedCategory = categories.find(category => category.id === categoryId);
    const formattedDate = createDate ? new Date(createDate).toISOString() : new Date().toISOString();

    const transaction = {
      transactionType,
      createDate: formattedDate,
      category: selectedCategory,
      amount: parseFloat(amount),
      title,
    };

    await onCreateTransaction(transaction);

    // Сброс значений формы после отправки
    setTransactionType(0);
    setCreateDate('');
    setCategoryId('');
    setAmount('');
    setTitle('');
  };

  return (
    <Form onSubmit={handleSubmit} style={{ marginBottom: '20px' }}>
      <Form.Group className="mb-3">
        <Form.Label>Тип транзакции</Form.Label>
        <Form.Select value={transactionType} onChange={(e) => setTransactionType(parseInt(e.target.value))}>
          <option value={0}>Расход</option>
          <option value={1}>Доход</option>
        </Form.Select>
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Дата</Form.Label>
        <Form.Control
          type="date"
          value={createDate}
          onChange={(e) => setCreateDate(e.target.value)}
          required
        />
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Категория</Form.Label>
        <Form.Select value={categoryId} onChange={(e) => setCategoryId(e.target.value)} required>
          <option value="">Выберите категорию</option>
          {categories.map((category) => (
            <option key={category.id} value={category.id}>{category.name}</option>
          ))}
        </Form.Select>
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Сумма</Form.Label>
        <Form.Control
          type="number"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          placeholder="Сумма"
          required
        />
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Комментарии</Form.Label>
        <Form.Control
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Комментарии"
        />
      </Form.Group>
      <Button variant="primary" type="submit">Создать транзакцию</Button>
    </Form>
  );
};

export default TransactionForm;
