import React from 'react';
import { useLogin } from '../../hooks/authentication-hooks';

function Login() {
    const loginQuery = useLogin();

    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    function handleSubmit(event: React.FormEvent) {
        event.preventDefault();

        loginQuery.mutate({ email, password });
    }

    return (
    <form onSubmit={handleSubmit}>
                    <label htmlFor="login-email">Email</label>
                    <input
                        type="text"
                        id="login-email"
                        value={email}
                        onChange={(event) => {
                            setEmail(event.target.value);
                        }}
                    />

                    <label htmlFor="login-password">Password</label>
                    <input
                        type="password"
                        id="login-password"
                        value={password}
                        onChange={(event) => {
                            setPassword(event.target.value);
                        }}
                    />

                <button>Submit!</button>
            </form>
    );
}

export default Login;