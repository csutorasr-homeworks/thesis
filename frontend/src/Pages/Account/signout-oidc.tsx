import React, { useContext, useEffect, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';

import { AuthContext } from '../../auth/AuthModule';

export default function SignOutOIDC(): JSX.Element {
  const { userManager } = useContext(AuthContext);
  const [loading, setLoading] = useState(false);
  const history = useHistory();
  useEffect(() => {
    (async () => {
      if (loading) {
        return;
      }
      try {
        setLoading(true);
        await userManager?.signoutRedirectCallback();
        history.push('/account/login');
      } catch (e) {
        setLoading(false);
      }
    })();
  }, [history, loading, userManager]);
  return (
    <>
      {(loading && 'Processing response.') || (
        <>
          Could not process response.
          <Link to="/account/login">Go to login</Link>
        </>
      )}
    </>
  );
}
