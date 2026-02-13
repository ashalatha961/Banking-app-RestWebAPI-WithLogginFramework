import React from 'react';
import BankingApp from './BankingApp';

const App = () => {
  const [showBankingApp, setShowBankingApp] = React.useState(false);

  return (
    <div>
      <h1>Welcome to the Banking App</h1>
      <button onClick={() => setShowBankingApp(true)}>Go to Banking App</button>
      {showBankingApp && <BankingApp />}
    </div>
  );
}

export default App;