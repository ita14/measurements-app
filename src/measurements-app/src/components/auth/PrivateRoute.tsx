import React from 'react';
//import { Navigate } from 'react-router-dom';

function PrivateRoute({ children }: React.PropsWithChildren<unknown>) {
  //return user?.isAdmin ? <>{children}</> : <Navigate to="/" />;
  return <>{children}</>;
}

export default PrivateRoute;
