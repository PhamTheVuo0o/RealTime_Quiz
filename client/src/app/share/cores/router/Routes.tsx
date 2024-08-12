import {AuthLayout, LayoutWithoutAuth} from "@ShareLayouts";
import { createBrowserRouter, RouteObject } from "react-router-dom";
import React from 'react';
import {RouterAppEnum} from '@ShareEnums'

const Home = React.lazy(() => import('@Pages').then(module => ({ default: module.Home })));
const Test = React.lazy(() => import('@Pages').then(module => ({ default: module.Test })));
const Help = React.lazy(() => import('@Pages').then(module => ({ default: module.Help })));
const E404 = React.lazy(() => import('@Pages').then(module => ({ default: module.E404 })));
const E500 = React.lazy(() => import('@Pages').then(module => ({ default: module.E500 })));
const Login = React.lazy(() => import('@Pages').then(module => ({ default: module.Login })));

export const routes: RouteObject[] = [
    {
        path: RouterAppEnum.Base,
        element: <AuthLayout />,
        children:[
            {path: RouterAppEnum.HomeWithUserId,  element: <Home />},
            {path: RouterAppEnum.Home,  element: <Home />},
            {path: RouterAppEnum.Test, element: <Test />},
            {path: RouterAppEnum.Help, element: <Help />},
        ]
    },
    {
        path: RouterAppEnum.Base,
        element: <LayoutWithoutAuth />,
        children:[
            {path: RouterAppEnum.Login, element: <Login />},
        ]
    },
    { path: RouterAppEnum.E500, element: <E500/> },
    { path: RouterAppEnum.E404, element: <E404/> }
];

export const Router = createBrowserRouter(routes);