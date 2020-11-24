import { render } from '@testing-library/react';
import React from 'react';

import NotFound from '.';

test('renders 404 text', () => {
  const { getByText } = render(<NotFound />);
  const linkElement = getByText(/page is not found/i);
  expect(linkElement).toBeInTheDocument();
});
