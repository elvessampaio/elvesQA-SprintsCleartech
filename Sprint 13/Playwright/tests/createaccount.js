function generateRandomEmail() {
    const randomString = Math.random().toString(36).substring(7);
    return `test_${randomString}@ecommerce.com`; 
  }
  
  // Função para gerar uma senha aleatória
  function generateRandomPassword() {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let password = '';
    const passwordLength = 8; // Defina o comprimento da senha desejada
  
    for (let i = 0; i < passwordLength; i++) {
      const randomIndex = Math.floor(Math.random() * characters.length);
      password += characters.charAt(randomIndex);
    }
  
    return password;
  }
  
  module.exports = {
    generateRandomEmail,
    generateRandomPassword,
  };