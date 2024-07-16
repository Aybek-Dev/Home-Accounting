export const createTransaction = async (transaction) => {
    try {
      const response = await fetch('https://localhost:7193/transaction', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(transaction),
        credentials: 'include',
      });
      if (!response.ok) {
        throw new Error('Failed to create transaction');
      }
      return true;
    } catch (error) {
      console.error('Error creating transaction:', error);
      return false;
    }
  };
  
  export const getTransactions = async () => {
    try {
      const response = await fetch('https://localhost:7193/transaction', {
        method: 'GET',
        credentials: 'include',
      });
      if (!response.ok) {
        throw new Error('Failed to fetch transactions');
      }
      const data = await response.json();
      return data;
    } catch (error) {
      console.error('Error fetching transactions:', error);
      return [];
    }
  };
  
  export const deleteTransaction = async (id) => {
    try {
      const response = await fetch(`https://localhost:7193/transaction/${id}`, {
        method: 'DELETE',
        credentials: 'include',
      });
      if (!response.ok) {
        throw new Error('Failed to delete transaction');
      }
      return true;
    } catch (error) {
      console.error('Error deleting transaction:', error);
      return false;
    }
  };
  