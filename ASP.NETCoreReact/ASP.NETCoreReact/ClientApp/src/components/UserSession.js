import React, { Component } from "react";

export class UserSession extends Component {
    static displayName = UserSession.name;

    constructor(props) {
        super(props);
        this.state = { userSessionInfo: {}, loading: true };
    }

    componentDidMount() {
        this.fetchUserSessionInfo();
    }

    static renderUserSessionTable(userSession) {
        console.log(userSession)
        return (
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Claim Type</th>
                    <th>Claim Value</th>
                    <th>Claim role</th>
                </tr>
                </thead>
                <tbody>
                {userSession.map((claim) => (
                    <tr key={claim.type}>
                        <td>{claim.type}</td>
                        <td>{claim.value}</td>
                        <td>{claim.role}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading ? (
            <p>
                <em>Loading...</em>
            </p>
        ) : (
            UserSession.renderUserSessionTable(this.state.userSessionInfo)
        );

        return (
            <div>
                <h1 id="tabelLabel">User Session</h1>
                <p>This pages shows the current user's session.</p>
                {contents}
            </div>
        );
    }

    async fetchUserSessionInfo() {
        const response = await fetch("bff/user", {
            headers: {
                "X-CSRF": 1,
            },
        });
        const data = await response.json();
        console.log(data);
        this.setState({ userSessionInfo: data, loading: false });
    }
}