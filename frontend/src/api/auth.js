export const login = async (email, password) => {
    const response = await fetch('https://localhost:7193/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email, password }),
      credentials: 'include', // Это позволяет отправлять куки
    });
    return response.ok;
  };
  
  export const register = async (userName, email, password) => {
    const response = await fetch('https://localhost:7193/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ userName, email, password }),
    });
    return response.ok;
  };
  