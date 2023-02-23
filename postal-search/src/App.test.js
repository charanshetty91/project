import { render, screen } from '@testing-library/react';
import App from './App';

test('Home page label test', () => {
  render(<App />);
  const linkElement = screen.getByText(/Enter postal code here.../i);
  expect(linkElement).toBeInTheDocument();
});
