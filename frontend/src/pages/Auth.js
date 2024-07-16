import React from "react";
import { Button, Card, Container, Form, FormControl, Row } from "react-bootstrap";
import { NavLink, useLocation } from "react-router-dom";
import { LOGIN_ROUTE, REGISTRATION_ROUTE } from "../untils/consts";

const Auth = () =>{
    const location = useLocation()
    const isLogin = location.pathname === LOGIN_ROUTE
    return(
        <Container 
            className="d-flex justify-content-centre align-items-center"
            style={{height:window.innerHeight-54}}
        >
            <Card style={{width:600}} className='p-5'>
                <h2 className="m-auto">{isLogin ? 'Аторизация' : 'Регистрация'}</h2>
                <Form className="d-flex flex-column">
                    <FormControl
                    className="mt-3"
                    placeholder="Введите ваш email..."
                    />
                    <FormControl
                    className="mt-3"
                    placeholder="Введите ваш пароль..."
                    />
                    <Row className="d-flex justify-content-between mt-3 pl-3 pr-3">
                        {isLogin ? 
                        <div>
                            Нет аккаунта? <NavLink to={REGISTRATION_ROUTE}>Зарегистрируйтесь!</NavLink>
                        </div>
                        :
                        <div>
                            Есть аккаунт? <NavLink to={LOGIN_ROUTE}>Войдите</NavLink>
                        </div>
                        }
                        <Button 
                            variant={"outline-succes"}
                        >
                            {isLogin? 'Войти':'Регистрация'}
                        </Button>
                    </Row>
                    
                </Form>
            </Card>
            
        </Container>
    );
};

export default Auth;