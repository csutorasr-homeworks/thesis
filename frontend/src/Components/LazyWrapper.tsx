import React, { Suspense } from 'react';

import Loading from './Loading';

export default function LazyWrapper({
  module,
}: {
  module: () => Promise<{ default: React.ComponentType<unknown> }>;
}): JSX.Element {
  const Component = React.lazy(module);
  return (
    <Suspense fallback={<Loading />}>
      <Component />
    </Suspense>
  );
}
