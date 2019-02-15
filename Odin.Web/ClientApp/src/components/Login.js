import React, { Component } from 'react';
import { FormControl, FormGroup, ControlLabel, Button, Alert } from 'react-bootstrap';

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            message: ""
        };
    }

    validateForm() {
        return this.state.email.length > 0 && this.state.password.length > 0;
    }

    handleChange = event => {
        this.setState({
            [event.target.id]: event.target.value
        });
    }

    handleSubmit = event => {
        

        fetch('api/User/Login',
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userName: this.state.email,
                    password: this.state.password
                })
            })
            .then((response) => response.json())
            .then(data => {
                alert(JSON.stringify(data));
                if (data.success == true) {
                    this.props.history.push("/");
                } else {
                    this.setState({
                        message: data.message
                    });
                }
            });

        event.preventDefault();
    }

    renderMessage() {
        if (this.state.message) {
            return <Alert bsStyle="danger"><strong>{this.state.message}</strong></Alert>
        }        
    }

    render() {
        return (
            <div className="Login">
                <form onSubmit={this.handleSubmit}>
                    <FormGroup controlId="email" bsSize="large">
                        <ControlLabel>E-Mail / Benutzername</ControlLabel>
                        <FormControl
                            autoFocus
                            type="email"
                            value={this.state.email}
                            onChange={this.handleChange}
                        />
                    </FormGroup>
                    <FormGroup controlId="password" bsSize="large">
                        <ControlLabel>Passwort</ControlLabel>
                        <FormControl
                            value={this.state.password}
                            onChange={this.handleChange}
                            type="password"
                        />
                    </FormGroup>
                    <Button
                        block
                        bsSize="large"
                        disabled={!this.validateForm()}
                        type="submit"
                    >
                        Login
                    </Button>

                    {this.renderMessage()}                    
                </form>
            </div>
        );
    }
}