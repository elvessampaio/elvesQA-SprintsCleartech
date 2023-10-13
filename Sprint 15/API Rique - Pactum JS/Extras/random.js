module.exports = {
  generateRandomName: () => {
    const alphabet = "abcdefghijklmnopqrstuvwxyz";
    let name = "";
    for (let i = 0; i < 10; i++) {
      const randomIndex = Math.floor(Math.random() * alphabet.length);
      name += alphabet[randomIndex];
    }
    return name;
  }
};