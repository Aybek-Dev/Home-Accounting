import React, { useState, useEffect } from "react";
import { Form, Button } from "react-bootstrap";
import axios from "axios";

const MainPage = () => {
  const [createDate, setCreateDate] = useState(new Date().toISOString().slice(0, 16));
  const [transactionType, setTransactionType] = useState("EXPENSE");
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState("");
  const [amount, setAmount] = useState("");
  const [comment, setComment] = useState("");

  // Загрузка категорий с бекенда при монтировании компонента
  useEffect(() => {
    axios.get("https://localhost:7193/category")
      .then(response => {
        setCategories(response.data);
        setSelectedCategory(response.data[0]?.id); // Установка первой категории по умолчанию
      })
      .catch(error => {
        console.error("Error fetching categories:", error);
      });
  }, []);

  // Обработка отправки формы
  const handleSubmit = (event) => {
    event.preventDefault();
    const transactionData = {
      TransactionType: transactionType,
      CreateDate: new Date(createDate),
      Category: categories.find(cat => cat.id === selectedCategory),
      Amount: parseFloat(amount),
      Title: comment
    };
    // Здесь можно отправить данные на бекенд
    console.log("Submitting transaction data:", transactionData);
  };

  return (
    <div>
      <h2>Добавление транзакции</h2>
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="formCreateDate">
          <Form.Label>Дата добавления</Form.Label>
          <Form.Control
            type="datetime-local"
            value={createDate}
            onChange={(e) => setCreateDate(e.target.value)}
            required
          />
        </Form.Group>

        <Form.Group controlId="formTransactionType">
          <Form.Label>Доход/расход</Form.Label>
          <Form.Control
            as="select"
            value={transactionType}
            onChange={(e) => setTransactionType(e.target.value)}
            required
          >
            <option value="EXPENSE">Расход</option>
            <option value="INCOME">Доход</option>
          </Form.Control>
        </Form.Group>

        <Form.Group controlId="formCategory">
          <Form.Label>Категория</Form.Label>
          <Form.Control
            as="select"
            value={selectedCategory}
            onChange={(e) => setSelectedCategory(e.target.value)}
            required
          >
            {categories.map(category => (
              <option key={category.id} value={category.id}>{category.name}</option>
            ))}
          </Form.Control>
        </Form.Group>

        <Form.Group controlId="formAmount">
          <Form.Label>Сумма</Form.Label>
          <Form.Control
            type="number"
            step="0.01"
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
            required
          />
        </Form.Group>

        <Form.Group controlId="formComment">
          <Form.Label>Комментарий</Form.Label>
          <Form.Control
            as="textarea"
            rows={3}
            value={comment}
            onChange={(e) => setComment(e.target.value)}
          />
        </Form.Group>

        <Button variant="primary" type="submit">
          Создать транзакцию
        </Button>
      </Form>
    </div>
  );
};

export default MainPage;
