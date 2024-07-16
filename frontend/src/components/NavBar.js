import React, { useContext } from "react";
import { Context } from "../index";
import { Button, Nav, Navbar as BootstrapNavbar } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import { NavLink } from "react-router-dom";
import { MAIN_ROUTE } from "../untils/consts";

const NavBar = observer(() => {
    const { user } = useContext(Context);
    return (
        <BootstrapNavbar bg="dark" variant="dark">
            <NavLink style={{ color: 'white' }} to={MAIN_ROUTE}>Главная страница</NavLink>
            {user.isAuth ?
                <Nav className="ml-auto" style={{ color: 'white' }}>
                    <Button variant={"outline-light"} onClick={() => user.setIsAuth(false)}>Выйти</Button>
                    <Button variant={"outline-light"} className="ml-2">Транзакции</Button>
                </Nav>
                :
                <Nav className="ml-auto" style={{ color: 'white' }}>
                    <Button variant={"outline-light"} onClick={() => user.setIsAuth(true)}>Авторизация</Button>
                </Nav>
            }
        </BootstrapNavbar>
    );
});

export default NavBar;
