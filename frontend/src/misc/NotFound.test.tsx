import NotFound from './NotFound';
import React from 'react';
import { render } from '@testing-library/react';

test('renders 404 text', () => {
  const { getByText } = render(<NotFound />);
  const linkElement = getByText(/az oldal nem található/i);
  expect(linkElement).toBeInTheDocument();
});
