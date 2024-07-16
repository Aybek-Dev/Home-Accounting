import Auth from "./pages/Auth"
import MainPage from "./pages/MainPage"
import TransactionPage from "./pages/TransactionPage"
import { LOGIN_ROUTE, MAIN_ROUTE, REGISTRATION_ROUTE, TRANSACTION_ROUTE } from "./untils/consts"

export const authRoutes = [
    {
        path: MAIN_ROUTE,
        Component: MainPage
    },
    {
        path: TRANSACTION_ROUTE,
        Component: TransactionPage
    }
]

export const publicRoutes = [
    {
        path: LOGIN_ROUTE,
        Component: Auth
    },
    {
        path: REGISTRATION_ROUTE,
        Component: Auth
    }
]