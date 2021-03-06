import React from "react";
import { Route, Redirect } from "react-router-dom";

import { authenticationService } from "./authentication.service";

export const PrivateRoute = ({ component: Component, roles, ...rest }) => (
  <Route
    {...rest}
    render={props => {
      const currentUser = authenticationService.currentUserValue;
      if (!currentUser) {
        // not logged in so redirect to login page with the return url
        return (
          <Redirect
            to={{ pathname: "/login", state: { from: props.location } }}
          />
          //<PrivateRoute exact path="/login" component={LawPageUser} />
        );
      }
      // decode(currentUser.token)[Jwt.Token]
      // check if route is restricted by role

      if (roles && roles.indexOf(currentUser.role) === -1) {
        // role not authorised so redirect to home page
        return <Redirect to={{ pathname: "/" }} />;
      }

      // if (roles && roles.indexOf(decode(currentUser.token)[Jwt.Token]) === -1) {
      // role not authorised so redirect to home page
      // return <Redirect to={{ pathname: '/'}} />
      // }

      // authorised so return component
      return <Component {...props} />;
    }}
  />
);
export default PrivateRoute;
