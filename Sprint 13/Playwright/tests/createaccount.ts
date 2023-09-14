function generateRandomEmail(): string {
  const randomString: string = Math.random().toString(36).substring(7);
  return `test_${randomString}@ecommerce.com`;
}

function generateRandomPassword(): string {
  const characters: string = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let password: string = '';
  const passwordLength: number = 8; 

  for (let i: number = 0; i < passwordLength; i++) {
    const randomIndex: number = Math.floor(Math.random() * characters.length);
    password += characters.charAt(randomIndex);
  }

  return password;
}

export {
  generateRandomEmail,
  generateRandomPassword,
};