import React from 'react';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';

const TransactionsTable = ({ transactions, deleteTransaction }) => {
  const formatAmount = (amount) => {
    return amount.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ' ');
  };

  const formatDate = (dateString) => {
    const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
    const date = new Date(dateString);
    return date.toLocaleDateString('ru-RU', options);
  };

  const totalIncome = transactions
    .filter((transaction) => transaction.type === 1)
    .reduce((sum, transaction) => sum + transaction.amount, 0);

  const totalExpense = transactions
    .filter((transaction) => transaction.type === 0)
    .reduce((sum, transaction) => sum + transaction.amount, 0);

  const balance = totalIncome - totalExpense;

  return (
    <>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Тип</th>
            <th>Категория</th>
            <th>Дата</th>
            <th>Сумма</th>
            <th>Комментарии</th>
            <th>Действия</th>
          </tr>
        </thead>
        <tbody>
          {transactions.map((transaction) => (
            <tr key={transaction.id}>
              <td align="right">{transaction.type === 0 ? 'Расход' : 'Доход'}</td>
              <td align="right">{transaction.categoryName}</td>
              <td align="right">{formatDate(transaction.createdAt)}</td>
              <td align="right" style={{ color: transaction.type === 0 ?  'red' : 'green' }}>{formatAmount(transaction.amount)}</td>
              <td align="right">{transaction.title}</td>
              <td align="right">
                <Button variant="danger" onClick={() => deleteTransaction(transaction.id)}>Удалить</Button>
              </td>
            </tr>
          ))}
          <tr>
            <td colSpan="5" align="right">Итого</td>
            <td align="right" style={{ color: balance >= 0 ? 'green' : 'red' }}>
              {formatAmount(balance)}
            </td>
          </tr>
        </tbody>
      </Table>
    </>
  );
};

export default TransactionsTable;
