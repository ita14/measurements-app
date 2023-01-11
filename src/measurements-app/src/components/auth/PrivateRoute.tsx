import React from 'react';
import { Navigate } from 'react-router-dom';
import { useKeycloak } from '@react-keycloak/web';

type PrivateRouteProps = {
  children: JSX.Element;
  navigateTo?: string;
};

function PrivateRoute({ children, navigateTo }: PrivateRouteProps) {
  const { keycloak } = useKeycloak();

  if (keycloak?.authenticated) {
    return children;
  }

  return <Navigate to={navigateTo ?? '/'} />;
}

export default PrivateRoute;
