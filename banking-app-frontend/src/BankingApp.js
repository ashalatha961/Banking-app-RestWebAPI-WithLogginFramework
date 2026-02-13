import React, { useState, useEffect } from "react";
import axios from "axios";

const BankingApp = () => {
    const [account, setAccount] = useState(null);
    const [id, setId] = useState(1);
    const [amount, setAmount] = useState("");

    useEffect(() => {
        axios.get("https://localhost:7071/BankingApp/GetAccountDetailsByID?id=1" + id)
            .then(response => setAccount(response.data))
            .then(() => axios.get(`https://localhost:7071/BankingApp/GetAccountDetailsByID?id=${id}`))
            .catch(error => console.error(error));
    }, []);

    const handleGetAccount = () => {
        axios.get(`https://localhost:7071/BankingApp/GetAccountDetailsByID?id=${id}`)
            .then(response => setAccount(response.data))
            .catch(error => console.error(error));
    }
    const handleDeposit = () => {
        axios.post("https://localhost:7071/BankingApp/UpdatedAccountAfterDeposit", parseFloat(amount))
            .then(response => setAccount(response.data))
            .catch(error => console.error(error));
    };

    const handleWithdraw = () => {
        axios.post("https://localhost:7071/BankingApp/UpdatedAccountAfterWithdraw", parseFloat(amount))
            .then(response => setAccount(response.data))
            .catch(error => console.error(error));
    };
    const handleTransfer = () => {
        axios.post("https://localhost:7071/BankingApp/UpdatedAccountAfterTransfer", parseFloat(amount))
            .then(response => setAccount(response.data))
            .catch(error => console.error(error));
    };

    if (!account) return <div>Loading...</div>;

    return (
        <div>
            <h1>Banking App</h1>
            <p>Account Holder: {account.AccountHolder}</p>
            <p>Balance: ${account.Balance}</p>
            <input
                type="number"
                value={amount}
                onChange={(e) => setAmount(e.target.value)}
                placeholder="Enter amount"
            />
            <input
                type="number"
                value={id}
                onChange={(e) => setId(e.target.value)}
                placeholder="Enter Account ID"
            />
            <button onClick={handleGetAccount}>Get Account</button>
            <button onClick={handleDeposit}>Deposit</button>
            <button onClick={handleWithdraw}>Withdraw</button>
            <button onClick={handleTransfer}>Transfer</button>
        </div>
    );
};

export default BankingApp;
