import { render } from '@testing-library/react';
import React from 'react';

import NotFound from '.';

test('renders 404 text', () => {
  const { getByText } = render(<NotFound />);
  const linkElement = getByText(/az oldal nem található/i);
  expect(linkElement).toBeInTheDocument();
});
