import React, { Component } from 'react';
import { Col, Grid, Row } from 'react-bootstrap';

export class PlainLayout extends Component {
    displayName = PlainLayout.name

    render() {
        return (
            <Grid fluid>
                <Row>                
                    <Col sm={12}>
                        {this.props.children}
                    </Col>
                </Row>
            </Grid>
        );
    }
}
