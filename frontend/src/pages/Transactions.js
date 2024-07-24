import React, { useEffect, useState } from 'react';
import { getTransactions, createTransaction, deleteTransaction } from '../api/transactions';
import { getCategories } from '../api/categories';
import TransactionForm from '../components/TransactionForm';
import TransactionsTable from '../components/TransactionsTable';
import TransactionFilter from '../components/TransactionFilter';

const Transactions = () => {
  const [transactions, setTransactions] = useState([]);
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchTransactions = async () => {
      try {
        const data = await getTransactions();
        setTransactions(data);
      } catch (error) {
        console.error('Error fetching transactions:', error);
      }
    };

    const fetchCategories = async () => {
      try {
        const data = await getCategories();
        setCategories(data);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    };

    fetchTransactions();
    fetchCategories();
  }, []);

  const handleCreateTransaction = async (transaction) => {
    const success = await createTransaction(transaction);
    if (success) {
      const data = await getTransactions();
      setTransactions(data);
      alert('Transaction created successfully');
    } else {
      alert('Failed to create transaction');
    }
  };

  const handleFilter = (filteredTransactions) => {
    setTransactions(filteredTransactions);
  };

  const handleDelete = async (id) => {
    try {
      const success = await deleteTransaction(id);
      if (success) {
        setTransactions(transactions.filter((transaction) => transaction.id !== id));
        alert('Transaction deleted successfully');
      } else {
        alert('Failed to delete transaction');
      }
    } catch (error) {
      console.error('Error deleting transaction:', error);
      alert('Failed to delete transaction');
    }
  };

  return (
    <div>
      <h2>Transactions</h2>
      <TransactionForm categories={categories} onCreateTransaction={handleCreateTransaction} />
      <TransactionFilter onFilter={handleFilter} />
      <TransactionsTable transactions={transactions} deleteTransaction={handleDelete} />
    </div>
  );
};

export default Transactions;
